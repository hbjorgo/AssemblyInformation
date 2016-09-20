using System.Collections.Generic;
using System.Windows;

namespace AssemblyInformation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WindowSettings windowSettings;
        private MainWindowViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            windowSettings = new WindowSettings();
            Width = windowSettings.Width;
            Height = windowSettings.Height;
            Top = windowSettings.Top;
            Left = windowSettings.Left;
            WindowState = windowSettings.State;

            viewModel = new MainWindowViewModel(new FileOpenDialog(true, "Dynamic Link Libraries (*.dll)|*.dll|Executables (*.exe)|*.exe"));
            DataContext = viewModel;
        }

        private void DataGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                IEnumerable<string> files = (string[])e.Data.GetData(DataFormats.FileDrop);
                viewModel.OpenFiles(files);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            windowSettings.Width = Width;
            windowSettings.Height = Height;
            windowSettings.Top = Top;
            windowSettings.Left = Left;
            windowSettings.State = WindowState;
            windowSettings.SaveSettings();
        }
    }
}
