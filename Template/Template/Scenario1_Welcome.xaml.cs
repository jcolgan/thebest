using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.IO;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Template
{
    public sealed partial class Scenario1 : Page
    {
        MainPage rootPage = MainPage.Current;

        public Scenario1()
        {
            this.InitializeComponent();
        }

        private void continueBtn_Click(object sender, RoutedEventArgs e)
        {
            rootPage.changeToNextScenario();
        }
    }
}
