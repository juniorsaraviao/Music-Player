using Acr.UserDialogs;
using MediaManager;
using MusicPlayer.Model;
using MusicPlayer.Util;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicPlayer.ViewModel
{
   public class PlayPageViewModel : BaseViewModel
   {
      private Music     _selectedMusic;
      private string    _playPauseImage;
      private double    _maximum = 100f;
      private string    _finalTime;
      private TimeSpan  _position;
      private string    _initialTime;
      private TimeSpan  _duration;

      public Music SelectedMusic
      {
         get => _selectedMusic;
         set 
         {
            _selectedMusic = value;
            OnPropertyChanged();
         }
      }
      public string PlayPauseImg
      {
         get => _playPauseImage;
         set
         {
            _playPauseImage = value;
            OnPropertyChanged();
         }
      }
      public double Maximum
      {
         get => _maximum;
         set
         {
            if (value > 0)
            {
               _maximum = value;
               OnPropertyChanged();
            }
         }
      }
      public string FinalTime
      {
         get => _finalTime;
         set
         {
            _finalTime = value;
            OnPropertyChanged();
         }
      }
      public TimeSpan Position
      {
         get => _position;
         set
         {
            _position = value;
            OnPropertyChanged();
         }
      }
      public string InitialTime
      {
         get => _initialTime;
         set
         {
            _initialTime = value;
            OnPropertyChanged();
         }
      }
      public TimeSpan Duration
      {
         get => _duration;
         set
         {
            _duration = value;
            OnPropertyChanged();
         }
      }

      public ICommand BackwardCommand  => new Command( async () => await BackwardPosition() );
      public ICommand ForwardCommand   => new Command( async () => await ForwardPosition() );
      public ICommand PlayPauseCommand => new Command( async () => await PausePlay_OnTapped() );

      public async Task BackwardPosition()
      {
         await CrossMediaManager.Current.StepBackward();
      }
      public async Task ForwardPosition()
      {
         await CrossMediaManager.Current.StepForward();
      }
      private bool _isClicked = false;
      private async Task PausePlay_OnTapped()
      {
         if ( _isClicked )
         {
            await CrossMediaManager.Current.Play();
            PlayPauseImg = FontAwesomeIcon.PauseButton;
            _isClicked = false;
         }
         else
         {
            await CrossMediaManager.Current.Pause();
            PlayPauseImg = FontAwesomeIcon.PlayButton;
            _isClicked = true;
         }
      }

      public PlayPageViewModel(Music selectedMusic)
      {
         SelectedMusic = selectedMusic;
         CrossMediaManager.Current.Stop();
      }

      public async Task PlayMusic()
      {
         using (UserDialogs.Instance.Loading())
         {
            PlayPauseImg = FontAwesomeIcon.PauseButton;
            await Task.Delay(1000);
            await CrossMediaManager.Current.Play(SelectedMusic.SongUrl);            

            Position    = CrossMediaManager.Current.Position;
            Maximum     = CrossMediaManager.Current.Duration.TotalSeconds;
            FinalTime   = CrossMediaManager.Current.Duration.ToString(@"mm\:ss");
            InitialTime = CrossMediaManager.Current.Position.ToString(@"mm\:ss");

            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
               Position    = CrossMediaManager.Current.Position;
               Duration    = CrossMediaManager.Current.Duration;
               Maximum     = Duration.TotalSeconds;
               FinalTime   = Duration.ToString(@"mm\:ss");
               InitialTime = Position.ToString(@"mm\:ss");
               return true;
            });

         }         
      }

      private async void Current_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
      {
         Position    = e.Position;
         Duration    = CrossMediaManager.Current.Duration;
         Maximum     = Duration.TotalSeconds;
         FinalTime   = Duration.ToString(@"mm\:ss");
         InitialTime = Position.ToString(@"mm\:ss");

         if (e.Position >= CrossMediaManager.Current.Duration)
         {
            await Task.Delay(100);
            await CrossMediaManager.Current.Stop();
            await Task.Delay(100);
            PlayPauseImg = FontAwesomeIcon.PlayButton;
         }
      }
   }
}
