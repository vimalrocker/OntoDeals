﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dcupon.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OntoDealsEntities : DbContext
    {
        public OntoDealsEntities()
            : base("name=OntoDealsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Website> Websites { get; set; }
        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<AdminProfile> AdminProfiles { get; set; }
        public virtual DbSet<ImageDetail> ImageDetails { get; set; }
        public virtual DbSet<CuponDetail> CuponDetails { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<CustomerMobile> CustomerMobiles { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
    }
}
