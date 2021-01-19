#r "../_lib/Fornax.Core.dll"
#r "../_lib/Markdig.dll"


let contentDir = "content"

let loader (projectRoot: string) (siteContent: SiteContents) =
    let docsPath =
        System.IO.Path.Combine(projectRoot, contentDir)

    System.IO.Directory.GetDirectories docsPath
    |> Array.map (fun v -> v.Substring(v.LastIndexOf("/") + 1))
    |> Array.iter siteContent.Add

    siteContent
