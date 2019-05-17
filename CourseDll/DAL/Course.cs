using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CourseDll.DAL
{
    public class Course
    {
        #region 获取
        /// <summary>
        /// 获取课程信息 
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <returns></returns>
        public static DataSet GetCourse()
        {
            DataSet ds = CourseDll.DAL.SqlHelper.ExecuteDataset(CourseDll.DAL.DataProvider.ConnectionString, "GetCourse");
            return ds;
        }
        public static DataSet GetCourseChoose(long ID)
        {
            DataSet ds = CourseDll.DAL.SqlHelper.ExecuteDataset(CourseDll.DAL.DataProvider.ConnectionString, "GetCourseChoose", ID);
            return ds;
        }
        #endregion
        #region 添加
        public static long InsertCourseChoose(DataRow dr)
        {
            return ((long)CourseDll.DAL.SqlHelper.ExecuteNonQueryTypedParamsOutput(CourseDll.DAL.DataProvider.ConnectionString, "CourseChoose", dr)[0].Value);
        }
       
        #endregion
    }
}
