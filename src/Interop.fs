namespace Prelude
open System

module Func =
  let inline ofFSharp0 f = new Func<_>(f)
  let inline ofFSharp1 f = new Func<_, _>(f)
  let inline ofFSharp2 f = new Func<_, _, _>(f)
  let inline ofFSharp3 f = new Func<_, _, _, _>(f)
  let inline toFSharp0 (f: Func<_>) () = f.Invoke()
  let inline toFSharp1 (f: Func<_, _>) x = f.Invoke(x)
  let inline toFSharp2 (f: Func<_, _, _>) x y = f.Invoke(x, y)
  let inline toFSharp3 (f: Func<_, _, _, _>) x y z = f.Invoke(x, y, z)

module Action =
  let inline ofFSharp0 a = new Action(a)
  let inline ofFSharp1 a = new Action<_>(a)
  let inline ofFSharp2 a = new Action<_, _>(a)
  let inline ofFSharp3 a = new Action<_, _, _>(a)
  let inline toFSharp0 (f: Action) () = f.Invoke()
  let inline toFSharp1 (f: Action<_>) x = f.Invoke(x)
  let inline toFSharp2 (f: Action<_, _>) x y = f.Invoke(x, y)
  let inline toFSharp3 (f: Action<_, _, _>) x y z = f.Invoke(x, y, z)

module Flag =
  let inline combine (xs: ^flag seq) : ^flag
    when ^flag: enum<int> =
      xs |> Seq.fold (|||) (Unchecked.defaultof< ^flag >)
  let inline contains (x: ^flag) (flags: ^flag) : bool
    when ^flag: enum<int> =
      (x &&& flags) = x 

module Number =
  open System.Globalization

  let inline tryParse< ^T when ^T: (static member TryParse: string -> ^T byref -> bool) > str : ^T option =
    let mutable ret = Unchecked.defaultof<_> in
    if (^T: (static member TryParse: string -> ^T byref -> bool) (str, &ret)) then
      Some ret
    else
      None

  let inline tryParseWith< ^T when ^T: (static member TryParse: string -> NumberStyles -> IFormatProvider -> ^T byref -> bool) > str styleo formato : ^T option =
    let mutable ret = Unchecked.defaultof<_> in
    let style = styleo ?| NumberStyles.None in
    let format = formato ?| CultureInfo.InvariantCulture in
    if (^T: (static member TryParse: string -> NumberStyles -> IFormatProvider -> ^T byref -> bool) (str, style, format, &ret)) then
      Some ret
    else
      None
