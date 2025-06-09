module Page

open System
open Giraffe

module Utils =
    let endsWith (str2 : string) (str : string) = str.EndsWith str2
    let (|CSS|_|) = endsWith ".css"
    let (|JS|_|) = endsWith ".js"
    let (|HTML|_|) = endsWith ".html"

    let setContentHeader path =
        let contentType = setHttpHeader "Content-Type"
        match path with
        | CSS -> contentType "text/css"
        | JS -> contentType "text/javascript"
        | HTML -> contentType "text/html"
        | _ -> fun next ctx -> next ctx

    let fileHandler path =   
        let handler (url : string) = 
            if url.StartsWith "/" then route url else route ("/" + url)
            >=> (IO.File.ReadAllBytes >> setBody) path 
            >=> setContentHeader path

        match path with
        | HTML -> choose [
            handler (IO.Directory.GetParent path).Name
            handler path
            ]
        | _ -> handler path
    

open Utils    
let setFromFolder folderPath = 
    let handlers = 
        folderPath |> IO.Directory.GetFiles
                   |> Array.map fileHandler
                   |> Array.toList

    choose handlers
    

