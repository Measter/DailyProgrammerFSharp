// Daily Programmer Challenge #229
// https://www.reddit.com/r/dailyprogrammer/comments/3i99w8
// 2015-08-25

let Difficulty = "Easy"

let OutStream = new System.IO.StreamWriter(Difficulty+"Data/Output.txt", false)
System.Console.SetOut(OutStream) |> ignore
let sw = System.Diagnostics.Stopwatch.StartNew()

let DoubleEquals (x:float) (y:float) = (System.Math.Abs (x-y)) < System.Double.Epsilon

let rec FixedPoint f x =
    let next = f x
    if DoubleEquals x next then x else FixedPoint f next

let dottie = FixedPoint System.Math.Cos 5.0
printfn "f(x) = cos(x):     %s" (dottie.ToString())

let dottie2 = FixedPoint (fun x -> x - System.Math.Tan x ) 2.0                                               
printfn "f(x) = x - tan(x): %s" (dottie2.ToString())
printfn "f(x) = 1 + 1/x:"
for i = 1 to 10 do
    let dottie3 = FixedPoint (fun x -> 1.0 + 1.0/x) (float i)
    printfn "\t%2d: %s" i (dottie3.ToString())
    
sw.Stop()
printfn "Time: %O" sw.Elapsed
OutStream.Close()
OutStream.Dispose()