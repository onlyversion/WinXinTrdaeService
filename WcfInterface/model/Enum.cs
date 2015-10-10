using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace WcfInterface.model
{
    /// <summary>
    /// 证件类型
    /// </summary>

    public enum IDType
    {
        /// <summary>
        /// 身份证号
        /// </summary>
        Identity= 1,
        /// <summary>
        /// 机构代码
        /// </summary>
        Company  = 2,
        /// <summary>
        /// 营业执照
        /// </summary>
        Business = 3
    }

    /// <summary>
    /// 物品类别
    /// </summary>

    public enum GoodsType
    {
        /// <summary>
        /// 黄金
        /// </summary>
        Au = 1,
        /// <summary>
        /// 白银
        /// </summary>
        Ag = 2,
        /// <summary>
        /// 铂金
        /// </summary>
        Pt = 3,
        /// <summary>
        /// 钯金
        /// </summary>
        Pd = 4
    }

    /// <summary>
    /// 交割单方向  买入库，实物受理，金商转库，金生金本金，金生金生金，员工提成，公司提成，大区提成
    /// </summary>    
    public enum Direction
    {
        /// <summary>
        /// 提货单
        /// </summary>
        提货单 = 1,
        /// <summary>
        /// 卖单
        /// </summary>
        卖单 = 2,
        //回购单 = 3,
        /// <summary>
        /// 金生金
        /// </summary>
        金生金 = 4,
        /// <summary>
        /// 到期单
        /// </summary>
        到期单 = 5,
        /// <summary>
        /// 已生金单
        /// </summary>
        已生金单 = 6,
        /// <summary>
        /// 提成单
        /// </summary>
        提成单 = 7,
        /// <summary>
        /// 金店库存同步
        /// </summary>
        金店库存同步 = 8,
        /// <summary>
        /// 
        /// </summary>
        实物受理 = 9,
        /// <summary>
        /// 实物受理
        /// </summary>
        金商转库 = 10,
        /// <summary>
        /// 公司提成
        /// </summary>
        公司提成 = 11,
        /// <summary>
        /// 大区提成
        /// </summary>
        大区提成 = 12,
        /// <summary>
        /// 库存调整
        /// </summary>
        库存调整 = 13
    }

    /// <summary>
    /// 订单类型
    /// </summary>  
    [DataContract]
    public enum OrderType
    {
        /// <summary>
        /// 提货单
        /// </summary>
        提货单 = 1,
        /// <summary>
        /// 卖单
        /// </summary>
        卖单 = 2,
        /// <summary>
        /// 回购单
        /// </summary>
        回购单 = 3,
        /// <summary>
        /// 金生金
        /// </summary>
        金生金 = 4,
        /// <summary>
        /// 到期订单
        /// </summary>
        到期订单 = 5,
        /// <summary>
        /// 已生金订单
        /// </summary>
        已生金订单 = 6,
        /// <summary>
        /// 提成订单
        /// </summary>
        提成订单 = 7,
        /// <summary>
        /// 库存调整
        /// </summary>
        库存调整 = 13
    }

    /// <summary>
    /// 订单状态
    /// </summary>

    public enum OrderState
    {
        /// <summary>
        /// 新订单
        /// </summary>
        新订单 = 1,
        /// <summary>
        /// 已完成
        /// </summary>
        已完成 = 2,
        /// <summary>
        /// 已取消
        /// </summary>
        已取消 = 3,
        /// <summary>
        /// 已过期
        /// </summary>
        已过期 = 4
    }

    /// <summary>
    /// 提货方式
    /// </summary>

    public enum CarryWay
    {
        /// <summary>
        /// 非提货卖
        /// </summary>
        非提货卖 = 0,
        /// <summary>
        /// 在线提货
        /// </summary>
        在线提货 = 1,
        /// <summary>
        /// 金店提货
        /// </summary>
        金店提货 = 2,
        /// <summary>
        /// 邮寄卖
        /// </summary>
        邮寄卖 = 3,
        /// <summary>
        /// 金店卖
        /// </summary>
        金店卖 = 4
    }

    /// <summary>
    /// 订单操作类型
    /// </summary>

    public enum OperationType
    {
        /// <summary>
        /// 确认提货
        /// </summary>
        确认提货 = 1,
        /// <summary>
        /// 确认卖
        /// </summary>
        确认卖 = 2,
        /// <summary>
        /// 确认回购
        /// </summary>
        确认回购 = 3,
        /// <summary>
        /// 用户创建金生金
        /// </summary>
        用户创建金生金 = 4,
        /// <summary>
        /// 用户创建回购单
        /// </summary>
        用户创建回购单 = 5,
        /// <summary>
        /// 管理员修改
        /// </summary>
        管理员修改 = 6,
        /// <summary>
        /// 用户取消订单
        /// </summary>
        用户取消订单 = 7,
        /// <summary>
        /// 过期订单
        /// </summary>
        过期订单 = 8,
        /// <summary>
        /// 用户确定金生金
        /// </summary>
        用户确定金生金 = 9,
        /// <summary>
        /// 用户到期订单
        /// </summary>
        用户到期订单 = 10,
        /// <summary>
        /// 用户已生金订单
        /// </summary>
        用户已生金订单 = 11,
        /// <summary>
        /// 员工提成订单
        /// </summary>
        员工提成订单 = 12
    }

    /// <summary>
    /// 禁用状态
    /// </summary>

    public enum Enable
    {
        /// <summary>
        /// 禁用
        /// </summary>
        禁用 = 0,
        /// <summary>
        /// 正常
        /// </summary>
        正常 = 1
    }
    /// <summary>
    /// 返回信息类别
    /// </summary>   
    public enum CodeType
    {
        /// <summary>
        /// 成功
        /// </summary>
        成功 = 1,
        /// <summary>
        /// 失败
        /// </summary>
        失败 = 2,
        /// <summary>
        /// 没要找到对应用户
        /// </summary>
        没要找到对应用户 = 3,
        /// <summary>
        /// 黄金数量不足
        /// </summary>
        黄金数量不足 = 4,
        /// <summary>
        /// 铂金数量不足
        /// </summary>
        铂金数量不足 = 5,
        /// <summary>
        /// 钯金数量不足
        /// </summary>
        钯金数量不足 = 6,
        /// <summary>
        /// 白银数量不足
        /// </summary>
        白银数量不足 = 7,
        /// <summary>
        /// 黄金交易失败
        /// </summary>
        黄金交易失败 = 8,
        /// <summary>
        /// 白银交易失败
        /// </summary>
        白银交易失败 = 9,
    }
    /// <summary>
    /// 日志内型 
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 用户库存调整减少
        /// </summary>
        用户库存调整减少 = 1,
        /// <summary>
        /// 创建回购订单
        /// </summary>
        创建回购订单 = 2,
        /// <summary>
        /// 金商回购单状态修改
        /// </summary>
        金商回购单状态修改 = 3,
        /// <summary>
        /// 订单修改
        /// </summary>
        订单修改 = 4,
    }
    /// <summary>
    /// 操作员角色
    /// </summary>
    public enum RoleType
    {
        /// <summary>
        /// 管理员
        /// </summary>
        Admin = 0,
        /// <summary>
        /// 金商
        /// </summary>
        Agent = 1,
        /// <summary>
        /// 店员
        /// </summary>
        Clerk = 2,
        /// <summary>
        /// 普通用户
        /// </summary>
        User = 3,
    }

    /// <summary>
    /// 权限类别
    /// </summary>
    public enum PrivilegeType
    {
        /// <summary>
        /// 模块
        /// </summary>
        Module = 0,//模块
        /// <summary>
        /// Page
        /// </summary>
        Page = 1,//页面
        /// <summary>
        /// 控件
        /// </summary>
        Control = 2,//控件
        /// <summary>
        /// 右键菜单权限
        /// </summary>
        Menu = 3 //右键菜单权限
    }
    /// <summary>
    /// 状态
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// 启用
        /// </summary>
        Enabled=1,//启用
        /// <summary>
        /// 禁用
        /// </summary>
        Disable=0//禁用
    }
}
