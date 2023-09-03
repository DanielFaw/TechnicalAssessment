using Microsoft.VisualBasic;
using PenskeTechnicalAssessment.JSONObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenskeTechnicalAssessment.Models
{
    public class Series
    {
        public int RaceID { get; set; }
        public int SeriesID { get; set; }
        public int RaceSeason { get; set; }
        public string? RaceName { get; set; }
        public int RaceTypeID { get; set; }
        public bool? RestrictorPlate { get; set; }
        public int TrackID { get; set; }
        public string TrackName { get; set; }
        public DateTime? DateScheduled { get; set; }
        public DateTime?RaceDate { get; set; }
        public DateTime? QualifyingDate { get; set; }
        public DateTime? TuneinDate { get; set; }
        public float ScheduledDistance { get; set; }
        public float ActualDistance { get; set; }
        public int ScheduledLaps { get; set; }
        public int ActualLaps { get; set; }
        public int Stage1Laps { get; set; }
        public int Stage2Laps { get; set; }
        public int Stage3Laps { get; set; }
        public int NumberOfCarsInField { get; set; }
        public int PoleWinnerDriverID { get; set; }
        public float PoleWinnerSpeed { get; set; }
        public int NumberOfLeadChanges { get; set; }
        public int NumberOfLeaders { get; set; }
        public int NumberOfCautions { get; set; }
        public int NumberOfCautionLaps { get; set; }
        public float AverageSpeed { get; set; }
        public string? TotalRaceTime { get; set; }
        public string MarginOfVictory { get; set; }
        public int RacePurse { get; set; }
        public string RaceComments { get; set; }
        public int Attendance { get; set; }

        public string RadioBroadcaster { get; set; }
        public string TelevisionBroadcaster { get; set; }
        public string SatelliteRadioBroadcaster { get; set; }

        public int MasterRaceId { get; set; }
        public bool InspectionComplete { get; set; }
        public int PlayoffRound { get; set; }
        public bool IsQualifyingRace { get; set; }
        public int QualifyingRaceNumber { get; set; }
        public int QualifyingRaceID { get; set; }
        public bool HasQualifying { get; set; }
        public int? WinnerDriverID { get; set; }
        public DateTime? PoleWinnerLaptime { get; set; }


        //Non-JSON data
        public bool HasHappened => WinnerDriverID.HasValue;
        public int Year { get; set; }
        public LapDataRequest RequestData => new LapDataRequest(Year, SeriesID, RaceID, RaceName, TrackName);

        public Series(JSONSeries dataSeries)
        {
            RaceID = dataSeries.raceId;
            SeriesID = dataSeries.seriesId;
            RaceSeason = dataSeries.raceSeason;
            RaceName = dataSeries.raceName;
            RaceTypeID = dataSeries.raceTypeId;
            RestrictorPlate = dataSeries.restrictorPlate;
            TrackID = dataSeries.trackId;
            TrackName = dataSeries.trackName;
            DateScheduled = dataSeries.dateScheduled;
            RaceDate = dataSeries.raceDate;
            QualifyingDate = dataSeries.qualifyingDate;
            TuneinDate = dataSeries.tuneinDate;
            ScheduledDistance = dataSeries.scheduledDistance;
            ActualDistance = dataSeries.actualDistance;
            ScheduledLaps = dataSeries.scheduledLaps;
            ActualLaps = dataSeries.actualLaps;
            Stage1Laps = dataSeries.stage1Laps;
            Stage2Laps = dataSeries.stage2Laps;
            Stage3Laps = dataSeries.stage3Laps;
            NumberOfCarsInField = dataSeries.numberOfCarsInField;
            PoleWinnerDriverID = dataSeries.poleWinnerDriverId;
            PoleWinnerSpeed = dataSeries.poleWinnerSpeed;
            NumberOfLeadChanges = dataSeries.numberOfLeadChanges;
            NumberOfLeaders = dataSeries.numberOfLeaders;
            NumberOfCautions = dataSeries.numberOfCautions;
            NumberOfCautionLaps = dataSeries.numberOfCautionLaps;
            AverageSpeed = dataSeries.averageSpeed;
            TotalRaceTime = dataSeries.totalRaceTime;
            MarginOfVictory = dataSeries.marginOfVictory;
            RacePurse = dataSeries.racePurse;
            RaceComments = dataSeries.raceComments;
            Attendance = dataSeries.attendance;
            RadioBroadcaster = dataSeries.radioBroadcaster;
            TelevisionBroadcaster = dataSeries.televisionBroadcaster;
            SatelliteRadioBroadcaster = dataSeries.satelliteRadioBroadcaster;
            MasterRaceId = dataSeries.masterRaceId;
            InspectionComplete = dataSeries.inspectionComplete;
            PlayoffRound = dataSeries.playoffRound;
            IsQualifyingRace = dataSeries.isQualifyingRace;
            QualifyingRaceNumber = dataSeries.qualifyingRaceNo;
            QualifyingRaceID = dataSeries.qualifyingRaceId;
            HasQualifying = dataSeries.hasQualifying;
            WinnerDriverID = dataSeries.winnerDriverId;
            PoleWinnerLaptime = dataSeries.poleWinnerLaptime;

        }

    }

    public class SeriesList
    {
        public string SeriesName {  get; set; }
        public int SeriesNumber { get; set; }
        public List<Series>? QueryResult { get; set; }
    }

    public class LapDataRequest
    {
        public int Year { get; set; }
        public int Series { get; set; }
        public int RaceID { get; set; }
        public string RaceName { get; set; }
        public string TrackName {  set; get; }

        public LapDataRequest(int year, int series, int raceId, string raceName, string trackName)
        {
            Year = year;
            Series = series;
            RaceID = raceId;
            RaceName = raceName;
            TrackName = trackName;
        }



    }
}
