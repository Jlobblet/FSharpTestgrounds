module FSharpTestgrounds.Examples.``Breadth First Search``

type Node<'a> = { value: 'a; children: Node<'a> list }

let rec bfs pred queue =
    match queue with
    | [] -> None
    | x :: _ when pred x -> Some x
    | x :: xs -> bfs pred (xs @ x.children)
