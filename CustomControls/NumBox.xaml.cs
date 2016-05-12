using System.Windows;
using System.Windows.Controls;

namespace IronKingdomsUnleashedCharacterSheet.CustomControls
{
    /// <summary>
    /// Interaction logic for NumBox.xaml
    /// </summary>
    public partial class NumBox : UserControl
    {
        #region Data Dependency Properties
        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(NumBox), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion

        #region Style Dependency Properties
        public int DataFontSize
        {
            get { return (int)GetValue(DataFontSizeProperty); }
            set { SetValue(DataFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataFontSizeProperty =
            DependencyProperty.Register("DataFontSize", typeof(int), typeof(NumBox), new PropertyMetadata(30));

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(NumBox), new PropertyMetadata(false));
        #endregion

        public NumBox()
        {
            InitializeComponent();
        }
    }
}
