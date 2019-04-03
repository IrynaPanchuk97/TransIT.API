using System;
using System.Collections.Generic;

namespace TransIT.DAL.Models.Entities
{
    public partial class User
    {
        public User()
        {
            ActionTypeCreate = new HashSet<ActionType>();
            ActionTypeMod = new HashSet<ActionType>();
            BillCreate = new HashSet<Bill>();
            BillMod = new HashSet<Bill>();
            DocumentCreate = new HashSet<Document>();
            DocumentMod = new HashSet<Document>();
            InverseCreate = new HashSet<User>();
            InverseMod = new HashSet<User>();
            InverseRole = new HashSet<User>();
            IssueAssignedToNavigation = new HashSet<Issue>();
            IssueCreate = new HashSet<Issue>();
            IssueLogCreate = new HashSet<IssueLog>();
            IssueLogMod = new HashSet<IssueLog>();
            IssueMod = new HashSet<Issue>();
            MalfunctionCreate = new HashSet<Malfunction>();
            MalfunctionGroupCreate = new HashSet<MalfunctionGroup>();
            MalfunctionGroupMod = new HashSet<MalfunctionGroup>();
            MalfunctionMod = new HashSet<Malfunction>();
            MalfunctionSubgroupCreate = new HashSet<MalfunctionSubgroup>();
            MalfunctionSubgroupMod = new HashSet<MalfunctionSubgroup>();
            RoleCreate = new HashSet<Role>();
            RoleMod = new HashSet<Role>();
            SupplierCreate = new HashSet<Supplier>();
            SupplierMod = new HashSet<Supplier>();
            TokenCreate = new HashSet<Token>();
            TokenMod = new HashSet<Token>();
            VehicleCreate = new HashSet<Vehicle>();
            VehicleMod = new HashSet<Vehicle>();
            VehicleTypeCreate = new HashSet<VehicleType>();
            VehicleTypeMod = new HashSet<VehicleType>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public int? CreateId { get; set; }
        public int? ModId { get; set; }

        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
        public virtual User Role { get; set; }
        public virtual ICollection<ActionType> ActionTypeCreate { get; set; }
        public virtual ICollection<ActionType> ActionTypeMod { get; set; }
        public virtual ICollection<Bill> BillCreate { get; set; }
        public virtual ICollection<Bill> BillMod { get; set; }
        public virtual ICollection<Document> DocumentCreate { get; set; }
        public virtual ICollection<Document> DocumentMod { get; set; }
        public virtual ICollection<User> InverseCreate { get; set; }
        public virtual ICollection<User> InverseMod { get; set; }
        public virtual ICollection<User> InverseRole { get; set; }
        public virtual ICollection<Issue> IssueAssignedToNavigation { get; set; }
        public virtual ICollection<Issue> IssueCreate { get; set; }
        public virtual ICollection<IssueLog> IssueLogCreate { get; set; }
        public virtual ICollection<IssueLog> IssueLogMod { get; set; }
        public virtual ICollection<Issue> IssueMod { get; set; }
        public virtual ICollection<Malfunction> MalfunctionCreate { get; set; }
        public virtual ICollection<Malfunction> MalfunctionMod { get; set; }
        public virtual ICollection<MalfunctionGroup> MalfunctionGroupCreate { get; set; }
        public virtual ICollection<MalfunctionGroup> MalfunctionGroupMod { get; set; }
        public virtual ICollection<MalfunctionSubgroup> MalfunctionSubgroupCreate { get; set; }
        public virtual ICollection<MalfunctionSubgroup> MalfunctionSubgroupMod { get; set; }
        public virtual ICollection<Role> RoleCreate { get; set; }
        public virtual ICollection<Role> RoleMod { get; set; }
        public virtual ICollection<Supplier> SupplierCreate { get; set; }
        public virtual ICollection<Supplier> SupplierMod { get; set; }
        public virtual ICollection<Token> TokenCreate { get; set; }
        public virtual ICollection<Token> TokenMod { get; set; }
        public virtual ICollection<Vehicle> VehicleCreate { get; set; }
        public virtual ICollection<Vehicle> VehicleMod { get; set; }
        public virtual ICollection<VehicleType> VehicleTypeCreate { get; set; }
        public virtual ICollection<VehicleType> VehicleTypeMod { get; set; }
    }
}
