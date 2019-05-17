using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDll.DAL
{
    public class ProjectDal
    {
        public static long NewProjectApprove(Proj_ProjectApprove approve)
        {
            using (ProjectEntities db=new ProjectEntities ())
            {
                Proj_ProjectApprove pa = new Proj_ProjectApprove();
                pa.ApprovedBy = approve.ApprovedBy;
                pa.ApprovedTime = approve.ApprovedTime;
                pa.Comments = approve.Comments;
                pa.Flag = approve.Flag;
                pa.ProjectID = approve.ProjectID;
                pa.Result = approve.Result;
                db.Proj_ProjectApprove.Add(pa);
                db.SaveChanges();
                return pa.ApproveID;
            }
        }
        /// <summary>
        /// 创建新项目
        /// </summary>
        /// <param name="dr"></param>
        public static long NewProject(Proj_Project dr)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                Proj_Project pr = new Proj_Project();
                pr.PName = dr.PName;
                pr.PCode = dr.PCode;
                pr.Budget = dr.Budget;
                pr.Sponsor = dr.Sponsor;
                pr.CreatedDate = dr.CreatedDate;
                pr.TypeID = dr.TypeID;
                pr.SubjectID = dr.SubjectID;
                pr.Introduce = dr.Introduce;
                pr.Background = dr.Background;
                pr.Implementation = dr.Implementation;
                pr.ExpectedGoal = dr.ExpectedGoal;
                pr.StateID = 1;
                pr.IsMatch = dr.IsMatch;
                pr.Flag = dr.Flag;
                db.Proj_Project.Add(pr);
                db.SaveChanges();
                return pr.ProjectID;
            }
        }
        public static void UpdateProjectById(Proj_Project dr)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                Proj_Project pr = db.Proj_Project.SingleOrDefault(p => p.ProjectID == dr.ProjectID);
                if (pr != null)
                {
                    pr.SubjectID = dr.SubjectID;
                    pr.Introduce = dr.Introduce;
                    pr.Background = dr.Background;
                    pr.Implementation = dr.Implementation;
                    pr.ExpectedGoal = dr.ExpectedGoal;
                    pr.ModifiedBy = dr.ModifiedBy;
                    pr.ModifyTime = dr.ModifyTime;
                    pr.IsMatch = dr.IsMatch;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="delBy"></param>
        /// <param name="closedTime"></param>
        /// <returns></returns>
        public static long DelProjectById(Proj_Project dr, long delBy, DateTime closedTime)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                Proj_Project pr = db.Proj_Project.SingleOrDefault(p => p.ProjectID == dr.ProjectID);
                pr.ModifiedBy = delBy;
                pr.ModifyTime = closedTime;
                pr.Flag = 0;//项目删除
                db.SaveChanges();
                return 1;
            }

        }
        public static long ChangeStateByProjectId(long projectId, int stateId)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                Proj_Project pr = db.Proj_Project.SingleOrDefault(p => p.ProjectID == projectId);
                pr.StateID = stateId;//项目状态变更
                db.SaveChanges();
                return 1;
            }

        }
        /// <summary>
        /// 变更项目状态(项目状态：0.未启动；1.进行中；2.已结束；3. 已转化；4.延期；5.暂停；-1.关闭)
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="stateId">状态参数</param>
        /// <param name="changedBy">状态变更人</param>
        /// <param name="changedTime">状态变更时间</param>
        /// <returns></returns>
        public static long ChangeStateByProjectId(long projectId,int stateId, long changedBy, DateTime changedTime)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                Proj_Project pr = db.Proj_Project.SingleOrDefault(p => p.ProjectID == projectId);
                pr.ModifiedBy = changedBy;
                pr.ModifyTime = changedTime;
                pr.StateID = stateId;//项目状态变更
                db.SaveChanges();
                return 1;
            }

        }
        /// <summary>
        /// 根据项目标题查询项目
        /// </summary>
        /// <param name="periodsTitle">项目标题</param>
        /// <returns></returns>
        public static Proj_Project GetProjectByTitle(string periodsTitle)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                Proj_Project ps = db.Proj_Project.FirstOrDefault(p => p.PName == periodsTitle);
                return ps;
            }
        }



        /// <summary>
        /// Gets the projects to experts.
        /// </summary>
        /// <returns>List&lt;Proj_ToExpertProj&gt;.</returns>
        public static List<Proj_ToExpertProj> GetProjectsToExperts()
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<Proj_ToExpertProj> psList = db.Proj_ToExpertProj.ToList();
                return psList;
            }
        }

        /// <summary>
        /// 获取所有项目
        /// </summary>
        /// <returns></returns>
        public static List<Proj_Project> GetProjects()
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<Proj_Project> prList = db.Proj_Project.ToList();
                return prList;
            }
        }

        /// <summary>
        /// 获取项目中所有成员
        /// </summary>
        /// <param name="projId">项目ID</param>
        /// <returns></returns>
        public static List<Proj_UserProject> GetProjectMembers(long projId)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<Proj_UserProject> prList = db.Proj_UserProject.Where(up => up.ProjectID == projId).ToList();
                return prList;
            }
        }

        /// <summary>
        /// 获取某个用户参与的所有项目
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static List<Proj_UserProject> GetUserProjects(long userId)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<Proj_UserProject> prList = db.Proj_UserProject.Where(up=>up.UserID==userId).ToList();
                return prList;
            }
        }
        /// <summary>
        /// 根据发布者ID获取该发布者创建的所有项目
        /// </summary>
        /// <param name="sponsor">项目发布者ID</param>
        /// <returns></returns>
        public static List<Proj_Project> GetPeriodByUserId(long sponsor)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<Proj_Project> pd = db.Proj_Project.Where(p => p.Sponsor == sponsor).ToList();
                return pd;
            }
        }
        /// <summary>
        /// 获取所有状态的项目信息
        /// </summary>
        /// <returns></returns>
        public static List<Proj_ProjectState> GetProjectStates()
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<Proj_ProjectState> psList = db.Proj_ProjectState.ToList();
                return psList;
            }
        }
        /// <summary>
        /// 获取用户与项目创建的关联
        /// </summary>
        /// <returns></returns>
        public static List<Proj_ProjectsCreatedByUser> GetProjectsCreatedByUser()
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<Proj_ProjectsCreatedByUser> psList = db.Proj_ProjectsCreatedByUser.ToList();
                return psList;
            }
        }
        /// <summary>
        /// 获取用户与项目参与的关联
        /// </summary>
        /// <returns></returns>
        public static List<Proj_ProjectsIAmINorOut> GetProjectsUserINorOut()
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<Proj_ProjectsIAmINorOut> psList = db.Proj_ProjectsIAmINorOut.ToList();
                return psList;
            }
        }
        /// <summary>
        /// 添加成员参加项目信息，表Proj_Member
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static long NewProjectMember(Proj_Member dr)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                Proj_Member pr = new Proj_Member();
                pr.UserID = dr.UserID;
                pr.ProjectID = dr.ProjectID;
                pr.ApplyInTime = dr.ApplyInTime;
                pr.ApplyOutTime = dr.ApplyOutTime;
                pr.OutReason = dr.OutReason;
                pr.Score = dr.Score;
                pr.Flag = dr.Flag;
                db.Proj_Member.Add(pr);
                db.SaveChanges();
                return 1;
            }
        }
        /// <summary>
        /// 添加申请对接的信息
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static long  NewProjectMatch(Proj_Match dr)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                Proj_Match pr = new Proj_Match();
                pr.ProjectID = dr.ProjectID;
                pr.MatchSender = dr.MatchSender;
                pr.EnterpriseName = dr.EnterpriseName;
                pr.Name = dr.Name;
                pr.Email = dr.Email;
                pr.Telephone = dr.Telephone;
                pr.SendTime = dr.SendTime;
                pr.IsAccepted = dr.IsAccepted;
                pr.Description = dr.Description;
                pr.Flag = dr.Flag;
                db.Proj_Match.Add(pr);
                db.SaveChanges();
                return 1;
            }
        }
        /// <summary>
        /// 获取Proj_Match表中所有信息
        /// </summary>
        /// <returns></returns>
        public static List<Proj_Match> GetProjectsMatch()
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<Proj_Match> psList = db.Proj_Match.ToList();
                return psList;
            }
        }
        public static long  UpdateProjectsMatch(long MatchID,int IsAccetpted)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                Proj_Match pr = db.Proj_Match.SingleOrDefault(p =>p.MatchID == MatchID);
                pr.IsAccepted =IsAccetpted;
                pr.AcceptedTime = DateTime.Now;
                db.SaveChanges();
            }
            return 1;
        }
        /// <summary>
        /// 获取视图Proj_MatchResult的信息
        /// </summary>
        /// <returns></returns>
        public static List<Proj_MatchResult> GetProjMatchResult()
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<Proj_MatchResult> psList = db.Proj_MatchResult.ToList();
                return psList;
            }
        }    

        public static List<Proj_CountProjMember> GetProjMemberCount()
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<Proj_CountProjMember> psList = db.Proj_CountProjMember.Where(ps=>ps.StateID==3).OrderByDescending(ps=>ps.MemberCount).Take(10).ToList(); //项目进行中，stateId=3;
                return psList;
            }
        }
        /// <summary>
        /// 获取参加过项目的所有成员，从视图Proj_VUserScore
        /// </summary>
        /// <returns></returns>

        public static long[] GetProjUser()
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<Proj_VUserScore> psList = db.Proj_VUserScore.ToList();
                long[] ArrUserId= psList.Select(ps => ps.UserID).Distinct().ToArray();
               
                return ArrUserId;
            }
        }
        /// <summary>
        /// 获取每个用户参与所有项目的得分
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<Proj_VUserScore> GetProjMemScoreById(long userId)
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                List<Proj_VUserScore> psList = db.Proj_VUserScore.Where(ps => ps.UserID == userId).ToList(); 
                return psList;
            }
        }
        
    }
}
