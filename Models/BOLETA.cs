//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DSW_CL2_JOSUE_CHUPICA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BOLETA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOLETA()
        {
            this.DETALLEBOLETA = new HashSet<DETALLEBOLETA>();
        }
    
        public int NUM_BOL { get; set; }
        public System.DateTime FEC_BOL { get; set; }
        public Nullable<double> SUBTOTAL { get; set; }
        public Nullable<double> IVA { get; set; }
        public Nullable<double> TOTAL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLEBOLETA> DETALLEBOLETA { get; set; }
    }
}
