﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRMInventory.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class invetoryEntities : DbContext
    {
        public invetoryEntities()
            : base("name=invetoryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<company_master> company_master { get; set; }
        public virtual DbSet<godown_master> godown_master { get; set; }
        public virtual DbSet<ledger_master> ledger_master { get; set; }
        public virtual DbSet<product_expenses_master> product_expenses_master { get; set; }
        public virtual DbSet<product_master> product_master { get; set; }
        public virtual DbSet<purchase_details> purchase_details { get; set; }
        public virtual DbSet<purchase_expenses> purchase_expenses { get; set; }
        public virtual DbSet<purchase_master> purchase_master { get; set; }
        public virtual DbSet<stock_group_master> stock_group_master { get; set; }
        public virtual DbSet<under_master> under_master { get; set; }
        public virtual DbSet<unit_master> unit_master { get; set; }
        public virtual DbSet<user_master> user_master { get; set; }
        public virtual DbSet<stock_detail> stock_detail { get; set; }
        public virtual DbSet<sales_details> sales_details { get; set; }
        public virtual DbSet<sales_expenses> sales_expenses { get; set; }
        public virtual DbSet<sales_master> sales_master { get; set; }
    }
}