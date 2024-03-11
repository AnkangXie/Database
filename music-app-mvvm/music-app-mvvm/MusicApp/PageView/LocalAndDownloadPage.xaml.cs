using System;
using System.Collections.Generic;
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
using MusicApp.ViewModels;
namespace MusicApp.PageView
{
    /// <summary>
    /// LocalAndDownloadPage.xaml 的交互逻辑
    /// </summary>
    public partial class LocalAndDownloadPage : Page
    {
        public static LocalAndDownloadPage localAndDownloadPage;
        public LocalAndDownloadPage()
        {
            localAndDownloadPage = this;
            var model = new LocalAndDownloadViewModel();
            InitializeComponent();
            DataContext = model;
        }
    }
}
