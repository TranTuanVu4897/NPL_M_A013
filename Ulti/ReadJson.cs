using Newtonsoft.Json;
using NPL_M_A013.Model;
using System.Collections.Generic;
using System.IO;
using System;

namespace NPL_M_A013.Ulti
{
    class ReadJson
    {
        const string AIRPORT_DATAS = @"airport.json";
        const string HELICOPTER_DATAS = @"helicopter.json";
        const string FIXEDWING_DATAS = @"fixedwing.json";
        public List<Airport> GetAirportDatas()
        {
            var json = ReadJsonFile(AIRPORT_DATAS);
            return string.IsNullOrEmpty(json) ? new List<Airport>() : JsonConvert.DeserializeObject<List<Airport>>(json);
        }

        public List<Helicopter> GetHelicopterDatas()
        {
            var json = ReadJsonFile(HELICOPTER_DATAS);
            return string.IsNullOrEmpty(json) ? new List<Helicopter>() : JsonConvert.DeserializeObject<List<Helicopter>>(json);
        }

        public List<Fixedwing> GetFixedwingDatas()
        {
            var json = ReadJsonFile(FIXEDWING_DATAS);
            return string.IsNullOrEmpty(json) ? new List<Fixedwing>() : JsonConvert.DeserializeObject<List<Fixedwing>>(json);
        }
        private string ReadJsonFile(string file)
        {
            try
            {
                return File.ReadAllText(file);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Can not find {file}." +
                    $"\nCreate {file}");
                File.CreateText(file).Close();
                return string.Empty;
            }
        }

    }
}
