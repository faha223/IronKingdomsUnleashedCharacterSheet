using IronKingdomsUnleashedCharacterSheet.ViewModels;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IronKingdomsUnleashedCharacterSheet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            GameData.Tables.Initialize();
            InitializeComponent();
            DataContext = CharacterSheetViewModel.Generate();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "Save Files (*.json)|*.json";
            var result = dlg.ShowDialog();
            if (result.HasValue && result.Value)
            {
                File.WriteAllText(dlg.FileName, JsonConvert.SerializeObject(DataContext, Formatting.Indented));
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Save Files (*.json)|*.json";
            var result = dlg.ShowDialog();
            if (result.HasValue && result.Value)
            {
                DataContext = JsonConvert.DeserializeObject<CharacterSheetViewModel>(File.ReadAllText(dlg.FileName));
            }
        }
    }
}
