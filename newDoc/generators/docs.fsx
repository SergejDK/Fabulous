#r "../_lib/Fornax.Core.dll"
#if !FORNAX
#load "../loaders/postloader.fsx"
#load "../loaders/pageloader.fsx"
#load "../loaders/globalloader.fsx"
#load "../loaders/docloader.fsx"
#endif

open Html

#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"


let generate' (ctx: SiteContents) (page: string) =
    let posts =
        ctx.TryGetValues<Postloader.Post>()
        |> Option.defaultValue Seq.empty

    let psts =
        posts
        |> Seq.sortByDescending Layout.published
        |> Seq.toList
        |> List.map (Layout.postLayout true)

    let docData =
        ctx.TryGetValues<Docloader.DocData>()
        |> Option.defaultValue Seq.empty

    let simpleDocMenuLayout (doc: Docloader.SimpleDoc) = li [] [ a [] [ string doc.name ] ]

    let docMenuLayout (doc: Docloader.DocData) =
        let listItems =
            doc.docFiles |> List.map simpleDocMenuLayout

        div [] [
            p [ Class "menu-list accordion" ] [
                string doc.name
            ]
            ul [ Class "menu-list accordion-panel" ] listItems
        ]

    let menuLayout =
        let entries =
            docData |> Seq.map docMenuLayout |> Seq.toList

        aside [ Class "menu" ] entries

    Layout.layout
        ctx
        "Docs"
        [

          section [ Class "hero is-dark page-banner" ] [
              div [ Class "hero-body has-text-centered" ] [
                  h1 [ Class "title is-1 is-spaced" ] [
                      string "Fabulous"
                  ]
                  h2 [ Class "subtitle is-2" ] [
                      string "Documentation"
                  ]
              ]
          ]

          menuLayout

          main [] [
              div [ Class "container" ] [
                  section [ Class "articles" ] [
                      div [ Class "column is-8 is-offset-2" ] psts
                  ]
              ]
          ] ]

let generate (ctx: SiteContents) (projectRoot: string) (page: string) = generate' ctx page |> Layout.render ctx
