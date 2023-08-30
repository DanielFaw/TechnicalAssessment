using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenskeTechnicalAssessment.Models
{
    public class Series
    {
        public int raceId { get; set; }
        public int seriesId { get; set; }
        public int raceSeason { get; set; }
        public string? raceName { get; set; }
        public int raceTypeId { get; set; }
        public bool? restrictorPlate { get; set; }
        public int trackId {  get; set; }
        public string trackName { get; set; }
        public DateTime? dateScheduled { get; set; }
        public DateTime? raceDate { get; set; }
        public DateTime? qualifyingDate { get; set; }
        public DateTime? tuneinDate { get; set; }
        public float scheduledDistance { get; set; }
        public float actualDistance { get; set; }
        public int scheduledLaps { get; set; }
        public int actualLaps { get; set; }
        public int stage1Laps {  get; set; } 
        public int stage2Laps { get; set; }
        public int stage3Laps { get; set; }
        public int numberOfCarsInField { get;set; }
        public int poleWinnerDriverId { get; set; }
        public float poleWinnerSpeed { get; set; }
        public int numberOfLeadChanges { get; set; }
        public int numberOfLeaders { get; set; }
        public int numberOfCautions { get; set; }
        public int numberOfCautionLaps { get; set; }
        public float averageSpeed { get; set; }
        public string? totalRaceTime { get; set; }
        public string marginOfVictory {  get; set; }
        public int racePurse {  get; set; }
        public string raceComments {  get; set; }
        public int attendance { get; set; }

        public List<Schedule?> schedules { get; set; }

        public string radioBroadcaster { get; set; }
        public string televisionBroadcaster {  get; set; }
        public string satelliteRadioBroadcaster {  get; set; }

        public int masterRaceId { get; set; }
        public bool inspectionComplete { get; set; }
        public int playoffRound { get; set; }
        public bool isQualifyingRace { get; set; }
        public int qualifyingRaceNo {  get; set; }
        public int qualifyingRaceId { get; set; }
        public bool hasQualifying {  get; set; }
        public int? winnerDriverId { get; set; }
        public DateTime? poleWinnerLaptime { get; set; }


    }

    public class SeriesList
    {
        public string SeriesName {  get; set; }
        public int SeriesIndex { get; set; }
        public List<Series>? QueryResult { get; set; }
    }
}
