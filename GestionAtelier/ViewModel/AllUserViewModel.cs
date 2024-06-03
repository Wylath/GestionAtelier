using GestionAtelier.DB;
using GestionAtelier.Model;
using GestionAtelier.ToolBox;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace GestionAtelier.ViewModel
{
    class AllUserViewModel : ObservableObject
    {
        RequestDBUser RU = new RequestDBUser();
        RequestDBParmSite RPS = new RequestDBParmSite();

        const string ErrorMessageLogin = "Pas d'utilisateur windows trouvé.";
        const string ErrorMessageUserProfile = "Aucun profile utilisateur correspondant n'a été trouvé.\nLe nom de profile doit être 'Responsable', 'Garage', 'Administrateur' ou 'Compta'.";
        const string ErrorMessageUserNotFound = "Aucun utilisateur correspondant à votre session n'a été trouvé.";
        const string ErrorMessageUserNotActive = "Votre compte utilisateur n'est plus actif.\nVeuillez contacter l'administrateur.";
        const string ErrorTitleLogin = "Erreur interface utilisateur";
        const string ErrorTitleUserProfile = "Erreur profile utilisateur";
        const string ErrorTitleUserNotFound = "Erreur utilisateur";
        const string ErrorTitleUserNotActive = "Utilisateur inactif";

        static string method = "";
        static int line = 0;

        enum UserProfileEnum
        {
            Garage                  = 1,
            Responsable             = 2,
            Compta                  = 3,
            Administrateur          = 4,
        }

        private static void ShowDebugInfo()
        {
            StackFrame stackFrame = new StackFrame(1, true);

            method = stackFrame.GetMethod().ToString();
            line = stackFrame.GetFileLineNumber();
        }

        private readonly ObservableCollection<DetailViewModel<UserModel>> _User;

        public ObservableCollection<DetailViewModel<UserModel>> User
        {
            get
            {
                return _User;
            }
        }

        private readonly ObservableCollection<DetailViewModel<ParmSiteModel>> _ParmSite;

        public ObservableCollection<DetailViewModel<ParmSiteModel>> ParmSite
        {
            get
            {
                return _ParmSite;
            }
        }

        private readonly ObservableCollection<DetailViewModel<UserProfileModel>> _UserProfile;

        public ObservableCollection<DetailViewModel<UserProfileModel>> UserProfile
        {
            get
            {
                return _UserProfile;
            }
        }

        private DetailViewModel<UserModel> _SelectedUserItem;

        public DetailViewModel<UserModel> SelectedUserItem
        {
            get
            {
                return _SelectedUserItem;
            }
            set
            {
                if (value != _SelectedUserItem)
                {
                    _SelectedUserItem = value;
                    RaisePropertyChanged(() => SelectedUserItem);
                }
            }
        }

        private DetailViewModel<UserProfileModel> _SelectedUserProfileItem;

        public DetailViewModel<UserProfileModel> SelectedUserProfileItem
        {
            get
            {
                return _SelectedUserProfileItem;
            }
            set
            {
                if (value != _SelectedUserProfileItem)
                {
                    _SelectedUserProfileItem = value;
                    if (_SelectedUserProfileItem != null)
                    {
                        CurrentUser.UserProfile = new UserProfile(SelectedUserProfileItem.Detail.UserProfileId, SelectedUserProfileItem.Detail.Name, SelectedUserProfileItem.Detail.ProfileLevel, SelectedUserProfileItem.Detail.SiteId);
                        CurrentUser.ParmSite = new ParmSite(SelectedUserProfileItem.Detail.SiteId.SiteId, SelectedUserProfileItem.Detail.SiteId.Name);
                    }
                    RaisePropertyChanged(() => SelectedUserProfileItem);
                }
            }
        }

        public AllUserViewModel()
        {
            _User = new ObservableCollection<DetailViewModel<UserModel>>();
            _ParmSite = new ObservableCollection<DetailViewModel<ParmSiteModel>>();
            _UserProfile = new ObservableCollection<DetailViewModel<UserProfileModel>>();
            string userName = Environment.UserName;

            if (string.IsNullOrEmpty(userName))
            {
                ShowDebugInfo();
                throw new MyErrorException(ErrorMessageLogin, ErrorTitleLogin, GetType().Name, method, line);
            }

            if (string.IsNullOrEmpty(CurrentUser.employeeId))
            {
                User user = null;
                SelectedUserItem = null;
                user = RU.GetUserByEmployeeId(userName);

                if (user != null)
                {
                    _User.Add(new DetailViewModel<UserModel>(new UserModel(user)));
                    SelectedUserItem = new DetailViewModel<UserModel>(new UserModel(user));
                    CurrentUser.employeeId = userName;

                    foreach (var ParmSite in RPS.GetAllParmSiteByEmployee(userName))
                    {
                        _ParmSite.Add(new DetailViewModel<ParmSiteModel>(new ParmSiteModel(ParmSite)));
                        CurrentUser.ParmSite = ParmSite;
                    }

                    foreach (var userProfile in RU.GetAllLevelAccessForSession(userName))
                    {
                        _UserProfile.Add(new DetailViewModel<UserProfileModel>(new UserProfileModel(userProfile)));
                    }

                    // Thread for load the data in the application before open the window 'intervention'
                    int TimerOpenWindow = 0;
                    TimerOpenWindow = Convert.ToInt32(ConfigurationManager.AppSettings["TimerOpenApplicationInSecondes"].ToString());
                    if (TimerOpenWindow > 0)
                    {
                        var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(TimerOpenWindow) };
                        timer.Start();
                        timer.Tick += (sender, args) =>
                        {
                            timer.Stop();
                            // Open the intervention directly if the userProfile is Garage
                            UserProfile access = GetLevelAccessUser(CurrentUser.employeeId, CurrentUser.ParmSite);
                            if (access.ProfileLevel == (int)UserProfileEnum.Garage)
                                _OpenIntervention();
                        };
                    }
                }
                else
                {
                    ShowDebugInfo();
                    throw new MyErrorException(ErrorMessageUserNotFound, ErrorTitleUserNotFound, GetType().Name, method, line);
                }
            }
            else
            {
                foreach (var user in RU.SelectAllElement())
                {
                    _User.Add(new DetailViewModel<UserModel>(new UserModel(user)));
                }
            }
        }

        public bool _CanExecuteOpenIntervention()
        {
            return SelectedUserProfileItem != null;
        }

        public ICommand OpenIntervention
        {
            get
            {
                return new RelayCommand(_OpenIntervention, _CanExecuteOpenIntervention);
            }
        }

        /// <summary>
        /// Open view intervention
        /// </summary>
        public void _OpenIntervention()
        {
            UserProfile profile = null;
            SwitchWindows sw = new SwitchWindows();

            if (RU.CheckIfUserIsActive(CurrentUser.employeeId))
            {
                if (SelectedUserProfileItem != null)
                    profile = new UserProfile(SelectedUserProfileItem.Detail.UserProfileId, SelectedUserProfileItem.Detail.Name, SelectedUserProfileItem.Detail.ProfileLevel, SelectedUserProfileItem.Detail.SiteId);
                else profile = GetLevelAccessUser(CurrentUser.employeeId, CurrentUser.ParmSite); // Check the profile name for close the application if inactif for the profile garage

                if (!string.IsNullOrEmpty(CurrentUser.employeeId))
                    if (profile.ProfileLevel == (int)UserProfileEnum.Garage)
                    {
                        CurrentUser.CheckAuthorized = true;
                    }

                // Change content main windows with the AllIntervention View
                switch(profile.ProfileLevel)
                {
                    case (int)UserProfileEnum.Garage:
                        sw.ChangeViewWindows("AllIntervention", profile);
                        break;
                    case (int)UserProfileEnum.Administrateur:
                    case (int)UserProfileEnum.Responsable: 
                        sw.ChangeViewWindows("AllInterventionAdvanced", profile);
                        break;
                    case (int)UserProfileEnum.Compta:
                        sw.ChangeViewWindows("AllInvoice", profile);
                        break;
                    default:
                        ShowDebugInfo();
                        throw new MyErrorException(ErrorMessageUserProfile, ErrorTitleUserProfile, GetType().Name, method, line);
                }
            }
            else
            {
                ShowDebugInfo();
                throw new MyErrorException(ErrorMessageUserNotActive, ErrorTitleUserNotActive, GetType().Name, method, line);
            }
        }

        /// <summary>
        /// Return the UserProfile for an employeeId and the parmSite
        /// </summary>
        /// <param name="_employeeId"></param>
        /// <param name="_parmSite"></param>
        /// <returns></returns>
        public UserProfile GetLevelAccessUser(string _employeeId, ParmSite _parmSite)
        {
            UserProfile userPro = null;
            userPro = RU.GetLevelAccessForSession(_employeeId, _parmSite);
            return userPro;
        }

        #region status users   
        public bool _CanExecuteSaveStatusUser()
        {
            return SelectedUserItem != null;
        }

        public ICommand SaveStatusUser
        {
            get
            {
                return new RelayCommand(_SaveStatusUser, _CanExecuteSaveStatusUser);
            }
        }

        /// <summary>
        /// Save the status user
        /// </summary>
        /// <returns></returns>
        public void _SaveStatusUser()
        {
            User user = new User(SelectedUserItem.Detail.UserId, SelectedUserItem.Detail.Active, SelectedUserItem.Detail.EmployeeId);
            if (user != null)
                RU.UpdateStatusUser(user);
        }

        public ICommand CloseWindowStatusUser
        {
            get
            {
                return new RelayCommand(_CloseWindowStatusUser, null);
            }
        }

        /// <summary>
        /// Close the window with the users status
        /// </summary>
        /// <param name="parameter"></param>
        public void _CloseWindowStatusUser(object parameter)
        {
            if (parameter != null)
            {
                Window win = (Window)parameter;
                win.Close();
            }
        }
        #endregion
    }

    // Current user on application
    static class CurrentUser
    {
        private static string _employeeId = null;
        private static ParmSite _ParmSite = null;
        private static UserProfile _UserProfile = null;
        public static bool IsActif = false;
        public static bool CheckAuthorized = false;
        public static string employeeId { get { return _employeeId; } set { if (_employeeId != value) _employeeId = value; } }
        public static ParmSite ParmSite { get { return _ParmSite; } set { if (_ParmSite != value) _ParmSite = value; } }
        public static UserProfile UserProfile { get { return _UserProfile; } set { if (_UserProfile != value) _UserProfile = value; } }
    }
}
