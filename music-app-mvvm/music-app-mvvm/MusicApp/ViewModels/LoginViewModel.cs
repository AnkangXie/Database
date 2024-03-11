using MusicApp.Common;
using MusicApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using MusicApp.Models;
using MySqlConnector;
using static MusicApp.Models.LoginModel;
using Microsoft.Xaml.Behaviors.Core;

namespace MusicApp.ViewModels
{
    public class LoginViewModel
    {
        private static LoginViewModel loginViewModel = new LoginViewModel();
        public static LoginViewModel GetInstance()   //静态方法 直接用类名访问

        {
            return loginViewModel;
        }
        //事件委托
        public Action<object> BaseBorderMouseDownDelegate { get; set; }

        public LoginModel Model { get; set; }
        public LoginUI LoginUI { get; set; }

        public MainWindowViewModel MainWindowViewModel { get; set; }

        //点击时触发特效
        public CommandBase Border_Mousedown_Command { get; set; }
        //退出登录界面
        public CommandBase Close_Mousedown_Command { get; set; }
        //关闭注册
        public CommandBase Change_Mousedown_Command { get; set; }
        public CommandBase Register_Mousedown_Command { get; set; }
        //点击图标
        public CommandBase Jump_Command { get; set; }

        public CommandBase Navigate_Mousedown_Command { get; set; }


        private LoginViewModel()  //构造函数一样

        {
            LoginUI = LoginUI.loginUI;
            Model = new LoginModel();
            LoginModel.userID = FetchCurrentMaxID();
            MainWindowViewModel=MainWindowViewModel.GetInstance();
            //初始化第一页

            //缩小
            Border_Mousedown_Command = new CommandBase();
            Border_Mousedown_Command.DoExecute = new Action<object>((o) => Border_Mousedown());
            Border_Mousedown_Command.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //最大化
            Close_Mousedown_Command = new CommandBase();
            Close_Mousedown_Command.DoExecute = new Action<object>((o) => Close_Mousedown());
            Close_Mousedown_Command.DoCanExecute = new Func<object, bool>((o) => { return true; });

            Change_Mousedown_Command = new CommandBase();
            Change_Mousedown_Command.DoExecute = new Action<object>((o) => Change_Mousedown());
            Change_Mousedown_Command.DoCanExecute = new Func<object, bool>((o) => { return true; });
            //关闭应用
            Register_Mousedown_Command = new CommandBase();
            Register_Mousedown_Command.DoExecute = new Action<object>((o) => Register_Mousedown());
            Register_Mousedown_Command.DoCanExecute = new Func<object, bool>((o) => { return true; });

            Jump_Command = new CommandBase();
            Jump_Command.DoExecute = new Action<object>((o) => Jump());
            Jump_Command.DoCanExecute = new Func<object, bool>((o) => { return true; });

            Navigate_Mousedown_Command = new CommandBase();
            Navigate_Mousedown_Command.DoExecute = new Action<object>((o) => Navigate());
            Navigate_Mousedown_Command.DoCanExecute = new Func<object, bool>((o) => { return true; });

        }


        public void Border_Mousedown()
        {
            LoginUI.DragMove();
        }

        public void Close_Mousedown()
        {
            System.Environment.Exit(0);
        }

        public void Change_Mousedown()
        {
            Model.rpt_visibility = Visibility.Visible;
        }

        public void Register_Mousedown()
        {
            try
            {
                LoginModel.userID++;
                string username = Model.username;
                string useremail = Model.useremail;
                string userpassword = LoginUI._PasswordBox.Password;
                string userrptpassword = LoginUI._Rpt_PasswordBox.Password;
                string head = $"SELECT Password FROM user WHERE UserName = '{Model.username}' OR Email = '{Model.useremail}';";
                //string ins = $"insert into user(ID,UserName,Email,Password) values ({LoginModel.userID} ,'{username}','{useremail}','{userpassword}')";
                MySqlConnection conn = new MySqlConnection("Server=mysql.sqlpub.com;port=3306;Database=musicplayerdb;user=xieakk;Pwd=72f66c3d257c6525");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(head, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    if (userpassword == userrptpassword)
                    {
                        string ins = $"insert into user(ID,UserName,Email,Password) values ({LoginModel.userID} ,'{username}','{useremail}','{userpassword}')";
                        MySqlConnection conn1 = new MySqlConnection("Server=mysql.sqlpub.com;port=3306;Database=musicplayerdb;user=xieakk;Pwd=72f66c3d257c6525");
                        conn1.Open();
                        MySqlCommand cmd1 = new MySqlCommand(ins, conn1);
                        cmd1.ExecuteNonQuery();
                        Model.info = Login_info.Content_Successfully_Register;
                        Model.interact_info = FetchLoginInfo();
                        LoginUI.Info_Block.Foreground = new SolidColorBrush(Colors.Green);        
                        Model.rpt_visibility = Visibility.Hidden;
                    }
                    else
                    {
                        Model.username = "";
                        Model.useremail = "";
                        LoginUI._PasswordBox.Password = "";
                        LoginUI._Rpt_PasswordBox.Password = "";
                        Model.info = Login_info.Content_Pas_Uneql_Rpt;
                        Model.interact_info = FetchLoginInfo();
                        LoginUI.Info_Block.Foreground = new SolidColorBrush(Colors.Red);
                    }
                }
                else
                {
                    Model.username = "";
                    Model.useremail = "";
                    LoginUI._PasswordBox.Password = "";
                    LoginUI._Rpt_PasswordBox.Password = "";
                    Model.info = Login_info.Content_Username_Exited;
                    Model.interact_info = FetchLoginInfo();
                    LoginUI.Info_Block.Foreground = new SolidColorBrush(Colors.Red);
                }

            }
            catch
            {
                Model.info = Login_info.Content_DB_Failure;
                Model.username = "";
                Model.useremail = "";
                LoginUI._PasswordBox.Password = "";
                LoginUI._Rpt_PasswordBox.Password = "";
                Model.interact_info = FetchLoginInfo();
                LoginUI.Info_Block.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        public void Jump()
        {
            try
            {
                string ins = $"SELECT Password FROM user WHERE UserName = '{Model.username}' AND Email = '{Model.useremail}';";
                MySqlConnection conn = new MySqlConnection("Server=mysql.sqlpub.com;port=3306;Database=musicplayerdb;user=xieakk;Pwd=72f66c3d257c6525");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(ins, conn);
                var ii = cmd.ExecuteScalar();
                if (ii == null)
                {
                    Model.info = Login_info.Content_Can_Not_Find_Target;
                    Model.interact_info = FetchLoginInfo();
                    LoginUI.Info_Block.Foreground = new SolidColorBrush(Colors.Red);
                    Model.username = "";
                    Model.useremail = "";
                    LoginUI._PasswordBox.Password = "";
                }
                else
                {
                    string fetch_password_from_db = ii.ToString();
                    if (fetch_password_from_db == LoginUI._PasswordBox.Password)
                    {
                        Model.canAdmit = true;
                        Model.info = Login_info.Content_Successfully_Login;
                        Model.interact_info = FetchLoginInfo();
                        LoginUI.Info_Block.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        Model.info = Login_info.Content_Password_Wrong;
                        LoginUI._PasswordBox.Password = "";
                        Model.interact_info = FetchLoginInfo();
                        LoginUI.Info_Block.Foreground = new SolidColorBrush(Colors.Red);
                    }
                }
                if (Model.canAdmit)
                {
                    Model.info = Login_info.Content_Successfully_Login;
                    Model.interact_info = FetchLoginInfo();
                    LoginUI.Info_Block.Foreground = new SolidColorBrush(Colors.Green);
                    MainWindowViewModel.Model.login_username = Model.username;
                    FetchCurrentID(Model.username);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    LoginUI.Close();
                }
            }
            catch
            {
                Model.info = Login_info.Content_DB_Failure;
                Model.username = "";
                Model.useremail = "";
                LoginUI._PasswordBox.Password = "";
                LoginUI._Rpt_PasswordBox.Password = "";
                Model.interact_info = FetchLoginInfo();
                LoginUI.Info_Block.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        public void Navigate()
        {

        }

        public long FetchCurrentMaxID()
        {
            try
            {
                string ins = "SELECT MAX(ID) AS MaxID FROM user;";
                MySqlConnection conn = new MySqlConnection("Server=mysql.sqlpub.com;port=3306;Database=musicplayerdb;user=xieakk;Pwd=72f66c3d257c6525");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(ins, conn);
                var ii = cmd.ExecuteScalar();
                long res = Convert.ToInt64(ii);
                return res;
            }
            catch
            {
                Model.info = Login_info.Content_DB_Failure;
                Model.username = "";
                Model.useremail = "";
                LoginUI._PasswordBox.Password = "";
                LoginUI._Rpt_PasswordBox.Password = "";
                Model.interact_info = FetchLoginInfo();
                LoginUI.Info_Block.Foreground = new SolidColorBrush(Colors.Red);
                return -1;
            }
        }
        public void FetchCurrentID(string username)
        {
            string ins = $"SELECT ID FROM user WHERE UserName='{username}';";
            MySqlConnection conn = new MySqlConnection("Server=mysql.sqlpub.com;port=3306;Database=musicplayerdb;user=xieakk;Pwd=72f66c3d257c6525");
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(ins, conn);
            var ii = cmd.ExecuteScalar();
            MainWindowViewModel.GetInstance().Model.login_id= Convert.ToInt32(ii);
        }
        public string FetchLoginInfo()
        {
            switch (Model.info)
            {
                case Login_info.Content_Pas_Uneql_Rpt:
                    return "密码输入两次不一样，请重新输入！";
                    break;
                case Login_info.Content_Username_Exited:
                    return "该用户名已经存在，请重新输入！";
                    break;
                case Login_info.Content_Email_Exited:
                    return "该邮箱已经存在，请重新输入！";
                    break;
                case Login_info.Content_Can_Not_Find_Target:
                    return "无法找到该注册用户！";
                    break;
                case Login_info.Content_Password_Wrong:
                    return "密码错误，请重新输入！";
                    break;
                case Login_info.Content_Successfully_Login:
                    return "恭喜你，登录成功！";
                    break;
                case Login_info.Content_Successfully_Register:
                    return "恭喜你，注册成功！";
                    break;
                case Login_info.Content_Already_Registered:
                    return "该账号已经注册！";
                    break;
                case Login_info.Content_DB_Failure:
                    return "数据库异常！";
                    break;
                default:
                    return "";
                    break;
            }
        }
    }
}
