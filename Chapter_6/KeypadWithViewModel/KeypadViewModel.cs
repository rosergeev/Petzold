using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KeypadWithViewModel
{
    public class KeypadViewModel : INotifyPropertyChanged
    {
        string inputString = "";
        string displayText = "";
        char[] specialChars = { '*', '#' };
        
        public event PropertyChangedEventHandler PropertyChanged;

        public KeypadViewModel()
        {
            AddCharacterCommand = new DelegateCommand(ExecuteAddCharacter);
            DeleteCharacterCommand = new DelegateCommand(ExecuteDeleteCharacter, CanExecuteDeleteCharacter);
        }

        public string InputString
        {
            get { return inputString; }
            set
            {
                bool previousCanExecuteDeleteChar = CanExecuteDeleteCharacter(null);
                if (SetProperty<string>(ref inputString, value))
                {
                    DisplayText = FormatText(inputString);

                    if (previousCanExecuteDeleteChar != CanExecuteDeleteCharacter(null))
                        DeleteCharacterCommand.RaiseCanExecuteChanged();
                }

            }
        }
        
        public string DisplayText
        {
            get { return displayText; }
            set { SetProperty<string>(ref displayText, value); }
        }
        public IDelegateCommand AddCharacterCommand { get; protected set; }
        public IDelegateCommand DeleteCharacterCommand { get; protected set; }

        private void ExecuteAddCharacter(object param)
        {
            InputString += param as string;
        }

        private void ExecuteDeleteCharacter(object obj)
        {
            InputString = InputString.Substring(0, InputString.Length - 1);
        }

        private bool CanExecuteDeleteCharacter(object arg)
        {
            return InputString.Length > 0;
        }

        private string FormatText(string str)
        {
            bool hasNonNumbers = str.IndexOfAny(specialChars) != -1;
            string formatted = str;

            if (hasNonNumbers || str.Length < 4 || str.Length > 10)
            {
            }
            else if (str.Length < 8)
            {
                formatted = $"{str.Substring(0, 3)}-{str.Substring(3)}";
            }
            else
            {
                formatted = $"({str.Substring(0, 3)}) {str.Substring(3, 3)}-{str.Substring(6)}";
            }

            return formatted;
        }        

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
