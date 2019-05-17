using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestDll.DAL
{
    public class Course
    {
        /// <summary>
        /// 根据竞赛名称获取竞赛信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ContestDll.Course GetCourseByName(string name)
        {
            using (ContestEntities db=new ContestEntities() )
            {
                ContestDll.Course c = db.Course.SingleOrDefault(p => p.CourseName == name);
                
                return c;
            }
        }
        /// <summary>
        /// 获取所有竞赛列表
        /// </summary>
        /// <returns></returns>
        
        public static List<ContestDll.Course> GetCourses()
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<ContestDll.Course> cList = db.Course.ToList();
                return cList;
            }
        }
    }
}
