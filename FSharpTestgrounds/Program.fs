// Learn more about F# at http://fsharp.org

open FSharpTestGrounds.Reflection

[<EntryPoint>]
let main argv =
    let rec inner () =
        let methods = getTargetMethods ()

        (chooseMethod methods).Invoke(obj, Array.empty)
        |> ignore

        inner ()

    inner ()
    0 // return an integer exit code
