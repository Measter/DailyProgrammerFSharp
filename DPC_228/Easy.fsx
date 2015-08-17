// Daily Programmer Challenge #228: Letters in Alphabetical Order
// https://www.reddit.com/r/dailyprogrammer/comments/3h9pde

let Difficulty = "Easy"

let OutStream = new System.IO.StreamWriter(Difficulty+"Data/Output.txt", false)
System.Console.SetOut(OutStream) |> ignore
let sw = System.Diagnostics.Stopwatch.StartNew()
let lines = List.ofSeq (System.IO.File.ReadAllLines (Difficulty+"Data/Input.txt")) 

type Order = Ordered | NotOrdered | Reverse

let InOrder = "IN ORDER"
let NotInOrder = "NOT IN ORDER"
let InReverseOrder = "REVERSE ORDER"

type Word = { Word:string; CharCodes:int list }

let ParseStrings s = { Word = s; CharCodes = [for ch in s do yield int (System.Char.ToLower ch) ] }
let IsOrdered l = match l with  
                    | _ when l = (l |> List.sort) -> Ordered
                    | _ when l = (l |> List.sort |> List.rev ) -> Reverse
                    | _ -> NotOrdered 

match lines with
| [] -> printfn "Error: Input List is empty."
| tail ->
    let parsed = tail |> List.map ParseStrings
    for w in parsed do
        match IsOrdered w.CharCodes with
            | Ordered -> printfn "%s %s" w.Word InOrder
            | NotOrdered -> printfn "%s %s" w.Word NotInOrder
            | Reverse -> printfn "%s %s" w.Word InReverseOrder
    
sw.Stop()
printfn "Time: %A" sw.Elapsed
OutStream.Close()
OutStream.Dispose()