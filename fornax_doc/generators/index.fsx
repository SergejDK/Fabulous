#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"

open Html

let generate' (ctx: SiteContents) (_: string) =
    // let posts =
    //     ctx.TryGetValues<Postloader.Post>()
    //     |> Option.defaultValue Seq.empty

    let siteInfo = ctx.TryGetValue<Globalloader.SiteInfo>()

    let ttl =
        siteInfo
        |> Option.map (fun si -> si.title)
        |> Option.defaultValue ""

    let desc =
        siteInfo
        |> Option.map (fun si -> si.description)
        |> Option.defaultValue ""

    // let psts =
    //     posts
    //     |> Seq.sortByDescending Layout.published
    //     |> Seq.toList
    //     |> List.map (Layout.postLayout true)

    Layout.layout
        ctx
        "Home"
        [ section [ Class "hero is-dark page-banner" ] [
            div [ Class "hero-body has-text-centered" ] [
                h1 [ Class "title is-1 is-spaced" ] [
                    !!ttl
                ]
                h2 [ Class "subtitle is-2" ] [ !!desc ]
            ]
          ]

          section [ Class "hero is-dark" ] [
              div [ Class "hero-body has-text-centered" ] [


                  h1 [ Class "title is-spaced" ] [
                      string "Get started easily"
                  ]
                  div [ Class "container" ] [
                      div [ Class "columns is-desktop is-8 is-variable is-reveresed-table" ] [
                          div [ Class "column home-splash has-text-centered platforms-column" ] [
                              p [ Class "subtitle" ] [
                                  string "Choose your preferred UI Framework"
                              ]
                              div [ Class "platforms" ] [
                                  label [ Class "platform" ] [
                                      input [ Name "backend"
                                              Id "XamarinForms"
                                              Value "Xamarin.Forms"
                                              Type "radio" ]
                                      img [ Src "https://cdn.worldvectorlogo.com/logos/xamarin.svg" ]
                                      string "Xamarin.Forms"
                                  ]
                              ]
                              div [] [
                                  p [] [
                                      string "Don't see your favorite framework ?"
                                  ]
                                  a [] [
                                      string "Learn how to add support for Fabulous"
                                  ]
                              ]
                          ]
                          div [ Class "column has-text-left-desktop has-text-centered" ] [
                              p [ Class "subtitle" ] [
                                  string "Install the project template"
                              ]
                              div [ Class "command" ] [
                                  input [ ReadOnly true
                                          Value "dotnet new -i Fabulous.XamarinForms.Templates" ]
                              ]
                              p [ Class "subtitle" ] [
                                  string "Create a project"
                              ]
                              div [ Class "command" ] [
                                  input [ ReadOnly true
                                          Value "dotnet new fabulous-xf-app --name MyApp" ]
                              ]
                              p [ Class "subtitle is-7" ] [
                                  string
                                      "* Creates Android and iOS projects by default. <br/> WPF, UWP, GTK and macOS are also available as flags."
                              ]
                              div [ Class "level" ] [
                                  div [ Class "level-left" ] [
                                      div [ Class "level-item" ] [
                                          a [ Class "button is-medium" ] [
                                              string "Learn more..."
                                          ]
                                      ]
                                  ]
                              ]
                          ]
                      ]
                  ]
              ]
          ]
          div [ Class "hero is-white" ] [
              div [ Class "hero-body" ] [
                  div [ Class "container" ] [
                      div [ Class "columns is-desktop is-vcentered-tablet is-variable is-8" ] [
                          div [ Class "column has-text-right-desktop has-text-centered" ] [
                              h1 [ Class "title is-spaced" ] [
                                  string "Safe and simple logic with MVU"
                              ]
                              p [ Class "subtitle" ] [
                                  string
                                      "Structure applications with clear separation of concerns between logic and display with the popular Model-View-Update architecture."
                              ]
                              div [ Class "level" ] [
                                  div [] []
                                  div [ Class "level-right" ] [
                                      div [ Class "level-item" ] [
                                          a [ Class "button is-medium" ] [
                                              string "Learn more..."
                                          ]
                                      ]
                                  ]
                              ]
                          ]
                          div [ Class "column home-splash has-text-centered is-narrow" ] [
                              div [ Class "card" ] [
                                  div [ Class "card-content" ] [
                                      image [] [
                                          img [ Src "images/update-sample.png"
                                                Class "sample" ]
                                      ]
                                  ]
                              ]
                          ]
                      ]
                  ]
              ]
          ]

          div [ Class "hero is-dark" ] [
              div [ Class "hero-body" ] [
                  div [ Class "container" ] [
                      div [ Class "columns is-desktop is-vcentered-tablet is-variable is-8" ] [
                          div [ Class "column home-splash has-text-centered is-narrow" ] [
                              div [ Class "card" ] [
                                  div [ Class "card-content" ] [
                                      img [ Src "images/view-sample.png"
                                            Class "sample" ]
                                  ]
                              ]
                              a [] [
                                  string "Prefer XAML? Learn more about static views"
                              ]
                          ]
                          div [ Class "column has-text-left-desktop has-text-centered" ] [
                              h1 [ Class "title is-spaced" ] [
                                  string "Declarative F# views"
                              ]
                              p [ Class "subtitle" ] [
                                  string
                                      "Declare your UI without worrying about what happens next. Fabulous will ask for a new view each time the state changes but will only apply the delta to the actual UI."
                              ]
                              div [ Class "level" ] [
                                  div [ Class "level-left" ] [
                                      div [ Class "level-item" ] [
                                          a [ Class "button is-medium" ] [
                                              string "Learn more..."
                                          ]
                                      ]
                                  ]
                              ]
                          ]
                      ]
                  ]
              ]
          ]

          div [ Class "hero is-white" ] [
              div [ Class "hero-body" ] [
                  div [ Class "container" ] [
                      div [ Class "columns is-desktop is-vcentered-tablet is-variable is-8" ] [
                          div [ Class "column has-text-right-desktop has-text-centered" ] [
                              h1 [ Class "title is-spaced" ] [
                                  string "Fully unit-testable, including the UI"
                              ]
                              p [ Class "subtitle" ] [
                                  string
                                      "Take advantage of functional programming to ensure your app behaves like you expect. MVU encourages putting side-effects outside of the main loop, perfect for unit testing."
                              ]
                              div [ Class "level" ] [
                                  div [] []
                                  div [ Class "level-right" ] [
                                      div [ Class "level-item" ] [
                                          a [ Class "button is-medium" ] [
                                              string "Learn more..."
                                          ]
                                      ]
                                  ]
                              ]
                          ]
                          div [ Class "column home-splash has-text-centered is-narrow" ] [
                              div [ Class "card" ] [
                                  div [ Class "card-content" ] [
                                      image [] [
                                          img [ Src "images/unittest-sample.png"
                                                Class "sample" ]
                                      ]
                                  ]
                              ]
                          ]
                      ]
                  ]
              ]
          ]

          div [ Class "hero is-dark" ] [
              div [ Class "hero-body" ] [
                  div [ Class "container" ] [
                      div [ Class "columns is-desktop is-vcentered-tablet is-variable is-8" ] [
                          div [ Class "column home-splash has-text-centered is-narrow" ] [
                              div [ Class "card" ] [
                                  div [ Class "card-content" ] [
                                      img [ Src "images/live-update.png"
                                            Class "sample" ]
                                  ]
                              ]
                          ]
                          div [ Class "column has-text-left-desktop has-text-centered" ] [
                              h1 [ Class "title is-spaced" ] [
                                  string "Save time with LiveUpdate"
                              ]
                              p [ Class "subtitle" ] [
                                  string
                                      "You don't need to restart the app anymore to see your changes. LiveUpdate will recompile your code when saving and automatically reload the app while it's running."
                              ]
                              div [ Class "level" ] [
                                  div [ Class "level-right" ] [
                                      div [ Class "level-item" ] [
                                          a [ Class "button is-medium" ] [
                                              string "Learn more..."
                                          ]
                                      ]
                                  ]
                              ]
                          ]
                      ]
                  ]
              ]
          ]

          div [] [] ]

let generate (ctx: SiteContents) (projectRoot: string) (page: string) = generate' ctx page |> Layout.render ctx
