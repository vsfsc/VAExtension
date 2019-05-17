using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LADLL;

namespace LearningActivity.DAL
{
    public class LocationDAL
    {
        /// <summary>
        /// 查询数据库中所有学习地点
        /// </summary>
        /// <returns></returns>
        public static List<Location> GetLoctionsByParentId(long parentId)
        {
            var db = new LAActivityEntities();
            List<Location> loctions = db.Location.Where(lt => lt.ParentID == parentId).ToList();//获取对应ParentID的学习地点
            return loctions;
        }
        public static List<Location> GetLoctions()
        {
            long userId = DAL.UserDAL.GetUserId();
            var db = new LAActivityEntities();
            List<Location> loctions = db.Location.Where(lc=>lc.CreatBy==userId||lc.IsShare==1).ToList();//获取对应ParentID的学习地点
            return loctions;
        }
        /// <summary>
        /// 根据地点ID查询地点名称
        /// </summary>
        /// <param name="locaId">地点ID</param>
        /// <returns></returns>
        public static List<Location> GetLoctionById(long locaId)
        {
            var db = new LAActivityEntities();
            var loction = db.Location.Where(lc => lc.LocationID == locaId).ToList();//获取对应ParentID的学习地点
            return loction;
        }
        /// <summary>
        ///  新增活动地点
        /// </summary>
        /// <param name="locName">地点名称</param>
        /// <param name="description">类别描述</param>
        /// <param name="isShare">其他人可见</param>
        /// <param name="parentId">父级类别ID</param>
        /// <param name="userId">创建者ID</param>
        /// <returns>新增的地点ID</returns>
        public static long AddLocation(string locName, string description,long userId,int isShare, long parentId)
        {
            var db = new LAActivityEntities();
            var loc = new Location
            {
                Address = locName,
                Description = description,
                ParentID = parentId,
                IsShare = isShare,
                CreatBy = userId
            };
            db.Location.Add(loc);
            db.SaveChanges();
            return loc.LocationID;
        }
        public static string GetLoctionHtml()
        {
            string strCon = null;
            List<Location> locList0 = GetLoctionsByParentId(0);
            if (locList0.Count > 0)
            {
                strCon = "<ul id='accordion' class='accordion'>";
                foreach (var item0 in locList0)//遍历ParentID=0的类别action
                {
                    long locId0 = item0.LocationID;
                    string locName0 = item0.Address;

                    strCon += "<li><div class='link'>" + locName0 + "<i class='fa fa-chevron-down'></i></div>";
                    strCon += "<ul class='submenu'>";
                    List<Location> atList = GetLoctionsByParentId(locId0);
                    foreach (var litem in atList)//遍历ParentID=LocationId的类别action
                    {
                        long locId = litem.LocationID; ;
                        string locName = litem.Address;
                        strCon += "<li><a href='#' onclick='setType(" + locId + ");CloseDiv('TypeDiv','fade')'>" + locName + "</li>";
                    }
                    strCon += " </ul>";
                    strCon += "</li>";
                }
                strCon += " </ul>";
            }
            return strCon;
        }
    }
}
