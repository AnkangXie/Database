using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.ViewModels
{
    public class FavouriteViewModel
    {
        private static FavouriteViewModel favouriteViewModel= new FavouriteViewModel();

        public static FavouriteViewModel GetInstance()
        {
            return favouriteViewModel;
        }
    }
}
