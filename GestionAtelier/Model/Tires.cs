using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class Tires
    {
        public int RECID { get; set; }
        public string CompanyId { get; set; }
        public string ItemId { get; set; }
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string BarCode { get; set; }
        public string Comment { get; set; }
        public string ItemType { get; set; }

        /// <summary>
        /// Constructor Tires
        /// </summary>
        /// <param name="RECID"></param>
        public Tires(int RECID)
        {
            this.RECID = RECID;
        }

        /// <summary>
        /// Constructor Tires (pneus)
        /// </summary>
        /// <param name="rECID"></param>
        /// <param name="companyId"></param>
        /// <param name="itemId"></param>
        /// <param name="dim1"></param>
        /// <param name="dim2"></param>
        /// <param name="dim3"></param>
        /// <param name="barCode"></param>
        /// <param name="comment"></param>
        public Tires(int rECID, string companyId, string itemId, string dim1, string dim2, string dim3, string barCode, string comment, string itemType)
        {
            RECID = rECID;
            CompanyId = companyId;
            ItemId = itemId;
            Dim1 = dim1;
            Dim2 = dim2;
            Dim3 = dim3;
            BarCode = barCode;
            Comment = comment;
            ItemType = itemType;
        }
    }
}
