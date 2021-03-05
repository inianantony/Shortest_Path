using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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

        public List<RawStationData> GetRawStationRecords()
        {
            using var reader = new StreamReader(_filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<StationDataMap>();
            csv.Context.Configuration.Delimiter = ",";
            return csv.GetRecords<RawStationData>().ToList();
        }
    }
}