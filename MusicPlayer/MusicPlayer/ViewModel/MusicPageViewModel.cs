using Acr.UserDialogs;
using MusicPlayer.Service;
using MusicPlayer.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MusicPlayer.Model
{
   public class MusicPageViewModel : BaseViewModel
   {
      private string                         _greeting;
      private ObservableCollection<Playlist> _myPlaylist;
      private ObservableCollection<Music>    _favoriteMusicList;
      private string                         _recentlyPlayedTitle;
      private string                         _topMusicTitle;

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
      public ObservableCollection<Playlist> MyPlaylist
      {
         get => _myPlaylist;
         set 
         {
            _myPlaylist = value;
            OnPropertyChanged();
         }
      }
      public ObservableCollection<Music> FavoriteMusicList
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

      private void DefineGreeting()
      {
         if( DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 12 )
         {
            Greeting = "Good morning";
         }
         else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18 )
         {
            Greeting = "Good afternoon";
         }
         else
         {
            Greeting = "Good evening";
         }
      }

      public ICommand LikeCommand => new Command<Music>( LikeMethod );

      private void LikeMethod(Music selectedMusic)
      {
         var index = FavoriteMusicList.IndexOf(selectedMusic);
         selectedMusic.IsLike     = !selectedMusic.IsLike;
         FavoriteMusicList[index] = selectedMusic;
         var saveData             = JsonConvert.SerializeObject(FavoriteMusicList);
         Preferences.Set("dataUpdated", saveData);
         if ( selectedMusic.IsLike )
         {
            UserDialogs.Instance.Toast("Include in Favorite Music", new TimeSpan(500));
            MessagingCenter.Send(this, "Reload");
         }
         else
         {
            UserDialogs.Instance.Toast("Remove from Favorite Music", new TimeSpan(500));
         }         
      }

      public async Task GetSongs()
      {
         try
         {
            using (UserDialogs.Instance.Loading())
            {
               if ( string.IsNullOrEmpty(Preferences.Get("dataUpdated", null)) )
               {
                  var SongsData = new ObservableCollection<Music>();
                  var songs     = await MusicService.GetAllSongs(1, 10);
                  songs.ForEach(x =>
                  {
                     SongsData.Add(x);
                  });

                  var saveData = JsonConvert.SerializeObject(SongsData.ToList());
                  Preferences.Set("dataUpdated", saveData);

                  DefineGreeting();
                  MyPlaylist          = Playlist.PopularPlaylist();
                  RecentlyPlayedTitle = "Recently played";
                  TopMusicTitle       = "Top Music";
                  FavoriteMusicList   = SongsData;
               }
               else
               {
                  DefineGreeting();
                  MyPlaylist           = Playlist.PopularPlaylist();
                  RecentlyPlayedTitle  = "Recently played";
                  TopMusicTitle        = "Top Music";
                  var dataRetrieved    = JsonConvert.DeserializeObject<List<Music>>(Preferences.Get("dataUpdated", null));                  
                  var collection       = new ObservableCollection<Music>();
                  dataRetrieved.ForEach( x => collection.Add(x) );
                  FavoriteMusicList    = collection;                  
               }               
            }            
         }
         catch (Exception)
         {
            throw;
         }
      }      

      public MusicPageViewModel()
      {         
      }
   }
}
