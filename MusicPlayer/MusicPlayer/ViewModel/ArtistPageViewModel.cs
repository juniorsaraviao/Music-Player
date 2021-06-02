using Autofac;
using MusicPlayer.Constant;
using MusicPlayer.Model;
using MusicPlayer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.ViewModel
{
   public class ArtistPageViewModel : BaseViewModel
   {
      #region Fields

      private          IList<Artist> _artistList;
      private readonly IMusicService  _musicService;
      private readonly IDialogService _dialogService;

      #endregion

      #region Properties

      public IList<Artist> ArtistList
      {
         get => _artistList;
         set
         {
            _artistList = value;
            OnPropertyChanged();
         }
      }

      public string ErrorMessage { get; set; }

      #endregion

      #region Constructor

      public ArtistPageViewModel() : this( 
         DIServiceContainer.Container.Resolve<IMusicService>(), 
         DIServiceContainer.Container.Resolve<IDialogService>()
      ) 
      {

      }

      public ArtistPageViewModel( 
         IMusicService  musicService,
         IDialogService dialogService
      )
      {
         _musicService    = musicService;
         _dialogService   = dialogService;
         ArtistList       = new List<Artist>();
      }

      #endregion

      #region Methods

      public async Task LoadArtist()
      {
         try
         {
            using ( _dialogService.Dialog() )
            {
               ArtistList = await _musicService.GetArtist();
            }               
         }
         catch (Exception ex)
         {
            ErrorMessage = ex.Message;
            _dialogService.Alert(Constants.LoadingArtistsError, Constants.LoadingArtistTitle, Constants.OkText);
         }
          
      }

      #endregion      

   }
}
