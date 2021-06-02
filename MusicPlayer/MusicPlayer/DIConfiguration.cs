using Autofac;
using MusicPlayer.API;
using MusicPlayer.API.Interfaces;
using MusicPlayer.Service;
using MusicPlayer.Service.Interfaces;
using MusicPlayer.View;

namespace MusicPlayer
{
   public class DIConfiguration
   {
      public static IContainer Configure()
      {
         var builder = new ContainerBuilder();

         builder.RegisterType<MusicService>().As<IMusicService>();
         builder.RegisterType<DialogService>().As<IDialogService>();
         builder.RegisterType<APIServices>().As<IServices>();
         builder.RegisterType<FavoritePage>();
         builder.RegisterType<MusicPage>();
         builder.RegisterType<PlayPage>();

         return builder.Build();
      }
   }
}
