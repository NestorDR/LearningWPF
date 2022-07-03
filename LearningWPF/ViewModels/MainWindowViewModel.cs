using System.Timers;
using Common.Library;

namespace LearningWPF.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Private Variables
        private string _loginMenuHeader = "Login";
        private string _statusMessage = string.Empty;

        private bool _isInfoMessageVisible = true;
        private string _infoMessageTitle = string.Empty;
        private string _infoMessage = string.Empty;

        private Timer? _infoMessageTimer;
        private int _infoMessageTimeout;
        #endregion

        #region Public Properties
        public string LoginMenuHeader
        {
            get => _loginMenuHeader;
            set
            {
                _loginMenuHeader = value;
                RaisePropertyChanged(nameof(LoginMenuHeader));
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                RaisePropertyChanged("StatusMessage");
            }
        }

        public bool IsInfoMessageVisible
        {
            get => _isInfoMessageVisible;
            set
            {
                _isInfoMessageVisible = value;
                RaisePropertyChanged(nameof(IsInfoMessageVisible));
            }
        }

        public string InfoMessage
        {
            get => _infoMessage;
            set
            {
                _infoMessage = value;
                RaisePropertyChanged(nameof(InfoMessage));
            }
        }

        public string InfoMessageTitle
        {
            get => _infoMessageTitle;
            set
            {
                _infoMessageTitle = value;
                RaisePropertyChanged(nameof(InfoMessageTitle));
            }
        }

        public int InfoMessageTimeout
        {
            get => _infoMessageTimeout;
            set
            {
                _infoMessageTimeout = value;
                RaisePropertyChanged(nameof(InfoMessageTimeout));
            }
        }
        #endregion

        #region ClearInfoMessage Method
        public void ClearInfoMessages()
        {
            InfoMessage = string.Empty;
            InfoMessageTitle = string.Empty;
            IsInfoMessageVisible = false;
        }
        #endregion

        #region Message Timer Methods
        public virtual void CreateInfoMessageTimer()
        {
            if (_infoMessageTimer == null)
            {
                // Create informational message timer
                _infoMessageTimer = new Timer(_infoMessageTimeout);
                // Connect to an Elapsed event
                _infoMessageTimer.Elapsed += MessageTimer_Elapsed;
            }
            _infoMessageTimer.AutoReset = false;
            _infoMessageTimer.Enabled = true;
            IsInfoMessageVisible = true;
        }

        private void MessageTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            IsInfoMessageVisible = false;
        }
        #endregion
    }
}
