module WasmRepo

open System
open System.IO

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

let thisWorksThisDoesnt() =
    try 
        Console.WriteLine "This works"
        printfn "This does too"
        XXX.print "so does this"
        XXX.print2 "this"
        Console.WriteLine XXX.hello
        Console.WriteLine (newFileName())
        printfn "but this %d" 0
        printfn "or this %A" 0
        printfn "or this %A" "doesn't"
        printfn "or this %s" "doesn't"
    with e -> 
        Console.Write "**** THIS FAILS:"
        Console.Write e.Message

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

let mutable foo = false

if foo then 
    doFibosTimed ""
    dir ""
    thisWorksThisDoesnt()