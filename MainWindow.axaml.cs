using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Threading.Tasks;

namespace ThingLing.Avalonia.Controls
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            MessageBoxResult result;
            var task = new Task(async () =>
              {
                  result = await MessageBox.ShowAsync(this, "Hello world message", "Title", MessageBoxButton.YesNoCancel,MessageBoxImage.Warning);

                  this.Content = result;
              });
            task.RunSynchronously();

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
