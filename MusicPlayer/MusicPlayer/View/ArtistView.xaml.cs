using MusicPlayer.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPlayer.View
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ArtistView : ContentView
   {
      public ArtistView()
      {
         InitializeComponent();
      }

      public static readonly BindableProperty ArtistBindingProperty = BindableProperty.Create(
         nameof(ArtistBinding), 
         typeof(Artist), 
         typeof(ArtistView));

      public Artist ArtistBinding
      {
         get => (Artist) GetValue( ArtistBindingProperty );
         set => SetValue( ArtistBindingProperty, value );
      }
   }
}