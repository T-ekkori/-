using System.Windows;

namespace ДОМАШКА
{
    public partial class AccessDeniedWindow : Window
    {
        public AccessDeniedWindow()
        {
            InitializeComponent();
        }

        private void RetryButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}