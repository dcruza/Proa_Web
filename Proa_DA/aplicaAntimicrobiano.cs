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
    
    public partial class aplicaAntimicrobiano
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public aplicaAntimicrobiano()
        {
            this.aplicaAntimicrobianoGermen = new HashSet<aplicaAntimicrobianoGermen>();
        }
    
        public int id { get; set; }
        public string idPaciente { get; set; }
        public int numCaso { get; set; }
        public bool seguimiento { get; set; }
        public bool activo { get; set; }
        public string idUsuarioIn { get; set; }
        public System.DateTime fechaHoraIn { get; set; }
    
        public virtual estanciaPaciente estanciaPaciente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aplicaAntimicrobianoGermen> aplicaAntimicrobianoGermen { get; set; }
    }
}