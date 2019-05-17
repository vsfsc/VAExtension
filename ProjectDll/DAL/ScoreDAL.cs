using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDll.DAL
{
    /// <summary>
    /// 对项目或者项目参与者的评分或点评
    /// </summary>
    public class ScoreDal
    {
        #region 评分部分

        #endregion

        #region 点评部分Proj_Comments        
        /// <summary>
        /// Gets all comments.
        /// </summary>
        /// <returns>List&lt;Proj_Comments&gt;.</returns>
        public static List<Proj_Comments> GetAllComments()
        {
            using (var db=new ProjectEntities())
            {
                List<Proj_Comments> pc = db.Proj_Comments.ToList();
                return pc;
            }
        }
        #endregion
    }
}
