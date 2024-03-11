using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Windows.Shapes;
using System.Windows.Shell;
using MusicApp.ViewModels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Window = System.Windows.Window;

namespace MusicApp.Views
{
    /// <summary>
    /// LoginUI.xaml 的交互逻辑
    /// </summary>
    public partial class LoginUI : Window
    {
        public static LoginUI loginUI;
        public LoginUI()
        {
            loginUI = this;
            InitializeComponent();
            var loginViewModel = LoginViewModel.GetInstance();
            this.DataContext = loginViewModel;
        }
        private void Border_Mousedown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void username_focus(object sender,RoutedEventArgs e)
        {
            if (_username.Text == "输入你的用户名")
            {
                _username.Text = "";
            }
        }
        private void username_lostfocus(object sender,RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_username.Text))
            {
                _username.Text = "输入你的用户名";
            }
        }
        private void email_focus(object sender,RoutedEventArgs e)
        {
            if (_useremail.Text == "输入你的邮箱")
            {
                _useremail.Text = "";
            }
        }

        private void email_lostfocus(Object sender,RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_useremail.Text))
            {
                _useremail.Text = "输入你的邮箱";
            }
        }

    }
}
