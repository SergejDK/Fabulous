#r "../_lib/Fornax.Core.dll"
#r "../_lib/Markdig.dll"


let contentDir = "content"

type SimpleDoc = { name: string; content: string }

type DocData =
    { name: string
      docFiles: List<SimpleDoc> }

let loader (projectRoot: string) (siteContent: SiteContents) =
    let postsPath =
        System.IO.Path.Combine(projectRoot, contentDir)

    System.IO.Directory.GetDirectories postsPath
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

            dirPath
            |> System.IO.Directory.GetFiles
            |> Array.filter (fun file -> file.EndsWith ".md")
            |> Array.map
                (fun file ->
                    { name = fileName file
                      content = content file })
            |> fun files ->
                { name = dirName dirPath
                  docFiles = files |> Array.toList }
            |> siteContent.Add)

    siteContent
