using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class UserProfileModel : ObservableObject
    {
        private readonly UserProfile _UserProfile;

        public UserProfileModel(UserProfile UserProfile)
        {
            _UserProfile = UserProfile;
            RaisePropertyChanged();
        }

        public int UserProfileId
        {
            get
            {
                return _UserProfile.UserProfileId;
            }
            set
            {
                if (_UserProfile.UserProfileId != value)
                    _UserProfile.UserProfileId = value;
                RaisePropertyChanged(() => UserProfileId);
            }
        }

        public string Name
        {
            get
            {
                return _UserProfile.Name;
            }
            set
            {
                if (_UserProfile.Name != value)
                    _UserProfile.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public int ProfileLevel
        {
            get
            {
                return _UserProfile.ProfileLevel;
            }
            set
            {
                if (_UserProfile.ProfileLevel != value)
                    _UserProfile.ProfileLevel = value;
                RaisePropertyChanged(() => ProfileLevel);
            }
        }

        public ParmSite SiteId
        {
            get
            {
                return _UserProfile.SiteId;
            }
            set
            {
                if (_UserProfile.SiteId != value)
                    _UserProfile.SiteId = value;
                RaisePropertyChanged(() => SiteId);
            }
        }
    }
}
