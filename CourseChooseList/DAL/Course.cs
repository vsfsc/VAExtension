using System.Data;

namespace CourseChooseList.DAL
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
        //public static DataSet GetFieldLongth(string tableName)
        //{
        //    DataSet ds = CourseDll.DAL.SqlHelper.ExecuteDataset(CourseDll.DAL.DataProvider.ConnectionString, "GetFieldLongthByTableName", tableName);
        //    return ds;
        //}
        #endregion
        #region 添加
        /// <summary>
        /// 选课
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static long InsertCourseChoose(DataRow dr)
        {
            return ((long)CourseDll.DAL.SqlHelper.ExecuteNonQueryTypedParamsOutput(CourseDll.DAL.DataProvider.ConnectionString, "InsertCourseChoose", dr)[0].Value);
        }
        #region 更新

        public static long DeleteCourseChoose(DataRow dr)
        {
            return (CourseDll.DAL.SqlHelper.ExecuteAppointedParameters(CourseDll.DAL.DataProvider.ConnectionString, "DeleteCourseChoose", dr));
        }
        #endregion

        #endregion
    }
}
