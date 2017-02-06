#region
using System;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;

#endregion

namespace BTOCommunicator
{
    public partial class BtoCommunicator
    {
       

        /// <summary>
        ///     This is used to load and intialize UI Component
        /// </summary>
        public BtoCommunicator()
        {
            try
            {
                InitializeComponent();
                
            }
            catch (Exception ex)
            {
            }
        }

      
        /// <summary>
        ///     This method is used to shutdown application on close button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        ///     This method is used for opening async messagebox to show messages to user.
        /// </summary>
        /// <param name="message"></param>
        private async void OpenMessageBox(string message)
        {
            await this.ShowMessageAsync(message, "BTOCommunicator");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("you hit me");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

    }
}