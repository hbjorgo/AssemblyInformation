using System.Windows;

namespace AssemblyInformation
{
    public class WindowSettings
    {
        public WindowSettings()
        {
            LoadSettings();
            SizeToFit();
            MoveIntoView();
        }

        public double Height { get; set; }
        public double Width { get; set; }
        public double Top { get; set; }
        public double Left { get; set; }
        public WindowState State { get; set; }

        private void LoadSettings()
        {
            if (Properties.Settings.Default.WindowTop > 0)
                Top = Properties.Settings.Default.WindowTop;
            if (Properties.Settings.Default.WindowLeft > 0)
                Left = Properties.Settings.Default.WindowLeft;
            if (Properties.Settings.Default.WindowHeight > 0)
                Height = Properties.Settings.Default.WindowHeight;
            if (Properties.Settings.Default.WindowWidth > 0)
                Width = Properties.Settings.Default.WindowWidth;
            State = Properties.Settings.Default.WindowState;
        }

        public void SaveSettings()
        {
            if (State != WindowState.Minimized)
            {
                if (Top > 0)
                    Properties.Settings.Default.WindowTop = Top;
                if (Left > 0)
                    Properties.Settings.Default.WindowLeft = Left;
                if (Height > 0)
                    Properties.Settings.Default.WindowHeight = Height;
                if (Width > 0)
                    Properties.Settings.Default.WindowWidth = Width;
                Properties.Settings.Default.WindowState = State;

                Properties.Settings.Default.Save();
            }
        }

        private void SizeToFit()
        {
            if (Height > SystemParameters.VirtualScreenHeight)
            {
                Height = SystemParameters.VirtualScreenHeight;
            }

            if (Width > SystemParameters.VirtualScreenWidth)
            {
                Width = SystemParameters.VirtualScreenWidth;
            }
        }

        private void MoveIntoView()
        {
            if (Top + Height / 2 > SystemParameters.VirtualScreenHeight)
                Top = SystemParameters.VirtualScreenHeight - Height;

            if (Left + Width / 2 > SystemParameters.VirtualScreenWidth)
                Left = SystemParameters.VirtualScreenWidth - Width;

            if (Top < 0)
                Top = 0;

            if (Left < 0)
                Left = 0;
        }
    }
}
