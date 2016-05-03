using Newtonsoft.Json;

namespace IronKingdomsUnleashedCharacterSheet.GameData
{
    public class RangedWeapon : Weapon
    {
        public int Ammo { get; set; }

        [JsonIgnore]
        public int Range
        {
            get
            {
                return Stat1;
            }
            set
            {
                Stat1 = value;
            }
        }

        [JsonIgnore]

        public int Rating
        {
            get
            {
                return Stat2;
            }
            set
            {
                Stat2 = value;
            }
        }

        [JsonIgnore]

        public int Power
        {
            get
            {
                return Stat3;
            }
            set
            {
                Stat3 = value;
            }
        }
    }
}
