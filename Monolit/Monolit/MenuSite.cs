//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Monolit
{
    using System;
    using System.Collections.Generic;
    
    public partial class MenuSite
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuSite()
        {
            this.ContextPage = new HashSet<ContextPage>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int LevelMenu { get; set; }
        public Nullable<int> ParentMenu { get; set; }
        public Nullable<int> Position { get; set; }
        public Nullable<int> Context { get; set; }
        public string Template { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }
        public string URL { get; set; }
    
        public virtual Template Template1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContextPage> ContextPage { get; set; }
    }
}