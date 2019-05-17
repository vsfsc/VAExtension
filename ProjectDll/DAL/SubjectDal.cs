using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDll.DAL
{
    public class SubjectDal
    {
        public static List<Subject> GetSubjects()
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<Subject> sbList = db.Subject.ToList();
                
                return sbList;
            }
        }
    }
}
