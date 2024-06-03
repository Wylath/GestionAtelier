using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class RightUserProfil
    {
        public int UserId { get; set; }
        public int UserProfileId { get; set; }

        public RightUserProfil(int userId, int userProfileId)
        {
            UserId = userId;
            UserProfileId = userProfileId;
        }
    }
}
