using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class PrestationType
    {
        public int PrestationTypeId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        /// <summary>
        /// Constructor PrestationType
        /// </summary>
        /// <param name="prestationTypeId"></param>
        /// <param name="name"></param>
        public PrestationType(int prestationTypeId, string name)
        {
            PrestationTypeId = prestationTypeId;
            Name = name;
        }
    }
}
