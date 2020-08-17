using MediaManager;
using MusicPlayer.Model;
using MusicPlayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPlayer.View
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class PlayPage : ContentPage
   {
      public PlayPageViewModel viewModel;
      public PlayPage( Music music)
      {
         InitializeComponent();
         BindingContext = viewModel = new PlayPageViewModel( music );
      }

      protected async override void OnAppearing()
      {
         base.OnAppearing();         
         await ((PlayPageViewModel)BindingContext).PlayMusic();
      }

      private void MySlider_ValueChanged(object sender, ValueChangedEventArgs e)
      {
         if (Math.Abs(e.NewValue - e.OldValue) >= 2)
         {
            CrossMediaManager.Current.SeekTo(TimeSpan.FromSeconds(e.NewValue));
         }
      }
   }
}