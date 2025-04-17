module TailwindAutocomplete.Client.View

open Feliz

[<ReactComponent>]
let AppView () =
    Html.div [
        Html.div [
            prop.className "flex"
        ]
        Html.divClassed "flex" []
    ]