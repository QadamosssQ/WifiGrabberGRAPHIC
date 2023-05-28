using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace testWpf
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            TitleLabel.MouseLeftButtonDown += TitleLabel_MouseLeftButtonDown;
            TitleLabel.MouseMove += TitleLabel_MouseMove;
            TitleLabel.MouseLeftButtonUp += TitleLabel_MouseLeftButtonUp;
        }

        private bool _isDragging = false;
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
                Label_WifiList.FontSize = 20;
                Label_Password.FontSize = 20;
                Label_Password.FontSize = 20;
                TextBox_Password.FontSize = 15;
            }
            else
            {
                WindowState = WindowState.Maximized;
                Label_WifiList.FontSize = 50;
                Label_Password.FontSize = 50;
                Label_Password.FontSize = 50;
                TextBox_Password.FontSize = 35;
            }
        }

        private void btn_hover_advanced(object sender, MouseEventArgs e)
        {
            Button_Advanced.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(70, 16, 118)); 
            
        }

        private void btn_unhover_advanced(object sender, MouseEventArgs e)
        {
            Button_Advanced.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
        }
        
        private void btn_hover_grab(object sender, MouseEventArgs e)
        {
            Button_Grab.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(70, 16, 118)); 
            
        }

        private void btn_unhover_grab(object sender, MouseEventArgs e)
        {
            Button_Grab.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
        }
    }
}
