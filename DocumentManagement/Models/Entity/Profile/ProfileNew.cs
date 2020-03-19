using DocumentManagement.Model.Entity.GearBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.Entity.Profile
{
    public class ProfileNew
    {
        public List<GearBox> lstGearBox = new List<GearBox>();
        public List<ProfileTypes> lstProfileTypes = new List<ProfileTypes>();
    }
}
