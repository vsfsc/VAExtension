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
    
    public partial class WorksFile
    {
        public long WorksFileID { get; set; }
        public long WorksID { get; set; }
        public Nullable<int> Type { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public Nullable<int> FileSize { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public Nullable<long> Flag { get; set; }
    
        public virtual Works Works { get; set; }
    }
}
