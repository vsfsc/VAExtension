// ***********************************************************************
// Assembly         : ProjectDll
// Author           : administrator
// Created          : 04-20-2016
//
// Last Modified By : administrator
// Last Modified On : 04-25-2016
// ***********************************************************************
// <copyright file="ProjectFileDAL.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.ApplicationPages.Calendar.Exchange;


namespace ProjectDll.DAL
{
    /// <summary>
    /// Class ProjectFileDAL.
    /// </summary>
    public class ProjectFileDAL
    {
        /// <summary>
        /// Gets the name of the project identifier by file.
        /// </summary>
        /// <param name="pfName">Name of the pf.</param>
        /// <returns>System.Int64.</returns>
        public static long GetProjectIdByFileName(string pfName)
        {
            long pfId = 0;
             using (ProjectEntities db=new ProjectEntities ())
             {
                 proj_ProjectFile  pFile = db.proj_ProjectFile.FirstOrDefault(w => w.FileName.Equals(pfName));
                 if (pFile != null)
                 {
                    pfId = pFile.FileID;
                 }
             }
             return pfId;
        }
        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <returns>List&lt;proj_ProjectFile&gt;.</returns>
        public static List<proj_ProjectFile> GetFiles()
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                var pfList = db.proj_ProjectFile.ToList();
                return pfList;
            }
        }

        /// <summary>
        /// Inserts the project file.
        /// </summary>
        /// <param name="drFile"></param>
        /// <returns>System.Int64.</returns>
        public static long InsertProjectFile(proj_ProjectFile drFile)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                proj_ProjectFile projectFile = new proj_ProjectFile();
                projectFile.FileName = drFile.FileName;
                projectFile.FileNameInDocLib = drFile.FileNameInDocLib;
                projectFile.FilePurpose = drFile.FilePurpose;
                projectFile.FilePath = drFile.FilePath;
                projectFile.FileSize = drFile.FileSize;
                projectFile.CreatedBy = drFile.CreatedBy;
                projectFile.Created = drFile.Created;
                projectFile.Flag = drFile.Flag;
                db.proj_ProjectFile.Add(projectFile);
                db.SaveChanges();
                return projectFile.FileID;
            }
        }

        public static long UpdataFile(proj_ProjectFile drFile)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                proj_ProjectFile projectFile = db.proj_ProjectFile.SingleOrDefault(p => p.FileID == drFile.FileID);
                if (projectFile != null)
                {
                    projectFile.FileName = drFile.FileName;
                    projectFile.FileNameInDocLib = drFile.FileNameInDocLib;
                    //projectFile.FilePurpose = drFile.FilePurpose;
                    projectFile.FilePath = drFile.FilePath;
                    projectFile.FileSize = drFile.FileSize;
                    projectFile.ModifiedBy = drFile.ModifiedBy;
                    projectFile.Modified = drFile.Modified;
                    
                    db.SaveChanges();
                   
                } 
                return 1;
            }
        }
        /// <summary>
        /// 如果网站中不存在保存活动附件的库，则创建他们
        /// </summary>
        /// <param name="listName">Name of the list.</param>
        /// <returns>SPList.</returns>
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
        /// 判断当前文件记录是否在数据库中存在
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="createdBy"></param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static proj_ProjectFile JudgeIsExistInDb(string fileName, long createdBy)
        {
            return GetFiles().FirstOrDefault(f => f.FileName == fileName && f.CreatedBy == createdBy);
        }
        /// <summary>
        /// 判断当前文件记录是否在文档库(type=1)/图片库(type=2)/资产库(type=3或type=4)中存在
        /// </summary>
        /// <param name="docLib">The document library.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool JudgeIsExistInDocLib(SPList docLib, string fileName, int type)
        {
            bool isExist = false;

            if (type == 3 || type == 4)//音视频或压缩文件
            {
                //网站开启了资产库功能(富媒体)
                for (int i = 0; i < docLib.RootFolder.SubFolders.Count; i++)
                {
                    if (docLib.RootFolder.SubFolders[i].Files[0].Name == fileName)
                    {
                        isExist = true;
                        break;
                    }
                }
                //网站没开启资产库
                for (int i = 0; i < docLib.RootFolder.Files.Count; i++)
                {
                    if (docLib.RootFolder.Files[i].Name == fileName)
                    {
                        isExist = true;
                        break;
                    }
                }

            }
            else//文档库或图片库
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

        /// <summary>
        /// Gets the file types.
        /// </summary>
        /// <returns>List&lt;FileType&gt;.</returns>
        public static List<FileType> GetFileTypes()
        {
            using (ProjectEntities db=new ProjectEntities())
            {
                List<FileType> ftList = db.FileType.ToList();
                return ftList;
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <param name="delBy">删除人ID</param>
        /// <param name="delTime">删除时间</param>
        /// <returns></returns>
        public static long DelFileById(long fileId, long delBy, DateTime delTime)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                proj_ProjectFile file = db.proj_ProjectFile.SingleOrDefault(p => p.FileID == fileId);
                if (file != null)
                {
                    file.ModifiedBy = delBy;
                    file.Modified = delTime;
                    file.Flag = 0;//项目删除
                    db.SaveChanges();
                }
                return 1;
            }

        }
    }
}
