using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface
{
    /// <summary>
    /// 表字段结构
    /// </summary>
    public class Fields
    {
        /// <summary>
        ///交割单 Deliver_BZJ
        /// </summary>
        public const string Deliver_FIELD_LIST = "DeliverId,DeliverNo,Account,Goods,Direction,Total,DeliverDate,LockPrice,AvailableTotal,FromFlag,State,CreateDate,OperationUserID,UserID";
        /// <summary>
        /// 库存 Stock_BZJ
        /// </summary>
        public const string Stack_FIELD_LIST = "StockID,UserId,Au,Ag,Pt,Pd,Au_b,Ag_b,Pt_b,Pd_b,AuPrice,AgPrice,PtPrice,PdPrice,AuTotal,AgTotal,PtTotal,PdTotal,AuAmount,AgAmount,PtAmount,PdAmount";
        /// <summary>
        /// 用户绑定信息
        /// </summary>
        public const string UserBind_FIELD_LIST = "UserBindId,UserId,JUserId,IsEnable,CreateDate";
        /// <summary>
        /// 订单
        /// </summary>
        public const string Order_FIELD_LIST = "OrderId,OrderNo,OrderCode,OrderType,CarryWay,UserId,Account,JUserId,Au,Ag,Pt,Pd,CreateDate,EndDate,State,Version,AuQuantity,AgQuantity,PtQuantity,PdQuantity,AgP,PtP,PdP,AuP";

        /// <summary>
        /// 金生金订单操作记录
        /// </summary>
        public const string OrderOperation_FIELD_LIST = "OperationId,OrderId,OrderNo,Type,Account,OperationIP,OperationDate,Remark";
        /// <summary>
        /// 交割单记录
        /// </summary>
        public const string DeliverRecord_FIELD_LIST = "DeliverRecordId,DeliverId,DeliverNo,OrderId,OrderNo,OrderType,Goods,Direction,UseTotal,LockPrice,CreateDate";
        /// <summary>
        /// 金商库存
        /// </summary>
        public const string Agent_FIELD_LIST = "AgentDeliverId,AgentInfoId,FromTo,OrderId,Direction,Au,Ag,Pt,Pd,AvailableAu,AvailableAg,AvailablePt,AvailablePd,CreateDate,State";
        /// <summary>
        /// 订单回购价
        /// </summary>
        public const string OrderPrice_FIELD_LIST = "OrderPriceId,OrderId,OrderNo,PriceId,AuPrice,AgPrice,PtPrice,PdPrice";

        /// <summary>
        /// 日志记录
        /// </summary>
        public const string Log_FIELD_List = "OperTime,Account,UserType,Remark";
        /// <summary>
        /// 库存调整
        /// </summary>
        public const string DeliverAdjustment_List = "DeliverAdjustmentId ,DeliverAdjustmentNo ,Account ,Goods ,Direction ,Total ,DeliverDate ,LockPrice ,State ,AvailableTotal ,CreateDate,UserID";
        /// <summary>
        /// 组织机构
        /// </summary>
        public const string Org_FIELD_List = "OrgID,OrgName,Coperson,CardType,CardNum,ParentOrgId,ParentOrgName,Reperson,PhoneNum,TelePhone,Email,Address,AddTime,Status";
        /// <summary>
        /// 用户角色
        /// </summary>
        public const string UserRole_FIELD_List = "UserID,RoleID";
        /// <summary>
        /// 权限角色
        /// </summary>
        public const string RolePrivilege_FIELD_List = "RoleID,PrivilegeId";
    }


}
