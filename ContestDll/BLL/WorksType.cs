using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestDll.BLL
{
    public class WorksType
    {
        /// <summary>
        /// 获取顶级作品类型
        /// </summary>
        /// <returns></returns>
        public static List<ContestDll.WorksType> GetWorksTypeTopLevel()
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<ContestDll. WorksType> ds = db.WorksType.Where(p => p.ParentID == 0).ToList ();
                return ds;
            }
        }
        public static List<WorksFile > GetWorksFileByTypeID(long worksID, int typeID)
        {
            List<WorksFile> ds = DAL.Works.GetWorksFile(worksID, typeID);
            if (typeID < 4)
            {
                List<WorksFile> dsTmp;
                dsTmp = ds.Where(p => p.Type == typeID).ToList();// ds.Tables[0].Select("Type=" + typeID);
                return dsTmp;
            }
            return ds;
        }


    }
}
