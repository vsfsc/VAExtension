using System;
using System.Data;
using CourseChooseList.DAL;

namespace CourseChooseList.Bll
{
    public class CourseManage
    {
        #region 变量
        private DataTable courseChooseDt;
        //private DataTable tableFeildsDt;
        #endregion
        #region 属性
        public DataTable CourseChooseDt
        {
            get
            {
                if (courseChooseDt == null)
                {

                    courseChooseDt = new DataTable();
                    courseChooseDt.Columns.Add("ID", typeof(long));
                    courseChooseDt.Columns.Add("ModuleID", typeof(long));
                    courseChooseDt.Columns.Add("StudentID", typeof(long));
                    courseChooseDt.Columns.Add("Flag", typeof(long));

                }
                return courseChooseDt;

            }

        }
        #endregion
        #region 方法
        /// <summary>
        /// 获取课程
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public DataTable GetCourseManage(long studentID)
        {
            return Course.GetCourseChoose(studentID).Tables[0];
        }

        /// <summary>
        /// 获取指定表名的各字段长度
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        //public DataTable GetFieldLongth(string tableName)
        //{
        //    return Course.GetFieldLongth(tableName).Tables[0];
        //}
        
        /// <summary>
        /// 添加选课
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public long AddCourseChoose(DataRow dr)
        {

            long ID = Course.InsertCourseChoose(dr);
            return ID;

        }
        public void DeleteStudentCourse(DataRow dr)
        {
            Course.DeleteCourseChoose(dr);
        }
        public bool IsAddCourse(string endTime, int studyHour)
        {
            bool isAdd = true;
            DateTime eTime;
            if (endTime != "&nbsp;")
            {
                eTime = DateTime.Parse(endTime);
                if (DateTime.Now > eTime)
                {
                    isAdd = false;
                }
            }           
            if (studyHour > 56)
            {
                isAdd = false;
            }
            return isAdd;

        }
        #endregion
    }
}
