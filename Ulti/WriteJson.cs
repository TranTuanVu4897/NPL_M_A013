using Newtonsoft.Json;
using NPL_M_A013.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NPL_M_A013.Ulti
{
    class WriteJson
    {
        const string AIRPORT_DATAS = @"airport.json";
        const string HELICOPTER_DATAS = @"helicopter.json";
        const string FIXEDWING_DATAS = @"fixedwing.json";

        public void WriteAirportDatas(List<Airport> airports)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            if (!File.Exists(AIRPORT_DATAS))
                using (StreamWriter sw = File.CreateText(AIRPORT_DATAS))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    jsonSerializer.Serialize(writer, airports);
                }
            else
            {
                File.Delete(AIRPORT_DATAS);
                StreamWriter sw = File.CreateText(AIRPORT_DATAS);
                using (sw)
                using (JsonWriter writer = new JsonTextWriter(sw))
                    jsonSerializer.Serialize(writer, airports);
                sw.Close();
            }
        }

        public void WriteFixedwingDatas(List<Fixedwing> fixedwings)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            if (!File.Exists(FIXEDWING_DATAS))
                using (StreamWriter sw = File.CreateText(FIXEDWING_DATAS))
                using (JsonWriter writer = new JsonTextWriter(sw))
                    jsonSerializer.Serialize(writer, fixedwings);

            else
            {
                File.Delete(FIXEDWING_DATAS);
                StreamWriter sw = File.CreateText(FIXEDWING_DATAS);
                using (sw)
                using (JsonWriter writer = new JsonTextWriter(sw))
                    jsonSerializer.Serialize(writer, fixedwings);
                sw.Close();
            }
        }

        public void WriteHelicopterDatas(List<Helicopter> helicopters)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            if (!File.Exists(HELICOPTER_DATAS))
                using (StreamWriter sw = File.CreateText(HELICOPTER_DATAS))
                using (JsonWriter writer = new JsonTextWriter(sw))
                    jsonSerializer.Serialize(writer, helicopters);

            else
            {
                File.Delete(HELICOPTER_DATAS);
                StreamWriter sw = File.CreateText(HELICOPTER_DATAS);
                using (sw)
                using (JsonWriter writer = new JsonTextWriter(sw))
                    jsonSerializer.Serialize(writer, helicopters);
                sw.Close();
            }
        }

    }
}
