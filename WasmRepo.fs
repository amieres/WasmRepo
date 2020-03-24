module WasmRepo

open System
open System.IO

let rec dir (d:string) =
    Console.WriteLine "dir:::"
    try 
        Console.WriteLine d
        if d = "/proc/self/fd" then Console.WriteLine "skip /proc/self/fd"  else
        Console.WriteLine "before for"
        for file in Directory.GetFiles(d, "*") do
            Console.WriteLine file  
        for subdir in Directory.EnumerateDirectories d do
            Console.WriteLine subdir
            dir               subdir
    with e -> Console.WriteLine e.Message 

