using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicApp.ViewModels;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MusicApp.Views
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class lyrics : Window
    {
        int isLocked = 0;
        string clr = "#ef6d32";
        public lyrics()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

        }

        string path = "D:\\cache.txt";
        private void timer_Tick(object sender, EventArgs e)
        {
            string str = File.ReadAllText(path);
            lbl.Content = str;
        }
        private void lock_Click(object sender, RoutedEventArgs e)
        {
            if (isLocked == 0)
            {
                isLocked = 1;
                Button button = (Button)sender;
                button.Background = new SolidColorBrush(Color.FromArgb(255, 239, 109, 50));
                button.Foreground = Brushes.White;
                plus.Visibility = Visibility.Hidden;
                minus.Visibility = Visibility.Hidden;
            }
            else
            {
                isLocked = 0;
                Button button = (Button)sender;
                button.Background = Brushes.Transparent;
                button.Foreground = new SolidColorBrush(Color.FromArgb(255, 239, 109, 50));
                plus.Visibility = Visibility.Visible;
                minus.Visibility = Visibility.Visible;
            }
        }

        void Move_MouseMove(object sender, MouseEventArgs e)
        {
            if (isLocked == 1)
            {
                return;
            }
            else
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
                else
                {
                    return;
                }
            }
        }

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            if (lbl.FontSize >= 6 && lbl.FontSize <= 48)
            {
                lbl.FontSize += 2;
            }
            else if (lbl.FontSize <= 6)
            {
                lbl.FontSize = 6;
            }
            else
            {
                lbl.FontSize = 50;
            }
        }

        private void minus_Click(object sender, RoutedEventArgs e)
        {
            if (lbl.FontSize >= 8 && lbl.FontSize <= 50)
            {
                lbl.FontSize -= 2;
            }
            else if (lbl.FontSize <= 6)
            {
                lbl.FontSize = 6;
            }
            else
            {
                lbl.FontSize = 50;
            }
        }
    }
}
