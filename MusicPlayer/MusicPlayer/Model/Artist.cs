using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPlayer.Model
{
   public class Artist
   {
      public int    Id            { get; set; }
      public string ArtistName    { get; set; }
      public int    Followers     { get; set; }
      public string MusicalGenre  { get; set; }
      public string ImageUrl      { get; set; }
   }
}
