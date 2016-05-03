using System.Linq;

namespace IronKingdomsUnleashedCharacterSheet.Enums
{
    public class Career
    {
        public string Name { get; set; }
        public Race[] RequiredRace { get; set; }
        public Sex? RequiredSex { get; set; }
        public Archetype? RequiredArchetype { get; set; }
        public bool StartingCareerOnly { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public static bool RacePermitted(Career c, Race r)
        {
            return (c.RequiredRace == null || c.RequiredRace.Contains(r));
        }

        public static bool SexPermitted(Career c, Sex s)
        {
            return (!c.RequiredSex.HasValue || c.RequiredSex.Value == s);
        }

        public static bool ArchetypePermitted(Career c, Archetype a)
        {
            return (!c.RequiredArchetype.HasValue || c.RequiredArchetype.Value == a);
        }
    }
}
