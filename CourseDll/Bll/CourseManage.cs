using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CourseDll.DAL;
using Microsoft.SharePoint;




namespace CourseDll.Bll
{
    public class CourseManage
    {
        //#region 变量
        //private DataTable userDt;

        //public DataTable UserDt
        //{
        //    get
        //    {
        //        if (userDt == null)
        //        {
        //            userDt = new DataTable();
        //            userDt.Columns.Add("ID", typeof(long));
        //            userDt.Columns.Add("Account", typeof(long));
        //            userDt.Columns.Add("Username", typeof(long));
        //            userDt.Columns.Add("StudentNO", typeof(long));
        //            userDt.Columns.Add("Class", typeof(long));
        //            userDt.Columns.Add("Gender", typeof(long));
        //            userDt.Columns.Add("Flag", typeof(long));

        //        }
        //        return userDt;
        //    }

        //}
        //#endregion



        public DataTable GetCourseManage(string Account)
        {
            DataTable userDt;
            userDt = new DataTable();
            userDt.Columns.Add("ID", typeof(long));
            userDt.Columns.Add("Account", typeof(long));
            userDt.Columns.Add("Username", typeof(long));
            userDt.Columns.Add("StudentNO", typeof(long));
            userDt.Columns.Add("Class", typeof(long));
            userDt.Columns.Add("Gender", typeof(long));
            userDt.Columns.Add("Flag", typeof(long));
            long studentID = 1;
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

            return Course.GetCourseChoose(studentID).Tables[0];
        }
    }
}
