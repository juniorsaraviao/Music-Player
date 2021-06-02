using MediaManager;
using MusicPlayer.Model;
using MusicPlayer.ViewModel;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPlayer.View
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class PlayPage : ContentPage
   {
      public readonly PlayPageViewModel _viewModel;
      public PlayPage( Music music)
      {
         InitializeComponent();
         _viewModel = new PlayPageViewModel( music );
      }

      protected async override void OnAppearing()
      {
         base.OnAppearing();
         BindingContext = _viewModel;
         await _viewModel.PlayMusic();
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