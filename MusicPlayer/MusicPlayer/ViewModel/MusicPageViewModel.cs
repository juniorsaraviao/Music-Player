using Acr.UserDialogs;
using MusicPlayer.Constant;
using MusicPlayer.Service;
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

      private string          _greeting;
      private IList<Playlist> _myPlaylist;
      private IList<Music>    _favoriteMusicList;
      private string          _recentlyPlayedTitle;
      private string          _topMusicTitle;

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

      #endregion

      #region Commands

      public ICommand LikeCommand => new Command<Music>(async (music) => await LikeMethod(music));

      #endregion

      #region Constructor

      public MusicPageViewModel()
      {
      }

      #endregion

      #region Methods

      private void DefineGreeting()
      {
         if( DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 12 )
         {
            Greeting = Constants.GoodMorning;
         }
         else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18 )
         {
            Greeting = Constants.GoodAfternoon;
         }
         else
         {
            Greeting = Constants.GoodEvening;
         }
      }      

      private async Task LikeMethod(Music selectedMusic)
      {
         using (UserDialogs.Instance.Loading())
         {
            selectedMusic.IsLike  = !selectedMusic.IsLike;
            var isUpdated         = await MusicService.UpdateSong(selectedMusic);

            FavoriteMusicList.Clear();
            FavoriteMusicList = await MusicService.GetAllSongs();

            if (selectedMusic.IsLike && isUpdated)
            {
               UserDialogs.Instance.Toast(Constants.IncludeMusicMessage, new TimeSpan(500));
            }
            else
            {
               UserDialogs.Instance.Toast(Constants.RemoveMusicMessage, new TimeSpan(500));
            }

            MessagingCenter.Send(this, Constants.MessagingCenterReload);
         }                  
      }

      public async Task GetSongs()
      {
         try
         {
            using (UserDialogs.Instance.Loading())
            {               
               DefineGreeting();
               MyPlaylist          = await MusicService.GetPlayLists();
               FavoriteMusicList   = await MusicService.GetAllSongs();              
               RecentlyPlayedTitle = Constants.RecentlyPlayedTitle;
               TopMusicTitle       = Constants.TopMusicTitle;               
            }            
         }
         catch (Exception)
         {
            throw;
         }
      }

      #endregion          
      
   }
}
