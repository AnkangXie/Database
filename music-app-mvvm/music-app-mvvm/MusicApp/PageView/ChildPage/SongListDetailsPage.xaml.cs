﻿using MusicApp.ViewModels.PageView.ChildPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicApp.PageView.ChildPage
{
    /// <summary>
    /// 歌单详情 的交互逻辑
    /// </summary>
    public partial class SongListDetailsPage : Page
    {
        public SongListDetailsPage(string songsId)
        {
            InitializeComponent();
            var model = new SongListDetailsViewModel(songsId);

            DataContext = model;
        }
    }
}
