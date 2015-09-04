// Daily Programmer Challenge #213
// https://www.reddit.com/r/dailyprogrammer/comments/34rxkc

let Difficulty = "Easy"

let OutStream = new System.IO.StreamWriter(Difficulty+"Data/Output.txt", false)
System.Console.SetOut(OutStream) |> ignore
let sw = System.Diagnostics.Stopwatch.StartNew()
let lines = List.ofSeq (System.IO.File.ReadAllLines (Difficulty+"Data/Input.txt"))

let SingleDict = new System.Collections.Generic.Dictionary<char, string>()
SingleDict.Add('0', "")
SingleDict.Add('1', "One")
SingleDict.Add('2', "Two")
SingleDict.Add('3', "Three")
SingleDict.Add('4', "Four")
SingleDict.Add('5', "Five")
SingleDict.Add('6', "Six")
SingleDict.Add('7', "Seven")
SingleDict.Add('8', "Eight")
SingleDict.Add('9', "Nine")
SingleDict.Add('A', "A")
SingleDict.Add('B', "Bee")
SingleDict.Add('C', "Cee")
SingleDict.Add('D', "Dee")
SingleDict.Add('E', "E")
SingleDict.Add('F', "Eff")

let TensDict = new System.Collections.Generic.Dictionary<char, string>()
TensDict.Add('0', "")
TensDict.Add('1', "teen")
TensDict.Add('2', "Twenty")
TensDict.Add('3', "Thirty")
TensDict.Add('4', "Fourty")
TensDict.Add('5', "Fifty")
TensDict.Add('6', "Sixty")
TensDict.Add('7', "Seventy")
TensDict.Add('8', "Eighty")
TensDict.Add('9', "Ninty")
TensDict.Add('A', "Atta")
TensDict.Add('B', "Bibbity")
TensDict.Add('C', "City")
TensDict.Add('D', "Dickety")
TensDict.Add('E', "Ebbity")
TensDict.Add('F', "Fleventy")

let rec GetPrettyName (num:string) =
    if num.Length % 2 <> 0 then
        GetPrettyName ("0" + num)
    else if num.Length > 2 then
        GetPrettyName num.[0..1] + " bitey " + GetPrettyName num.[2 .. num.Length-1]
    else
        TensDict.[num.[0]] + "-" + SingleDict.[num.[1]]


let rec GetNumberNames (names:string list) =
    match names with
    | head::tail when tail.Length > 0 -> (GetPrettyName head.[2 .. head.Length-1]) :: (tail |> GetNumberNames)
    | head::_ -> [GetPrettyName head.[2 .. head.Length-1]]
    | [] -> failwith "Error: Empty List"

match lines with
| [] -> printfn "Error: Input List is empty."
| _ when lines |> List.forall (fun x -> x.StartsWith "0x") ->     
    let numberNames = lines |> GetNumberNames
    for name in numberNames do
        printfn "%s" name
| _ -> printfn "Error: Invalid input."
    
sw.Stop()
printfn "Time: %O" sw.Elapsed
OutStream.Close()
OutStream.Dispose()