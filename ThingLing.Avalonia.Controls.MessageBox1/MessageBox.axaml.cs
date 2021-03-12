using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace ThingLing.Avalonia.Controls
{
    public class MessageBox : Window
    {
        private readonly double uniformThickness = 2;

        /// <summary>
        /// Holds the result returned by the ThingLing.Avalonia.Controls.MessageBox. Must appear after the message box is shown.
        /// </summary>
        /// <returns>
        /// A ThingLing.Avalonia.Controls.MessageBoxResult value that specifies which message box button is clicked by the user.
        /// </returns>
        public MessageBoxResult MessageBoxResult { get; private set; }

        /// <summary>
        /// Displays an empty message box.
        /// </summary>
        public MessageBox()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        /// <summary>
        ///  Displays a message box that has a message and that returns a result.
        /// </summary>
        /// <param name="message">
        /// A System.String that specifies the text to display.
        /// </param>
        /// <returns>A ThingLing.Avalonia.Controls.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public MessageBox(string message)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var messageTextBlock = this.FindControl<TextBlock>("MessageTextBlock");
            messageTextBlock.Text = message;
        }

        /// <summary>
        /// Displays a message box that has a message and title bar caption; and that returns a result.
        /// </summary>
        /// <param name="message">A System.String that specifies the text to display.</param>
        /// <param name="title">A System.String that specifies the title bar caption to display.</param>
        /// <returns>A ThingLing.Avalonia.Controls.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public MessageBox(string message, string title)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var titleTextBlock = this.FindControl<TextBlock>("TitleTextBlock");
            titleTextBlock.Text = title;

            var messageTextBlock = this.FindControl<TextBlock>("MessageTextBlock");
            messageTextBlock.Text = message;
        }

        /// <summary>
        /// Displays a message box that has a message and title bar caption; and that returns a result.
        /// </summary>
        /// <param name="message">A System.String that specifies the text to display.</param>
        /// <param name="title">A System.String that specifies the title bar caption to display.</param>
        /// <param name="button"> A ThingLing.Avalonia.Controls.MessageBoxButton value that specifies which button or buttons to display</param>
        /// <returns>A ThingLing.Avalonia.Controls.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public MessageBox(string message, string title, MessageBoxButton button)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var titleTextBlock = this.FindControl<TextBlock>("TitleTextBlock");
            titleTextBlock.Text = title;

            var messageTextBlock = this.FindControl<TextBlock>("MessageTextBlock");
            messageTextBlock.Text = message;

            var okButton = this.FindControl<Button>("OKButton");
            var yesButton = this.FindControl<Button>("YesButton");
            var noButton = this.FindControl<Button>("NoButton");
            var cancelButton = this.FindControl<Button>("CancelButton");
            okButton.IsVisible = false;
            switch (button)
            {
                case MessageBoxButton.OK:
                    okButton.IsVisible = true;
                    break;
                case MessageBoxButton.YesNo:
                    yesButton.IsVisible = true;
                    noButton.IsVisible = true;
                    break;
                case MessageBoxButton.YesNoCancel:
                    yesButton.IsVisible = true;
                    noButton.IsVisible = true;
                    cancelButton.IsVisible = true;
                    break;
                case MessageBoxButton.OKCancel:
                    cancelButton.IsVisible = true;
                    okButton.IsVisible = true;
                    break;
            }
        }

        /// <summary>
        /// Displays a message box that has a message and title bar caption; and that returns a result.
        /// </summary>
        /// <param name="message">A System.String that specifies the text to display.</param>
        /// <param name="title">A System.String that specifies the title bar caption to display.</param>
        /// <param name="button"> A ThingLing.Avalonia.Controls.MessageBoxButton value that specifies which button or buttons to display</param>
        /// <param name="icon"> A ThingLing.Avalonia.Controls.MessageBoxImage value that specifies the icon to display.</param>
        /// <returns>A ThingLing.Avalonia.Controls.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public MessageBox(string message, string title, MessageBoxButton button, MessageBoxImage icon)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var titleTextBlock = this.FindControl<TextBlock>("TitleTextBlock");
            titleTextBlock.Text = title;

            var messageTextBlock = this.FindControl<TextBlock>("MessageTextBlock");
            messageTextBlock.Text = message;

            var okButton = this.FindControl<Button>("OKButton");
            var yesButton = this.FindControl<Button>("YesButton");
            var noButton = this.FindControl<Button>("NoButton");
            var cancelButton = this.FindControl<Button>("CancelButton");
            okButton.IsVisible = false;
            switch (button)
            {
                case MessageBoxButton.OK:
                    okButton.IsVisible = true;
                    break;
                case MessageBoxButton.YesNo:
                    yesButton.IsVisible = true;
                    noButton.IsVisible = true;
                    break;
                case MessageBoxButton.YesNoCancel:
                    yesButton.IsVisible = true;
                    noButton.IsVisible = true;
                    cancelButton.IsVisible = true;
                    break;
                case MessageBoxButton.OKCancel:
                    cancelButton.IsVisible = true;
                    okButton.IsVisible = true;
                    break;
            }
            switch (icon)
            {
                case MessageBoxImage.None:
                    break;
                case MessageBoxImage.Error:
                    var errorImage = this.FindControl<Image>("ErrorImage");
                    errorImage.IsVisible = true;
                    break;
                case MessageBoxImage.Stop:
                    var stopImage = this.FindControl<Image>("StopImage");
                    stopImage.IsVisible = true;
                    break;
                case MessageBoxImage.Warning:
                    var warningImage = this.FindControl<Image>("WarningImage");
                    warningImage.IsVisible = true;
                    break;
                case MessageBoxImage.Information:
                    var informationImage = this.FindControl<Image>("InformationImage");
                    informationImage.IsVisible = true;
                    break;
            }
        }

        /// <summary>
        /// Displays a message box that has a message and title bar caption; and that returns a result.
        /// </summary>
        /// <param name="message">A System.String that specifies the text to display.</param>
        /// <param name="title">A System.String that specifies the title bar caption to display.</param>
        /// <param name="button"> A ThingLing.Avalonia.Controls.MessageBoxButton value that specifies which button or buttons to display</param>
        /// <param name="icon"> A ThingLing.Avalonia.Controls.MessageBoxImage value that specifies the icon to display.</param>
        /// <param name="defaultResult"> A ThingLing.Avalonia.Controls.MessageBoxResult value that specifies the default result of the message box.</param>
        /// <returns>A ThingLing.Avalonia.Controls.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public MessageBox(string message, string title, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var titleTextBlock = this.FindControl<TextBlock>("TitleTextBlock");
            titleTextBlock.Text = title;

            var messageTextBlock = this.FindControl<TextBlock>("MessageTextBlock");
            messageTextBlock.Text = message;

            var yesButton = this.FindControl<Button>("YesButton");
            var noButton = this.FindControl<Button>("NoButton");
            var cancelButton = this.FindControl<Button>("CancelButton");
            var okButton = this.FindControl<Button>("OKButton");
            okButton.IsVisible = false;
            switch (button)
            {
                case MessageBoxButton.OK:
                    okButton.IsVisible = true;
                    break;
                case MessageBoxButton.YesNo:
                    yesButton.IsVisible = true;
                    noButton.IsVisible = true;
                    break;
                case MessageBoxButton.YesNoCancel:
                    yesButton.IsVisible = true;
                    noButton.IsVisible = true;
                    cancelButton.IsVisible = true;
                    break;
                case MessageBoxButton.OKCancel:
                    cancelButton.IsVisible = true;
                    okButton.IsVisible = true;
                    break;
            }
            switch (icon)
            {
                case MessageBoxImage.None:
                    break;
                case MessageBoxImage.Error:
                    var errorImage = this.FindControl<Image>("ErrorImage");
                    errorImage.IsVisible = true;
                    break;
                case MessageBoxImage.Stop:
                    var stopImage = this.FindControl<Image>("StopImage");
                    stopImage.IsVisible = true;
                    break;
                case MessageBoxImage.Warning:
                    var warningImage = this.FindControl<Image>("WarningImage");
                    warningImage.IsVisible = true;
                    break;
                case MessageBoxImage.Information:
                    var informationImage = this.FindControl<Image>("InformationImage");
                    informationImage.IsVisible = true;
                    break;
            }

            okButton.IsDefault = false;

            switch (defaultResult)
            {
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.OK:
                    okButton.IsDefault = true;
                    okButton.BorderThickness = new Thickness(uniformThickness);
                    okButton.Focus();
                    break;
                case MessageBoxResult.Cancel:
                    cancelButton.IsDefault = true;
                    cancelButton.BorderThickness = new Thickness(uniformThickness);
                    cancelButton.Focus();
                    break;
                case MessageBoxResult.Yes:
                    yesButton.IsDefault = true;
                    yesButton.BorderThickness = new Thickness(uniformThickness);
                    break;
                case MessageBoxResult.No:
                    noButton.IsDefault = true;
                    noButton.BorderThickness = new Thickness(uniformThickness);
                    break;
                default:
                    okButton.IsDefault = true;
                    okButton.BorderThickness = new Thickness(uniformThickness);
                    break;
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.None;
            Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.OK;
            Close();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.Yes;
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.No;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.Cancel;
            Close();
        }

        private void Header_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            BeginMoveDrag(e);
        }
        private void Window_KeyUp(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MessageBoxResult = MessageBoxResult.None;
                Close();
            }
            if (e.Key == Key.Enter)
            {
                Close();
            }

        }

        private void Button_LostFocus(object? sender, RoutedEventArgs e)
        {
            (sender as Button).BorderThickness = new Thickness(0);
        }

        private void Button_GotFocus(object? sender, GotFocusEventArgs e)
        {
            (sender as Button).BorderThickness = new Thickness(2);
        }
    }
}
