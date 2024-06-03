using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class Status
    {
        public int StatusId { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Constructor Status
        /// </summary>
        /// <param name="statusId"></param>
        /// <param name="name"></param>
        public Status(int statusId, string name)
        {
            StatusId = statusId;
            Name = name;
        }

        /// <summary>
        /// Constructor status
        /// </summary>
        /// <param name="statusId"></param>
        public Status(int statusId)
        {
            StatusId = statusId;
        }
    }
}
