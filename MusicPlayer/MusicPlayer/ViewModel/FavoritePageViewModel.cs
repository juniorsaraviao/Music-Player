using Acr.UserDialogs;
using MusicPlayer.Model;
using MusicPlayer.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MusicPlayer.ViewModel
{
   public class FavoritePageViewModel : BaseViewModel
   {
      private RadialGradientBrush         _radialGradient;
      private ObservableCollection<Music> _favoritePlaylist;
      private bool                        _isVisibleList;
      private string                      _favoriteMusicTitle;
      private int                         _height;
      public bool IsNotVisibleList => !IsVisibleList;

      public ObservableCollection<Music> FavoritePlaylist
      {
         get => _favoritePlaylist;
         set
         {
            _favoritePlaylist = value;
            OnPropertyChanged();
         }
      }
      public RadialGradientBrush RadialGradient
      {
         get => _radialGradient;
         set
         {
            _radialGradient = value;
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

      public async Task GetRadialBackground()
      {
         await Task.Delay(1000);
         var radialGradient = new RadialGradientBrush()
         {
            Center = new Point(0.0, 0.0),
            Radius = 0.5d,
            GradientStops = new GradientStopCollection()
            {
               new GradientStop()
               {
                  Color = Color.DarkGreen,
                  Offset = 0.1f
               },
               new GradientStop()
               {
                  Color = Color.Black,
                  Offset = 1.0f
               }
            }
         };
         RadialGradient = radialGradient;
         FavoriteMusicTitle = "Favorite Music";
         //await GetFavoritePlaylist();         
      }
      public async Task GetFavoritePlaylist()
      {
         using (UserDialogs.Instance.Loading())
         {
            await Task.Delay(500);
            if (string.IsNullOrEmpty(Preferences.Get("dataUpdated", null)))
            {
               IsVisibleList = false;
            }
            else
            {
               var dataRetrieved = JsonConvert.DeserializeObject<List<Music>>(Preferences.Get("dataUpdated", null))
                                    .Where(x => x.IsLike);
               if (dataRetrieved.Any())
               {
                  var collection   = new ObservableCollection<Music>();
                  dataRetrieved.ForEach(x => collection.Add(x));
                  FavoritePlaylist = collection;
                  IsVisibleList    = true;
                  Height           = collection.Count * 80;
               }
               else
               {
                  IsVisibleList = false;
               }
            }
         }            
      }
   }
}
