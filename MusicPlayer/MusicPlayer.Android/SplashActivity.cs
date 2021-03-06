﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MusicPlayer.Droid
{
   [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize)]
   public class SplashActivity : Activity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         // Create your application here
         Window.SetNavigationBarColor(Android.Graphics.Color.Rgb(0, 0, 0));
         StartActivity(typeof(MainActivity));
      }
   }
}