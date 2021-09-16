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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CoffeeShop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CoffeePage : Page
    {
        string roast = "None";
        string sweetner = "None";
        string cream = "None";

        public CoffeePage()
        {
            this.InitializeComponent();
        }

        private void RefreshInfo()
        {
            string info = "";

            if (roast != "None")
                info = roast;

            if (sweetner != "None")
                info = info + (info != "" ? " + " : "") + sweetner;

            if( cream != "None" )
                info = info + (info != "" ? " + " : ""  ) + cream;

            CoffeeTextBlock.Text = info;
        }

        private void NoCreamButton_Click(object sender, RoutedEventArgs e)
        {
            cream = "None";
            CreamButton.Flyout.Hide();
            RefreshInfo();
        }

        private void SkimCreamButton_Click(object sender, RoutedEventArgs e)
        {
            cream = "2% Milk";
            CreamButton.Flyout.Hide();
            RefreshInfo();
        }

        private void WholeCreamButton_Click(object sender, RoutedEventArgs e)
        {
            cream = "Whole Milk";
            CreamButton.Flyout.Hide();
            RefreshInfo();
        }

        private void Roast_Click(object sender, RoutedEventArgs e)
        {
            roast = (sender as MenuFlyoutItem).Text;
            RefreshInfo();
        }

        private void Sweetner_Click(object sender, RoutedEventArgs e)
        {
            sweetner = (sender as MenuFlyoutItem).Text;
            RefreshInfo();
        }
    }
}
