//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Image
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Image()
        {
            this.Comments = new HashSet<Comments>();
            this.Likes1 = new HashSet<Likes>();
        }
    
        public long imgId { get; set; }
        public long usrId { get; set; }
        public string pathImg { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string dateImg { get; set; }
        public string category { get; set; }
        public string f { get; set; }
        public string t { get; set; }
        public string ISO { get; set; }
        public string wb { get; set; }
        public long likes { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Likes> Likes1 { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
