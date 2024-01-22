using System.Windows;

namespace EncryptedNotes.Views
{
    /// <summary>
    /// Interaction logic for EncryptionWindow.xaml
    /// </summary>
    public partial class EncryptionWindow : Window
    {
        private string _text;
       
        public string Text
        {
            get { return _text; }
        }

        private string _key;

        public string Key
        {
            get { return _key; }
        }

        public EncryptionWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string text = txtText.Text;
            string key = txtKey.Text;
            bool isValid = int.TryParse(txtKey.Text, out int num);

            if (!isValid)
            {
                MessageBox.Show("Form is invalid.");
                return;
            }

            _text = text;
            _key = key;

            Close();
        }

        private void txtText_GotFocus(object sender, RoutedEventArgs e)
        {
            txtText.Text = null;
        }

        private void txtKey_GotFocus(object sender, RoutedEventArgs e)
        {
            txtKey.Text = null;
        }
    }
}
