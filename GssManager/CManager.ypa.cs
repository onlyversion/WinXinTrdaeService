using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WcfInterface.model;
using WcfInterface;
using JinTong.Jyrj.Common;

namespace GssManager
{
    public partial class CManager : IManager
    {
        #region Base
        public EntityBase _entityBase = null;
        /// <summary>
        /// 基类
        /// </summary>
        public EntityBase entityBase
        {
            get
            {
                if (_entityBase == null)
                    _entityBase = new EntityBase()
                    {
                        Result = false,
                        Desc = ResCode.UL003Desc
                    };
                return _entityBase;
            }
        }
        /// <summary>
        /// 设置错误处理
        /// </summary>
        /// <param name="exStr"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private EntityBase SetException(string exStr, Exception ex)
        {
            entityBase.Result = false;
            //entityBase.Desc = exStr + ex.ToString();
            ManagerLog.WriteErr(ex);
            return entityBase;
        }
        #endregion

        #region Role
       /// <summary>
        /// 向数据库插入角色
       /// </summary>
       /// <param name="loginId"></param>
       /// <param name="roleEntity"></param>
       /// <returns></returns>
        public EntityBase AddRole(string loginId, RoleEntity roleEntity)
        {
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                return entityBase;
            }
            try
            {
                if (ComFunction.AddRole(roleEntity) > 0)
                {
                    entityBase.Result = true;
                    entityBase.Desc = "角色新增成功";
                }
                else
                {
                    entityBase.Result = false;
                    entityBase.Desc = "角色新增失败";
                }
            }
            catch (Exception ex)
            {
                SetException("角色新增失败,原因:", ex);
            }
            return entityBase;
        }
       /// <summary>
        /// 修改角色
       /// </summary>
       /// <param name="loginId"></param>
       /// <param name="roleEntity"></param>
       /// <returns></returns>
        public EntityBase UpdateRole(string loginId, RoleEntity roleEntity)
        {
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                return entityBase;
            }
            try
            {
                if (ComFunction.UpdateRole(roleEntity) > 0)
                {
                    entityBase.Result = true;
                    entityBase.Desc = "角色修改成功";
                }
                else
                {
                    entityBase.Result = false;
                    entityBase.Desc = "角色修改失败";
                }
            }
            catch (Exception ex)
            {
                SetException("角色修改失败,原因:", ex);
            }
            return entityBase;
        }
       /// <summary>
        /// 读取角色
       /// </summary>
       /// <param name="loginId"></param>
       /// <param name="roleID"></param>
       /// <returns></returns>
        public RoleEntity ReadRole(string loginId, string roleID)
        {
            RoleEntity roleEntity = new RoleEntity();
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                return roleEntity;
            }
            try
            {
                DataTable dt = ComFunction.ReadRole(roleID);
                foreach (DataRow dr in dt.Rows)
                {
                    roleEntity = new RoleEntity();
                    roleEntity.RoleID = dr["RoleID"].ToString();
                    roleEntity.RoleName = dr["RoleName"].ToString();
                    roleEntity.Remark = dr["Remark"].ToString();
                }
                roleEntity.Desc = "获取系统角色成功！";
                roleEntity.Result = true;
            }
            catch (Exception ex)
            {
                SetException("获取角色记录数出错,原因:", ex);
            }
            return roleEntity;
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public EntityBase DeleteRole(string loginId, string roleID)
        {
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                return entityBase;
            }
            try
            {
                if (ComFunction.DeleteRole(roleID) > 0)
                {
                    entityBase.Result = true;
                    entityBase.Desc = "角色删除成功";
                }
                else
                {
                    entityBase.Result = false;
                    entityBase.Desc = "角色删除失败";
                }
            }
            catch (Exception ex)
            {
                SetException("角色删除失败,原因:", ex);
            }
            return entityBase;
        }
        /// <summary>
        /// 获取系统所有角色数据
        /// </summary>
        /// <returns>角色数据集</returns>
        public EntityBase GetRoles(string loginId, ref List<RoleEntity> list)
        {
            TradeUser reftradeuser = new TradeUser();
            if (ComFunction.ExistUserLoginID(loginId, ref reftradeuser) == false)
            {
                return entityBase;
            }
            try
            {
                string sqlCommand = @"select * from Base_Role where roleid not in(select roleid from base_userrole 
                    where userid in(select userid from base_user where account in('root','admin')))";
                if ("ROOT" == reftradeuser.Account.ToUpper())
                {
                    sqlCommand = @"select * from Base_Role where roleid not in(select roleid from base_userrole 
                                where userid in(select userid from base_user where account='admin'))";
                }
                else if ("ADMIN" == reftradeuser.Account.ToUpper())
                {
                    sqlCommand = @"select * from Base_Role";
                }
                DataTable dt = ComFunction.GetRoles(sqlCommand);
                RoleEntity roleEntity = null;
                foreach (DataRow dr in dt.Rows)
                {
                    roleEntity = new RoleEntity();
                    roleEntity.RoleID = dr["RoleID"].ToString();
                    roleEntity.RoleName = dr["RoleName"].ToString();
                    roleEntity.Remark = dr["Remark"].ToString();
                    list.Add(roleEntity);
                }
                entityBase.Desc = "获取系统所有角色成功！";
                entityBase.Result = true;
            }
            catch (Exception ex)
            {
                SetException("获取角色记录数出错,原因:", ex);
            }
            return entityBase;
        }

        /// <summary>
        /// 系统角色权限设置
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="list"></param>
        /// <param name="roldeID"></param>
        /// <returns></returns>
        public EntityBase AddRolePrivileges(string loginId, List<RolePrivilegeEntity> list, string roldeID)
        {
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                return entityBase;
            }
            YicelTransaction tran = new YicelTransaction();
            try
            {
                tran.BeginTransaction();
                ComFunction.DeleteRolePrivilege(roldeID, tran);//第一步先删除已有角色权限
                list.ForEach(m => ComFunction.AddRolePrivilege(m, tran));//第二步增加新权限
                tran.Commit();
                entityBase.Result = true;
                entityBase.Desc = "角色新增成功";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                SetException("角色新增失败,原因:", ex);
            }
            return entityBase;
        }
       /// <summary>
        /// 获取系统角色权限列表
       /// </summary>
       /// <param name="loginId"></param>
       /// <param name="roleID"></param>
       /// <param name="list"></param>
       /// <returns></returns>
        public EntityBase GetPrivilegesRole(string loginId, string roleID, ref List<PrivilegeEntity> list)
        {
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                return entityBase;
            }
            try
            {
                DataTable dt = ComFunction.GetPrivilegesRole(roleID);
                PrivilegeEntity pEntity = null;
                foreach (DataRow dr in dt.Rows)
                {
                    pEntity = new PrivilegeEntity();
                    pEntity.PrivilegeId = dr["PrivilegeId"].ToString();
                    pEntity.PrivilegeName = dr["PRIVILEGENAME"].ToString();
                    pEntity.ParentPrivilegeId = dr["ParentPrivilegeId"].ToString();
                    list.Add(pEntity);
                }
                entityBase.Desc = "获取系角色权限成功！";
                entityBase.Result = true;
            }
            catch (Exception ex)
            {
                SetException("获取系角色权限出错,原因:", ex);
            }
            return entityBase;
        }

       

       /// <summary>
        /// 获取带有角色标记的所有权限记录集
       /// </summary>
       /// <param name="loginId"></param>
        /// <param name="roleId"></param>
       /// <param name="list"></param>
       /// <returns></returns>
        public EntityBase GetPrivilegesWithRoleSign(string loginId, string roleId, ref List<PrivilegeEntity> list)
        {
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                return entityBase;
            }
            try
            {
                DataTable dt = ComFunction.GetPrivilegesWithRoleSign(roleId);
                PrivilegeEntity pEntity = null;
                foreach (DataRow dr in dt.Rows)
                {
                    pEntity = new PrivilegeEntity();
                    pEntity.Check = Convert.ToBoolean(dr["ISCHECK"]);
                    pEntity.PrivilegeId = dr["PrivilegeId"].ToString();
                    pEntity.PrivilegeName = dr["PrivilegeName"].ToString();
                    pEntity.PrivilegeCode = dr["PrivilegeCode"].ToString();
                    pEntity.ParentPrivilegeId = dr["ParentPrivilegeId"].ToString();
                    pEntity.ParentPrivilegeName = dr["ParentPrivilegeName"].ToString();
                    pEntity.PrivilegeType = (PrivilegeType)dr["PrivilegeType"];
                    pEntity.Library = dr["Library"].ToString();
                    pEntity.NameSpace = dr["NameSpace"].ToString();
                    pEntity.MenuPic = dr["MenuPic"].ToString();
                    pEntity.Displayorder =(int)dr["Displayorder"];
                    pEntity.Status = (Status)dr["Status"];
                    pEntity.Remark = dr["Remark"].ToString();
                    list.Add(pEntity);
                }
                entityBase.Desc = "获取系统所有角色成功！";
                entityBase.Result = true;
            }
            catch (Exception ex)
            {
                SetException("获取角色记录数出错,原因:", ex);
            }
            return entityBase;
        }
        #endregion

        #region 权限管理 Privileges
        /// <summary>
        /// 新增权限
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public EntityBase AddPrivileges(string loginId, PrivilegeEntity pEntity)
        {
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                return entityBase;
            }
            try
            {
                if (ComFunction.AddPrivileges(pEntity) > 0)
                {
                    entityBase.Result = true;
                    entityBase.Desc = "权限新增成功";
                }
                else
                {
                    entityBase.Result = false;
                    entityBase.Desc = "权限新增失败";
                }
            }
            catch (Exception ex)
            {
                entityBase.Result = false;
                entityBase.Desc = "权限新增失败,原因:" + ex.ToString();
                ManagerLog.WriteErr(ex);
            }
            return entityBase;
        }
       /// <summary>
        /// 修改权限数据
       /// </summary>
       /// <param name="loginId"></param>
       /// <param name="pEntity"></param>
       /// <returns></returns>
        public EntityBase UpdatePrivileges(string loginId, PrivilegeEntity pEntity)
        {
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                return entityBase;
            }
            try
            {
                if (ComFunction.UpdatePrivileges(pEntity) > 0)
                {
                    entityBase.Result = true;
                    entityBase.Desc = "权限修改成功";
                }
                else
                {
                    entityBase.Result = false;
                    entityBase.Desc = "权限修改失败";
                }
            }
            catch (Exception ex)
            {
                SetException("权限修改失败,原因:", ex);
            }
            return entityBase;
        }
        /// <summary>
        /// 读取权限
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="privilegeId"></param>
        /// <returns></returns>
        public PrivilegeEntity ReadPrivileges(string loginId, string privilegeId)
        {
            PrivilegeEntity pEntity = new PrivilegeEntity();
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                pEntity.Result = false;
                pEntity.Desc = ResCode.UL003Desc;
                return pEntity;
            }
            try
            {
                DataTable dt = ComFunction.ReadPrivileges(privilegeId);
                foreach (DataRow dr in dt.Rows)
                {
                    pEntity.PrivilegeId = dr["PrivilegeId"].ToString();
                    pEntity.PrivilegeName = dr["PrivilegeName"].ToString();
                    pEntity.PrivilegeCode = dr["PrivilegeCode"].ToString();
                    pEntity.ParentPrivilegeId = dr["ParentPrivilegeId"].ToString();
                    pEntity.ParentPrivilegeName = dr["ParentPrivilegeName"].ToString();
                    pEntity.PrivilegeType = (PrivilegeType)dr["PrivilegeType"];
                    pEntity.Library = dr["Library"].ToString();
                    pEntity.NameSpace = dr["NameSpace"].ToString();
                    pEntity.MenuPic = dr["MenuPic"].ToString();
                    pEntity.Displayorder = (int)dr["Displayorder"];
                    pEntity.Status = (Status)dr["Status"];
                    pEntity.Remark = dr["Remark"].ToString();
                }
                pEntity.Desc = "获取系统权限成功！";
                pEntity.Result = true;
            }
            catch (Exception ex)
            {
                pEntity.Result = false;
                pEntity.Desc = "获取权限记录出错,原因:" + ex.ToString();
                ManagerLog.WriteErr(ex);
            } return pEntity;
        }
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="privilegeId"></param>
        /// <returns></returns>
        public EntityBase DeletePrivileges(string loginId, string privilegeId)
        {
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                return entityBase;
            }
            try
            {
                if (ComFunction.DeletePrivileges(privilegeId) > 0)
                {
                    entityBase.Result = true;
                    entityBase.Desc = "权限删除成功";
                }
                else
                {
                    entityBase.Result = false;
                    entityBase.Desc = "权限删除失败";
                }
            }
            catch (Exception ex)
            {
                SetException("权限删除失败,原因:", ex);
            }
            return entityBase;
        }

        /// <summary>
        /// 获取子权限列表
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="privilegeId">父权限ID</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public EntityBase GetPrivilegeList(string loginId, string privilegeId, ref List<PrivilegeEntity> list)
        {
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                return entityBase;
            }
            try
            {
                DataTable dt = ComFunction.GetPrivilegeList(privilegeId);
                PrivilegeEntity pEntity = null;
                foreach (DataRow dr in dt.Rows)
                {
                    pEntity = new PrivilegeEntity();
                    pEntity.PrivilegeId = dr["PrivilegeId"].ToString();
                    pEntity.PrivilegeName = dr["PrivilegeName"].ToString();
                    pEntity.PrivilegeCode = dr["PrivilegeCode"].ToString();
                    pEntity.ParentPrivilegeId = dr["ParentPrivilegeId"].ToString();
                    pEntity.ParentPrivilegeName = dr["ParentPrivilegeName"].ToString();
                    pEntity.PrivilegeType = (PrivilegeType)dr["PrivilegeType"];
                    pEntity.Library = dr["Library"].ToString();
                    pEntity.NameSpace = dr["NameSpace"].ToString();
                    pEntity.MenuPic = dr["MenuPic"].ToString();
                    pEntity.Displayorder = (int)dr["Displayorder"];
                    pEntity.Status = (Status)dr["Status"];
                    pEntity.Remark = dr["Remark"].ToString();
                    list.Add(pEntity);
                }
                entityBase.Desc = "获取获取子权限成功！";
                entityBase.Result = true;
            }
            catch (Exception ex)
            {
                SetException("获取获取子权限记录数出错,原因:", ex);
            }
            return entityBase;
        }
        /// <summary>
        /// 获取父权限列表
        /// </summary>
        /// <returns></returns>
        public EntityBase GetPrivilegeParentLit(string loginId, ref List<PrivilegeEntity> list)
        {
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                return entityBase;
            }
            try
            {
                DataTable dt = ComFunction.GetPrivilegeParentLit();
                PrivilegeEntity pEntity = null;
                foreach (DataRow dr in dt.Rows)
                {
                    pEntity = new PrivilegeEntity();
                    pEntity.PrivilegeId = dr["PrivilegeId"].ToString();
                    pEntity.PrivilegeName = dr["PrivilegeName"].ToString();
                    pEntity.PrivilegeCode = dr["PrivilegeCode"].ToString();
                    pEntity.ParentPrivilegeId = dr["ParentPrivilegeId"].ToString();
                    pEntity.ParentPrivilegeName = dr["ParentPrivilegeName"].ToString();
                    pEntity.PrivilegeType = (PrivilegeType)dr["PrivilegeType"];
                    pEntity.Library = dr["Library"].ToString();
                    pEntity.NameSpace = dr["NameSpace"].ToString();
                    pEntity.MenuPic = dr["MenuPic"].ToString();
                    pEntity.Displayorder = (int)dr["Displayorder"];
                    pEntity.Status = (Status)dr["Status"];
                    pEntity.Remark = dr["Remark"].ToString();
                    list.Add(pEntity);
                }
                entityBase.Desc = "获取获取父权限列表成功！";
                entityBase.Result = true;
            }
            catch (Exception ex)
            {
                SetException("获取获取父权限列表记录数出错,原因:", ex);
                ManagerLog.WriteErr(ex);
            } return entityBase;
        }
       /// <summary>
        /// 获取用户顶部菜单
       /// </summary>
       /// <param name="loginId"></param>
       /// <param name="userId"></param>
       /// <param name="list"></param>
       /// <returns></returns>
        public EntityBase GetPrivilegeTopMenu(string loginId, string userId, ref List<PrivilegeEntity> list)
        {
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                return entityBase;
            }
            try
            {
                DataTable dt = ComFunction.GetPrivilegeTopMenu(userId);
                PrivilegeEntity pEntity = null;
                foreach (DataRow dr in dt.Rows)
                {
                    pEntity = new PrivilegeEntity();
                    pEntity.PrivilegeName = dr["PrivilegeName"].ToString();
                    pEntity.PrivilegeId = dr["PrivilegeId"].ToString();
                    pEntity.PrivilegeCode = dr["PrivilegeCode"].ToString();
                    pEntity.MenuPic = dr["MenuPic"].ToString();
                    list.Add(pEntity);
                }
                entityBase.Desc = "获取顶部菜单成功！";
                entityBase.Result = true;
            }
            catch (Exception ex)
            {
                SetException("获取顶部菜单出错,原因:", ex);
            } return entityBase;
        }
       /// <summary>
        /// leftMenu菜单
       /// </summary>
       /// <param name="loginId"></param>
       /// <param name="currentPrivilegID"></param>
       /// <param name="userid"></param>
       /// <param name="list"></param>
       /// <returns></returns>
        public EntityBase GetPrivilegesByParentID(string loginId, string currentPrivilegID, string userid, ref List<PrivilegeEntity> list)
        {
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                return entityBase;
            }
            try
            {
                DataTable dt = ComFunction.GetPrivilegesByParentID(currentPrivilegID, userid);
                PrivilegeEntity pEntity = null;
                foreach (DataRow dr in dt.Rows)
                {
                    pEntity = new PrivilegeEntity();
                    pEntity.PrivilegeName = dr["PrivilegeName"].ToString();
                    pEntity.PrivilegeId = dr["PrivilegeId"].ToString();
                    pEntity.PrivilegeCode = dr["PrivilegeCode"].ToString();
                    pEntity.MenuPic = dr["MenuPic"].ToString();
                    pEntity.Library = dr["Library"].ToString();
                    pEntity.NameSpace = dr["NameSpace"].ToString();
                    pEntity.MenuPic = dr["MenuPic"].ToString();
                    list.Add(pEntity);
                }
                entityBase.Desc = "获取系统所有角色成功！";
                entityBase.Result = true;
            }
            catch (Exception ex)
            {
                SetException("获取角色记录数出错,原因:", ex);
            }
            return entityBase;
        }
        /// <summary>
        /// 检查控件权限集合
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public EntityBase ValidataUserRole(string loginId, string userId, string type, ref List<PrivilegeEntity> list)
        {
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                return entityBase;
            }
            try
            {
                DataTable dt = ComFunction.ValidataUserRole(userId, type);
                PrivilegeEntity pEntity = null;
                foreach (DataRow dr in dt.Rows)
                {
                    pEntity = new PrivilegeEntity();
                    pEntity.PrivilegeName = dr["PrivilegeName"].ToString();
                    pEntity.PrivilegeId = dr["PrivilegeId"].ToString();
                    pEntity.PrivilegeCode = dr["PrivilegeCode"].ToString();
                    list.Add(pEntity);
                }
                entityBase.Desc = "获取系统所有角色成功！";
                entityBase.Result = true;
            }
            catch (Exception ex)
            {
                SetException("获取角色记录数出错,原因:", ex);
            }
            return entityBase;
        }

        /// <summary>
        /// 根据用户ID获取所有权限
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="userId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public EntityBase UserRolePrivileges(string loginId, string userId, ref List<PrivilegeEntity> list)
        {
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                return entityBase;
            }
            try
            {
                DataTable dt = ComFunction.UserRolePrivileges(userId);
                PrivilegeEntity pEntity = null;
                foreach (DataRow dr in dt.Rows)
                {
                    pEntity = new PrivilegeEntity();

                    pEntity.PrivilegeId = dr["PrivilegeId"].ToString();
                    pEntity.PrivilegeName = dr["PrivilegeName"].ToString();
                    pEntity.ParentPrivilegeId = dr["ParentPrivilegeId"].ToString();
                    pEntity.ParentPrivilegeName = dr["ParentPrivilegeName"].ToString();
                    list.Add(pEntity);
                }
                entityBase.Desc = "根据用户ID获取所有权限成功！";
                entityBase.Result = true;
            }
            catch (Exception ex)
            {
                SetException("根据用户ID获取所有权限记录出错,原因:", ex);
            }
            return entityBase;
        }
        /// <summary>
        /// 获取所以权限
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="list">权限集合</param>
        /// <returns>权限集合</returns>
        public EntityBase GetPrivileges(string loginId, ref List<PrivilegeEntity> list)
        {
            TradeUser reftradeuser = new TradeUser();
            if (ComFunction.ExistUserLoginID(loginId,ref reftradeuser) == false)
            {
                return entityBase;
            }
            try
            {
                DataTable dt = ComFunction.GetPrivileges();
                PrivilegeEntity pEntity = null;
                string PrivilegeName = string.Empty;
                foreach (DataRow dr in dt.Rows)
                {
                    PrivilegeName = dr["PrivilegeName"].ToString();
                    if (("手工报价" == PrivilegeName || "历史数据" == PrivilegeName || "汇率/水" == PrivilegeName)
                        && "ADMIN" != reftradeuser.Account.ToUpper() && "ROOT" != reftradeuser.Account.ToUpper())
                    { continue; }//只有ADMIN和ROOT账户才返回以上3个权限
                    pEntity = new PrivilegeEntity();
                    pEntity.PrivilegeId = dr["PrivilegeId"].ToString();
                    pEntity.PrivilegeName = dr["PrivilegeName"].ToString();
                    pEntity.ParentPrivilegeId = dr["ParentPrivilegeId"].ToString();
                    pEntity.ParentPrivilegeName = dr["ParentPrivilegeName"].ToString();
                    pEntity.Displayorder = (int)dr["Displayorder"];
                    list.Add(pEntity);
                }
                entityBase.Desc = "获取获取所以权限成功！";
                entityBase.Result = true;
            }
            catch (Exception ex)
            {
                SetException("获取获取所以权限记录数出错,原因:", ex);
            }
            return entityBase;
        }
        #endregion

        #region org
        /// <summary>
        /// 新增组织机构
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="orgEntity"></param>
        /// <returns></returns>
        public EntityBase AddOrg(string loginId, OrgEntity orgEntity)
        {
            TradeUser TdUser = new TradeUser();
            if (ComFunction.ExistUserLoginID(loginId, ref TdUser) == false)
            {
                return entityBase;
            }
            UserLogEntity userLogEntity = new UserLogEntity();
            userLogEntity.Account = TdUser.Account;
            userLogEntity.DESC = string.Format(@"后台新增组织机构;{0}", orgEntity.OrgName);
            userLogEntity.UserType = (int)TdUser.UType;
            //检测使用组织编码作为默认组织账户时，组织账户是否已经存在
            if(ComFunction.TradeAccountExist(orgEntity.TelePhone))
            {
                entityBase.Result = false;
                entityBase.Desc = string.Format("新增失败，已存在组织账户{0}！", orgEntity.TelePhone);
                return entityBase;
            }
            if (ComFunction.IsExitOrgName(orgEntity.OrgName, orgEntity.TelePhone) >= 1)
            {
                entityBase.Result = false;
                entityBase.Desc = "新增失败，存在相同的机构名称或组织编码！";
                return entityBase;
            }
	        if (string.IsNullOrEmpty(orgEntity.ParentOrgId))
	        {
		        orgEntity.ParentOrgId = "";
		        orgEntity.ParentOrgName = "";
	        }
	        YicelTransaction tran = new YicelTransaction();
            try
            {
                string ipmac = string.Empty;
                if (!string.IsNullOrEmpty(TdUser.Ip))
                {
                    ipmac += string.Format("IP={0},", TdUser.Ip);
                }
                if (!string.IsNullOrEmpty(TdUser.Mac))
                {
                    ipmac += string.Format("MAC={0},", TdUser.Mac);
                }
                tran.BeginTransaction();
                ComFunction.AddOrg(orgEntity, tran);
                ComFunction.CreateLogEx(userLogEntity,ipmac, tran);
                tran.Commit();
                entityBase.Result = true;
                entityBase.Desc = "组织机构新增成功";
            }
            catch (Exception ex)
            {
				tran.Rollback();
                entityBase.Result = false;
                entityBase.Desc = "组织机构新增失败";
                SetException("组织机构新增失败,原因:", ex);
            }
            return entityBase;
        }
       /// <summary>
        /// 修改组织机构数据
       /// </summary>
       /// <param name="loginId"></param>
       /// <param name="orgEntity"></param>
       /// <returns></returns>
        public EntityBase UpdateOrg(string loginId, OrgEntity orgEntity)
        {
            TradeUser TdUser = new TradeUser();
            if (ComFunction.ExistUserLoginID(loginId, ref TdUser) == false)
            {
                return entityBase;
            }
            UserLogEntity userLogEntity = new UserLogEntity();
            userLogEntity.Account = TdUser.Account;
            userLogEntity.DESC = string.Format(@"后台修改组织机构;{0}", orgEntity.OrgName);
            userLogEntity.UserType = (int)TdUser.UType;
            if (ComFunction.IsExitOrgName(orgEntity.OrgName, orgEntity.TelePhone,orgEntity.OrgID) >= 1)
            {
                entityBase.Result = false;
                entityBase.Desc = "修改失败，存在相同的机构名称或组织编码！";
                return entityBase;
            }
            if (ComFunction.IsExistHuWeiFuzi(orgEntity.OrgID, orgEntity.ParentOrgId)>=1)
            {
                entityBase.Result = false;
                entityBase.Desc = "修改失败，上级组织不能是该组织的下级组织！";
                return entityBase;
            }
            YicelTransaction tran = new YicelTransaction();
            try
            {
                string ipmac = string.Empty;
                if (!string.IsNullOrEmpty(TdUser.Ip))
                {
                    ipmac += string.Format("IP={0},", TdUser.Ip);
                }
                if (!string.IsNullOrEmpty(TdUser.Mac))
                {
                    ipmac += string.Format("MAC={0},", TdUser.Mac);
                }
                tran.BeginTransaction();
                ComFunction.UpdateOrg(orgEntity, tran);
                ComFunction.UpdateChileOrg(orgEntity, tran);
                ComFunction.CreateLogEx(userLogEntity,ipmac, tran);
                tran.Commit();
                entityBase.Result = true;
                entityBase.Desc = "组织机构修改成功";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                SetException("组织机构修改失败,原因:", ex);
            }
            return entityBase;
        }
        /// <summary>
        /// 读取组织机构
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public OrgEntity ReadOrg(string loginId, string orgId)
        {
            OrgEntity orgEntity = new OrgEntity();
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                orgEntity.Result = false;
                orgEntity.Desc = ResCode.UL003Desc;
                return orgEntity;
            }
            try
            {
                DataTable dt = ComFunction.ReadOrg(orgId);
                foreach (DataRow dr in dt.Rows)
                {
                    orgEntity.OrgID = dr["OrgID"].ToString();
                    orgEntity.OrgName = dr["OrgName"].ToString();
                    orgEntity.Coperson = dr["Coperson"].ToString();
                    orgEntity.CardType =(IDType) dr["CardType"];
                    orgEntity.CardNum = dr["CardNum"].ToString();
                    orgEntity.ParentOrgId = dr["ParentOrgID"].ToString();
                    orgEntity.Reperson = dr["Reperson"].ToString();
                    orgEntity.PhoneNum = dr["PhoneNum"].ToString();
                    orgEntity.TelePhone = dr["TelePhone"].ToString();
                    orgEntity.Email = dr["Email"].ToString();
                    orgEntity.Address = dr["Address"].ToString();
                    orgEntity.AddTime = dr["AddTime"].ToString();
                    orgEntity.Status = (Status)dr["Status"];
                }
                orgEntity.Desc = "获取系统权限成功！";
                orgEntity.Result = true;
            }
            catch (Exception ex)
            {
                orgEntity.Result = false;
                orgEntity.Desc = "获取组织机构记录出错,原因:" + ex.ToString();
                ManagerLog.WriteErr(ex);
            }
            return orgEntity;
        }
       /// <summary>
        /// 删除组织机构
       /// </summary>
       /// <param name="loginId"></param>
        /// <param name="orgId"></param>
       /// <param name="orgName"></param>
       /// <returns></returns>
        public EntityBase DeleteOrg(string loginId, string orgId, string orgName)
        {
            TradeUser TdUser = new TradeUser();
            if (ComFunction.ExistUserLoginID(loginId, ref TdUser) == false)
            {
                return entityBase;
            }
            UserLogEntity userLogEntity = new UserLogEntity();
            userLogEntity.Account = TdUser.Account;
            userLogEntity.DESC = string.Format(@"后台删除组织机构;{0}", orgName);
            userLogEntity.UserType = (int)TdUser.UType;
            YicelTransaction tran = new YicelTransaction();
            try
            {
                string ipmac = string.Empty;
                if (!string.IsNullOrEmpty(TdUser.Ip))
                {
                    ipmac += string.Format("IP={0},", TdUser.Ip);
                }
                if (!string.IsNullOrEmpty(TdUser.Mac))
                {
                    ipmac += string.Format("MAC={0},", TdUser.Mac);
                }
                tran.BeginTransaction();
                ComFunction.DeleteOrg(orgId, tran);
                ComFunction.CreateLogEx(userLogEntity,ipmac,tran);
                tran.Commit();
                entityBase.Result = true;
                entityBase.Desc = "组织机构删除成功";
            }
            catch (Exception ex)
            {
                SetException("组织机构删除失败,原因:", ex);
            }
            return entityBase;
        }
       /// <summary>
        /// 组织机构查询
       /// </summary>
       /// <param name="loginId"></param>
       /// <param name="orgEntity"></param>
       /// <param name="pageindex"></param>
       /// <param name="pagesize"></param>
       /// <param name="page"></param>
       /// <param name="list"></param>
       /// <returns></returns>
		public EntityBase GetOrgList(string loginId, OrgEntity orgEntity, int pageindex, int pagesize, ref int page, ref List<OrgEntity> list)
        {
            TradeUser TdUser = new TradeUser();
            if (ComFunction.ExistUserLoginID(loginId,ref TdUser) == false)
            {
                return entityBase;
            }
            try
            {
				DataTable dt = ComFunction.GetOrgList(orgEntity,TdUser, pageindex, pagesize, ref page);
                orgEntity = new OrgEntity();
                foreach (DataRow dr in dt.Rows)
                {
                    orgEntity = new OrgEntity();
                    orgEntity.OrgID = dr["OrgID"].ToString();
                    orgEntity.OrgName = dr["OrgName"].ToString();
                    orgEntity.Coperson = dr["Coperson"].ToString();
                    orgEntity.CardType = (IDType)dr["CardType"];
                    orgEntity.CardNum = dr["CardNum"].ToString();
                    orgEntity.ParentOrgId = dr["ParentOrgID"].ToString();
                    orgEntity.ParentOrgName = dr["ParentOrgName"].ToString();
                    orgEntity.Reperson = dr["Reperson"].ToString();
                    orgEntity.PhoneNum = dr["PhoneNum"].ToString();
                    orgEntity.TelePhone = dr["TelePhone"].ToString();
                    orgEntity.Email = dr["Email"].ToString();
                    orgEntity.Address = dr["Address"].ToString();
                    orgEntity.AddTime = dr["AddTime"].ToString();
                    orgEntity.Status = (Status)dr["Status"];
                    list.Add(orgEntity);
                }
                entityBase.Desc = "获取组织机构成功！";
                entityBase.Result = true;
            }
            catch (Exception ex)
            {
                SetException("获取组织机构记录数出错,原因:", ex);
            }
            return entityBase;
        }
		
		/// <summary>
		///  取得所有组织机构
		/// </summary>
		/// <param name="loginId"></param>
		/// <param name="list"></param>
		/// <returns></returns>
		public EntityBase GetBaseOrgListAll(string loginId, ref List<OrgEntity> list)
		{
            TradeUser TdUser = new TradeUser();
            if (ComFunction.ExistUserLoginID(loginId, ref TdUser) == false)
		    {
		        return entityBase;
		    }
		    try
		    {
				DataTable dt = ComFunction.GetBaseOrgListAll(TdUser);
		        OrgEntity orgEntity = null;
		        foreach (DataRow dr in dt.Rows)
		        {
					orgEntity = new OrgEntity();
                    orgEntity.CodeOrgName = dr["CodeOrgName"].ToString();
					orgEntity.OrgID = dr["OrgID"].ToString();
					orgEntity.OrgName = dr["OrgName"].ToString();
					orgEntity.Coperson = dr["Coperson"].ToString();
					orgEntity.CardType = (IDType)dr["CardType"];
					orgEntity.CardNum = dr["CardNum"].ToString();
					orgEntity.ParentOrgId = dr["ParentOrgID"].ToString();
					orgEntity.ParentOrgName = dr["ParentOrgName"].ToString();
					orgEntity.Reperson = dr["Reperson"].ToString();
					orgEntity.PhoneNum = dr["PhoneNum"].ToString();
					orgEntity.TelePhone = dr["TelePhone"].ToString();
					orgEntity.Email = dr["Email"].ToString();
					orgEntity.Address = dr["Address"].ToString();
					orgEntity.AddTime = dr["AddTime"].ToString();
					orgEntity.Status = (Status)dr["Status"];
					list.Add(orgEntity);
		        }
		        entityBase.Desc = "获取组织机构成功！";
		        entityBase.Result = true;
		    }
		    catch (Exception ex)
		    {
		        SetException("获取组织机构记录数出错,原因:", ex);
		    }
		    return entityBase;
		}    
	    #endregion

        #region Base_UserRole
        /// <summary>
        /// 新增用户角色
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="userRoleEntity"></param>
        /// <returns></returns>
        public EntityBase AddUserRole(string loginId, UserRoleEntity userRoleEntity)
        {
            TradeUser TdUser = new TradeUser();
            if (ComFunction.ExistUserLoginID(loginId, ref TdUser) == false)
            {
                return entityBase;
            }
            UserLogEntity userLogEntity = new UserLogEntity();
            userLogEntity.Account = TdUser.Account;
            userLogEntity.DESC = string.Format(@"后台新增用户角色;{0}", TdUser.Account);
            userLogEntity.UserType = (int)TdUser.UType;
            YicelTransaction tran = new YicelTransaction();
            try
            {
                string ipmac = string.Empty;
                if (!string.IsNullOrEmpty(TdUser.Ip))
                {
                    ipmac += string.Format("IP={0},", TdUser.Ip);
                }
                if (!string.IsNullOrEmpty(TdUser.Mac))
                {
                    ipmac += string.Format("MAC={0},", TdUser.Mac);
                }
                tran.BeginTransaction();
                ComFunction.AddUserRole(userRoleEntity, tran);
                ComFunction.CreateLogEx(userLogEntity,ipmac, tran);
                tran.Commit();
                entityBase.Result = true;
                entityBase.Desc = "用户角色新增成功";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                SetException("用户角色新增失败,原因:", ex);
            }
            return entityBase;
        }
       /// <summary>
        /// 修改用户角色数据
       /// </summary>
       /// <param name="loginId"></param>
       /// <param name="userRoleEntity"></param>
       /// <returns></returns>
        public EntityBase UpdateUserRole(string loginId, UserRoleEntity userRoleEntity)
        {
            TradeUser TdUser = new TradeUser();
            if (ComFunction.ExistUserLoginID(loginId, ref TdUser) == false)
            {
                return entityBase;
            }
            UserLogEntity userLogEntity = new UserLogEntity();
            userLogEntity.Account = TdUser.Account;
            userLogEntity.DESC = string.Format(@"后台修改用户角色;{0}", TdUser.Account);
            userLogEntity.UserType = (int)TdUser.UType;
            YicelTransaction tran = new YicelTransaction();
            try
            {
                string ipmac = string.Empty;
                if (!string.IsNullOrEmpty(TdUser.Ip))
                {
                    ipmac += string.Format("IP={0},", TdUser.Ip);
                }
                if (!string.IsNullOrEmpty(TdUser.Mac))
                {
                    ipmac += string.Format("MAC={0},", TdUser.Mac);
                }
                tran.BeginTransaction();
                ComFunction.UpdateUserRole(userRoleEntity, tran);
                ComFunction.CreateLogEx(userLogEntity,ipmac, tran);
                tran.Commit();
                entityBase.Result = true;
                entityBase.Desc = "用户角色修改成功";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                SetException("用户角色修改失败,原因:", ex);
            }
            return entityBase;
        }
        /// <summary>
        /// 读取用户角色
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public UserRoleEntity ReadUserRole(string loginId, string account)
        {
            UserRoleEntity userRoleEntity = new UserRoleEntity();
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                userRoleEntity.Result = false;
                userRoleEntity.Desc = ResCode.UL003Desc;
                return userRoleEntity;
            }
            try
            {
                DataTable dt = ComFunction.ReadUserRole(account);
                foreach (DataRow dr in dt.Rows)
                {
                    userRoleEntity.UserId = dr["UserID"].ToString();
                    userRoleEntity.RoleID = dr["RoleID"].ToString();
                }
                userRoleEntity.Desc = "获取用户角色成功！";
                userRoleEntity.Result = true;
            }
            catch (Exception ex)
            {
                userRoleEntity.Result = false;
                userRoleEntity.Desc = "获取用户角色记录出错,原因:" + ex.ToString();
                ManagerLog.WriteErr(ex);
            }
            return userRoleEntity;
        }
        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="account"></param>
        /// <returns></returns>
        public EntityBase DeleteUserRole(string loginId, string account)
        {
            TradeUser TdUser = new TradeUser();
            if (ComFunction.ExistUserLoginID(loginId, ref TdUser) == false)
            {
                return entityBase;
            }
            UserLogEntity userLogEntity = new UserLogEntity();
            userLogEntity.Account = TdUser.Account;
            userLogEntity.DESC = string.Format(@"后台删除用户角色;{0}", TdUser.Account);
            userLogEntity.UserType = (int)TdUser.UType;
            YicelTransaction tran = new YicelTransaction();
            try
            {
                string ipmac = string.Empty;
                if (!string.IsNullOrEmpty(TdUser.Ip))
                {
                    ipmac += string.Format("IP={0},", TdUser.Ip);
                }
                if (!string.IsNullOrEmpty(TdUser.Mac))
                {
                    ipmac += string.Format("MAC={0},", TdUser.Mac);
                }
                tran.BeginTransaction();
                ComFunction.DeleteUserRole(account, tran);
                ComFunction.CreateLogEx(userLogEntity,ipmac, tran);
                tran.Commit();
                entityBase.Result = true;
                entityBase.Desc = "用户角色删除成功";
            }
            catch (Exception ex)
            {
                SetException("用户角色删除失败,原因:", ex);
            }
            return entityBase;
        }
        #endregion

        #region Base_OrgUser
        /// <summary>
        /// 新增组织机构-用户帐号
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="orgUserEntity"></param>
        /// <returns></returns>
        public EntityBase AddOrgUser(string loginId, OrgUserEntity orgUserEntity)
        {
            TradeUser TdUser = new TradeUser();
            if (ComFunction.ExistUserLoginID(loginId, ref TdUser) == false)
            {
                return entityBase;
            }
            try
            {
                ComFunction.AddOrgUser(orgUserEntity);
                entityBase.Desc = "组织机构-用户帐号新增成功";
            }
            catch (Exception ex)
            {
                SetException("用户帐号新增失败,原因:", ex);
            }
            return entityBase;
        }
       /// <summary>
        /// 修改用组织机构-用户帐号
       /// </summary>
       /// <param name="loginId"></param>
       /// <param name="orgUserEntity"></param>
       /// <returns></returns>
        public EntityBase UpdateOrgUser(string loginId, OrgUserEntity orgUserEntity)
        {
            TradeUser TdUser = new TradeUser();
            if (ComFunction.ExistUserLoginID(loginId, ref TdUser) == false)
            {
                return entityBase;
            }
            try
            {
                ComFunction.UpdateOrgUser(orgUserEntity);
                entityBase.Result = true;
                entityBase.Desc = "组织机构-用户帐号修改成功";
            }
            catch (Exception ex)
            {
                SetException("组织机构-用户帐号修改失败,原因:", ex);
            }
            return entityBase;
        }
        /// <summary>
        /// 读取组织机构-用户帐号
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="account"></param>
        /// <returns></returns>
        public OrgUserEntity ReadOrgUser(string loginId, string account)
        {
            OrgUserEntity orgUserEntity = new OrgUserEntity();
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                orgUserEntity.Result = false;
                orgUserEntity.Desc = ResCode.UL003Desc;
                return orgUserEntity;
            }
            try
            {
                DataTable dt = ComFunction.ReadOrgUser(account);
                foreach (DataRow dr in dt.Rows)
                {
                    orgUserEntity.Account = dr["Account"].ToString();
                    orgUserEntity.OrgID = dr["OrgID"].ToString();
                    orgUserEntity.Status = (Status)dr["Status"];
                }
                orgUserEntity.Desc = "获取组织机构-用户帐号成功！";
                orgUserEntity.Result = true;

            }
            catch (Exception ex)
            {
                orgUserEntity.Result = false;
                orgUserEntity.Desc = "获取组织机构-用户帐号记录出错,原因:" + ex.ToString();
                ManagerLog.WriteErr(ex);
            }
            return orgUserEntity;
        }
        /// <summary>
        /// 删除组织机构-用户帐号
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="account"></param>
        /// <returns></returns>
        public EntityBase DeleteOrgUser(string loginId, string account)
        {
            TradeUser TdUser = new TradeUser();
            if (ComFunction.ExistUserLoginID(loginId, ref TdUser) == false)
            {
                return entityBase;
            }
            try
            {
                ComFunction.DeleteOrgUser(account);
                entityBase.Result = true;
                entityBase.Desc = "组织机构-用户帐号删除成功";
            }
            catch (Exception ex)
            {
                SetException("组织机构-用户帐号删除失败,原因:", ex);
            }
            return entityBase;
        }
        #endregion

        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public bool CheckUserLogin(string loginId)
        {
            if (ComFunction.ExistUserLoginID(loginId) == false)
            {
                return false;
            }
            return true;
        }
    }
}
