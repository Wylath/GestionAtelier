using GestionAtelier.Model;
using GestionAtelier.ToolBox;
using GestionAtelier.View;
using GestionAtelier.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GestionAtelier.ViewModel
{
    class AllInvoiceViewModel : ObservableObject
    {
        RequestDBVehicle RV = new RequestDBVehicle();
        RequestDBPrestation RP = new RequestDBPrestation();
        RequestDBEmployee RE = new RequestDBEmployee();
        RequestDBInterventionType RIT = new RequestDBInterventionType();
        RequestDBInterventionTire RITI = new RequestDBInterventionTire();
        RequestDBPrestationType RPT = new RequestDBPrestationType();
        RequestDBInvoice RIV = new RequestDBInvoice();
        
        Window detailInvoiceWindow = null;
        bool AllPrestationChecked = false;

        const string ErrorMessageListEmpty = "Vous n'avez choisis aucune prestation pour la génération de la facture.";
        const string ErrorTitleListEmpty = "Erreur de génération de facture";
        const string ErrorMessageListEmptyInvoice = "Vous n'avez choisis aucune facture pour la regénération.";
        const string ErrorTitleListEmptyInvoice = "Erreur de regénération de facture";
        const string ErrorMessageInfoInvoice = "Vous n'avez pas complété toutes les informations pour la génération de la facture.\nVous devez compléter le numéro de facture, le prix à l'heure, le taux de tva et la pondération sur le total d'heure.";
        const string ErrorTitleInfoInvoice = "Erreur de génération de facture (Info)";
        static string method = "";
        static int line = 0;

        private static void ShowDebugInfo()
        {
            StackFrame stackFrame = new StackFrame(1, true);

            method = stackFrame.GetMethod().ToString();
            line = stackFrame.GetFileLineNumber();
        }

        #region ObservableCollection
        private readonly ObservableCollection<DetailViewModel<PrestationModel>> _Prestation;

        public ObservableCollection<DetailViewModel<PrestationModel>> Prestation
        {
            get
            {
                return _Prestation;
            }
        }

        private readonly ObservableCollection<DetailViewModel<InvoiceModel>> _Invoice;

        public ObservableCollection<DetailViewModel<InvoiceModel>> Invoice
        {
            get
            {
                return _Invoice;
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

        private readonly ObservableCollection<DetailViewModel<EmployeeModel>> _Employee;

        public ObservableCollection<DetailViewModel<EmployeeModel>> Employee
        {
            get
            {
                return _Employee;
            }
        }

        private readonly ObservableCollection<DetailViewModel<PrestationTypeModel>> _PrestationType;

        public ObservableCollection<DetailViewModel<PrestationTypeModel>> PrestationType
        {
            get
            {
                return _PrestationType;
            }
        }

        private readonly ObservableCollection<DetailViewModel<PrestationModel>> _listPrestationForSheet;

        public ObservableCollection<DetailViewModel<PrestationModel>> listPrestationForSheet
        {
            get
            {
                return _listPrestationForSheet;
            }
        }
        #endregion

        #region SeletedItem
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
                    RaisePropertyChanged(() => SelectedPrestationItem);
                }
            }
        }

        private DetailViewModel<InvoiceModel> _SelectedInvoiceItem;

        public DetailViewModel<InvoiceModel> SelectedInvoiceItem
        {
            get
            {
                return _SelectedInvoiceItem;
            }
            set
            {
                if (value != _SelectedInvoiceItem)
                {
                    _SelectedInvoiceItem = value;
                    RaisePropertyChanged(() => SelectedInvoiceItem);
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

        private DetailViewModel<PrestationTypeModel> _SelectedPrestationTypeItem;

        public DetailViewModel<PrestationTypeModel> SelectedPrestationTypeItem
        {
            get
            {
                return _SelectedPrestationTypeItem;
            }
            set
            {
                if (value != _SelectedPrestationTypeItem)
                {
                    _SelectedPrestationTypeItem = value;
                    RaisePropertyChanged(() => SelectedPrestationTypeItem);
                }
            }
        }

        private DetailViewModel<VehicleModel> _SelectedCompanyItem;

        public DetailViewModel<VehicleModel> SelectedCompanyItem
        {
            get
            {
                return _SelectedCompanyItem;
            }
            set
            {
                if (value != _SelectedCompanyItem)
                {
                    _SelectedCompanyItem = value;
                    RaisePropertyChanged(() => SelectedCompanyItem);
                }
            }
        }

        private int _SelectedDayDateInItem;

        public int SelectedDayDateInItem
        {
            get
            {
                return _SelectedDayDateInItem;
            }
            set
            {
                if (value != _SelectedDayDateInItem)
                {
                    _SelectedDayDateInItem = value;
                    RaisePropertyChanged(() => SelectedDayDateInItem);
                }
            }
        }

        private int _SelectedMonthDateInItem;

        public int SelectedMonthDateInItem
        {
            get
            {
                return _SelectedMonthDateInItem;
            }
            set
            {
                if (value != _SelectedMonthDateInItem)
                {
                    _SelectedMonthDateInItem = value;
                    RaisePropertyChanged(() => SelectedMonthDateInItem);
                }
            }
        }

        private int _SelectedYearDateInItem;

        public int SelectedYearDateInItem
        {
            get
            {
                return _SelectedYearDateInItem;
            }
            set
            {
                if (value != _SelectedYearDateInItem)
                {
                    _SelectedYearDateInItem = value;
                    RaisePropertyChanged(() => SelectedYearDateInItem);
                }
            }
        }

        private int _SelectedNumberInvoiceItem;

        public int SelectedNumberInvoiceItem
        {
            get
            {
                return _SelectedNumberInvoiceItem;
            }
            set
            {
                if (value != _SelectedNumberInvoiceItem)
                {
                    _SelectedNumberInvoiceItem = value;
                    RaisePropertyChanged(() => SelectedNumberInvoiceItem);
                }
            }
        }

        private int _SelectedCreateNumberInvoiceItem;

        public int SelectedCreateNumberInvoiceItem
        {
            get
            {
                return _SelectedCreateNumberInvoiceItem;
            }
            set
            {
                if (value != _SelectedCreateNumberInvoiceItem)
                {
                    _SelectedCreateNumberInvoiceItem = value;
                    RaisePropertyChanged(() => SelectedCreateNumberInvoiceItem);
                }
            }
        }

        private int _SelectedPriceInvoiceItem;

        public int SelectedPriceInvoiceItem
        {
            get
            {
                return _SelectedPriceInvoiceItem;
            }
            set
            {
                if (value != _SelectedPriceInvoiceItem)
                {
                    _SelectedPriceInvoiceItem = value;
                    RaisePropertyChanged(() => SelectedPriceInvoiceItem);
                }
            }
        }

        private int _SelectedTVAInvoiceItem;

        public int SelectedTVAInvoiceItem
        {
            get
            {
                return _SelectedTVAInvoiceItem;
            }
            set
            {
                if (value != _SelectedTVAInvoiceItem)
                {
                    _SelectedTVAInvoiceItem = value;
                    RaisePropertyChanged(() => SelectedTVAInvoiceItem);
                }
            }
        }

        private float _SelectedWeightInvoiceItem;

        public float SelectedWeightInvoiceItem
        {
            get
            {
                return _SelectedWeightInvoiceItem;
            }
            set
            {
                if (value != _SelectedWeightInvoiceItem)
                {
                    _SelectedWeightInvoiceItem = value;
                    RaisePropertyChanged(() => SelectedWeightInvoiceItem);
                }
            }
        }

        private bool _SelectedBeforeDate;

        public bool SelectedBeforeDate
        {
            get
            {
                return _SelectedBeforeDate;
            }
            set
            {
                if (value != _SelectedBeforeDate)
                {
                    _SelectedBeforeDate = value;
                    RaisePropertyChanged(() => SelectedBeforeDate);
                }
            }
        }
        #endregion

        public AllInvoiceViewModel()
        {
            _Prestation = new ObservableCollection<DetailViewModel<PrestationModel>>();
            _Invoice = new ObservableCollection<DetailViewModel<InvoiceModel>>();
            _Vehicle = new ObservableCollection<DetailViewModel<VehicleModel>>();
            _InterventionType = new ObservableCollection<DetailViewModel<InterventionTypeModel>>();
            _Employee = new ObservableCollection<DetailViewModel<EmployeeModel>>();
            _PrestationType = new ObservableCollection<DetailViewModel<PrestationTypeModel>>();
            _listPrestationForSheet = new ObservableCollection<DetailViewModel<PrestationModel>>();

            foreach (var prestation in RP.GetAllPrestations(true))
            {
                _Prestation.Add(new DetailViewModel<PrestationModel>(new PrestationModel(prestation)));
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
                _Employee.Add(new DetailViewModel<EmployeeModel>(new EmployeeModel(employee)));
            }

            foreach (var prestationType in RPT.SelectAllElement())
            {
                _PrestationType.Add(new DetailViewModel<PrestationTypeModel>(new PrestationTypeModel(prestationType)));
            }

            foreach (var invoice in RIV.SelectAllElement())
            {
                _Invoice.Add(new DetailViewModel<InvoiceModel>(new InvoiceModel(invoice)));
            }
        }

        /// <summary>
        /// Reload all prestations in observableCollection prestations
        /// </summary>
        public void ReloadAllPrestation()
        {
            _Prestation.Clear();

            foreach (var prestation in RP.GetAllPrestations(true))
            {
                _Prestation.Add(new DetailViewModel<PrestationModel>(new PrestationModel(prestation)));
            }
        }

        /// <summary>
        /// Reload all invoices in observableCollection invoices
        /// </summary>
        public void ReloadAllInvoice()
        {
            _Invoice.Clear();

            foreach (var invoice in RIV.SelectAllElement())
            {
                _Invoice.Add(new DetailViewModel<InvoiceModel>(new InvoiceModel(invoice)));
            }
        }

        /// <summary>
        /// Reset all selected items
        /// </summary>
        public void ClearSelectedItem()
        {
            SelectedVehicleItem = null;
            SelectedPrestationTypeItem = null;
            SelectedApplicantItem = null;
            SelectedInterventionTypeItem = null;
            SelectedCompanyItem = null;
            SelectedMonthDateInItem = 0;
            SelectedYearDateInItem = 0;
            SelectedDayDateInItem = 0;
            SelectedBeforeDate = false;
        }

        /// <summary>
        /// Reset all selected items invoice
        /// </summary>
        public void ClearSelectedItemInvoice()
        {
            SelectedNumberInvoiceItem = 0;
        }

        /// <summary>
        /// Reset all selected items for generate the invoice
        /// </summary>
        public void ClearSelectedItemGenerateInvoice()
        {
            SelectedCreateNumberInvoiceItem = 0;
            SelectedPriceInvoiceItem = 0;
            SelectedTVAInvoiceItem = 0;
            SelectedWeightInvoiceItem = 0.00f;
        }

        #region ObjectClassBySelectedEntity
        /// <summary>
        /// Return the selected vehicle
        /// </summary>
        /// <returns></returns>
        public Vehicle GetSelectedVehicle()
        {
            Vehicle vehicle = null;
            if (SelectedVehicleItem != null)
                vehicle = new Vehicle(SelectedVehicleItem.Detail.VehicleId);
            return vehicle;
        }

        /// <summary>
        /// Return the selected intervention type
        /// </summary>
        /// <returns></returns>
        public InterventionType GetSelectedInterventionType()
        {
            InterventionType interventionType = null;
            if (SelectedInterventionTypeItem != null)
                interventionType = new InterventionType(SelectedInterventionTypeItem.Detail.InterventionTypeId);
            return interventionType;
        }

        /// <summary>
        /// Return the selected employee
        /// </summary>
        /// <returns></returns>
        public Employee GetSelectedEmployee()
        {
            Employee employee = null;
            if (SelectedApplicantItem != null)
                employee = new Employee(SelectedApplicantItem.Detail.EmployeeId);
            return employee;
        }

        /// <summary>
        /// Return the selected prestation type
        /// </summary>
        /// <returns></returns>
        public PrestationType GetSelectedPrestationType()
        {
            PrestationType prestationType = null;
            if (SelectedPrestationTypeItem != null)
                prestationType = new PrestationType(SelectedPrestationTypeItem.Detail.PrestationTypeId, SelectedPrestationTypeItem.Detail.Name);
            return prestationType;
        }

        /// <summary>
        /// Return the selected company
        /// </summary>
        /// <returns></returns>
        public string GetSelectedCompany()
        {
            string company = "";
            if (SelectedCompanyItem != null)
                company = SelectedCompanyItem.Detail.CompanyId;
            return company;
        }

        /// <summary>
        /// Return the selected date
        /// </summary>
        /// <returns></returns>
        public DateTime GetSelectedDate()
        {
            DateTime datePrestation = new DateTime();

            if (SelectedMonthDateInItem != 0 && SelectedDayDateInItem == 0 && SelectedYearDateInItem == 0)
                datePrestation = new DateTime(1, SelectedMonthDateInItem, 1);
            if (SelectedMonthDateInItem == 0 && SelectedDayDateInItem != 0 && SelectedYearDateInItem == 0)
                datePrestation = new DateTime(1, 1, SelectedDayDateInItem);
            if (SelectedMonthDateInItem == 0 && SelectedDayDateInItem == 0 && SelectedYearDateInItem != 0)
                datePrestation = new DateTime(SelectedYearDateInItem, 1, 1);
            if (SelectedMonthDateInItem != 0 && SelectedDayDateInItem == 0 && SelectedYearDateInItem != 0)
                datePrestation = new DateTime(SelectedYearDateInItem, SelectedMonthDateInItem, 1);
            if (SelectedMonthDateInItem == 0 && SelectedDayDateInItem != 0 && SelectedYearDateInItem != 0)
                datePrestation = new DateTime(1, SelectedMonthDateInItem, SelectedDayDateInItem);
            if (SelectedMonthDateInItem != 0 && SelectedDayDateInItem == 0 && SelectedYearDateInItem != 0)
                datePrestation = new DateTime(SelectedYearDateInItem, SelectedMonthDateInItem, 1);
            if (SelectedMonthDateInItem == 0 && SelectedDayDateInItem != 0 && SelectedYearDateInItem != 0)
                datePrestation = new DateTime(SelectedYearDateInItem, 1, SelectedDayDateInItem);
            if (SelectedMonthDateInItem != 0 && SelectedDayDateInItem != 0 && SelectedYearDateInItem != 0)
                datePrestation = new DateTime(SelectedYearDateInItem, SelectedMonthDateInItem, SelectedDayDateInItem);

            return datePrestation;
        }

        /// <summary>
        /// Return the number invoice
        /// </summary>
        /// <returns></returns>
        public int GetSelectedNumberInvoice()
        {
            return (SelectedNumberInvoiceItem != 0) ? SelectedNumberInvoiceItem : 0;
        }
        #endregion

        #region command
        public ICommand FilterPrestation
        {
            get
            {
                return new RelayCommand(_FilterPrestation, null);
            }
        }

        /// <summary>
        /// Filter for the prestations
        /// </summary>
        public void _FilterPrestation()
        {
            Vehicle vehicle = null;
            InterventionType interventionType = null;
            PrestationType prestationType = null;
            Employee employee = null;
            string company = "";
            DateTime datePrestation = new DateTime();

            vehicle = GetSelectedVehicle();
            interventionType = GetSelectedInterventionType();
            prestationType = GetSelectedPrestationType();
            employee = GetSelectedEmployee();
            company = GetSelectedCompany();
            datePrestation = GetSelectedDate();

            List<DetailViewModel<PrestationModel>> prestationRemove = new List<DetailViewModel<PrestationModel>>();

            foreach (var prestation in _Prestation)
            {
                if (vehicle != null)
                    if (prestation.Detail.InterventionId.vehicleId.VehicleId != vehicle.VehicleId)
                        prestationRemove.Add(prestation);

                if (interventionType != null)
                    if (prestation.Detail.InterventionId.interventionTypeId.InterventionTypeId != interventionType.InterventionTypeId)
                        prestationRemove.Add(prestation);

                if (prestationType != null)
                    if (prestation.Detail.PrestationTypeId.PrestationTypeId != prestationType.PrestationTypeId)
                        prestationRemove.Add(prestation);

                if (employee != null)
                    if (prestation.Detail.InterventionId.Applicant.EmployeeId != employee.EmployeeId)
                        prestationRemove.Add(prestation);

                if (!string.IsNullOrEmpty(company))
                    if (prestation.Detail.InterventionId.vehicleId.CompanyId != company)
                        prestationRemove.Add(prestation);

                if (SelectedMonthDateInItem != 0 && !SelectedBeforeDate)
                {
                    if (prestation.Detail.Date.Month != datePrestation.Month)
                        prestationRemove.Add(prestation);
                }
                else if (SelectedMonthDateInItem != 0 && SelectedBeforeDate)
                {
                    if (prestation.Detail.Date.Month >= datePrestation.Month)
                        prestationRemove.Add(prestation);
                }

                if (SelectedYearDateInItem != 0 && !SelectedBeforeDate)
                {
                    if (prestation.Detail.Date.Year != datePrestation.Year)
                        prestationRemove.Add(prestation);
                }
                else if (SelectedYearDateInItem != 0 && SelectedBeforeDate)
                {
                    if (prestation.Detail.Date.Year >= datePrestation.Year)
                        prestationRemove.Add(prestation);
                }

                if (SelectedDayDateInItem != 0 && !SelectedBeforeDate)
                {
                    if (prestation.Detail.Date.Day != datePrestation.Day)
                        prestationRemove.Add(prestation);
                }
                else if (SelectedDayDateInItem != 0 && SelectedBeforeDate)
                {
                    if (prestation.Detail.Date.Day >= datePrestation.Day)
                        prestationRemove.Add(prestation);
                }
            }

            foreach (var prestation in prestationRemove)
            {
                _Prestation.Remove(prestation);
            }
        }

        public ICommand ResetFilterPrestation
        {
            get
            {
                return new RelayCommand(_ResetFilterPrestation, null);
            }
        }

        /// <summary>
        /// Reset filter prestation
        /// </summary>
        public void _ResetFilterPrestation()
        {
            _Prestation.Clear();

            foreach (var prestation in RP.GetAllPrestations(true))
            {
                _Prestation.Add(new DetailViewModel<PrestationModel>(new PrestationModel(prestation)));
            }

            ClearSelectedItem();
        }

        public ICommand FilterInvoice
        {
            get
            {
                return new RelayCommand(_FilterInvoice, null);
            }
        }

        /// <summary>
        /// Filter for the Invoices
        /// </summary>
        public void _FilterInvoice()
        {
            Vehicle vehicle = null;
            InterventionType interventionType = null;
            PrestationType prestationType = null;
            Employee employee = null;
            string company = "";
            DateTime datePrestation = new DateTime();
            int numberInvoice = 0;

            vehicle = GetSelectedVehicle();
            interventionType = GetSelectedInterventionType();
            prestationType = GetSelectedPrestationType();
            employee = GetSelectedEmployee();
            company = GetSelectedCompany();
            datePrestation = GetSelectedDate();
            numberInvoice = GetSelectedNumberInvoice();

            List<DetailViewModel<InvoiceModel>> InvoiceRemove = new List<DetailViewModel<InvoiceModel>>();

            foreach (var invoice in _Invoice)
            {
                if (vehicle != null)
                {
                    foreach (var prestation in invoice.Detail.prestation)
                    {
                        if (prestation.InterventionId.vehicleId.VehicleId != vehicle.VehicleId)
                            InvoiceRemove.Add(invoice);
                    }
                }     

                if (interventionType != null)
                {
                    foreach (var prestation in invoice.Detail.prestation)
                    {
                        if (prestation.InterventionId.interventionTypeId.InterventionTypeId != interventionType.InterventionTypeId)
                            InvoiceRemove.Add(invoice);
                    }
                }       

                if (prestationType != null)
                {
                    foreach (var prestation in invoice.Detail.prestation)
                    {
                        if (prestation.PrestationTypeId.PrestationTypeId != prestationType.PrestationTypeId)
                            InvoiceRemove.Add(invoice);
                    }
                }          

                if (employee != null)
                {
                    foreach (var prestation in invoice.Detail.prestation)
                    {
                        if (prestation.InterventionId.Applicant.EmployeeId != employee.EmployeeId)
                            InvoiceRemove.Add(invoice);
                    }
                } 

                if (!string.IsNullOrEmpty(company))
                {
                    foreach (var prestation in invoice.Detail.prestation)
                    {
                        if (prestation.InterventionId.vehicleId.CompanyId != company)
                            InvoiceRemove.Add(invoice);
                    }
                }  

                if (SelectedMonthDateInItem != 0)
                    if (invoice.Detail.DateInvoice.Month != datePrestation.Month)
                        InvoiceRemove.Add(invoice);

                if (SelectedYearDateInItem != 0)
                    if (invoice.Detail.DateInvoice.Year != datePrestation.Year)
                        InvoiceRemove.Add(invoice);

                if (SelectedDayDateInItem != 0)
                    if (invoice.Detail.DateInvoice.Day != datePrestation.Day)
                        InvoiceRemove.Add(invoice);

                if (numberInvoice != 0)
                    if (invoice.Detail.NumberInvoice != numberInvoice)
                        InvoiceRemove.Add(invoice);
            }

            foreach (var invoice in InvoiceRemove)
            {
                _Invoice.Remove(invoice);
            }
        }

        public ICommand ResetFilterInvoice
        {
            get
            {
                return new RelayCommand(_ResetFilterInvoice, null);
            }
        }

        /// <summary>
        /// Reset filter invoice
        /// </summary>
        public void _ResetFilterInvoice()
        {
            _Invoice.Clear();

            foreach (var invoice in RIV.SelectAllElement())
            {
                _Invoice.Add(new DetailViewModel<InvoiceModel>(new InvoiceModel(invoice)));
            }

            ClearSelectedItem();
            ClearSelectedItemInvoice();
        }

        public ICommand GenerateInvoice
        {
            get
            {
                return new RelayCommand(_GenerateInvoice, null);
            }
        }

        /// <summary>
        /// Generate the invoices
        /// </summary>
        public void _GenerateInvoice()
        {
            Window gdi = new GenerateDocInvoice();
            int numberInvoice = 0;
            int priceInvoice = 0;
            int tvaInvoice = 0;
            float weightInvoice = 0.0f;
            DateTime dateInvoice = DateTime.Now;
            Invoice invoice = null;
            List<Prestation> prestationList = new List<Prestation>();

            numberInvoice = SelectedCreateNumberInvoiceItem;
            priceInvoice = SelectedPriceInvoiceItem;
            tvaInvoice = SelectedTVAInvoiceItem;
            weightInvoice = SelectedWeightInvoiceItem;

            if (numberInvoice > 0 && priceInvoice > 0 && tvaInvoice > 0 && weightInvoice > 0.0f)
                invoice = new Invoice(weightInvoice, tvaInvoice, priceInvoice, numberInvoice, dateInvoice);

            if (invoice != null)
            {
                foreach (var prestation in Prestation)
                {
                    if (prestation.Detail.IsCheckedForGenerateSheet)
                    {
                        Prestation presta = new Prestation(prestation.Detail.PrestationId, prestation.Detail.InterventionId, prestation.Detail.HoursCount, prestation.Detail.EmployeeId, prestation.Detail.PrestationTypeId, prestation.Detail.Date);
                        prestationList.Add(presta);

                        _listPrestationForSheet.Add(prestation);
                    }
                }

                invoice.TotalPrice = invoice.Price * invoice.Ponderation;
                invoice.ValuePriceTva = (invoice.TotalPrice / 100) * invoice.TVA;
                invoice.TotalPriceWithTva = invoice.TotalPrice + ((invoice.TotalPrice / 100) * invoice.TVA);

                if (!_listPrestationForSheet.Any() || !prestationList.Any())
                {
                    ShowDebugInfo();
                    new MyErrorException(ErrorMessageListEmpty, ErrorTitleListEmpty, GetType().Name, method, line);
                }

                SelectedInvoiceItem = new DetailViewModel<InvoiceModel>(new InvoiceModel(invoice));

                RIV.InsertNewInvoice(invoice, prestationList);

                ReloadAllPrestation();
                ReloadAllInvoice();

                ClearSelectedItemGenerateInvoice();

                gdi.DataContext = this;
                gdi.Show();
            }
            else
            {
                ShowDebugInfo();
                new MyErrorException(ErrorMessageInfoInvoice, ErrorTitleInfoInvoice, GetType().Name, method, line);
            }
        }

        public bool _CanExecuteRegenerateInvoice()
        {
            return SelectedInvoiceItem != null;
        }

        public ICommand RegenerateInvoice
        {
            get
            {
                return new RelayCommand(_RegenerateInvoice, _CanExecuteRegenerateInvoice);
            }
        }

        /// <summary>
        /// Regenerate the invoices
        /// </summary>
        public void _RegenerateInvoice()
        {
            _listPrestationForSheet.Clear();

            Window gdi = new GenerateDocInvoice();
            Invoice invoice = null;

            invoice = new Invoice(SelectedInvoiceItem.Detail.InvoiceId, SelectedInvoiceItem.Detail.Ponderation, SelectedInvoiceItem.Detail.TVA, SelectedInvoiceItem.Detail.Price, SelectedInvoiceItem.Detail.NumberInvoice, SelectedInvoiceItem.Detail.DateInvoice);

            if (invoice != null)
            {
                foreach (var invoi in _Invoice)
                {
                    if (invoi.Detail.InvoiceId == invoice.InvoiceId)
                    {
                        foreach (var prestation in invoi.Detail.prestation)
                        {
                            _listPrestationForSheet.Add(new DetailViewModel<PrestationModel>(new PrestationModel(prestation)));
                        }
                        break;
                    }
                }

                if (!_listPrestationForSheet.Any())
                {
                    ShowDebugInfo();
                    new MyErrorException(ErrorMessageListEmptyInvoice, ErrorTitleListEmptyInvoice, GetType().Name, method, line);
                }

                SelectedInvoiceItem.Detail.TotalPrice = SelectedInvoiceItem.Detail.Price * SelectedInvoiceItem.Detail.Ponderation;
                SelectedInvoiceItem.Detail.ValuePriceTva = (SelectedInvoiceItem.Detail.TotalPrice / 100) * SelectedInvoiceItem.Detail.TVA;
                SelectedInvoiceItem.Detail.TotalPriceWithTva = SelectedInvoiceItem.Detail.TotalPrice + ((SelectedInvoiceItem.Detail.TotalPrice / 100) * SelectedInvoiceItem.Detail.TVA);

                gdi.DataContext = this;
                gdi.Show();
            }
        }

        public bool _CanExecuteDisplayDetailInvoice()
        {
            return SelectedInvoiceItem != null;
        }

        public ICommand DisplayDetailInvoice
        {
            get
            {
                return new RelayCommand(_DisplayDetailInvoice, _CanExecuteDisplayDetailInvoice);
            }
        }

        /// <summary>
        /// Display the details for the selected invoice
        /// </summary>
        public void _DisplayDetailInvoice()
        {
            detailInvoiceWindow = new DetailInvoice();
            detailInvoiceWindow.DataContext = SelectedInvoiceItem.Detail.prestation;
            detailInvoiceWindow.Show();
        }

        public ICommand CloseDetailInvoice
        {
            get
            {
                return new RelayCommand(_CloseDetailInvoice, null);
            }
        }

        /// <summary>
        /// Close window for the details invoice
        /// </summary>
        public void _CloseDetailInvoice()
        {
            detailInvoiceWindow.Close();
        }

        public ICommand SelectAllPrestationForGenerateInvoice
        {
            get
            {
                return new RelayCommand(_SelectAllPrestationForGenerateInvoice, null);
            }
        }

        /// <summary>
        /// Selected all prestations for generate the invoice
        /// </summary>
        public void _SelectAllPrestationForGenerateInvoice()
        {
            AllPrestationChecked = (!AllPrestationChecked) ? true : false;

            foreach (var invoice in _Prestation)
            {
                invoice.Detail.IsCheckedForGenerateSheet = AllPrestationChecked;
            }
        }
        #endregion
    }
}
