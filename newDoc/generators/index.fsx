#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"

open Html

let generate' (ctx: SiteContents) (_: string) =
    let posts =
        ctx.TryGetValues<Postloader.Post>()
        |> Option.defaultValue Seq.empty

    let siteInfo = ctx.TryGetValue<Globalloader.SiteInfo>()

    let ttl =
        siteInfo
        |> Option.map (fun si -> si.title)
        |> Option.defaultValue ""

    let desc =
        siteInfo
        |> Option.map (fun si -> si.description)
        |> Option.defaultValue ""

    let psts =
        posts
        |> Seq.sortByDescending Layout.published
        |> Seq.toList
        |> List.map (Layout.postLayout true)

    Layout.layout
        ctx
        "Home"
        [ section [ Class "hero is-info is-medium is-bold" ] [
            div [ Class "hero-body" ] [
                div [ Class "container has-text-centered" ] [
                    h1 [ Class "title" ] [ !!ttl ]
                    h2 [ Class "subtitle" ] [ !!desc ]
                ]
            ]
          ]

          div [ Class "container" ] [
              h1 [ Class "title" ] [
                  string "Get started easily"
              ]
              section [ Class "columns" ] [
                  div [ Class "column" ] [
                      string "Choose your preferred UI Framework"
                      image [] [
                          img [ Src "https://cdn.worldvectorlogo.com/logos/xamarin.svg" ]
                      ]
                      string "Don't see your favorite framework ?"
                      a [] [
                          string "Learn how to add support for Fabulous"
                      ]
                  ]
                  div [ Class "column" ] [
                      string "Install the project template"
                      div [] [
                          string "dotnet new -i Fabulous.XamarinForms.Templates"
                      ]
                      string "Create a project"
                      div [] [
                          string "dotnet new fabulous-xf-app --name MyApp"
                      ]
                      div [] [
                          string "* Creates Android and iOS projects by default."
                          string "WPF, UWP, GTK and macOS are also available as flags."
                      ]
                      button [] [ string "Learn more..." ]
                  ]
              ]
          ]
          hr []
          div [ Class "container is-light" ] [
              section [ Class "columns" ] [
                  div [ Class "column" ] [
                      string "Safe and simple logic with MVU"

                      string
                          "Structure applications with clear separation of concerns between logic and display with the popular Model-View-Update architecture."
                      button [] [ string "Learn more..." ]
                  ]
                  div [ Class "column" ] [
                      image [] [
                          img [ Src "images/update-sample.png" ]
                      ]
                  ]
              ]
          ]
          hr []
          div [ Class "container is-dark" ] [
              section [ Class "columns" ] [
                  div [ Class "column" ] [
                      image [] [
                          img [ Src "images/view-sample.png" ]
                      ]
                      a [] [
                          string "Prefer XAML? Learn more about static views"
                      ]
                  ]
                  div [ Class "column" ] [
                      string "Declarative F# views"

                      string
                          "Declare your UI without worrying about what happens next. Fabulous will ask for a new view each time the state changes but will only apply the delta to the actual UI."
                      button [] [ string "Learn more..." ]
                  ]
              ]
          ]
          hr []
          div [ Class "container is-light" ] [
              section [ Class "columns" ] [
                  div [ Class "column" ] [
                      string "Fully unit-testable, including the UI"

                      string
                          "Take advantage of functional programming to ensure your app behaves like you expect. MVU encourages putting side-effects outside of the main loop, perfect for unit testing."
                      button [] [ string "Learn more..." ]
                  ]
                  div [ Class "column" ] [
                      image [] [
                          img [ Src "images/unittest-sample.png" ]
                      ]
                      a [] [
                          string "Prefer XAML? Learn more about static views"
                      ]
                  ]
              ]
          ]
          hr []
          div [ Class "container is-dark" ] [
              section [ Class "columns" ] [
                  div [ Class "column" ] [
                      image [] [
                          img [ Src "images/live-update.png" ]
                      ]
                  ]
                  div [ Class "column" ] [
                      string "Save time with LiveUpdate"

                      string
                          "You don't need to restart the app anymore to see your changes. LiveUpdate will recompile your code when saving and automatically reload the app while it's running."
                      button [] [ string "Learn more..." ]
                  ]
              ]
          ]

          div [] [] ]

let generate (ctx: SiteContents) (projectRoot: string) (page: string) = generate' ctx page |> Layout.render ctx
