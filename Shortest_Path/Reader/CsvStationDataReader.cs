using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using Shortest_Path.Mapper;
using Shortest_Path.Models;

namespace Shortest_Path.Reader
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
            var rawStationDatas = new List<RawStationData>();
            using (var reader = new StreamReader(_filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<StationDataMap>();
                csv.Context.Configuration.Delimiter = ",";
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