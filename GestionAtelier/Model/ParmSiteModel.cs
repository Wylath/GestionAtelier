using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class ParmSiteModel : ObservableObject
    {
        private readonly ParmSite _ParmSite;

        public ParmSiteModel(ParmSite ParmSite)
        {
            _ParmSite = ParmSite;
            RaisePropertyChanged();
        }

        public string SiteId
        {
            get
            {
                return _ParmSite.SiteId;
            }
            set
            {
                if (_ParmSite.SiteId != value)
                    _ParmSite.SiteId = value;
                RaisePropertyChanged(() => SiteId);
            }
        }

        public string Name
        {
            get
            {
                return _ParmSite.Name;
            }
            set
            {
                if (_ParmSite.Name != value)
                    _ParmSite.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public int Status
        {
            get
            {
                return _ParmSite.Status;
            }
            set
            {
                if (_ParmSite.Status != value)
                    _ParmSite.Status = value;
                RaisePropertyChanged(() => Status);
            }
        }

        public string CompanyID
        {
            get
            {
                return _ParmSite.CompanyID;
            }
            set
            {
                if (_ParmSite.CompanyID != value)
                    _ParmSite.CompanyID = value;
                RaisePropertyChanged(() => CompanyID);
            }
        }
    }
}
