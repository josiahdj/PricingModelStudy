module Travel

open UnitsOfMeasure
open LanguagePrimitives

type ShortTermParkingDrivers = {Hours: int<Hour>; CostPerHour: decimal<Dollar/Hour>}
type LongTermParkingDrivers = {Days: int<Day>; CostPerDay: decimal<Dollar/Day>}
type ParkingDrivers = 
    | ShortTerm of ShortTermParkingDrivers
    | LongTerm of LongTermParkingDrivers

let parkingCost inputs =
    match inputs with
        | ShortTerm st -> st.CostPerHour * (decimal st.Hours |> DecimalWithMeasure<Hour>)
        | LongTerm lt -> lt.CostPerDay * (decimal lt.Days |> DecimalWithMeasure<Day>)


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
    | Flight of FlightDrivers * ParkingDrivers
    | Drive of DriveDrivers * ParkingDrivers
    | Train of TrainDrivers * ParkingDrivers


let rec transportCost inputs =
    match inputs with
        | Flight (flight, parking) -> flight.AirFare * (decimal flight.Seats |> DecimalWithMeasure<Person>) + parkingCost parking // + toFromAirportTransport
        | Drive (drive, parking) -> 
            match drive with
                | CostPerMile cpm -> cpm.CostPerMile * cpm.Distance + parkingCost parking
                | GasAndTolls gat -> gat.FuelCost / gat.FuelEfficiency * gat.Distance
                | DrivingRental drive -> drive.CostPerDay * (1M<Day> * decimal drive.Days)
        | Train (train, parking) -> train.CostPerSeat * (decimal train.Seats |> DecimalWithMeasure<Person>) + parkingCost parking // + toFromStationTransport

type LodgingDrivers = {RoomCost:decimal<Dollar/Person/Day>; Rooms: int<Person>; Nights: int<Day>}

let lodgingCost (inputs: LodgingDrivers) = 
    inputs.RoomCost * (decimal inputs.Rooms * decimal inputs.Nights |> DecimalWithMeasure<Person*Day>)

  