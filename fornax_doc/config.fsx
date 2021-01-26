#r "_lib/Fornax.Core.dll"

open Config
open System.IO

// let postPredicate (projectRoot: string, page: string) =
//     let fileName = Path.Combine(projectRoot, page)
//     let ext = Path.GetExtension page

//     if ext = ".md" then
//         let ctn = File.ReadAllText fileName
//         ctn.Contains("layout: post")
//     else
//         false

let staticPredicate (projectRoot: string, page: string) =
    let ext = Path.GetExtension page

    not (
        page.Contains "_public"
        || page.Contains "_bin"
        || page.Contains "_lib"
        || page.Contains "_data"
        || page.Contains "_settings"
        || page.Contains "_config.yml"
        || page.Contains ".sass-cache"
        || page.Contains ".git"
        || page.Contains ".ionide"
        || ext = ".fsx"
    )

let config =
    { Generators =
          [ { Script = "less.fsx"
              Trigger = OnFileExt ".less"
              OutputFile = ChangeExtension "css" }
            { Script = "sass.fsx"
              Trigger = OnFileExt ".scss"
              OutputFile = ChangeExtension "css" }
            { Script = "staticfile.fsx"
              Trigger = OnFilePredicate staticPredicate
              OutputFile = SameFileName }
            { Script = "index.fsx"
              Trigger = Once
              OutputFile = NewFileName "index.html" }
            { Script = "docs.fsx"
              Trigger = Once
              OutputFile = NewFileName "docs.html" } ] }
