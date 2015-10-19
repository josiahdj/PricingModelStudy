open Implementation
open Travel
open UnitsOfMeasure

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    
    // let tripInputs = Drive (GasAndTolls {FuelCost = 3.5M<Dollar/Gallon>; FuelEfficiency = 25M<Mile/Gallon>; Distance = 50M<Mile>})
    let teamMembers = 1<Person>;
    let tripLength = 2<Day>
    let transportInputs = Drive (CostPerMile {CostPerMile = 0.26M<Dollar/Mile>; Distance = 50M<Mile>}, ShortTerm {Hours = 8<Hour>; CostPerHour = 1M<Dollar/Hour>})
    let lodgingInputs = {RoomCost = 150M<Dollar/Day/Person>; Rooms = teamMembers; Nights = tripLength - 1<Day>}
    let mealInputs = {Meal = 20M<Dollar/Meal>; Meals = 3<Meal/Day/Person>; Days = tripLength - 1<Day>; People = teamMembers}
    let impInputs = {TeamMembers = teamMembers; TripsPerMonth = 2<Trip/Month>; TripLength = tripLength; TravelDrivers = transportInputs; LodgingDrivers = lodgingInputs; MealDrivers = mealInputs}
    let cost = implementationCost impInputs
    printfn "monthly implementation transport cost = %f" cost.TransportCost
    printfn "monthly implementation lodging cost = %f" cost.LodgingCost
    printfn "monthly implementation meal cost = %f" cost.MealCost
        
    0 // return an integer exit code
