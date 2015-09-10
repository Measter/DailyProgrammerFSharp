// Daily Programmer Challenge #231 Intermediate: Set Game Solver
// https://www.reddit.com/r/dailyprogrammer/comments/3ke4l6

let Difficulty = "Medium"

let OutStream = new System.IO.StreamWriter(Difficulty+"Data/Output.txt", false)
System.Console.SetOut(OutStream) |> ignore
let sw = System.Diagnostics.Stopwatch.StartNew()
let lines = List.ofSeq (System.IO.File.ReadAllLines (Difficulty+"Data/Input.txt")) 

// This function shamelessly ripped from http://stackoverflow.com/a/4495708/4194885
let rec combinations acc size set = seq {
    match size, set with
    | n, x::xs ->
        if n > 0 then yield! combinations (x::acc) (n-1) xs
        if n >= 0 then yield! combinations acc n xs
    | 0, [] -> yield acc
    | _, [] -> () }

type CardNumber = One | Two | Three
type CardSymbol = Diamond | Oval | Squiggle
type CardColour = Red | Purple | Green
type CardFill = Open | Hatched | Filled

type Card = {Number:CardNumber; Symbol:CardSymbol; Colour:CardColour; Fill:CardFill}

let ParseCard (cardString:string) =
    let num = match cardString.[2] with
              | '1' -> Some One
              | '2' -> Some Two
              | '3' -> Some Three
              | _ -> None
    let symbol = match cardString.[0] with
                 | 'D' -> Some Diamond
                 | 'O' -> Some Oval
                 | 'S' -> Some Squiggle
                 | _ -> None
    let col = match cardString.[1] with
              | 'R' -> Some Red
              | 'G' -> Some Green
              | 'P' -> Some Purple
              | _ -> None
    let fill = match cardString.[3] with
               | 'O' -> Some Open
               | 'H' -> Some Hatched
               | 'F' -> Some Filled
               | _ -> None

    if num.IsNone && symbol.IsNone && col.IsNone && fill.IsNone then
        None
    else
        Some {Number = num.Value; Symbol = symbol.Value; Colour = col.Value; Fill = fill.Value}

let AreEqual a b c = a = b && a = c
let AreNotEqual a b c = a <> b && a <> c && b <> c
let ConditionCheck a b c = (AreEqual a b c) || (AreNotEqual a b c)

let IsSetMatch (set:Card list) =
    match set with
    | a::b::c::tail when tail.Length = 0 ->
        let colorCheck = ConditionCheck a.Colour b.Colour c.Colour
        let fillCheck = ConditionCheck a.Fill b.Fill c.Fill
        let symbolCheck = ConditionCheck a.Symbol b.Symbol c.Symbol
        let numberCheck = ConditionCheck a.Number b.Number c.Number
        numberCheck && symbolCheck && fillCheck && colorCheck
    | _ -> failwith "Invalid Set"


let CodePrintCard (card:Card) =
    (match card.Symbol with | Diamond -> "D" | Oval -> "O" | Squiggle -> "S") +
    (match card.Colour with | Red -> "R" | Purple -> "P" | Green -> "G") +
    (match card.Number with | One -> "1" | Two -> "2" | Three -> "3") +
    (match card.Fill with | Open -> "O" | Hatched -> "H" | Filled -> "F")


match lines with
| [] -> printfn "Error: Input List is empty."
| _ ->
    let cards = lines |> List.map (fun x -> ParseCard x)
    
    if cards |> List.exists (fun x -> x.IsNone) then
        printfn "Invalid input."
    else 
        let cards = cards |> List.map (fun x -> x.Value)
        let combs = (combinations [] 3 cards) |> Seq.filter (fun x -> IsSetMatch x )

        for set in combs do
            printfn "%s" (set |> Seq.fold (fun a x -> a + (CodePrintCard x) + " ") System.String.Empty )
    
sw.Stop()
printfn "Time: %O" sw.Elapsed
OutStream.Close()
OutStream.Dispose()