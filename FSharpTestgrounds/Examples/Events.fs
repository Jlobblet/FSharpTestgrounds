module FSharpTestgrounds.Examples.Events

open System
open FSharpTestGrounds.Reflection

[<Struct>]
type ConversationOption = ConversationOption of string

[<Struct>]
type Reply = Reply of string

type Action = unit -> unit
type State = Map<string, string>

[<Struct>]
type OutpostEvent =
    { Reply: Reply
      StateUpdate: State -> OutpostEvent -> State
      Children: Map<int, ConversationOption * OutpostEvent> }

let tryInt (str: string) =
    match Int32.TryParse(str) with
    | false, _ -> None
    | true, i -> Some i

let rec interpret event state =
    let (Reply speech) = event.Reply
    printfn $"%s{speech}"

    if event.Children.IsEmpty then
        ()
    else
        event.Children
        |> Map.iter (fun i ((ConversationOption o), _) -> printfn $"%i{i}: %s{o}")

        let rec getInput () =
            let value = Console.ReadLine() |> tryInt

            match value with
            | Some i when event.Children.ContainsKey i -> i
            | _ -> getInput ()

        let chosenOption = getInput ()
        let newState = event.StateUpdate state event

        interpret (snd event.Children.[chosenOption]) newState

let exampleEvent =
    { Reply = Reply "This is the entry point"
      StateUpdate = (fun s _ -> s)
      Children =
          [ 1,
            (ConversationOption "Path 1",
             { Reply = Reply "You chose path 1"
               StateUpdate = (fun s _ -> s.Add("key", "value"))
               Children = Map.empty })
            2,
            (ConversationOption "Path 2",
             { Reply = Reply "You chose path 2"
               StateUpdate = (fun s _ -> s)
               Children = Map.empty }) ]
          |> Map.ofList }

[<ReflectionTarget>]
let ``show off a tree`` () = interpret exampleEvent
