//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proa_DA
{
    using System;
    using System.Collections.Generic;
    
    public partial class via
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public via()
        {
            this.aplicaAntimicrobianoGermen = new HashSet<aplicaAntimicrobianoGermen>();
        }
    
        public short id { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }
        public string idUsuarioIn { get; set; }
        public System.DateTime fechaHoraIn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aplicaAntimicrobianoGermen> aplicaAntimicrobianoGermen { get; set; }
    }
}
