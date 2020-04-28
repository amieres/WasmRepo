module WasmRepo

open System
open System.IO

let argError msg = printfn "excp: %s" msg ; failwith msg  // raise (ArgumentError msg)

open ICSharpCode.SharpZipLib

let testCompression() =
    try 
        use mem    = new System.IO.MemoryStream()
    (**)printfn "comp"    
        use comp   = new GZip.GZipOutputStream(mem)
        //use comp   = new System.IO.Compression.GZipStream(mem, System.IO.Compression.CompressionMode.Compress  )
    (**)printfn "wrt"    
        use wrt    = new System.IO.BinaryWriter(comp)
    (**)printfn "Write"    
        wrt.Write "Hello GZipStream"
    (**)printfn "Close"    
        wrt.Close()
    (**)printfn "mem2"    
        use mem2   = new System.IO.MemoryStream(mem.GetBuffer())
    (**)printfn "rdr"    
        use decomp = new GZip.GZipInputStream(mem2)
        //use decomp = new System.IO.Compression.GZipStream(mem2, System.IO.Compression.CompressionMode.Decompress)
        use rdr    = new System.IO.BinaryReader(decomp)
    (**)printfn "ReadString"    
        Console.WriteLine(rdr.ReadString())
    with e -> 
        Console.WriteLine "testCompression Error"
        Console.WriteLine e.Message
        Console.WriteLine e.StackTrace

let newFileName =
   System.Environment.SetEnvironmentVariable("FSHARP_COMPILER_BIN", "/tmp")
   let mutable counter = 0
   fun () -> 
       counter <- counter + 1
       "/tmp/app" + counter.ToString() + ".exe"


module XXX =
    let hello = "Hello"
    let print : string -> unit = Console.WriteLine
    let concatAnd v = "and " + v
    let print2 = concatAnd >> print

//let fail1() =
//    XXX.hello.GetType().GetMethodImpl

let getOps (options:string) =
    let opts =  options.Split '\n' |> Array.filter (fun x -> x.Trim() <> "")
    printfn "options = [| "
    for op in opts do printfn "    %s" op
    printfn "|]"
    opts


let thisWorksThisDoesnt() =
    try 
        Console.WriteLine "This works"
        testCompression()        
        printfn "This does too"
        XXX.print "so does this"
        XXX.print2 "this"
        Console.WriteLine XXX.hello
        Console.WriteLine (newFileName())
        printfn "but this %d" 0  // when FSharp.Core is AOT compiled this does not ever return (interpreted it works)
        printfn "or this %A"  0. // when above line fails this one doesn't print
        printfn "or this %A" "doesn't"
        printfn "or this %s" "doesn't"
        async { 
            printfn "async with no timer" 
        }
        |> Async.Start // and Async.RunSynchronously also worked
        async { 
            try
                printfn "async with timer before" 
                do! Async.Sleep 1000
                printfn "async with timer after" 
            with e -> printfn "Failed evalExpression: %A" e
        }
        |> Async.Start // and Async.RunSynchronously also worked
        //async { 
        //    do! Async.Sleep 1000
        //    printfn "async with timer & RunSynchronously" 
        //}
        //|> Async.RunSynchronously // error: "cannot wait for events on this runtime"
    with e -> 
        Console.Write "**** THIS FAILS:"
        Console.WriteLine e.Message 
        Console.WriteLine e.StackTrace
    printfn "after try ... with" // when line 31 fails this one doesn't print

let rec dir (d:string) =
    try 
        if d = "/proc/self/fd" then Console.WriteLine "skip /proc/self/fd"  else
        for file in Directory.GetFiles(d, "*") do
            Console.Write "F: "
            Console.WriteLine file  
        for subdir in Directory.EnumerateDirectories d do
            Console.Write "D: "
            Console.WriteLine subdir
            dir               subdir
    with e -> Console.WriteLine e.Message 

let nowStamp() = 
    let t = System.DateTime.UtcNow  //in two steps to avoid Warning: The value has been copied to ensure the original is not mutated
    t.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture)

let rec fibo = function
    | 0 | 1 -> 1
    | n -> fibo (n - 1) + fibo (n - 2)
    // fibo is slow (80s) interpreted, medium when mscorlib & WasmRepo are AOT (7s) & fast (3s) when FSharp.Core is compiled

let printFibo (n:int) = 
    Console.Write      "fibo(" 
    Console.Write       n 
    Console.Write       ") = "
    Console.WriteLine  (fibo n)

let tryParseWith tryParseFunc (s:string)  = tryParseFunc s |> function
    | true, v    -> Some v
    | false, _   -> None

let parseIntO = tryParseWith System.Int32.TryParse

let doFibos (args:string) =
    args.Split ' '
    |> Array.choose parseIntO
    |> Array.iter   printFibo

let inline TimeIt n f p =
    Console.WriteLine (nowStamp())
    let start = System.DateTime.UtcNow.Ticks
    f p
    let elapsedSpan = System.TimeSpan(System.DateTime.UtcNow.Ticks - start)
    Console.WriteLine (nowStamp())
    Console.WriteLine (elapsedSpan.ToString())

let doFibosTimed args = TimeIt "doFibos" doFibos args


let listFSCore () =
    
    let o1  = Microsoft.FSharp.Core.PrintfFormat<obj,obj,obj,obj>
    let t1  = o1.GetType()
    let asm = t1.Assembly
    
    Console.WriteLine (o1.ToString())

    let writeMethods (t:Type) =
        for m in t.GetMethods() do
            Console.Write "    "
            Console.WriteLine m.Name

    let writeTypes (asm:Reflection.Assembly) =
        for t in asm.GetTypes() do
            Console.WriteLine t.FullName
            if t.FullName = "Microsoft.FSharp.Core.PrintfImpl" then
                writeMethods t
    
    writeMethods t1
    Console.WriteLine "-----"
    writeTypes asm
    writeTypes typeof<Microsoft.FSharp.Core.PrintfFormat<obj,obj,obj,obj>>.Assembly
//let mutable foo = false
//
//if foo then 
//    doFibosTimed ""
//    dir ""
//    thisWorksThisDoesnt()

//let xxx = Microsoft.FSharp.Core.PrintfImpl.FormatString .intFromString  "5546"
