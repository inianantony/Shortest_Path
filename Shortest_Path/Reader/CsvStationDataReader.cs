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
        public List<RawStationData> GetRawStationRecords(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<StationDataMap>();
            csv.Context.Configuration.Delimiter = ",";
            return csv.GetRecords<RawStationData>().ToList();
        }
    }
}