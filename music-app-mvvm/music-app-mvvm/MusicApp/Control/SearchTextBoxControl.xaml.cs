using Microsoft.VisualBasic.Devices;
using MusicApp.Common;
using MusicApp.Models;
using MusicApp.Models.Vo;
using MusicApp.PageView;
using MusicApp.ViewModels;
using MusicApp.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicApp.Control
{
    /// <summary>
    /// 搜索框 的交互逻辑
    /// </summary>
    public partial class SearchTextBoxControl : UserControl
    {

        public SearchTextBoxControl()
        {
            InitializeComponent();

            var model = SearchTextBoxViewModel.GetInstance();

            DataContext = model;
        }

     
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.GetInstance().Model.MenusChecked = MenusChecked.SearchPage;
            string keyword = this.SearchBox.Text.ToString();
            if(string.IsNullOrEmpty(keyword) ) { return; }
            string result = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/search?keywords=" + keyword);
            SearchDataResultModel data = JsonConvert.DeserializeObject<SearchDataResultModel>(result);

            SearchListViewModel.This.Model.SingleList = data.result.songs;

            string mvResult = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/search?keywords=" + keyword + "&limit=6&type=1004");

            SearchListViewModel.This.Model.Videos = new List<Video>();

            try
            {
                JObject json = JObject.Parse(mvResult);
                JArray mvs = (JArray)json["result"]["mvs"];

                // 使用 Dispatcher.Invoke 确保在 UI 线程上更新 UI
                SearchPage.This.Dispatcher.Invoke(() =>
                {
                    foreach (var mv in mvs)
                    {
                        SearchListViewModel.This.Model.Videos.Add(new Video(mv.ToString()));
                    }
                    SearchPage.This.mv_list.ItemsSource = SearchListViewModel.This.Model.Videos;
                });
            }
            catch (Exception ex)
            {
                // 在实际应用中，请记录异常或采取适当的措施
                Console.WriteLine("Exception during MV data processing: " + ex.Message);
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
