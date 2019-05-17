using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LADLL;

namespace LearningActivity.DAL
{
    public class FileTypeDAL
    {
        public static List<FileType> GetFileTypesByParentId(long parentId)
        {
            LAActivityEntities db = new LAActivityEntities();
            List<FileType> ftList=db.FileType.Where(ft=>ft.ParentID==parentId).ToList();
            return ftList;
        }
    }
}
