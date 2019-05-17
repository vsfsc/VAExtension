using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LADLL;

namespace LearningActivity.DAL
{
    public class ActivityTypeDAL
    {
        public static List<ActivityType> GetTypes()
        {
            long userID = DAL.UserDAL.GetUserId();
            using (LAActivityEntities db = new LAActivityEntities())
            {
                List<ActivityType> atList = db.ActivityType.Where(t => t.ParentID != 0 && t.CreatBy == userID ||t.IsShare==1).ToList();
                return atList;
            }
        }

        /// <summary>
        /// 根据类别ID查询地点类别
        /// </summary>
        /// <param name="typeId">类别ID</param>
        /// <returns></returns>
        public static ActivityType GetTypeById(long typeId)
        {
            var db = new LAActivityEntities();
            var types = db.ActivityType.FirstOrDefault(tp => tp.ActivityTypeID == typeId);//获取对应ParentID的学习地点
            return types;
        }
        /// <summary>
        /// 获取每个父级类别下的所有子级类别
        /// </summary>
        /// <param name="parentId">父级类别ID</param>
        /// <returns></returns>
        public static List<ActivityType> GetTypesByparentId(long parentId)
        {
            using (LAActivityEntities db=new LAActivityEntities())
            {
                List<ActivityType> atList = db.ActivityType.Where(at => at.ParentID == parentId).ToList();
                return atList;
            }
        }

        /// <summary>
        ///  新增活动类别
        /// </summary>
        /// <param name="action">类别名称</param>
        /// <param name="description">类别描述</param>
        /// <param name="userId">创建者ID</param>
        /// <param name="isShare">其他人可见</param>
        /// <param name="parentId">父级类别ID</param>
        /// <returns>新增的类别ID</returns>
        public static long AddActivityType(string action, string description,long userId,int isShare, long parentId)
        {
            
            LAActivityEntities db = new LAActivityEntities();
            LADLL.ActivityType at = new LADLL.ActivityType();
            at.Action = action;
            at.Description = description;
            at.ParentID = parentId;
            at.IsShare = isShare;
            at.CreatBy = userId;
            db.ActivityType.Add(at);
            db.SaveChanges();
            return at.ActivityTypeID;
        }
        /// <summary>
        ///  新增父级活动类别
        /// </summary>
        /// <param name="action">类别名称</param>
        /// <param name="description">类别描述</param>
        /// <returns>新增的父级类别ID</returns>
        public static long AddParentType(string action, string description)
        {
            LAActivityEntities db = new LAActivityEntities();
            LADLL.ActivityType at = new LADLL.ActivityType();
            at.Action = action;
            at.Description = description;
            at.ParentID = 0;
            db.ActivityType.Add(at);
            db.SaveChanges();
            return at.ActivityTypeID;
        }

        /// <summary>
        /// 生成类别显示html
        /// </summary>
        /// <returns></returns>
        public static string GetTypeHtml()
        {
            string strCon = null;
            List<ActivityType> atList0 = GetTypesByparentId(0);
            if (atList0.Count>0)
            {
                strCon = "<ul id='accordion' class='accordion'>";
                foreach (var item0 in atList0)//遍历ParentID=0的类别action
                {
                    long at0Id = item0.ActivityTypeID;
                    string at0Action = item0.Action;

                    strCon += "<li><div class='link'>" + at0Action + "</div>";
                    strCon += "<div class='submenu'id='subul'>";
                    List<ActivityType> atList = GetTypesByparentId(at0Id);
                    long k = atList.Count;
                    strCon += "<div>";
                    int i = 0;
                    foreach (var litem in atList)//遍历ParentID=at0Id的类别action
                    {
                        if (i>=5)
                        {
                            strCon += "</div><div>";
                            i = 0;
                        }
                        long atId = litem.ActivityTypeID; ;
                        string atAction = litem.Action;
                        strCon += "<a id='" + atId + "' onclick=\"setType(this);CloseDiv('TypeDiv','fade')\">" + atAction + "</a>";
                        i++;
                    }
                    strCon += "</div>";
                    strCon += " </div>";
                    strCon += "</li>";
                }
                strCon += " </ul>";
            }
            return strCon;
        }
    }
}
