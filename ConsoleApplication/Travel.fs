module Travel

open UnitsOfMeasure
open LanguagePrimitives

type TransportDrivers = 
    | Flight of FlightDrivers * ParkingDrivers
    | Drive of DriveDrivers * ParkingDrivers
    | Train of TrainDrivers * ParkingDrivers
and FlightDrivers = {AirFare: decimal<Dollar/Person>; Seats: int<Person>}
and DriveDrivers = 
    | GasAndTolls of GasAndTollDrivers
    | CostPerMile of CostPerMileDrivers
    | DrivingRental of RentalCarDrivers
and GasAndTollDrivers = {FuelCost:decimal<Dollar/Gallon>; FuelEfficiency:decimal<Mile/Gallon>; Distance:decimal<Mile>}
and CostPerMileDrivers = {CostPerMile:decimal<Dollar/Mile>; Distance:decimal<Mile>}
and RentalCarDrivers = {CostPerDay:decimal<Dollar/Day>; Days:int<Day>; FuelCost:decimal<Dollar/Gallon>; FuelEfficiency:decimal<Mile/Gallon>; Distance:decimal<Mile>}
and TrainDrivers = {CostPerSeat:decimal<Dollar/Person>; Seats: int<Person>}
and ParkingDrivers = 
    | ShortTerm of ShortTermParkingDrivers
    | LongTerm of LongTermParkingDrivers
and ShortTermParkingDrivers = {Hours: int<Hour>; CostPerHour: decimal<Dollar/Hour>}
and LongTermParkingDrivers = {Days: int<Day>; CostPerDay: decimal<Dollar/Day>}

let parkingCost inputs =
    match inputs with
        | ShortTerm st -> st.CostPerHour * (decimal st.Hours |> DecimalWithMeasure<Hour>)
        | LongTerm lt -> lt.CostPerDay * (decimal lt.Days |> DecimalWithMeasure<Day>)

let flightCost inputs =
    inputs.AirFare * (decimal inputs.Seats |> DecimalWithMeasure<Person>)

let rec driveCost inputs =
        match inputs with
            | CostPerMile cpm -> cpm.CostPerMile * cpm.Distance // own car or taxi
            | GasAndTolls gat -> gat.FuelCost / gat.FuelEfficiency * gat.Distance // own car
            | DrivingRental rent -> rent.CostPerDay * (1M<Day> * decimal rent.Days)
                                    + driveCost (GasAndTolls {FuelCost = rent.FuelCost; FuelEfficiency = rent.FuelEfficiency; Distance = rent.Distance}) // rental + GasAndTolls

let trainCost inputs = 
    inputs.CostPerSeat * (decimal inputs.Seats |> DecimalWithMeasure<Person>)

let toFromTransportHub =
    driveCost (CostPerMile {CostPerMile = 0.26M<Dollar/Mile>; Distance = 50M<Mile>}) * 2M // to AND from airport, hence x2; TODO: assuming it's a cab w/no parking; in future make the mode of transport a higher-level choice (cab, own car w/parking, train)?

(* AKA trip segment. a leg is made of trip segments; a trip is made of legs. e.g. a leg might be: 1) taxi to airport 2) flight 3) taxi to hotel; 
then, a leg might be taxi to meeting and back to the hotel; then the reverse of the arrival leg (or on to another city)... *)
let transportCost inputs =
    match inputs with
        | Flight (flight, parking) -> flightCost flight + parkingCost parking + toFromTransportHub * 2M // for home and destination airport
        | Drive (drive, parking) -> driveCost drive + parkingCost parking
        | Train (train, parking) -> trainCost train + parkingCost parking + toFromTransportHub * 2M // for home and destination station

type LodgingDrivers = {RoomCost:decimal<Dollar/Person/Day>; Rooms: int<Person>; Nights: int<Day>}

let lodgingCost (inputs: LodgingDrivers) = 
    inputs.RoomCost * (decimal inputs.Rooms * decimal inputs.Nights |> DecimalWithMeasure<Person*Day>)

type MealDrivers = {Meal: decimal<Dollar/Meal>; Days: int<Day>; Meals: int<Meal/Person/Day>; People: int<Person>}
let mealCost inputs =
    inputs.Meal * (decimal inputs.Days * decimal inputs.Meals * decimal inputs.People |> DecimalWithMeasure<Meal>)
