//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Museum.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Exhibition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Exhibition()
        {
            this.Exhibits = new HashSet<Exhibit>();
        }
    
        public int IDExhibition { get; set; }
        public System.DateTime DateStart { get; set; }
        public System.DateTime DateStop { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Place { get; set; }
        public string PersonInCharge { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Exhibit> Exhibits { get; set; }
    }
}
