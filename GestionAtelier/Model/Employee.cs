using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class Employee
    {
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string BadgeNumber { get; set; }
        public string Company { get; set; }
        public string Comment { get; set; }
        public string Responsible { get; set; }
        public int Status { get; set; }

        /// <summary>
        /// Constructor base employee
        /// </summary>
        /// <param name="employeeId"></param>
        public Employee(string employeeId)
        {
            EmployeeId = employeeId;
        }

        /// <summary>
        /// Constructor Employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="name"></param>
        /// <param name="badgeNumber"></param>
        /// <param name="company"></param>
        /// <param name="comment"></param>
        /// <param name="responsible"></param>
        /// <param name="status"></param>
        public Employee(string employeeId, string name, string badgeNumber, string company, string comment, string responsible, int status)
        {
            EmployeeId = employeeId;
            Name = name;
            BadgeNumber = badgeNumber;
            Company = company;
            Comment = comment;
            Responsible = responsible;
            Status = status;
        }
    }
}
