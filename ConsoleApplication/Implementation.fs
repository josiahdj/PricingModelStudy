module Implementation

open Travel
open Equipment
open UnitsOfMeasure

type ImplementationDrivers = {TeamMembers: int<Person>; TripsPerMonth: int<Trip/Month>; TripLength: int<Day>; TravelDrivers: TransportDrivers; LodgingDrivers: LodgingDrivers}
type ImplementationCosts = {TransportCost: decimal<Dollar/Month>; LodgingCost: decimal<Dollar/Month>; EquipmentCost: decimal<Dollar/Month>; CrewCost: decimal<Dollar/Month>; OtherCost: decimal<Dollar/Month>}

let implementationCost inputs =
    let transport = (transportCost inputs.TravelDrivers / 1M<Trip>) * (decimal inputs.TripsPerMonth * 1M<Trip/Month>)
    let lodging = (lodgingCost inputs.LodgingDrivers / 1M<Trip>) * (decimal inputs.TripsPerMonth * 1M<Trip/Month>)
    let equipment = equipmentCost inputs
    let crew = crewCost inputs
    let other = otherCost inputs
    {TransportCost = transport; LodgingCost = lodging; EquipmentCost = equipment; CrewCost = crew; OtherCost = other}
