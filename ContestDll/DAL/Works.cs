using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Transactions;

namespace ContestDll.DAL
{
    public class Works
    {
        #region 专家陪分
        //获取待分组未评审的作品
        public static List<CSWorksPartition> GetWorksPartition(long contestID,string worksName)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<CSWorksPartition> csExpertCore ;
                if (worksName=="" )
                    csExpertCore = db.CSWorksPartition.Where(p => p.PeriodID == contestID && p.WorksState == 1).ToList() ;
                else
                    csExpertCore = db.CSWorksPartition.Where(p => p.PeriodID == contestID && p.WorksState == 1 && p.WorksName.Contains(worksName )).ToList();
                return csExpertCore;
            }
        }
        //获取决赛评分标准
        public static List<FinalsScoreStandard> GetFinalsScoreStandard()
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<FinalsScoreStandard> csExpertCore = db.FinalsScoreStandard.ToList(); 
                return csExpertCore;
            }
        }

        //当前作家待评作品
        public static List<CSFinalsExpertWillScore > GetFinalsExpertWillScore(int groupID, long expertID)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<CSFinalsExpertWillScore> csExpertCore = db.CSFinalsExpertWillScore.Where(p => p.ExpertID != expertID && p.GroupID !=groupID ).ToList();
                return csExpertCore;
            }
        }
        public static List<CSFinalsExpertGroup> GetExpertGroup(long contestID)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<CSFinalsExpertGroup> csExpertCore = db.CSFinalsExpertGroup.Where(p => p.ContestID ==contestID ).ToList();
                return csExpertCore;
            }
        }
        #endregion
        /// <summary>
        /// 获取每个作品的评分信息
        /// </summary>
        /// <param name="worksID"></param>
        /// <returns></returns>
        public static List<WorksExpert > GetWorksExpertByWorksID(long worksID)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<WorksExpert> works = db.WorksExpert .Where(p => p.WorksID==worksID ).ToList();
                return works;
            }
        }
        /// <summary>
        /// 获取期次下的学生作品
        /// </summary>
        /// <param name="periodID"></param>
        /// <returns></returns>
        public static List<CSWorksWorksType> GetWorksByPeriodID(long periodID)
        {
            using (ContestEntities db=new ContestEntities ())
            {
                List < CSWorksWorksType > works= db.CSWorksWorksType.Where(p => p.PeriodID == periodID).ToList();
                return works;
            }
        }
        public static  ContestDll.Works    GetWorksAllotTimesByWorsID(long WorksID)
        {
            using (ContestEntities db = new ContestEntities())
            {
                 ContestDll.Works  works = db.Works.SingleOrDefault(p => p.WorksID == WorksID); 
                return works;
            }
        }
        /// <summary>
        /// 获取样例作品
        /// </summary>
        /// <param name="periodID"></param>
        /// <returns></returns>
        public static List<CSWorksWorksType> GetSampleWorksByPeriodID(long periodID)
        {
            using (ContestEntities db=new ContestEntities ())
            {
                List<CSWorksWorksType> works = db.CSWorksWorksType.Where(p => p.IsSample == 1 && p.PeriodID == periodID).ToList() ;
                return works ;
            }

        }

        /// <summary>
        /// 通过期次和账户获取该用户在该期次中的作品,VIEW
        /// </summary>
        /// <param name="periodID">期次ID</param>
        /// <param name="account">用户帐户名</param>
        /// <returns>DataSet</returns>
        public static CSUserWorks  GetWorksByPeriodAndAccount(long periodID, string account)
        {
            using (ContestEntities db=new ContestEntities ())
            {
                List<CSUserWorks> cs = db.CSUserWorks.Where(p => p.PeriodID == periodID && p.Account == account).ToList() ;
                if (cs.Count > 0)
                    return cs[0];
                return null;
            }
        }
        /// <summary>
        /// 获取UserID可以评审但还未分配完成的作品
        /// </summary>
        /// <param name="userId">评分人ID</param>
        /// <param name="periodId">当前期次ID</param>
        /// <param name="allotTimes">本期互评组内人数</param>
        /// <returns></returns>
        public static List<ContestDll .Works>    GetWorksToAllot(long userId, long periodId, long allotTimes)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<ContestDll.Works> w = db.Works.Where(p => p.PeriodID == periodId && p.IsSample == 0 && p.Flag > 0 && p.CreatedBy != userId && p.AllotTimes < allotTimes).OrderBy(p=>p.AllotTimes ) .ToList();
                return w;
            }

        }
        /// <summary>
        /// 获取学生待评审的作品
        /// </summary>
        /// <param name="expertID">评分人ID</param>
        /// <returns></returns>
        public static List<CSWorksToEvaluate > GetWorksToEvaluate(long expertID, long periodID, int isSample)
        {
            using (ContestEntities db=new ContestEntities ())
            {
                List<CSWorksToEvaluate> ds;
                if (isSample == 0)
                    ds = db.CSWorksToEvaluate.Where(p => p.ExpertID == expertID && p.PeriodID == periodID && p.Flag == 1).ToList();
                else
                    ds = db.CSWorksToEvaluate.Where(p => p.ExpertID == expertID && p.PeriodID == periodID && p.Flag >2).ToList();
                return ds;
            }

        }
        /// <summary>
        /// 获取评分标准的子级
        /// </summary>
        /// <returns></returns>
        public static List<ScoreStandard > GetScoreStandardSubLevel()
        {
            using (ContestEntities db=new ContestEntities ())
            {
                List<ScoreStandard> cs = db.ScoreStandard.Where(p=>p.Flag ==1) .ToList();
                return cs;
            }

        }
     
        /// <summary>
        /// 获取一个期次中所有作品以及作品对应的独创作者或者队长
        /// </summary>
        /// <returns></returns>
        public static DataSet GetUserIdWorksIdByPeriodId(long periodId)
        {
            DataSet ds = null;
            return ds;
        }
        /// <summary>
        /// 获取每个期次下分配给我评分的作品
        /// </summary>
        /// <param name="periodId">期次ID</param>
        /// <param name="expertId">我的ID</param>
        /// <returns></returns>
        public static List<CSVWorksForMe> GetWorksForMeByPeriodId(long periodId, long expertId)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<CSVWorksForMe> details = db.CSVWorksForMe.Where(p=>p.ExpertID ==periodId && p.PeriodID ==periodId ).ToList();
                return details;
            }
        }
        /// <summary>
        /// 通过ID获取评分明细
        /// </summary>
        /// <param name="worksExpertsID"></param>
        /// <returns></returns>
        public static List<RatingDetails>   GetRatingDetailsByWorksExpertID(long worksExpertsID)
        {
           using (ContestEntities db=new ContestEntities ())
           {
               List<RatingDetails> details = db.RatingDetails.Where(p => p.Flag == 1 && p.WorksExpertID == worksExpertsID).ToList();
               return details;
           }
        }
        /// <summary>
        /// 获取作品分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<WorksExpert > GetWorksExpertByID(long worksExpertID)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<WorksExpert> details = db.WorksExpert.Where(p => p.WorksExpertID == worksExpertID).ToList();
                return details;
            }

        }
        /// <summary>
        /// 根据作品ID获取作品评分
        /// </summary>
        /// <param name="worksID">作品ID</param>
        /// <returns></returns>
        public static List<CSWorksExpertUser > GetWorksCommentsByWorksID(long worksID)
        {
           using (ContestEntities db=new ContestEntities ())
           {
               List<CSWorksExpertUser> ds = db.CSWorksExpertUser.Where(p => p.WorksID == worksID).ToList();
               return ds;
           }

        }
        /// <summary>
        /// 获取提交的作品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<ContestDll.Works> GetWorksSubmitByID(long worksID)
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<ContestDll.Works> ds = db.Works .Where(p => p.WorksID == worksID).ToList();
                return ds;
            }
        }
        /// <summary>
        /// 通过用户账户获取分配给我待评价的作品
        /// </summary>
        /// <param name="userAccount">用户账户</param>
        /// <returns></returns>
        public static DataSet GetWorksForMeToScore(string userAccount)
        {
            DataSet ds = null;
            return ds;
        }
        /// <summary>
        /// 通过作品类别获取评分标准
        /// </summary>
        /// <param name="worksTypeID"></param>
        /// <returns></returns>
        public static List <CSPeriodScoreStandard > GetScoreStandardByWorksType(int worksTypeID, long periodID)
        {
           using (ContestEntities db=new ContestEntities ())
           {
               List<CSPeriodScoreStandard> pS = db.CSPeriodScoreStandard.Where(p => p.WorkTypeID == worksTypeID && p.PeriodID == periodID).ToList ();
               return pS;
           }
        }
        /// <summary>
        /// 根据作品ID和作品类别获取作品文件
        /// </summary>
        /// <param name="WorksID">作品ID</param>
        /// <param name="Type">作品类别</param>
        /// <returns></returns>
        public static List<WorksFile>  GetWorksFile(long WorksID, int Type)
        {
            List<WorksFile> files;
            using (ContestEntities db=new ContestEntities ())
            {
                if (Type==4)
                {
                    files = db.WorksFile.Where(p => p.WorksID == WorksID && p.Type == Type).ToList();
                }
                else
                {
                    files = db.WorksFile.Where(p => p.WorksID == WorksID && p.Type!=4).ToList();

                }
                return files;
            }
        }
        public static List<ContestDll.WorksType> GetWorksType()
        {
            using (ContestEntities db = new ContestEntities())
            {
                List<ContestDll. WorksType> wt = db.WorksType.ToList();
                return wt;
            }
        }
        public static long UpdateWorksSubmit(ContestEntities db, CSWorksWorksType dr)
        {
            ContestDll.Works works = db.Works.SingleOrDefault(p=>p.WorksID ==dr.WorksID );
            works.WorksState = dr.WorksState;
            works.Score = dr.Score;
            db.SaveChanges();
            return 1;
        }
        public static long UpdateWorksExpert( WorksExpert  dr)
        {
            using (ContestEntities db=new ContestEntities ())
            {
                WorksExpert dr1 = db.WorksExpert.SingleOrDefault(p => p.WorksExpertID == dr.WorksExpertID);
                dr1.WorksID = dr.WorksID;
                dr1.ExpertID = dr.ExpertID;
                dr1.Score = dr.Score;
                dr1.Comments = dr.Comments;
                dr1.Modified = dr.Modified;
                dr1.Flag = dr.Flag;
                db.SaveChanges();
                return 1;
            }
        }
        public static long InsertWorksExpert(ContestEntities db, WorksExpert dr)
        {
            WorksExpert dr1 = new WorksExpert();
            dr1.WorksID = dr.WorksID;
            dr1.ExpertID = dr.ExpertID;
            dr1.Score = dr.Score;
            dr1.Flag = 1;
            db.WorksExpert.Add(dr1);
            db.SaveChanges();
            return dr1.WorksExpertID;
        }
        public static long UpdateWorksExpert(ContestEntities db,WorksExpert dr)
        {
                WorksExpert dr1 = db.WorksExpert.SingleOrDefault(p => p.WorksExpertID == dr.WorksExpertID);
                dr1.WorksID = dr.WorksID;
                dr1.ExpertID = dr.ExpertID;
                dr1.Score = dr.Score;
                dr1.Comments = dr.Comments;
                dr1.Modified = dr.Modified;
                dr1.Flag = dr.Flag;
                db.SaveChanges();
                return 1;
        }
        /// <summary>
        /// 作品评分明细
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static long InsertRatingDetails(ContestEntities db, RatingDetails dr)
        {
        
                RatingDetails dr1 = new RatingDetails();
                dr1.WorksExpertID = dr.WorksExpertID;
                dr1.StandardID = dr.StandardID;
                dr1.Score = dr.Score;
                dr1.Flag = 1;
                db.RatingDetails.Add(dr1);
                db.SaveChanges();
                return dr1.RatingDetailsID;
          
        }
        public static long UpdateRatingDetails(ContestEntities db,RatingDetails dr)
        {
           
                RatingDetails dr1 = db.RatingDetails.SingleOrDefault(p => p.RatingDetailsID == dr.RatingDetailsID);
                dr1.Score = dr.Score;
                dr1.Flag = dr.Flag ;
                db.SaveChanges();
                return 1;
           
        }
        /// <summary>
        /// 加入新记录
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static long InsertWorksComments(CSWorksExpertUser dr)
        {
           using (ContestEntities db=new ContestEntities ())
           {
               WorksComments dr1 = new WorksComments();
               dr1.WorksID = dr.WorksID;
               dr1.UserID  = dr.ExpertID;
               dr1.Flag = 1;
               db.WorksComments.Add(dr1);
               db.SaveChanges();
               return dr1.CommentsID;
           }
        }

        #region 查询

        public static DataSet GetWorksTypeForEnroll()
        {
            DataSet ds = null;
            return ds;
        }

        public static CSWorksWorksType  GetWorksByWorksID(long WorksID)
        {
            using (ContestEntities db=new ContestEntities ())
            {
                CSWorksWorksType  wk = db.CSWorksWorksType.SingleOrDefault(p => p.WorksID == WorksID);
                return wk;
            }
        }
        /// <summary>
        /// 获取公示的作品，以提交 作品并审批通过
        /// </summary>
        /// <returns></returns>
        public static List<CSWorksPublic> GetWorksPublic(long contestID)
        {
            using (ContestEntities db=new ContestEntities ())
            {
                List<CSWorksPublic> ds = db.CSWorksPublic.Where(p=>p.PeriodID ==contestID ).ToList ();
                return ds;
            }
        }
        /// <summary>
        /// 获取公示的作品，以提交 作品并审批通过
        /// </summary>
        /// <returns></returns>
        
        /// <summary>
        /// 获取我的作品
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        public static List<CSMyWorks>  GetMyWorks(long userID)
        {
           using (ContestEntities db=new ContestEntities ())
           {
               List<CSMyWorks> ds = db.CSMyWorks.Where(p => p.UserID == userID).ToList();
               return ds;
           }
        }
        /// <summary>
        /// 获取公示的作品，以提交 作品并审批通过
        /// </summary>
        /// <returns></returns>
        public static DataSet GetWorksPublicByWorksId(long periodId, long worksId)
        {
            DataSet ds = null;
            return ds;
        }
        #endregion
        #region 插入


        public static long InsertWorks(ContestDll.Works   dr)
        {
            using (ContestEntities db = new ContestEntities())
            {
                ContestDll.Works  file = new ContestDll.Works ();
                file.AllotTimes =dr.AllotTimes;
                file.BackReason =dr.BackReason;
                file.Comment =dr.Comment;
                file.Created =dr.Created;
                file.CreatedBy =dr.CreatedBy;
                file.DemoURL =dr.DemoURL;
                file.DesignIdeas =dr.DesignIdeas;
                file.Flag =dr.Flag;
                file.InstallationGuide=dr.InstallationGuide;
                file.IsSample =dr.IsSample;
                file.KeyPoints =dr.KeyPoints;
                file.Score =dr.Score;
                file.WorksCode =dr.WorksCode;
                file.WorksName =dr.WorksName;
                file.WorksState =dr.WorksState;
                file.WorksTypeID=dr.WorksTypeID;
                file.PeriodID =dr.PeriodID;
                file.SubmitProfile =dr.SubmitProfile;
                file.DemoURL =dr.DemoURL;
                file.DesignIdeas=dr.DesignIdeas;
                file.KeyPoints =dr.KeyPoints;
                file.Score =dr.Score;
                file.Flag = dr.Flag;
                db.Works.Add(file);
                db.SaveChanges();
                return file.WorksID ;
               
            }
        }
        public static long InsertWorksImages(WorksFile dr)
        {
           using (ContestEntities db=new ContestEntities ())
           {
               WorksFile file = new WorksFile();
               file.WorksID = dr.WorksID;
               file.Type = dr.Type;
               file.FileName = dr.FileName;
               file.FilePath = dr.FilePath;
               file.FileSize = dr.FileSize;
               file.Created = dr.Created;
               file.CreatedBy = dr.CreatedBy;
               db.WorksFile.Add(file);
               db.SaveChanges();
               return file.WorksFileID;
           }
        }
        #endregion
        #region 更新
        public static long UpdateWorksFile(WorksFile dr)
        {
            using (ContestEntities db = new ContestEntities())
            {
                WorksFile file = db.WorksFile.SingleOrDefault(p => p.WorksFileID == dr.WorksFileID);
                file.FileSize = dr.FileSize;
                file.Modified = dr.Modified;
                file.ModifiedBy = dr.ModifiedBy;
                file.Flag = dr.Flag;
                db.SaveChanges();
                return 1;
            }
        }
        public static long UpdateWorksFileForSize(WorksFile dr)
        {
            using (ContestEntities db = new ContestEntities())
            {
                WorksFile file = db.WorksFile.SingleOrDefault(p=>p.WorksFileID ==dr.WorksFileID );
                file.FileSize = dr.FileSize;
                file.Modified = dr.Modified;
                file.ModifiedBy = dr.ModifiedBy;
                db.SaveChanges();
               return 1;
            }
        }


        public static long UpdateWorksInfo(ContestDll.Works   dr)
        {
           using (ContestEntities db=new ContestEntities() )
           {
               ContestDll.Works file = db.Works.SingleOrDefault(p => p.WorksID == dr.WorksID);
               file.WorksName = dr.WorksName;
               file.WorksTypeID = dr.WorksTypeID;
               file.WorksCode = dr.WorksCode;
               file.PeriodID = dr.PeriodID;
               file.SubmitProfile = dr.SubmitProfile;
               file.DesignIdeas = dr.DesignIdeas;
               file.KeyPoints = dr.KeyPoints;
               file.DemoURL = dr.DemoURL;
               file.Created = dr.Created;
               file.CreatedBy = dr.CreatedBy;
               file.WorksState = dr.WorksState;
               file.Score = dr.Score;
               file.Flag = dr.Flag;

               db.SaveChanges(); 
               return 1;
           }
        }
        //
        public static long UpdateWorksCode(ContestDll.Works  dr)
        {
             using (ContestEntities db = new ContestEntities())
            {
                ContestDll.Works  file = db.Works.SingleOrDefault(p=>p.WorksID ==dr.WorksID );
                file.WorksCode =dr.WorksCode;
                db.SaveChanges();
               return 1;
            }
        }

        /// <summary>
        /// 对一个作品每分配评分一次,作品分配次数+1
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static long UpdateWorksAllotTimes(ContestDll.Works dr)
        {
            using (ContestEntities db = new ContestEntities())
            {
                ContestDll.Works file = db.Works.SingleOrDefault(p => p.WorksID == dr.WorksID);
                file.AllotTimes = dr.AllotTimes;
                file.WorksState = dr.WorksState;
                db.SaveChanges();
                return 1;
            }
   
        }

        /// <summary>
        /// 获取每个作品的评分信息
        /// </summary>
        /// <param name="worksID"></param>
        /// <returns></returns>
        public static int GetWorksNumByPeriodID(long PeriodID)
        {
            using (ContestEntities db = new ContestEntities())
            {
               List< ContestDll.Works> file = db.Works.Where(p => p.IsSample == 0 && p.Flag > 0 && p.PeriodID == PeriodID).ToList();
               return file.Count;
             }
        }
        /// <summary>
        /// 添加用户作品关系表，无返回行数据 
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <returns></returns>
        
        #endregion


    }
}
