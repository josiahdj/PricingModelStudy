// exploring cell references with multiple dimensions
let accountClasses = Set.ofList ["Revenue"; "COGS"; "OpEx"]
let departments = Set.ofList ["Help Center"; "Field Ops"; "Sales"; "Engineering"; "G&A"]
let clients = Set.ofList ["NYC"; "CHI"; "WIL"; "BAL"]
let initiatives = Set.ofList ["Implementation"; "New Crew"]

let dimensions = Map.ofList ["AccountClass", accountClasses; "Department", departments; "Client", clients; "Initiative", initiatives]

let months = [1..12]

let tags1 = Set.ofList ["COGS"; "Help Center"; "NYC";] // in real life, I'd make these integers and only map them to strings when displayed
let tags2 = Set.ofList ["COGS"; "Field Ops"; "NYC";]

type Cell = {Month: int; Dimensions: Set<string>; Value: string}

let cell1 = {Month = 1; Dimensions = tags1; Value = "Cell1"}
let cell2 = {Month = 2; Dimensions = Set.ofList ["Field Ops"; "COGS"; "NYC"; "Implementation"]; Value = "Cell2"}
let cell3 = {Month = 3; Dimensions = tags1; Value = "Cell3"}
let cell4 = {Month = 4; Dimensions = tags2; Value = "Cell4"}
let cell5 = {Month = 5; Dimensions = tags1; Value = "Cell5"}
let cell6 = {Month = 6; Dimensions = tags2; Value = "Cell6"}
let cell7 = {Month = 7; Dimensions = tags1; Value = "Cell7"}
let cell8 = {Month = 8; Dimensions = tags2; Value = "Cell8"}
let cell9 = {Month = 9; Dimensions = tags1; Value = "Cell9"}

let cells = [cell1; cell2; cell3; cell4; cell5; cell6; cell7; cell8; cell9]
let filterCells cs m ts = 
    List.filter (fun c -> c.Dimensions = ts && c.Month = m) cells // this WORKS! (could use subsetOf for Subtotals)
let cellsWeWant = filterCells cells 4 tags2
