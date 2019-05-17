// ***********************************************************************
// Assembly         : ProjectDll
// Author           : administrator
// Created          : 04-18-2016
//
// Last Modified By : administrator
// Last Modified On : 04-22-2016
// ***********************************************************************
// <copyright file="ProjectBll.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectDll.BLL
{
    /// <summary>
    /// Class ProjectBll.
    /// </summary>
    public class ProjectBll
    {
        /// <summary>
        /// 根据状态ID分类显示项目列表
        /// </summary>
        /// <param name="stateId">项目状态ID</param>
        /// <returns>List&lt;Proj_ProjectState&gt;.</returns>
        public static List<Proj_ProjectState> GetProjectStates(int stateId)
        {
            List<Proj_ProjectState> psList = DAL.ProjectDal.GetProjectStates().Where(ps => ps.StateID == stateId).ToList();
            return psList;
        }
        public static Proj_ProjectState GetProjectStateByProjectId(long projectId)
        {
            Proj_ProjectState project = DAL.ProjectDal.GetProjectStates().FirstOrDefault(p => p.ProjectID == projectId);
            return project;
        }
        /// <summary>
        /// Gets the project by identifier.
        /// </summary>
        /// <param name="projectID">The project identifier.</param>
        /// <returns>List&lt;Proj_Project&gt;.</returns>
        public static List<Proj_Project> GetProjectByID(long projectID)
        {
            List<Proj_Project> AllProjects = DAL.ProjectDal.GetProjects();
            List<Proj_Project> projects = AllProjects.Where(p => p.ProjectID == projectID).ToList();
            return projects;
        }
        /// <summary>
        /// 获取指定用户ID的用户创建的项目
        /// </summary>
        /// <param name="userId">发布者ID</param>
        /// <returns>List&lt;Proj_ProjectsCreatedByUser&gt;.</returns>
        public static List<Proj_ProjectsCreatedByUser> GetProjectsCreatedByUserId(long userId)
        {

            List<Proj_ProjectsCreatedByUser> psList = DAL.ProjectDal.GetProjectsCreatedByUser().Where(ps => ps.Sponsor == userId).ToList();
            return psList;

        }

        /// <summary>
        /// 获取指定ID的用户参与的项目
        /// </summary>
        /// <param name="userId">参与者ID</param>
        /// <returns>List&lt;Proj_ProjectsIAmINorOut&gt;.</returns>
        public static List<Proj_ProjectsIAmINorOut> GetProjectsUserINorOut(long userId)
        {
            List<Proj_ProjectsIAmINorOut> psList = DAL.ProjectDal.GetProjectsUserINorOut().Where(ps => ps.UserID == userId).ToList();
            return psList;
        }
        #region 审批
        /// <summary>
        ///  Approve Projects.
        /// </summary>
        /// <param name="pApprove">The Project approve.</param>
        /// <param name="pStateId">StateID of the Project.</param>
        public static void ProjectApprove (Proj_ProjectApprove pApprove,int pStateId)
        {
            DAL.ProjectDal.ChangeStateByProjectId((long)pApprove.ProjectID, pStateId);
            DAL.ProjectDal.NewProjectApprove(pApprove);

        }
        #endregion
        /// <summary>
        /// 获取对应状态的项目
        /// </summary>
        /// <param name="stateId">项目状态ID</param>
        /// <returns>List&lt;Proj_Project&gt;.</returns>
        public static List<Proj_Project> GetProjectsByState(long stateId)
        {
            List<Proj_Project> prList = DAL.ProjectDal.GetProjects().Where(pr => pr.StateID == stateId).ToList();
            return prList;
        }
        /// <summary>
        /// 获取可进行对接的项目信息
        /// </summary>
        /// <param name="isMatch">The is match.</param>
        /// <returns>List&lt;Proj_ProjectState&gt;.</returns>
        public static List<Proj_ProjectState> GetMatchProjects(int isMatch)
        {
            List<Proj_ProjectState> psList = DAL.ProjectDal.GetProjectStates().Where(ps => ps.IsMatch == isMatch).ToList();
            return psList;
        }
        /// <summary>
        /// 获取指定专家ID要评分的项目
        /// </summary>
        /// <param name="expertId">专家ID</param>
        /// <returns>List&lt;Proj_ProjectsCreatedByUser&gt;.</returns>
        public static List<Proj_ToExpertProj> GetProjectsToExpertByExpertId(long expertId)
        {
            List<Proj_ToExpertProj> psList = DAL.ProjectDal.GetProjectsToExperts().Where(ps => ps.UserID == expertId).ToList();
            return psList;
        }
        /// <summary>
        /// 获取当前用户申请对接的信息
        /// </summary>
        /// <param name="MatcheSender"></param>
        /// <returns></returns>
        public static List<Proj_Match> GetProjectsrMatchByUser(long MatcheSender)
        {
            List<Proj_Match> psList = DAL.ProjectDal.GetProjectsMatch().Where(ps => ps.MatchSender == MatcheSender).ToList();
            return psList;
        }
        /// <summary>
        /// 根据projectID获取申请对接的那些企业或者个人的信息
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public static List<Proj_Match> GetMatchByProjID(long projectID)
        {
            List<Proj_Match> psList= DAL.ProjectDal.GetProjectsMatch().Where(ps => ps.ProjectID == projectID).ToList();
            return psList;
        }
        public static List<Proj_MatchResult> GetMyProjMatchInfo(long currentUserId)
        {
            List<Proj_MatchResult> psList = DAL.ProjectDal.GetProjMatchResult().Where(ps => ps.MatchSender == currentUserId).ToList();
            return psList;
        }
    }
}
