using System.Windows;
using System.Windows.Controls;

namespace IronKingdomsUnleashedCharacterSheet.CustomControls
{
    /// <summary>
    /// Interaction logic for StatBox.xaml
    /// </summary>
    public partial class StatBox : UserControl
    {
        #region Data Dependency Properties
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(StatBox), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(StatBox), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion

        #region Style Dependency Properties
        public int HeaderFontSize
        {
            get { return (int)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register("HeaderFontSize", typeof(int), typeof(StatBox), new PropertyMetadata(12));

        public int DataFontSize
        {
            get { return (int)GetValue(DataFontSizeProperty); }
            set { SetValue(DataFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataFontSizeProperty =
            DependencyProperty.Register("DataFontSize", typeof(int), typeof(StatBox), new PropertyMetadata(30));



        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(StatBox), new PropertyMetadata(false));


        #endregion

        public StatBox()
        {
            InitializeComponent();
        }
    }
}

