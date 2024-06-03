using GestionAtelier.ViewModel;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using Examen2018.Toolbox;

namespace GestionAtelier
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new AllInterventionViewModel();
            Height = SystemParameters.WorkArea.Height;
            Width = SystemParameters.WorkArea.Width;
            DispatcherTimer dtClockTime = new DispatcherTimer();
            int hour = 0;
            int minutes = 0;
            int secondes = 0;
            hour = Convert.ToInt32(ConfigurationManager.AppSettings["TimeHourCloseApplication"].ToString());
            minutes = Convert.ToInt32(ConfigurationManager.AppSettings["TimeMinuteCloseApplication"].ToString());
            secondes = Convert.ToInt32(ConfigurationManager.AppSettings["TimeSecondeCloseApplication"].ToString());
            if (hour > 0 || minutes > 0 || secondes > 0)
            {
                dtClockTime.Interval = new TimeSpan(hour, minutes, secondes); //in Hour, Minutes, Second.
                dtClockTime.Tick += dtClockTime_Tick;

                dtClockTime.Start();
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = this;
        }

        private void dtClockTime_Tick(object sender, EventArgs e)
        {
            CurrentUser.IsActif = (!CurrentUser.IsActif) ? true : false;

            if (CurrentUser.IsActif && CurrentUser.CheckAuthorized)
            {
                CurrentUser.UserProfile = null;
                CurrentUser.ParmSite = null;
                CurrentUser.employeeId = "";
                SwitchWindows sw = new SwitchWindows();
                sw.ChangeViewWindows("MainWindow", null);
                //MessageBox.Show("Tick :" + DateTime.Now.ToLongTimeString());
            }
        }
    }
}
