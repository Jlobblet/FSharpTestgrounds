module FSharpTestgrounds.Examples.OperatorAbuse

let (|>>) x (f, g) = (f x, g x)
let (|>>>) x (f, g, h) = (f x, g x, h x)
let (|>>>>) x (f, g, h, i) = (f x, g x, h x, i x)

let (||>>) (x, y) (f, g) = (f x y, g x y)
let (||>>>) (x, y) (f, g, h) = (f x y, g x y, h x y)
let (||>>>>) (x, y) (f, g, h, i) = (f x y, g x y, h x y, i x y)

let (|||>>) (x, y, z) (f, g) = (f x y z, g x y z)
let (|||>>>) (x, y, z) (f, g, h) = (f x y z, g x y z, h x y z)
let (|||>>>>) (x, y, z) (f, g, h, i) = (f x y z, g x y z, h x y z, i x y z)

let (===||=========>) = "my sword"
