using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace LearningWPF.UserControls
{
    /// <summary>
    /// Interaction logic for HelloWorld.xaml
    /// </summary>
    public partial class HelloWorld : UserControl
    {
        
        public HelloWorld()
        {
            InitializeComponent();
        }

        private void HelloWorld_OnLayoutUpdated(object? sender, EventArgs e)
        {
            DataContext = Window.GetWindow(this);
        }

        private void UpdateSourceButton_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression binding = WindowTitleTextBox.GetBindingExpression(TextBox.TextProperty);
            binding?.UpdateSource();
        }

        private void ClickMeButton_Click(object sender, RoutedEventArgs e)
        {
            LbResult.Items.Add(UserControlMainPanel.FindResource("PanelResourceString").ToString());
            LbResult.Items.Add(this.FindResource("UserControlResourceString").ToString());
            LbResult.Items.Add(Window.GetWindow(this).FindResource("WindowResourceString").ToString());
            LbResult.Items.Add(Application.Current.FindResource("ApplicationResourceString")?.ToString());
        }

    }
}
