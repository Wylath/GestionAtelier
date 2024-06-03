using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class ParmSite
    {
        public string SiteId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string CompanyID { get; set; }

        /// <summary>
        /// Constructor ParmSite (Garage)
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="name"></param>
        public ParmSite(string siteId, string name)
        {
            SiteId = siteId;
            Name = name;
        }

        /// <summary>
        /// Constructor ParmSite
        /// </summary>
        /// <param name="siteId"></param>
        public ParmSite(string siteId)
        {
            SiteId = siteId;
        }

        /// <summary>
        /// Constructor ParmSite (Garage)
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="name"></param>
        /// <param name="status"></param>
        public ParmSite(string siteId, string name, int status) : this(siteId, name)
        {
            Status = status;
        }

        /// <summary>
        /// Constructor ParmSite With CompanyID
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="name"></param>
        /// <param name="status"></param>
        /// <param name="companyID"></param>
        public ParmSite(string siteId, string name, int status, string companyID) : this(siteId, name, status)
        {
            CompanyID = companyID;
        }
    }
}
