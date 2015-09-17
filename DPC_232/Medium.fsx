// Daily Programmer Challenge #232: Where Should Grandma's House Go
// https://www.reddit.com/r/dailyprogrammer/comments/3l61vx

open System.Threading.Tasks

let Difficulty = "Medium"

let OutStream = new System.IO.StreamWriter(Difficulty+"Data/Output.txt", false)
System.Console.SetOut(OutStream) |> ignore
let sw = System.Diagnostics.Stopwatch.StartNew()
let lines = List.ofSeq (System.IO.File.ReadAllLines (Difficulty+"Data/Input.txt")) 

type Coordinate = {X:float; Y:float} 
type CoordinatePair = {First:Coordinate; Second:Coordinate}

let rec TryParseCoordinates (lines:string list) =
    match lines with
    | this::rest ->
        let parts = this.[1 .. this.Length-2].Split([|","|], System.StringSplitOptions.None)
        let parsedX, x = System.Double.TryParse (parts.[0].Trim())
        let parsedY, y = System.Double.TryParse (parts.[1].Trim())

        match parsedX && parsedY, rest.Length with
        | true, 0 -> [Some {X = x; Y=y}]
        | true, _ -> Some {X = x; Y=y} :: TryParseCoordinates rest
        | false, 0 -> [None]
        | false, _ -> None :: TryParseCoordinates rest
    | [] -> []

let GetRadius (x:Coordinate) (y:Coordinate) = System.Math.Pow(x.X - y.X, 2.0) + System.Math.Pow(x.Y-y.Y, 2.0)

// This function shamelessly ripped from http://stackoverflow.com/a/4495708/4194885
let rec combinations acc size set = seq {
    match size, set with
    | n, x::xs ->
        if n > 0 then yield! combinations (x::acc) (n-1) xs
        if n >= 0 then yield! combinations acc n xs
    | 0, [] -> yield acc
    | _, [] -> () }

let rec GetMinDistance (list:Coordinate list) =
    if list.Length > 650 then
        let left, right = list.[0 .. list.Length/2], list.[list.Length/2 .. list.Length-1]

        let rhsTask = Task.Factory.StartNew<CoordinatePair>( (fun () -> GetMinDistance right ) )
        let lhsTask = Task.Factory.StartNew<CoordinatePair>( (fun () -> GetMinDistance left ) )
    
        let result = Task.WhenAll([rhsTask;lhsTask])
        let minLeft, minRight = result.Result.[0], result.Result.[1]

        let center = (list.[list.Length/2].X + list.[list.Length/2+1].X) / 2.0
        let radLeft = System.Math.Sqrt (GetRadius minLeft.First minLeft.Second)
        let radRight = System.Math.Sqrt (GetRadius minRight.First minRight.Second)
        let minRad = System.Math.Min(radLeft, radRight)                       
        
        let centreColumn = list |> List.filter (fun c -> c.X > (center-minRad) && c.X < (center+minRad))

        if centreColumn = list || centreColumn = [] then
            if minRad = radLeft then minLeft
            else minRight
        else
            GetMinDistance centreColumn
    else
        let combs = list |> combinations [] 2 |> Seq.minBy (fun l -> GetRadius l.Head l.Tail.Head)         
        {First = combs.Head; Second = combs.Tail.Head}
    

match lines with
| [] -> printfn "Error: Input List is empty."
| countStr::tail ->
    let hasParsed, count = System.Int32.TryParse countStr
    match hasParsed with
    | false -> printfn "Error: Unable to parse input length."
    | true when count > 1 && tail.Length = count ->
        let coords = TryParseCoordinates tail

        if coords |> List.exists (fun x -> x.IsNone) then printfn "One or more inputs were not parsed."
        else
            let coords = coords |> List.map (fun x -> x.Value) |> List.sortBy (fun x -> x.X)                 
            let pair = GetMinDistance coords

            printfn "(%s,%s) (%s,%s)" (pair.First.X.ToString()) (pair.First.Y.ToString()) (pair.Second.X.ToString()) (pair.Second.Y.ToString())
    | _ -> printfn "Error: Input data length mismatch."
    
sw.Stop()
printfn "Time: %O" sw.Elapsed
OutStream.Close()
OutStream.Dispose()