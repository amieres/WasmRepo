module WasmRepo

let compile(options:string) =
    System.Environment.SetEnvironmentVariable("FSHARP_COMPILER_BIN", "/tmp")
    let opts =  options.Split '\n' |> Array.filter (fun x -> x.Trim() <> "")
    printfn "FCS 34.0.1 options= [| "
    for op in opts do printfn "    %s" op
    printfn "|]"

    async {
        try
            printfn "before Compile"
            let! errors1, exitCode1 = FSharp.Compiler.SourceCodeServices.FSharpChecker.Create().Compile opts
            for e in errors1 do printfn "%A" e
            if exitCode1 <> 0 then printfn "exit = %d" exitCode1 
            printfn "Compiled!"
        with e -> printfn "Failed evalExpression: %A" e
    } |> Async.Start

open System
open System.IO

let rec dir d =
    try 
        if d = "/proc/self/fd" then printfn "skip /proc/self/fd"  else
        for file in Directory.GetFiles(d, "*") do
            printfn "F %s" file  
        for subdir in Directory.EnumerateDirectories d do
            printfn "D %s" subdir
            dir            subdir
    with e -> printfn "%s" e.Message 

open System.Reflection

let run(commandLine: string) =
    let args  = commandLine.Split ' '
    let path  = args.[0] 
    Environment.CurrentDirectory <- Path.GetDirectoryName path
    let bytes = File.ReadAllBytes path
    let asm   = Assembly.Load bytes //Assembly.LoadFrom(path)
    let parms = asm.EntryPoint.GetParameters() 
                |> Array.map(fun p -> if p.ParameterType.IsArray then args :> obj else null)
    asm.EntryPoint.Invoke(null, parms) 
    |> printfn "res = %A"
