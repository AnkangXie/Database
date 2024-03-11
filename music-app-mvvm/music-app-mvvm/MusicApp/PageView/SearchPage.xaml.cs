using MusicApp.ViewModels;
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

namespace MusicApp.PageView
{
    /// <summary>
    /// SearchPage.xaml 的交互逻辑
    /// </summary>
    public partial class SearchPage : Page
    {
        public static SearchPage This;
        public SearchPage()
        {
            This = this;
            InitializeComponent();
            var model =new SearchListViewModel();
            DataContext = model;
            mv_list.ItemsSource = model.Model.Videos;
            //model.Model.SingleList;
        }
    }
}
