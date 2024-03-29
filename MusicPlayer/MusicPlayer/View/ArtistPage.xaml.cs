﻿using MusicPlayer.ViewModel;
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
   public partial class ArtistPage : ContentPage
   {
      public ArtistPage()
      {
         InitializeComponent();
      }

      protected override async void OnAppearing()
      {
         base.OnAppearing();
         await ((ArtistPageViewModel)BindingContext).LoadArtist();
      }
   }
}