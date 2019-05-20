using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class User : IEntity
    {
        public User()
        {
            ActionTypeCreate = new HashSet<ActionType>();
            ActionTypeMod = new HashSet<ActionType>();
            BillCreate = new HashSet<Bill>();
            BillMod = new HashSet<Bill>();
            CountryCreate = new HashSet<Country>();
            CountryMod = new HashSet<Country>();
            CurrencyCreate = new HashSet<Currency>();
            CurrencyMod = new HashSet<Currency>();
            DocumentCreate = new HashSet<Document>();
            DocumentMod = new HashSet<Document>();
            EmployeeCreate = new HashSet<Employee>();
            EmployeeMod = new HashSet<Employee>();
            InverseCreate = new HashSet<User>();
            InverseMod = new HashSet<User>();
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
            PostCreate = new HashSet<Post>();
            PostMod = new HashSet<Post>();
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
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public int? CreateId { get; set; }
        public int? ModId { get; set; }

        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<ActionType> ActionTypeCreate { get; set; }
        public virtual ICollection<ActionType> ActionTypeMod { get; set; }
        public virtual ICollection<Bill> BillCreate { get; set; }
        public virtual ICollection<Bill> BillMod { get; set; }
        public virtual ICollection<Country> CountryCreate { get; set; }
        public virtual ICollection<Country> CountryMod { get; set; }
        public virtual ICollection<Currency> CurrencyCreate { get; set; }
        public virtual ICollection<Currency> CurrencyMod { get; set; }
        public virtual ICollection<Document> DocumentCreate { get; set; }
        public virtual ICollection<Document> DocumentMod { get; set; }
        public virtual ICollection<Employee> EmployeeCreate { get; set; }
        public virtual ICollection<Employee> EmployeeMod { get; set; }
        public virtual ICollection<User> InverseCreate { get; set; }
        public virtual ICollection<User> InverseMod { get; set; }
        public virtual ICollection<Issue> IssueCreate { get; set; }
        public virtual ICollection<IssueLog> IssueLogCreate { get; set; }
        public virtual ICollection<IssueLog> IssueLogMod { get; set; }
        public virtual ICollection<Issue> IssueMod { get; set; }
        public virtual ICollection<Malfunction> MalfunctionCreate { get; set; }
        public virtual ICollection<MalfunctionGroup> MalfunctionGroupCreate { get; set; }
        public virtual ICollection<MalfunctionGroup> MalfunctionGroupMod { get; set; }
        public virtual ICollection<Malfunction> MalfunctionMod { get; set; }
        public virtual ICollection<MalfunctionSubgroup> MalfunctionSubgroupCreate { get; set; }
        public virtual ICollection<MalfunctionSubgroup> MalfunctionSubgroupMod { get; set; }
        public virtual ICollection<Post> PostCreate { get; set; }
        public virtual ICollection<Post> PostMod { get; set; }
        public virtual ICollection<Role> RoleCreate { get; set; }
        public virtual ICollection<Role> RoleMod { get; set; }
        public virtual ICollection<Supplier> SupplierCreate { get; set; }
        public virtual ICollection<Supplier> SupplierMod { get; set; }
        public virtual ICollection<Token> TokenCreate { get; set; }
        public virtual ICollection<Token> TokenMod { get; set; }
        public virtual ICollection<Transition> TransitionCreate { get; set; }
        public virtual ICollection<Transition> TransitionMod { get; set; }
        public virtual ICollection<Vehicle> VehicleCreate { get; set; }
        public virtual ICollection<Vehicle> VehicleMod { get; set; }
        public virtual ICollection<VehicleType> VehicleTypeCreate { get; set; }
        public virtual ICollection<VehicleType> VehicleTypeMod { get; set; }
    }
}