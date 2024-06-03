using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class Priority
    {
        public enum Status { Basse = 1, Normal = 2, Urgent = 3 };

        public string Name { get; set; }

        /// <summary>
        /// Basse = 1, Normal = 2, Urgent = 3
        /// </summary>
        /// <param name="Priority"></param>
        public Priority(int Priority)
        {
            Status prio = (Status)Priority;
            Name = prio.ToString();
        }

        public Priority(string Priority)
        {
            Name = Priority;
        }

        public Priority(InterventionType interventionType)
        {
            int Priority = 1;
            switch(interventionType.InterventionTypeId)
            {
                case 2: // GE
                case 3: // CT
                case 5: // Change Tires
                    Priority = 2;
                    break;
                case 4: // Panne
                    Priority = 3;
                    break;
                default:
                    Priority = 1;
                    break;
            }
            Status prio = (Status)Priority;
            Name = prio.ToString();
        }

        public int GetIntNamePriority(string Name)
        {
            switch(Name)
            {
                case "Basse":
                    return 1;
                case "Normal":
                    return 2;
                case "Urgent":
                    return 3;
                default:
                    return 4;
            }
        }
    }
}
