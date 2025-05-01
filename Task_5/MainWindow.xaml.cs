using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Color(object sender, RoutedEventArgs e)
        {
            Background = Background == Brushes.DarkOliveGreen ? Brushes.Pink : Brushes.DarkOliveGreen;
        }

        private void Button_Opacity(object sender, RoutedEventArgs e)
        {
            Opacity += Opacity > 0.5 ? -0.5 : 0.5;
        }

        private void Button_Click_Hello(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello World.");
        }

        private void CheckBox_Color(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                if (checkBox.IsChecked == true)
                    superButton.Click += Button_Color;
                else
                    superButton.Click -= Button_Color;
            }
        }

        private void CheckBox_Opacity(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                if (checkBox.IsChecked == true)
                    superButton.Click += Button_Opacity;
                else
                    superButton.Click -= Button_Opacity;
            }
        }

        private void CheckBox_Hello(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                if (checkBox.IsChecked == true)
                    superButton.Click += Button_Click_Hello;
                else
                    superButton.Click -= Button_Click_Hello;
            }
        }

        private void Button_Super(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Я суперкнопка,\nі мене цього не позбавиш!");
        }

        private void Mouse_Down(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
