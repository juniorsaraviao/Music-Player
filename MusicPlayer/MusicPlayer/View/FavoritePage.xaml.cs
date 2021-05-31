using MusicPlayer.Model;
using MusicPlayer.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPlayer.View
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class FavoritePage : ContentPage
   {
      public FavoritePage()
      {
         InitializeComponent();
      }

      protected async override void OnAppearing()
      {
         base.OnAppearing();
         await ( (FavoritePageViewModel)BindingContext ).GetFavoritePlaylist();

         MessagingCenter.Subscribe<MusicPageViewModel>(this, "Reload", async (sender) => {
            await ( (FavoritePageViewModel)BindingContext ).GetFavoritePlaylist();
         });

      }

      protected override void OnDisappearing()
      {
         base.OnDisappearing();
         MessagingCenter.Unsubscribe<MusicPageViewModel>(this, "Reload");
      }
   }
}