//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class UserBlackList
    {
        public int id { get; set; }
        public int clanUserId { get; set; }
        public int clanId { get; set; }
        public string note { get; set; }
        public System.DateTime addedOn { get; set; }
    
        public virtual Clan Clan { get; set; }
        public virtual ClanUser ClanUser { get; set; }
    }
}