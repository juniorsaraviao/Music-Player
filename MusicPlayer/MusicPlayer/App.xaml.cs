﻿using MusicPlayer.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPlayer
{
   public partial class App : Application
   {
      public App()
      {         
         InitializeComponent();
         MainPage = new MainTabbedPage();
      }

      protected override void OnStart()
      {
      }

      protected override void OnSleep()
      {
      }

      protected override void OnResume()
      {
      }
   }
}
