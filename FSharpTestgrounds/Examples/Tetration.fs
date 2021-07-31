module FSharpTestGrounds.Examples.Tetration

open System
open FSharpTestGrounds.Reflection

[<StructuredFormatDisplay("{AsString}")>]
type TetrationBase =
    | Float of float
    | Bigint of bigint
    override this.ToString() =
        match this with
        | Float f -> f.ToString()
        | Bigint b -> b.ToString()

    member this.AsString = this.ToString()

let bigintPow b e =
    seq { for _ in bigint 1 .. e -> b }
    |> Seq.reduce (*)

let tetration x n =
    let genSeq n v = seq { for _ in 1 .. n -> v }

    match x with
    | Bigint (v) -> genSeq n v |> Seq.reduceBack (bigintPow) |> Bigint
    | Float (v) ->
        genSeq n v
        |> Seq.reduceBack (fun a b -> a ** b)
        |> Float

let demonstrateTetration value number =
    (value, number, (tetration value number))
    |||> printfn "%A *** %A -> %A"

[<ReflectionTarget>]
let ``Calculate 2 tetrated 5 times (2^2^2^2^2)`` () =
    demonstrateTetration (Bigint(bigint 2)) 6

[<ReflectionTarget>]
let ``Show the limit of e^{1/e} tetrated is e`` () =
    let magicNumber = Float(Math.E ** (1.0 / Math.E))

    [ 1
      2
      3
      4
      5
      10
      20
      30
      40
      50
      100
      1000
      100000 ]
    |> List.map (fun n -> demonstrateTetration magicNumber n)
    |> ignore
