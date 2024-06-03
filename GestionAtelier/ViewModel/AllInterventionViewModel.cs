using GestionAtelier.DB;
using GestionAtelier.Model;
using GestionAtelier.ToolBox;
using GestionAtelier.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GestionAtelier.ViewModel
{
    class AllInterventionViewModel : ObservableObject
    {
        RequestDBPrestation RP = new RequestDBPrestation();
        RequestDBEmployee RE = new RequestDBEmployee();
        RequestDBInterventionType RIT = new RequestDBInterventionType();
        RequestDBInterventionTire RITI = new RequestDBInterventionTire();
        RequestDBPrestationType RPT = new RequestDBPrestationType();
        RequestDBInvoice RIV = new RequestDBInvoice();
        RequestDBVehicle RV = new RequestDBVehicle();
        RequestDBStatus RST = new RequestDBStatus();
        RequestDBIntervention RI = new RequestDBIntervention();
        RequestDBTires RTI = new RequestDBTires();
        RequestDBParmSite RPS = new RequestDBParmSite();
        private bool close = false;
        Window vehicleWindow = null;
        //Window tiresWindow = null;
        Window pictureWindow = null;
        UserProfile userProfile = null;
        public int TiresTypeForOpenWindow = 0;
        public enum TiresType { TIRE = 1, WHEEL = 2 };
        const string TextTire = "Pneu";
        const string TextStatusOpen = "Open";
        const string TextStatusClose = "Close";
        const string ErrorMessageRFID = "Il n'y a pas de code barre valide d'entrée. Utiliser le lecteur code barre sur votre carte.";
        const string ErrorMessageRealized = "Vous devez cocher la case 'Réalisé' sur la prestation pour valider.";
        const string ErrorMessageHoursCount = "Nombre d’heure non renseigné.";
        const string ErrorMessageTiresCount = "Nombre de pneu non renseigné.";
        const string ErrorMessageGenerateBarcode = "Aucun répertoire n'a été choisis pour l'emplacement des barcodes des interventions.";
        const string ErrorMessageDayOfYear = "La date choisis doit être la date du jour ou une date précédente.";
        const string ErrorMessagePictureInterventionFolder = "Aucun dossier n'a été choisis pour l'emplacement des images d'interventions.";
        const string ErrorTitlePictureInterventionFolder = "Erreur dossier image d'interventions";
        const string ErrorTitleRFID = "Erreur de code barre";
        const string ErrorTitlePrestation = "Erreur dans la prestation";
        const string ErrorTitleTires = "Erreur dans l'encodage des pneus";
        const string ErrorTitleGenerateBarCode = "Erreur barcode";
        const string ErrorTitleDayOfYear = "Erreur dans la date";
        const string ErrorTitleOpenPicture = "Erreur ouverture photo";
        const int minLengthRFID = 4;
        static string method = "";
        static int line = 0;

        private static void ShowDebugInfo()
        {
            StackFrame stackFrame = new StackFrame(1, true);

            method = stackFrame.GetMethod().ToString();
            line = stackFrame.GetFileLineNumber();
        }

        enum UserProfileEnum
        {
            Garage                  = 1,
            Responsable             = 2,
            Compta                  = 3,
            Administrateur          = 4,
        }

        public enum ChangeTireCause
        {
            Use         = 1,
            Creve       = 2,
            Jante       = 3,
            Permutation = 4
        }

        enum InterventionTypeEnum
        {
            SMALL_SERVICING         = 1,
            LARGE_SERVICING         = 2,
            TECHNICAL_CONTROL       = 3,
            PANNE                   = 4,
            TIRE_CHANGE             = 5,
            STORAGE                 = 6,
            CLEANING                = 7
        }

        enum PrestationTypeEnum
        {
            TIRE_CHANGE             = 1
        }

        enum TypeTruckOrCrane
        {
            TRUCK                   = 1,
            CRANE                   = 2
        }

        #region ObservableCollection
        private readonly ObservableCollection<DetailViewModel<InterventionModel>> _Intervention;

        public ObservableCollection<DetailViewModel<InterventionModel>> Intervention
        {
            get
            {
                return _Intervention;
            }
        }

        private readonly ObservableCollection<DetailViewModel<InterventionModel>> _listInterventionForSheet;

        public ObservableCollection<DetailViewModel<InterventionModel>> listInterventionForSheet
        {
            get
            {
                return _listInterventionForSheet;
            }
        }

        private readonly ObservableCollection<DetailViewModel<VehicleModel>> _Vehicle;

        public ObservableCollection<DetailViewModel<VehicleModel>> Vehicle
        {
            get
            {
                return _Vehicle;
            }
        }

        private readonly ObservableCollection<DetailViewModel<InterventionTypeModel>> _InterventionType;

        public ObservableCollection<DetailViewModel<InterventionTypeModel>> InterventionType
        {
            get
            {
                return _InterventionType;
            }
        }

        private readonly ObservableCollection<DetailViewModel<EmployeeModel>> _Applicant;

        public ObservableCollection<DetailViewModel<EmployeeModel>> Applicant
        {
            get
            {
                return _Applicant;
            }
        }

        private readonly ObservableCollection<DetailViewModel<EmployeeModel>> _Submitter;

        public ObservableCollection<DetailViewModel<EmployeeModel>> Submitter
        {
            get
            {
                return _Submitter;
            }
        }

        private readonly ObservableCollection<DetailViewModel<PrestationModel>> _Prestation;

        public ObservableCollection<DetailViewModel<PrestationModel>> Prestation
        {
            get
            {
                return _Prestation;
            }
        }

        private readonly ObservableCollection<DetailViewModel<TiresModel>> _Tires;

        public ObservableCollection<DetailViewModel<TiresModel>> Tires
        {
            get
            {
                return _Tires;
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

        private readonly ObservableCollection<DetailViewModel<StatusModel>> _Status;

        public ObservableCollection<DetailViewModel<StatusModel>> Status
        {
            get
            {
                return _Status;
            }
        }

        private readonly ObservableCollection<DetailViewModel<PriorityModel>> _Priority;

        public ObservableCollection<DetailViewModel<PriorityModel>> Priority
        {
            get
            {
                return _Priority;
            }
        }

        private readonly ObservableCollection<DetailViewModel<ImageIntervention>> _PictureIntervention;

        public ObservableCollection<DetailViewModel<ImageIntervention>> PictureIntervention
        {
            get
            {
                return _PictureIntervention;
            }
        }
        #endregion

        #region SelectedItem
        private DetailViewModel<InterventionModel> _SelectedInterventionItem;

        public DetailViewModel<InterventionModel> SelectedInterventionItem
        {
            get
            {
                return _SelectedInterventionItem;
            }
            set
            {
                if (value != _SelectedInterventionItem)
                {
                    _SelectedInterventionItem = value;
                    CurrentUser.IsActif = true;
                    DisplayPrestationForIntervention();
                    DisplayInterventionTireForIntervention();
                    // if the intervention type = change tires then display interface tires
                    if (value != null)
                    {
                        if (value.Detail.interventionTypeId.InterventionTypeId == (int)InterventionTypeEnum.TIRE_CHANGE)
                        {
                            SwitchVisibilityTiresInterface(Visibility.Visible);
                        }
                        else SwitchVisibilityTiresInterface(Visibility.Hidden);
                    }
                    else SwitchVisibilityTiresInterface(Visibility.Hidden);
                    if (!CurrentUser.CheckAuthorized && value != null)
                    {
                        SelectedDateEstimateItem = value.Detail.DateEstimate;
                        SelectedDateInItem = value.Detail.DateIn;
                        SelectedDateOutItem = value.Detail.DateOut;
                        SelectedTimeEstimateItem = value.Detail.TimeEstimate;
                        SelectedPieceOrderItem = value.Detail.PieceOrder;
                        SelectedPieceComItem = value.Detail.PieceCom;
                    }

                    RaisePropertyChanged(() => SelectedInterventionItem);
                }
            }
        }

        private DetailViewModel<VehicleModel> _SelectedVehicleItem;

        public DetailViewModel<VehicleModel> SelectedVehicleItem
        {
            get
            {
                return _SelectedVehicleItem;
            }
            set
            {
                if (value != _SelectedVehicleItem)
                {
                    _SelectedVehicleItem = value;
                    CurrentUser.IsActif = true;
                    if (value != null && CurrentUser.CheckAuthorized)
                        ChangeAllInterventionByVehicleId(value.Detail.VehicleId);
                    //else if (value == null && CurrentUser.CheckAuthorized)
                    //    ReloadAllIntervention();
                    RaisePropertyChanged(() => SelectedVehicleItem);
                }
            }
        }

        private DetailViewModel<InterventionTypeModel> _SelectedInterventionTypeItem;

        public DetailViewModel<InterventionTypeModel> SelectedInterventionTypeItem
        {
            get
            {
                return _SelectedInterventionTypeItem;
            }
            set
            {
                if (value != _SelectedInterventionTypeItem)
                {
                    _SelectedInterventionTypeItem = value;
                    CurrentUser.IsActif = true;
                    RaisePropertyChanged(() => SelectedInterventionTypeItem);
                }
            }
        }

        private DetailViewModel<EmployeeModel> _SelectedApplicantItem;

        public DetailViewModel<EmployeeModel> SelectedApplicantItem
        {
            get
            {
                return _SelectedApplicantItem;
            }
            set
            {
                if (value != _SelectedApplicantItem)
                {
                    _SelectedApplicantItem = value;
                    CurrentUser.IsActif = true;
                    RaisePropertyChanged(() => SelectedApplicantItem);
                }
            }
        }

        private DetailViewModel<EmployeeModel> _SelectedSubmitterItem;

        public DetailViewModel<EmployeeModel> SelectedSubmitterItem
        {
            get
            {
                return _SelectedSubmitterItem;
            }
            set
            {
                if (value != _SelectedSubmitterItem)
                {
                    _SelectedSubmitterItem = value;
                    CurrentUser.IsActif = true;
                    RaisePropertyChanged(() => SelectedSubmitterItem);
                }
            }
        }

        private DetailViewModel<PrestationModel> _SelectedPrestationItem;

        public DetailViewModel<PrestationModel> SelectedPrestationItem
        {
            get
            {
                return _SelectedPrestationItem;
            }
            set
            {
                if (value != _SelectedPrestationItem)
                {
                    _SelectedPrestationItem = value;
                    CurrentUser.IsActif = true;
                    if (!CurrentUser.CheckAuthorized && value != null)
                    {
                        if (value.Detail.EmployeeId != null)
                            SelectedBadgeNumberItem = value.Detail.EmployeeId.BadgeNumber;
                        else SelectedBadgeNumberItem = "";
                    }
                    else if (!CurrentUser.CheckAuthorized && value == null)
                        SelectedBadgeNumberItem = "";
                    RaisePropertyChanged(() => SelectedPrestationItem);
                }
            }
        }

        private string _SelectedBadgeNumberItem;

        public string SelectedBadgeNumberItem
        {
            get
            {
                if (string.IsNullOrEmpty(_SelectedBadgeNumberItem))
                    return "";
                else return _SelectedBadgeNumberItem;
            }
            set
            {
                if (value != _SelectedBadgeNumberItem)
                {
                    _SelectedBadgeNumberItem = value;
                    CurrentUser.IsActif = true;
                    // If check authorized, profile = garage so we call the function insertPrestation
                    if (CurrentUser.CheckAuthorized && !string.IsNullOrEmpty(value) && value.Length >= minLengthRFID)
                        _InsertPrestation();
                    RaisePropertyChanged(() => SelectedBadgeNumberItem);
                }
            }
        }

        private float _GetNumberHourCountOnIntervention;

        public float GetNumberHourCountOnIntervention
        {
            get
            {
                return _GetNumberHourCountOnIntervention;
            }
            set
            {
                if (value != _GetNumberHourCountOnIntervention)
                {
                    _GetNumberHourCountOnIntervention = value;
                    RaisePropertyChanged(() => GetNumberHourCountOnIntervention);
                }
            }
        }

        private DetailViewModel<TiresModel> _SelectedTireCreveItem;

        public DetailViewModel<TiresModel> SelectedTireCreveItem
        {
            get
            {
                return _SelectedTireCreveItem;
            }
            set
            {
                if (value != _SelectedTireCreveItem)
                {
                    _SelectedTireCreveItem = value;
                    if (value != null)
                    {
                        GetInfoCreveTire = value.Detail.ItemId;
                        ReloadAllDataForTiresList((int)ChangeTireCause.Creve);
                    }
                    CurrentUser.IsActif = true;
                    RaisePropertyChanged(() => SelectedTireCreveItem);
                }
            }
        }

        private int _SelectedTireNumberCreveItem;

        public int SelectedTireNumberCreveItem
        {
            get
            {
                return _SelectedTireNumberCreveItem;
            }
            set
            {
                if (value != _SelectedTireNumberCreveItem)
                {
                    _SelectedTireNumberCreveItem = value;
                    CurrentUser.IsActif = true;
                    RaisePropertyChanged(() => SelectedTireNumberCreveItem);
                }
            }
        }

        private string _GetInfoCreveTire;

        public string GetInfoCreveTire
        {
            get
            {
                if (string.IsNullOrEmpty(_GetInfoCreveTire))
                    return TextTire;
                else return _GetInfoCreveTire;
            }
            set
            {
                if (value != _GetInfoCreveTire)
                {
                    _GetInfoCreveTire = value;
                    CurrentUser.IsActif = true;
                    RaisePropertyChanged(() => GetInfoCreveTire);
                }
            }
        }

        private DetailViewModel<TiresModel> _SelectedTireUseItem;

        public DetailViewModel<TiresModel> SelectedTireUseItem
        {
            get
            {
                return _SelectedTireUseItem;
            }
            set
            {
                if (value != _SelectedTireUseItem)
                {
                    _SelectedTireUseItem = value;
                    if (value != null)
                    {
                        GetInfoUseTire = value.Detail.ItemId;
                        ReloadAllDataForTiresList((int)ChangeTireCause.Use);
                    }
                    CurrentUser.IsActif = true;
                    RaisePropertyChanged(() => SelectedTireUseItem);
                }
            }
        }

        private int _SelectedTireNumberUseItem;

        public int SelectedTireNumberUseItem
        {
            get
            {
                return _SelectedTireNumberUseItem;
            }
            set
            {
                if (value != _SelectedTireNumberUseItem)
                {
                    _SelectedTireNumberUseItem = value;
                    CurrentUser.IsActif = true;
                    RaisePropertyChanged(() => SelectedTireNumberUseItem);
                }
            }
        }

        private string _GetInfoUseTire;

        public string GetInfoUseTire
        {
            get
            {
                if (string.IsNullOrEmpty(_GetInfoUseTire))
                    return TextTire;
                else return _GetInfoUseTire;
            }
            set
            {
                if (value != _GetInfoUseTire)
                {
                    _GetInfoUseTire = value;
                    CurrentUser.IsActif = true;
                    RaisePropertyChanged(() => GetInfoUseTire);
                }
            }
        }

        private DetailViewModel<TiresModel> _SelectedTireJanteItem;

        public DetailViewModel<TiresModel> SelectedTireJanteItem
        {
            get
            {
                return _SelectedTireJanteItem;
            }
            set
            {
                if (value != _SelectedTireJanteItem)
                {
                    _SelectedTireJanteItem = value;
                    if (value != null)
                    {
                        GetInfoJanteTire = value.Detail.ItemId;
                        ReloadAllDataForTiresList((int)ChangeTireCause.Jante);
                    }
                    CurrentUser.IsActif = true;
                    RaisePropertyChanged(() => SelectedTireJanteItem);
                }
            }
        }

        private int _SelectedTireNumberJanteItem;

        public int SelectedTireNumberJanteItem
        {
            get
            {
                return _SelectedTireNumberJanteItem;
            }
            set
            {
                if (value != _SelectedTireNumberJanteItem)
                {
                    _SelectedTireNumberJanteItem = value;
                    CurrentUser.IsActif = true;
                    RaisePropertyChanged(() => SelectedTireNumberJanteItem);
                }
            }
        }

        private string _GetInfoJanteTire;

        public string GetInfoJanteTire
        {
            get
            {
                if (string.IsNullOrEmpty(_GetInfoJanteTire))
                    return TextTire;
                else return _GetInfoJanteTire;
            }
            set
            {
                if (value != _GetInfoJanteTire)
                {
                    _GetInfoJanteTire = value;
                    CurrentUser.IsActif = true;
                    RaisePropertyChanged(() => GetInfoJanteTire);
                }
            }
        }

        private DetailViewModel<TiresModel> _SelectedTirePermutationItem;

        public DetailViewModel<TiresModel> SelectedTirePermutationItem
        {
            get
            {
                return _SelectedTirePermutationItem;
            }
            set
            {
                if (value != _SelectedTirePermutationItem)
                {
                    _SelectedTirePermutationItem = value;
                    if (value != null)
                    {
                        GetInfoPermutationTire = value.Detail.ItemId;
                        ReloadAllDataForTiresList((int)ChangeTireCause.Permutation);
                    }
                    CurrentUser.IsActif = true;
                    RaisePropertyChanged(() => SelectedTirePermutationItem);
                }
            }
        }

        private int _SelectedTireNumberPermutationItem;

        public int SelectedTireNumberPermutationItem
        {
            get
            {
                return _SelectedTireNumberPermutationItem;
            }
            set
            {
                if (value != _SelectedTireNumberPermutationItem)
                {
                    _SelectedTireNumberPermutationItem = value;
                    CurrentUser.IsActif = true;
                    RaisePropertyChanged(() => SelectedTireNumberPermutationItem);
                }
            }
        }

        private string _GetInfoPermutationTire;

        public string GetInfoPermutationTire
        {
            get
            {
                if (string.IsNullOrEmpty(_GetInfoPermutationTire))
                    return TextTire;
                else return _GetInfoPermutationTire;
            }
            set
            {
                if (value != _GetInfoPermutationTire)
                {
                    _GetInfoPermutationTire = value;
                    CurrentUser.IsActif = true;
                    RaisePropertyChanged(() => GetInfoPermutationTire);
                }
            }
        }

        private DetailViewModel<TiresModel> _SelectedTiresItem;

        public DetailViewModel<TiresModel> SelectedTiresItem
        {
            get
            {
                return _SelectedTiresItem;
            }
            set
            {
                if (value != _SelectedTiresItem)
                {
                    _SelectedTiresItem = value;
                    if (value != null)
                        SetSelectedTiresItem(value);
                    RaisePropertyChanged(() => SelectedTiresItem);
                }
            }
        }

        private Visibility _GetVisibilityForTiresInterface;

        public Visibility GetVisibilityForTiresInterface
        {
            get
            {
                return _GetVisibilityForTiresInterface;
            }
            set
            {
                if (value != _GetVisibilityForTiresInterface)
                {
                    _GetVisibilityForTiresInterface = value;
                    RaisePropertyChanged(() => GetVisibilityForTiresInterface);
                }
            }
        }

        private Visibility _GetVisibilityForMenuInterface;

        public Visibility GetVisibilityForMenuInterface
        {
            get
            {
                return _GetVisibilityForMenuInterface;
            }
            set
            {
                if (value != _GetVisibilityForMenuInterface)
                {
                    _GetVisibilityForMenuInterface = value;
                    RaisePropertyChanged(() => GetVisibilityForMenuInterface);
                }
            }
        }

        private string _SelectedBarcodeItem;

        public string SelectedBarcodeItem
        {
            get
            {
                if (string.IsNullOrEmpty(_SelectedBarcodeItem))
                    return "";
                else return _SelectedBarcodeItem;
            }
            set
            {
                if (value != _SelectedBarcodeItem)
                {
                    _SelectedBarcodeItem = value;
                    CurrentUser.IsActif = true;
                    bool BarcodeFind = false;
                    if (!string.IsNullOrEmpty(value) && value != "")
                        if (Convert.ToInt32(value) > 0)
                        {
                            ReloadAllInterventionByBarcode(Convert.ToInt32(value));
                            BarcodeFind = true;
                        }

                    if (!BarcodeFind)
                        ReloadAllIntervention();
                    RaisePropertyChanged(() => SelectedBarcodeItem);
                }
            }
        }

        #region advancedInterface
        private DetailViewModel<ParmSiteModel> _SelectedParmSiteItem;

        public DetailViewModel<ParmSiteModel> SelectedParmSiteItem
        {
            get
            {
                return _SelectedParmSiteItem;
            }
            set
            {
                if (value != _SelectedParmSiteItem)
                {
                    _SelectedParmSiteItem = value;
                    RaisePropertyChanged(() => SelectedParmSiteItem);
                }
            }
        }

        private DetailViewModel<StatusModel> _SelectedStatusItem;

        public DetailViewModel<StatusModel> SelectedStatusItem
        {
            get
            {
                return _SelectedStatusItem;
            }
            set
            {
                if (value != _SelectedStatusItem)
                {
                    _SelectedStatusItem = value;
                    RaisePropertyChanged(() => SelectedStatusItem);
                }
            }
        }

        private DetailViewModel<PriorityModel> _SelectedPriorityItem;

        public DetailViewModel<PriorityModel> SelectedPriorityItem
        {
            get
            {
                return _SelectedPriorityItem;
            }
            set
            {
                if (value != _SelectedPriorityItem)
                {
                    _SelectedPriorityItem = value;
                    RaisePropertyChanged(() => SelectedStatusItem);
                }
            }
        }

        private DateTime _SelectedDateInItem;

        public DateTime SelectedDateInItem
        {
            get
            {
                if (_SelectedDateInItem.Year == 1)
                    _SelectedDateInItem = DateTime.Now;
                return _SelectedDateInItem;
            }
            set
            {
                if (value != _SelectedDateInItem)
                {
                    _SelectedDateInItem = value;
                    RaisePropertyChanged(() => SelectedDateInItem);
                }
            }
        }

        private DateTime _SelectedDateOutItem;

        public DateTime SelectedDateOutItem
        {
            get
            {
                if (_SelectedDateOutItem.Year == 1)
                    _SelectedDateOutItem = DateTime.Now;
                return _SelectedDateOutItem;
            }
            set
            {
                if (value != _SelectedDateOutItem)
                {
                    _SelectedDateOutItem = value;
                    RaisePropertyChanged(() => SelectedDateOutItem);
                }
            }
        }

        private DateTime _SelectedDateEstimateItem;

        public DateTime SelectedDateEstimateItem
        {
            get
            {
                if (_SelectedDateEstimateItem.Year == 1)
                    _SelectedDateEstimateItem = DateTime.Now;
                return _SelectedDateEstimateItem;
            }
            set
            {
                if (value != _SelectedDateEstimateItem)
                {
                    _SelectedDateEstimateItem = value;
                    RaisePropertyChanged(() => SelectedDateEstimateItem);
                }
            }
        }

        private float _SelectedTimeEstimateItem;

        public float SelectedTimeEstimateItem
        {
            get
            {
                return _SelectedTimeEstimateItem;
            }
            set
            {
                if (value != _SelectedTimeEstimateItem)
                {
                    _SelectedTimeEstimateItem = value;
                    RaisePropertyChanged(() => SelectedTimeEstimateItem);
                }
            }
        }

        private bool _SelectedPieceOrderItem;

        public bool SelectedPieceOrderItem
        {
            get
            {
                return _SelectedPieceOrderItem;
            }
            set
            {
                if (value != _SelectedPieceOrderItem)
                {
                    _SelectedPieceOrderItem = value;
                    RaisePropertyChanged(() => SelectedPieceOrderItem);
                }
            }
        }

        private string _SelectedPieceComItem;

        public string SelectedPieceComItem
        {
            get
            {
                return _SelectedPieceComItem;
            }
            set
            {
                if ((value != _SelectedPieceComItem && SelectedPieceOrderItem) || string.IsNullOrEmpty(value))
                {
                    _SelectedPieceComItem = value;
                    RaisePropertyChanged(() => SelectedPieceComItem);
                }
            }
        }

        private DetailViewModel<ImageIntervention> _SelectedInterventionPictureItem;

        public DetailViewModel<ImageIntervention> SelectedInterventionPictureItem
        {
            get
            {
                return _SelectedInterventionPictureItem;
            }
            set
            {
                if (value != _SelectedInterventionPictureItem)
                {
                    _SelectedInterventionPictureItem = value;
                    RaisePropertyChanged(() => SelectedInterventionPictureItem);
                }
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// Empty constructor for the input command
        /// </summary>
        public AllInterventionViewModel() { }

        public AllInterventionViewModel(UserProfile userProfile)
        {
            _Intervention = new ObservableCollection<DetailViewModel<InterventionModel>>();
            _Vehicle = new ObservableCollection<DetailViewModel<VehicleModel>>();
            _InterventionType = new ObservableCollection<DetailViewModel<InterventionTypeModel>>();
            _Applicant = new ObservableCollection<DetailViewModel<EmployeeModel>>();
            _Submitter = new ObservableCollection<DetailViewModel<EmployeeModel>>();
            _Prestation = new ObservableCollection<DetailViewModel<PrestationModel>>();
            _Tires = new ObservableCollection<DetailViewModel<TiresModel>>();
            _listInterventionForSheet = new ObservableCollection<DetailViewModel<InterventionModel>>();

            this.userProfile = userProfile;

            // Get level access, if access administrator -> display menu item for user management
            if (userProfile.ProfileLevel == (int)UserProfileEnum.Administrateur)
                GetVisibilityForMenuInterface = Visibility.Visible;
            else GetVisibilityForMenuInterface = Visibility.Collapsed;

            if (!CurrentUser.CheckAuthorized)
            {
                _ParmSite = new ObservableCollection<DetailViewModel<ParmSiteModel>>();
                _Status = new ObservableCollection<DetailViewModel<StatusModel>>();
                _Priority = new ObservableCollection<DetailViewModel<PriorityModel>>();
                _PictureIntervention = new ObservableCollection<DetailViewModel<ImageIntervention>>();
            }

            SwitchVisibilityTiresInterface(Visibility.Hidden);

            foreach (var intervention in RI.GetAllInterventions(false, userProfile.SiteId.SiteId))
            {
                _Intervention.Add(new DetailViewModel<InterventionModel>(new InterventionModel(intervention)));
            }

            foreach (var vehicle in RV.SelectAllElement())
            {
                _Vehicle.Add(new DetailViewModel<VehicleModel>(new VehicleModel(vehicle)));
            }

            foreach (var interventionType in RIT.SelectAllElement())
            {
                _InterventionType.Add(new DetailViewModel<InterventionTypeModel>(new InterventionTypeModel(interventionType)));
            }

            foreach (var employee in RE.SelectAllElement())
            {
                _Applicant.Add(new DetailViewModel<EmployeeModel>(new EmployeeModel(employee)));
            }

            foreach (var employee in RE.SelectAllEmployeeByUserProfile(userProfile.SiteId))
            {
                _Submitter.Add(new DetailViewModel<EmployeeModel>(new EmployeeModel(employee)));
            }

            foreach (var tires in RTI.SelectAllTiresByCompanyId(userProfile.SiteId.CompanyID))
            {
                _Tires.Add(new DetailViewModel<TiresModel>(new TiresModel(tires)));
            }

            if (!CurrentUser.CheckAuthorized)
            {
                foreach (var parmSite in RPS.GetAllParmSiteByEmployee(CurrentUser.employeeId))
                {
                    _ParmSite.Add(new DetailViewModel<ParmSiteModel>(new ParmSiteModel(parmSite)));
                }

                foreach (var status in RST.SelectAllElement())
                {
                    _Status.Add(new DetailViewModel<StatusModel>(new StatusModel(status)));
                }

                _Priority.Add(new DetailViewModel<PriorityModel>(new PriorityModel(new Priority(1))));
                _Priority.Add(new DetailViewModel<PriorityModel>(new PriorityModel(new Priority(2))));
                _Priority.Add(new DetailViewModel<PriorityModel>(new PriorityModel(new Priority(3))));
            }
        }

        /// <summary>
        /// Change the observableCollection with intervention on vehicle
        /// </summary>
        /// <param name="VehicleId"></param>
        public void ChangeAllInterventionByVehicleId(string VehicleId)
        {
            _Intervention.Clear();

            foreach (var intervention in RI.GetAllInterventionsOnVehicle(VehicleId, userProfile.SiteId.SiteId))
            {

                _Intervention.Add(new DetailViewModel<InterventionModel>(new InterventionModel(intervention)));
            }
        }

        /// <summary>
        /// Reload all interventions in observableCollection Intervention
        /// </summary>
        public void ReloadAllIntervention(bool OpenLastInterventionEncoded = false)
        {
            _Intervention.Clear();

            foreach (var intervention in RI.GetAllInterventions(close, userProfile.SiteId.SiteId))
            {
                _Intervention.Add(new DetailViewModel<InterventionModel>(new InterventionModel(intervention)));
            }

            if (OpenLastInterventionEncoded)
            {
                SelectedInterventionItem = _Intervention.Last();
                if (SelectedInterventionItem.Detail.interventionTypeId.InterventionTypeId == (int)InterventionTypeEnum.TIRE_CHANGE)
                {
                    SwitchVisibilityTiresInterface(Visibility.Visible);
                }
            }
        }

        /// <summary>
        /// Reload all interventions in observableCollection intervention by barcode
        /// </summary>
        public void ReloadAllInterventionByBarcode(int barcode)
        {
            _Intervention.Clear();

            foreach (var intervention in RI.GetAllInterventions(close, userProfile.SiteId.SiteId, barcode))
            {
                _Intervention.Add(new DetailViewModel<InterventionModel>(new InterventionModel(intervention)));
            }
        }

        /// <summary>
        /// Reload all data for the tires list
        /// </summary>
        public void ReloadAllDataForTiresList(int tiresType)
        {
            _Tires.Clear();

            TiresTypeForOpenWindow = tiresType;

            foreach (var tires in RTI.SelectAllTiresByCompanyId(userProfile.SiteId.CompanyID))
            {
                switch (TiresTypeForOpenWindow)
                {
                    case (int)ChangeTireCause.Creve:
                        if (tires.ItemType == TiresType.TIRE.ToString())
                            _Tires.Add(new DetailViewModel<TiresModel>(new TiresModel(tires)));
                        break;
                    case (int)ChangeTireCause.Jante:
                        if (tires.ItemType == TiresType.WHEEL.ToString())
                            _Tires.Add(new DetailViewModel<TiresModel>(new TiresModel(tires)));
                        break;
                    case (int)ChangeTireCause.Use:
                        if (tires.ItemType == TiresType.TIRE.ToString())
                            _Tires.Add(new DetailViewModel<TiresModel>(new TiresModel(tires)));
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Change the selectedItem in the list creve/jante/use/permu
        /// </summary>
        /// <param name="tires"></param>
        public void SetSelectedTiresItem(DetailViewModel<TiresModel> tires)
        {
            switch (TiresTypeForOpenWindow)
            {
                case (int)ChangeTireCause.Creve:
                    SelectedTireCreveItem = tires;
                    break;
                case (int)ChangeTireCause.Jante:
                    SelectedTireJanteItem = tires;
                    break;
                case (int)ChangeTireCause.Use:
                    SelectedTireUseItem = tires;
                    break;
                case (int)ChangeTireCause.Permutation:
                    SelectedTirePermutationItem = tires;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Display the prestations for intervention selected
        /// </summary>
        public void DisplayPrestationForIntervention()
        {
            _Prestation.Clear();
            List<int> ActivePrestation = new List<int>();
            float countHourOnPrestation = 0;

            if (SelectedInterventionItem != null)
                if (SelectedInterventionItem.Detail.StatusId.Name != TextStatusClose)
                {
                    foreach (var prestation in RP.GetAllPrestationsByIntervention(SelectedInterventionItem.Detail.interventionId))
                    {
                        _Prestation.Add(new DetailViewModel<PrestationModel>(new PrestationModel(prestation)));
                        ActivePrestation.Add(prestation.PrestationTypeId.PrestationTypeId);
                        countHourOnPrestation += prestation.HoursCount;
                    }

                    foreach (var prestation in RP.GetAllPrestationsBase(SelectedInterventionItem.Detail.interventionTypeId.InterventionTypeId, SelectedInterventionItem.Detail.vehicleId.VehicleId/*, ActivePrestation*/))
                    {
                        _Prestation.Add(new DetailViewModel<PrestationModel>(new PrestationModel(prestation)));
                    }
                }

            GetNumberHourCountOnIntervention = countHourOnPrestation;
        }

        /// <summary>
        /// Clean the different data information
        /// </summary>
        public void _CleanData()
        {
            SelectedApplicantItem = null;
            SelectedSubmitterItem = null;
            SelectedVehicleItem = null;
            SelectedPrestationItem = null;
            SelectedInterventionTypeItem = null;
            SelectedBadgeNumberItem = "";
            SelectedBarcodeItem = "";
            GetNumberHourCountOnIntervention = 0;
            SelectedPieceComItem = "";
            _CleanDataTire();
        }

        public void _CleanDataTire()
        {
            SelectedTireNumberUseItem = 0;
            SelectedTireNumberCreveItem = 0;
            SelectedTireNumberJanteItem = 0;
            SelectedTireNumberPermutationItem = 0;
            SelectedTireUseItem = null;
            SelectedTireCreveItem = null;
            SelectedTireJanteItem = null;
            SelectedTirePermutationItem = null;
            GetInfoCreveTire = "";
            GetInfoJanteTire = "";
            GetInfoUseTire = "";
            GetInfoPermutationTire = "";
            SwitchVisibilityTiresInterface(Visibility.Hidden);
        }

        /// <summary>
        /// Change the visiblity for tires interface
        /// </summary>
        /// <param name="visibility"></param>
        public void SwitchVisibilityTiresInterface(Visibility visibility)
        {
            GetVisibilityForTiresInterface = visibility;
        }

        #region ObjectClassBySelectedEntity
        /// <summary>
        /// Get the selected intervention
        /// </summary>
        /// <returns></returns>
        public Intervention GetInterventionSelected()
        {
            Intervention intervention = null;

            if (SelectedInterventionItem != null)
            {
                intervention = new Intervention(SelectedInterventionItem.Detail.interventionId, SelectedInterventionItem.Detail.vehicleId, SelectedInterventionItem.Detail.interventionTypeId,
                    SelectedInterventionItem.Detail.Submitter, SelectedInterventionItem.Detail.Applicant, SelectedInterventionItem.Detail.StatusId, SelectedInterventionItem.Detail.StatusDate,
                    SelectedInterventionItem.Detail.DateIn, SelectedInterventionItem.Detail.DateOut, SelectedInterventionItem.Detail.DateEstimate,
                    SelectedInterventionItem.Detail.PieceOrder, SelectedInterventionItem.Detail.PieceCom, SelectedInterventionItem.Detail.TimeEstimate,
                    SelectedInterventionItem.Detail.Priority, SelectedInterventionItem.Detail.SiteId);
            }

            return intervention;
        }

        /// <summary>
        /// Return the selected vehicle
        /// </summary>
        /// <returns></returns>
        public Vehicle GetSelectedVehicle()
        {
            return (SelectedVehicleItem != null) ? new Vehicle(SelectedVehicleItem.Detail.VehicleId) : null;
        }

        /// <summary>
        /// Return the selected intervention type
        /// </summary>
        /// <returns></returns>
        public InterventionType GetSelectedInterventionType()
        {
            return (SelectedInterventionTypeItem != null) ? new InterventionType(SelectedInterventionTypeItem.Detail.InterventionTypeId) : null;
        }

        /// <summary>
        /// Return the selected submitter
        /// </summary>
        /// <returns></returns>
        public Employee GetSelectedSubmitter()
        {
            return (SelectedSubmitterItem != null) ? new Employee(SelectedSubmitterItem.Detail.EmployeeId) : null;
        }

        /// <summary>
        /// Return the selected applicant
        /// </summary>
        /// <returns></returns>
        public Employee GetSelectedApplicant()
        {
            return (SelectedApplicantItem != null) ? new Employee(SelectedApplicantItem.Detail.EmployeeId) : null;
        }

        /// <summary>
        /// Return the selected status
        /// </summary>
        /// <returns></returns>
        public Status GetSelectedStatus()
        {
            return (SelectedStatusItem != null) ? new Status(SelectedStatusItem.Detail.StatusId) : null;
        }

        /// <summary>
        /// Return the selected priority
        /// </summary>
        /// <returns></returns>
        public Priority GetSelectedPriority()
        {
            return (SelectedPriorityItem != null) ? new Priority(SelectedPriorityItem.Detail.Name) : null;
        }

        /// <summary>
        /// Return the selected parmsite
        /// </summary>
        /// <returns></returns>
        public ParmSite GetSelectedParmSite()
        {
            return (SelectedParmSiteItem != null) ? new ParmSite(SelectedParmSiteItem.Detail.SiteId) : null;
        }

        /// <summary>
        /// Return the selected TireUseItem
        /// </summary>
        /// <returns></returns>
        public Tires GetSelectedTireUseItem()
        {
            return (SelectedTireUseItem != null) ? new Tires(SelectedTireUseItem.Detail.RECID) : null;
        }

        /// <summary>
        /// Return the selected TireCreveItem
        /// </summary>
        /// <returns></returns>
        public Tires GetSelectedTireCreveItem()
        {
            return (SelectedTireCreveItem != null) ? new Tires(SelectedTireCreveItem.Detail.RECID) : null;
        }

        /// <summary>
        /// Return the selected TireJanteItem
        /// </summary>
        /// <returns></returns>
        public Tires GetSelectedTireJanteItem()
        {
            return (SelectedTireJanteItem != null) ? new Tires(SelectedTireJanteItem.Detail.RECID) : null;
        }

        /// <summary>
        /// Return the selected TirePermutationItem
        /// </summary>
        /// <returns></returns>
        public Tires GetSelectedTirePermutationItem()
        {
            return (SelectedTirePermutationItem != null) ? new Tires(SelectedTirePermutationItem.Detail.RECID) : null;
        }
        #endregion

        public ICommand CloseIntervention
        {
            get
            {
                return new RelayCommand(_CloseIntervention, null);
            }
        }

        /// <summary>
        /// Display intervention close
        /// </summary>
        public void _CloseIntervention()
        {
            _Intervention.Clear();

            CurrentUser.IsActif = true;

            close = (!close) ? true : false;
            foreach (var intervention in RI.GetAllInterventions(close, userProfile.SiteId.SiteId))
            {
                _Intervention.Add(new DetailViewModel<InterventionModel>(new InterventionModel(intervention)));
            }

            _CleanData();
        }

        public ICommand ChangePieceOrderValue
        {
            get
            {
                return new RelayCommand(_ChangePieceOrderValue, null);
            }
        }

        /// <summary>
        /// Switch value piece order
        /// </summary>
        public void _ChangePieceOrderValue()
        {
            SelectedPieceOrderItem = (SelectedPieceOrderItem) ? true : false;
        }

        public bool _CanExecuteInsertIntervention()
        {
            if (CurrentUser.CheckAuthorized)
                return SelectedVehicleItem != null && SelectedInterventionTypeItem != null && SelectedApplicantItem != null && SelectedSubmitterItem != null;
            else return SelectedVehicleItem != null && SelectedInterventionTypeItem != null && SelectedApplicantItem != null && SelectedSubmitterItem != null
                    && SelectedParmSiteItem != null && SelectedPriorityItem != null && SelectedStatusItem != null;
        }

        public ICommand InsertIntervention
        {
            get
            {
                return new RelayCommand(_InsertIntervention, _CanExecuteInsertIntervention);
            }
        }

        /// <summary>
        /// Insert new intervention
        /// </summary>
        public void _InsertIntervention()
        {
            // Initialize the variables
            Vehicle vehicle = null;
            InterventionType interventionType = null;
            Employee submitter = null;
            Employee applicant = null;
            Status status = null;
            Priority priority = null;
            ParmSite parmSite = null;
            Intervention intervention = null;

            // Get value by selected item
            vehicle = GetSelectedVehicle();
            interventionType = GetSelectedInterventionType();
            submitter = GetSelectedSubmitter();
            applicant = GetSelectedApplicant();
            // Default value for user 'garage'
            status = RST.GetStatusByName(TextStatusOpen);
            priority = new Priority(interventionType);
            parmSite = CurrentUser.ParmSite;

            if (CurrentUser.CheckAuthorized)
            {
                intervention = new Intervention(vehicle, interventionType, submitter, applicant, status, priority, parmSite);
                intervention.DateIn = DateTime.Now;
                intervention.DateOut = DateTime.Now;
                intervention.DateEstimate = DateTime.Now.Date;
                intervention.StatusDate = DateTime.Now.Date;
                intervention.TimeEstimate = 0.0f;
                intervention.PieceOrder = false;
                intervention.PieceCom = "";
            }
            else // Get value by selected item for user 'responsable'
            {
                intervention = new Intervention(vehicle, interventionType, submitter, applicant, status, priority, parmSite);
                intervention.StatusId = GetSelectedStatus();
                intervention.Priority = GetSelectedPriority();
                intervention.SiteId = GetSelectedParmSite();
                intervention.DateIn = SelectedDateInItem;
                intervention.DateOut = SelectedDateOutItem;
                intervention.DateEstimate = SelectedDateEstimateItem;
                intervention.TimeEstimate = SelectedTimeEstimateItem;
                intervention.PieceOrder = SelectedPieceOrderItem;
                if (SelectedPieceOrderItem)
                    intervention.PieceCom = SelectedPieceComItem;
                else intervention.PieceCom = "";
                intervention.StatusDate = DateTime.Now.Date;
            }
            
            RI.InsertNewIntervention(intervention);
            _CleanData();
            ReloadAllIntervention(true);
        }

        public bool _CanExecuteCloseCurrentIntervention()
        {
            return SelectedInterventionItem != null;
        }

        public ICommand CloseCurrentIntervention
        {
            get
            {
                return new RelayCommand(_CloseCurrentIntervention, _CanExecuteCloseCurrentIntervention);
            }
        }

        /// <summary>
        /// Close the current selected intervention
        /// </summary>
        public void _CloseCurrentIntervention()
        {
            Intervention intervention = GetInterventionSelected();
            RI.CloseIntervention(intervention);
            ReloadAllIntervention();
            _CleanData();
        }

        public bool _CanExecuteUpdateCurrentIntervention()
        {
            return SelectedInterventionItem != null;
        }

        public ICommand UpdateCurrentIntervention
        {
            get
            {
                return new RelayCommand(_UpdateCurrentIntervention, _CanExecuteUpdateCurrentIntervention);
            }
        }

        /// <summary>
        /// Update the current selected intervention
        /// </summary>
        public void _UpdateCurrentIntervention()
        {
            // Initialize the variables
            int interventionId = 0;
            Vehicle vehicle = null;
            InterventionType interventionType = null;
            Employee submitter = null;
            Employee applicant = null;
            Status status = null;
            DateTime statusDate;
            DateTime dateIn;
            DateTime dateOut;
            DateTime dateEstimate;
            bool pieceOrder = false;
            string pieceCom = "";
            float timeEstimate;
            Priority priority = null;
            ParmSite site = null;
            Intervention intervention = null;

            // Get value for the different variables
            interventionId = SelectedInterventionItem.Detail.interventionId;
            vehicle = (SelectedVehicleItem != null) ? GetSelectedVehicle() : SelectedInterventionItem.Detail.vehicleId;
            interventionType = (SelectedInterventionTypeItem != null) ? GetSelectedInterventionType() : SelectedInterventionItem.Detail.interventionTypeId;
            submitter = (SelectedSubmitterItem != null) ? GetSelectedSubmitter() : SelectedInterventionItem.Detail.Submitter;
            applicant = (SelectedApplicantItem != null) ? GetSelectedApplicant() : SelectedInterventionItem.Detail.Applicant;
            status = (SelectedStatusItem != null) ? GetSelectedStatus() : SelectedInterventionItem.Detail.StatusId;
            statusDate = (SelectedStatusItem != null) ? DateTime.Now.Date : SelectedInterventionItem.Detail.StatusDate;
            dateIn = (SelectedDateInItem.Date != SelectedInterventionItem.Detail.DateIn.Date) ? SelectedDateInItem : SelectedInterventionItem.Detail.DateIn;
            dateOut = (SelectedDateOutItem.Date != SelectedInterventionItem.Detail.DateOut.Date) ? SelectedDateOutItem : SelectedInterventionItem.Detail.DateOut;
            dateEstimate = (SelectedDateEstimateItem.Date != SelectedInterventionItem.Detail.DateEstimate.Date) ? SelectedDateEstimateItem : SelectedInterventionItem.Detail.DateEstimate;
            pieceOrder = (SelectedPieceOrderItem != SelectedInterventionItem.Detail.PieceOrder) ? SelectedPieceOrderItem : SelectedInterventionItem.Detail.PieceOrder;
            pieceCom = (SelectedPieceComItem != SelectedInterventionItem.Detail.PieceCom) ? SelectedPieceComItem : SelectedInterventionItem.Detail.PieceCom;
            timeEstimate = (SelectedTimeEstimateItem != SelectedInterventionItem.Detail.TimeEstimate) ? SelectedTimeEstimateItem : SelectedInterventionItem.Detail.TimeEstimate;
            priority = (SelectedPriorityItem != null) ? GetSelectedPriority() : SelectedInterventionItem.Detail.Priority;
            site = (SelectedParmSiteItem != null) ? GetSelectedParmSite() : SelectedInterventionItem.Detail.SiteId;

            intervention = new Intervention(interventionId, vehicle, interventionType, submitter, applicant, status, statusDate, dateIn, dateOut, dateEstimate, pieceOrder, pieceCom, timeEstimate, priority, site);

            RI.UpdateIntervention(intervention);
            ReloadAllIntervention();
            _CleanData();
        }

        public bool _CanExecuteGenerateSheetIntervention()
        {
            return SelectedInterventionItem != null;
        }

        public ICommand GenerateSheetIntervention
        {
            get
            {
                return new RelayCommand(_GenerateSheetIntervention, _CanExecuteGenerateSheetIntervention);
            }
        }

        /// <summary>
        /// Generate sheet intervention
        /// </summary>
        public void _GenerateSheetIntervention()
        {
            Window gdi = new GenerateDocIntervention();
            bool showWindow = true;

            _listInterventionForSheet.Clear();

            //gdi.DataContext = SelectedInterventionItem;
            foreach (var intervention in Intervention)
            {
                if (intervention.Detail.IsCheckedForGenerateSheet)
                {
                    showWindow = _GenerateBarcodeIntervention(intervention);
                    _listInterventionForSheet.Add(intervention);
                }
            }

            if (showWindow && _listInterventionForSheet.Any())
            {
                gdi.DataContext = this;
                gdi.Show();
            }
        }

        public ICommand DisplayVehicleWindow
        {
            get
            {
                return new RelayCommand(_DisplayVehicleWindow, null);
            }
        }

        /// <summary>
        /// Display vehicle window
        /// </summary>
        public void _DisplayVehicleWindow()
        {
            vehicleWindow = new AllVehicle();
            vehicleWindow.DataContext = new AllVehicleViewModel(vehicleWindow);
            vehicleWindow.Show();
        }

        //public ICommand DisplayTiresWindow
        //{
        //    get
        //    {
        //        return new RelayCommand(_DisplayTiresWindow, null);
        //    }
        //}

        ///// <summary>
        ///// Display tires window
        ///// </summary>
        //public void _DisplayTiresWindow(object element)
        //{
        //    tiresWindow = new AllTires();
        //    switch(element.ToString())
        //    {
        //        case "LB_TiresListCreve":
        //            TiresTypeForOpenWindow = (int)ChangeTireCause.Creve;
        //            break;
        //        case "LB_TiresListUse":
        //            TiresTypeForOpenWindow = (int)ChangeTireCause.Use;
        //            break;
        //        case "LB_TiresListJante":
        //            TiresTypeForOpenWindow = (int)ChangeTireCause.Jante;
        //            break;
        //        case "LB_TiresListPermutation":
        //            TiresTypeForOpenWindow = (int)ChangeTireCause.Permutation;
        //            break;
        //        default:
        //            TiresTypeForOpenWindow = 0;
        //            break;
        //    }
        //    tiresWindow.DataContext = new AllTiresViewModel(tiresWindow, userProfile, this);
        //    tiresWindow.Show();
        //}

        public bool _CanExecuteOpenUserManagement()
        {
            return (userProfile.ProfileLevel == (int)UserProfileEnum.Administrateur) ? true : false;
        }

        public ICommand OpenUserManagement
        {
            get
            {
                return new RelayCommand(_OpenUserManagement, _CanExecuteOpenUserManagement);
            }
        }

        /// <summary>
        /// Display User Management Window
        /// </summary>
        public void _OpenUserManagement()
        {
            Window userManagement = new AllUsers();
            userManagement.Show();
        }

        public ICommand OpenSupport
        {
            get
            {
                return new RelayCommand(_OpenSupport, null);
            }
        }

        /// <summary>
        /// Display the support window
        /// </summary>
        public void _OpenSupport()
        {
            Window support = new Support();
            support.Show();
        }

        public bool _CanExecuteJoinPictureToIntervention()
        {
            return SelectedInterventionItem != null;
        }

        public ICommand JoinPictureToIntervention
        {
            get
            {
                return new RelayCommand(_JoinPictureToIntervention, _CanExecuteJoinPictureToIntervention);
            }
        }

        /// <summary>
        /// Open dialog for join pictures to intervention selected
        /// </summary>
        public void _JoinPictureToIntervention()
        {
            string directory = ConfigurationManager.AppSettings["DirectoryPicturesIntervention"].ToString();

            if (string.IsNullOrEmpty(directory))
            {
                ShowDebugInfo();
                new MyErrorException(ErrorMessagePictureInterventionFolder, ErrorTitlePictureInterventionFolder, GetType().Name, method, line);
            }

            if (!string.IsNullOrEmpty(directory))
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

            string directoryIntervention = directory + @"\" + SelectedInterventionItem.Detail.interventionId;

            if (!string.IsNullOrEmpty(directoryIntervention))
                if (!Directory.Exists(directoryIntervention))
                    Directory.CreateDirectory(directoryIntervention);

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;

            if (fileDialog.ShowDialog() == true)
            {
                string[] files = fileDialog.FileNames;

                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    File.Copy(file, directoryIntervention + @"\" + fileName, true);
                }
            }
        }

        public bool _CanExecuteDisplayPictureIntervention()
        {
            return SelectedInterventionItem != null;
        }

        public ICommand DisplayPictureIntervention
        {
            get
            {
                return new RelayCommand(_DisplayPictureIntervention, _CanExecuteDisplayPictureIntervention);
            }
        }

        /// <summary>
        /// Display the pictures for the intervention selected
        /// </summary>
        public void _DisplayPictureIntervention()
        {
            string directory = ConfigurationManager.AppSettings["DirectoryPicturesIntervention"].ToString();

            if (string.IsNullOrEmpty(directory))
            {
                ShowDebugInfo();
                new MyErrorException(ErrorMessagePictureInterventionFolder, ErrorTitlePictureInterventionFolder, GetType().Name, method, line);
            }

            _PictureIntervention.Clear();

            if (!string.IsNullOrEmpty(directory))
                if (Directory.Exists(directory))
                {
                    string directoryIntervention = directory + @"\" + SelectedInterventionItem.Detail.interventionId;

                    if (!string.IsNullOrEmpty(directoryIntervention))
                        if (Directory.Exists(directoryIntervention))
                        {
                            DirectoryInfo directoryInter = new DirectoryInfo(directoryIntervention);
                            FileInfo[] files = directoryInter.GetFiles();
                            foreach (var file in files)
                            {
                                _PictureIntervention.Add(new DetailViewModel<ImageIntervention>(new ImageIntervention(file.FullName, "Picture : " + file.Name)));
                            }

                            // Open AllPicturesIntervention window
                            if (_PictureIntervention.Any())
                            {
                                pictureWindow = new AllPicturesIntervention();
                                pictureWindow.DataContext = this;
                                pictureWindow.Show();
                            }
                        }
                }
        }

        public ICommand CloseAllPicturesIntervention
        {
            get
            {
                return new RelayCommand(_CloseAllPicturesIntervention, null);
            }
        }

        /// <summary>
        /// Close windows all pictures intervention
        /// </summary>
        public void _CloseAllPicturesIntervention()
        {
            pictureWindow.Close();
        }

        public bool _CanExecuteOpenInterventionPicture()
        {
            return SelectedInterventionPictureItem != null;
        }

        public ICommand OpenInterventionPicture
        {
            get
            {
                return new RelayCommand(_OpenInterventionPicture, _CanExecuteOpenInterventionPicture);
            }
        }

        /// <summary>
        /// Open the picture by default with the windows picture viewer
        /// </summary>
        public void _OpenInterventionPicture()
        {
            string path = SelectedInterventionPictureItem.Detail.Image;
            try
            {
                Process.Start(@"" + path);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                new MyErrorException(ex.Message, ErrorTitleOpenPicture, GetType().Name, method, line);
            }
        }

        public ICommand InterventionFilter
        {
            get
            {
                return new RelayCommand(_InterventionFilter, null);
            }
        }

        /// <summary>
        /// Filter for the interventions
        /// </summary>
        public void _InterventionFilter()
        {
            // Initialize the variables
            Vehicle vehicle = null;
            InterventionType interventionType = null;
            Employee submitter = null;
            Employee applicant = null;
            Status status = null;
            Priority priority = null;
            ParmSite site = null;

            List<DetailViewModel<InterventionModel>> interventionRemove = new List<DetailViewModel<InterventionModel>>();

            // Get value for the different variables
            vehicle = GetSelectedVehicle();
            interventionType = GetSelectedInterventionType();
            submitter = GetSelectedSubmitter();
            applicant = GetSelectedApplicant();
            status = GetSelectedStatus();
            priority = GetSelectedPriority();
            site = GetSelectedParmSite();

            foreach (var intervention in _Intervention)
            {
                if (vehicle != null)
                    if (intervention.Detail.vehicleId.VehicleId != vehicle.VehicleId)
                        interventionRemove.Add(intervention);

                if (interventionType != null)
                    if (intervention.Detail.interventionTypeId.InterventionTypeId != interventionType.InterventionTypeId)
                        interventionRemove.Add(intervention);

                if (submitter != null)
                    if (intervention.Detail.Submitter.EmployeeId != submitter.EmployeeId)
                        interventionRemove.Add(intervention);

                if (applicant != null)
                    if (intervention.Detail.Applicant.EmployeeId != applicant.EmployeeId)
                        interventionRemove.Add(intervention);

                if (status != null)
                    if (intervention.Detail.StatusId.StatusId != status.StatusId)
                        interventionRemove.Add(intervention);

                if (priority != null)
                    if (intervention.Detail.Priority.GetIntNamePriority(intervention.Detail.Priority.Name) != priority.GetIntNamePriority(priority.Name))
                        interventionRemove.Add(intervention);

                if (site != null)
                    if (intervention.Detail.SiteId.SiteId != site.SiteId)
                        interventionRemove.Add(intervention);
            }

            foreach (var intervention in interventionRemove)
            {
                _Intervention.Remove(intervention);
            }
        }

        public ICommand ResetInterventionFilter
        {
            get
            {
                return new RelayCommand(_ResetInterventionFilter, null);
            }
        }

        /// <summary>
        /// Reset the filter for the interventions
        /// </summary>
        public void _ResetInterventionFilter()
        {
            _CleanData();
            ReloadAllIntervention();
        }

        #region prestation
        public ICommand CheckInterventionType
        {
            get
            {
                return new RelayCommand(_CheckInterventionType, null);
            }
        }

        /// <summary>
        /// CheckTheInterventionType
        /// </summary>
        public void _CheckInterventionType()
        {
            if (SelectedInterventionItem != null)
                if (SelectedInterventionItem.Detail.interventionTypeId.InterventionTypeId == (int)InterventionTypeEnum.TIRE_CHANGE)
                    SelectedPrestationItem.Detail.Crane = false;
        }

        public bool _CanExecuteInsertPrestation()
        {
            return SelectedInterventionItem != null && SelectedBadgeNumberItem != "";
        }

        public ICommand InsertPrestation
        {
            get
            {
                return new RelayCommand(_InsertPrestation, _CanExecuteInsertPrestation);
            }
        }

        /// <summary>
        /// Insert prestation
        /// </summary>
        public void _InsertPrestation()
        {
            int truckCrane = 0;
            string badgeNumber = "";
            Employee employee = null;
            Prestation presta = null;
            Intervention intervention = null;

            foreach (var prestation in _Prestation)
            {
                truckCrane = 0;
                badgeNumber = "";
                employee = null;
                presta = null;

                if (prestation.Detail.Truck)
                    truckCrane = (int)TypeTruckOrCrane.TRUCK;
                else if (prestation.Detail.Crane)
                    truckCrane = (int)TypeTruckOrCrane.CRANE;
                badgeNumber = SelectedBadgeNumberItem;
                employee = RE.GetEmployeeByBadgeNumber(badgeNumber);
                if (employee == null)
                {
                    ShowDebugInfo();
                    new MyErrorException(ErrorMessageRFID, ErrorTitleRFID, GetType().Name, method, line);
                    break;
                }
                intervention = GetInterventionSelected();

                if ((prestation.Detail.Date.DayOfYear > DateTime.Now.DayOfYear) && prestation.Detail.Date.Year >= DateTime.Now.Year)
                {
                    ShowDebugInfo();
                    new MyErrorException(ErrorMessageDayOfYear, ErrorTitleDayOfYear, GetType().Name, method, line);
                    break;
                }

                presta = new Prestation(prestation.Detail.PrestationId, intervention, prestation.Detail.HoursCount, employee, prestation.Detail.PrestationTypeId, prestation.Detail.Date, truckCrane, prestation.Detail.HourVehicle, prestation.Detail.KilometerVehicle, prestation.Detail.HourFuel, prestation.Detail.KilometerFuel, prestation.Detail.DateFuel);
                presta.Realized = prestation.Detail.Realized;
                if (presta.Realized && presta.HoursCount > 0.00f)
                {
                    if (!RP.CheckIfPrestationIsAlreadyOnIntervention(presta.InterventionId.interventionId, presta.PrestationId))
                    {
                        RP.InsertNewPrestation(presta);
                        // If prestation type and intervention type = change tires, then insert intervention tire
                        if (presta.PrestationTypeId.PrestationTypeId == (int)PrestationTypeEnum.TIRE_CHANGE && SelectedInterventionItem.Detail.interventionTypeId.InterventionTypeId == (int)InterventionTypeEnum.TIRE_CHANGE)
                            _InsertInterventionTire(SelectedInterventionItem.Detail.interventionId);
                    }
                }
                else if (presta.HoursCount <= 0.00f && presta.Realized)
                {
                    ShowDebugInfo();
                    new MyErrorException(ErrorMessageHoursCount, ErrorTitlePrestation, GetType().Name, method, line);
                    break;
                }
            }

            ReloadAllIntervention(); // Refresh the status of intervention if status is open
            _CleanData();
            SelectedInterventionItem = new DetailViewModel<InterventionModel>(new InterventionModel(intervention));
            DisplayPrestationForIntervention();
        }

        public bool _CanExecuteUpdateCurrentPrestation()
        {
            return SelectedInterventionItem != null && SelectedPrestationItem != null && SelectedBadgeNumberItem != "";
        }

        public ICommand UpdateCurrentPrestation
        {
            get
            {
                return new RelayCommand(_UpdateCurrentPrestation, _CanExecuteUpdateCurrentPrestation);
            }
        }

        /// <summary>
        /// Update the current selected prestation
        /// </summary>
        public void _UpdateCurrentPrestation()
        {
            int truckCrane = 0;
            string badgeNumber = "";
            Employee employee = null;
            Prestation presta = null;

            if (SelectedPrestationItem.Detail.Truck)
                truckCrane = (int)TypeTruckOrCrane.TRUCK;
            else if (SelectedPrestationItem.Detail.Crane)
                truckCrane = (int)TypeTruckOrCrane.CRANE;

            badgeNumber = SelectedBadgeNumberItem;
            employee = RE.GetEmployeeByBadgeNumber(badgeNumber);

            if (employee == null)
            {
                ShowDebugInfo();
                new MyErrorException(ErrorMessageRFID, ErrorTitleRFID, GetType().Name, method, line);
                return;
            }
            Intervention intervention = GetInterventionSelected();
            presta = new Prestation(SelectedPrestationItem.Detail.PrestationId, intervention, SelectedPrestationItem.Detail.HoursCount, employee, SelectedPrestationItem.Detail.PrestationTypeId, SelectedPrestationItem.Detail.Date, truckCrane, SelectedPrestationItem.Detail.HourVehicle, SelectedPrestationItem.Detail.KilometerVehicle, SelectedPrestationItem.Detail.HourFuel, SelectedPrestationItem.Detail.KilometerFuel, SelectedPrestationItem.Detail.DateFuel);
            presta.Realized = SelectedPrestationItem.Detail.Realized;
            presta.Comment = SelectedPrestationItem.Detail.Comment;

            if (!presta.Realized)
            {
                ShowDebugInfo();
                new MyErrorException(ErrorMessageRealized, ErrorTitlePrestation, GetType().Name, method, line);
                return;
            }

            if (presta.HoursCount <= 0.00f)
            {
                ShowDebugInfo();
                new MyErrorException(ErrorMessageHoursCount, ErrorTitlePrestation, GetType().Name, method, line);
                return;
            }

            if ((presta.Date.DayOfYear > DateTime.Now.DayOfYear) && presta.Date.Year >= DateTime.Now.Year)
            {
                ShowDebugInfo();
                new MyErrorException(ErrorMessageDayOfYear, ErrorTitleDayOfYear, GetType().Name, method, line);
                return;
            }

            if (presta.Realized && presta.HoursCount > 0.00f)
                if (RP.CheckIfPrestationIsAlreadyOnIntervention(presta.InterventionId.interventionId, presta.PrestationId))
                {
                    RP.UpdatePrestation(presta);
                    // If prestation type and intervention type = change tires, then insert intervention tire
                    if (presta.PrestationTypeId.PrestationTypeId == (int)PrestationTypeEnum.TIRE_CHANGE && SelectedInterventionItem.Detail.interventionTypeId.InterventionTypeId == (int)InterventionTypeEnum.TIRE_CHANGE)
                    {
                        bool ExistInInterventionTire = false;
                        List<InterventionTire> interventionTireList = RITI.GetAllInterventionTireByIntervention(SelectedInterventionItem.Detail.interventionId);
                        foreach (var interventionTire in interventionTireList)
                        {
                            if (interventionTire.InterventionId == SelectedPrestationItem.Detail.InterventionId.interventionId)
                                ExistInInterventionTire = true;
                        }
                        if (!ExistInInterventionTire)
                            _InsertInterventionTire(SelectedInterventionItem.Detail.interventionId);
                        else
                        {
                            foreach (var interventionTire in interventionTireList)
                            {
                                InterventionTire interTire = null;

                                switch (interventionTire.ChangeCause)
                                {
                                    case (int)ChangeTireCause.Use:
                                        if (SelectedTireUseItem != null && SelectedTireNumberUseItem > 0)
                                            interTire = new InterventionTire(interventionTire.InterventionTireId, SelectedTireNumberUseItem, (int)ChangeTireCause.Use, SelectedPrestationItem.Detail.InterventionId.interventionId, GetSelectedTireUseItem());
                                        break;
                                    case (int)ChangeTireCause.Creve:
                                        if (SelectedTireCreveItem != null && SelectedTireNumberCreveItem > 0)
                                            interTire = new InterventionTire(interventionTire.InterventionTireId, SelectedTireNumberCreveItem, (int)ChangeTireCause.Creve, SelectedPrestationItem.Detail.InterventionId.interventionId, GetSelectedTireCreveItem());
                                        break;
                                    case (int)ChangeTireCause.Jante:
                                        if (SelectedTireJanteItem != null && SelectedTireNumberJanteItem > 0)
                                            interTire = new InterventionTire(interventionTire.InterventionTireId, SelectedTireNumberJanteItem, (int)ChangeTireCause.Jante, SelectedPrestationItem.Detail.InterventionId.interventionId, GetSelectedTireJanteItem());
                                        break;
                                    case (int)ChangeTireCause.Permutation:
                                        if (/*SelectedTirPermutationItem != null &&*/ SelectedTireNumberPermutationItem > 0)
                                            interTire = new InterventionTire(interventionTire.InterventionTireId, SelectedTireNumberPermutationItem, (int)ChangeTireCause.Permutation, SelectedPrestationItem.Detail.InterventionId.interventionId, null);
                                        break;
                                }

                                if (interTire != null)
                                    RITI.UpdateInterventionTire(interTire);
                            }

                            int countFindValueUse = 0;
                            int countFindValueCreve = 0;
                            int countFindValueJante = 0;
                            int countFindValuePermu = 0;

                            InterventionTire interventionTir = null;

                            foreach (var interventionTire in interventionTireList)
                            {
                                if (SelectedTireUseItem != null && SelectedTireNumberUseItem > 0)
                                    if (interventionTire.ChangeCause == (int)ChangeTireCause.Use)
                                        countFindValueUse++;
                                if (SelectedTireCreveItem != null && SelectedTireNumberCreveItem > 0)
                                    if (interventionTire.ChangeCause == (int)ChangeTireCause.Creve)
                                        countFindValueCreve++;
                                if (SelectedTireJanteItem != null && SelectedTireNumberJanteItem > 0)
                                    if (interventionTire.ChangeCause == (int)ChangeTireCause.Jante)
                                        countFindValueJante++;
                                if (/*SelectedTirPermutationItem != null &&*/ SelectedTireNumberPermutationItem > 0)
                                    if (interventionTire.ChangeCause == (int)ChangeTireCause.Permutation)
                                        countFindValuePermu++;
                            }

                            if (SelectedTireUseItem != null && SelectedTireNumberUseItem > 0 && countFindValueUse == 0)
                            {
                                interventionTir = new InterventionTire(SelectedTireNumberUseItem, (int)ChangeTireCause.Use, SelectedPrestationItem.Detail.InterventionId.interventionId, GetSelectedTireUseItem());
                                RITI.InsertNewInterventionTire(interventionTir);
                                interventionTir = null;
                            }
                            else if (SelectedTireUseItem != null && SelectedTireNumberUseItem <= 0)
                            {
                                ShowDebugInfo();
                                new MyErrorException(ErrorMessageTiresCount, ErrorTitleTires, GetType().Name, method, line);
                            }

                            if (SelectedTireCreveItem != null && SelectedTireNumberCreveItem > 0 && countFindValueCreve == 0)
                            {
                                interventionTir = new InterventionTire(SelectedTireNumberCreveItem, (int)ChangeTireCause.Creve, SelectedPrestationItem.Detail.InterventionId.interventionId, GetSelectedTireCreveItem());
                                RITI.InsertNewInterventionTire(interventionTir);
                                interventionTir = null;
                            }
                            else if (SelectedTireCreveItem != null && SelectedTireNumberCreveItem <= 0)
                            {
                                ShowDebugInfo();
                                new MyErrorException(ErrorMessageTiresCount, ErrorTitleTires, GetType().Name, method, line);
                            }

                            if (SelectedTireJanteItem != null && SelectedTireNumberJanteItem > 0 && countFindValueJante == 0)
                            {
                                interventionTir = new InterventionTire(SelectedTireNumberJanteItem, (int)ChangeTireCause.Jante, SelectedPrestationItem.Detail.InterventionId.interventionId, GetSelectedTireJanteItem());
                                RITI.InsertNewInterventionTire(interventionTir);
                                interventionTir = null;
                            }
                            else if (SelectedTireJanteItem != null && SelectedTireNumberJanteItem <= 0)
                            {
                                ShowDebugInfo();
                                new MyErrorException(ErrorMessageTiresCount, ErrorTitleTires, GetType().Name, method, line);
                            }

                            if (/*SelectedTirPermutationItem != null &&*/ SelectedTireNumberPermutationItem > 0 && countFindValuePermu == 0)
                            {
                                interventionTir = new InterventionTire(SelectedTireNumberPermutationItem, (int)ChangeTireCause.Permutation, SelectedPrestationItem.Detail.InterventionId.interventionId, null);
                                RITI.InsertNewInterventionTire(interventionTir);
                                interventionTir = null;
                            }
                        }
                    }
                }

            ReloadAllIntervention(); // Refresh the status of intervention if status is open
            _CleanData();
        }
        #endregion

        #region InterventionTires
        /// <summary>
        /// Get the different element for the intervention tire and call the function for insert data to database
        /// </summary>
        /// <param name="InterventionId"></param>
        public void _InsertInterventionTire(int InterventionId)
        {
            InterventionTire interventionTireUse = null;
            InterventionTire interventionTireCreve = null;
            InterventionTire interventionTireJante = null;
            InterventionTire interventionTirePermutation = null;

            if (SelectedTireUseItem != null && SelectedTireNumberUseItem > 0)
                interventionTireUse = new InterventionTire(SelectedTireNumberUseItem, (int)ChangeTireCause.Use, InterventionId, GetSelectedTireUseItem());
            else if (SelectedTireUseItem != null && SelectedTireNumberUseItem <= 0)
            {
                ShowDebugInfo();
                new MyErrorException(ErrorMessageTiresCount, ErrorTitleTires, GetType().Name, method, line);
            }
            if (SelectedTireCreveItem != null && SelectedTireNumberCreveItem > 0)
                interventionTireCreve = new InterventionTire(SelectedTireNumberCreveItem, (int)ChangeTireCause.Creve, InterventionId, GetSelectedTireCreveItem());
            else if (SelectedTireCreveItem != null && SelectedTireNumberCreveItem <= 0)
            {
                ShowDebugInfo();
                new MyErrorException(ErrorMessageTiresCount, ErrorTitleTires, GetType().Name, method, line);
            }
            if (SelectedTireJanteItem != null && SelectedTireNumberJanteItem > 0)
                interventionTireJante = new InterventionTire(SelectedTireNumberJanteItem, (int)ChangeTireCause.Jante, InterventionId, GetSelectedTireJanteItem());
            else if (SelectedTireJanteItem != null && SelectedTireNumberJanteItem <= 0)
            {
                ShowDebugInfo();
                new MyErrorException(ErrorMessageTiresCount, ErrorTitleTires, GetType().Name, method, line);
            }
            if (/*SelectedTirPermutationItem != null &&*/ SelectedTireNumberPermutationItem > 0)
                interventionTirePermutation = new InterventionTire(SelectedTireNumberPermutationItem, (int)ChangeTireCause.Permutation, InterventionId, null);

            if (interventionTireUse != null)
                RITI.InsertNewInterventionTire(interventionTireUse);
            if (interventionTireCreve != null)
                RITI.InsertNewInterventionTire(interventionTireCreve);
            if (interventionTireJante != null)
                RITI.InsertNewInterventionTire(interventionTireJante);
            if (interventionTirePermutation != null)
                RITI.InsertNewInterventionTire(interventionTirePermutation);
        }

        /// <summary>
        /// Display the data for intervention tire
        /// </summary>
        public void DisplayInterventionTireForIntervention()
        {
            // Clean the data for tire intervention before display
            _CleanDataTire();

            if (SelectedInterventionItem != null)
                if (SelectedInterventionItem.Detail.interventionTypeId.InterventionTypeId == (int)InterventionTypeEnum.TIRE_CHANGE)
                    if (SelectedInterventionItem.Detail.StatusId.Name != TextStatusClose)
                    {
                        foreach (var interventionTire in RITI.GetAllInterventionTireByIntervention(SelectedInterventionItem.Detail.interventionId))
                        {
                            switch (interventionTire.ChangeCause)
                            {
                                case (int)ChangeTireCause.Use:
                                    SelectedTireNumberUseItem = interventionTire.TireNumber;
                                    GetInfoUseTire = interventionTire.NewTireBarcodeId.ItemId.ToString();
                                    SelectedTireUseItem = new DetailViewModel<TiresModel>(new TiresModel(interventionTire.NewTireBarcodeId));
                                    break;
                                case (int)ChangeTireCause.Creve:
                                    SelectedTireNumberCreveItem = interventionTire.TireNumber;
                                    GetInfoCreveTire = interventionTire.NewTireBarcodeId.ItemId.ToString();
                                    SelectedTireCreveItem = new DetailViewModel<TiresModel>(new TiresModel(interventionTire.NewTireBarcodeId));
                                    break;
                                case (int)ChangeTireCause.Jante:
                                    SelectedTireNumberJanteItem = interventionTire.TireNumber;
                                    GetInfoJanteTire = interventionTire.NewTireBarcodeId.ItemId.ToString();
                                    SelectedTireJanteItem = new DetailViewModel<TiresModel>(new TiresModel(interventionTire.NewTireBarcodeId));
                                    break;
                                case (int)ChangeTireCause.Permutation:
                                    SelectedTireNumberPermutationItem = interventionTire.TireNumber;
                                    //GetInfoPermutationTire = interventionTire.NewTireBarcodeId.ItemId.ToString();
                                    //SelectedTirePermutationItem = new DetailViewModel<TiresModel>(new TiresModel(interventionTire.NewTireBarcodeId));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

            SwitchVisibilityTiresInterface(Visibility.Visible);
        }
        #endregion

        #region Barcode Intervention
        /// <summary>
        /// Generate a barcode for intervention
        /// </summary>
        private bool _GenerateBarcodeIntervention(DetailViewModel<InterventionModel> intervention)
        {
            GenerateBarcode gb = new GenerateBarcode();
            string directory = ConfigurationManager.AppSettings["DirectoryBarcodeIntervention"].ToString();
            string format = ConfigurationManager.AppSettings["FormatBarcodeIntervention"].ToString();
            string barcode = "";
            int barcodeInt = 0;

            if (!string.IsNullOrEmpty(directory))
            {
                if (RI.CheckIfExistingBarcodeForIntervention(intervention.Detail.interventionId))
                {
                    // Configure the message box to be displayed
                    string messageBoxText = "Une feuille d'intervention et un code barre existent déjà pour l'intervention choisie.\nVoulez-vous continuer et générer une nouvelle feuille d'intervention et un nouveau code barre ?";
                    string caption = "Feuille d'intervention";
                    MessageBoxButton button = MessageBoxButton.YesNoCancel;
                    MessageBoxImage icon = MessageBoxImage.Warning;

                    // Display message box
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                    // Process message box results
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            {
                                if (File.Exists(directory + intervention.Detail.interventionId.ToString() + "." + format))
                                {
                                    try
                                    {
                                        File.Delete(directory + intervention.Detail.interventionId.ToString() + "." + format);
                                    }
                                    catch (IOException ex)
                                    {
                                        ShowDebugInfo();
                                        throw new MyErrorException(ex.Message, ErrorTitleGenerateBarCode, GetType().Name, method, line);
                                    }
                                }

                                gb.AutoGenerateBarcode(intervention.Detail.interventionId.ToString(), directory, intervention.Detail.interventionId.ToString(), format, out barcode);
                                intervention.Detail.barcodePicture = directory + intervention.Detail.interventionId.ToString() + "." + format;

                                barcode = barcode.Replace("*", "");

                                if (int.TryParse(barcode, out barcodeInt))
                                    RI.UpdateBarcodeIntervention(intervention.Detail.interventionId, barcodeInt);

                                return true;
                            }
                        case MessageBoxResult.No:
                            return false;
                        case MessageBoxResult.Cancel:
                            return false;
                    }
                }
                else
                {
                    gb.AutoGenerateBarcode(intervention.Detail.interventionId.ToString(), directory, intervention.Detail.interventionId.ToString(), format, out barcode);
                    intervention.Detail.barcodePicture = directory + intervention.Detail.interventionId.ToString() + "." + format;

                    barcode = barcode.Replace("*", "");

                    if (int.TryParse(barcode, out barcodeInt))
                        RI.UpdateBarcodeIntervention(intervention.Detail.interventionId, barcodeInt);

                    return true;
                }
            }
            else
            {
                ShowDebugInfo();
                throw new MyErrorException(ErrorMessageGenerateBarcode, ErrorTitleGenerateBarCode, GetType().Name, method, line);
            }

            return false;
        }
        #endregion
    }
}
