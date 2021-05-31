using Acr.UserDialogs;
using MusicPlayer.Model;
using MusicPlayer.Service;
using MusicPlayer.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicPlayer.ViewModel
{
   public class FavoritePageViewModel : BaseViewModel
   {
      private IList<Music> _favoritePlaylist;
      private bool         _isVisibleList;
      private string       _favoriteMusicTitle;
      private int          _height;
      public bool IsNotVisibleList => !IsVisibleList;

      public IList<Music> FavoritePlaylist
      {
         get => _favoritePlaylist;
         set
         {
            _favoritePlaylist = value;
            OnPropertyChanged();
         }
      }      
      public bool IsVisibleList
      {
         get => _isVisibleList;
         set
         {
            _isVisibleList = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsNotVisibleList));
         }
      }
      public string FavoriteMusicTitle
      {
         get => _favoriteMusicTitle;
         set
         {
            _favoriteMusicTitle = value;
            OnPropertyChanged();
         }
      }
      public int Height
      {
         get => _height;
         set
         {
            _height = value;
            OnPropertyChanged();
         }
      }

      public ICommand PlayCommand => new Command<Music>( PlayMethod );

      private async void PlayMethod( Music selectedMusic )
      {
         await Application.Current.MainPage.Navigation.PushModalAsync( new PlayPage( selectedMusic ) );
      }

      public FavoritePageViewModel()
      {
         FavoritePlaylist = new ObservableCollection<Music>();
      }
      
      public async Task GetFavoritePlaylist()
      {
         using (UserDialogs.Instance.Loading())
         {
            var dataRetrieved = await MusicService.GetAllSongs();
            var likeMusic     = dataRetrieved.Where( x => x.IsLike ).ToList(); 
            if (likeMusic.Any())
            {
               
               FavoritePlaylist   = likeMusic;
               IsVisibleList      = true;
               Height             = likeMusic.Count * 80;
               FavoriteMusicTitle = "Enjoy your music!";
            }
            else
            {
               IsVisibleList = false;
            }
            
         }            
      }
   }
}
