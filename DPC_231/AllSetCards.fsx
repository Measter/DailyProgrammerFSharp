let OutStream = new System.IO.StreamWriter("AllCardsList.txt", false)
System.Console.SetOut(OutStream) |> ignore

type CardNumber = One | Two | Three
type CardSymbol = Diamond | Oval | Squiggle
type CardColour = Red | Purple | Green
type CardFill = Open | Hatched | Filled

type Card = {Number:CardNumber; Symbol:CardSymbol; Colour:CardColour; Fill:CardFill}

let CodePrintCard (card:Card) =
    (match card.Symbol with | Diamond -> "D" | Oval -> "O" | Squiggle -> "S") +
    (match card.Colour with | Red -> "R" | Purple -> "P" | Green -> "G") +
    (match card.Number with | One -> "1" | Two -> "2" | Three -> "3") +
    (match card.Fill with | Open -> "O" | Hatched -> "H" | Filled -> "F")

for sym in [Diamond; Oval; Squiggle] do
    for col in [Red; Purple; Green] do
        for num in [One; Two; Three] do
            for fil in [Open; Hatched; Filled] do
                printfn "%s" (CodePrintCard {Number = num; Symbol = sym; Colour = col; Fill = fil})

OutStream.Close()
OutStream.Dispose()