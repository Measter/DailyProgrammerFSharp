// Daily Programmer Challenge #214
// https://www.reddit.com/r/dailyprogrammer/comments/35l5eo

let Difficulty = "Easy"

let OutStream = new System.IO.StreamWriter(Difficulty+"Data/Output.txt", false)
System.Console.SetOut(OutStream) |> ignore
let sw = System.Diagnostics.Stopwatch.StartNew()
let lines = List.ofSeq (System.IO.File.ReadAllLines (Difficulty+"Data/Input.txt")) 

let rec ParseIntList (ints:string list) =
    match ints with
    | this::rest ->
        let parsedThis, value = System.Int32.TryParse this
    
        if rest.Length > 0 then
            let parsedRest, restValues = ParseIntList rest
            (parsedThis && parsedRest, value :: restValues)
        else
            (parsedThis, [value])
    | _ -> failwith "Error: Empty list."

match lines with
| head when lines.Length = 1 ->
    let (hasParsed, values) = List.ofSeq (lines.[0].Split ' ') |> ParseIntList
    match hasParsed with
    | false -> printfn "Error: Unable to parse input length."
    | true when values.Length > 1 ->
        let mean = values |> List.averageBy (fun x -> float x)
        let squareDif = values |> List.map (fun x -> System.Math.Pow(float x - mean, 2.0))
        let standardDev = System.Math.Sqrt((squareDif |> List.sum)/ float values.Length) 
        printfn "Standard Deviation: %.4f" standardDev       
    | _ -> printfn "Error: Input data length mismatch."
    
| [] -> printfn "Error: Input List is empty."
| _ -> printfn "Error: Input should be on one line."
    
sw.Stop()
printfn "Time: %O" sw.Elapsed
OutStream.Close()
OutStream.Dispose()