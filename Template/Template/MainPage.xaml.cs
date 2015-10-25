using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Template
{
    /// <summary>
    /// Template for making navigatable universal app.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public const string FEATURE_NAME = "Hyperloop";
        public static MainPage Current;
        List<Scenario> scenarios = new List<Scenario>();
        List<Configuration> configs = new List<Configuration>();
        int configIndex;

        public MainPage()
        {
            this.InitializeComponent();
            scenarios.Add(new Scenario() { Title = "Welcome!", ClassType = typeof(Scenario1) });
            scenarios.Add(new Scenario() { Title = "Netflix", ClassType = typeof(Scenario2) });
            scenarios.Add(new Scenario() { Title = "Spotify", ClassType = typeof(Scenario3) });
            scenarios.Add(new Scenario() { Title = "Facebook", ClassType = typeof(Scenario4) });
            scenarios.Add(new Scenario() { Title = "Twitter", ClassType = typeof(Scenario5) });
            //Public property that allows other classes to access/control this page.
            Current = this;
            SampleTitle.Text = FEATURE_NAME;
            etaText.Text = "3 hours";
            transportRouteText.Text = "Saint Louis";
            ApplicationView.GetForCurrentView().TitleBar.BackgroundColor=Colors.DarkGreen;
            ApplicationView.GetForCurrentView().TitleBar.ForegroundColor = Colors.White;
            ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
        }

        //Used with Back button to keep track of all the user's old and current movements/settings
        public void saveLastConfig()
        {
            configs.Add(new Configuration() { LastScenario = ScenarioFrame.CurrentSourcePageType });
            configIndex = configs.Count - 1;
        }

        //When Back button is clicked, this will load the last config.
        public void implementLastConfig()
        {
            if(configIndex>0)
            {
                ScenarioFrame.Navigate(configs[configIndex].LastScenario);
                for(int i=0; i<scenarios.Count; i++)
                {
                    if(scenarios[i].ClassType.Equals(ScenarioFrame.CurrentSourcePageType))
                    {
                       ScenarioControl.SelectedIndex = i;
                    } 
                }
            }
            configIndex -= 1;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Populate the scenario list from the SampleConfiguration.cs file
            ScenarioControl.ItemsSource = scenarios;
            saveLastConfig();
            if (Window.Current.Bounds.Width < 640)
            {
                ScenarioControl.SelectedIndex = -1;
            }
            else
            {
                ScenarioControl.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Called whenever the user changes selection in the scenarios list.  This method will navigate to the respective
        /// sample scenario page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScenarioControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          ScenarioFrame.Navigate(scenarios[ScenarioControl.SelectedIndex].ClassType);
        }

        public void changeToNextScenario()
        {
            //  saveLastConfig();
            for (int i = 0; i < scenarios.Count; i++)
            {
                if (scenarios[i].Equals(ScenarioControl.SelectedItem as Scenario))
                {
                    ScenarioFrame.Navigate(scenarios[i + 1].ClassType);
                }
            }
            ScenarioControl.SelectedIndex += 1;
        }

        public List<Scenario> Scenarios
        {
            get { return this.scenarios; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Splitter.IsPaneOpen = (Splitter.IsPaneOpen == true) ? false : true;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            implementLastConfig();
        }

       

    }

    public enum NotifyType
    {
        StatusMessage,
        ErrorMessage
    };

    public class ScenarioBindingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Scenario s = value as Scenario;
            return (MainPage.Current.Scenarios.IndexOf(s) + 1) + ") " + s.Title;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return true;
        }
    }
}
