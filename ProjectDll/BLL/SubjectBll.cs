using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDll.BLL
{
    public class SubjectBll
    {
        public static List<Subject> GetSubjectsByParentId(long parentId)
        {
            List<Subject> sbList = DAL.SubjectDal.GetSubjects().Where(sb=>sb.ParentID==parentId).ToList();
            return sbList;
        }

        public static List<Subject> GetSubjectById(long subjectId)
        {
            List<Subject> sbList = DAL.SubjectDal.GetSubjects().Where(sb => sb.SubjectID == subjectId).ToList();
            return sbList;
        }
    }
}
