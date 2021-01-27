#r "../_lib/Fornax.Core.dll"

type Page = { Title: string; Link: string }

let loader (projectRoot: string) (siteContent: SiteContents) =
    siteContent.Add({ Title = "Home"; Link = "/" })

    siteContent.Add({ Title = "Docs"; Link = "/docs.html" })

    siteContent
