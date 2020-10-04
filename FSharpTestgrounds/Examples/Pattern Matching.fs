module FSharpTestgrounds.Examples.``Pattern Matching``

open FSharpTestGrounds.Reflection

type IntOrBool =
    | Int of int
    | Bool of bool
    
let ``Type Reveal Party`` v =
    match v with
    | Int i -> printfn "It's an integer! %i" i
    | Bool b -> printfn "It's a bool! %b" b


[<ReflectionTarget>]
let ``Demonstrate pattern matching to deconstruct a discriminated union`` () =
    let x = Int 5
    let y = Bool true
        
    ``Type Reveal Party`` x // It's an integer! 5
    ``Type Reveal Party`` y // It's a bool! true
