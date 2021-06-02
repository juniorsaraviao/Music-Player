using MusicPlayer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.Service.Interfaces
{
   public interface IMusicService
   {
      Task<List<Music>> GetAllSongs();
      Task<List<Playlist>> GetPlayLists();
      Task<List<Artist>> GetArtist();
      Task<bool> UpdateSong(Music music);
   }
}
