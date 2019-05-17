using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestDll.BLL
{
    public class Course
    {
        /// <summary>
        /// 判断竞赛现在所处的阶段：1、评价训练期（作品上传期） 2、评分期 3、公示点评期
        /// </summary>
        /// <param name="periodID"></param>
        /// <returns></returns>
        public static int JudgeDate(long periodID)
        {
            CSPeriodsWorksType  ds = DAL.Periods.GetPeriodsByID(periodID);//获取该期次竞赛在详细信息

            if (ds==null)
            {
                return 0;
            }

            DateTime dtStart =(DateTime )ds.StartScore;
            DateTime dtEnd = (DateTime)ds.EndScore;
            if (DateTime.Today < dtStart)
            { 
                return 1; 
            }
            else if (DateTime.Today >= dtStart && DateTime.Today < dtEnd.AddDays(1))
            {
                return 2;
            }
            dtStart = (DateTime)ds.StartPublic;
            dtEnd = (DateTime)ds.EndPublic;
            if (DateTime.Today >= dtStart && DateTime.Today < dtEnd.AddDays(1))
            {
                return 3;
            }
            else
            {
                return 0;
            }
            
        }
        public static long GetCourseID()
        {
            ContestDll.Course  CourseDt = DAL.Course.GetCourseByName(DAL.Common.SPWeb.Title);
            if (CourseDt!=null)
            {

                return CourseDt.CourseID;
            }
            else
            {
                return 1;
            }
        }
        public static long GetContestWebID()
        {
            string courseName = DAL.Common.SPWeb.Title;
            if (courseName=="众创竞赛")
            {
                return 0;
            } 
            else
            {
                ContestDll.Course CourseDt = DAL.Course.GetCourseByName(courseName);
                if (CourseDt != null)
                {
                    return CourseDt.CourseID;
                }
                else
                {
                    return 1;
                }
            }
            
        }
    }
}
