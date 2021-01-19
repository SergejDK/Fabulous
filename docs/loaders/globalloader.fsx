#r "../_lib/Fornax.Core.dll"

type SiteInfo = { title: string; description: string }

let loader (projectRoot: string) (siteContent: SiteContents) =
    siteContent.Add
        ({ title = "Fabulous"
           description = "Mobile & Desktop Development with F# and MVU" })

    siteContent
