using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CourseDll.DAL
{
    public class User
    {      
        #region 添加数据
        /// <summary>
        /// 添加用户信息 
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <returns></returns>
        public static long InsertUser(DataRow dr)
        {
            return ((long)CourseDll.DAL.SqlHelper.ExecuteNonQueryTypedParamsOutput(CourseDll.DAL.DataProvider.ConnectionString, "InsertUser", dr)[0].Value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static long InsertUser(SqlTransaction trans, DataRow dr)
        {
            return ((long)CourseDll.DAL.SqlHelper.ExecuteNonQueryTypedParamsOutput(trans, "InsertUser", dr)[0].Value);
        }
       
        #endregion
        #region 更新数据
        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <returns></returns>
        public static int UpdateUser(DataRow dr)
        {
            return (CourseDll.DAL.SqlHelper.ExecuteAppointedParameters(CourseDll.DAL.DataProvider.ConnectionString, "UpdateUser", dr));
        }
        public static int UpdateUser(SqlTransaction trans, DataRow dr)
        {
            return (CourseDll.DAL.SqlHelper.ExecuteAppointedParameters(trans, "UpdateUser", dr));
        }
      
       
        #endregion
        #region 获取数据
        public static DataSet GetUserByAccount(string account)
        {
            DataSet ds = CourseDll.DAL.SqlHelper.ExecuteDataset(CourseDll.DAL.DataProvider.ConnectionString, "GetUserByAccount", account);
            return ds;
        }
        #endregion

    }
}
