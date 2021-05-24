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
using Windows.UI.ViewManagement;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace notePad1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            
            // App Customization
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            ApplicationView appView = ApplicationView.GetForCurrentView();
            appView.Title = "Untitled Text - Grant's Notepad";

            // Set Active Window Colors
            titleBar.BackgroundColor = Windows.UI.Colors.DarkBlue;
            titleBar.ForegroundColor = Windows.UI.Colors.White;
            txtBox1.Background = new SolidColorBrush(Windows.UI.Colors.Black);
            txtBox1.Foreground = new SolidColorBrush(Windows.UI.Colors.White);

        }
    }
}
