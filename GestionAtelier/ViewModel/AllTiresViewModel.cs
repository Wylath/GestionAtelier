using GestionAtelier.DB;
using GestionAtelier.Model;
using GestionAtelier.ToolBox;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GestionAtelier.ViewModel
{
    class AllTiresViewModel : ObservableObject
    {
        RequestDBTires RT = new RequestDBTires();
        static Window window = null;
        AllInterventionViewModel _AllIntervention = null;

        public enum TiresType { TIRE = 1, WHEEL = 2 };

        private readonly ObservableCollection<DetailViewModel<TiresModel>> _Tires;

        public ObservableCollection<DetailViewModel<TiresModel>> Tires
        {
            get
            {
                return _Tires;
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
                    switch(_AllIntervention.TiresTypeForOpenWindow)
                    {
                        case (int)AllInterventionViewModel.ChangeTireCause.Creve:
                            _AllIntervention.SelectedTireCreveItem = value;
                            break;
                        case (int)AllInterventionViewModel.ChangeTireCause.Jante:
                            _AllIntervention.SelectedTireJanteItem = value;
                            break;
                        case (int)AllInterventionViewModel.ChangeTireCause.Use:
                            _AllIntervention.SelectedTireUseItem = value;
                            break;
                        case (int)AllInterventionViewModel.ChangeTireCause.Permutation:
                            _AllIntervention.SelectedTirePermutationItem = value;
                            break;
                        default:
                            break;
                    }
                    _SelectedTiresItem = value;
                    RaisePropertyChanged(() => SelectedTiresItem);
                }
            }
        }

        public AllTiresViewModel()
        {
            _Tires = new ObservableCollection<DetailViewModel<TiresModel>>();

            foreach (var tires in RT.SelectAllElement())
            {
                _Tires.Add(new DetailViewModel<TiresModel>(new TiresModel(tires)));
            }
        }

        public AllTiresViewModel(Window win, UserProfile userProfile, AllInterventionViewModel AllIntervention)
        {
            _AllIntervention = AllIntervention;
            _Tires = new ObservableCollection<DetailViewModel<TiresModel>>();
            if (win != null)
                window = win;

            foreach (var tires in RT.SelectAllTiresByCompanyId(userProfile.SiteId.CompanyID))
            {
                switch (_AllIntervention.TiresTypeForOpenWindow)
                {
                    case (int)AllInterventionViewModel.ChangeTireCause.Creve:
                        if(tires.ItemType == TiresType.TIRE.ToString())
                            _Tires.Add(new DetailViewModel<TiresModel>(new TiresModel(tires)));
                        break;
                    case (int)AllInterventionViewModel.ChangeTireCause.Jante:
                        if (tires.ItemType == TiresType.WHEEL.ToString())
                            _Tires.Add(new DetailViewModel<TiresModel>(new TiresModel(tires)));
                        break;
                    case (int)AllInterventionViewModel.ChangeTireCause.Use:
                        if (tires.ItemType == TiresType.TIRE.ToString())
                            _Tires.Add(new DetailViewModel<TiresModel>(new TiresModel(tires)));
                        break;
                    default:
                        break;
                }
                //_Tires.Add(new DetailViewModel<TiresModel>(new TiresModel(tires)));
            }
        }

        public ICommand CloseTireWindow
        {
            get
            {
                return new RelayCommand(_CloseTireWindow, null);
            }
        }

        /// <summary>
        /// Close tires window
        /// </summary>
        public void _CloseTireWindow()
        {
            if (window != null)
                window.Close();
        }
    }
}
