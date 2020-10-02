module FSharpTestGrounds.Reflection

open System
open System.Reflection

type ReflectionTargetAttribute() = inherit Attribute()

let getMethodsWithAttribute<'attr> () =
    Assembly.GetExecutingAssembly().GetTypes()
    |> Array.collect (fun t -> t.GetMethods())
    |> Array.filter (fun m ->
        (m.CustomAttributes
         |> Seq.map (fun a -> a.AttributeType)
         |> Seq.contains typeof<'attr>
         )
        )

let getTargetMethods = getMethodsWithAttribute<ReflectionTargetAttribute>
    
let chooseMethod (methods: MethodInfo[]) =
    let methodsIndexed =
        methods
        |> Array.mapi (fun i m -> string i, m)

    let map =
        methodsIndexed
        |> Map.ofArray
            
    methodsIndexed
    |> Array.map (fun (k, v) -> sprintf "%s: %O" k v.Name)
    |> String.concat "\n"
    |> printfn "%s"
    
    let rec getChoice () =
        Console.Write("Enter selection: ")
        match Console.ReadLine() with
        | k when map.ContainsKey k -> map.[k]
        | _ -> getChoice ()

    getChoice ()
