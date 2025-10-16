module Page

open System
open Giraffe

module Utils =
    let endsWith (str2 : string) (str : string) = str.EndsWith str2
    let (|CSS|_|) = endsWith ".css"
    let (|JS|_|) = endsWith ".js"
    let (|HTML|_|) = endsWith ".html"

    let setContentHeader filePath =
        let contentType = setHttpHeader "Content-Type"
        match filePath with
        | CSS -> contentType "text/css"
        | JS -> contentType "text/javascript"
        | HTML -> contentType "text/html"
        | _ -> fun next ctx -> next ctx

open Utils    
let setPageFromFolder folderPath =
    let fileHandler filePath =   
        let url = 
            "/" +  match filePath with 
                    | HTML -> (IO.Directory.GetParent filePath).Name
                    | _ -> filePath

        route url
        >=> (IO.File.ReadAllBytes >> setBody) filePath 
        >=> setContentHeader filePath

    "pages/" + folderPath 
    |> IO.Directory.GetFiles
    |> Array.map fileHandler
    |> Array.toList
    |> choose

    

