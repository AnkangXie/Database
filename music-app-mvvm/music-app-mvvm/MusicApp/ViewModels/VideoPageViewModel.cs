using MusicApp.Common;
using MusicApp.Models;
using MusicApp.PageView;
using MusicApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace MusicApp.ViewModels
{
    class VideoPageViewModel
    {
        public static VideoPageViewModel This { get; set; }
        public VideoPageModel Model;

        public CommandBase EuropeJumpCommand { get; set; }

        public CommandBase LocalJumpCommand { get; set; }

        public CommandBase HongKongJumpCommand { get; set; }

        public CommandBase JapanJumpCommand { get; set; }

        public CommandBase KoreaJumpCommand { get; set; }
        public VideoPageViewModel()
        {
            This = this;
            Model = new VideoPageModel();

            EuropeJumpCommand = new CommandBase();
            EuropeJumpCommand.DoExecute= new Action<object>((o) => EuropeJump());
            EuropeJumpCommand.DoCanExecute= new Func<object, bool>((o) => { return true; });

            LocalJumpCommand = new CommandBase();
            LocalJumpCommand.DoExecute = new Action<object>((o) => LocalJump());
            LocalJumpCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            HongKongJumpCommand= new CommandBase();
            HongKongJumpCommand.DoExecute = new Action<object>((o) => HongKongJump());
            HongKongJumpCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            JapanJumpCommand=new CommandBase();
            JapanJumpCommand.DoExecute = new Action<object>((o) => JapanJump());
            JapanJumpCommand.DoCanExecute=new Func<object, bool>((o) => { return true; });

            KoreaJumpCommand=new CommandBase();
            KoreaJumpCommand.DoExecute= new Action<object>((o) => KoreaJump());
            KoreaJumpCommand.DoCanExecute= new Func<object, bool>((o) => { return true; });
        }

        public void GetInformation(string area = "港台", int limit = 30)
        {
            // 构造请求URL
            string url = $"{HttpUtil.serveUrl}/mv/all?area={area}&limit={limit}";

            // 发送HTTP请求
            string result = HttpUtil.HttpRequset(url);

            Model.VideoList = new List<Video>();

            JsonDocument jsonDocument = JsonDocument.Parse(result);
            JsonElement root = jsonDocument.RootElement;

            if (root.TryGetProperty("data", out JsonElement dataElement))
            {
                foreach (JsonElement item in dataElement.EnumerateArray())
                {
                    // 处理每一段JSON数据
                    string jsonSegment = item.GetRawText();

                    // 创建 Video 对象
                    Video videoInstance = new Video(jsonSegment);
                    Model.VideoList.Add(videoInstance);
                }
            }
        }

        public string getVideoURL(string id)
        {
            string url = $"{HttpUtil.serveUrl}/mv/url?id={id}&r=1080";
            string result = HttpUtil.HttpRequset(url);
            return ExtractUrlFromJson(result);
        }

        public void EuropeJump()
        {
            if(MainWindow.GetInstance().WindowState==WindowState.Normal)
            {
                VideoPageViewModel.This.GetInformation("欧美", 6);
            }
            else
            {
                VideoPageViewModel.This.GetInformation("欧美", 15);
            }
            
            VideoPage.GetInstance().mv_list.ItemsSource = Model.VideoList;
        }

        public void LocalJump()
        {
            if (MainWindow.GetInstance().WindowState == WindowState.Normal)
            {
                VideoPageViewModel.This.GetInformation("内地", 6);
            }
            else
            {
                VideoPageViewModel.This.GetInformation("内地", 15);
            }

            VideoPage.GetInstance().mv_list.ItemsSource = Model.VideoList;

        }

        public void HongKongJump()
        {
            if (MainWindow.GetInstance().WindowState == WindowState.Normal)
            {
                VideoPageViewModel.This.GetInformation("港台", 6);
            }
            else
            {
                VideoPageViewModel.This.GetInformation("港台", 15);
            }
            VideoPage.GetInstance().mv_list.ItemsSource = Model.VideoList;
            //SearchPage.This.mv_list.ItemsSource = Model.VideoList;
        }

        public void JapanJump()
        {
            if (MainWindow.GetInstance().WindowState == WindowState.Normal)
            {
                VideoPageViewModel.This.GetInformation("日本", 6);
            }
            else
            {
                VideoPageViewModel.This.GetInformation("日本", 15);
            }
            VideoPage.GetInstance().mv_list.ItemsSource = Model.VideoList;
        }

        public void KoreaJump()
        {
            if (MainWindow.GetInstance().WindowState == WindowState.Normal)
            {
                VideoPageViewModel.This.GetInformation("韩国", 6);
            }
            else
            {
                VideoPageViewModel.This.GetInformation("韩国", 15);
            }
            VideoPage.GetInstance().mv_list.ItemsSource = Model.VideoList;
        }
        static string ExtractUrlFromJson(string json)
        {
            try
            {
                JsonDocument jsonDocument = JsonDocument.Parse(json);
                JsonElement root = jsonDocument.RootElement;

                // 从 "data" 字段中获取 "url" 字段的值
                JsonElement urlElement = root.GetProperty("data").GetProperty("url");

                // 转换为字符串
                string url = urlElement.GetString();

                return url;
            }
            catch (JsonException)
            {
                // JSON 解析失败
                return null;
            }
        }

    }

}