// Daily Programmer Challenge #232 - Palindromes
// https://www.reddit.com/r/dailyprogrammer/comments/3kx6oh

let Difficulty = "Easy"

let OutStream = new System.IO.StreamWriter(Difficulty+"Data/Output.txt", false)
System.Console.SetOut(OutStream) |> ignore
let sw = System.Diagnostics.Stopwatch.StartNew()
let lines = List.ofSeq (System.IO.File.ReadAllLines (Difficulty+"Data/Input.txt")) 

match lines with
| [] -> printfn "Error: Input List is empty."
| countStr::tail ->
    let hasParsed, count = System.Int32.TryParse countStr
    match hasParsed with
    | false -> printfn "Error: Unable to parse input length."
    | true when count > 1 && tail.Length = count ->
        let letterForward = (tail |> List.fold (fun i x -> i + x) System.String.Empty).ToLower()
                            |> List.ofSeq |> List.filter System.Char.IsLetter
        let letterBackward = letterForward |> List.rev

        printfn "%s" (if letterForward = letterBackward then "Palindrome" else "Not a palindrome")
    | _ -> printfn "Error: Input data length mismatch."
    
sw.Stop()
printfn "Time: %O" sw.Elapsed
OutStream.Close()
OutStream.Dispose()