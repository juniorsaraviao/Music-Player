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
   public partial class FavoritePage : ContentPage
   {
      public FavoritePage()
      {
         InitializeComponent();
      }

      protected async override void OnAppearing()
      {
         base.OnAppearing();
         await ((FavoritePageViewModel)BindingContext).GetRadialBackground();
         await ((FavoritePageViewModel)BindingContext).GetFavoritePlaylist();
      }
   }
}