module FSharpTestgrounds.Examples.Parallel

open System
open System.Diagnostics
open System.Threading.Tasks
open FSharpTestGrounds.Reflection

let timeAction act name =
    let stopwatch = Stopwatch.StartNew()
    let result = act ()
    stopwatch.Stop()
    printfn $"%s{name} Elapsed milliseconds: %i{stopwatch.ElapsedMilliseconds}"
    result

let reduceParallel<'a> func (xs: 'a list) =
    let rec reduceRec func (xs: 'a list) =
        function
        | 1 -> xs.[0]
        | 2 -> func xs.[0] xs.[1]
        | length ->
            let half = length / 2

            let h1 =
                Task.Run(fun _ -> reduceRec func (xs |> List.take half) half)

            let h2 =
                Task.Run(fun _ -> reduceRec func (xs |> List.skip half) (length - half))

            Task.WaitAll(h1, h2)
            func h1.Result h2.Result

    match xs.Length with
    | 0 -> failwith "Sequence contains no elements"
    | c -> reduceRec func xs c

//[<ReflectionTarget>]
let ``Test async stuff`` () =
    let rec getInput () =
        try
            printf "Enter an integer: "
            Console.ReadLine() |> int
        with :? FormatException ->
            printfn "Invalid number!"
            getInput ()

    let n = getInput ()
    let l = [ 1 .. n ]

    let par () =
        l
        |> Array.ofList
        |> Array.splitInto (n / 100_000)
        |> Array.map (fun l -> async { return Array.reduce (+) l })
        |> Async.Parallel
        |> Async.RunSynchronously
        |> Array.reduce (+)
        |> printfn "%i"

    let lin () = l |> List.reduce (+) |> printfn "%i"

    timeAction par "Parallel"
    timeAction lin "Linear"
