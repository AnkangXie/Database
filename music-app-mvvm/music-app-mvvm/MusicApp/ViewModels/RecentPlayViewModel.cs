using MusicApp.Common;
using MusicApp.Models;
using MusicApp.PageView;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.ViewModels
{
    public class RecentPlayViewModel
    {
        public static RecentPlayViewModel recentPlayViewModel { get; set; }

        public static RecentPlayViewModel GetInstance()
        {
            return recentPlayViewModel;
        }

        public RecentPlayModel Model;


        public CommandBase ShowButCommand { get; set; }

        public CommandBase FreshButCommand { get; set; }

        public CommandBase CleanButCommand { get;set; }
        public RecentPlayViewModel()
        {
            Model = new RecentPlayModel();
            ShowButCommand = new CommandBase();
            ShowButCommand.DoExecute = new Action<object>((o) => Show());
            ShowButCommand.DoCanExecute= new Func<object, bool>((o) => { return true; });
            
            FreshButCommand = new CommandBase();
            FreshButCommand.DoExecute = new Action<object>((o) => Fresh());
            FreshButCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            CleanButCommand = new CommandBase();
            CleanButCommand.DoExecute = new Action<object>((o) => Clean());
            CleanButCommand.DoCanExecute=new Func<object, bool>((o) => { return true; });

        }


        public void Show()
        {
            string sql = $"SELECT SongName,Singer,PlayTime FROM history_play where ID={MainWindowViewModel.GetInstance().Model.login_id} ORDER BY PlayTime DESC;";
            MySqlConnection conn = new MySqlConnection("Server=mysql.sqlpub.com;port=3306;Database=musicplayerdb;user=xieakk;Pwd=72f66c3d257c6525");
            conn.Open();
            MySqlCommand cmd1 = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                Simple_SongModel song = new Simple_SongModel
                {
                    SongName = reader.GetString(0),
                    Singer = reader.GetString(1),
                    PlayTime = reader.GetDateTime(2).ToShortDateString(),
                };
                Model.PlayList.Add(song);
            }
            Model.Count = Model.PlayList.Count();
            PageManager.recentPlayPage.RecentPlayBox.ItemsSource = Model.PlayList;
            PageManager.recentPlayPage.Count.Text = Model.Count.ToString();
            PageManager.recentPlayPage.Display.Visibility = System.Windows.Visibility.Hidden;
        }

        public void Fresh()
        {
            Clean();
            Show();
        }

        public void Clean()
        {
            Model.PlayList.Clear();
            PageManager.recentPlayPage.Count.Text = "0";
            PageManager.recentPlayPage.RecentPlayBox.ItemsSource = null;
        }
    }
}
