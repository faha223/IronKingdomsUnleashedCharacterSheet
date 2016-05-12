using IronKingdomsUnleashedCharacterSheet.ViewModels;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System.ComponentModel;

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
            Closing += onClosing;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "Save Files (*.json)|*.json";
            var result = dlg.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var content = JsonConvert.SerializeObject(DataContext, Formatting.Indented);
                File.WriteAllText(dlg.FileName, content);
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Save Files (*.json)|*.json";
            var result = dlg.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var content = File.ReadAllText(dlg.FileName);
                DataContext = JsonConvert.DeserializeObject<CharacterSheetViewModel>(content);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void onClosing(object sender, CancelEventArgs e)
        {
            var result = MessageBox.Show("Save Changes?", "Exiting", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Save_Click(sender, null);
                    break;
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }
    }
}
