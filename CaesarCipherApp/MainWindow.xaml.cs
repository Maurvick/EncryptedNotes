using CaesarCipherApp.Models;
using CaesarCipherApp.Services;
using EncryptedNotes.Views;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace CaesarCipherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<User> _list;
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
                _list = _fileManager.LoadUsersFromFile();
            }
            catch (Exception ex)
            {
                _eventLogger.Log(ex.Message);

                MessageBox.Show($"Faied to load data from file.");
            }

            dgCipherList.ItemsSource = _list;
        }

        private void mnuDecrypt_Click(object sender, RoutedEventArgs e)
        {
            int cipherKey = int.Parse("3");

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
            EncryptionWindow w = new EncryptionWindow();

            w.ShowDialog();

            string text = w.Text;
            int key = int.Parse(w.Key);

            if (w.Text != null & w.Key != null)
            {
                var encryptedText = EncryptionHelper.Encipher(text, key);

                _list.Add(new User
                {
                    Id = 1,
                    Text = encryptedText,
                    Key = key,
                });

                _fileManager.SaveData(_list);
            }
        }

        private void mnuCopy_Click(object sender, RoutedEventArgs e)
        {
            // int cipherKey = int.Parse(txtCipherKey.Text);

            User selectedItem = (User)dgCipherList.SelectedItem;

            if (selectedItem != null)
            {
                // Access the property value from the selected item
                string columnNameValue = selectedItem.Text ?? "";

                // Do something with the value (e.g., display it)
                string decryptedText = EncryptionHelper.Decipher(columnNameValue, 3);

                Clipboard.SetText(decryptedText);
            } 
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