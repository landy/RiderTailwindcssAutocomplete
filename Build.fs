open Fake
open Fake.Core
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.Core.TargetOperators

open BuildHelpers
open BuildTools

initializeContext()

let publishPath = Path.getFullName "publish"
let srcPath = Path.getFullName "src"
let clientSrcPath = srcPath </> "TailwindAutocomplete.Client"
let serverSrcPath = srcPath </> "TailwindAutocomplete.Server"
let appPublishPath = publishPath </> "app"

// Targets
let clean proj = [ proj </> "bin"; proj </> "obj"; proj </> ".fable-build" ] |> Shell.cleanDirs

Target.create "Clean" (fun _ ->
    serverSrcPath |> clean
    clientSrcPath |> clean
)

Target.create "InstallClient" (fun _ ->
    printfn "Node version:"
    run Tools.node "--version" clientSrcPath
    printfn "Yarn version:"
    run Tools.yarn "--version" clientSrcPath
    run Tools.yarn "install --frozen-lockfile" clientSrcPath
)

Target.create "Publish" (fun _ ->
    [ appPublishPath ] |> Shell.cleanDirs
    let publishArgs = sprintf "publish -c Release -o \"%s\"" appPublishPath
    run Tools.dotnet publishArgs serverSrcPath
    [ appPublishPath </> "appsettings.Development.json" ] |> File.deleteAll

    run Tools.dotnet "fable clean --yes" ""
    run Tools.yarn "build" ""
)

Target.create "Run" (fun _ ->
    Environment.setEnvironVar "ASPNETCORE_ENVIRONMENT" "Development"
    [
        "server", Tools.dotnet "watch run" serverSrcPath
        "client", Tools.yarn "start" ""
    ]
    |> runParallel
)

let dependencies = [
    "InstallClient"
        ==> "Clean"
        ==> "Publish"

    "InstallClient"
        ==> "Clean"
        ==> "Run"
]

[<EntryPoint>]
let main args = runOrDefault "Run" args