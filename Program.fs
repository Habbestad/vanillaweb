open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Giraffe

let webApp = 
    choose [
        Page.setFromFolder "index"
        route "/" >=> redirectTo true "index"
        
        Page.setFromFolder "page1"
    ]

let builder = WebApplication.CreateBuilder()
let app = builder.Build()

app.UseGiraffe webApp


[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder()
    builder.Services.AddGiraffe()
    |> ignore

    let app = builder.Build()
    app.UseGiraffe webApp

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