using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using IronKingdomsUnleashedCharacterSheet.DataHelpers;
using IronKingdomsUnleashedCharacterSheet.Enums;
using IronKingdomsUnleashedCharacterSheet.GameData;
using IronKingdomsUnleashedCharacterSheet.UIHelpers;

namespace IronKingdomsUnleashedCharacterSheet.ViewModels
{
    [Serializable]
    public class CharacterSheetViewModel : ViewModelBase
    {
        #region Profile Info
        private string _charName { get; set; }
        public string CharacterName
        {
            get
            {
                return _charName;
            }
            set
            {
                _charName = value;
                NotifyPropertyChanged("CharacterName");
            }
        }

        private int _charHeight { get; set; }
        public int CharacterHeight
        {
            get
            {
                return _charHeight;
            }
            set
            {
                _charHeight = value;
                NotifyPropertyChanged("CharacterHeight");
            }
        }

        private int _charWeight { get; set; }
        public int CharacterWeight
        {
            get
            {
                return _charWeight;
            }
            set
            {
                _charWeight = Math.Max(0, value);
            }
        }

        private Archetype _archetype { get; set; }
        public Archetype Archetype
        {
            get
            {
                return _archetype;
            }
            set
            {
                _archetype = value;
                NotifyPropertyChanged("Archetype");
            }
        }

        [JsonProperty]
        private List<string> _careers { get; set; }
        [JsonIgnore]
        public IEnumerable<object> SelectedCareers
        {
            get
            {
                if(_careers != null)
                    return Tables.Careers.Where(c => _careers.Contains(c.Name));
                return null;
            }
            set
            {
                _careers = value.Cast<Career>().Select(c => c.Name).ToList();
                NotifyPropertyChanged("SelectedCareers");
            }
        }

        private string _playerName { get; set; }
        public string PlayerName
        {
            get
            {
                return _playerName;
            }
            set
            {
                _playerName = value;
                NotifyPropertyChanged("PlayerName");
            }
        }

        private string _definingCharacteristics { get; set; }
        public string DefiningCharacteristics
        {
            get
            {
                return _definingCharacteristics;
            }
            set
            {
                _definingCharacteristics = value;
                NotifyPropertyChanged("DefiningCharacteristics");
            }
        }

        [JsonProperty]
        private Race _race { get; set; }
        [JsonIgnore]
        public RaceSpecification Race
        {
            get
            {
                return Tables.Races[_race];
            }
            set
            {
                _race = value.Name;
                SetStartingStats(value);
                NotifyPropertyChanged("Race");
            }
        }

        private Sex _sex { get; set; }
        public Sex Sex
        {
            get
            {
                return _sex;
            }
            set
            {
                _sex = value;
                NotifyPropertyChanged("Sex");
            }
        }

        private string _level { get; set; }
        public string Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
                NotifyPropertyChanged("Level");
            }
        }

        private int _totalXP { get; set; }
        public int TotalXPEarned
        {
            get
            {
                return _totalXP;
            }
            set
            {
                _totalXP = value;
                NotifyPropertyChanged("TotalXPEarned");
            }
        }
        #endregion

        #region Character Portrait
        [JsonIgnore]
        private byte[] _binaryCharacterPortrait { get; set; }
        public byte[] BinaryCharacterPortrait
        {
            get
            {
                return _binaryCharacterPortrait;
            }
            set
            {
                _binaryCharacterPortrait = value;
                NotifyPropertyChanged("BinaryCharacterPortrait");
                NotifyPropertyChanged("CharacterPortrait");
            }
        }

        [JsonIgnore]
        public BitmapImage CharacterPortrait
        {
            get
            {
                return _binaryCharacterPortrait.GetBitmapImage();
            }
        }

        private void ExecuteChangeCharacterPortrait()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "All Images|*.bmp;*.jpg;*.png;*.tif;*.exif";
            var result = dlg.ShowDialog();
            if(result.HasValue && result.Value)
            {
                BinaryCharacterPortrait = File.ReadAllBytes(dlg.FileName);
            }
        }

        [JsonIgnore]
        public ICommand ChangeCharacterPortraitCommand { get { return new ParameterlessCommandRouter(ExecuteChangeCharacterPortrait, DefaultCanExecute); } }
        #endregion

        #region Player Stats
        [JsonIgnore]
        private int _physique { get; set; }
        public int Physique
        {
            get
            {
                return _physique;
            }
            set
            {
                _physique = value;
                NotifyPropertyChanged("Physique");
            }
        }

        [JsonIgnore]
        public int MaxPhysique
        {
            get
            {
                return GetMaxValue(Races.First(c => c.Name == _race).Physique);
            }
        }

        private int _speed { get; set; }
        public int Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
                NotifyPropertyChanged("Speed");
            }
        }

        [JsonIgnore]
        public int MaxSpeed
        {
            get
            {
                return GetMaxValue(Races.First(c => c.Name == _race).Speed);
            }
        }

        private int _strength { get; set; }
        public int Strength
        {
            get
            {
                return _strength;
            }
            set
            {
                _strength = value;
                NotifyPropertyChanged("Strength");
            }
        }

        [JsonIgnore]
        public int MaxStrength
        {
            get
            {
                return GetMaxValue(Races.First(c => c.Name == _race).Strength);
            }
        }

        private int _agility;
        public int Agility
        {
            get
            {
                return _agility;
            }
            set
            {
                _agility = value;
                NotifyPropertyChanged("Agility");
            }
        }

        [JsonIgnore]
        public int MaxAgility
        {
            get
            {
                return GetMaxValue(Races.First(c => c.Name == _race).Agility);
            }
        }

        private int _prowess { get; set; }
        public int Prowess
        {
            get
            {
                return _prowess;
            }
            set
            {
                _prowess = value;
                NotifyPropertyChanged("Prowess");
            }
        }

        [JsonIgnore]
        public int MaxProwess
        {
            get
            {
                return GetMaxValue(Races.First(c => c.Name == _race).Prowess);
            }
        }

        private int _poise { get; set; }
        public int Poise
        {
            get
            {
                return _poise;
            }
            set
            {
                _poise = value;
                NotifyPropertyChanged("Poise");
            }
        }

        [JsonIgnore]
        public int MaxPoise
        {
            get
            {
                return GetMaxValue(Races.First(c => c.Name == _race).Poise);
            }
        }

        private int _intelligence { get; set; }
        public int Intelligence
        {
            get
            {
                return _intelligence;
            }
            set
            {
                _intelligence = value;
                NotifyPropertyChanged("Intelligence");
            }
        }

        [JsonIgnore]
        public int MaxIntelligence
        {
            get
            {
                return GetMaxValue(Races.First(c => c.Name == _race).Intelligence);
            }
        }

        private int _arcane { get; set; }
        public int Arcane
        {
            get
            {
                return _arcane;
            }
            set
            {
                _arcane = value;
                NotifyPropertyChanged("Arcane");
            }
        }

        [JsonIgnore]
        public int MaxArcane
        {
            get
            {
                return GetMaxValue(Races.First(c => c.Name == _race).Arcane);
            }
        }

        private int _perception { get; set; }
        public int Perception
        {
            get
            {
                return _perception;
            }
            set
            {
                _perception = value;
                NotifyPropertyChanged("Perception");
            }
        }

        [JsonIgnore]
        public int MaxPerception
        {
            get
            {
                return GetMaxValue(Races.First(c => c.Name == _race).Perception);
            }
        }

        [JsonIgnore]
        public int Willpower
        {
            get
            {
                return Physique + Intelligence;
            }
        }
        #endregion

        #region Defense Stats
        public int RacialModifier
        {
            get
            {
                return 0;
            }
        }

        public int EquipmentModifier
        {
            get
            {
                return 0;
            }
        }

        [JsonIgnore]
        public int TotalDefense
        {
            get
            {
                return Speed + Agility + Perception + RacialModifier + EquipmentModifier;
            }
        }
        #endregion

        #region Armor Stats
        private int _armorModifier { get; set; }
        public int ArmorModifier
        {
            get
            {
                return _armorModifier;
            }
            set
            {
                _armorModifier = value;
                NotifyPropertyChanged("ArmorModifier");
            }
        }

        private int _sheildModifier { get; set; }
        public int ShieldModifier
        {
            get
            {
                return _sheildModifier;
            }
            set
            {
                _sheildModifier = value;
                NotifyPropertyChanged("ShieldModifier");
            }
        }

        private int _otherModifier { get; set; }
        public int OtherModifier
        {
            get
            {
                return _otherModifier;
            }
            set
            {
                _otherModifier = value;
                NotifyPropertyChanged("OtherModifier");
            }
        }

        [JsonIgnore]
        public int TotalArmor
        {
            get
            {
                return Physique + ArmorModifier + ShieldModifier + OtherModifier;
            }
        }
        #endregion

        #region Initiative
        private int _additionalModifier { get; set; }
        public int AdditionalModifier
        {
            get
            {
                return _additionalModifier;
            }
            set
            {
                _additionalModifier = value;
                NotifyPropertyChanged("AdditionalModifier");
            }
        }

        [JsonIgnore]
        public int TotalInitiative
        {
            get
            {
                return Speed + Prowess + Perception + EquipmentModifier + AdditionalModifier;
            }
        }
        #endregion

        #region CommandRange
        private int _commandSkill { get; set; }
        public int CommandSkill
        {
            get
            {
                return _commandSkill;
            }
            set
            {
                _commandSkill = value;
                NotifyPropertyChanged("CommandSkill");
            }
        }

        private int _abilityModifier { get; set; }
        public int AbilityModifier
        {
            get
            {
                return _abilityModifier;
            }
            set
            {
                _abilityModifier = value;
                NotifyPropertyChanged("AbilityModifier");
            }
        }

        [JsonIgnore]
        public int TotalCommandRange
        {
            get
            {
                return Intelligence + CommandSkill + AbilityModifier;
            }
        }
        #endregion

        #region Ranged Weapons
        private RangedWeapon _rangedWeapon1 { get; set; }
        public RangedWeapon RangedWeapon1
        {
            get
            {
                return _rangedWeapon1;
            }
            set
            {
                _rangedWeapon1 = value;
                NotifyPropertyChanged("RangedWeapon1");
            }
        }
        private RangedWeapon _rangedWeapon2 { get; set; }
        public RangedWeapon RangedWeapon2
        {
            get
            {
                return _rangedWeapon2;
            }
            set
            {
                _rangedWeapon2 = value;
                NotifyPropertyChanged("RangedWeapon2");
            }
        }
        #endregion

        #region Melee Weapons
        private MeleeWeapon _meleeWeapon1 { get; set; }
        public MeleeWeapon MeleeWeapon1
        {
            get
            {
                return _meleeWeapon1;
            }
            set
            {
                _meleeWeapon1 = value;
                NotifyPropertyChanged("MeleeWeapon1");
            }
        }
        private MeleeWeapon _meleeWeapon2 { get; set; }
        public MeleeWeapon MeleeWeapon2
        {
            get
            {
                return _meleeWeapon2;
            }
            set
            {
                _meleeWeapon2 = value;
                NotifyPropertyChanged("MeleeWeapon2");
            }
        }
        #endregion

        #region Additional Weapon
        public Weapon AdditionalWeapon { get; set; }
        #endregion

        #region Skills
        public ObservableCollection<Skill> Skills { get; set; }
        #endregion

        #region Damage Capacity
        public ObservableCollection<bool> Damage { get; set; }
        #endregion

        #region Benefits and Abilities
        public ObservableCollection<BookRef> BenefitsAndAbilities { get; set; }
        #endregion

        #region Gear
        public ObservableCollection<Gear> Gear { get; set; }
        #endregion

        #region Worn Armor
        public ObservableCollection<WornArmor> WornArmor { get; set; }
        #endregion

        #region Connections
        public ObservableCollection<BookRef> Connections { get; set; }
        #endregion

        #region Spells
        private string _arcaneTradition { get; set; }
        public string ArcaneTradition
        {
            get
            {
                return _arcaneTradition;
            }
            set
            {
                _arcaneTradition = value;
                NotifyPropertyChanged("ArcaneTradition");
            }
        }

        public ObservableCollection<Spell> Spells { get; set; }
        #endregion

        #region Notes
        private string _notes { get; set; }
        public string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                _notes = value;
                NotifyPropertyChanged("Notes");
            }
        }
        #endregion

        #region Permanent Injuries
        private string _permanentInjuries { get; set; }
        public string PermanentInjuries
        {
            get
            {
                return _permanentInjuries;
            }
            set
            {
                _permanentInjuries = value;
                NotifyPropertyChanged("PermanentInjuries");
            }
        }
        #endregion

        #region Spoken Languages
        private string _spokenLanguages { get; set; }
        public string SpokenLanguages
        {
            get
            {
                return _spokenLanguages;
            }
            set
            {
                _spokenLanguages = value;
                NotifyPropertyChanged("SpokenLanguages");
            }
        }
        #endregion

        #region Religious Beliefs
        private string _religiousBeliefs { get; set; }
        public string ReligiousBeliefs
        {
            get
            {
                return _religiousBeliefs;
            }
            set
            {
                _religiousBeliefs = value;
                NotifyPropertyChanged("ReligiousBeliefs");
            }
        }
        #endregion

        #region Gold
        public int Gold { get; set; }
        #endregion

        #region Table Proxies
        [JsonIgnore]
        public IEnumerable<RaceSpecification> Races
        {
            get
            {
                return Tables.Races.Select(c => c.Value);
            }
        }

        [JsonIgnore]
        public IEnumerable<Archetype> Archetypes
        {
            get
            {
                return GameData.Tables.Archetypes;
            }
        }

        [JsonIgnore]
        public IEnumerable<Career> Careers
        {
            get
            {
                return GameData.Tables.Careers.Where(c => Career.RacePermitted(c, _race)
                                                && Career.SexPermitted(c, _sex)
                                                && Career.ArchetypePermitted(c, _archetype));
            }
        }

        [JsonIgnore]
        public List<Sex> Sexes
        {
            get
            {
                return new List<Sex>(new Sex[] { Sex.Male, Sex.Female });
            }
        }
        #endregion

        #region General Use
        private bool DefaultCanExecute()
        {
            return true;
        }
        #endregion

        public CharacterSheetViewModel() : base()
        {
            Damage = new ObservableCollection<bool>();
            Skills = new ObservableCollection<Skill>();
            BenefitsAndAbilities = new ObservableCollection<BookRef>();
            Spells = new ObservableCollection<Spell>();
            Gear = new ObservableCollection<Gear>();
            WornArmor = new ObservableCollection<WornArmor>();
            Connections = new ObservableCollection<BookRef>();
        }

        public static CharacterSheetViewModel Generate()
        {
            var vm = new CharacterSheetViewModel();
            vm.CharacterHeight = 72;
            vm.CharacterWeight = 200;
            vm.SetStartingStats(vm.Races.First(c => c.Name == vm._race));

            vm.RangedWeapon1 = new RangedWeapon();
            vm.RangedWeapon2 = new RangedWeapon();
            vm.MeleeWeapon1 = new MeleeWeapon();
            vm.MeleeWeapon2 = new MeleeWeapon();
            vm.AdditionalWeapon = new Weapon();

            vm.Skills = new ObservableCollection<Skill>
            {
                new Skill()
                {
                    Name = "hand weapon (prw)",
                    NameIsReadOnly = true
                },
                new Skill()
                {
                    Name = "great weapon (prw)",
                    NameIsReadOnly = true
                },
                new Skill()
                {
                    Name = "pistol (poi)",
                    NameIsReadOnly = true
                },
                new Skill()
                {
                    Name = "rifle (poi)",
                    NameIsReadOnly = true
                },
                new Skill(),
                new Skill(),
                new Skill(),
                new Skill()
                {
                    Name = "detection (per)",
                    NameIsReadOnly = true
                },
                new Skill()
                {
                    Name = "sneak (agl)",
                    NameIsReadOnly = true
                },
                new Skill()
                {
                    Name = "command (social)",
                    NameIsReadOnly = true
                },
                new Skill(),
                new Skill(),
                new Skill(),
                new Skill(),
                new Skill(),
                new Skill(),
                new Skill()
            };

            for (int i = 0; i < 36; i++)
                vm.Damage.Add(false);

            for (int i = 0; i < 20; i++)
                vm.BenefitsAndAbilities.Add(new BookRef());

            for (int i = 0; i < 8; i++)
                vm.Spells.Add(new Spell());

            for (int i = 0; i < 19; i++)
                vm.Gear.Add(new Gear());

            for (int i = 0; i < 4; i++)
                vm.WornArmor.Add(new WornArmor());

            for (int i = 0; i < 4; i++)
                vm.Connections.Add(new BookRef());

            return vm;
        }

        protected override void _internalOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch(args.PropertyName)
            {
                case "Race":
                    NotifyPropertyChanged("RacialModifier");
                    NotifyPropertyChanged("Careers");
                    break;
                case "Sex":
                case "Archetype":
                    NotifyPropertyChanged("Careers");
                    break;

                case "Physique":
                    NotifyPropertyChanged("Willpower");
                    NotifyPropertyChanged("TotalArmor");
                    break;
                case "Intelligence":
                    NotifyPropertyChanged("Willpower");
                    NotifyPropertyChanged("TotalCommandRange");
                    break;
                case "OtherModifier":
                case "ShieldModifier":
                case "ArmorModifier":
                    NotifyPropertyChanged("TotalArmor");
                    break;
                case "Agility":
                    NotifyPropertyChanged("TotalDefense");
                    break;
                case "Speed":
                    NotifyPropertyChanged("TotalInitiative");
                    NotifyPropertyChanged("TotalDefense");
                    break;
                case "Perception":
                    NotifyPropertyChanged("TotalInitiative");
                    NotifyPropertyChanged("TotalDefense");
                    break;
                case "Prowess":
                case "AdditionalModifier":
                    NotifyPropertyChanged("TotalInitiative");
                    break;
                case "AbilityModifier":
                case "CommandSkill":
                    NotifyPropertyChanged("TotalCommandRange");
                    break;
            }
        }

        private void SetStartingStats(RaceSpecification race)
        {
            Physique = race.Physique.StartingValue;
            Speed = race.Speed.StartingValue;
            Strength = race.Strength.StartingValue;
            Agility = race.Agility.StartingValue;
            Prowess = race.Prowess.StartingValue;
            Poise = race.Poise.StartingValue;
            Intelligence = race.Intelligence.StartingValue;
            Arcane = race.Arcane.StartingValue;
            Perception = race.Perception.StartingValue;
        }

        private int GetMaxValue(Stat stat)
        {
            return stat.HeroLimit;
        }
    }
}
