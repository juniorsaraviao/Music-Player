using Autofac;
using MusicPlayer.API.Interfaces;
using MusicPlayer.Model;
using MusicPlayer.Service.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.Service
{
   public class MusicService : IMusicService
   {
      private readonly IServices _apiService;

      public MusicService() : this(
         DIServiceContainer.Container.Resolve<IServices>()
      )
      {
      }

      public MusicService( IServices apiService )
      {
         _apiService = apiService;
      }
      public async Task<List<Music>> GetAllSongs()
      {
         var songList = await _apiService.GetAllSong();
         return JsonConvert.DeserializeObject<List<Music>>(songList);
      }

      public async Task<List<Playlist>> GetPlayLists()
      {
         var playList = await _apiService.GetPlayLists();         
         return JsonConvert.DeserializeObject<List<Playlist>>(playList);
      }

      public async Task<bool> UpdateSong(Music music)
      {
         var dictionary = new Dictionary<string, object>
         {
            { "isLike", music.IsLike },
            { "songName", music.SongName },
            { "songFileCover", music.SongFileCover },
            { "songUrl", music.SongUrl },
            { "songDuration", music.SongDuration },
            { "singerName", music.SingerName }
         };

         await _apiService.UpdateSong(JsonConvert.SerializeObject(dictionary), music.Id);

         return true;
      }
   }
}
