using System.Windows;
using System.Windows.Input;
using System;
using System.Diagnostics;

namespace WifiGrabberGRAPHIC
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadDetectedNetworks();

            TitleLabel.MouseLeftButtonDown += TitleLabel_MouseLeftButtonDown;
            TitleLabel.MouseMove += TitleLabel_MouseMove;
            TitleLabel.MouseLeftButtonUp += TitleLabel_MouseLeftButtonUp;
        }

        private bool _isDragging;
        private double _offsetX, _offsetY;

        private void TitleLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isDragging = true;

            _offsetX = e.GetPosition(this).X;
            _offsetY = e.GetPosition(this).Y;
        }

        private void TitleLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                double newX = e.GetPosition(this).X - _offsetX + Left;
                double newY = e.GetPosition(this).Y - _offsetY + Top;

                Left = newX;
                Top = newY;
            }
        }

        private void TitleLabel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void FullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                LabelWifiList.FontSize = 20;
                LabelPassword.FontSize = 20;
                LabelPassword.FontSize = 20;
                TextBoxPassword.FontSize = 15;
            }
            else
            {
                WindowState = WindowState.Maximized;
                LabelWifiList.FontSize = 50;
                LabelPassword.FontSize = 50;
                LabelPassword.FontSize = 50;
                TextBoxPassword.FontSize = 35;
            }
        }

        private void HoverAdvanced(object sender, MouseEventArgs e)
        {
            ButtonAdvanced.Foreground =
                new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(70, 16, 118));
        }

        private void BtnLandAdvanced(object sender, MouseEventArgs e)
        {
            ButtonAdvanced.Foreground =
                new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
        }

        private void BtnHoverGrab(object sender, MouseEventArgs e)
        {
            ButtonGrab.Foreground =
                new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(70, 16, 118));
        }

        private void BtnLandGrab(object sender, MouseEventArgs e)
        {
            ButtonGrab.Foreground =
                new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
        }

        private void Button_Grab_OnClick(object sender, RoutedEventArgs e)
        {
            string selectedNetwork = (string)NetworkComboBox.SelectedItem;
            if (selectedNetwork != null)
            {
                TextBoxPassword.Text = Show_wifi_password(selectedNetwork);
            }
        }
        
        private void ShowAdvanced(object sender, RoutedEventArgs e)
        {
            string selectedNetwork = (string)NetworkComboBox.SelectedItem;

            if (selectedNetwork != null)
            {
                MessageBox.Show(show_wifi(selectedNetwork), "Advanced show");
                GridPageMain.Visibility = Visibility.Hidden;
            }
        }

        private void LoadDetectedNetworks()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "netsh",
                Arguments = "wlan show profile",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            int a = 0;
            string[] networks = new string[lines.Length];

            foreach (string line in lines)
            {
                if (line.Contains("All User Profile"))
                {
                    a++;
                    string line1 = line.Replace("All User Profile     : ", "");
                    string line2 = line1.Substring(4);
                    networks[a] = line2;
                }
            }

            if (a > 0)
            {
                for (int i = 1; i <= a; i++)
                {
                    NetworkComboBox.Items.Add(networks[i]);
                }
            }
        }

        static string Show_wifi_password(string wifiName)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "netsh",
                Arguments = "wlan show profile name=\"" + wifiName + "\" key=clear",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            int a = 0;
            string[] network = new string[lines.Length];

            foreach (string line in lines)
            {
                if (line.Contains("    Key Content            : "))
                {
                    a++;
                    string line1 = line.Replace("    Key Content            : ", "");
                    network[0] = line1;
                }
            }

            return network[0];
        }

        static string show_wifi(string wifiName)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "netsh",
                Arguments = "wlan show profile name=\"" + wifiName + "\" key=clear",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return output;
        }
    }
}
