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
   public class ArtistPageViewModelTesting
   {
      [TestMethod]
      public async Task GetArtistListTest()
      {
         // Arrange

         var mockMusicService  = new Mock<IMusicService>();
         var mockDialogService = new Mock<IDialogService>();

         mockMusicService.Setup(x => x.GetArtist())
            .ReturnsAsync(MockArtist());

         // Act

         var artistPageViewModel = new ArtistPageViewModel(
            mockMusicService.Object,
            mockDialogService.Object
            );

         await artistPageViewModel.LoadArtist();

         // Assert
         Assert.AreEqual(10, artistPageViewModel.ArtistList.Count);
      }

      [TestMethod]
      public async Task GetArtistListThrowErrorTest()
      {
         // Arrange

         var mockMusicService  = new Mock<IMusicService>();
         var mockDialogService = new Mock<IDialogService>();

         mockMusicService.Setup(x => x.GetArtist())
            .Throws(new Exception("Cannot load Artists"));

         // Act

         var artistPageViewModel = new ArtistPageViewModel(
            mockMusicService.Object,
            mockDialogService.Object
            );

         await artistPageViewModel.LoadArtist();

         // Assert
         Assert.AreEqual("Cannot load Artists", artistPageViewModel.ErrorMessage);
      }

      private List<Artist> MockArtist()
      {
         var artistList = new List<Artist>();

         for (int i = 1; i <= 10; i++)
         {
            artistList.Add( new Artist
            {
               ArtistName = $"Artist {i}"
            } );
         }

         return artistList;
      }
   }
}
