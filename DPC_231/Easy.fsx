// Daily Programmer Challenge #231
// https://www.reddit.com/r/dailyprogrammer/comments/3jz8tt

let Difficulty = "Easy"

let OutStream = new System.IO.StreamWriter(Difficulty+"Data/Output.txt", false)
System.Console.SetOut(OutStream) |> ignore
let sw = System.Diagnostics.Stopwatch.StartNew()
let lines = List.ofSeq (System.IO.File.ReadAllLines (Difficulty+"Data/Input.txt")) 

type Cell = On | Off  

let rec ParseCells (line:char list) =
    match line with
    | this::rest ->
        let value =
            match this with
            | '0' -> Some Off
            | '1' -> Some On
            | _ -> None

        if rest.Length > 0 then
            value :: ParseCells rest
        else
            [value]
    | [] -> failwith "Error: Empty List."  
    
let PrettyPrint cell =
    match cell with
    | On -> "x"
    | Off -> " "

let GetNewCell neighbours =
    match neighbours with
        | (Off, Off)
        | (On, On) -> Off
        | (Off, On)
        | (On, Off) -> On

let rec IterateCells (cells:Cell list) i =
    if i <> 0 then    
        let partList = [for i = 1 to cells.Length-2 do yield GetNewCell (cells.[i-1],cells.[i+1])]
        let partList = Off :: (partList @ [Off]) 
    
        let outLine = partList |> List.fold (fun acc x -> acc + PrettyPrint x) System.String.Empty
        printfn "%s" ( outLine.[1 .. outLine.Length-2] )

        IterateCells partList (i-1)
    
match lines with
| [] -> printfn "Error: Input is empty."
| _ ->
    let line = "0" + lines.[0] + "0"
    let cellLine = ParseCells ( List.ofSeq (line.ToCharArray()) )
    if cellLine |> List.exists (fun x -> Option.isNone x) then
        printfn "Error: Invalid Input"
    else
        let cellLine = cellLine |> List.map (fun x -> x.Value)
        let outLine = cellLine |> List.fold (fun acc x -> acc + PrettyPrint x) System.String.Empty
        printfn "%s" ( outLine.[1 .. outLine.Length-2] )

        IterateCells cellLine 25
    
sw.Stop()
printfn "Time: %O" sw.Elapsed
OutStream.Close()
OutStream.Dispose()