using Newtonsoft.Json.Converters;
using PowerplantCodingChallenge.Controllers;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace PowerplantCodingChallenge
{
    public class PowerPlantResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("p")]
        public double Power { get; set; } 
    }

    public class PowerPlantRequest
    {
        [JsonPropertyName("load")]
        public int Load { get; set; }

        [JsonPropertyName("fuels")]
        public Fuel Fuels { get; set; }

        [JsonPropertyName("powerplants")]
        public required IEnumerable<PowerPlant> PowerPlants { get; set; }
    }

    public class Fuel
    {
        [JsonPropertyName("gas(euro/MWh)")]
        public float Gas { get; set; }

        [JsonPropertyName("kerosine(euro/MWh)")]
        public float Kerosine { get; set; }
      
        [JsonPropertyName("co2(euro/ton)")]
        public float CO2 { get; set; }

        [JsonPropertyName("wind(%)")]
        public float Wind { get; set; }
     }

    public class PowerPlant
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public PlantType Type { get; set; }

        [JsonPropertyName("efficiency")]
        public float Efficiency { get; set; }

        [JsonPropertyName("pmin")]
        public int PMin { get; set; }

        [JsonPropertyName("pmax")]
        public int PMax { get; set; }
    }

    public class PowerPlantDTO
    {
        public string Name { get; set; }

        public int PMin { get; set; }

        public int PMax { get; set; }

        public float PriceMWh { get; set; }

        public float PMaxToDeliver { get; set; }
    }

    public static class Load
    {
        public static IEnumerable<PowerPlantResponse> Calculate(PowerPlantRequest request)
        {
            List<PowerPlantDTO> plants = request.PowerPlants.Select(plant =>
            {
                return new PowerPlantDTO()
                {
                    Name = plant.Name,
                    PriceMWh = (plant.Type.Equals(PlantType.Gasfired) ? (request.Fuels.Gas / plant.Efficiency) :
                                plant.Type.Equals(PlantType.Turbojet) ? (request.Fuels.Kerosine / plant.Efficiency) : 0),
                    PMaxToDeliver = plant.Type.Equals(PlantType.Windturbine) ? ((request.Fuels.Wind / 100) * plant.PMax) : plant.PMax,
                    PMax = plant.PMax,
                    PMin = plant.PMin
                };
            }).ToList();
          
            float TotalPower = 0;

            return plants
                .OrderBy(x => x.PMaxToDeliver == 0)
                .ThenBy(x => x.PriceMWh)
                .ThenByDescending(y => y.PMax)
                .Select(z => {                    
                    z.PMaxToDeliver = (TotalPower <= request.Load ?
                    (z.PMaxToDeliver + TotalPower) >= request.Load ?
                     (request.Load - TotalPower) : z.PMaxToDeliver : 0);
                    TotalPower += z.PMaxToDeliver;
                    return new PowerPlantResponse()
                    {
                        Name = z.Name,
                        Power = (Math.Truncate(z.PMaxToDeliver * 10) / 10)
                    }; 
                });
        }
    }


    

    public enum PlantType
    {
        [EnumMember(Value = "gasfired")]
        Gasfired,
        [EnumMember(Value = "turbojet")]
        Turbojet,
        [EnumMember(Value = "windturbine")]
        Windturbine
    }
}