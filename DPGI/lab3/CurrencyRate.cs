//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace lab3
{
    using System;
    using System.Collections.Generic;
    
    public partial class CurrencyRate
    {
        public CurrencyRate()
        {
            this.ConversionHistory = new HashSet<ConversionHistory>();
            this.ConversionHistory1 = new HashSet<ConversionHistory>();
        }
    
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public double ExchangeRate { get; set; }
    
        public virtual ICollection<ConversionHistory> ConversionHistory { get; set; }
        public virtual ICollection<ConversionHistory> ConversionHistory1 { get; set; }
    }
}