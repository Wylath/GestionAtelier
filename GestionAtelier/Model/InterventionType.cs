using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class InterventionType
    {
        public int InterventionTypeId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        /// <summary>
        /// Constructor base interventiontype
        /// </summary>
        /// <param name="interventionTypeId"></param>
        public InterventionType(int interventionTypeId)
        {
            InterventionTypeId = interventionTypeId;
        }

        /// <summary>
        /// Constructor InterventionType
        /// </summary>
        /// <param name="interventionTypeId"></param>
        /// <param name="name"></param>
        public InterventionType(int interventionTypeId, string name)
        {
            InterventionTypeId = interventionTypeId;
            Name = name;
        }

        /// <summary>
        /// Constructor InterventionType with status
        /// </summary>
        /// <param name="interventionTypeId"></param>
        /// <param name="name"></param>
        /// <param name="status"></param>
        public InterventionType(int interventionTypeId, string name, int status) : this(interventionTypeId, name)
        {
            Status = status;
        }
    }
}
