using Autofac;
using MusicPlayer.Constant;
using MusicPlayer.Service.Interfaces;
using MusicPlayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicPlayer.Model
{
   public class MusicPageViewModel : BaseViewModel
   {

      #region Fields

      private readonly IMusicService   _musicService;
      private readonly IDialogService  _dialogService;
      private          string          _greeting;
      private          IList<Playlist> _myPlaylist;
      private          IList<Music>    _favoriteMusicList;
      private          string          _recentlyPlayedTitle;
      private          string          _topMusicTitle;

      #endregion

      #region Properties

      public string RecentlyPlayedTitle
      {
         get => _recentlyPlayedTitle;
         set 
         {
            _recentlyPlayedTitle = value;
            OnPropertyChanged();
         }
      }
      public string TopMusicTitle
      {
         get => _topMusicTitle;
         set
         {
            _topMusicTitle = value;
            OnPropertyChanged();
         }
      }
      public IList<Playlist> MyPlaylist
      {
         get => _myPlaylist;
         set 
         {
            _myPlaylist = value;
            OnPropertyChanged();
         }
      }
      public IList<Music> FavoriteMusicList
      {
         get => _favoriteMusicList;
         set
         {
            _favoriteMusicList = value;
            OnPropertyChanged();
         }
      }
      public string Greeting
      {
         get => _greeting;
         set 
         { 
            _greeting = value;
            OnPropertyChanged();
         }
      }

      public string ErrorMessage  { get; set; }
      public bool   IsUpdated     { get; set; }
      public DateTime CurrentDate { get; set; }

      #endregion

      #region Commands

      public ICommand LikeCommand => new Command<Music>(async (music) => await LikeMethod(music));

      #endregion

      #region Constructor

      public MusicPageViewModel() : this(
         DIServiceContainer.Container.Resolve<IMusicService>(),
         DIServiceContainer.Container.Resolve<IDialogService>()
      )
      {
      }

      public MusicPageViewModel(
         IMusicService  musicService,
         IDialogService dialogService
      )
      {
         _musicService     = musicService;
         _dialogService    = dialogService;
         FavoriteMusicList = new List<Music>();
         CurrentDate       = DateTime.Now;
      }

      #endregion

      #region Methods

      public void DefineGreeting()
      {
         if( CurrentDate.Hour >= 6 && CurrentDate.Hour < 12 )
         {
            Greeting = Constants.GoodMorning;
         }
         else if ( CurrentDate.Hour >= 12 && CurrentDate.Hour < 18 )
         {
            Greeting = Constants.GoodAfternoon;
         }
         else
         {
            Greeting = Constants.GoodEvening;
         }
      }      

      public async Task LikeMethod(Music selectedMusic)
      {
         try
         {
            using ( _dialogService.Dialog() )
            {
               selectedMusic.IsLike  = !selectedMusic.IsLike;
               var isUpdated         = await _musicService.UpdateSong(selectedMusic);

               FavoriteMusicList.Clear();
               FavoriteMusicList = await _musicService.GetAllSongs();

               if (selectedMusic.IsLike && isUpdated)
               {
                  IsUpdated = true;
                  _dialogService.Toast(Constants.IncludeMusicMessage, new TimeSpan(500));
               }
               else
               {
                  IsUpdated = false;
                  _dialogService.Toast(Constants.RemoveMusicMessage, new TimeSpan(500));
               }

               MessagingCenter.Send(this, Constants.MessagingCenterReload);
            }
         }
         catch (Exception ex)
         {
            ErrorMessage = ex.Message;
            _dialogService.Alert(Constants.LikeErrorMessage, Constants.LikeErrorTitle, Constants.OkText);
         }
                           
      }

      public async Task GetSongs()
      {
         try
         {
            using ( _dialogService.Dialog() )
            {               
               DefineGreeting();
               MyPlaylist          = await _musicService.GetPlayLists();
               FavoriteMusicList   = await _musicService.GetAllSongs();              
               RecentlyPlayedTitle = Constants.RecentlyPlayedTitle;
               TopMusicTitle       = Constants.TopMusicTitle;               
            }            
         }
         catch (Exception ex)
         {
            ErrorMessage = ex.Message;
            _dialogService.Alert(Constants.GetSongError, Constants.GetSongTitle, Constants.OkText);
         }
      }

      #endregion          
      
   }
}
