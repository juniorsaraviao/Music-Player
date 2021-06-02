using MusicPlayer.API.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.API
{
   public class APIServices : IServices
   {
      public async Task<string> GetAllSong()
      {
         using (var httpClient = new HttpClient())
         {
            var request = new HttpRequestMessage()
            {
               RequestUri = new Uri($"{APIConstants.BaseUrl}/songs"),
               Method     = HttpMethod.Get,
            };
            var response = await httpClient.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
         }
      }

      public async Task<string> GetPlayLists()
      {
         using (var httpClient = new HttpClient())
         {
            var request = new HttpRequestMessage()
            {
               RequestUri = new Uri($"{APIConstants.BaseUrl}/playlist"),
               Method     = HttpMethod.Get,
            };
            var response = await httpClient.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
         }
      }

      public async Task<string> GetArtist()
      {
         using (var httpClient = new HttpClient())
         {
            var request = new HttpRequestMessage()
            {
               RequestUri = new Uri($"{APIConstants.BaseUrl}/artists"),
               Method     = HttpMethod.Get,
            };
            var response = await httpClient.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
         }
      }

      public async Task<bool> UpdateSong(string parameters, int id)
      {
         using (var httpClient = new HttpClient())
         {
            var content = new StringContent(parameters, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage()
            {
               RequestUri = new Uri($"{APIConstants.BaseUrl}/songs/{id}"),
               Content    = content,
               Method     = HttpMethod.Put,
            };

            await httpClient.SendAsync(request);

            return true;
         }
      }
   }
}
