//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ContestDll
{
    using System;
    using System.Collections.Generic;
    
    public partial class ResetPassword
    {
        public long OperateID { get; set; }
        public string Account { get; set; }
        public string Email { get; set; }
        public string CheckCode { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<bool> IsFinished { get; set; }
    }
}
