using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class UserModel : ObservableObject
    {
        private readonly User _User;

        public UserModel(User User)
        {
            _User = User;
            RaisePropertyChanged();
        }

        public int UserId
        {
            get
            {
                return _User.UserId;
            }
            set
            {
                if (_User.UserId != value)
                    _User.UserId = value;
                RaisePropertyChanged(() => UserId);
            }
        }

        public int Active
        {
            get
            {
                return _User.Active;
            }
            set
            {
                if (_User.Active != value)
                    _User.Active = value;
                RaisePropertyChanged(() => Active);
            }
        }

        public string EmployeeId
        {
            get
            {
                return _User.EmployeeId;
            }
            set
            {
                if (_User.EmployeeId != value)
                    _User.EmployeeId = value;
                RaisePropertyChanged(() => EmployeeId);
            }
        }
    }
}
