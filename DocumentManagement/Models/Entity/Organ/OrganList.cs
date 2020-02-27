using System;
using System.Collections.Generic;

namespace DocumentManagement.Models.Entity.Organ
{
    public class OrganList
    {
        public List<string> OrganTypes { get; set; } = new List<string>();
        public List<string> OrganName { get; set; } = new List<string>();
        public List<string> OrganAddress { get; set; } = new List<string>();
    }
}

