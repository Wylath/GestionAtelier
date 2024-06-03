using GestionAtelier;
using GestionAtelier.Model;
using GestionAtelier.View;
using GestionAtelier.ViewModel;
using System;
using System.Windows;

namespace GestionAtelier.Toolbox
{
    class SwitchWindows
    {
        public void ChangeViewWindows(string nameWindow, UserProfile parameterId)
        {
            Window wd = Application.Current.MainWindow;

            switch(nameWindow)
            {
                case "AllIntervention":
                    AllIntervention inter = new AllIntervention();
                    wd.Title = "Gestion Garage : " + CurrentUser.ParmSite.Name + ", Mode : " + parameterId.Name;
                    inter.DataContext = new AllInterventionViewModel(parameterId);
                    wd.Content = inter;
                    break;
                case "AllInterventionAdvanced":
                    AllInterventionAdvanced interventionAdv = new AllInterventionAdvanced();
                    wd.Title = "Gestion Garage : " + CurrentUser.ParmSite.Name + ", Mode : " + parameterId.Name;
                    interventionAdv.DataContext = new AllInterventionViewModel(parameterId);
                    wd.Content = interventionAdv;
                    break;
                case "AllInvoice":
                    AllInvoice allInvoice = new AllInvoice();
                    wd.Title = "Gestion Garage : " + CurrentUser.ParmSite.Name + ", Mode : " + parameterId.Name;
                    wd.Content = allInvoice;
                    break;
                case "MainWindow":
                    MainWindow main = new MainWindow();
                    main.DataContext = new AllUserViewModel();
                    wd.Title = "Gestion Garage";
                    wd.Content = main.Content;
                    break;
                default:
                    break;
            }
        }
    }
}
