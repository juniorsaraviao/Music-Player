using MusicPlayer.Model;
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
   public partial class MusicPage : ContentPage
   {
      public MusicPage()
      {
         InitializeComponent();
      }

      protected async override void OnAppearing()
      {
         base.OnAppearing();
         await ((MusicPageViewModel)BindingContext).GetSongs();
      }
   }
}