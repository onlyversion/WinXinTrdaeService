using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JinTong.Jyrj.Data;
using JinTong.Jyrj.Common;
using System.Data.Common;
using WcfInterface.model;
using System.Data.SqlClient;

namespace WcfInterface
{
    public partial class ComFunction
    {
        #region 系统角色管理
        /// <summary>
        /// 获取系统所有角色数据
        /// </summary>
        /// <returns>角色数据集</returns>
        public static DataTable GetRoles(string sql)
        {
            return DbHelper.ExecQuery(sql);
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <returns></returns>
        public static int AddRole(RoleEntity roleEntity)
        {
            string sqlCommand = string.Format(@"INSERT INTO Base_Role (RoleID,RoleName,Remark)   VALUES ('{0}' ,'{1}' ,'{2}')", roleEntity.RoleID, roleEntity.RoleName, roleEntity.Remark);
            return DbHelper.ExecuteNonQuery(sqlCommand);
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <returns></returns>
        public static int UpdateRole(RoleEntity roleEntity)
        {
            string sqlCommand = string.Format(@"UPDATE  Base_Role SET RoleName = '{0}',Remark = '{1}' WHERE   RoleID = '{2}'", roleEntity.RoleName, roleEntity.Remark, roleEntity.RoleID);
            return DbHelper.ExecuteNonQuery(sqlCommand);
        }
        /// <summary>
        /// 读取角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static DataTable ReadRole(string roleId)
        {
            string sqlCommand = string.Format(@"SELECT  * FROM    Base_Role WHERE ROLEID = '{0}' ", roleId);
            return DbHelper.ExecQuery(sqlCommand);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>影响记录数</returns>
        public static int DeleteRole(string roleId)
        {
            string sqlCommand = string.Format(@"delete from Base_Role where ROLEID='{0}'", roleId);
            return DbHelper.ExecuteNonQuery(sqlCommand);
        }


        /// <summary>
        /// 新增角色权限
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int AddRolePrivilege(RolePrivilegeEntity roleEntity, YicelTransaction tran)
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@PrivilegeId",roleEntity.PrivilegeId),
                new SqlParameter("@RoleID",roleEntity.RoleID)               
            };
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("insert into Base_RolePrivilege({0})", Fields.RolePrivilege_FIELD_List);
            strSql.AppendFormat(" values ({0})", "@" + Fields.RolePrivilege_FIELD_List.Replace(",", ",@"));
            object obj = DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }




        /// <summary>
        /// 根据角色对象删除角色权限关系
        /// </summary>
        /// <param name="roleID">ID</param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int DeleteRolePrivilege(string roleID, YicelTransaction tran)
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@RoleID",roleID)};
            string sqlCommand = string.Format(@"DELETE Base_RolePrivilege where RoleID=@RoleID");
            return DbHelper.ExecuteNonQuery(sqlCommand, parms, tran.Transaction);
        }

        /// <summary>
        /// 根据角色实体对象获取权限记录集
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>权限记录集</returns>
        public static DataTable GetPrivilegesRole(string roleId)
        {
            string sqlCommand = string.Format(@"SELECT  p.PrivilegeId,p.PRIVILEGENAME,p.ParentPrivilegeId FROM    Base_Privilege p, Base_ROLEPRIVILEGE rp
WHERE   p.PrivilegeId = rp.PrivilegeId AND rp.ROLEID = '{0}' ORDER BY p.DISPLAYORDER ASC", roleId);
            return DbHelper.ExecQuery(sqlCommand);
        }
        /// <summary>
        /// 获取带有角色标记的所有权限记录集
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>记录集</returns>
        public static DataTable GetPrivilegesWithRoleSign(string roleId)
        {
            string sqlCommand = string.Format(@"select p.*,(case when( rp.ROLEID is null) then  'false'  else   'true'  end) ISCHECK 
                                    from Base_Privilege p left  join Base_ROLEPRIVILEGE rp
                                     on p.PrivilegeId=rp.PrivilegeId and rp.ROLEID='{0}' where p.Status='1' order by p.DISPLAYORDER ASC", roleId);
            return DbHelper.ExecQuery(sqlCommand);
        }

        #endregion

        #region
        /// <summary>
        /// 新增权限
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public static int AddPrivileges(PrivilegeEntity pEntity)
        {
            string sqlCommand = string.Format(@"INSERT  INTO Base_Privilege ( PrivilegeId,PrivilegeName,PrivilegeCode,ParentPrivilegeId,ParentPrivilegeName,PrivilegeType,Library,NameSpace,MenuPic,Displayorder,Status,Remark )
VALUES  ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}' )", pEntity.PrivilegeId, pEntity.PrivilegeName, pEntity.PrivilegeCode, pEntity.ParentPrivilegeId,
                            pEntity.ParentPrivilegeName, (int)pEntity.PrivilegeType, pEntity.Library, pEntity.NameSpace, pEntity.MenuPic, pEntity.Displayorder, (int)pEntity.Status, pEntity.Remark);
            return DbHelper.ExecuteNonQuery(sqlCommand);
        }
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public static int UpdatePrivileges(PrivilegeEntity pEntity)
        {
            string sqlCommand = string.Format(@"UPDATE  Base_Privilege SET     PrivilegeName = '{1}',PrivilegeCode = '{2}',ParentPrivilegeId = '{3}',ParentPrivilegeName = '{4}',
        PrivilegeType = '{5}',Library = '{6}',NameSpace = '{7}',MenuPic = '{8}',Displayorder = '{9}',Status = '{10}',
        Remark = '{10}' WHERE   PrivilegeId = '{0}'", pEntity.PrivilegeId, pEntity.PrivilegeName, pEntity.PrivilegeCode, pEntity.ParentPrivilegeId,
                            pEntity.ParentPrivilegeName, (int)pEntity.PrivilegeType,
                            pEntity.Library, pEntity.NameSpace, pEntity.MenuPic,
                            pEntity.Displayorder, (int)pEntity.Status, pEntity.Remark);
            return DbHelper.ExecuteNonQuery(sqlCommand);
        }
        /// <summary>
        /// 读取权限
        /// </summary>
        /// <param name="privilegeId"></param>
        /// <returns></returns>
        public static DataTable ReadPrivileges(string privilegeId)
        {
            string sqlCommand = string.Format(@"SELECT  * FROM    Base_Privilege WHERE PrivilegeId = '{0}' ", privilegeId);
            return DbHelper.ExecQuery(sqlCommand);
        }
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="privilegeId">权限ID</param>
        /// <returns>影响记录数</returns>
        public static int DeletePrivileges(string privilegeId)
        {
            string sqlCommand = string.Format(@"delete from Base_Privilege where PrivilegeId='{0}'", privilegeId);
            return DbHelper.ExecuteNonQuery(sqlCommand);
        }
        /// <summary>
        /// 获取子权限列表
        /// </summary>
        /// <param name="privilegeId"></param>
        /// <returns></returns>
        public static DataTable GetPrivilegeList(string privilegeId)
        {
            string strSql = string.Format(@"select * from Base_Privilege where ParentPrivilegeId='{0}' order by Displayorder asc", privilegeId);
            return DbHelper.ExecQuery(strSql);
        }
        /// <summary>
        /// 获取顶级权限列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPrivilegeParentLit()
        {
            string strSql = "SELECT * FROM Base_Privilege p  WHERE p.ParentPrivilegeId is null order by p.Displayorder asc";
            return DbHelper.ExecQuery(strSql);
        }
        /// <summary>
        ///  获取用户顶部菜单
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPrivilegeTopMenu(string userId)
        {
            string strSql = string.Format(@"select p.PrivilegeName,p.PrivilegeId,p.privilegecode,p.MenuPic from Base_UserRole ur
left outer join Base_RolePrivilege rp on ur.ROLEID=rp.ROLEID
left outer join Base_Privilege p on rp.PrivilegeId =p.PrivilegeId
where ur.userId='{0}' and p.ParentPrivilegeId in(select PrivilegeId from Base_Privilege where ParentPrivilegeId is null) order by p.Displayorder asc", userId);
            return DbHelper.ExecQuery(strSql);
        }
        /// <summary>
        /// leftMenu菜单
        /// </summary>
        /// <param name="currentPrivilegId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DataTable GetPrivilegesByParentID(string currentPrivilegId, string userId)
        {
            string strSql = string.Format(@"select p.PrivilegeName,p.PrivilegeId,p.privilegecode,p.MenuPic,p.Library,p.NameSpace from Sys_UserRole ur
left outer join Base_RolePrivilege rp on ur.ROLEID=rp.ROLEID
left outer join Base_Privilege p on rp.PrivilegeId =p.PrivilegeId
where ur.userId='{0}' and p.ParentPrivilegeId='{1}' order by p.Displayorder asc", userId, currentPrivilegId);
            return DbHelper.ExecQuery(strSql);
        }
        /// <summary>
        /// 检查控件权限集合
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable ValidataUserRole(string userId, string type)
        {
            string strSql = string.Format(@"select p.PrivilegeName,p.ParentPrivilegeName,p.PrivilegeCode from Base_Privilege p
inner join Base_RolePrivilege r on r.PrivilegeId=p.PrivilegeId
inner join Base_UserRole u on u.RoleID=r.ROLEID
 where Type='{1}' and u.userId='{0}'  and p.Status='1'", userId, type);
            return DbHelper.ExecQuery(strSql);
        }

        /// <summary>
        /// 根据用户ID获取所有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DataTable UserRolePrivileges(string userId)
        {
            string sqlCommand = string.Format(@"SELECT d.* FROM  dbo.Base_UserRole a
LEFT OUTER JOIN dbo.Base_Role b ON a.RoleID=b.RoleID
LEFT OUTER JOIN dbo.Base_RolePrivilege c ON b.RoleID=c.RoleID
LEFT OUTER JOIN dbo.Base_Privilege d ON c.PrivilegeId=d.PrivilegeId WHERE a.userId = '{0}' ", userId);
            return DbHelper.ExecQuery(sqlCommand);
        }
        /// <summary>
        /// 获取所以权限
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPrivileges()
        {
            string strSql = string.Format(@"select * from Base_Privilege  order by Displayorder asc");
            return DbHelper.ExecQuery(strSql);
        }
        #endregion

        #region 组织机构

        /// <summary>
        /// 新增组织机构
        /// </summary>
        /// <param name="orgEntity"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int AddOrg(OrgEntity orgEntity, YicelTransaction tran)
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@OrgID",orgEntity.OrgID),
                new SqlParameter("@OrgName",orgEntity.OrgName),
                new SqlParameter("@Coperson",orgEntity.Coperson),
                new SqlParameter("@CardType",orgEntity.CardType),
                new SqlParameter("@CardNum",orgEntity.CardNum),
                new SqlParameter("@ParentOrgId",orgEntity.ParentOrgId),   
                new SqlParameter("@ParentOrgName",orgEntity.ParentOrgName),   
                new SqlParameter("@Reperson",orgEntity.Reperson),                
                new SqlParameter("@PhoneNum",orgEntity.PhoneNum),
                new SqlParameter("@TelePhone",orgEntity.TelePhone),
                new SqlParameter("@Email",orgEntity.Email),
                new SqlParameter("@Address",orgEntity.Address),
                new SqlParameter("@AddTime",orgEntity.AddTime),
                new SqlParameter("@Status",(int)orgEntity.Status)
            };
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("insert into Base_Org({0})", Fields.Org_FIELD_List);
            strSql.AppendFormat(" values ({0})", "@" + Fields.Org_FIELD_List.Replace(",", ",@"));
            object obj = DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
            StringBuilder strbld = new StringBuilder();
            string strdt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //构造新增组织用户信息的sql语句
            strbld.AppendFormat(@"insert into Base_User([userId],[userName],[status],[Accounttype],[Account],[LoginPwd],
                                        [CardType],[CardNum],[OrgId],[PhoneNum],[TelNum],[Email],[LinkAdress],[sex],[OpenTime],
                                        [LastUpdateTime],[LastUpdateID],[Online],[UserType],[BindAccount]) 
                                        values('{0}','{1}','{2}','{3}','{4}','{5}',",
                                orgEntity.OrgID, string.IsNullOrEmpty(orgEntity.OrgName) ? string.Empty : orgEntity.OrgName,
                                1, 1, orgEntity.TelePhone, com.individual.helper.Des3.Des3EncodeCBC("123456"));
            strbld.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}',", 1, string.IsNullOrEmpty(orgEntity.CardNum) ? string.Empty : orgEntity.CardNum, orgEntity.OrgID,
                orgEntity.PhoneNum, string.Empty, string.IsNullOrEmpty(orgEntity.Email) ? string.Empty : orgEntity.Email);
            strbld.AppendFormat("'{0}','{1}','{2}','{3}','{4}',{5},{6},'{7}')",
                string.IsNullOrEmpty(orgEntity.Address) ? string.Empty : orgEntity.Address, 1, strdt, strdt, 
                string.Empty, 0, 2, string.Empty);

            obj = DbHelper.ExecuteNonQuery(strbld.ToString(), null, tran.Transaction);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 修改组织机构
        /// </summary>
        /// <param name="orgEntity"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int UpdateOrg(OrgEntity orgEntity, YicelTransaction tran)
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@OrgID",orgEntity.OrgID),
                new SqlParameter("@OrgName",orgEntity.OrgName),
                new SqlParameter("@Coperson",orgEntity.Coperson),
                new SqlParameter("@CardType",orgEntity.CardType),
                new SqlParameter("@CardNum",orgEntity.CardNum),
                new SqlParameter("@ParentOrgID",orgEntity.ParentOrgId),    
                 new SqlParameter("@ParentOrgName",orgEntity.ParentOrgName),
                new SqlParameter("@Reperson",orgEntity.Reperson),                
                new SqlParameter("@PhoneNum",orgEntity.PhoneNum),
               // new SqlParameter("@TelePhone",orgEntity.TelePhone),//组织编码不能修改
                new SqlParameter("@Email",orgEntity.Email),
                new SqlParameter("@Address",orgEntity.Address),
                new SqlParameter("@AddTime",orgEntity.AddTime),
                new SqlParameter("@Status",(int)orgEntity.Status)
            };
            StringBuilder strSql = new StringBuilder();
//            strSql.AppendFormat(@"UPDATE  Base_Org SET     
//                                OrgName = @OrgName,Coperson = @Coperson,CardType =@CardType,CardNum = @CardNum,ParentOrgID = @ParentOrgID,ParentOrgName=@ParentOrgName,Reperson = @Reperson,
//                                PhoneNum = @PhoneNum,TelePhone = @TelePhone,Email = @Email,Address =@Address,AddTime = @AddTime,Status = @Status
//                                WHERE   OrgID = @OrgID", Fields.Org_FIELD_List);
            strSql.AppendFormat(@"UPDATE  Base_Org SET     
                                            OrgName = @OrgName,Coperson = @Coperson,CardType =@CardType,CardNum = @CardNum,ParentOrgID = @ParentOrgID,ParentOrgName=@ParentOrgName,Reperson = @Reperson,
                                            PhoneNum = @PhoneNum,Email = @Email,Address =@Address,AddTime = @AddTime,Status = @Status
                                            WHERE   OrgID = @OrgID", Fields.Org_FIELD_List);
            object obj = DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        public static int UpdateChileOrg(OrgEntity orgEntity, YicelTransaction tran)
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@ParentOrgID",orgEntity.ParentOrgId),    
                 new SqlParameter("@ParentOrgName",orgEntity.ParentOrgName)
            };
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"UPDATE  Base_Org SET  ParentOrgName=@ParentOrgName   WHERE   ParentOrgID = @ParentOrgID");
            object obj = DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        /// <summary>
        /// 读取组织机构
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public static DataTable ReadOrg(string orgID)
        {
            string sqlCommand = string.Format(@"SELECT  * FROM    Base_Org WHERE orgID = '{0}' ", orgID);
            return DbHelper.ExecQuery(sqlCommand);
        }
        /// <summary>
        /// 删除组织机构
        /// </summary>
        /// <param name="orgId">组织机构ID</param>
        /// <param name="tran"></param>
        /// <returns>影响记录数</returns>
        public static int DeleteOrg(string orgId, YicelTransaction tran)
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@OrgID",orgId)};
            string sqlCommand = string.Format(@"delete from Base_Org where orgID=@OrgID");
            return DbHelper.ExecuteNonQuery(sqlCommand, parms, tran.Transaction);
        }
        /// <summary>
        /// 根据组织名称和组织编码，判断组织机构是否重复
        /// </summary>
        /// <param name="orgName"></param>
        /// <param name="telephone">组织编码</param>
        /// <returns></returns>
        public static int IsExitOrgName(string orgName,string telephone)
        {
            if (string.IsNullOrEmpty(orgName)) orgName = string.Empty;
            if (string.IsNullOrEmpty(telephone)) telephone = string.Empty;
            string strSql = string.Format(@"select count(*) from base_org where OrgName='{0}' or telephone='{1}' ", orgName, telephone);
            return (int)DbHelper.ExecQuery(strSql).Rows[0][0];
        }
        /// <summary>
        /// 判断是否存在互为父子关系
        /// </summary>
        /// <param name="orgid"></param>
        /// <param name="parentorgid"></param>
        /// <returns></returns>
        public static int IsExistHuWeiFuzi(string orgid,string parentorgid)
        {
            string strSql = string.Format(@"select count(*) from base_org where orgid='{0}' and ParentOrgID='{1}'", parentorgid, orgid);
            return (int)DbHelper.ExecQuery(strSql).Rows[0][0];
        }

        /// <summary>
        /// 根据组织名称，组织编码和组织ID,判断组织机构是否重复
        /// </summary>
        /// <param name="orgName"></param>
        /// <param name="telephone">组织编码</param>
        /// <param name="orgid">组织ID</param>
        /// <returns></returns>
        public static int IsExitOrgName(string orgName, string telephone,string orgid)
        {
            if (string.IsNullOrEmpty(orgName)) orgName = string.Empty;
            if (string.IsNullOrEmpty(telephone)) telephone = string.Empty;
            if (string.IsNullOrEmpty(orgid)) orgid = string.Empty;

            string strSql = string.Format(@"select count(*) from base_org where orgid<>'{2}' and (OrgName='{0}' or telephone='{1}') ", orgName, telephone,orgid);
            return (int)DbHelper.ExecQuery(strSql).Rows[0][0];
        }

        /// <summary>
        /// 组织机构查询
        /// </summary>
        /// <param name="orgEntity"></param>   
        /// <param name="TdUser"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static DataTable GetOrgList(OrgEntity orgEntity,TradeUser TdUser, int pageindex, int pagesize, ref int page)
        {
            SqlParameter[] paras = null;
            StringBuilder sb = new StringBuilder();

            string PartSearchCondition = string.Empty;
            string ParentOrgID = string.Empty;
            if (UserType.OrgType == TdUser.UType && !string.IsNullOrEmpty(TdUser.OrgId))
            {
                PartSearchCondition = " and orgid in (select orgid from #tmp) ";
                ParentOrgID = TdUser.OrgId;
            }

            if (!string.IsNullOrEmpty(orgEntity.OrgName))
            {
                //PartSearchCondition = " and orgid in (select orgid from #tmp) ";
                //ParentOrgID = orgEntity.OrgName;
                sb.AppendFormat(@" and orgid='{0}'", orgEntity.OrgName);
            }

            if (!string.IsNullOrEmpty(orgEntity.CardNum))
            {
                sb.AppendFormat(@" and CardNum like '{0}%'", orgEntity.CardNum);
            }
            if (!string.IsNullOrEmpty(orgEntity.Reperson))
            {
                sb.AppendFormat(@" and Reperson like '{0}%'", orgEntity.Reperson);
            }
            paras = new SqlParameter[] 
            { 
                new SqlParameter("@selectlist", "OrgID,OrgName,Coperson,CardType,CardNum,ParentOrgID,ParentOrgName,Reperson,PhoneNum,TelePhone,Email,Address,AddTime,Status"),
                new SqlParameter("@SubSelectList", "OrgID,OrgName,Coperson,CardType,CardNum,ParentOrgID,ParentOrgName,Reperson,PhoneNum,TelePhone,Email,Address,AddTime,Status"),
                new SqlParameter("@TableSource", "V_Base_org"),
                new SqlParameter("@TableOrder", "a"),
                new SqlParameter("@SearchCondition", string.Format(@" 1=1 {0} {1}",sb.ToString(),PartSearchCondition)),
                new SqlParameter("@OrderExpression", " order by OrgName desc"),  
                new SqlParameter("@ParentOrgID", ParentOrgID), 
                new SqlParameter("@PageIndex", pageindex),
                new SqlParameter("@PageSize", pagesize),
                new SqlParameter("@PageCount", page)
              };
            paras[9].Direction = ParameterDirection.Output;
            DataTable dt = DbHelper.RunProcedure("GetRecordFromPageEx", paras, "Base_org").Tables[0];
            page = Convert.ToInt32(paras[9].Value);
            return dt;
        }
		
		/// <summary>
		/// 取得所有组织机构
		/// </summary>
        /// <param name="TdUser"></param>
		/// <returns></returns>
		public static DataTable GetBaseOrgListAll(TradeUser TdUser)
	    {
            //string strSql = string.Format("select * from Base_org");
            //return DbHelper.ExecQuery(strSql);

            SqlParameter[] paras = null;

            string SearchCondition = string.Empty;
            string ParentOrgID = string.Empty;
            if (UserType.OrgType == TdUser.UType && !string.IsNullOrEmpty(TdUser.OrgId))
            {
                SearchCondition = " where orgid in (select orgid from #tmp) ";
                ParentOrgID = TdUser.OrgId;
            }
            paras = new SqlParameter[] 
            { 
                new SqlParameter("@selectlist", "OrgID,OrgName,Coperson,CardType,CardNum,ParentOrgID,ParentOrgName,Reperson,PhoneNum,TelePhone,Email,Address,AddTime,Status"),
                new SqlParameter("@TableSource", "Base_org"),
                new SqlParameter("@SearchCondition", SearchCondition),
                new SqlParameter("@ParentOrgID", ParentOrgID)
              };
            DataTable dt = DbHelper.RunProcedure("GetBaseOrg", paras, "Base_org").Tables[0];
            return dt;
	    }

	    #endregion

        #region base_UserRole
        /// <summary>
        /// 新增用户角色
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int AddUserRole(UserRoleEntity roleEntity, YicelTransaction tran)
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@userId",roleEntity.UserId),
                new SqlParameter("@RoleID",roleEntity.RoleID)               
            };
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("insert into Base_UserRole({0})", Fields.UserRole_FIELD_List);
            strSql.AppendFormat(" values ({0})", "@" + Fields.UserRole_FIELD_List.Replace(",", ",@"));
            object obj = DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 修改用户角色
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int UpdateUserRole(UserRoleEntity roleEntity, YicelTransaction tran)
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@userId",roleEntity.UserId),
                new SqlParameter("@RoleID",roleEntity.RoleID)               
            };
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"UPDATE  Base_UserRole SET  RoleID = @RoleID  WHERE   userId = @userId", Fields.UserRole_FIELD_List);
            object obj = DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 读取用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DataTable ReadUserRole(string userId)
        {
            string sqlCommand = string.Format(@"SELECT  * FROM    Base_UserRole WHERE userId = '{0}' ", userId);
            return DbHelper.ExecQuery(sqlCommand);
        }
        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="userId">用户角色ID</param>
        /// <param name="tran"></param>
        /// <returns>影响记录数</returns>
        public static int DeleteUserRole(string userId, YicelTransaction tran)
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@userId",userId)};
            string sqlCommand = string.Format(@"delete from Base_Org where userId=@userId");
            return DbHelper.ExecuteNonQuery(sqlCommand, parms, tran.Transaction);
        }
        #endregion

        #region Base_OrgUser
        /// <summary>
        /// 新增组织机构-用户帐号
        /// </summary>
        /// <param name="orgUserEntity"></param>
        /// <returns></returns>

        public static int AddOrgUser(OrgUserEntity orgUserEntity)
        {
            string sqlCommand = string.Format(@"INSERT INTO Base_OrgUser (OrgID,Account,Status)   VALUES ('{0}' ,'{1}' ,'{2}')", orgUserEntity.OrgID, orgUserEntity.Account, (int)orgUserEntity.Status);
            return DbHelper.ExecuteNonQuery(sqlCommand);
        }

        /// <summary>
        /// 修改组织机构-用户帐号
        /// </summary>
        /// <param name="orgUserEntity"></param>

        public static int UpdateOrgUser(OrgUserEntity orgUserEntity)
        {
            string sqlCommand = string.Format(@"UPDATE  Base_OrgUser SET OrgID = '{0}',Status = '{1}' WHERE   Account = '{2}'", orgUserEntity.OrgID, (int)orgUserEntity.Status, orgUserEntity.Account);
            return DbHelper.ExecuteNonQuery(sqlCommand);
        }
        /// <summary>
        /// 读取组织机构-用户帐号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>

        public static DataTable ReadOrgUser(string account)
        {
            string sqlCommand = string.Format(@"SELECT  * FROM    Base_OrgUser WHERE Account = '{0}' ", account);
            return DbHelper.ExecQuery(sqlCommand);
        }
        /// <summary>
        /// 删除组织机构-用户帐号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>

        public static int DeleteOrgUser(string account)
        {
            string sqlCommand = string.Format(@"delete from Base_OrgUser where Account='{0}'", account);
            return DbHelper.ExecuteNonQuery(sqlCommand);
        }
        #endregion
    }
}
