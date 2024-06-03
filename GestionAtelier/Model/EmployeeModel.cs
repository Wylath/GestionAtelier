using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class EmployeeModel : ObservableObject
    {
        private readonly Employee _Employee;

        public EmployeeModel(Employee Employee)
        {
            _Employee = Employee;
            RaisePropertyChanged();
        }

        public string EmployeeId
        {
            get
            {
                return _Employee.EmployeeId;
            }
            set
            {
                if (_Employee.EmployeeId != value)
                    _Employee.EmployeeId = value;
                RaisePropertyChanged(() => EmployeeId);
            }
        }

        public string Name
        {
            get
            {
                return _Employee.Name;
            }
            set
            {
                if (_Employee.Name != value)
                    _Employee.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public string BadgeNumber
        {
            get
            {
                return _Employee.BadgeNumber;
            }
            set
            {
                if (_Employee.BadgeNumber != value)
                    _Employee.BadgeNumber = value;
                RaisePropertyChanged(() => BadgeNumber);
            }
        }

        public string Company
        {
            get
            {
                return _Employee.Company;
            }
            set
            {
                if (_Employee.Company != value)
                    _Employee.Company = value;
                RaisePropertyChanged(() => Company);
            }
        }

        public string Comment
        {
            get
            {
                return _Employee.Comment;
            }
            set
            {
                if (_Employee.Comment != value)
                    _Employee.Comment = value;
                RaisePropertyChanged(() => Comment);
            }
        }

        public string Responsible
        {
            get
            {
                return _Employee.Responsible;
            }
            set
            {
                if (_Employee.Responsible != value)
                    _Employee.Responsible = value;
                RaisePropertyChanged(() => Responsible);
            }
        }

        public int Status
        {
            get
            {
                return _Employee.Status;
            }
            set
            {
                if (_Employee.Status != value)
                    _Employee.Status = value;
                RaisePropertyChanged(() => Status);
            }
        }
    }
}
