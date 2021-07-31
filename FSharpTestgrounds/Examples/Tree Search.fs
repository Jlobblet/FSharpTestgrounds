module FSharpTestGrounds.Examples.``Tree Search``

open FSharpTestGrounds.Reflection

type Leaf<'a> =
    { value: 'a
      left: Leaf<'a> option
      right: Leaf<'a> option }

let rec addLeaf value tree =
    match tree with
    | Some t when t.value > value ->
        { t with
              left = Some(addLeaf value t.left) }
    | Some t when t.value < value ->
        { t with
              right = Some(addLeaf value t.right) }
    | Some t when t.value = value -> t
    | _ ->
        { value = value
          left = None
          right = None }

let rec searchTree value tree =
    match tree with
    | Some t when value = t.value -> Some t
    | Some t when value < t.value -> searchTree value t.left
    | Some t when value > t.value -> searchTree value t.right
    | _ -> None

let rec bfs pred queue =
    match queue with
    | x :: _ when pred x -> Some x
    | x :: xs -> bfs pred (xs @ Option.toList x.left @ Option.toList x.right)
    | [] -> None

let constructTree () =
    [ 12; 246; 312; 56; 32; 5; 10; 12 ]
    |> List.fold (fun acc item -> Some(addLeaf item acc)) None

[<ReflectionTarget>]
let ``Construct a tree with some values`` () = constructTree () |> printfn "%A"

[<ReflectionTarget>]
let ``Search a tree for an existing value`` () =
    constructTree () |> searchTree 5 |> printfn "%A"

[<ReflectionTarget>]
let ``Search a tree for a missing value`` () =
    constructTree () |> searchTree 621 |> printfn "%A"

[<ReflectionTarget>]
let ``Perform a breadth first search for an existing value`` () =
    [ constructTree () |> Option.get ]
    |> bfs (fun x -> x.value = 312)
    |> printfn "%A"
