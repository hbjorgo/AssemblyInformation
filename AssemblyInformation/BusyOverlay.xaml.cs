using System.Windows;
using System.Windows.Controls;

namespace AssemblyInformation
{
    /// <summary>
    /// Interaction logic for BusyOverlay.xaml
    /// </summary>
    public partial class BusyOverlay : UserControl
    {
        public BusyOverlay()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsBusy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(BusyOverlay), new PropertyMetadata(default(bool)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(BusyOverlay), new PropertyMetadata(default(string)));
    }
}
