using MusicPlayer.Model;
using MusicPlayer.Service.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Service
{
   public class MusicService : IMusicService
   {     
      public async Task<List<Music>> GetAllSongs()
      {
         using ( var httpClient = new HttpClient() )
         {
            var songList = await httpClient.GetStringAsync("http://localhost:3000/songs");

            return JsonConvert.DeserializeObject<List<Music>>(songList);
         }         
      }

      public async Task<List<Playlist>> GetPlayLists()
      {
         using ( var httpClient = new HttpClient() )
         {
            var songs = await httpClient.GetStringAsync("http://localhost:3000/playlist");

            return JsonConvert.DeserializeObject<List<Playlist>>(songs);
         }            
      }

      public async Task<bool> UpdateSong(Music music)
      {
         using (var httpClient = new HttpClient())
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
            var content = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage()
            {
               RequestUri = new Uri($"http://localhost:3000/songs/{music.Id}"),
               Content    = content,
               Method     = HttpMethod.Put,
            };
            
            await httpClient.SendAsync(request);

            return true;
         }
      }
   }
}
