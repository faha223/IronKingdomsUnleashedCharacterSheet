using IronKingdomsUnleashedCharacterSheet.UIHelpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System;

namespace IronKingdomsUnleashedCharacterSheet.CustomControls
{
    /// <summary>
    /// Interaction logic for MultiselectComboBox.xaml
    /// </summary>
    public partial class MultiselectComboBox : UserControl, INotifyPropertyChanged
    {
        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<object>), typeof(MultiselectComboBox), new PropertyMetadata(new List<object>(), itemsSourceChanged));

        private static void itemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mcb = d as MultiselectComboBox;
            if(mcb != null)
            {
                if(mcb.SelectedItems != null)
                {
                    List<object> selectedItems = new List<object>(mcb.SelectedItems);
                    mcb.SelectedItems = selectedItems.Where(c => mcb.ItemsSource.Contains(c));
                }
            }
        }

        public IEnumerable<object> SelectedItems
        {
            get { return (IEnumerable<object>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(IEnumerable<object>), typeof(MultiselectComboBox), new PropertyMetadata(null));

        public int MaxItemsSelected
        {
            get { return (int)GetValue(MaxItemsSelectedProperty); }
            set { SetValue(MaxItemsSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxItemsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxItemsSelectedProperty =
            DependencyProperty.Register("MaxItemsSelected", typeof(int), typeof(MultiselectComboBox), new PropertyMetadata(int.MaxValue));

        public MultiselectComboBox()
        {
            InitializeComponent();
        }

        #region Item Checked Command

        internal void CheckItem(object arg)
        {
            var cb = arg as CheckBox;
            if(cb != null)
            {
                var selectedItems = (SelectedItems == null) ? new List<object>() : new List<object>(SelectedItems);

                if (cb.IsChecked.Value && !selectedItems.Contains(cb.Content))
                    selectedItems.Add(cb.Content);
                else if (!cb.IsChecked.Value && selectedItems.Contains(cb.Content))
                    selectedItems.Remove(cb.Content);

                SelectedItems = selectedItems;
            }
        }

        private bool CanCheckItem(object arg)
        {
            var cb = arg as CheckBox;
            if (cb == null)
                return false;

            if (SelectedItems == null)
                return true;
            return SelectedItems.Contains(cb.Content) || (SelectedItems.Count() < MaxItemsSelected);
        }

        public ICommand CheckItemCommand { get { return new ParameteredCommandRouter<object>(CheckItem, CanCheckItem); } }

        #endregion Item Checked Command

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion INotifyPropertyChanged

    }
}
