//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LADLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Location
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Location()
        {
            this.LearningActivity = new HashSet<LearningActivity>();
        }
    
        public long LocationID { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public Nullable<long> ParentID { get; set; }
        public Nullable<int> IsShare { get; set; }
        public Nullable<long> CreatBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LearningActivity> LearningActivity { get; set; }
    }
}
