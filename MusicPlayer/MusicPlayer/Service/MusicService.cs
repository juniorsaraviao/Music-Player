using MusicPlayer.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Service
{
   public class MusicService
   {
      private const string url = "https://musicstreamindemoapp.azurewebsites.net/api/Songs";

      public static async Task<List<Music>> GetAllSongs(int pagNumber, int pageSize)
      {
         var httpClient = new HttpClient();

         var songList = await httpClient.GetStringAsync( string.Format($"{ url }?pageNumber={ pagNumber }&pageSize={ pageSize }"));

         return JsonConvert.DeserializeObject<List<Music>>(songList);
      }
   }
}
