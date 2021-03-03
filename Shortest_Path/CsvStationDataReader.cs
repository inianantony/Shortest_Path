using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace Shortest_Path
{
    public class CsvStationDataReader : IStationDataReader
    {
        private readonly string _filePath;

        public CsvStationDataReader(string filePath)
        {
            _filePath = filePath;
        }

        public List<RawStationData> GetRawStaionRecords()
        {
            List<RawStationData> rawStationDatas = new List<RawStationData>();
            using (var reader = new StreamReader(@"c:\tmp\StationMap.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<StationDataMap>();
                csv.Context.Configuration.Delimiter = ",";
                var stationData = new RawStationData();
                //var records = csv.EnumerateRecords(stationData);
                var records = csv.GetRecords<RawStationData>();
                foreach (var record in records)
                {
                    rawStationDatas.Add(record);
                }

                return rawStationDatas;
            }
        }
    }
}