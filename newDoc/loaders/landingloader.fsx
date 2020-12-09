#r "../_lib/Fornax.Core.dll"
#r "../_lib/Markdig.dll"

open Markdig

type Landing = {
    title: string
    description: string
    
}

let contentDir = "content/Landing"

let markdownPipeline =
    MarkdownPipelineBuilder()
        .UsePipeTables()
        .UseGridTables()
        .Build()

let isSeparator (input : string) =
    input.StartsWith "---"

let getConfig (fileContent : string) =
    let fileContent = fileContent.Split '\n'
    let fileContent = fileContent |> Array.skip 1 //First line must be ---
    let indexOfSeperator = fileContent |> Array.findIndex isSeparator
    let splitKey (line: string) = 
        let seperatorIndex = line.IndexOf(':')
        if seperatorIndex > 0 then
            let key = line.[.. seperatorIndex - 1].Trim().ToLower()
            let value = line.[seperatorIndex + 1 ..].Trim() 
            Some(key, value)
        else 
            None
    fileContent
    |> Array.splitAt indexOfSeperator
    |> fst
    |> Seq.choose splitKey
    |> Map.ofSeq

let trimString (str : string) =
    str.Trim().TrimEnd('"').TrimStart('"')