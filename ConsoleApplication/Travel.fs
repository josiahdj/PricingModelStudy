module Travel

open UnitsOfMeasure
open LanguagePrimitives

type FlightDrivers = {AirFare: decimal<Dollar/Person>; Seats: int<Person>}

type GasAndTollDrivers = {FuelCost:decimal<Dollar/Gallon>; FuelEfficiency:decimal<Mile/Gallon>; Distance:decimal<Mile>}
type CostPerMileDrivers = {CostPerMile:decimal<Dollar/Mile>; Distance:decimal<Mile>}
type RentalCarDrivers = {CostPerDay:decimal<Dollar/Day>; Days:int<Day>}
type DriveDrivers = 
    | GasAndTolls of GasAndTollDrivers
    | CostPerMile of CostPerMileDrivers
    | DrivingRental of RentalCarDrivers

type TrainDrivers = {CostPerSeat:decimal<Dollar/Person>; Seats: int<Person>}

type TransportDrivers = 
    | Flight of FlightDrivers
    | Drive of DriveDrivers
    | Train of TrainDrivers

let transportCost inputs =
    match inputs with
        | Flight flight -> flight.AirFare * (decimal flight.Seats |> DecimalWithMeasure<Person>)
        | Drive drive -> 
            match drive with
                | CostPerMile cpm -> cpm.CostPerMile * cpm.Distance
                | GasAndTolls gat -> gat.FuelCost / gat.FuelEfficiency * gat.Distance
                | DrivingRental drive -> drive.CostPerDay * (1M<Day> * decimal drive.Days)
        | Train train -> train.CostPerSeat * (decimal train.Seats |> DecimalWithMeasure<Person>)

type LodgingDrivers = {RoomCost:decimal<Dollar/Person/Day>; Rooms: int<Person>; Nights: int<Day>}

let lodgingCost (inputs: LodgingDrivers) = 
    inputs.RoomCost * (decimal inputs.Rooms * decimal inputs.Nights |> DecimalWithMeasure<Person*Day>)

  