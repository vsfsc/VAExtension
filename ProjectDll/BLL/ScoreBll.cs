using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDll.BLL
{
    /// <summary>
    /// 对项目或者项目参与者的评分或点评
    /// </summary>
    public class ScoreBll
    {
        #region 评分部分

        #endregion

        #region 点评部分        
        /// <summary>
        /// Gets the comments by createdby.
        /// </summary>
        /// <param name="createdBy">The createdby.</param>
        /// <returns>List&lt;Proj_Comments&gt;.</returns>
        public static List<Proj_Comments> GetCommentsByCreatedBy(long createdBy)
        {
            List<Proj_Comments> pc = DAL.ScoreDal.GetAllComments().Where(c => c.CreatedBy == createdBy).ToList();
            return pc;
        }
        /// <summary>
        /// 根据评分对象的ID和评分类型获取评分列表.
        /// </summary>
        /// <param name="itemId">评分对象ID</param>
        /// <param name="cType">评分类型(1:项目,2:人员)</param>
        /// <returns>List&lt;Proj_Comments&gt;.</returns>
        public static List<Proj_Comments> GetCommentsByItemIdandType(long itemId,int cType)
        {
            List<Proj_Comments> pc = DAL.ScoreDal.GetAllComments().Where(c => c.ItemID == itemId&c.CType==cType).ToList();
            return pc;
        }
        #endregion
    }
}
