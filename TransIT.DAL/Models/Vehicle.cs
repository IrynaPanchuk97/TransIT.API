﻿using System;
using System.Collections.Generic;

namespace TransIT.DAL.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Issue = new HashSet<Issue>();
        }

        public int Id { get; set; }
        public int VehicleTypeId { get; set; }
        public string Vincode { get; set; }
        public string InventoryId { get; set; }
        public string RegNum { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public int CreateId { get; set; }
        public int ModId { get; set; }

        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        public virtual ICollection<Issue> Issue { get; set; }
    }
}
