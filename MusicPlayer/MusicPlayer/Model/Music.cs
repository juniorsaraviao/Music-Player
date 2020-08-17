using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPlayer.Model
{
   public class Music
   {
      public int    SongId        { get; set; }
      public string SongName      { get; set; }
      public string SongFileCover { get; set; }
      public string SongUrl       { get; set; }
      public string SongDuration  { get; set; }
      public string SingerName    { get; set; }
      public bool   IsLike        { get; set; }
      public bool   IsNotLike     => !IsLike;
   }
}
