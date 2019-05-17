using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LADLL;

namespace LearningActivity.DAL
{
    public class LObjectDAL
    {
        /// <summary>
        /// 获取指定ID的学习对象
        /// </summary>
        /// <param name="loId">学习对象ID</param>
        /// <returns>生成对象详情表</returns>
        public static string GetLObjectById(long loId)
        {
            string strContent = "<table>";
            var db = new LAActivityEntities();
            LearningObject lObjects = db.LearningObject.FirstOrDefault(lo => lo.LearningObjectID == loId);
            if (lObjects!=null)
            {
                strContent += "<tr>";
                strContent += "<td>对象名称:</td><td>" + lObjects.Title + "</td>";
                strContent += "</tr><tr>";
                strContent += "<td>创建时间:</td><td>" + lObjects.CreatedDate + "</td>";
                strContent += "</tr><tr>";
                strContent += "<td>对象描述:</td><td>" + lObjects.Description + "</td>";
                //strContent += "</tr><tr>";
                //strContent += "<td>学习内容:</td><td>" + lObjects.LearningContent + "</td>";
                strContent += "</tr>";
            }
            strContent += "</table>";
            return strContent;
        }
        public static List<LearningObject> GetLObjects()
        {
            long userId = DAL.UserDAL.GetUserId();
            var db = new LAActivityEntities();
            List<LearningObject> lObjects = db.LearningObject.Where(lo=>lo.CreatedBy==userId||lo.IsShare==1).ToList();//获取所有学习对象
            return lObjects;
        }

        /// <summary>
        /// 查询数据库中所有学习对象
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static List<LearningObject> GetLObjectsByUserId(long userId)
        {
            var db = new LAActivityEntities();
            var lObjects = db.LearningObject.Where(lo=>lo.CreatedBy==userId).ToList();//获取指定用户的学习对象
            return lObjects;
        }
        /// <summary>
        /// 获取备选学习对象的列表
        /// </summary>
        /// <returns></returns>
        public static string GetLObjectsHtml()
        {
            long userId = UserDAL.GetUserId();
            string strContent = "";
            List<LearningObject> loList = GetLObjectsByUserId(userId);
            if (loList.Count>0)
            {
                strContent = "<ul id='accordion' class='accordion'>";
                foreach (var loItem in loList)//遍历ParentID=0的类别action
                {
                    long loId = loItem.LearningObjectID;//学习对象ID
                    string loTitle = loItem.Title;//学习对象标题
                    strContent += "<li><div class='link'>" + loTitle + "<i class='fa fa-chevron-down'></i></div>";
                    strContent += GetLObjectById(loId);//x学习对象的详细说明表
                    strContent += "</li>";
                }
                strContent += " </ul>";
            }
            return strContent;
        }

        /// <summary>
        /// 新增学习对象
        /// </summary>
        /// <param name="title">对象名称</param>
        /// <param name="desc">对象描述</param>
        /// <param name="isShare">其他人可见</param>
        /// <param name="lContent">学习内容</param>
        /// <param name="userId">创建者ID</param>
        /// <returns>新增对象的ID</returns>
        public static long AddNewOject(string title, string desc, int isShare,long userId, string lContent)
        {
            var db = new LAActivityEntities();
            var lo = new LearningObject
            {
                Title = title,
                Description = desc,
                LearningContent = lContent,
                IsShare = isShare,
                CreatedBy = userId,
                CreatedDate = DateTime.Now
            };
            db.LearningObject.Add(lo);
            db.SaveChanges();
            return lo.LearningObjectID;
        }
    }
}
