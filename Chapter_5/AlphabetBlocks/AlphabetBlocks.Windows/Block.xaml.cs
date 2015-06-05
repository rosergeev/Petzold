using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace AlphabetBlocks
{
    public sealed partial class Block : UserControl
    {
        static int zindex;

        public static DependencyProperty TextProperty { get; private set; }

        static Block()
        {
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(Block), new PropertyMetadata("?"));
        }
        
        public Block()
        {
            this.InitializeComponent();
        }

        public static int ZIndex
        {
            get { return ++zindex; }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private void OnThumbDragStarted(object sender, DragStartedEventArgs e)
        {
            Canvas.SetZIndex(this, ZIndex);
        }

        private void OnThumbDragDelta(object sender, DragDeltaEventArgs args)
        {
            Canvas.SetLeft(this, Canvas.GetLeft(this) + args.HorizontalChange);
            Canvas.SetTop(this, Canvas.GetTop(this) + args.VerticalChange);
        }
    }
}
