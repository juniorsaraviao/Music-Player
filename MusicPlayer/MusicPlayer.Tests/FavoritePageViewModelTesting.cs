using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MusicPlayer.Model;
using MusicPlayer.Service.Interfaces;
using MusicPlayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.Tests
{
   [TestClass]
   public class FavoritePageViewModelTesting
   {
      [TestMethod]
      public async Task GetFavoritePlaylistTest()
      {
         // Arrange

         var mockMusicService  = new Mock<IMusicService>();
         var mockDialogService = new Mock<IDialogService>();

         mockMusicService.Setup(x => x.GetAllSongs())
            .ReturnsAsync(MockSongs());

         // Act

         var favoritePageViewModel = new FavoritePageViewModel(
            mockMusicService.Object,
            mockDialogService.Object
            );

         await favoritePageViewModel.GetFavoritePlaylist();

         // Assert
         Assert.AreEqual(5, favoritePageViewModel.FavoritePlaylist.Count);
      }

      [TestMethod]
      public async Task GetFavoritePlaylistThrowErrorTest()
      {
         // Arrange

         var mockMusicService  = new Mock<IMusicService>();
         var mockDialogService = new Mock<IDialogService>();

         mockMusicService.Setup(x => x.GetAllSongs())
            .Throws(new Exception("Load songs error"));

         // Act

         var favoritePageViewModel = new FavoritePageViewModel(
            mockMusicService.Object,
            mockDialogService.Object
            );

         await favoritePageViewModel.GetFavoritePlaylist();

         // Assert
         Assert.AreEqual("Load songs error", favoritePageViewModel.ErrorMessage);
      }

      private List<Music> MockSongs()
      {
         var musicList = new List<Music>();
         for (int i = 0; i < 15; i++)
         {
            if ( i < 5)
            {
               musicList.Add(new Music {
                  Id       = i,
                  IsLike   = true,
                  SongName = $"Music mock {i}"
               });
            }
            else
            {
               musicList.Add(new Music {
                  Id       = i,
                  IsLike   = false,
                  SongName = $"Music mock {i}"
               });
            }            
         }
         return musicList;
      }

   }
}
