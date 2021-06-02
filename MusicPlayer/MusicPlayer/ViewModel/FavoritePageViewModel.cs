using Autofac;
using MusicPlayer.Constant;
using MusicPlayer.Model;
using MusicPlayer.Service.Interfaces;
using MusicPlayer.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicPlayer.ViewModel
{
   public class FavoritePageViewModel : BaseViewModel
   {

      #region Fields

      private readonly IMusicService  _musicService;
      private readonly IDialogService _dialogService;
      private          IList<Music>   _favoritePlaylist;
      private          bool           _isVisibleList;
      private          string         _favoriteMusicTitle;
      private          int            _height;      

      #endregion

      #region Properties

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

      public bool IsNotVisibleList => !IsVisibleList;

      public string ErrorMessage { get; set; }

      #endregion

      #region Command

      public ICommand PlayCommand => new Command<Music>( PlayMethod );

      #endregion

      #region Constructor

      public FavoritePageViewModel() : this( 
         DIServiceContainer.Container.Resolve<IMusicService>(), 
         DIServiceContainer.Container.Resolve<IDialogService>()
      ) 
      {

      }

      public FavoritePageViewModel( 
         IMusicService  musicService,
         IDialogService dialogService
      )
      {
         _musicService    = musicService;
         _dialogService   = dialogService;
         FavoritePlaylist = new List<Music>();
      }

      #endregion

      #region Method

      private async void PlayMethod( Music selectedMusic )
      {
         await Application.Current.MainPage.Navigation.PushModalAsync( new PlayPage( selectedMusic ) );
      }      
      
      public async Task GetFavoritePlaylist()
      {
         try
         {
            using ( _dialogService.Dialog() )
            {
               var dataRetrieved = await _musicService.GetAllSongs();
               var likeMusic     = dataRetrieved.Where( x => x.IsLike ).ToList(); 
               if (likeMusic.Any())
               {
                  FavoritePlaylist   = likeMusic;
                  IsVisibleList      = true;
                  FavoriteMusicTitle = Constants.EnjoyMusicTitle;
               }
               else
               {
                  IsVisibleList = false;
               }

            }
         }
         catch (Exception ex)
         {
            ErrorMessage = ex.Message;
            _dialogService.Alert(Constants.GetFavoriteMusicError, Constants.GetFavoriteMusicTitle, Constants.OkText);
         }
                     
      }

      #endregion
      
   }
}
