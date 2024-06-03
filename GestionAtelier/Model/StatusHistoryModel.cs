using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class StatusHistoryModel : ObservableObject
    {
        private readonly StatusHistory _StatusHistory;

        public StatusHistoryModel(StatusHistory StatusHistory)
        {
            _StatusHistory = StatusHistory;
            RaisePropertyChanged();
        }

        public int StatusHistoryId
        {
            get
            {
                return _StatusHistory.StatusHistoryId;
            }
            set
            {
                if (_StatusHistory.StatusHistoryId != value)
                    _StatusHistory.StatusHistoryId = value;
                RaisePropertyChanged(() => StatusHistoryId);
            }
        }

        public DateTime StatusHistoryDate
        {
            get
            {
                return _StatusHistory.StatusHistoryDate;
            }
            set
            {
                if (_StatusHistory.StatusHistoryDate != value)
                    _StatusHistory.StatusHistoryDate = value;
                RaisePropertyChanged(() => StatusHistoryDate);
            }
        }

        public int InterventionId
        {
            get
            {
                return _StatusHistory.InterventionId;
            }
            set
            {
                if (_StatusHistory.InterventionId != value)
                    _StatusHistory.InterventionId = value;
                RaisePropertyChanged(() => InterventionId);
            }
        }

        public int StatusIdPrev
        {
            get
            {
                return _StatusHistory.StatusIdPrev;
            }
            set
            {
                if (_StatusHistory.StatusIdPrev != value)
                    _StatusHistory.StatusIdPrev = value;
                RaisePropertyChanged(() => StatusIdPrev);
            }
        }
    }
}
