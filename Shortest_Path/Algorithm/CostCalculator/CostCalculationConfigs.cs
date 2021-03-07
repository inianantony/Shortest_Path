using System.Collections.Generic;

namespace Shortest_Path.Algorithm.CostCalculator
{
    public static class CostCalculationConfigs
    {
        public static List<string> NeNsLines = new List<string> { "NE", "NS" };
        public static List<string> DtTeLines = new List<string> { "DT", "TE" };
        public static List<string> TeLine = new List<string> { "TE" };
        public static decimal PeakHourNeNsCost = 12;
        public static decimal NonPeakInDtTeCost = 8;
        public static decimal NonPeakInAllLinesCost = 10;
        public static decimal NightInTeCost = 8;
        public static decimal NightInOtherLinesCost = 10;
        public static decimal InterchangingAtPeakHourCost = 15;
        public static decimal InterchangingAtNonPeakCost = 10;
        public static decimal InterchangingAtNightCost = 10;
        public static decimal BaseCost = 1;
    }
}
