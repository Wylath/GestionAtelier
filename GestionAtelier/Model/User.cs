using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class User
    {
        public int UserId { get; set; }
        public int Active { get; set; }
        public string EmployeeId { get; set; }

        /// <summary>
        /// Constructor User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="active"></param>
        /// <param name="employeeId"></param>
        public User(int userId, int active, string employeeId)
        {
            UserId = userId;
            Active = active;
            EmployeeId = employeeId;
        }
    }
}
