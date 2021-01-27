#r "../_lib/Fornax.Core.dll"
#r "../_lib/Markdig.dll"

open Markdig

let contentDir = "content"

type SimpleDoc = { Name: string; Content: string }
type IndexDoc = { Content: string }

type DocData =
    { Name: string
      DocFiles: List<SimpleDoc>
      Index: IndexDoc }

let markdownPipeline =
    MarkdownPipelineBuilder()
        .UsePipeTables()
        .UseGridTables()
        .Build()

let loader (projectRoot: string) (siteContent: SiteContents) =
    let postsPath =
        System.IO.Path.Combine(projectRoot, contentDir)

    let rec getAllDocsWithSubDirs dir =
        System.IO.Directory.GetDirectories dir
        |> Array.iter
            (fun dirPath ->
                let content file = System.IO.File.ReadAllText file

                let fileName (file: string) =
                    let filename =
                        file.Substring(file.LastIndexOf("/") + 1)

                    filename
                        .Substring(0, filename.LastIndexOf("."))
                        .ToUpper()

                let dirName (dirPath: string) =
                    dirPath.Substring(dirPath.LastIndexOf("/") + 1)

                if (System.IO.Directory.GetDirectories dirPath)
                    .Length > 0 then
                    System.IO.Directory.GetDirectories dirPath
                    |> Array.iter getAllDocsWithSubDirs

                dirPath
                |> System.IO.Directory.GetFiles
                |> Array.filter (fun file -> file.EndsWith ".md")
                |> Array.map
                    (fun file ->
                        { Name = fileName file
                          Content = Markdown.ToHtml(content file, markdownPipeline) })
                |> fun files ->
                    let getIndexFile =
                        let indexExists =
                            files |> Array.exists (fun v -> v.Name = "INDEX")

                        match indexExists with
                        | true ->
                            files
                            |> Array.find (fun v -> v.Name = "INDEX")
                            |> fun v -> Markdown.ToHtml(v.Content, markdownPipeline)
                        | false -> ""

                    let filterIndexFile =
                        files |> Array.filter (fun v -> v.Name <> "INDEX")

                    { Name = dirName dirPath
                      DocFiles = filterIndexFile |> Array.toList
                      Index = { Content = getIndexFile } }
                |> fun data ->
                    if not data.DocFiles.IsEmpty then
                        siteContent.Add data)

    getAllDocsWithSubDirs postsPath

    // get main index
    System.IO.Directory.GetFiles postsPath
    |> Array.iter
        (fun file ->
            let content = System.IO.File.ReadAllText file

            let docsMainData =
                { Name = "Main" //postsPath + file
                  DocFiles = []
                  Index = { Content = Markdown.ToHtml(content, markdownPipeline) } }

            siteContent.Add(docsMainData))

    siteContent
