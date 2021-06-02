using System.Threading.Tasks;

namespace MusicPlayer.API.Interfaces
{
   public interface IServices
   {
      Task<string> GetAllSong();
      Task<string> GetPlayLists();
      Task<string> GetArtist();
      Task<bool>   UpdateSong(string parameters, int id);
   }
}
