// Daily Programmer Challenge #214: Chester, the Greedy Pomeranian
// https://www.reddit.com/r/dailyprogrammer/comments/3629st
// 2015-08-17

open System

let Difficulty = "Hard"

let OutStream = new System.IO.StreamWriter(Difficulty+"Data/Output.txt", false)
System.Console.SetOut(OutStream) |> ignore
let sw = System.Diagnostics.Stopwatch.StartNew()
let lines = List.ofSeq (System.IO.File.ReadAllLines (Difficulty+"Data/Input.txt"))

type Coordinate = {X:double; Y:double}
let Radius (a:Coordinate) (b:Coordinate) = (a.X-b.X)**2.0 + (a.Y-b.Y)**2.0 

let ParseCoords (c:string) =
    let parts = c.Split ' '
    if parts.Length <> 2 then None else

    let hasParsedLeft, left = Double.TryParse parts.[0]
    let hasParsedRight, right = Double.TryParse parts.[1]

    if hasParsedLeft && hasParsedRight then Some {X = left; Y = right} else None

let rec GetDistance pos list =
    let min = list |> List.minBy (Radius pos)

    match list with
        | head when head.Length >= 2 -> sqrt(Radius pos min) + GetDistance min (head |> List.except [min])
        | head -> sqrt(Radius pos head.Head)
        
let startCoords = {X = 0.5; Y = 0.5}

match lines with
| [] -> printfn "Error: Input List is empty."
| countStr::tail ->
    let hasParsed, count = System.Int32.TryParse countStr
    match hasParsed with
    | false -> printfn "Error: Unable to parse input length."
    | true when count > 1 && tail.Length = count ->
        let coords = tail |> List.map ParseCoords

        if coords |> List.forall (fun i -> i.IsNone)
            then printfn "Error: One or more invalid coordinates."
        else
            let distance = coords |> List.map (fun i -> i.Value) |> GetDistance startCoords
            printfn "%f" distance
    | _ -> printfn "Error: Input data length mismatch."

sw.Stop()
printfn "Time: %O" sw.Elapsed
OutStream.Close()
OutStream.Dispose()