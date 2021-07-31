module FSharpTestgrounds.Examples.FizzBuzz

open FSharpTestGrounds.Reflection

let private fizzbuzzMap = [ 3, "Fizz"; 5, "Buzz" ] |> Map.ofList

let private mapping i =
    fizzbuzzMap
    |> Map.fold (fun acc key elt ->
        match i % key with
        | 0 -> $"{acc}{elt}"
        | _ -> acc) ""
    |> function
    | "" -> $"{i}"
    | s -> s

let binder i =
    fizzbuzzMap
    |> Map.filter (fun key _ -> i % key = 0)
    |> Map.toList
    |> List.map snd
    |> function
    | [] -> [ $"{i}" ]
    | l -> l
    |> List.append [ "\n" ]

[<ReflectionTarget>]
let Fizzbuzz () =
    [ 1 .. 100 ]
    |> List.collect binder
    |> List.iter (printf "%s")
