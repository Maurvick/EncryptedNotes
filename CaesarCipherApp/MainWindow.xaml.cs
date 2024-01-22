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
        private const string OUTPUT_TEXT_PATH = "./user.json";
        private readonly FileIOService _fileIOService;
        private BindingList<User>? dataList;
        
        public MainWindow()
        {
            InitializeComponent();
            _fileIOService = new FileIOService(OUTPUT_TEXT_PATH);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dataList = _fileIOService.LoadData();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load data from file.");
            }

            dgCipherList.ItemsSource = dataList;

            dataList.ListChanged += dataList_ListChanged;
        }

        private void dataList_ListChanged(object? sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded ||
                e.ListChangedType == ListChangedType.ItemDeleted ||
                e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIOService.SaveData(sender);
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to save data.");
                }
            }
        }

        private void mnuDecrypt_Click(object sender, RoutedEventArgs e)
        {
            User selectedItem = (User)dgCipherList.SelectedItem;

            if (selectedItem != null)
            {
                // Access the property value from the selected item
                string columnText = selectedItem.Text;
                int columnKey = selectedItem.Key;

                // Do something with the value (e.g., display it)
                string decryptedText = CaesarEncryptionService.Decipher(columnText, columnKey);

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
                var encryptedText = CaesarEncryptionService.Encipher(text, key);

                dataList.Add(new User
                {
                    Id = 1,
                    Text = encryptedText,
                    Key = key,
                });

                _fileIOService.SaveData(dataList);
            }
        }

        private void mnuCopy_Click(object sender, RoutedEventArgs e)
        {
            User selectedItem = (User)dgCipherList.SelectedItem;

            if (selectedItem != null)
            {
                // Access the property value from the selected item
                string columnNameValue = selectedItem.Text ?? "";

                // Do something with the value (e.g., display it)
                string decryptedText = CaesarEncryptionService.Decipher(columnNameValue, 3);

                Clipboard.SetText(decryptedText);
            } 
        }

        private void mnuDelete_Click(object sender, RoutedEventArgs e)
        {
            // Replace with the index of the column you want to remove
            DataGridColumn clickedColumn = dgCipherList.CurrentColumn;
            int columnIndexToRemove = dgCipherList.Columns.IndexOf(clickedColumn);
            dgCipherList.Columns.RemoveAt(columnIndexToRemove);
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
            catch (Exception)
            {
                MessageBox.Show("Failed to open program folder.");
            }
        }
    }
}