using Newtonsoft.Json;
using IronKingdomsUnleashedCharacterSheet.BaseClasses;

namespace IronKingdomsUnleashedCharacterSheet.GameData
{
    public class Skill : PropertyChangedNotifier
    {
        [JsonIgnore]
        private string _name { get; set; }
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        [JsonIgnore]
        public int _setParentValue { get; set; }
        public int SetParentValue
        {
            get
            {
                return _setParentValue;
            }

            set
            {
                _setParentValue = value;
                NotifyPropertyChanged("SetParentValue");
                NotifyPropertyChanged("Total");
            }
        }

        [JsonIgnore]
        private int _skillLevel { get; set; }
        public int SkillLevel
        {
            get
            {
                return _skillLevel;
            }

            set
            {
                _skillLevel = value;
                NotifyPropertyChanged("SkillLevel");
                NotifyPropertyChanged("Total");
            }
        }

        [JsonIgnore]
        public int Total
        {
            get
            {
                return SetParentValue + SkillLevel;
            }
        }

        public bool NameIsReadOnly { get; set; }

        public Skill()
        {
            NameIsReadOnly = false;
        }
    }
}
