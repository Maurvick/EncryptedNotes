using CaesarCipherApp.Models;
using CaesarCipherApp.Services;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaesarCipherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<User> _data;
        private FileManager _fileManager;
        private EventLogger _eventLogger;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Load text from user.json when program starts
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileManager = new FileManager();

            try
            {
                _data = _fileManager.LoadUsersFromFile();
            }
            catch (Exception ex)
            {
                _eventLogger.Log(ex.Message);
                MessageBox.Show($"Faied to load data from file.");
            }

            dgCipherList.ItemsSource = _data;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int cipherKey = int.Parse(txtCipherKey.Text);

            string textToChipher = txtUserInput.Text;

            if (txtUserInput.Text != "")
            {
                var encryptedText = EncryptionHelper.Encipher(textToChipher, cipherKey);

                _data.Add(new User
                {
                    Id = 1,
                    Text = encryptedText,
                    Key = cipherKey,
                });

                _fileManager.SaveData(_data);

                txtUserInput.Text = null;
            }
        }

        private void mnuDecrypt_Click(object sender, RoutedEventArgs e)
        {
            int cipherKey = int.Parse(txtCipherKey.Text);

            User selectedItem = (User)dgCipherList.SelectedItem;

            if (selectedItem != null)
            {
                // Access the property value from the selected item
                string columnNameValue = selectedItem.Text;

                // Do something with the value (e.g., display it)
                string decryptedText = EncryptionHelper.Decipher(columnNameValue, cipherKey);

                MessageBox.Show(decryptedText);
            }
        }

        private void mnuEncrypt_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void mnuDelete_Click(object sender, RoutedEventArgs e)
        {
            // Replace with the index of the column you want to remove
            DataGridColumn clickedColumn = dgCipherList.CurrentColumn;
            int columnIndexToRemove = dgCipherList.Columns.IndexOf(clickedColumn);
            dgCipherList.Columns.RemoveAt(columnIndexToRemove);
            // _fileManager.SaveData(_data);
        }

        private void btnShowFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Use ProcessStartInfo to specify the file or folder to open
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = Environment.CurrentDirectory,
                    UseShellExecute = true
                };

                // Start the process
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                _eventLogger.Log(ex.Message);
            }
        }
    }
}