using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HamburgerTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            MyFrame.Navigate(typeof(Financial));
        }

        private void hamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(FinancialItem.IsSelected)
            {
                MyFrame.Navigate(typeof(Financial));
                Title.Text = "Financial";
                BackIcon.Visibility = Visibility.Collapsed;
            }
            else if(FoodItem.IsSelected)
            {
                MyFrame.Navigate(typeof(Food));
                Title.Text = "Food";
                BackIcon.Visibility = Visibility.Visible;
            }
        }

        private void BackIcon_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(Financial));
            Title.Text = "Financial";
            BackIcon.Visibility = Visibility.Collapsed;
        }
    }
}
