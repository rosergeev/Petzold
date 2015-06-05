using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Windows.UI;

namespace ColorScrollWithViewModel
{
    public class RgbViewModel : INotifyPropertyChanged
    {
        private double red, green, blue;
        Color color = Color.FromArgb(255, 0, 0, 0);

        public event PropertyChangedEventHandler PropertyChanged;

        public double Red
        {
            get { return red; }
            set
            {
                if (red != value)
                {
                    red = value;
                    OnPropertyChanged("Red");
                    Calculate();
                }
            }
        }

        public double Green
        {
            get { return green; }
            set
            {
                if (green != value)
                {
                    green = value;
                    OnPropertyChanged("Green");
                    Calculate();
                }
            }
        }

        public double Blue
        {
            get { return blue; }
            set
            {
                if (blue != value)
                {
                    blue = value;
                    OnPropertyChanged("Blue");
                    Calculate();
                }
            }
        }

        public Color Color
        {
            get { return color; }
            protected set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged("Color");
                }
            }
        }
        
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Calculate()
        {
            Color = Color.FromArgb(255, (byte)Red, (byte)Green, (byte)Blue);
        }
    }
}
