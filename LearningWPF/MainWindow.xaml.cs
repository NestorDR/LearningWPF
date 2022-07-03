using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Common.Library;
using LearningWPF.ViewModels;

namespace LearningWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Private variables
        // Main window's view model class
        private readonly MainWindowViewModel _viewModel;
        // Hold the main window's original status message
        private readonly string _originalMessage;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            // Connect to instance of the view model created by the XAML
            _viewModel = (MainWindowViewModel)Resources["ViewModel"];

            // Get the original status message
            _originalMessage = _viewModel.StatusMessage;

            // Initialize the Message Broker Events
            MessageBroker.Instance.MessageReceived += Instance_MessageReceived;

        }
        #endregion

        private async void MainWindow_OnContentRendered(object? sender, EventArgs e)
        {
            // Call method to load resources application
            await LoadApplication();

            // Turn off informational message area
            _viewModel.ClearInfoMessages();
        }

        private void Instance_MessageReceived(object sender, MessageBrokerEventArgs e)
        {
            switch (e.MessageName)
            {
                case MessageBrokerMessages.DISPLAY_TIMEOUT_INFO_MESSAGE_TITLE:
                    _viewModel.InfoMessageTitle = e.MessagePayload.ToString();
                    _viewModel.CreateInfoMessageTimer();
                    break;

                case MessageBrokerMessages.DISPLAY_TIMEOUT_INFO_MESSAGE:
                    _viewModel.InfoMessage = e.MessagePayload.ToString();
                    _viewModel.CreateInfoMessageTimer();
                    break;

                case MessageBrokerMessages.DISPLAY_STATUS_MESSAGE:
                    // Set new status message
                    _viewModel.StatusMessage = e.MessagePayload.ToString();
                    break;

                case MessageBrokerMessages.CLOSE_USER_CONTROL:
                    CloseUserControl();
                    break;
            }
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var mnu = (MenuItem)sender;

            // The Tag property contains a command or the name of a user control to load
            if (mnu.Tag == null) return;
            var cmd = mnu.Tag.ToString();

            if (cmd!.Contains('.'))
            {
                // Display a user control
                LoadUserControl(cmd);

            }
            else
            {
                // Process special commands
                ProcessMenuCommands(cmd);
            }
        }

        private void LoadUserControl(string controlName)
        {
            if (!ShouldLoadUserControl(controlName)) return;

            // Create a Type from controlName parameter
            var ucType = Type.GetType(controlName);
            if (ucType == null)
            {
                MessageBox.Show($"The control: {controlName} does not exist.");
            }
            else
            {
                // Close current user control in content area
                // NOTE: Optionally add current user control to a list 
                //       so you can restore it when you close the newly added one
                CloseUserControl();

                // Create an instance of this control
                if (Activator.CreateInstance(ucType) is UserControl uc)
                {
                    // Display control in content area
                    DisplayUserControl(uc);
                }
            }
        }

        public void DisplayUserControl(UserControl uc)
        {
            // Add new user control to content area
            ContentArea.Children.Add(uc);
        }

        private void ProcessMenuCommands(string command)
        {
            switch (command.ToLower())
            {
                case "exit":
                    this.Close();
                    break;

                case "login":
                    /*
                    // TODO: Login/Logout
                    if (_viewModel.UserEntity.IsLoggedIn)
                    {
                        // Logging out, so close any open windows
                        // Reset the user object
                        // Make menu display Login
                    }
                    else
                    {
                        // Display the login screen
                    }
                    */
                    break;
            }
        }

        private bool ShouldLoadUserControl(string controlName)
        {
            bool ret = true;

            // Make sure you don't reload a control already loaded.
            if (ContentArea.Children.Count > 0)
            {
                if (((UserControl)ContentArea.Children[0]).GetType().FullName == controlName)
                {
                    ret = false;
                }
            }

            return ret;
        }

        private void CloseUserControl()
        {
            // Remove current user control
            ContentArea.Children.Clear();

            // Restore the original status message
            _viewModel.StatusMessage = _originalMessage;
        }

        #region LoadApplication Method
        public async Task LoadApplication()
        {
            static async Task Delay(float seconds) => await Task.Delay((int) (seconds * 1000));

            _viewModel.InfoMessage = "Simulating load...";
            await Delay((float)0.5);
            _viewModel.InfoMessage = "Simulating another load...";
            await Delay(1);
        }
        #endregion
    }
}
