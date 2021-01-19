#r "../_lib/Fornax.Core.dll"
#r "../_lib/Markdig.dll"

open Markdig

let contentDir = "content"

type SimpleDoc = { name: string; content: string }
type IndexDoc = { content: string }

type DocData =
    { name: string
      docFiles: List<SimpleDoc>
      index: IndexDoc }

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
                        { name = fileName file
                          content = Markdown.ToHtml(content file, markdownPipeline) })
                |> fun files ->
                    let getIndexFile =
                        let indexExists =
                            files |> Array.exists (fun v -> v.name = "INDEX")

                        match indexExists with
                        | true ->
                            files
                            |> Array.find (fun v -> v.name = "INDEX")
                            |> fun v -> Markdown.ToHtml(v.content, markdownPipeline)
                        | false -> ""

                    let filterIndexFile =
                        files |> Array.filter (fun v -> v.name <> "INDEX")

                    { name = dirName dirPath
                      docFiles = filterIndexFile |> Array.toList
                      index = { content = getIndexFile } }
                |> fun data ->
                    if not data.docFiles.IsEmpty then
                        siteContent.Add data)

    getAllDocsWithSubDirs postsPath

    siteContent
