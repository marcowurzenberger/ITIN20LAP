//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace innovations4austria.logic
{
    using System;
    using System.Collections.Generic;
    
    public partial class bookingdetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public bookingdetail()
        {
            this.billdetails = new HashSet<billdetail>();
        }
    
        public int id { get; set; }
        public int booking_id { get; set; }
        public System.DateTime bookingdate { get; set; }
        public decimal price { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<billdetail> billdetails { get; set; }
        public virtual booking booking { get; set; }
    }
}
