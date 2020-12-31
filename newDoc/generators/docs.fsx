#r "../_lib/Fornax.Core.dll"
#if !FORNAX
#load "../loaders/postloader.fsx"
#load "../loaders/pageloader.fsx"
#load "../loaders/globalloader.fsx"
#endif

open Html

#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"

open Html


let generate' (ctx: SiteContents) (page: string) =
    let posts =
        ctx.TryGetValues<Postloader.Post>()
        |> Option.defaultValue Seq.empty

    let psts =
        posts
        |> Seq.sortByDescending Layout.published
        |> Seq.toList
        |> List.map (Layout.postLayout true)

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

          aside [ Class "menu" ] [
              p [ Class "menu-label accordion" ] [
                  string "Fabulous"
              ]
              ul [ Class "menu-list accordion-panel" ] [
                  li [] [ a [] [ string "Coming soon" ] ]
              ]
              p [ Class "menu-label accordion" ] [
                  string "Fabulous.XamarinForms"
              ]
              ul [ Class "menu-list accordion-panel" ] [
                  li [] [ a [] [ string "Overview" ] ]
                  li [] [
                      a [] [ string "Getting Started" ]
                  ]
                  li [] [
                      p [ Class "menu-label accordion" ] [
                          string "Views"
                      ]
                      ul [ Class "menu-list accordion-panel" ] [
                          li [] [
                              a [] [ string "Basic Elements" ]
                          ]
                          li [] [ a [] [ string "Pages" ] ]
                          li [] [ a [] [ string "Layouts" ] ]
                          li [] [
                              a [] [ string "Lists and Tables" ]
                          ]
                          li [] [ a [] [ string "Gestures" ] ]
                          li [] [ a [] [ string "Pop-ups" ] ]
                          li [] [ a [] [ string "ViewRefs" ] ]
                          li [] [ a [] [ string "Animations" ] ]
                          li [] [
                              a [] [ string "Performance hints" ]
                          ]
                          li [] [
                              a [] [
                                  string "Multi-page applications and Navigation"
                              ]
                          ]
                          li [] [ a [] [ string "Styling" ] ]
                          li [] [ a [] [ string "Effects" ] ]
                          li [] [ a [] [ string "Extensions" ] ]
                          li [] [
                              a [] [
                                  string "Extensions: FFImageLoading (image caching)"
                              ]
                          ]
                          li [] [
                              a [] [
                                  string "Extensions: Maps (platform maps)"
                              ]
                          ]
                          li [] [
                              a [] [
                                  string "Extensions: SkiaSharp (drawing 2D graphics)"
                              ]
                          ]
                          li [] [
                              a [] [
                                  string "Extensions: OxyPlot (charting)"
                              ]
                          ]
                          li [] [
                              a [] [
                                  string "Extensions: VideoManager (audio and video)"
                              ]
                          ]
                      ]
                  ]
                  li [] [ a [] [ string "Models" ] ]
                  li [] [
                      a [] [ string "Update and Messages" ]
                  ]
                  li [] [
                      a [] [ string "Traces and Crashes" ]
                  ]
                  li [] [ a [] [ string "Unit Testing" ] ]
                  li [] [ a [] [ string "Tools" ] ]
                  li [] [
                      a [] [
                          string "Pitfalls and F# 5.0 support"
                      ]
                  ]
                  li [] [
                      a [] [
                          string "Migration guide from v0.57 to v0.60"
                      ]
                  ]
                  li [] [
                      a [] [ string "Further Resources" ]
                  ]
                  li [] [
                      a [] [ string "They use Fabulous" ]
                  ]
              ]
          ]

          main [] [
              div [ Class "container" ] [
                  section [ Class "articles" ] [
                      div [ Class "column is-8 is-offset-2" ] psts
                  ]
              ]
          ] ]

let generate (ctx: SiteContents) (projectRoot: string) (page: string) = generate' ctx page |> Layout.render ctx
