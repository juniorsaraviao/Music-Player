using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MusicPlayer.Model
{
   public class Playlist
   {
      public string PlaylistName { get; set; }
      public string PlaylistUrl { get; set; }

      public static ObservableCollection<Playlist> PopularPlaylist()
      {
         return new ObservableCollection<Playlist>()
         {
            new Playlist()
            {
               PlaylistName = "Relax & Unwind",
               PlaylistUrl  = "https://www.noteburner.com/images-new/guide/top-10-spotify-playlist/relax-unwind.jpg"
            },
            new Playlist()
            {
               PlaylistName = "Have a Great Day!",
               PlaylistUrl  = "https://www.noteburner.com/images-new/guide/top-10-spotify-playlist/have-a-great-day.jpg"
            },
            new Playlist()
            {
               PlaylistName = "Your Favorite CoffeeHouse",
               PlaylistUrl  = "https://www.noteburner.com/images-new/guide/top-10-spotify-playlist/coffee-house.jpg"
            },
            new Playlist()
            {
               PlaylistName = "Wake Up Happy",
               PlaylistUrl  = "https://www.noteburner.com/images-new/guide/top-10-spotify-playlist/wake-up-happy.jpg"
            },
            new Playlist()
            {
               PlaylistName = "Totally Stress Free",
               PlaylistUrl  = "https://www.noteburner.com/images-new/guide/top-10-spotify-playlist/totally-stress-free.jpg"
            },
            new Playlist()
            {
               PlaylistName = "Acoustic Love",
               PlaylistUrl  = "https://www.noteburner.com/images-new/guide/top-10-spotify-playlist/acoustic-love.jpg"
            }
         };
      }
   }
}
