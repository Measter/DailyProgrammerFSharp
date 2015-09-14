// Daily Programmer Challenge #1
// https://www.reddit.com/r/dailyprogrammer/comments/pih8x

printf "What is your name? "
let name = System.Console.ReadLine()

printf "What is your age? "
let rec GetAge() = 
    let isValid, age = System.Int32.TryParse( System.Console.ReadLine() )
    if isValid && age >= 0 then age else
        printf "Age must be a number: "
        GetAge()

let age = GetAge()

printf "What... is the air-speed vel... What is your Reddit username? "
let username = System.Console.ReadLine()

printfn "Your name is %s, you are %d years old, and your username is %s." name age username

if System.IO.Directory.Exists "EasyData" = false then System.IO.Directory.CreateDirectory "EasyData" |> ignore
let stream = new System.IO.FileStream("EasyData/Input.txt", System.IO.FileMode.Create)
let bw = new System.IO.StreamWriter(stream)

bw.WriteLine name
bw.WriteLine age
bw.WriteLine username

bw.Dispose()
stream.Dispose()