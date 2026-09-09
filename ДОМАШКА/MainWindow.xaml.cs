using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ДОМАШКА
{
    public partial class MainWindow : Window
    {
        private string dataFile = "users.txt";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text.Trim();
            string password = PasswordBox.Password;

            if (login.Length < 1)
            {
                StatusText.Text = "Логин должен быть минимум 1 символ!";
                return;
            }
            if (password.Length < 4)
            {
                StatusText.Text = "Пароль должен быть минимум 4 символа!";
                return;
            }

            if (CheckUser(login, password))
            {
                StatusText.Text = "ДОБРО ПОЖАЛОВАТЬ!";
                StatusText.Foreground = System.Windows.Media.Brushes.Green;
                MessageBox.Show("Вход выполнен успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                AccessDeniedWindow deniedWindow = new AccessDeniedWindow();
                deniedWindow.ShowDialog();
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text.Trim();
            string password = PasswordBox.Password;

            if (login.Length < 1)
            {
                StatusText.Text = "Логин должен быть минимум 1 символ!";
                return;
            }
            if (password.Length < 4)
            {
                StatusText.Text = "Пароль должен быть минимум 4 символа!";
                return;
            }

            SaveUser(login, password);
            StatusText.Text = "Регистрация успешна!";
            StatusText.Foreground = System.Windows.Media.Brushes.Green;
            MessageBox.Show("Пользователь зарегистрирован!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveUser(string login, string password)
        {
            string encryptedPassword = EncryptPassword(password);
            string userData = $"{login}:{encryptedPassword}";
            File.AppendAllText(dataFile, userData + Environment.NewLine);
        }

        private bool CheckUser(string login, string password)
        {
            if (!File.Exists(dataFile))
                return false;

            string encryptedPassword = EncryptPassword(password);
            string[] lines = File.ReadAllLines(dataFile);

            foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 2 && parts[0] == login && parts[1] == encryptedPassword)
                {
                    return true;
                }
            }
            return false;
        }

        private string EncryptPassword(string password)
        {
            char[] chars = password.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)(chars[i] + 3);
            }
            return new string(chars);
        }
    }
}