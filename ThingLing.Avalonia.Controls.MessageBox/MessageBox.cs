using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Threading.Tasks;

namespace ThingLing.Avalonia.Controls
{
    /// <summary>
    /// Displays a message box.
    /// </summary>
    public class MessageBox
    {
        static private readonly double uniformThickness = 2;
        static private MessageBoxResult MessageBoxResult { get; set; }
        static Button okButton;
        static Button yesButton;
        static Button noButton;
        static Button cancelButton;

        static Image errorImage;
        static Image stopImage;
        static Image warningImage;
        static Image informationImage;

        static Window window;

        static private void MessageBoxItems(string message, string title = "")
        {
            #region Content panel
            errorImage = new Image
            {
                Height = 25,
                Width = 25,
                Margin = new Thickness(5, 0),
                IsVisible = false,
                Source = LoadBitmap("avares://ThingLing.Avalonia.Controls.MessageBox/Images/delete.png")
            };
            stopImage = new Image
            {
                Height = 25,
                Width = 25,
                Margin = new Thickness(5, 0),
                IsVisible = false,
                Source = LoadBitmap("avares://ThingLing.Avalonia.Controls.MessageBox/Images/No-entry.png")
            };
            warningImage = new Image
            {
                Height = 25,
                Width = 25,
                Margin = new Thickness(5, 0),
                IsVisible = false,
                Source = LoadBitmap("avares://ThingLing.Avalonia.Controls.MessageBox/Images/Warning.png")
            };
            informationImage = new Image
            {
                Height = 25,
                Width = 25,
                Margin = new Thickness(5, 0),
                IsVisible = false,
                Source = LoadBitmap("avares://ThingLing.Avalonia.Controls.MessageBox/Images/Info.png")
            };

            var messageTextBlock = new TextBlock
            {
                Margin = new Thickness(5),
                Text = message,
                TextWrapping = TextWrapping.Wrap
            };

            var contentPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10)
            };
            contentPanel.Children.Add(errorImage);
            contentPanel.Children.Add(stopImage);
            contentPanel.Children.Add(warningImage);
            contentPanel.Children.Add(informationImage);
            contentPanel.Children.Add(messageTextBlock);
            #endregion

            #region MessageBox buttons panel
            okButton = new Button
            {
                Content = "OK",
                Margin = new Thickness(5),
                Width = 75,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                IsDefault = true,
                BorderBrush = Brushes.Red,
                BorderThickness = new Thickness(0),
            };
            okButton.GotFocus += Button_GotFocus;
            okButton.LostFocus += Button_LostFocus;
            Grid.SetColumn(okButton, 1);

            yesButton = new Button
            {
                Content = "Yes",
                Margin = new Thickness(5),
                Width = 75,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                IsDefault = true,
                BorderBrush = Brushes.Red,
                BorderThickness = new Thickness(0),
                IsVisible = false
            };
            yesButton.GotFocus += Button_GotFocus;
            yesButton.LostFocus += Button_LostFocus;
            Grid.SetColumn(yesButton, 2);

            noButton = new Button
            {
                Content = "No",
                Margin = new Thickness(5),
                Width = 75,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                IsDefault = true,
                BorderBrush = Brushes.Red,
                BorderThickness = new Thickness(0),
                IsVisible = false
            };
            noButton.GotFocus += Button_GotFocus;
            noButton.LostFocus += Button_LostFocus;
            Grid.SetColumn(noButton, 3);

            cancelButton = new Button
            {
                Content = "Cancel",
                Margin = new Thickness(5),
                Width = 75,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                IsDefault = true,
                BorderBrush = Brushes.Red,
                BorderThickness = new Thickness(0),
                IsVisible = false
            };
            cancelButton.GotFocus += Button_GotFocus;
            cancelButton.LostFocus += Button_LostFocus;
            Grid.SetColumn(cancelButton, 4);

            var messageBoxButtonsPanel = new Grid
            {
                ColumnDefinitions = new ColumnDefinitions
                {
                    new ColumnDefinition{Width=new GridLength(1,GridUnitType.Star)},
                    new ColumnDefinition{Width=new GridLength(1,GridUnitType.Auto)},
                    new ColumnDefinition{Width=new GridLength(1,GridUnitType.Auto)},
                    new ColumnDefinition{Width=new GridLength(1,GridUnitType.Auto)},
                    new ColumnDefinition{Width=new GridLength(1,GridUnitType.Auto)},
                    new ColumnDefinition{Width=new GridLength(1,GridUnitType.Star)}
                }

            };
            DockPanel.SetDock(messageBoxButtonsPanel, Dock.Bottom);
            messageBoxButtonsPanel.Children.Add(okButton);
            messageBoxButtonsPanel.Children.Add(yesButton);
            messageBoxButtonsPanel.Children.Add(noButton);
            messageBoxButtonsPanel.Children.Add(cancelButton);
            #endregion

            #region Header panel
            var closeButtonImage = new Image
            {
                Source = LoadBitmap("avares://ThingLing.Avalonia.Controls.MessageBox/Images/close.png"),
                Height = 20
            };
            var closeButton = new Button
            {
                Background = Brushes.Transparent,
                Content = closeButtonImage,
                Focusable = false
            };
            ToolTip.SetTip(closeButton, "Close");
            DockPanel.SetDock(closeButton, Dock.Right);

            var titleTextBlock = new TextBlock
            {
                Padding = new Thickness(2, 10),
                Text = title
            };

            var headerPanel = new DockPanel { Background = Brushes.LightGray };
            DockPanel.SetDock(headerPanel, Dock.Top);
            headerPanel.Children.Add(closeButton);
            headerPanel.Children.Add(titleTextBlock);
            #endregion

            #region Main panel
            var mainPanel = new DockPanel();
            mainPanel.Children.Add(headerPanel);
            mainPanel.Children.Add(messageBoxButtonsPanel);
            mainPanel.Children.Add(contentPanel);
            #endregion

            #region Window border
            var border = new Border
            {
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.Gray,
                Child = mainPanel
            };

            #endregion

            #region Window
            window = new Window
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                SizeToContent = SizeToContent.WidthAndHeight,
                CanResize = false,
                SystemDecorations = SystemDecorations.BorderOnly,
                ShowInTaskbar = false,
                Content = border
            };
            window.KeyUp += (sender, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    MessageBoxResult = MessageBoxResult.None;
                    window.Close();
                }
                if (e.Key == Key.Enter)
                {
                    window.Close();
                }
            };

            headerPanel.PointerPressed += (sender, e) => { window.BeginMoveDrag(e); };
            closeButton.Click += (sender, e) => { MessageBoxResult = MessageBoxResult.None; window.Close(); };
            okButton.Click += (sender, e) => { MessageBoxResult = MessageBoxResult.OK; window.Close(); };
            yesButton.Click += (sender, e) => { MessageBoxResult = MessageBoxResult.Yes; window.Close(); };
            noButton.Click += (sender, e) => { MessageBoxResult = MessageBoxResult.No; window.Close(); };
            cancelButton.Click += (sender, e) => { MessageBoxResult = MessageBoxResult.Cancel; window.Close(); };

            #endregion
        }

        static private IBitmap LoadBitmap(string uri)
        {
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            return new Bitmap(assets.Open(new Uri(uri)));
        }

        static private void Button_LostFocus(object? sender, RoutedEventArgs e)
        {
            (sender as Button).BorderThickness = new Thickness(0);
        }

        static private void Button_GotFocus(object? sender, GotFocusEventArgs e)
        {
            (sender as Button).BorderThickness = new Thickness(uniformThickness);
        }

        /// <summary>
        ///  Displays a message box that has a message and that returns a result.
        /// </summary>
        /// <param name="message">
        /// A System.String that specifies the text to display.
        /// </param>
        /// <returns>A ThingLing.Avalonia.Controls.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        async static public Task<MessageBoxResult> ShowAsync(Window owner, string message)
        {
            MessageBoxItems(message);
            await window.ShowDialog(owner);
            return MessageBoxResult;
        }

        /// <summary>
        /// Displays a message box that has a message and title bar caption; and that returns a result.
        /// </summary>
        /// <param name="message">A System.String that specifies the text to display.</param>
        /// <param name="title">A System.String that specifies the title bar caption to display.</param>
        /// <returns>A ThingLing.Avalonia.Controls.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>

        async static public Task<MessageBoxResult> ShowAsync(Window owner, string message, string title)
        {
            MessageBoxItems(message, title);
            await window.ShowDialog(owner);
            return MessageBoxResult;
        }

        static private void MessageBoxButtonMethod(MessageBoxButton button)
        {
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

        static private void MessageBoxImageMethod(MessageBoxImage icon)
        {
            switch (icon)
            {
                case MessageBoxImage.None:
                    break;
                case MessageBoxImage.Error:
                    errorImage.IsVisible = true;
                    break;
                case MessageBoxImage.Stop:
                    stopImage.IsVisible = true;
                    break;
                case MessageBoxImage.Warning:
                    warningImage.IsVisible = true;
                    break;
                case MessageBoxImage.Information:
                    informationImage.IsVisible = true;
                    break;
            }

        }

        static private void MessageBoxResultMethod(MessageBoxResult defaultResult)
        {
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

        /// <summary>
        /// Displays a message box that has a message and title bar caption; and that returns a result.
        /// </summary>
        /// <param name="message">A System.String that specifies the text to display.</param>
        /// <param name="title">A System.String that specifies the title bar caption to display.</param>
        /// <param name="button"> A ThingLing.Avalonia.Controls.MessageBoxButton value that specifies which button or buttons to display</param>
        /// <returns>A ThingLing.Avalonia.Controls.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>

        async static public Task<MessageBoxResult> ShowAsync(Window owner, string message, string title, MessageBoxButton button)
        {
            MessageBoxItems(message, title);

            okButton.IsVisible = false;
            MessageBoxButtonMethod(button);

            await window.ShowDialog(owner);
            return MessageBoxResult;
        }

        /// <summary>
        /// Displays a message box that has a message and title bar caption; and that returns a result.
        /// </summary>
        /// <param name="message">A System.String that specifies the text to display.</param>
        /// <param name="title">A System.String that specifies the title bar caption to display.</param>
        /// <param name="button"> A ThingLing.Avalonia.Controls.MessageBoxButton value that specifies which button or buttons to display</param>
        /// <param name="icon"> A ThingLing.Avalonia.Controls.MessageBoxImage value that specifies the icon to display.</param>
        /// <returns>A ThingLing.Avalonia.Controls.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>

        async static public Task<MessageBoxResult> ShowAsync(Window owner, string message, string title, MessageBoxButton button, MessageBoxImage icon)
        {
            MessageBoxItems(message, title);

            okButton.IsVisible = false;
            MessageBoxButtonMethod(button);
            MessageBoxImageMethod(icon);

            await window.ShowDialog(owner);
            return MessageBoxResult;
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
        async static public Task<MessageBoxResult> ShowAsync(Window owner, string message, string title, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            MessageBoxItems(message, title);

            okButton.IsVisible = false;
            MessageBoxButtonMethod(button);
            MessageBoxImageMethod(icon);

            okButton.IsDefault = false;

            MessageBoxResultMethod(defaultResult);

            await window.ShowDialog(owner);
            return MessageBoxResult;
        }
    }
}
