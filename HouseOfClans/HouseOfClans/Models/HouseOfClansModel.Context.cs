﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HouseOfClans.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HouseOfClansEntities : DbContext
    {
        public HouseOfClansEntities()
            : base("name=HouseOfClansEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ActionLog> ActionLogs { get; set; }
        public virtual DbSet<Action> Actions { get; set; }
        public virtual DbSet<ClanGroup> ClanGroups { get; set; }
        public virtual DbSet<Clan> Clans { get; set; }
        public virtual DbSet<ClanUser> ClanUsers { get; set; }
        public virtual DbSet<ClanWarPick> ClanWarPicks { get; set; }
        public virtual DbSet<ClanWar> ClanWars { get; set; }
        public virtual DbSet<Rule> Rules { get; set; }
        public virtual DbSet<UserBlackList> UserBlackLists { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<WarLog> WarLogs { get; set; }
        public virtual DbSet<WarType> WarTypes { get; set; }
        public virtual DbSet<C__MigrationHistory1> C__MigrationHistory1 { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<WarRanking> WarRankings { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
    }
}