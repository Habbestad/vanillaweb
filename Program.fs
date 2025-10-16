open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Giraffe

let webApp = 
    choose [
        Page.setPageFromFolder "index"
        route "/" >=> redirectTo true "index"
        
        Page.setPageFromFolder "page1"
    ]


[<EntryPoint>]
let main args =
    //let webAppOptions = WebApplicationOptions ( WebRootPath = "public" )
    let builder = WebApplication.CreateBuilder () //webAppOptions

    builder.Services.AddGiraffe()
    |> ignore

    let app = builder.Build()
    // app.MapStaticAssets () |> ignore
    app.UseGiraffe webApp

    printfn "%A" app.Environment.WebRootPath

    app.Run()
                
    0 

// Note on .NET hosting
// IHost is the common ground for using built-in .NET configuration, DI (services), logging, and other things.
// Above we build an IHost through the WebApplicationBuilder : IHostApplicationBuilder (**)
// The Build() builds a WebApplication, which implements
//  * IApplicationBuilder - build request pipeline (middleware)
//  * IEndpointRouteBuilder - build endpoints
//  * IHost 
// (**) (IHostApplicationBuilder also has a default implementation HostApplicationBuilder which can be used
// for non-web apps.) 
// The IHost can be setup "directly" from IHostBuilder as the example below shows (just adding Giraffe)

//let configureWebHost (webHostBuilder : IWebHostBuilder ) = 
//    let configure (app : IApplicationBuilder) = 
//        app.UseGiraffe webApp
//    
//    let configureServices (services : IServiceCollection) = 
//        services.AddGiraffe() |> ignore
//    
//    webHostBuilder
//        .Configure(configure)
//        .ConfigureServices(configureServices)
//        |> ignore
//
//Host.CreateDefaultBuilder()
//    .ConfigureWebHostDefaults(configureWebHost)
//    .Build()
//    .Run()