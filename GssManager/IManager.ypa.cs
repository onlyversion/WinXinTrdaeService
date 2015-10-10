using System.ServiceModel;
using System.Collections.Generic;
using System;
using WcfInterface.model;
using System.Data;
using System.Data.Common;

namespace GssManager
{
    /// <summary>
    /// 后台系统权限管理接口
    /// </summary>
    public partial interface IManager
    {
        #region 角色管理

        /// <summary>
        /// 向数据库插入角色权限关系记录
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="roleEntity"></param>
        /// <returns></returns>
        [OperationContract]
        EntityBase AddRole(string loginId, RoleEntity roleEntity);
        /// <summary>
        /// 修改角色数据
        /// </summary>
        /// <param name="roleEntity"></param>
        [OperationContract]
        EntityBase UpdateRole(string loginId, RoleEntity roleEntity);
        /// <summary>
        /// 读取角色
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        [OperationContract]
        RoleEntity ReadRole(string loginId, string roleID);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        [OperationContract]
        EntityBase DeleteRole(string loginId, string roleID);

        /// <summary>
        /// 系统角色权限设置
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="list"></param>
        /// <returns></returns>
        [OperationContract]
        EntityBase AddRolePrivileges(string loginId, List<RolePrivilegeEntity> list, string roldeID);

        /// <summary>
        /// 获取系统所有角色数据
        /// </summary>
        /// <returns>角色数据集</returns>
        [OperationContract]
        EntityBase GetRoles(string loginId, ref List<RoleEntity> list);
        /// <summary>
        /// 根据角色实体对象获取权限记录集
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns>权限记录集</returns>
        [OperationContract]
        EntityBase GetPrivilegesRole(string loginId, string roleID, ref List<PrivilegeEntity> list);
        /// <summary>
        /// 获取带有角色标记的所有权限记录集
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns>记录集</returns>
        [OperationContract]
        EntityBase GetPrivilegesWithRoleSign(string loginId, string roleID, ref List<PrivilegeEntity> list);

        #endregion

        #region 权限管理 Privileges
        /// <summary>
        /// 新增权限
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        [OperationContract]
        EntityBase AddPrivileges(string loginId, PrivilegeEntity pEntity);
        /// <summary>
        /// 修改权限数据
        /// </summary>
        /// <param name="pEntity"></param>
        [OperationContract]
        EntityBase UpdatePrivileges(string loginId, PrivilegeEntity pEntity);
        /// <summary>
        /// 读取权限
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="PrivilegeId"></param>
        /// <returns></returns>
        [OperationContract]
        PrivilegeEntity ReadPrivileges(string loginId, string PrivilegeId);
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        [OperationContract]
        EntityBase DeletePrivileges(string loginId, string PrivilegeId);

        /// <summary>
        /// 获取权限列表
        /// </summary>
        [OperationContract]
        EntityBase GetPrivilegeList(string loginId, string PrivilegeId, ref List<PrivilegeEntity> list);
        /// <summary>
        /// 获取父权限列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        EntityBase GetPrivilegeParentLit(string loginId, ref List<PrivilegeEntity> list);
        /// <summary>
        /// 获取顶部菜单
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        EntityBase GetPrivilegeTopMenu(string loginId, string userID, ref List<PrivilegeEntity> list);
        /// <summary>
        /// leftMenu菜单
        /// </summary>
        /// <param name="currentPrivilegID"></param>
        /// <returns></returns>
        [OperationContract]
        EntityBase GetPrivilegesByParentID(string loginId, string currentPrivilegID, string userid, ref List<PrivilegeEntity> list);
        /// <summary>
        /// 检查控件权限集合
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        EntityBase ValidataUserRole(string loginId, string userID, string type, ref List<PrivilegeEntity> list);
        /// <summary>
        /// 根据用户ID获取所有权限
        /// </summary>
        /// <param name="loginId">登录</param>
        /// <param name="userId">权限集合</param>
        /// <param name="list">权限集合</param>
        /// <returns></returns>
        [OperationContract]
        EntityBase UserRolePrivileges(string loginId, string userId, ref List<PrivilegeEntity> list);
        /// <summary>
        /// 获取所以权限
        /// </summary>
        /// <param name="loginId">登录</param>
        /// <param name="list"></param>
        /// <returns></returns>
        [OperationContract]
        EntityBase GetPrivileges(string loginId, ref List<PrivilegeEntity> list);
        #endregion

        #region 组织机构 Org
        /// <summary>
        /// 新增组织机构
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="orgEntity"></param>
        /// <returns></returns>
        [OperationContract]
        EntityBase AddOrg(string loginId, OrgEntity orgEntity);
        /// <summary>
        /// 修改组织机构数据
        /// </summary>
        /// <param name="orgEntity"></param>
        [OperationContract]
        EntityBase UpdateOrg(string loginId, OrgEntity orgEntity);
        /// <summary>
        /// 读取组织机构
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        [OperationContract]
        OrgEntity ReadOrg(string loginId, string orgID);
        /// <summary>
        /// 删除组织机构
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        [OperationContract]
        EntityBase DeleteOrg(string loginId, string orgID, string orgName);
        /// <summary>
        /// 组织机构查询
        /// </summary>
        /// <param name="orgEntity"></param>      
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        [OperationContract]
        EntityBase GetOrgList(string loginId, OrgEntity orgEntity, int pageindex, int pagesize, ref int page, ref List<OrgEntity> list);

	    /// <summary>
	    ///  取得所有组织机构
	    /// </summary>
	    /// <param name="loginId"></param>
	    /// <param name="list"></param>
	    /// <returns></returns>
		[OperationContract]
		EntityBase GetBaseOrgListAll(string loginId, ref List<OrgEntity> list);

        #endregion

        #region Base_UserRole
        /// <summary>
        /// 新增用户角色
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="userRoleEntity"></param>
        /// <returns></returns>
        [OperationContract]
        EntityBase AddUserRole(string loginId, UserRoleEntity userRoleEntity);
        /// <summary>
        /// 修改用户角色
        /// </summary>
        /// <param name="roleEntity"></param>
        [OperationContract]
        EntityBase UpdateUserRole(string loginId, UserRoleEntity userRoleEntity);
        /// <summary>
        /// 读取用户角色
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        UserRoleEntity ReadUserRole(string loginId, string userID);
        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        EntityBase DeleteUserRole(string loginId, string userID);
        #endregion

        #region Base_OrgUser

        /// <summary>
        /// 新增组织机构-用户帐号
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="orgUserEntity"></param>
        /// <returns></returns>
        [OperationContract]
        EntityBase AddOrgUser(string loginId, OrgUserEntity orgUserEntity);
        /// <summary>
        /// 修改组织机构-用户帐号
        /// </summary>
        /// <param name="orgUserEntity"></param>
        [OperationContract]
        EntityBase UpdateOrgUser(string loginId, OrgUserEntity orgUserEntity);
        /// <summary>
        /// 读取组织机构-用户帐号
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="account"></param>
        /// <returns></returns>
        [OperationContract]
        OrgUserEntity ReadOrgUser(string loginId, string account);
        /// <summary>
        /// 删除组织机构-用户帐号
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="Account"></param>
        /// <returns></returns>
        [OperationContract]
        EntityBase DeleteOrgUser(string loginId, string Account);

        #endregion
    }
}
