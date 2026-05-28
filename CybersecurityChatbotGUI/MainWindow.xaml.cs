using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CybersecurityChatbotGUI
{
    public partial class MainWindow : Window
    {
        private ChatBot _chatBot;

        public MainWindow()
        {
            InitializeComponent();

            _chatBot = new ChatBot();

            PlayVoiceGreeting();
            AppendBotMessage(_chatBot.GetGreeting());
        }

        private void PlayVoiceGreeting()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
                if (File.Exists(path))
                {
                    SoundPlayer player = new SoundPlayer(path);
                    player.Play();
                }
            }
            catch (Exception ex)
            {
                StatusBar.Text = "Audio note: " + ex.Message;
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SendMessage();
        }

        private void TopicButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            string topic = btn.Tag.ToString();
            UserInput.Text = topic;
            SendMessage();
        }

        private void SendMessage()
        {
            string input = UserInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            AppendUserMessage(input);
            UserInput.Text = string.Empty;
            UserInput.Foreground = Brushes.White;

            string response = _chatBot.ProcessInput(input);
            AppendBotMessage(response);

            StatusBar.Text = "SecureNet replied • " + DateTime.Now.ToString("HH:mm:ss");
        }

        private void AppendUserMessage(string text)
        {
            var container = new Grid { Margin = new Thickness(60, 6, 8, 6) };
            container.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            });

            var bubble = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(0x23, 0x8B, 0xE6)),
                CornerRadius = new CornerRadius(14, 14, 2, 14),
                Padding = new Thickness(14, 10, 14, 10),
                HorizontalAlignment = HorizontalAlignment.Right,
                MaxWidth = 480
            };

            var label = new TextBlock
            {
                Text = text,
                Foreground = Brushes.White,
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 13,
                TextWrapping = TextWrapping.Wrap
            };

            bubble.Child = label;
            Grid.SetColumn(bubble, 0);
            container.Children.Add(bubble);
            ChatPanel.Children.Add(container);
            ScrollToBottom();
        }

        private void AppendBotMessage(string text)
        {
            var nameLabel = new TextBlock
            {
                Text = "SECURENET",
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromRgb(0x00, 0xD4, 0xFF)),
                Margin = new Thickness(12, 8, 0, 2)
            };
            ChatPanel.Children.Add(nameLabel);

            var container = new Grid { Margin = new Thickness(8, 0, 60, 4) };
            container.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            });

            var bubble = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(0x16, 0x1B, 0x22)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(0x21, 0x26, 0x2D)),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(2, 14, 14, 14),
                Padding = new Thickness(14, 10, 14, 10),
                HorizontalAlignment = HorizontalAlignment.Left,
                MaxWidth = 480
            };

            var label = new TextBlock
            {
                Text = text,
                Foreground = new SolidColorBrush(Color.FromRgb(0xE6, 0xED, 0xF3)),
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 13,
                TextWrapping = TextWrapping.Wrap,
                LineHeight = 20
            };

            bubble.Child = label;
            Grid.SetColumn(bubble, 0);
            container.Children.Add(bubble);
            ChatPanel.Children.Add(container);
            ScrollToBottom();
        }

        private void ScrollToBottom()
        {
            ChatScrollViewer.UpdateLayout();
            ChatScrollViewer.ScrollToEnd();
        }
    }
}