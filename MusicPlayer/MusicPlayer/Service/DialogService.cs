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

      public IDisposable Alert(string message, string title, string okText)
      {
         return UserDialogs.Instance.Alert(message, title, okText);
      }
   }
}
