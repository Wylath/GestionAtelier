using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class StatusHistory
    {
        public int StatusHistoryId { get; set; }
        public DateTime StatusHistoryDate { get; set; }
        public int InterventionId { get; set; }
        public int StatusIdPrev { get; set; }

        /// <summary>
        /// Constructor StatusHistory
        /// </summary>
        /// <param name="statusHistoryId"></param>
        /// <param name="statusHistoryDate"></param>
        /// <param name="interventionId"></param>
        /// <param name="statusIdPrev"></param>
        public StatusHistory(DateTime statusHistoryDate, int interventionId, int statusIdPrev)
        {
            StatusHistoryDate = statusHistoryDate;
            InterventionId = interventionId;
            StatusIdPrev = statusIdPrev;
        }

        public StatusHistory(int statusHistoryId, DateTime statusHistoryDate, int interventionId, int statusIdPrev)
        {
            StatusHistoryId = statusHistoryId;
            StatusHistoryDate = statusHistoryDate;
            InterventionId = interventionId;
            StatusIdPrev = statusIdPrev;
        }
    }
}
