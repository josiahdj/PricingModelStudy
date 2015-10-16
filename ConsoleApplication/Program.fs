open Implementation
open Travel
open UnitsOfMeasure

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    
    // let tripInputs = Drive (GasAndTolls {FuelCost = 3.5M<Dollar/Gallon>; FuelEfficiency = 25M<Mile/Gallon>; Distance = 50M<Mile>})
    let teamMembers = 1<Person>;
    let tripLength = 2<Day>
    let tripInputs = Drive (CostPerMile {CostPerMile = 0.26M<Dollar/Mile>; Distance = 50M<Mile>})
    let lodgingInputs = {RoomCost = 150M<Dollar/Day/Person>; Rooms = teamMembers; Nights = tripLength}
    let impInputs = {TeamMembers = teamMembers; TripsPerMonth = 2<Trip/Month>; TravelDrivers = tripInputs; LodgingDrivers = lodgingInputs}
    let cost = implementationCost impInputs
    printfn "implementation transport cost = %f" cost.TransportCost

            
    0 // return an integer exit code
