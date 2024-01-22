using System.ComponentModel;

namespace CaesarCipherApp.Models
{
    internal class User : INotifyPropertyChanged
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _key;

        public int Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private string? _text;

        public string Text
        {
            get 
            { 
                return _text; 
            }
            set 
            {
                if (_text == value) return;
                _text = value;
                OnPropertyChanged("Text");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
