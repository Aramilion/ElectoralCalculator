//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vote
    {
        public int idvotes { get; set; }
        public System.DateTime date { get; set; }
        public Nullable<int> idcandidate { get; set; }
        public byte valid { get; set; }
        public byte withRights { get; set; }
    
        public virtual Candidate Candidate { get; set; }
    }
}
