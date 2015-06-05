using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;

namespace ColorTextBoxes
{
    public class RgbViewModel : INotifyPropertyChanged

    {
        private double red, green, blue;
        private Color color = Color.FromArgb(255, 0, 0, 0);
        public event PropertyChangedEventHandler PropertyChanged;


        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public double Red
        {
            get { return red; }
            set
            {
                if (SetProperty<double>(ref red, value, "Red"))
                    Calculate();
            }
        }

        public double Green
        {
            get { return green; }
            set
            {
                if (SetProperty<double>(ref green, value, "Green"))
                    Calculate();
            }
        }

        public double Blue
        {
            get { return blue; }
            set
            {
                if (SetProperty<double>(ref blue, value, "Blue"))
                    Calculate();
            }
        }

        public Color Color
        {
            get { return color; }
            set
            {
                if (SetProperty<Color>(ref color, value))
                {
                    Red = value.R;
                    Green = value.G;
                    Blue = value.B;
                }
            }
        }

        private void Calculate()
        {
            Color = Color.FromArgb(255, (byte)Red, (byte)Green, (byte)Blue);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
