#r "../_lib/Fornax.Core.dll"

type SiteInfo = { Title: string; Description: string }

let loader (projectRoot: string) (siteContent: SiteContents) =
    siteContent.Add(
        { Title = "Fabulous"
          Description = "Mobile & Desktop Development with F# and MVU" }
    )

    siteContent
