#r "../_lib/Fornax.Core.dll"
#if !FORNAX
#load "../loaders/postloader.fsx"
#load "../loaders/pageloader.fsx"
#load "../loaders/globalloader.fsx"
#load "../loaders/docfolderloader.fsx"
#endif

open Html


let injectWebsocketCode (webpage: string) =
    let websocketScript = """
        <script type="text/javascript">
          var wsUri = "ws://localhost:8080/websocket";
      function init()
      {
        websocket = new WebSocket(wsUri);
        websocket.onclose = function(evt) { onClose(evt) };
      }
      function onClose(evt)
      {
        console.log('closing');
        websocket.close();
        document.location.reload();
      }
      window.addEventListener("load", init, false);
      </script>
        """

    let head = "<head>"
    let index = webpage.IndexOf head
    webpage.Insert((index + head.Length + 1), websocketScript)

let layout (ctx: SiteContents) active bodyCnt =
    let siteInfo = ctx.TryGetValue<Globalloader.SiteInfo>()

    let ttl =
        siteInfo
        |> Option.map (fun si -> si.title)
        |> Option.defaultValue ""

    let docPages =
        ctx.TryGetValues<string>()
        |> Option.defaultValue Seq.empty

    let docs =
        docPages
        |> Seq.map
            (fun v ->
                let dHref = sprintf "/docs.html#%s" v

                a [ Class "navbar-link is-arrowless"
                    Href dHref ] [
                    string v
                ])
        |> Seq.toList


    html [] [
        head [] [
            meta [ CharSet "utf-8" ]
            meta [ Name "viewport"
                   Content "width=device-width, initial-scale=1" ]
            title [] [ !!ttl ]
            link [ Rel "icon"
                   Type "image/png"
                   Sizes "32x32"
                   Href "images/favicon.ico" ]
            link [ Rel "stylesheet"
                   Href "https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css" ]
            link [ Rel "stylesheet"
                   Href "https://fonts.googleapis.com/css?family=Open+Sans" ]
            link [ Rel "stylesheet"
                   Href "https://unpkg.com/bulma@0.8.0/css/bulma.min.css" ]
            link [ Rel "stylesheet"
                   Type "text/css"
                   Href "style/style.css" ]
            script [ Src "js/sampleJsFile.js" ] []
        ]
        body [] [
            nav [ Class "navbar overlay-bar" ] [
                div [ Class "container" ] [
                    div [ Class "navbar-brand" ] [
                        a [ Class "navbar-item"; Href "/" ] [
                            img [ Src "images/logo-title-fabulous.png"
                                  Alt "Fabulous Logo" ]
                        ]
                        a [ Role "button"
                            Class "navbar-burger"
                            Custom("aria-label", "menu")
                            Custom("aria-expanded", "false")
                            Custom("data-target", "navbarMenu") ] [
                            span [ Custom("aria-hidden", "true") ] []
                            span [ Custom("aria-hidden", "true") ] []
                            span [ Custom("aria-hidden", "true") ] []
                        ]
                    ]
                    div [ Class "navbar-menu is-open"
                          Id "navbarMenu" ] [
                        div [ Class "navbar-start " ] [
                            div [ Class "navbar-item has-dropdown is-hoverable" ] [
                                a [ Class "navbar-link is-arrowless"
                                    Href "/docs.html" ] [
                                    string "Docs"
                                ]
                                div [ Class "navbar-dropdown is-boxed" ] docs
                            ]
                            a [ Class "navbar-item"
                                Href "/showcase" ] [
                                string "Showcase"
                            ]
                            a [ Class "navbar-item"
                                Href "https://github.com/jimbobbennett/awesome-fabulous" ] [
                                string "Community"
                            ]
                        ]
                        div [ Class "navbar-end" ] [
                            a [ Class "navbar-item"
                                HtmlProperties.Title "Fabulous on GitHub"
                                Href "https://github.com/fsprojects/Fabulous" ] [
                                i [ Class "fab fa-github" ] []
                            ]
                            a [ Class "navbar-item"
                                HtmlProperties.Title "Discuss and ask questions on Gitter"
                                Href "https://gitter.im/fsprojects/Fabulous" ] [
                                i [ Class "fab fa-gitter" ] []
                            ]
                            a [ Class "navbar-item"
                                HtmlProperties.Title "Discuss and ask questions on the F# Slack #mobiledev channel"
                                Href "https://fsharp.slack.com/messages/mobiledev/" ] [
                                i [ Class "fab fa-slack" ] []
                            ]
                        ]
                    ]
                ]
            ]
            yield! bodyCnt

            footer [ Class "footer" ] [
                div [ Class "container" ] [
                    div [ Class "level" ] [
                        div [ Class "level-left" ] [
                            div [ Class "level-item" ] [
                                div [ Class "footer-logo" ] [
                                    img [ Src "images/logo-title-fabulous.png" ]
                                ]
                            ]
                        ]
                        div [ Class "level-right" ] [
                            div [ Class "level-item" ] [
                                div [ Class "footer-column" ] [
                                    div [ Class "footer-header" ] [
                                        h3 [] [ string "Follow Us" ]
                                        div [] [
                                            a [] [ i [ Class "fab fa-github" ] [] ]
                                            a [] [ i [ Class "fab fa-twitter" ] [] ]
                                        ]
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]
    ]

let render (ctx: SiteContents) cnt =
    let disableLiveRefresh =
        // ctx.TryGetValue<Postloader.PostConfig>()
        // |> Option.map (fun n -> n.disableLiveRefresh)
        // |> Option.defaultValue false
        false

    cnt
    |> HtmlElement.ToString
    |> fun n ->
        if disableLiveRefresh then
            n
        else
            injectWebsocketCode n

// let published (post: Postloader.Post) =
//     post.published
//     |> Option.defaultValue System.DateTime.Now
//     |> fun n -> n.ToString("yyyy-MM-dd")

// let postLayout (useSummary: bool) (post: Postloader.Post) =
//     div [ Class "card article" ] [
//         div [ Class "card-content" ] [
//             div [ Class "media-content has-text-centered" ] [
//                 p [ Class "title article-title" ] [
//                     a [ Href post.link ] [ !!post.title ]
//                 ]
//                 p [ Class "subtitle is-6 article-subtitle" ] [
//                     a [ Href "#" ] [
//                         !!(defaultArg post.author "")
//                     ]
//                     !!(sprintf "on %s" (published post))
//                 ]
//             ]
//             div [ Class "content article-body" ] [
//                 !!(if useSummary then
//                        post.summary
//                    else
//                        post.content)
//             ]
//         ]
//     ]
