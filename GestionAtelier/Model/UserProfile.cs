using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.Model
{
    class UserProfile
    {
        public int UserProfileId { get; set; }
        public string Name { get; set; }
        public int ProfileLevel { get; set; }
        public ParmSite SiteId { get; set; }

        /// <summary>
        /// Constructor UserProfile
        /// </summary>
        /// <param name="userProfileId"></param>
        /// <param name="name"></param>
        /// <param name="profileLevel"></param>
        /// <param name="siteId"></param>
        public UserProfile(int userProfileId, string name, int profileLevel, ParmSite siteId)
        {
            UserProfileId = userProfileId;
            Name = name;
            ProfileLevel = profileLevel;
            SiteId = siteId;
        }
    }
}
