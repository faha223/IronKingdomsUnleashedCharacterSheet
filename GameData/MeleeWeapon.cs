using Newtonsoft.Json;

namespace IronKingdomsUnleashedCharacterSheet.GameData
{
    public class MeleeWeapon : Weapon
    {
        [JsonIgnore]
        public int Material
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
        public int P
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
        public int S
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

        [JsonIgnore]
        public int PSComposite
        {
            get
            {
                return P + S;
            }
        }
    }
}
