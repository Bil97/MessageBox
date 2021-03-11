using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ThingLing.Avalonia.Controls;

namespace TestApp
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        async private void Button_Click(object? sender, global::Avalonia.Interactivity.RoutedEventArgs e)
        {
            var mb = new MessageBox("Hello world, this message box is working fine", "Hello title", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            await mb.ShowDialog(this);
            var res = mb.MessageBoxResult;
            this.FindControl<TextBlock>("result").Text = res.ToString();

        }
    }
}
