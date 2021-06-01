using Acr.UserDialogs;
using MusicPlayer.Service.Interfaces;
using System;

namespace MusicPlayer.Service
{
   public class DialogService : IDialogService
   {
      public IProgressDialog Dialog()
      {
         return UserDialogs.Instance.Loading();
      }

      public IDisposable Toast(string message, TimeSpan dismissTimer)
      {
         return UserDialogs.Instance.Toast(message, dismissTimer);
      }
   }
}
