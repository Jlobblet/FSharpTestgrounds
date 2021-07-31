module List

let ipartition predicate list =
    list
    |> List.indexed
    |> List.partition predicate
    |> fun (t, f) -> (List.map snd t), (List.map snd f)

let linspace num start stop =
    let step = (stop - start) / (float num)
    List.init num (fun i -> (i |> float) * step + start)

let filteri predicate list =
    list
    |> List.indexed
    |> List.filter predicate
    |> List.map snd
