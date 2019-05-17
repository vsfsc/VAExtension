using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using LADLL;
using Microsoft.SharePoint;

namespace LearningActivity.DAL
{
    class WorksDAL
    {
        public static long GetWorksId(string wName)
        {
            var db = new LAActivityEntities();
            WorksFile  works = db.WorksFile .FirstOrDefault(w => w.FileName.Equals(wName));
            long WorksId = works.WorksFileID;
            return WorksId;
           
        }

        public static long InsertWorkFile(string fileName,string filePath,int fSize,long flag)
        {
            var db = new LAActivityEntities();
            WorksFile works = new WorksFile();
            works.FileName = fileName;
            works.FilePath = filePath;
            works.FileSize = fSize;
            works.CreatedBy = DAL.UserDAL.GetUserId();
            works.Created = DateTime.Now;
            works.Flag = flag ;
            db.WorksFile.Add(works);
            db.SaveChanges();
            return works.WorksFileID;

        }
        /// <summary>
        /// 如果网站中不存在保存活动附件的库，则创建他们
        /// </summary>
        public static SPList CreateList(string listName)
        {

            //  "MediaWorks" "ImageWorks",  "DocWorks"

            SPListCollection col = DAL.Common.SPWeb.Lists;
            Guid guid;
            if (listName == "ImageWorks")
            {
                guid = col.Add(listName, "", SPListTemplateType.PictureLibrary);

            }
            else
            {
                guid = col.Add(listName, "", SPListTemplateType.DocumentLibrary);

            }
            SPList list = col.GetList(guid, false);
            list.Hidden = true;
            list.Update();
            return list;

        }
        /// <summary>
        /// 判断当前作品是否存在
        /// </summary>
        /// <param name="drr"></param>
        /// <param name="viewstateName"></param>
        /// <returns></returns>
        public static bool JudgeIsExist(string fileName, string viewstateName, int length)
        {

            bool isExist = false;
            
            return isExist;
        }
        public static bool JudgeIsExistInDoc(SPList docLib, string fileName, int type)
        {
            //判断资产库

            bool isExist = false;
            if (type == 3 || type == 4)
            {

                for (int i = 0; i < docLib.RootFolder.SubFolders.Count; i++)
                {
                    if (docLib.RootFolder.SubFolders[i].Files[0].Name == fileName)
                    {
                        isExist = true;
                        break;

                    }
                }
                //不开启资产库
                for (int i = 0; i < docLib.RootFolder.Files.Count; i++)
                {
                    if (docLib.RootFolder.Files[i].Name == fileName)
                    {
                        isExist = true;
                        break;
                    }
                }

            }
            else
            {
                for (int i = 0; i < docLib.RootFolder.Files.Count; i++)
                {
                    if (docLib.RootFolder.Files[i].Name == fileName)
                    {
                        isExist = true;
                        break;
                    }
                }
            }
            return isExist;
        }
        

    }
}
