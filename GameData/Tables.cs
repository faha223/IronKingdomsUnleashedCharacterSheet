using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronKingdomsUnleashedCharacterSheet.Enums;

namespace IronKingdomsUnleashedCharacterSheet.GameData
{
    public static class Tables
    {
        public static Dictionary<Race, RaceSpecification> Races { get; set; }
        public static List<Career> Careers { get; set; }
        public static List<Archetype> Archetypes { get; set; }
        public static void Initialize()
        {
            #region Load Race Specifications
            Races = new Dictionary<Race, RaceSpecification>();
            dynamic tables = JsonConvert.DeserializeObject(File.ReadAllText("GameData.json"));
            foreach (dynamic dyn in tables.RaceStats)
            {
                var rs = JsonConvert.DeserializeObject<RaceSpecification>(dyn.ToString());
                Races.Add(rs.Name, rs);
            }
            #endregion

            #region Load Archetypes
            Archetypes = new List<Archetype>
            {
                Archetype.Cunning,
                Archetype.Gifted,
                Archetype.Mighty,
                Archetype.Skilled
            };
            #endregion

            #region Load Careers
            Careers = JsonConvert.DeserializeObject<List<Career>>(tables.Careers.ToString());
            #endregion
        }
        public static void WriteToJson()
        {
            object obj = new
            {
                RaceStats = Races.Select(c => c.Value),
                Careers = Careers,
            };

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.Formatting = Formatting.Indented;
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            serializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            serializerSettings.Converters.Add(new StringEnumConverter());

            File.WriteAllText("GameData.json", JsonConvert.SerializeObject(obj, Formatting.Indented, serializerSettings));
        }
    }
}
