using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseDll.DAL;
using Microsoft.SharePoint;
using System.Data;

namespace CourseDll.Bll
{
    public class UserManage
    {
        public long GetOrAddUserIDByAccount(string Account)
        {

            long studentID = 0;
            DataTable dt = User.GetUserByAccount(Account).Tables[0];
            DataRow dr = dt.NewRow();
            if (dt.Rows.Count == 0)
            {
                string[] bj = null;
                string sex = null;
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    bj = ADHelper.GetUserGroups(Account);
                    sex = ADHelper.GetUserSex(Account);
                });
                if (sex == "男")
                    sex = "女";
                else if (sex == "女")
                    sex = "男";
                dr["ID"] = 1;
                dr["Account"] = Account;
                dr["Username"] = Account;
                dr["StudentNO"] = Account;
                if (bj != null)
                {
                    dr["Class"] = bj[0];
                }
                dr["Gender"] = sex;
                dr["Flag"] = 1;

                studentID = User.InsertUser(dr);
            }
            else
            {
                studentID = long.Parse(User.GetUserByAccount(Account).Tables[0].Rows[0]["ID"].ToString());
            }
            return studentID;
        }
    }
}
