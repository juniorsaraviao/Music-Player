using Acr.UserDialogs;
using System;

namespace MusicPlayer.Service.Interfaces
{
   public interface IDialogService
   {
      IProgressDialog Dialog();
      IDisposable Toast(string message, TimeSpan dismissTimer);
   }
}
