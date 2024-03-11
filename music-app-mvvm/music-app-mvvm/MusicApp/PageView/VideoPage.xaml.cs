using MusicApp.Models;
using MusicApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicApp.PageView
{
    /// <summary>
    /// VideoPage.xaml 的交互逻辑
    /// </summary>
    public partial class VideoPage : Page
    {
        public static VideoPage videoPage;

        public static VideoPage GetInstance()
        {
            return videoPage;
            
        }
        public VideoPage()
        {
            videoPage = this;
            var model = new VideoPageViewModel();
            InitializeComponent();
            DataContext= model;
        }

        private void Image_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                StackPanel stackPanel = button.Content as StackPanel;
                if (stackPanel != null)
                {
                    TextBlock idTextBlock = stackPanel.FindName("Bridge") as TextBlock;
                    if (idTextBlock != null)
                    {
                        string id = idTextBlock.Text; // 获取ID值
                        try
                        {
                            string url = VideoPageViewModel.This.getVideoURL(id);
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = url,
                                UseShellExecute = true
                            }) ;
                        }
                        catch
                        {
                            
                        }
                    }
                }
            }
        }

        
    }
}
