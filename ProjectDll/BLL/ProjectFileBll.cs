using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDll.BLL
{
    using System.IO;

    public class ProjectFileBll
    {
        /// <summary>
        /// Gets the file by projid and purpose.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="purposeId">The purpose identifier.</param>
        /// <returns>List&lt;proj_ProjectFile&gt;.</returns>
        public static List<proj_ProjectFile> GetFileByProjIdandPurpose(long projectId, int purposeId)
        {
                var pfList = DAL.ProjectFileDAL.GetFiles().Where(pf => pf.ProjectID == projectId && pf.FilePurpose == purposeId).ToList();
                return pfList;
        }
        /// <summary>
        /// Gets the file types by parentid.
        /// </summary>
        /// <param name="parentId">The parent identifier.</param>
        /// <returns>List&lt;FileType&gt;.</returns>
        public static List<FileType> GetFileTypesByParentId(int parentId)
        {
            var ftList = DAL.ProjectFileDAL.GetFileTypes().Where(ft => ft.ParentID ==parentId).ToList();
            return ftList;
        }
        public static FileType GetFileTypeByFileExtension(string fileExtension)
        {
            var ftype = DAL.ProjectFileDAL.GetFileTypes().FirstOrDefault(ft => ft.TypeName == fileExtension);
            return ftype;
        }
        public static proj_ProjectFile GetFileByFileId(long fileId)
        {
            var file = DAL.ProjectFileDAL.GetFiles().FirstOrDefault(f => f.FileID == fileId);
            return file;
        }
        public static proj_ProjectFile GetFileByFileNameInDocLib(string fileNameInDocLib)
        {
            proj_ProjectFile file = DAL.ProjectFileDAL.GetFiles().FirstOrDefault(f => f.FileNameInDocLib == fileNameInDocLib&f.Flag==1);
            return file;
        }
        public static List<proj_ProjectFile> GetFileByCreatedBy(long userId)
        {
            var files = DAL.ProjectFileDAL.GetFiles().Where(f => f.CreatedBy == userId).OrderByDescending(f=>f.Created).ToList();
            return files;
        }
    }
}
