using System.Windows;
using System.Windows.Controls;

namespace AssemblyInformation
{
    /// <summary>
    /// Interaction logic for ProgressOverlay.xaml
    /// </summary>
    public partial class ProgressOverlay : UserControl
    {
        public ProgressOverlay()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(ProgressOverlay), new PropertyMetadata(null));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ProgressOverlay), new PropertyMetadata(default(string)));
    }
}
