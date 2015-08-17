// Daily Programmer Challenge #226: Adding Fractions
// https://www.reddit.com/r/dailyprogrammer/comments/3fmke1

open System

let Difficulty = "Easy"

let OutStream = new System.IO.StreamWriter(Difficulty+"Data/Output.txt", false)
System.Console.SetOut(OutStream) |> ignore
let sw = System.Diagnostics.Stopwatch.StartNew()
let lines = List.ofSeq (System.IO.File.ReadAllLines (Difficulty+"Data/Input.txt")) 

type Fraction(numerator:Int64, denomenator:Int64) = 
    let rec gcd (a:Int64) (b:Int64) = if b = 0L then a else gcd b ( a % b )
    let div = gcd numerator denomenator

    member this.Num = numerator / div
    member this.Denom = denomenator / div

    override this.ToString() = this.Num.ToString() + "/" + this.Denom.ToString()

    static member Zero = Fraction (0L,1L)                                                                                                       
    static member (+) (a:Fraction, b:Fraction) = Fraction(a.Num * b.Denom + b.Num * a.Denom, a.Denom * b.Denom)

let TryParseFraction (c:string) =
    let parts = c.Split '/'
    if parts.Length <> 2 then None else

    let hasParsedLeft, left = Int64.TryParse parts.[0]
    let hasParsedRight, right = Int64.TryParse parts.[1]

    if hasParsedLeft && hasParsedRight then Some (Fraction (left, right)) else None

match lines with
| [] -> printfn "Error: Input List is empty."
| countStr::tail ->
    let hasParsed, count = System.Int32.TryParse countStr
    match hasParsed with
    | false -> printfn "Error: Unable to parse input length."
    | true when count > 1 && tail.Length = count ->
        let fracList = tail |> List.map TryParseFraction

        if fracList |> List.forall (fun i -> i.IsNone)
            then printfn "Error: One or more invalid fractions."
            else
                let result = fracList |> List.map (fun i -> i.Value) |> List.sum
                printfn "Result: %O" result

    | _ -> printfn "Error: Input data length mismatch."

sw.Stop()
printfn "Time: %O" sw.Elapsed
OutStream.Close()
OutStream.Dispose()