module FSharpTestgrounds.Examples.Shuffle

open FSharpTestGrounds.Reflection

let rng = System.Random()

let shuffle N arr =
    Seq.init N (fun i -> rng.Next(i, Array.length arr))
    |> Seq.map (Array.get arr)

[<ReflectionTarget>]
let ``shuffle a list`` () =
    [| 'A' .. 'Z' |]
    |> shuffle 5
    |> List.ofSeq
    |> printfn "%A"
