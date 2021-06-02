using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MusicPlayer.Model;
using MusicPlayer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.Tests
{
   [TestClass]
   public class MusicPageViewModelTests
   {
      [TestMethod]
      public async Task PlayListCountTest()
      {
         // Arrange

         var mockMusicService  = new Mock<IMusicService>();
         var mockDialogService = new Mock<IDialogService>();

         mockMusicService.Setup( x => x.GetPlayLists() )
            .ReturnsAsync(MockPlayList());

         mockMusicService.Setup(x => x.GetAllSongs())
            .ReturnsAsync(new List<Music>());

         // Act

         var musicPageViewModel = new MusicPageViewModel(
            mockMusicService.Object,
            mockDialogService.Object
            );

         await musicPageViewModel.GetSongs();

         // Assert
         Assert.AreEqual(10, musicPageViewModel.MyPlaylist.Count);
         Assert.AreEqual(0, musicPageViewModel.FavoriteMusicList.Count);
      }

      [TestMethod]
      public async Task FavoriteMusicListCountTest()
      {
         // Arrange

         var mockMusicService  = new Mock<IMusicService>();
         var mockDialogService = new Mock<IDialogService>();

         mockMusicService.Setup( x => x.GetPlayLists() )
            .ReturnsAsync(new List<Playlist>());

         mockMusicService.Setup(x => x.GetAllSongs())
            .ReturnsAsync(MockSongs());

         // Act

         var musicPageViewModel = new MusicPageViewModel(
            mockMusicService.Object,
            mockDialogService.Object
            );

         await musicPageViewModel.GetSongs();

         // Assert
         Assert.AreEqual(0, musicPageViewModel.MyPlaylist.Count);
         Assert.AreEqual(15, musicPageViewModel.FavoriteMusicList.Count);
      }

      [TestMethod]
      public async Task ThrowErrorMessageTest()
      {
         // Arrange

         var mockMusicService  = new Mock<IMusicService>();
         var mockDialogService = new Mock<IDialogService>();

         mockMusicService.Setup( x => x.GetPlayLists() )
            .Throws(new Exception("No PlaylistData"));

         // Act

         var musicPageViewModel = new MusicPageViewModel(
            mockMusicService.Object,
            mockDialogService.Object
            );

         await musicPageViewModel.GetSongs();

         // Assert
         Assert.AreEqual("No PlaylistData", musicPageViewModel.ErrorMessage);
      }

      [TestMethod]
      public async Task LikeMethodTrueTest()
      {
         // Arrange

         var mockMusicService  = new Mock<IMusicService>();
         var mockDialogService = new Mock<IDialogService>();

         mockMusicService.Setup(x => x.GetAllSongs())
            .ReturnsAsync(MockSongs());

         mockMusicService.Setup(x => x.UpdateSong(It.IsAny<Music>()))
            .ReturnsAsync(true);

         // Act

         var musicPageViewModel = new MusicPageViewModel(
            mockMusicService.Object,
            mockDialogService.Object
            );

         await musicPageViewModel.LikeMethod(new Music());

         // Assert
         Assert.IsTrue(musicPageViewModel.IsUpdated);
      }

      [TestMethod]
      public async Task LikeMethodFalseTest()
      {
         // Arrange

         var mockMusicService  = new Mock<IMusicService>();
         var mockDialogService = new Mock<IDialogService>();

         mockMusicService.Setup(x => x.GetAllSongs())
            .ReturnsAsync(MockSongs());

         mockMusicService.Setup(x => x.UpdateSong(It.IsAny<Music>()))
            .ReturnsAsync(false);

         // Act

         var musicPageViewModel = new MusicPageViewModel(
            mockMusicService.Object,
            mockDialogService.Object
            );

         await musicPageViewModel.LikeMethod(new Music());

         // Assert
         Assert.IsFalse(musicPageViewModel.IsUpdated);
      }

      [TestMethod]
      public async Task LikeMethodThrowErrorsTest()
      {
         // Arrange

         var mockMusicService  = new Mock<IMusicService>();
         var mockDialogService = new Mock<IDialogService>();

         mockMusicService.Setup(x => x.UpdateSong(It.IsAny<Music>()))
            .Throws(new Exception("Update song failed."));

         // Act

         var musicPageViewModel = new MusicPageViewModel(
            mockMusicService.Object,
            mockDialogService.Object
            );

         await musicPageViewModel.LikeMethod(new Music());

         // Assert
         Assert.AreEqual(musicPageViewModel.ErrorMessage, "Update song failed.");
      }

      [TestMethod]
      public void DefineGreetingTesting()
      {
         // Arrange

         var mockMusicService  = new Mock<IMusicService>();
         var mockDialogService = new Mock<IDialogService>();

         // Act

         var musicPageViewModel = new MusicPageViewModel(
            mockMusicService.Object,
            mockDialogService.Object
            );

         musicPageViewModel.CurrentDate = new DateTime(2020, 6, 20, 15, 0, 0);

         musicPageViewModel.DefineGreeting();

         // Assert
         Assert.AreEqual("Good afternoon", musicPageViewModel.Greeting);
      }


      private List<Playlist> MockPlayList()
      {
         var playList = new List<Playlist>();
         for (int i = 0; i < 10; i++)
         {
            playList.Add(new Playlist {
               PlaylistName = $"Playlist {i}",
               PlaylistUrl  = "www.google.com"
            });
         }
         return playList;
      }

      private List<Music> MockSongs()
      {
         var musicList = new List<Music>();
         for (int i = 0; i < 15; i++)
         {
            musicList.Add(new Music {
               Id       = i,
               IsLike   = true,
               SongName = $"Music mock {i}"
            });;
         }
         return musicList;
      }
   }
}
