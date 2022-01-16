module CommonUtil

open System
open System.Security.Cryptography
open System.Text.RegularExpressions
open Akka

let proc = System.Diagnostics.Process.GetCurrentProcess()
let mutable cpu_time_stamp = proc.TotalProcessorTime
let timer = System.Diagnostics.Stopwatch()

let gatorId = "pnarkar"
let mutable actorSystem = Unchecked.defaultof<Actor.ActorSystem>

type MessageType =
    | Mine
    | GenerateRandomString

type LogType = 
    | RandomStringLog
    | MineLog

type Message = {Type: MessageType; Value: String; mutable Nonce: int}

let randomStringGen n =
    let r = Random()
    let chars = Array.concat([[|'a' .. 'z'|];[|'A' .. 'Z'|];[|'0' .. '9'|]])
    let sz = Array.length chars in
    gatorId + ";" + String(Array.init n (fun _ -> chars.[r.Next sz]))

let computeSha256 (input : String) = 
    use sha256hash = SHA256Managed.Create();
    let inputBytes = System.Text.Encoding.ASCII.GetBytes(input)
    sha256hash.ComputeHash(inputBytes) |> Array.map (sprintf "%02X") |> String.concat ""
    
let checkPrefix (hash : String) (zeroCount : int) = 
    Regex.IsMatch(hash, "^0{"+zeroCount.ToString()+"}")

let printStats() =
    let cpu_time = (proc.TotalProcessorTime-cpu_time_stamp).TotalMilliseconds
    printfn "\n\n======================================================================\n\
    Current Stats\n\
    CPU time = %dms\n\
    Absolute time = %dms\n\
    ======================================================================\n" (int64 cpu_time) timer.ElapsedMilliseconds

let printCoin input coin hash nodeAddress= 
    printfn "\n\n======================================================================\n\
    New Coin found\n\
    Leading Zeros = %d\n\
    Coin = %s\n\
    Hash = %s\n\
    Actor = %s\n\
    ======================================================================\n" input coin hash nodeAddress

let initStatParams() = 
    cpu_time_stamp <- proc.TotalProcessorTime
    timer.Start()

let rec readInput() =
    let command = System.Console.ReadLine()
    
    match command with
    |   "printStats" ->
            printStats()
            readInput()
    | _ -> readInput()