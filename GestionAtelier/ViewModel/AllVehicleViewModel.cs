using GestionAtelier.Model;
using GestionAtelier.ToolBox;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GestionAtelier.ViewModel
{
    class AllVehicleViewModel : ObservableObject
    {
        RequestDBVehicle RV = new RequestDBVehicle();
        static Window window = null;

        private readonly ObservableCollection<DetailViewModel<VehicleModel>> _Vehicle;

        public ObservableCollection<DetailViewModel<VehicleModel>> Vehicle
        {
            get
            {
                return _Vehicle;
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

        public AllVehicleViewModel()
        {
            _Vehicle = new ObservableCollection<DetailViewModel<VehicleModel>>();

            foreach (var vehicle in RV.SelectAllElement())
            {
                _Vehicle.Add(new DetailViewModel<VehicleModel>(new VehicleModel(vehicle)));
            }
        }

        public AllVehicleViewModel(Window win)
        {
            _Vehicle = new ObservableCollection<DetailViewModel<VehicleModel>>();
            if (win != null)
                window = win;

            foreach (var vehicle in RV.SelectAllElement())
            {
                _Vehicle.Add(new DetailViewModel<VehicleModel>(new VehicleModel(vehicle)));
            }
        }

        public ICommand CloseVehicleWindow
        {
            get
            {
                return new RelayCommand(_CloseVehicleWindow, null);
            }
        }

        /// <summary>
        /// Close vehicle window
        /// </summary>
        public void _CloseVehicleWindow()
        {
            if (window != null)
                window.Close();
        }
    }
}
