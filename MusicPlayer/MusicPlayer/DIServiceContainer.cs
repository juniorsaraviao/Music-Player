using Autofac;

namespace MusicPlayer
{
   public class DIServiceContainer
   {
      private static IContainer _container;

      public static IContainer Container
      {
         get => _container;
         set
         {
            _container = value;
         }
      }

      public static T Resolve<T>()
      {
         return Container.Resolve<T>();
      }
   }
}
