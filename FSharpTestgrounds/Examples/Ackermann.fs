module FSharpTestgrounds.Examples.Ackermann

open FSharpTestGrounds.Reflection



//let rec ackermann m n =
//    match m, n with
//    | z, n when z = 0I -> n + 1I
//    | m, z when z = 0I -> ackermann (m - 1I) 1I
//    | m, n -> ackermann (m - 1I) (ackermann m (n - 1I))

let ackermann M N =
    let rec inner m n continuation =
        match m, n with
        | z, n when z = 0I -> n + 1I |> continuation
        | m, z when z = 0I -> inner (m - 1I) 1I continuation
        | m, n -> inner m (n - 1I) (fun x -> inner (m - 1I) x continuation)

    inner M N id

[<ReflectionTarget>]
let ``calculate values of the Ackermann function`` () =
    seq {
        for m in 0I .. 4I do
            for n in 0I .. 4I -> m, n, ackermann m n
    }
    |> Seq.iter (fun (m, n, ack) -> printfn $"m: %A{m} n: %A{n} A(m, n): %A{ack}")
