// Daily Programmer Challenge #239 - A Game of Threes
// https://www.reddit.com/r/dailyprogrammer/comments/3r7wxz

let Difficulty = "Easy"

let OutStream = new System.IO.StreamWriter(Difficulty+"Data/Output.txt", false)
System.Console.SetOut(OutStream) |> ignore
let sw = System.Diagnostics.Stopwatch.StartNew()
let lines = List.ofSeq (System.IO.File.ReadAllLines (Difficulty+"Data/Input.txt")) 

let rec gameOfThrees = function
    | x when x = 1 || x = 0 -> printfn "%i" x
    | x when x%3 = 0 -> printfn "%i %i" x 0
                        gameOfThrees (x/3)
    | x when x%3 = 1 -> printfn "%i %i" x -1
                        gameOfThrees ((x-1)/3)
    | x when x%3 = 2 -> printfn "%i %i" x 1
                        gameOfThrees ((x+1)/3)
    | _ -> printfn "%s" "How did I even get here? This isn't possible!"

match lines with
| [] -> printfn "Error: Input List is empty."
| valueStr::_ ->
    let hasParsed, value = System.Int32.TryParse valueStr
    match hasParsed with
    | false -> printfn "Error: Unable to parse input value."
    | true -> gameOfThrees value
    
sw.Stop()
printfn "Time: %O" sw.Elapsed
OutStream.Close()
OutStream.Dispose()