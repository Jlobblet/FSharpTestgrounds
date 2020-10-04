module FSharpTestgrounds.Examples.Fibonacci

open FSharpTestGrounds.Reflection

let fib n =
    let rec inner acc1 acc2 n =
        match n with
        | 0 -> acc1
        | 1 -> acc2
        | _ -> inner acc2 (acc1 + acc2) (n - 1)
    inner 0 1 n
    
[<ReflectionTarget>]
let ``Print the first 10 Fibonacci numbers`` () =
    [ 1 .. 10 ]
    |> List.map fib
    |> printfn "%A"
