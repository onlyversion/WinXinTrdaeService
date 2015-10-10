using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WcfInterface.model;
using JinTong.Jyrj.Data;
using System.Data.Common;
using System.Linq;
using JinTong.Jyrj.Common;
namespace WcfInterface
{
    /// <summary>
    /// bzj
    /// </summary>
    public partial class ComFunction
    {
        #region 用户订单
        /// <summary>
        /// 用户个人库存信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static StockEntity GetUserStockInfo(string userId)
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@UserID", userId) };
            DataTable dt = DbHelper.RunProcedure("P_UserStockInfo", paras, "Stock_BZJ").Tables[0];
            if (dt.Rows.Count > 0)
            {
                StockEntity stockEntity = CreateEntity(dt);
                return stockEntity;
            }
            StockEntity sEntity = new StockEntity();
            sEntity.Ag = 0;
            sEntity.Au = 0;
            sEntity.Pd = 0;
            sEntity.Pt = 0;
            sEntity.AuPrice = 0;
            sEntity.AgPrice = 0;
            sEntity.PdPrice = 0;
            sEntity.PtPrice = 0;
            return sEntity;
        }
        /// <summary>
        /// 后台用户库存查询
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static StockEntity GetAdminUserStockInfo(string userId)
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@UserID", userId) };
            DataTable dt = DbHelper.RunProcedure("P_UserAdminStockInfo", paras, "Stock_BZJ").Tables[0];
            if (dt.Rows.Count > 0)
            {
                StockEntity stockEntity = CreateEntity(dt);
                return stockEntity;
            }
            StockEntity sEntity = new StockEntity();
            sEntity.Ag = 0;
            sEntity.Au = 0;
            sEntity.Pd = 0;
            sEntity.Pt = 0;
            sEntity.AuPrice = 0;
            sEntity.AgPrice = 0;
            sEntity.PdPrice = 0;
            sEntity.PtPrice = 0;
            return sEntity;
        }
        /// <summary>
        /// 用户库存查询
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static StockEntity GetAccountStockInfo(string account)
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@Account", account) };
            DataTable dt = DbHelper.RunProcedure("P_AccountStockInfo", paras, "Stock_BZJ").Tables[0];
            if (dt.Rows.Count > 0)
            {
                StockEntity stockEntity = CreateEntity(dt);
                return stockEntity;
            }
            return null;
        }
        /// <summary>
        /// 创建个人库存实体
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static StockEntity CreateEntity(DataTable dt)
        {
            StockEntity entity = new StockEntity();
            entity.StockID = dt.Rows[0]["StockID"].ToString();
            entity.UserId = dt.Rows[0]["UserId"].ToString();
            entity.Au = Convert.ToDecimal(dt.Rows[0]["Au"]);
            entity.Ag = Convert.ToDecimal(dt.Rows[0]["Ag"]);
            entity.Pt = Convert.ToDecimal(dt.Rows[0]["Pt"]);
            entity.Pd = Convert.ToDecimal(dt.Rows[0]["Pd"]);
            entity.Au_b = Convert.ToDecimal(dt.Rows[0]["Au_b"]);
            entity.Ag_b = Convert.ToDecimal(dt.Rows[0]["Ag_b"]);
            entity.Pt_b = Convert.ToDecimal(dt.Rows[0]["Pt_b"]);
            entity.Pd_b = Convert.ToDecimal(dt.Rows[0]["Pd_b"]);
            entity.AuTotal = Convert.ToDecimal(dt.Rows[0]["AuTotal"]);
            entity.AgTotal = Convert.ToDecimal(dt.Rows[0]["AgTotal"]);
            entity.PtTotal = Convert.ToDecimal(dt.Rows[0]["PtTotal"]);
            entity.PdTotal = Convert.ToDecimal(dt.Rows[0]["PdTotal"]);
            entity.AuPrice = Convert.ToDecimal(dt.Rows[0]["AuPrice"]);
            entity.AgPrice = Convert.ToDecimal(dt.Rows[0]["AgPrice"]);
            entity.PtPrice = Convert.ToDecimal(dt.Rows[0]["PtPrice"]);
            entity.PdPrice = Convert.ToDecimal(dt.Rows[0]["PdPrice"]);
            entity.AuAmount = Convert.ToDecimal(dt.Rows[0]["AuAmount"]);
            entity.AgAmount = Convert.ToDecimal(dt.Rows[0]["AgAmount"]);
            entity.PtAmount = Convert.ToDecimal(dt.Rows[0]["PtAmount"]);
            entity.PdAmount = Convert.ToDecimal(dt.Rows[0]["PdAmount"]);
            return entity;
        }

        /// <summary>
        /// 根据用户ID获取订单列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static DataTable GetOrderList(string userId)
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@UserID", userId) };
            DataTable dt = DbHelper.RunProcedure("P_UserOrder", paras, "order").Tables[0];
            return dt;
        }


        /// <summary>
        /// 提货单，回购单、金生金单查询
        /// </summary>
        /// <param name="order"></param>
        /// <param name="agentId"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static DataTable GetOrderList(OrderEntity order, string agentId, int pageindex, int pagesize, ref int page)
        {
            SqlParameter[] paras = null;
            StringBuilder sb = new StringBuilder();
            StringBuilder sbSearch = new StringBuilder();


            if (!string.IsNullOrEmpty(order.OrderCode))
            {
                sbSearch.AppendFormat(@" and OrderCode='{0}'", order.OrderCode);
            }

            if (!string.IsNullOrEmpty(order.OrderCode))
            {
                sbSearch.AppendFormat(@" and OrderNo='{0}'", order.OrderNo);
            }

            if (!string.IsNullOrEmpty(order.Account))
            {
                sbSearch.AppendFormat(@" and Account='{0}'", order.Account);
            }
            if (!string.IsNullOrEmpty(order.Name))
            {
                sbSearch.AppendFormat(@" and userName like '{0}%'", order.Name);
            }
            if (!string.IsNullOrEmpty(order.CreateDate))
            {
                sbSearch.AppendFormat(@" and CreateDate BETWEEN '{0}' AND '{1}'", order.CreateDate, order.EndDate);
            }

            if (!string.IsNullOrEmpty(agentId))
            {
                sbSearch.AppendFormat(@" and agentid in ({0}) ", ComFunction.GetOrgIds(agentId));
            }


            if (order.OrderType == 0) // 交割单
            {
                paras = new SqlParameter[] 
            {
                new SqlParameter("@selectlist", "  Account,userName,DeliverId,DeliverDate,DeliverNo,Goods,Direction,Total,LockPrice,State,AvailableTotal,FromFlag,CreateDate"),
                new SqlParameter("@SubSelectList", " Account,userName,DeliverId,DeliverDate,DeliverNo,Goods,Direction,Total,LockPrice,State,AvailableTotal,FromFlag,CreateDate"),
                new SqlParameter("@TableSource", "V_OrderDelivery"),
                new SqlParameter("@TableOrder", "a"),
                new SqlParameter("@SearchCondition", string.Format(@" OrderType=1 {0}",sbSearch.ToString())),
                new SqlParameter("@OrderExpression", "order by CreateDate desc"),             
                new SqlParameter("@PageIndex", pageindex),
                new SqlParameter("@PageSize", pagesize),
                new SqlParameter("@PageCount", page)
            };
            }
            if (order.OrderType == 1) // 提货单
            {
                paras = new SqlParameter[] 
            {
                new SqlParameter("@selectlist", "  Account, userName, OrderNo, OrderCode, CarryWay, Au, Ag, Pt, Pd, AgQuantity, AuQuantity, PtQuantity, PdQuantity, AuP, AgP, PtP, PdP, CreateDate, State, OrderType,OperationDate,AgentID,ClerkID,tradeAccount"),
                new SqlParameter("@SubSelectList", " Account, userName, OrderNo, OrderCode, CarryWay, Au, Ag, Pt, Pd, AgQuantity, AuQuantity, PtQuantity, PdQuantity, AuP, AgP, PtP, PdP, CreateDate, State, OrderType,OperationDate,AgentID,ClerkID,tradeAccount"),
                new SqlParameter("@TableSource", "V_OrderTake"),
                new SqlParameter("@TableOrder", "a"),
                new SqlParameter("@SearchCondition", string.Format(@" OrderType=1 {0}",sbSearch.ToString())),
                new SqlParameter("@OrderExpression", "order by CreateDate desc"),             
                new SqlParameter("@PageIndex", pageindex),
                new SqlParameter("@PageSize", pagesize),
                new SqlParameter("@PageCount", page)
            };
            }
            if (order.OrderType == 3) //回购
            {
                paras = new SqlParameter[] 
            {
                new SqlParameter("@selectlist", "  OperationDate ,Account, userName, OrderNo, OrderCode, CarryWay, Au, Ag, Pt, Pd,AuQuantity, AgQuantity, PtQuantity, PdQuantity, AuPrice, AgPrice,PtPrice, PdPrice, CreateDate, State,OrderType,EndDate,Aup,agp,ptp,pdp,ClerkID"),
                new SqlParameter("@SubSelectList", " OperationDate ,Account, userName, OrderNo, OrderCode, CarryWay, Au, Ag, Pt, Pd,AuQuantity, AgQuantity, PtQuantity, PdQuantity, AuPrice, AgPrice,PtPrice, PdPrice, CreateDate, State,OrderType,EndDate,Aup,agp,ptp,pdp,ClerkID"),
                new SqlParameter("@TableSource", "V_BzckOrder"),
                new SqlParameter("@TableOrder", "a"),
                new SqlParameter("@SearchCondition", string.Format(@" OrderType=3 {0}",sbSearch.ToString())),
                new SqlParameter("@OrderExpression", "order by CreateDate desc"),             
                new SqlParameter("@PageIndex", pageindex),
                new SqlParameter("@PageSize", pagesize),
                new SqlParameter("@PageCount", page)
            };
            }
            if (order.OrderType == 4) //金生金
            {
                paras = new SqlParameter[] 
            {
                new SqlParameter("@selectlist", "  tradeAccount,userName,OrderNo,AuP,AgP,Au,Ag,CreateDate,State,OrderType"),
                new SqlParameter("@SubSelectList", " tradeAccount,userName,OrderNo,AuP,AgP,Au,Ag,CreateDate,State,OrderType"),
                new SqlParameter("@TableSource", "V_JsjOrder"),
                new SqlParameter("@TableOrder", "a"),
                new SqlParameter("@SearchCondition", string.Format(@" OrderType=4 {0}",sbSearch.ToString())),
                new SqlParameter("@OrderExpression", "order by CreateDate desc"),             
                new SqlParameter("@PageIndex", pageindex),
                new SqlParameter("@PageSize", pagesize),
                new SqlParameter("@PageCount", page)
            };
            }
            paras[8].Direction = ParameterDirection.Output;
            DataTable dt = DbHelper.RunProcedure("GetRecordFromPage", paras, "Deliver_BZJ").Tables[0];
            page = Convert.ToInt32(paras[8].Value);
            return dt;
        }


        /// <summary>
        /// 根据用户ID获取订单数量
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static DataTable GetOrderTotal(string userId)
        {
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@UserID", userId) };
            return DbHelper.RunProcedure("P_OrderTotal", paras, "bzj_order").Tables[0];
        }
        /// <summary>
        /// 根据用户ID获取库存信息
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public static StockEntity LoadByAccount(string userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select top 1 {0} from Stock_BZJ where userid='{1}' ", Fields.Stack_FIELD_LIST, userid);
            DataTable dt = DbHelper.ExecQuery(strSql.ToString());
            if (dt.Rows.Count > 0)
            {
                return CreateEntity(dt);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 客户管理库存查询
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="userID"></param>
        /// <param name="deliverType"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static DataTable GetDUserStockList(string loginId, string userID, int deliverType, int pageindex, int pagesize, ref int page)
        {
            SqlParameter[] paras = null;
            StringBuilder sb = new StringBuilder();
            if (deliverType == 1)
            {
                paras = new SqlParameter[] 
            { 
                 new SqlParameter("@selectlist", "a.DeliverId,a.DeliverNo,a.Goods,a.Direction,a.Total,a.LockPrice,a.AvailableTotal,CreateDate"),
            new SqlParameter("@SubSelectList", "DeliverId,DeliverNo,Goods,Direction,Total,LockPrice,AvailableTotal,CreateDate"),
              new SqlParameter("@TableSource", "Deliver_BZJ"),
            new SqlParameter("@TableOrder", "a"),
              new SqlParameter("@SearchCondition", string.Format(@" a.UserId='{0}'",userID)),
            new SqlParameter("@OrderExpression", " order by CreateDate desc"),             
            new SqlParameter("@PageIndex", pageindex),
              new SqlParameter("@PageSize", pagesize),
            new SqlParameter("@PageCount", page)
              };
            }
            else
            {
                paras = new SqlParameter[] 
            { 
                 new SqlParameter("@selectlist", "a.DeliverId,a.DeliverRecordId,a.DeliverNo,a.OrderNo,a.Goods,a.Direction,a.UseTotal,a.LockPrice,a.CreateDate"),
            new SqlParameter("@SubSelectList", "DeliverId,DeliverRecordId,DeliverNo,OrderNo,Goods,Direction,UseTotal,LockPrice,CreateDate"),
              new SqlParameter("@TableSource", "V_DeliverRecord"),
            new SqlParameter("@TableOrder", "a"),
              new SqlParameter("@SearchCondition", string.Format(@" b.UserId='{0}'",userID)),
            new SqlParameter("@OrderExpression", "order by CreateDate desc"),             
            new SqlParameter("@PageIndex", pageindex),
              new SqlParameter("@PageSize", pagesize),
            new SqlParameter("@PageCount", page)
              };

            }
            paras[8].Direction = ParameterDirection.Output;
            DataTable dt = DbHelper.RunProcedure("GetRecordFromPage", paras, "Order").Tables[0];
            page = Convert.ToInt32(paras[8].Value);
            return dt;
        }

        /// <summary>
        ///  金商用户绑定保证金
        /// </summary>
        /// <param name="account"></param>
        /// <param name="agentid"></param>
        /// <param name="cardNum"></param>
        /// <returns></returns>

        public static int AgentBind(string account, string agentid, string cardNum)
        {
            int result;
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@Account", account),
                new SqlParameter("@AgentID", agentid),             
                new SqlParameter("@CardNum", cardNum),
                new SqlParameter("@Result", SqlDbType.Int)
            };
            return DbHelper.RunProcedure("P_AgentUserBind", paras, out result);
        }

        /// <summary>
        /// 根据用户账号获取订单列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<OrderEntity> GetListById(string id)
        {
            List<OrderEntity> list = new List<OrderEntity>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Order_BZJ ");
            strSql.Append("where UserId=@UserId  order by  CreateDate desc,OrderType desc,OrderCode desc");
            Dictionary<string, object> parms = new Dictionary<string, object>();
            parms.Add("@UserId", id);
            using (DbDataReader reader = DbHelper.ExecuteReader(strSql.ToString(), parms))
            {
                while (reader.Read())
                {
                    OrderEntity entity = CreateEntity(reader);
                    list.Add(entity);
                }
                reader.Close();
                reader.Dispose();
            }
            return list;
        }
        /// <summary>
        /// 根据订单号加载单据，个人，库存信息
        /// </summary>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        public static OrderEntity GetOrderNOInfo(string orderCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append("from V_OrderInfo ");
            strSql.AppendFormat("where orderCode='{0}'  ", orderCode);

            DataTable dt = DbHelper.ExecQuery(strSql.ToString());
            OrderEntity entity = new OrderEntity();
            if (dt.Rows.Count > 0)
            {
                entity.OrderId = dt.Rows[0]["OrderId"].ToString();
                entity.UserId = dt.Rows[0]["UserId"].ToString();
                entity.Au = Convert.ToDecimal(dt.Rows[0]["Au"]);
                entity.Ag = Convert.ToDecimal(dt.Rows[0]["Ag"]);
                entity.AuT = Convert.ToDecimal(dt.Rows[0]["AuTotal"]);
                entity.AgT = Convert.ToDecimal(dt.Rows[0]["AgTotal"]);

                entity.Pt = Convert.ToDecimal(dt.Rows[0]["Pt"]);
                entity.Pd = Convert.ToDecimal(dt.Rows[0]["Pd"]);
                entity.PtT = Convert.ToDecimal(dt.Rows[0]["PtTotal"]);
                entity.PdT = Convert.ToDecimal(dt.Rows[0]["PdTotal"]);

                entity.AgP = Convert.ToDecimal(dt.Rows[0]["AgPrice"]);
                entity.AuP = Convert.ToDecimal(dt.Rows[0]["AuPrice"]);
                entity.PtP = Convert.ToDecimal(dt.Rows[0]["PtPrice"]);
                entity.PdP = Convert.ToDecimal(dt.Rows[0]["PdPrice"]);

                entity.AgAmount = Convert.ToDecimal(dt.Rows[0]["AgAmount"]);
                entity.AuAmount = Convert.ToDecimal(dt.Rows[0]["AuAmount"]);
                entity.PtAmount = Convert.ToDecimal(dt.Rows[0]["PtAmount"]);
                entity.PdAmount = Convert.ToDecimal(dt.Rows[0]["PdAmount"]);





                entity.Name = dt.Rows[0]["userName"].ToString();
                entity.CardType = Convert.ToInt32(dt.Rows[0]["CardType"]);
                entity.CardNum = dt.Rows[0]["CardNum"].ToString();
                entity.PhoneNum = dt.Rows[0]["PhoneNum"].ToString();
                entity.OrderNo = dt.Rows[0]["OrderNo"].ToString();
                entity.State = Convert.ToInt32(dt.Rows[0]["State"]);
                entity.OrderType = Convert.ToInt32(dt.Rows[0]["OrderType"]);
                entity.IDType = Convert.ToInt32(dt.Rows[0]["IDType"]);
                entity.IDNo = dt.Rows[0]["IDNo"].ToString();
                entity.Phone = dt.Rows[0]["Phone"].ToString();
                entity.Account = dt.Rows[0]["tradeAccount"].ToString();
                return entity;
            }
            return entity;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static DataTable GetUserInfo(string userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select  * from Trade_User where userid='{0}' and state=1 ", userId);
            return DbHelper.ExecQuery(strSql.ToString());
        }
        #endregion
        #region 金生金
        /// <summary>
        /// 新建金生金用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int CreateGssUser(UserBindEntity entity)
        {
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@UserID", entity.UserId),
                new SqlParameter("@UserBindId", entity.UserBindId),
                new SqlParameter("@JUserId", entity.JUserId),
                new SqlParameter("@Result", SqlDbType.Int)
            };
            int result = 0;
            paras[3].Direction = ParameterDirection.Output;
            return DbHelper.RunProcedure("P_CreateGssUser", paras, out result);

        }

        /// <summary>
        /// 新建金生金用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int CreateAdminGssUser(UserBindEntity entity)
        {
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@UserID", entity.UserId),
                new SqlParameter("@UserBindId", entity.UserBindId),
                new SqlParameter("@JUserId", entity.JUserId),               
                new SqlParameter("@Result", SqlDbType.Int)
            };
            paras[3].Direction = ParameterDirection.Output;
            int result = 0;
            return DbHelper.RunProcedure("P_CreateAdminGssUser", paras, out result);

        }
        /// <summary>
        /// 修改金生金用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int UpdateGssUser(UserBindEntity entity)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();
            parms.Add("@UserBindId", entity.UserBindId);
            parms.Add("@UserId", entity.UserId);
            parms.Add("@JUserId", entity.JUserId);
            parms.Add("@IsEnable", entity.IsEnable);
            parms.Add("@CreateDate", entity.CreateDate);
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update UserBind_BZJ set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("JUserId=@JUserId,");
            strSql.Append("IsEnable=@IsEnable,");
            strSql.Append("CreateDate=@CreateDate");
            strSql.AppendFormat(" where UserBindId=@UserBindId");
            return DbHelper.ExecuteNonQuery(strSql.ToString(), parms);
        }

        /// <summary>
        /// 删除金生金用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static int DelteGssUser(string userId)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();
            string values = string.Empty;
            string[] arr = userId.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                parms.Add(string.Format("@FIELD_PK_{0}", i), arr[i]);
                values += string.Format("@FIELD_PK_{0},", i);
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserBind_BZJ ");
            strSql.AppendFormat(" where UserBindId in ({0}) ", values.TrimEnd(','));
            object obj = DbHelper.ExecuteNonQuery(strSql.ToString(), parms);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 加载金生金用户信息
        /// </summary>
        /// <param name="userBindId"></param>
        /// <returns></returns>
        public static UserBindEntity ReadGssUser(string userBindId)
        {
            string strSql = string.Format("SELECT * FROM dbo.UserBind_BZJ WHERE UserBindId='{0}'", userBindId);
            DataTable dt = DbHelper.ExecQuery(strSql);
            if (dt.Rows.Count > 0)
            {
                UserBindEntity userBindEntity = CreateUserBindEntity(dt);
                return userBindEntity;
            }
            return null;
        }
        private static UserBindEntity CreateUserBindEntity(DataTable dt)
        {
            UserBindEntity entity = new UserBindEntity();
            entity.UserBindId = dt.Rows[0]["UserBindId"].ToString();
            entity.UserId = dt.Rows[0]["UserId"].ToString();
            entity.JUserId = dt.Rows[0]["JUserId"].ToString();
            entity.IsEnable = Convert.ToInt32(dt.Rows[0]["IsEnable"]);
            entity.CreateDate = dt.Rows[0]["CreateDate"].ToString();
            return entity;
        }

        #region 金生金办理
        /// <summary>
        /// 金生金办理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationEntity"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public static int CreateJSJOrder(OrderEntity entity, OrderOperationEntity operationEntity, string loginId)
        {
            List<DeliverEntity> deliverList = new List<DeliverEntity>();//需要更新的交割单列表
            List<DeliverRecordEntity> deliverRecordList = new List<DeliverRecordEntity>();//交割单记录列表
            #region 赋值

            entity.OrderId = Guid.NewGuid().ToString("n");
            entity.OrderNo = "JSJ" + DateTime.Now.ToString("yyMMddhhmmssffff");
            entity.OrderCode = "";
            entity.OrderType = (int)OrderType.金生金;
            entity.State = (int)OrderState.新订单;
            entity.CreateDate = DateTime.Now.ToString();
            entity.EndDate = DateTime.Now.ToString();

            operationEntity.OperationId = Guid.NewGuid().ToString("n");
            operationEntity.OperationDate = DateTime.Now.ToString();
            operationEntity.Account = entity.Account;
            operationEntity.OperationId = Guid.NewGuid().ToString("n");
            operationEntity.OrderId = entity.OrderId;
            operationEntity.OrderNo = entity.OrderNo;
            operationEntity.Type = 5;
            operationEntity.Remark = string.Format("用户:{0} 创建金生金订单", entity.Account);

            #endregion

            List<DeliverEntity> list = GetListByAccount(entity.Account, Direction.提货单, 1);


            #region 计算交割单信息

            Dictionary<GoodsType, decimal> dic = new Dictionary<GoodsType, decimal>();
            dic.Add(GoodsType.Au, entity.Au);
            dic.Add(GoodsType.Ag, entity.Ag);
            dic.Add(GoodsType.Pd, entity.Pd);
            dic.Add(GoodsType.Pt, entity.Pt);
            foreach (var key in dic.Keys)
            {
                var tmp = list.Where(m => m.Goods.Equals((int)key)).ToList();
                var values = dic[key];
                if (values <= 0) continue;
                foreach (var mm in tmp)
                {
                    DeliverRecordEntity recordEntity = new DeliverRecordEntity
                    {
                        DeliverId = mm.DeliverId,
                        DeliverNo = mm.DeliverNo,
                        Goods = mm.Goods,
                        Direction = mm.Direction,
                        LockPrice = mm.LockPrice,
                        OrderType = (int)OrderType.金生金,
                        OrderId = entity.OrderId,
                        OrderNo = entity.OrderNo
                    };
                    if (values > mm.AvailableTotal)
                    {
                        recordEntity.UseTotal = mm.AvailableTotal;
                        values -= mm.AvailableTotal;
                        mm.AvailableTotal = 0;
                        mm.State = 0;
                        deliverList.Add(mm);
                        deliverRecordList.Add(recordEntity);
                        break;
                    }
                    else
                    {
                        mm.State = values.Equals(mm.AvailableTotal) ? 0 : 1;
                        recordEntity.UseTotal = values;
                        mm.AvailableTotal -= values;
                        deliverList.Add(mm);
                        deliverRecordList.Add(recordEntity);
                        break;
                    }
                }
            }

            #endregion
            StockEntity userEntity = new StockEntity();
            userEntity = LoadByAccount(entity.UserId);

            entity.AuP = userEntity.AuPrice;
            entity.AgP = userEntity.AgPrice;
            UserLogEntity userLogEntity = new UserLogEntity();

            userLogEntity.Account = entity.UserId;
            userLogEntity.DESC = string.Format(@"用户{0}金生金办理：Ag:{1},Au:{2};订单号：{3}", entity.Account, entity.Au, entity.Ag, entity.OrderNo);
            userLogEntity.UserType = (int)LogType.用户库存调整减少;
            YicelTransaction tran = new YicelTransaction();
            try
            {
                tran.BeginTransaction();
                CreateOrder(entity, tran);//创建订单信息                
                CreateOrderOperation(operationEntity, tran);//创建金生金订单操作记录
                deliverRecordList.ForEach(m => CreateDeliverRecord(m, tran));//插入交割单记录信息
                deliverList.ForEach(m => UpdateDeliver(m, entity, operationEntity.OperationId, tran));//更新交割单信息
                UpdateUserTotal(entity, userEntity, tran);//更新用户库存数量    
                CreateLog(userLogEntity, tran);
                tran.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                ManagerLog.WriteErr(ex);
                return 0;
            }

        }
        /// <summary>
        /// 更新员工库存数量
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userEntity"></param>
        /// <param name="tran"></param>
        private static void UpdateUserTotal(OrderEntity entity, StockEntity userEntity, YicelTransaction tran)
        {
            if (entity.OrderType == (int)OrderType.提货单 || entity.OrderType == (int)OrderType.回购单)
            {
                if (entity.Au != 0)
                    UpdateTotal(entity.UserId, Direction.提货单, GoodsType.Au, entity.Au * -1, userEntity, tran);
                if (entity.Ag != 0)
                    UpdateTotal(entity.UserId, Direction.提货单, GoodsType.Ag, entity.Ag * -1, userEntity, tran);
                if (entity.Pt != 0)
                    UpdateTotal(entity.UserId, Direction.提货单, GoodsType.Pt, entity.Pt * -1, userEntity, tran);
                if (entity.Pd != 0)
                    UpdateTotal(entity.UserId, Direction.提货单, GoodsType.Pd, entity.Pd * -1, userEntity, tran);
            }
            if (entity.OrderType == (int)OrderType.卖单)
            {
                if (entity.Au != 0)
                    UpdateTotal(entity.UserId, Direction.卖单, GoodsType.Au, entity.Au * -1, userEntity, tran);
                if (entity.Ag != 0)
                    UpdateTotal(entity.UserId, Direction.卖单, GoodsType.Ag, entity.Ag * -1, userEntity, tran);
                if (entity.Pt != 0)
                    UpdateTotal(entity.UserId, Direction.卖单, GoodsType.Pt, entity.Pt * -1, userEntity, tran);
            }
            if (entity.OrderType == (int)OrderType.金生金)
            {
                if (entity.Au != 0)
                    UpdateTotal(entity.UserId, Direction.金生金, GoodsType.Au, entity.Au * -1, userEntity, tran);
                if (entity.Ag != 0)
                    UpdateTotal(entity.UserId, Direction.金生金, GoodsType.Ag, entity.Ag * -1, userEntity, tran);
                if (entity.Pt != 0)
                    UpdateTotal(entity.UserId, Direction.金生金, GoodsType.Pt, entity.Pt * -1, userEntity, tran);
                if (entity.Pd != 0)
                    UpdateTotal(entity.UserId, Direction.金生金, GoodsType.Pd, entity.Pd * -1, userEntity, tran);
            }
            if (entity.OrderType == (int)OrderType.库存调整)
            {
                if (entity.Au != 0)
                    UpdateTotal(entity.UserId, Direction.库存调整, GoodsType.Au, entity.Au, userEntity, tran);
                if (entity.Ag != 0)
                    UpdateTotal(entity.UserId, Direction.库存调整, GoodsType.Ag, entity.Ag, userEntity, tran);
                if (entity.Pt != 0)
                    UpdateTotal(entity.UserId, Direction.库存调整, GoodsType.Pt, entity.Pt, userEntity, tran);
                if (entity.Pd != 0)
                    UpdateTotal(entity.UserId, Direction.库存调整, GoodsType.Pd, entity.Pd, userEntity, tran);
            }
        }
        /// <summary>
        /// 订单增加
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tran"></param> 
        /// <returns></returns>
        public static int CreateOrder(OrderEntity entity, YicelTransaction tran)
        {
            if (string.IsNullOrEmpty(entity.JUserId))
            {
                entity.JUserId = "";
            }
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@OrderId",entity.OrderId),
                new SqlParameter("@OrderNo",entity.OrderNo),
                new SqlParameter("@OrderCode",entity.OrderCode),
                new SqlParameter("@OrderType",entity.OrderType),
                new SqlParameter("@UserId",entity.UserId),
                new SqlParameter("@Account",entity.Account),               
                new SqlParameter("@JUserId",entity.JUserId),                
                new SqlParameter("@CarryWay",entity.CarryWay),
                new SqlParameter("@Au",entity.Au),
                new SqlParameter("@Ag",entity.Ag),
                new SqlParameter("@Pt",entity.Pt),
                new SqlParameter("@Pd",entity.Pd),
                new SqlParameter("@CreateDate",entity.CreateDate),
                new SqlParameter("@EndDate",entity.EndDate),
                new SqlParameter("@State",entity.State),
                new SqlParameter("@Version",entity.Version),
                new SqlParameter("@AuQuantity",entity.AuQuantity),
                new SqlParameter("@AgQuantity",entity.AgQuantity),
                new SqlParameter("@PtQuantity",entity.PtQuantity),
                new SqlParameter("@PdQuantity",entity.PdQuantity),
                new SqlParameter("@AgP",entity.AgP),
                new SqlParameter("@AuP",entity.AuP),
                new SqlParameter("@PdP",entity.PdP),
                new SqlParameter("@PtP",entity.PtP)
            };
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("insert into Order_BZJ({0})", Fields.Order_FIELD_LIST);
            strSql.AppendFormat(" values ({0})", "@" + Fields.Order_FIELD_LIST.Replace(",", ",@"));
            object obj = DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        /// <summary>
        /// 操作记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int CreateOrderOperation(OrderOperationEntity entity, YicelTransaction tran)
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@OperationId",entity.OperationId),
                new SqlParameter("@OrderId",entity.OrderId),
                new SqlParameter("@OrderNo",entity.OrderNo),
                new SqlParameter("@Type",entity.Type),
                new SqlParameter("@Account",entity.Account),
                new SqlParameter("@OperationIP",entity.OperationIP),
                new SqlParameter("@OperationDate",entity.OperationDate),
                new SqlParameter("@Remark",entity.Remark)
            };
            //   Dictionary<string, object> parms = OperationValueParas(entity);   @OperationId
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("insert into OrderOperation_BZJ({0})", Fields.OrderOperation_FIELD_LIST);
            strSql.AppendFormat(" values ({0})", "@" + Fields.OrderOperation_FIELD_LIST.Replace(",", ",@"));
            object obj = DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 交割单记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int CreateDeliverRecord(DeliverRecordEntity entity, YicelTransaction tran)
        {
            entity.DeliverRecordId = Guid.NewGuid().ToString("n");
            entity.CreateDate = DateTime.Now.ToString();

            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@DeliverRecordId",entity.DeliverRecordId),
                new SqlParameter("@DeliverId",entity.DeliverId),
                new SqlParameter("@DeliverNo",entity.DeliverNo),
                new SqlParameter("@OrderId",entity.OrderId),
                new SqlParameter("@OrderNo",entity.OrderNo),
                new SqlParameter("@OrderType",entity.OrderType),
                new SqlParameter("@Goods",entity.Goods),
                new SqlParameter("@Direction",entity.Direction),
                new SqlParameter("@UseTotal",entity.UseTotal),
                new SqlParameter("@LockPrice",entity.LockPrice),
                new SqlParameter("@CreateDate",entity.CreateDate)
            };
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("insert into DeliverRecord_BZJ({0})", Fields.DeliverRecord_FIELD_LIST);
            strSql.AppendFormat(" values ({0})", "@" + Fields.DeliverRecord_FIELD_LIST.Replace(",", ",@"));
            object obj = DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

       /// <summary>
        /// 交割单记录
       /// </summary>
       /// <param name="entity"></param>
       /// <param name="orderEntity"></param>
       /// <param name="operationId"></param>
       /// <param name="tran"></param>
       /// <returns></returns>
        public static int UpdateDeliver(DeliverEntity entity, OrderEntity orderEntity, string operationId, YicelTransaction tran)
        {

            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@DeliverId",entity.DeliverId),
                new SqlParameter("@DeliverNo",entity.DeliverNo),
                new SqlParameter("@Account",entity.Account),                
                new SqlParameter("@Direction",entity.Direction),
                new SqlParameter("@Total",entity.Total),
                new SqlParameter("@Goods",entity.Goods),
                new SqlParameter("@DeliverDate",entity.DeliverDate),
                new SqlParameter("@LockPrice",entity.LockPrice),
                new SqlParameter("@AvailableTotal",entity.AvailableTotal),
                new SqlParameter("@FromFlag",entity.FromFlag),
                new SqlParameter("@State",entity.State),
                new SqlParameter("@CreateDate",entity.CreateDate)
            };
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Deliver_BZJ set ");
            strSql.Append("DeliverNo=@DeliverNo,");
            strSql.Append("Account=@Account,");
            strSql.Append("Goods=@Goods,");
            strSql.Append("Direction=@Direction,");
            strSql.Append("Total=@Total,");
            strSql.Append("DeliverDate=@DeliverDate,");
            strSql.Append("LockPrice=@LockPrice,");
            strSql.Append("AvailableTotal=@AvailableTotal,");
            strSql.Append("FromFlag=@FromFlag,");
            strSql.Append("State=@State,");
            strSql.Append("CreateDate=@CreateDate");
            strSql.AppendFormat(" where DeliverId='{0}'", entity.DeliverId);
            return DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
            #region 记录出库记录
            //DeliverAdjustmentEntity deliver = new DeliverAdjustmentEntity();
            //deliver.DeliverAdjustmentID = Guid.NewGuid().ToString("n");
            //deliver.DeliverAdjustmentNO = "JS" + DateTime.Now.ToString("yyMMddhhmmssffff");
            //deliver.Goods = entity.Goods;
            //deliver.Direction = orderEntity.OrderType;
            //if (entity.Goods == 1)
            //{
            //    deliver.Total =- Math.Abs( orderEntity.Au);
            //}

            //if (entity.Goods == 2)
            //{
            //    deliver.Total = - Math.Abs(orderEntity.Ag);
            //}
            //if (entity.Goods == 3)
            //{
            //    deliver.Total = - Math.Abs(orderEntity.Pt);
            //}
            //if (entity.Goods == 4)
            //{
            //    deliver.Total = - Math.Abs(orderEntity.Pd);
            //}
            //deliver.Account = entity.Account;
            //deliver.LockPrice = 0;
            //deliver.State = 0;
            //deliver.AvailableTotal = 0;
            //deliver.FromFlag = 1;
            //deliver.OperationUserID = OperationId;
            //deliver.UserID = orderEntity.UserId;
            #endregion
            // return CreateDeliverAdjustment(deliver, tran);
        }
        
        /// <summary>
        /// 更新用户库存数量
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="d">提货 卖</param>
        /// <param name="type">物品类型</param>
        /// <param name="total"></param>
        /// <param name="userEntity">数量</param>
        /// <param name="tran"></param>
        public static void UpdateTotal(string uid, Direction d, GoodsType type, decimal total, StockEntity userEntity, YicelTransaction tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Stock_BZJ set ");
            #region
            if (d == Direction.提货单)
            {
                switch (type)
                {
                    case GoodsType.Au:
                        if (userEntity.AuTotal + total * userEntity.AuPrice >= 0)
                        {
                            strSql.Append("Au=Au+@Total,AuTotal=AuTotal+@Total*AuPrice,AuAmount=AuAmount+@Total ");
                        }
                        else
                        {
                            strSql.Append("Au=Au+@Total,AuTotal=0,AuAmount=0 ");
                        }
                        break;
                    case GoodsType.Ag:
                        if (userEntity.AgTotal + total * userEntity.AgPrice >= 0)
                        {
                            strSql.Append("Ag=Ag+@Total,AgTotal=AgTotal+@Total*AgPrice,AgAmount=AgAmount+@Total ");
                        }
                        else
                        {
                            strSql.Append("Ag=Ag+@Total,AgTotal=0,AgAmount=0 ");
                        }
                        break;
                    case GoodsType.Pt:
                        if (userEntity.PtTotal + total * userEntity.PtPrice >= 0)
                        {
                            strSql.Append("Pt=Pt+@Total,PtTotal=PtTotal+@Total*PtPrice,PtAmount=PtAmount+@Total ");
                        }
                        else
                        {
                            strSql.Append("Pt=Pt+@Total,PtTotal=0,PtAmount=0 ");
                        }
                        break;
                    case GoodsType.Pd:
                        if (userEntity.PdTotal + total * userEntity.PdPrice >= 0)
                        {
                            strSql.Append("Pd=Pd+@Total,PdTotal=PdTotal+@Total*PdPrice,PdAmount=PdAmount+@Total ");
                        }
                        else
                        {
                            strSql.Append("Pd=Pd+@Total,PdTotal=0,PdAmount=0 ");
                        }
                        break;
                }
            }
            else if (d == Direction.卖单)
            {
                switch (type)
                {
                    case GoodsType.Au:
                        strSql.Append("Au_b=Au_b+@Total ");
                        break;
                    case GoodsType.Ag:
                        strSql.Append("Ag_b=Ag_b+@Total ");
                        break;
                    case GoodsType.Pt:
                        strSql.Append("Pt_b=Pt_b+@Total ");
                        break;
                }
            }
            else if (d == Direction.金生金 || d == Direction.到期单 || d == Direction.已生金单 || d == Direction.提成单 || d == Direction.金店库存同步 || d == Direction.库存调整)
            {
                switch (type)
                {
                    case GoodsType.Au:
                        strSql.Append("Au=Au+@Total ");
                        break;
                    case GoodsType.Ag:
                        strSql.Append("Ag=Ag+@Total ");
                        break;
                    case GoodsType.Pt:
                        strSql.Append("Pt=Pt+@Total ");
                        break;
                    case GoodsType.Pd:
                        strSql.Append("Pd=Pd+@Total ");
                        break;
                }
            }
            #endregion
            strSql.Append("where UserId=@UserId");

            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@UserId",uid),
                new SqlParameter("@Total",total)
            };
            DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
        }
        #endregion
        /// <summary>
        /// 根据用户编号获取绑定信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static UserBindEntity LoadByUserId(string userId)
        {
            UserBindEntity userBindEntity = new UserBindEntity();
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select top 1 {0} from UserBind_BZJ where UserId='{1}' and IsEnable=1", Fields.UserBind_FIELD_LIST, userId);
            DataTable dt = DbHelper.ExecQuery(strSql.ToString());
            if (dt.Rows.Count > 0)
            {
                userBindEntity = CreateUserBindEntity(dt);
            }
            return userBindEntity;
        }
        #endregion
        #region 订单信息
        /// <summary>
        /// 创建提货，卖订单 返回提货，卖码
        /// </summary>
        public static int CreateOrder(string loginId, OrderEntity entity, OrderAttachEntity attachEntity)
        {
            SqlParameter[] paras = new SqlParameter[] 
            { 
            new SqlParameter("@loginId", loginId),
            new SqlParameter("@UserID", entity.UserId),
            new SqlParameter("@OrderType", entity.OrderType),         
            new SqlParameter("@CarryWay", entity.CarryWay),
            new SqlParameter("@UserType", attachEntity.UserType),
            new SqlParameter("@OrderNo", entity.OrderNo),
            new SqlParameter("@OrderCode", entity.OrderCode),          
            new SqlParameter("@Account", entity.Account),
            new SqlParameter("@State", entity.State),
            new SqlParameter("@Version", entity.Version),
            new SqlParameter("@Name", attachEntity.Name),            
            new SqlParameter("@Phone", attachEntity.Phone),
            new SqlParameter("@IDType", attachEntity.IDType),
            new SqlParameter("@IDNo", attachEntity.IDNo),
            new SqlParameter("@Remark", attachEntity.Remark)
            };
            int rows;
            return DbHelper.RunProcedure("P_CreateOrder", paras, out rows);
        }

        /// <summary>
        /// 创建回购订单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationEntity"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public static bool CreateBackOrder(OrderEntity entity, OrderOperationEntity operationEntity, string loginId)
        {
            List<DeliverEntity> deliverList = new List<DeliverEntity>();//需要更新的交割单列表
            List<DeliverRecordEntity> deliverRecordList = new List<DeliverRecordEntity>();//交割单记录列表

            StockEntity stockEntity = new StockEntity();
            stockEntity = LoadByAccount(entity.UserId); //用户信息
            #region 赋值

            entity.OrderId = Guid.NewGuid().ToString("n");
            entity.OrderNo = "HG" + DateTime.Now.ToString("yyMMddhhmmssffff");
            entity.OrderCode = "";
            entity.OrderType = (int)OrderType.回购单;
            entity.State = (int)OrderState.新订单;
            entity.CreateDate = DateTime.Now.ToString();
            entity.EndDate = DateTime.Now.AddDays(7).ToString();

            decimal temp = 0;
            if (stockEntity.Au < entity.Au)
            {
                temp = entity.Au - stockEntity.Au;
                entity.AuP = temp * entity.AuP + entity.Au * (entity.AuP - 3);
            }
            if (stockEntity.Ag < entity.Ag)
            {
                temp = entity.Ag - stockEntity.Ag;
                entity.AgP = temp * entity.AgP + entity.Ag * (entity.AgP - 1);
            }

            if (stockEntity.Pt < entity.Pt)
            {
                temp = entity.Pt - stockEntity.Pt;
                entity.PtP = temp * entity.PtP + entity.Pt * (entity.PtP - 3);
            }

            if (stockEntity.Pd < entity.Pd)
            {
                temp = entity.Pd - stockEntity.Pd;
                entity.PdP = temp * entity.PdP + entity.Pd * (entity.PdP - 3);
            }

            entity.AuQuantity = entity.Au * entity.AuP;
            entity.AgQuantity = entity.Ag * entity.AgP;
            entity.PtQuantity = entity.Pt * entity.PtP;
            entity.PdQuantity = entity.Pd * entity.PdP;

            operationEntity.OperationId = Guid.NewGuid().ToString("n");
            operationEntity.OperationDate = DateTime.Now.ToString();
            operationEntity.OrderNo = entity.OrderNo;
            operationEntity.Remark = string.Format("用户:{0} 创建回购订单", entity.Account);
            operationEntity.OrderId = entity.OrderId;
            operationEntity.Account = entity.Account;
            operationEntity.Type = entity.OrderType;

            OrderPriceEntity orderPriceEntity = new OrderPriceEntity();
            orderPriceEntity.OrderId = entity.OrderId;
            orderPriceEntity.OrderNo = entity.OrderNo;
            orderPriceEntity.PriceId = "";
            orderPriceEntity.AuPrice = stockEntity.AuPrice;
            orderPriceEntity.AgPrice = stockEntity.AgPrice;
            orderPriceEntity.PtPrice = stockEntity.PtPrice;
            orderPriceEntity.PdPrice = stockEntity.PdPrice;


            #endregion

            //判断用户是否存在交割单
            List<DeliverEntity> list = GetListByAccount(entity.Account, Direction.提货单, 1);
            if (list == null || list.Count <= 0)
            {
                return false;
            }
            #region 计算交割单信息

            Dictionary<GoodsType, decimal> dic = new Dictionary<GoodsType, decimal>();
            dic.Add(GoodsType.Au, entity.Au);
            dic.Add(GoodsType.Ag, entity.Ag);
            dic.Add(GoodsType.Pd, entity.Pd);
            dic.Add(GoodsType.Pt, entity.Pt);
            foreach (var key in dic.Keys)
            {
                var tmp = list.Where(m => m.Goods.Equals((int)key)).ToList();
                var values = dic[key];
                if (values <= 0) continue;
                foreach (var mm in tmp)
                {
                    DeliverRecordEntity recordEntity = new DeliverRecordEntity
                    {
                        DeliverId = mm.DeliverId,
                        DeliverNo = mm.DeliverNo,
                        Goods = mm.Goods,
                        Direction = mm.Direction,
                        LockPrice = mm.LockPrice,
                        OrderType = (int)OrderType.回购单,
                        OrderId = entity.OrderId,
                        OrderNo = entity.OrderNo
                    };
                    if (values > mm.AvailableTotal)
                    {
                        recordEntity.UseTotal = mm.AvailableTotal;
                        values -= mm.AvailableTotal;
                        mm.AvailableTotal = 0;
                        mm.State = 0;
                        deliverList.Add(mm);
                        deliverRecordList.Add(recordEntity);
                    }
                    else
                    {
                        mm.State = values.Equals(mm.AvailableTotal) ? 0 : 1;
                        recordEntity.UseTotal = values;
                        mm.AvailableTotal -= values;
                        deliverList.Add(mm);
                        deliverRecordList.Add(recordEntity);
                        break;
                    }
                }
            }

            #endregion

            UserLogEntity userLogEntity = new UserLogEntity();

            userLogEntity.Account = entity.UserId;
            userLogEntity.DESC = string.Format(@"用户{0}创建回购订单,订单号：{1}", entity.Account, entity.OrderNo);
            userLogEntity.UserType = (int)LogType.创建回购订单;

            YicelTransaction tran = new YicelTransaction();
            try
            {
                tran.BeginTransaction();
                CreateOrder(entity, tran);//创建订单信息
                CreateOrderOperation(operationEntity, tran);//创建回购订单操作记录
                CreatePrice(orderPriceEntity, tran);//创建回购单价格信息
                deliverRecordList.ForEach(m => CreateDeliverRecord(m, tran));//插入交割单记录信息
                deliverList.ForEach(m => UpdateDeliver(m, entity, operationEntity.OperationId, tran));//更新交割单信息
                UpdateUserTotal(entity, stockEntity, tran);//更新用户库存数量   
                CreateLog(userLogEntity, tran);
                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                return false;
            }
        }
      /// <summary>
        /// 订单回购价
      /// </summary>
      /// <param name="entity"></param>
      /// <param name="tran"></param>
      /// <returns></returns>
        public static int CreatePrice(OrderPriceEntity entity, YicelTransaction tran)
        {
            entity.OrderPriceId = Guid.NewGuid().ToString("n");

            SqlParameter[] parms = new SqlParameter[] {
            new SqlParameter("@OrderPriceId", entity.OrderPriceId),
            new SqlParameter("@OrderId", entity.OrderId),
            new SqlParameter("@OrderNo", entity.OrderNo),
            new SqlParameter("@PriceId", entity.PriceId),
            new SqlParameter("@AuPrice", entity.AuPrice),
            new SqlParameter("@AgPrice", entity.AgPrice),
            new SqlParameter("@PtPrice", entity.PtPrice),
            new SqlParameter("@PdPrice", entity.PdPrice)
            };

            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("insert into OrderPrice_BZJ({0})", Fields.OrderPrice_FIELD_LIST);
            strSql.AppendFormat(" values ({0})", "@" + Fields.OrderPrice_FIELD_LIST.Replace(",", ",@"));
            object obj = DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static DataTable OrderLog(string userID, int pageindex, int pagesize, ref int page)
        {
            SqlParameter[] paras = new SqlParameter[] 
            { 
             new SqlParameter("@selectlist", "*"),
            new SqlParameter("@SubSelectList", "*"),
            new SqlParameter("@TableSource", "Order_BZJ"),
            new SqlParameter("@TableOrder", "a"),
            new SqlParameter("@SearchCondition", string.Format(@" UserId='{0}'",userID)),
            new SqlParameter("@OrderExpression", "CreateDate"),             
            new SqlParameter("@PageIndex", pageindex),
            new SqlParameter("@PageSize", pagesize),
            new SqlParameter("@PageCount", page)
              };

            paras[8].Direction = ParameterDirection.Output;
            DataTable dt = DbHelper.RunProcedure("GetRecordFromPage", paras, "bzj_order").Tables[0];
            page = Convert.ToInt32(paras[8].Value);
            return dt;

        }

        /// <summary>
        /// 根据回购订单号加载回购信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static DataTable LoadByOrderHG(string orderNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append("from vOrder_BZJ ");
            strSql.AppendFormat("where OrderNo='{0}' ", orderNo);

            return DbHelper.ExecQuery(strSql.ToString());
        }

        /// <summary>
        /// 更新回购订单状态并记录操作日志 
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static bool UpdateBuyBackOrder(string agentId, OrderEntity order)
        {
            UserLogEntity userLogEntity = new UserLogEntity();
            userLogEntity.OperTime = DateTime.Now.ToString();
            userLogEntity.Account = agentId;
            userLogEntity.DESC = string.Format(@"更新回购订单状态：金商{0}，订单号：{1}订单ID:{2};", agentId, order.IDNo, order.OrderId);
            userLogEntity.UserType = (int)LogType.金商回购单状态修改;
            YicelTransaction tran = new YicelTransaction();
            SqlParameter[] parms = new SqlParameter[] {               
            };
            string strSql = string.Format(@"update Order_BZJ set version={0},state={1} ,OperationDate=GETDATE(),ClerkID='{4}',AgentID='{4}' where OrderNo='{2}' and userid='{3}'", order.Version, order.State, order.OrderNo, order.UserId, agentId);
            try
            {
                tran.BeginTransaction();
                DbHelper.ExecuteNonQuery(strSql, parms, tran.Transaction);
                CreateLog(userLogEntity, tran);
                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                return false;
            }

        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="orderNO"></param>
        /// <param name="userId">用户ID</param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static bool UpdateOrderState(string orderNO, string userId, int state)
        {
            UserLogEntity userLogEntity = new UserLogEntity();
            userLogEntity.OperTime = DateTime.Now.ToString();
            // userLogEntity.UserID = userid;
            userLogEntity.DESC = string.Format(@"更新回购订单状态：用户{0}，订单号：{1};", userId, orderNO);
            userLogEntity.UserType = (int)LogType.订单修改;
            YicelTransaction tran = new YicelTransaction();
            SqlParameter[] parms = new SqlParameter[] {               
            };
            string strSql = string.Format(@"update Order_BZJ set version={0},state={1} ,enddate='{2}' where OrderNo='{3}' and userid='{4}'", 2, state, DateTime.Now.ToString(), orderNO, userId);
            try
            {
                tran.BeginTransaction();
                DbHelper.ExecuteNonQuery(strSql, parms, tran.Transaction);
                CreateLog(userLogEntity, tran);
                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                return false;
            }
        }

        /// <summary>
        /// 根据订单号查询订单实体
        /// </summary>
        /// <param name="orderNO"></param>
        /// <returns></returns>
        public static OrderEntity LoadByOrder(string orderNO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append("from vOrder_BZJ ");
            strSql.AppendFormat("where OrderNo='{0}' ", orderNO);

            DataTable dt = DbHelper.ExecQuery(strSql.ToString());
            if (dt.Rows.Count > 0)
            {
                OrderEntity entity = new OrderEntity();
                entity.OrderId = dt.Rows[0]["OrderId"].ToString();
                entity.OrderNo = dt.Rows[0]["OrderNo"].ToString();
                entity.OrderCode = dt.Rows[0]["OrderCode"].ToString();
                entity.OrderType = Convert.ToInt32(dt.Rows[0]["OrderType"]);
                entity.UserId = dt.Rows[0]["UserId"].ToString();
                entity.Account = dt.Rows[0]["Account"].ToString();
                entity.JUserId = dt.Rows[0]["JUserId"].ToString();
                entity.CarryWay = Convert.ToInt32(dt.Rows[0]["CarryWay"]);
                entity.Au = Convert.ToDecimal(dt.Rows[0]["Au"]);
                entity.Ag = Convert.ToDecimal(dt.Rows[0]["Ag"]);
                entity.Pt = Convert.ToDecimal(dt.Rows[0]["Pt"]);
                entity.Pd = Convert.ToDecimal(dt.Rows[0]["Pd"]);
                entity.CreateDate = dt.Rows[0]["CreateDate"].ToString();
                entity.EndDate = dt.Rows[0]["EndDate"].ToString();
                entity.State = Convert.ToInt32(dt.Rows[0]["State"]);
                entity.Version = Convert.ToInt32(dt.Rows[0]["Version"]);
                entity.AuQuantity = Math.Abs(Convert.ToDecimal(dt.Rows[0]["AuQuantity"]));
                entity.AgQuantity = Math.Abs(Convert.ToDecimal(dt.Rows[0]["AgQuantity"]));
                entity.PtQuantity = Math.Abs(Convert.ToDecimal(dt.Rows[0]["PtQuantity"]));
                entity.PdQuantity = Math.Abs(Convert.ToDecimal(dt.Rows[0]["PdQuantity"]));
                entity.AgP = Convert.ToDecimal(dt.Rows[0]["AgP"]);
                entity.AuP = Convert.ToDecimal(dt.Rows[0]["AuP"]);
                entity.PdP = Convert.ToDecimal(dt.Rows[0]["PdP"]);
                entity.PtP = Convert.ToDecimal(dt.Rows[0]["PtP"]);
                return entity;
            }
            return null;
        }
        /// <summary>
        /// 创建订单实体
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static OrderEntity CreateOrderEntity(DbDataReader reader)
        {
            OrderEntity entity = new OrderEntity();
            entity.OrderId = reader["OrderId"].ToString();
            entity.OrderNo = reader["OrderNo"].ToString();
            entity.OrderCode = reader["OrderCode"].ToString();
            entity.OrderType = Convert.ToInt32(reader["OrderType"]);
            entity.UserId = reader["UserId"].ToString();
            entity.Account = reader["Account"].ToString();
            entity.JUserId = reader["JUserId"].ToString();
            entity.CarryWay = Convert.ToInt32(reader["CarryWay"]);
            entity.Au = Convert.ToDecimal(reader["Au"]);
            entity.Ag = Convert.ToDecimal(reader["Ag"]);
            entity.Pt = Convert.ToDecimal(reader["Pt"]);
            entity.Pd = Convert.ToDecimal(reader["Pd"]);
            entity.CreateDate = reader["CreateDate"].ToString();
            entity.EndDate = reader["EndDate"].ToString();
            entity.State = Convert.ToInt32(reader["State"]);
            entity.Version = Convert.ToInt32(reader["Version"]);
            entity.AuQuantity = Math.Abs(Convert.ToDecimal(reader["AuQuantity"]));
            entity.AgQuantity = Math.Abs(Convert.ToDecimal(reader["AgQuantity"]));
            entity.PtQuantity = Math.Abs(Convert.ToDecimal(reader["PtQuantity"]));
            entity.PdQuantity = Math.Abs(Convert.ToDecimal(reader["PdQuantity"]));
            entity.AgP = Convert.ToDecimal(reader["AgP"]);
            entity.AuP = Convert.ToDecimal(reader["AuP"]);
            entity.PdP = Convert.ToDecimal(reader["PdP"]);
            entity.PtP = Convert.ToDecimal(reader["PtP"]);
            return entity;
        }
        /// <summary>
        /// 根据回购订单号加载回购信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static DataTable LoadByHGOrderInfo(string orderNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append("from Order_BZJ ");
            strSql.AppendFormat("where OrderNo='{0}' and State=1 and (OrderType=1 or OrderType=2)", orderNo);

            return DbHelper.ExecQuery(strSql.ToString());
        }

        /// <summary>
        /// 根据用户账户和交割单方向统计不同物品重量
        /// </summary>
        /// <param name="account">用户账户</param>
        /// <param name="direction">交割单方向</param>
        /// <returns></returns>
        public static Dictionary<GoodsType, GoodsEntity> GetGoodsDic(string account, Direction direction)
        {
            string strSql = "";
            if (Convert.ToInt32(direction) == 1)
            {
                strSql = "Select Goods,Sum(AvailableTotal) as Total from Deliver_BZJ where Direction in('1','5','6','7','8','13') and State=1 and Account=@Account Group By Goods";
            }
            else
            {
                strSql = "Select Goods,Sum(AvailableTotal) as Total from Deliver_BZJ where Direction=@Direction and State=1 and Account=@Account Group By Goods";
            }
            Dictionary<string, object> parms = new Dictionary<string, object>();
            parms.Add("@Direction", (int)direction);
            parms.Add("@Account", account);

            Dictionary<GoodsType, GoodsEntity> dic = new Dictionary<GoodsType, GoodsEntity>();
            dic.Add(GoodsType.Au, new GoodsEntity());
            dic.Add(GoodsType.Ag, new GoodsEntity());
            dic.Add(GoodsType.Pd, new GoodsEntity());
            dic.Add(GoodsType.Pt, new GoodsEntity());
            using (DbDataReader reader = DbHelper.ExecuteReader(strSql, parms))
            {
                while (reader.Read())
                {
                    var entity = new GoodsEntity();
                    var goodsType = (GoodsType)Convert.ToInt32(reader["Goods"]);
                    if (dic.ContainsKey(goodsType))
                    {
                        dic.Remove(goodsType);
                        entity.Symbol = (GoodsType)Convert.ToInt32(reader["Goods"]);
                        entity.Total = Convert.ToDecimal(reader["Total"]);
                        dic.Add(entity.Symbol, entity);
                    }
                }
                reader.Close();
                reader.Dispose();
            }
            return dic;
        }

        /// <summary>
        /// 根据用户账号,交割单方向,状态 获取交割单信息
        /// </summary>
        /// <param name="account">用户账号</param>
        /// <param name="direction">交割单方向</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public static List<DeliverEntity> GetListByAccount(string account, Direction direction, int state)
        {
            List<DeliverEntity> list = new List<DeliverEntity>();
            StringBuilder strSql = new StringBuilder();
            if (Convert.ToInt32(direction) == 1)
            {
                strSql.AppendFormat("select {0} from Deliver_BZJ where Account=@Account and Direction in('1','5','6','7','9','13') and State=@State", Fields.Deliver_FIELD_LIST);
            }
            else
            {
                strSql.AppendFormat("select {0} from Deliver_BZJ where Account=@Account and Direction=@Direction and State=@State", Fields.Deliver_FIELD_LIST);
            }
            strSql.AppendFormat(" order by DeliverDate Asc,Total Asc");
            Dictionary<string, object> parms = new Dictionary<string, object>();
            parms.Add("@Account", account);
            parms.Add("@Direction", (int)direction);
            parms.Add("@State", state);
            using (DbDataReader reader = DbHelper.ExecuteReader(strSql.ToString(), parms))
            {
                while (reader.Read())
                {
                    DeliverEntity entity = CreateDeliverEntity(reader);
                    list.Add(entity);
                }
                reader.Close();
                reader.Dispose();
            }
            return list;
        }
        /// <summary>
        /// 保证金实体
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static DeliverEntity CreateDeliverEntity(DbDataReader reader)
        {
            DeliverEntity entity = new DeliverEntity();
            entity.DeliverId = reader["DeliverId"].ToString();
            entity.DeliverNo = reader["DeliverNo"].ToString();
            entity.Account = reader["Account"].ToString();
            entity.Goods = Convert.ToInt32(reader["Goods"]);
            entity.Direction = Convert.ToInt32(reader["Direction"]);
            entity.Total = Convert.ToDecimal(reader["Total"]);
            entity.DeliverDate = reader["DeliverDate"].ToString();
            entity.LockPrice = Convert.ToDecimal(reader["LockPrice"]);
            entity.AvailableTotal = Convert.ToDecimal(reader["AvailableTotal"]);
            entity.FromFlag = Convert.ToInt32(reader["FromFlag"]);
            entity.State = Convert.ToInt32(reader["State"]);
            entity.CreateDate = reader["CreateDate"].ToString();
            return entity;
        }

        /// <summary>
        /// 后台用户库存入库
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="orderType"></param>
        /// <param name="product"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public static int CreateDeliverAdmin(DeliverEntity entity, string orderType, string product, string loginId)
        {
            SqlParameter[] paras = new SqlParameter[] { 
                 new SqlParameter("@loginId", loginId),
                new SqlParameter("@OrderNo", entity.DeliverNo),
                new SqlParameter("@Account", entity.Account),
                 new SqlParameter("@UserID", entity.UserID),
                new SqlParameter("@Price", entity.LockPrice),
                new SqlParameter("@OrderType", orderType),
                new SqlParameter("@Product", product),
                new SqlParameter("@Quantity", entity.Total),   
              new SqlParameter("@Direction", entity.Direction),   
                new SqlParameter("@OperUserID", entity.OperationUserID)
            };
            int result;
            return DbHelper.RunProcedure("P_CreateDeliverAdmin", paras, out result);
        }
        /// <summary>
        /// 交割单报表
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="userID"></param>
        /// <param name="deliverType"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="goods"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static DataTable GetDeliverList(string loginId, string userID, int deliverType, string startTime, string endTime, int goods, int pageindex, int pagesize, ref int page)
        {
            SqlParameter[] paras = null;
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(@" UserId='{0}'", userID);
            if (!string.IsNullOrEmpty(startTime))
            {
                sb.AppendFormat(@" and CreateDate BETWEEN '{0}' AND '{1}'", startTime, endTime);
            }

            if (goods > 0)
            {
                sb.AppendFormat(@" and goods={0}", goods);
            }
            if (deliverType == 1)
            {
                //    sb.AppendFormat(@" and Direction IN ('1','9','10','11','12','3','6')");
                paras = new SqlParameter[] 
            { 
                    new SqlParameter("@selectlist", "DeliverId,DeliverNo,Goods,Direction,Total,LockPrice,AvailableTotal,CreateDate"),
                    new SqlParameter("@SubSelectList", "DeliverId,DeliverNo,Goods,Direction,Total,LockPrice,AvailableTotal,CreateDate"),
                    new SqlParameter("@TableSource", "Deliver_BZJ"),
                    new SqlParameter("@TableOrder", "a"),
                    new SqlParameter("@SearchCondition", sb.ToString()),
                    new SqlParameter("@OrderExpression", "CreateDate"),             
                    new SqlParameter("@PageIndex", pageindex),
                    new SqlParameter("@PageSize", pagesize),
                    new SqlParameter("@PageCount", page)
              };
            }
            else
            {
                // sb.AppendFormat(@" and Direction IN ('2','4','3')");
                paras = new SqlParameter[] 
            { 
                    new SqlParameter("@selectlist", "DeliverId,DeliverNo,Goods,Direction,0 as Total,UseTotal as AvailableTotal,LockPrice,CreateDate"),
                    new SqlParameter("@SubSelectList", "DeliverId,DeliverNo,Goods,Direction,UseTotal,LockPrice,CreateDate"),
                    new SqlParameter("@TableSource", "V_DeliverRecord"),
                    new SqlParameter("@TableOrder", "a"),
                    new SqlParameter("@SearchCondition", sb.ToString()),
                    new SqlParameter("@OrderExpression", "CreateDate"),             
                    new SqlParameter("@PageIndex", pageindex),
                    new SqlParameter("@PageSize", pagesize),
                    new SqlParameter("@PageCount", page)
              };
            }
            paras[8].Direction = ParameterDirection.Output;
            DataTable dt = DbHelper.RunProcedure("GetRecordFromPage", paras, "Deliver_BZJ").Tables[0];
            page = Convert.ToInt32(paras[8].Value);
            return dt;

        }

        /// <summary>
        /// 交割单报表查询
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="entity"></param>
        /// <param name="userName"></param>
        /// <param name="agentId"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static DataTable GetDeliverOrderList(string loginId, DeliverEntity entity, string userName, string agentId, int pageindex, int pagesize, ref int page)
        {
            SqlParameter[] paras = null;
            StringBuilder sb = new StringBuilder();
            StringBuilder sbSearch = new StringBuilder();
            if (!string.IsNullOrEmpty(entity.DeliverNo))
            {
                sbSearch.AppendFormat(@" and DeliverNo='{0}'", entity.DeliverNo);
            }
            if (!string.IsNullOrEmpty(entity.Account))
            {
                sbSearch.AppendFormat(@" and Account='{0}'", entity.Account);
            }
            if (!string.IsNullOrEmpty(userName))
            {
                sbSearch.AppendFormat(@" and userName like '{0}%'", userName);
            }
            if (!string.IsNullOrEmpty(entity.CreateDate))
            {
                sbSearch.AppendFormat(@" and CreateDate BETWEEN '{0}' AND '{1}'", entity.CreateDate, entity.EndDate);
            }
            if (!string.IsNullOrEmpty(agentId))
            {
                sbSearch.AppendFormat(@" and agentid in ({0}) ", ComFunction.GetOrgIds(agentId));
            }
            paras = new SqlParameter[] 
            {
                new SqlParameter("@selectlist", "  Account,userName,DeliverId,DeliverDate,DeliverNo,Goods,Direction,Total,LockPrice,State,AvailableTotal,FromFlag,CreateDate"),
                new SqlParameter("@SubSelectList", " Account,userName,DeliverId,DeliverDate,DeliverNo,Goods,Direction,Total,LockPrice,State,AvailableTotal,FromFlag,CreateDate"),
                new SqlParameter("@TableSource", "V_OrderDelivery"),
                new SqlParameter("@TableOrder", "a"),
                new SqlParameter("@SearchCondition", string.Format(@" 1=1 {0}",sbSearch.ToString())),
                new SqlParameter("@OrderExpression", " order by CreateDate desc"),             
                new SqlParameter("@PageIndex", pageindex),
                new SqlParameter("@PageSize", pagesize),
                new SqlParameter("@PageCount", page)
            };

            paras[8].Direction = ParameterDirection.Output;
            DataTable dt = DbHelper.RunProcedure("GetRecordFromPage", paras, "Deliver_BZJ").Tables[0];
            page = Convert.ToInt32(paras[8].Value);
            return dt;
        }
        /// <summary>
        /// 根据订单号和订单类别加载订单信息
        /// </summary>
        /// <param name="orderCode"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static DataTable LoadByOrder(string orderCode, OrderType orderType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append("from Order_BZJ ");
            strSql.AppendFormat("where OrderCode='{0}' and OrderType='{1}' ", orderCode, (int)orderType);
            return DbHelper.ExecQuery(strSql.ToString());
        }

        #endregion
        #region 金商管理
        /// <summary>
        /// 检查库存中心是否与交割信息相等
        /// </summary>
        private static bool CheckTotal(OrderEntity entity, StockEntity userEntity)
        {
            //获取用户交割单各物品总重量
            Dictionary<GoodsType, GoodsEntity> goodsDic = null;
            //判断用户物品数量是否与交割物品数量相等 2013/4/6
            if (entity.OrderType == (int)OrderType.提货单 || entity.OrderType == (int)OrderType.金生金 || entity.OrderType == (int)OrderType.回购单)
            {
                goodsDic = ComFunction.GetGoodsDic(entity.Account, Direction.提货单);
                if (goodsDic[GoodsType.Au].Total != userEntity.Au)
                    return false;
                if (goodsDic[GoodsType.Ag].Total != userEntity.Ag)
                    return false;
                if (goodsDic[GoodsType.Pt].Total != userEntity.Pt)
                    return false;
                if (goodsDic[GoodsType.Pd].Total != userEntity.Pd)
                    return false;
            }
            else if (entity.OrderType == (int)OrderType.卖单)
            {
                goodsDic = ComFunction.GetGoodsDic(entity.Account, Direction.卖单);
                if (goodsDic[GoodsType.Au].Total != userEntity.Au_b)
                    return false;
                if (goodsDic[GoodsType.Ag].Total != userEntity.Ag_b)
                    return false;
                if (goodsDic[GoodsType.Pt].Total != userEntity.Pt_b)
                    return false;
                if (goodsDic[GoodsType.Pd].Total != userEntity.Pd_b)
                    return false;
            }
            else
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 更新订单状态为已提货或卖
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="order"></param>
        /// <param name="dataMember"></param>
        /// <param name="operationEntity"></param>
        /// <returns></returns>
        public static bool UpdateOrder(string agentId, OrderEntity order, List<UpdateDataMember> dataMember, OrderOperationEntity operationEntity)
        {
            List<DeliverEntity> deliverList = new List<DeliverEntity>();//需要更新的交割单列表
            List<DeliverRecordEntity> deliverRecordList = new List<DeliverRecordEntity>();
            List<UpdateDataMember> updateList = new List<UpdateDataMember>();//需要更新的订单信息
            List<UpdateDataMember> userList = new List<UpdateDataMember>();//需要更新用户数量

            OrderEntity entity = LoadByOrderCode(order.OrderCode);//获取订单信息
            StockEntity stockEntity = LoadByAccount(entity.UserId);//获取订单所属用户信息
            Direction d = entity.OrderType == (int)OrderType.提货单 ? Direction.提货单 : Direction.卖单;

            #region 合法性验证
            //获取用户交割单各物品总重量
            Dictionary<GoodsType, GoodsEntity> goodsDic = GetGoodsDic(entity.Account, d);
            //判断用户交割单种类是否小于0 或者小于提交的种类数量
            if (goodsDic.Count <= 0 || goodsDic.Count < dataMember.Count)
                return false;
            //判断用户物品数量是否与交割物品数量相等 2013/4/6
            if (!CheckTotal(order, stockEntity))
                return false;

            //循环判断提交重量是否大于用户交割单总重量
            foreach (var m in dataMember)
            {
                foreach (var g in goodsDic.Values)
                {
                    if (m.field.ToLower() == g.Symbol.ToString().ToLower() && Convert.ToDecimal(m.value) > g.Total)
                    {
                        return false;
                    }
                    else
                    {
                        if (m.field.ToLower() == "au")
                        {
                            entity.Au = Convert.ToDecimal(m.value);
                            updateList.Add(new UpdateDataMember { field = "Au", value = m.value });

                            updateList.Add(new UpdateDataMember { field = "AuP", value = stockEntity.AuPrice });
                            if (Convert.ToDecimal(m.value) > stockEntity.AuAmount)
                            {
                                updateList.Add(new UpdateDataMember { field = "AuQuantity", value = stockEntity.AuAmount });
                            }
                            else
                            {
                                updateList.Add(new UpdateDataMember { field = "AuQuantity", value = m.value });
                            }
                            break;
                        }
                        if (m.field.ToLower() == "ag")
                        {
                            entity.Ag = Convert.ToDecimal(m.value);
                            updateList.Add(new UpdateDataMember { field = "Ag", value = m.value });

                            updateList.Add(new UpdateDataMember { field = "AgP", value = stockEntity.AgPrice });
                            if (Convert.ToDecimal(m.value) > stockEntity.AuAmount)
                            {
                                updateList.Add(new UpdateDataMember { field = "AgQuantity", value = stockEntity.AgAmount });
                            }
                            else
                            {
                                updateList.Add(new UpdateDataMember { field = "AgQuantity", value = m.value });
                            }
                            break;
                        }
                        if (m.field.ToLower() == "pt")
                        {
                            entity.Pt = Convert.ToDecimal(m.value);
                            updateList.Add(new UpdateDataMember { field = "Pt", value = m.value });

                            updateList.Add(new UpdateDataMember { field = "PtP", value = stockEntity.PtPrice });
                            if (Convert.ToDecimal(m.value) > stockEntity.AuAmount)
                            {
                                updateList.Add(new UpdateDataMember { field = "PtQuantity", value = stockEntity.PtAmount });
                            }
                            else
                            {
                                updateList.Add(new UpdateDataMember { field = "PtQuantity", value = m.value });
                            }
                            break;
                        }
                        if (m.field.ToLower() == "pd")
                        {
                            entity.Pd = Convert.ToDecimal(m.value);
                            updateList.Add(new UpdateDataMember { field = "Pd", value = m.value });

                            updateList.Add(new UpdateDataMember { field = "PdP", value = stockEntity.AuPrice });
                            if (Convert.ToDecimal(m.value) > stockEntity.AuAmount)
                            {
                                updateList.Add(new UpdateDataMember { field = "PdQuantity", value = stockEntity.AuAmount });
                            }
                            else
                            {
                                updateList.Add(new UpdateDataMember { field = "PdQuantity", value = m.value });
                            }
                            break;
                        }
                    }
                }
            }

            #endregion

            //获取交割单信息
            List<DeliverEntity> list = GetListByAccount(entity.Account, d, 1);
            if (list == null || list.Count <= 0)
                return false;

            #region 计算交割单信息

            Dictionary<GoodsType, decimal> dic = new Dictionary<GoodsType, decimal>();
            dic.Add(GoodsType.Au, entity.Au);
            dic.Add(GoodsType.Ag, entity.Ag);
            dic.Add(GoodsType.Pd, entity.Pd);
            dic.Add(GoodsType.Pt, entity.Pt);
            foreach (var key in dic.Keys)
            {
                var tmp = list.Where(m => m.Goods.Equals(key)).ToList(); var values = dic[key];
                if (values <= 0) continue;
                foreach (var mm in tmp)
                {
                    DeliverRecordEntity recordEntity = new DeliverRecordEntity
                    {
                        DeliverId = mm.DeliverId,
                        DeliverNo = mm.DeliverNo,
                        Goods = mm.Goods,
                        Direction = mm.Direction,
                        LockPrice = mm.LockPrice,
                        OrderType = order.OrderType,
                        OrderId = entity.OrderId,
                        OrderNo = entity.OrderNo
                    };
                    if (values > mm.AvailableTotal)
                    {
                        recordEntity.UseTotal = mm.AvailableTotal;
                        values -= mm.AvailableTotal;
                        mm.AvailableTotal = 0;
                        mm.State = 0;
                        deliverList.Add(mm);
                        deliverRecordList.Add(recordEntity);
                    }
                    else
                    {
                        mm.State = values.Equals(mm.AvailableTotal) ? 0 : 1;
                        recordEntity.UseTotal = values;
                        mm.AvailableTotal -= values;
                        deliverList.Add(mm);
                        deliverRecordList.Add(recordEntity);
                        break;
                    }
                }
            }

            #endregion

            #region 部分实体赋值
            AgentDeliverEntity agentDeliverEntity = new AgentDeliverEntity
            {
                AgentInfoId = agentId,
                OrderId = entity.OrderId,
                FromTo = 1,
                Direction = d,
                Ag = entity.Ag,
                Au = entity.Au,
                Pd = entity.Pd,
                Pt = entity.Pt
            };
            #endregion
            updateList.Add(new UpdateDataMember { field = "State", value = "2" });
            updateList.Add(new UpdateDataMember { field = "Version", value = (entity.Version + 1).ToString() });
            updateList.Add(new UpdateDataMember { field = "EndDate", value = DateTime.Now.ToString() });

            UserLogEntity userLogEntity = new UserLogEntity();

            userLogEntity.Account = agentId;
            userLogEntity.DESC = string.Format(@"金商{5},用户{0}提货：Ag:{1},Au:{2},Pt:{3},Pd:{4};", entity.Account, entity.Au, entity.Ag, entity.Pt, entity.Pd, entity.AgentName);
            userLogEntity.UserType = (int)LogType.用户库存调整减少;

            operationEntity.OrderId = entity.OrderId;
            operationEntity.OrderNo = entity.OrderNo;
            YicelTransaction tran = new YicelTransaction();
            try
            {
                tran.BeginTransaction();
                UpdateColumns(entity.OrderId, updateList, tran);
                CreateOrderOperation(operationEntity, tran);//创建订单操作记录
                deliverList.ForEach(m => UpdateDeliver(m, entity, operationEntity.OperationId, tran));//更新交割单信息
                deliverRecordList.ForEach(m => CreateDeliverRecord(m, tran));//创建订单对应交割单信息                  
                UpdateUserTotal(entity, stockEntity, tran);//更新用户库存数量
                CreateAgent(agentDeliverEntity, tran);//创建金商库存
                CreateLog(userLogEntity, tran);
                tran.Commit();
                return true;
            }
            catch 
            {
                tran.Rollback();
                return false;
            }

        }



        /// <summary>
        /// 更新订单状态为已提货或卖
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="order"></param>
        /// <param name="dataMember"></param>
        /// <param name="operationEntity"></param>
        /// <param name="dList"></param>
        /// <param name="userId"></param>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public static bool UpdateBackOrder(string agentId, OrderEntity order, List<UpdateDataMember> dataMember, OrderOperationEntity operationEntity, List<DeliverEntity> dList, string userId, StockEntity userEntity)
        {
            List<DeliverEntity> deliverList = new List<DeliverEntity>();//需要更新的交割单列表
            List<DeliverRecordEntity> deliverRecordList = new List<DeliverRecordEntity>();
            List<UpdateDataMember> updateList = new List<UpdateDataMember>();//需要更新的订单信息
            List<UpdateDataMember> userList = new List<UpdateDataMember>();//需要更新用户数量

            OrderEntity entity = LoadByOrderCode(order.OrderCode);//获取订单信息
            StockEntity stockEntity = LoadByAccount(entity.UserId);//获取订单所属用户信息
            Direction d = entity.OrderType == (int)OrderType.提货单 ? Direction.提货单 : Direction.卖单;

            #region 合法性验证
            //获取用户交割单各物品总重量
            Dictionary<GoodsType, GoodsEntity> goodsDic = GetGoodsDic(entity.Account, d);
            //判断用户交割单种类是否小于0 或者小于提交的种类数量
            if (goodsDic.Count <= 0 || goodsDic.Count < dataMember.Count)
                return false;
            //判断用户物品数量是否与交割物品数量相等 2013/4/6
            if (!CheckTotal(order, stockEntity))
                return false;

            //循环判断提交重量是否大于用户交割单总重量
            foreach (var m in dataMember)
            {
                foreach (var g in goodsDic.Values)
                {
                    if (m.field.ToLower() == g.Symbol.ToString().ToLower() && Convert.ToDecimal(m.value) > g.Total)
                    {
                        return false;
                    }
                    else
                    {
                        if (m.field.ToLower() == "au")
                        {
                            entity.Au = Convert.ToDecimal(m.value);
                            updateList.Add(new UpdateDataMember { field = "Au", value = m.value });

                            updateList.Add(new UpdateDataMember { field = "AuP", value = stockEntity.AuPrice });
                            if (Convert.ToDecimal(m.value) > stockEntity.AuAmount)
                            {
                                updateList.Add(new UpdateDataMember { field = "AuQuantity", value = stockEntity.AuAmount });
                            }
                            else
                            {
                                updateList.Add(new UpdateDataMember { field = "AuQuantity", value = m.value });
                            }
                            break;
                        }
                        if (m.field.ToLower() == "ag")
                        {
                            entity.Ag = Convert.ToDecimal(m.value);
                            updateList.Add(new UpdateDataMember { field = "Ag", value = m.value });

                            updateList.Add(new UpdateDataMember { field = "AgP", value = stockEntity.AgPrice });
                            if (Convert.ToDecimal(m.value) > stockEntity.AuAmount)
                            {
                                updateList.Add(new UpdateDataMember { field = "AgQuantity", value = stockEntity.AgAmount });
                            }
                            else
                            {
                                updateList.Add(new UpdateDataMember { field = "AgQuantity", value = m.value });
                            }
                            break;
                        }
                        if (m.field.ToLower() == "pt")
                        {
                            entity.Pt = Convert.ToDecimal(m.value);
                            updateList.Add(new UpdateDataMember { field = "Pt", value = m.value });

                            updateList.Add(new UpdateDataMember { field = "PtP", value = stockEntity.PtPrice });
                            if (Convert.ToDecimal(m.value) > stockEntity.AuAmount)
                            {
                                updateList.Add(new UpdateDataMember { field = "PtQuantity", value = stockEntity.PtAmount });
                            }
                            else
                            {
                                updateList.Add(new UpdateDataMember { field = "PtQuantity", value = m.value });
                            }
                            break;
                        }
                        if (m.field.ToLower() == "pd")
                        {
                            entity.Pd = Convert.ToDecimal(m.value);
                            updateList.Add(new UpdateDataMember { field = "Pd", value = m.value });

                            updateList.Add(new UpdateDataMember { field = "PdP", value = stockEntity.PdPrice });
                            if (Convert.ToDecimal(m.value) > stockEntity.AuAmount)
                            {
                                updateList.Add(new UpdateDataMember { field = "PdQuantity", value = stockEntity.PdAmount });
                            }
                            else
                            {
                                updateList.Add(new UpdateDataMember { field = "PdQuantity", value = m.value });
                            }
                            break;
                        }
                    }
                }
            }

            #endregion

            //获取交割单信息
            List<DeliverEntity> list = GetListByAccount(entity.Account, d, 1);
            if (list == null || list.Count <= 0)
                return false;

            #region 计算交割单信息

            Dictionary<GoodsType, decimal> dic = new Dictionary<GoodsType, decimal>();
            dic.Add(GoodsType.Au, entity.Au);
            dic.Add(GoodsType.Ag, entity.Ag);
            dic.Add(GoodsType.Pd, entity.Pd);
            dic.Add(GoodsType.Pt, entity.Pt);
            foreach (var key in dic.Keys)
            {
                var tmp = list.Where(m => m.Goods.Equals((int)key)).ToList(); var values = dic[key];
                if (values <= 0) continue;
                foreach (var mm in tmp)
                {
                    DeliverRecordEntity recordEntity = new DeliverRecordEntity
                    {
                        DeliverId = mm.DeliverId,
                        DeliverNo = mm.DeliverNo,
                        Goods = mm.Goods,
                        Direction = mm.Direction,
                        LockPrice = mm.LockPrice,
                        OrderType = order.OrderType,
                        OrderId = entity.OrderId,
                        OrderNo = entity.OrderNo
                    };
                    if (values > mm.AvailableTotal)
                    {
                        recordEntity.UseTotal = mm.AvailableTotal;
                        values -= mm.AvailableTotal;
                        mm.AvailableTotal = 0;
                        mm.State = 0;
                        deliverList.Add(mm);
                        deliverRecordList.Add(recordEntity);
                    }
                    else
                    {
                        mm.State = values.Equals(mm.AvailableTotal) ? 0 : 1;
                        recordEntity.UseTotal = values;
                        mm.AvailableTotal -= values;
                        deliverList.Add(mm);
                        deliverRecordList.Add(recordEntity);
                        break;
                    }
                }
            }

            #endregion

            #region 部分实体赋值
            AgentDeliverEntity agentDeliverEntity = new AgentDeliverEntity
            {
                AgentInfoId = agentId,
                OrderId = entity.OrderId,
                FromTo = 1,
                Direction = d,
                Ag = entity.Ag,
                Au = entity.Au,
                Pd = entity.Pd,
                Pt = entity.Pt
            };
            #endregion
            updateList.Add(new UpdateDataMember { field = "State", value = "2" });
            updateList.Add(new UpdateDataMember { field = "Version", value = (entity.Version + 1).ToString() });
            updateList.Add(new UpdateDataMember { field = "EndDate", value = DateTime.Now.ToString() });

            UserLogEntity userLogEntity = new UserLogEntity();

            userLogEntity.Account = agentId;
            userLogEntity.DESC = string.Format(@"金商{5},用户{0}提货：Ag:{1},Au:{2},Pt:{3},Pd:{4};", entity.Account, entity.Au, entity.Ag, entity.Pt, entity.Pd, entity.AgentName);
            userLogEntity.UserType = (int)LogType.用户库存调整减少;

            operationEntity.OrderId = entity.OrderId;
            operationEntity.OrderNo = entity.OrderNo;
            operationEntity.OperationId = Guid.NewGuid().ToString("n");
            StockEntity stock = new StockEntity();
            stock = LoadByAccount(userId);//获取订单所属用户信息


            stock.Au = stock.Au + Math.Abs(order.Au);
            stock.Ag = stock.Ag + Math.Abs(order.Ag);
            stock.Pt = stock.Pt + Math.Abs(order.Pt);
            stock.Pd = stock.Pd + Math.Abs(order.Pd);


            stock.AuAmount = stock.AuAmount + Math.Abs(order.Au);
            stock.AgAmount = stock.AgAmount + Math.Abs(order.Ag);
            stock.PtAmount = stock.PtAmount + Math.Abs(order.Pt);
            stock.PdAmount = stock.PdAmount + Math.Abs(order.Pd);


            stock.AuTotal = stock.AuTotal + Math.Abs(order.Au) * userEntity.AuPrice;
            stock.AgTotal = stock.AgTotal + Math.Abs(order.Ag) * userEntity.AgPrice;
            stock.PtTotal = stock.PtTotal + Math.Abs(order.Pt) * userEntity.PtPrice;
            stock.PdTotal = stock.PdTotal + Math.Abs(order.Pd) * userEntity.PdPrice;


            if (stock.AuAmount > 0)
            {
                stock.AuPrice = stock.AuTotal / stock.AuAmount;
            }
            if (stock.AgAmount > 0)
            {
                stock.AgPrice = stock.AgTotal / stock.AgAmount;
            }
            if (stock.PtAmount > 0)
            {
                stock.PtPrice = stock.PtTotal / stock.PtAmount;
            }
            if (stock.PdAmount > 0)
            {
                stock.PdPrice = stock.PdTotal / stock.PdAmount;
            }

            YicelTransaction tran = new YicelTransaction();
            try
            {
                tran.BeginTransaction();
                CreateOrderOperation(operationEntity, tran);//创建订单操作记录
                deliverList.ForEach(m => UpdateDeliver(m, entity, operationEntity.OperationId, tran));//更新交割单信息
                deliverRecordList.ForEach(m => CreateDeliverRecord(m, tran));//创建订单对应交割单信息                  
                UpdateUserTotal(entity, stockEntity, tran);//更新用户库存数量

                CreateAgent(agentDeliverEntity, tran);//创建金商库存
                dList.ForEach(m => CreateDeliver(m, tran));//创建绑定帐号交割单
                UpdateAgentTotal(stock, tran);    //更新金商绑定用户库存信息 

                UpdateColumns(entity.OrderId, updateList, tran);
                UpdateOrderState(entity.OrderId, agentId, userId, operationEntity.Account, tran);

                CreateLog(userLogEntity, tran);
                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                return false;
            }
        }

        //public static int CreateDeliverAgent(DeliverEntity entity, YicelTransaction tran)
        //{
        //    SqlParameter[] paras = new SqlParameter[] { 
        //        new SqlParameter("@OrderNo", entity.DeliverNo),
        //        new SqlParameter("@Account", entity.Account),
        //        new SqlParameter("@UserID", entity.UserID),
        //        new SqlParameter("@Price", entity.LockPrice),              
        //        new SqlParameter("@Goodst", entity.Goods),
        //        new SqlParameter("@Quantity", entity.Total),   
        //        new SqlParameter("@Direction", entity.Direction),   
        //        new SqlParameter("@OperUserID", entity.OperationUserID)
        //        };
        //    int result;
        //    return DbHelper.RunProcedure("P_CreateDeliverAgent", paras, out result);
        //}
        /// <summary>
        /// 更新金商绑定用户库存信息
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="tran"></param>
        public static void UpdateAgentTotal(StockEntity stock, YicelTransaction tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"update Stock_BZJ SET Au={0},Ag={1},Pt ={2},Pd={3},AuPrice={4},AgPrice={5},PtPrice={6},PdPrice={7},AuTotal={8},AgTotal={9},PtTotal={10},PdTotal={11},AuAmount={12},AgAmount={13},PtAmount={14},PdAmount={15}",
            stock.Au, stock.Ag, stock.Pt, stock.Pd, stock.AuPrice, stock.AgPrice,
            stock.PtPrice, stock.PdPrice, stock.AuTotal, stock.AgTotal, stock.PtTotal, stock.PdTotal, stock.AuAmount,
            stock.AgAmount, stock.PtAmount, stock.PdAmount);
            strSql.Append("where UserId=@UserId");
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@UserId",stock.UserId)
            };
            DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
        }

        /// <summary>
        /// 创建交割单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int CreateDeliver(DeliverEntity entity, YicelTransaction tran)
        {
            entity.DeliverDate = DateTime.Now.ToString();
            entity.CreateDate = entity.DeliverDate;
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@DeliverId",entity.DeliverId),
                new SqlParameter("@DeliverNo",entity.DeliverNo),
                new SqlParameter("@Goods",entity.Goods),
                new SqlParameter("@Direction",entity.Direction),
                new SqlParameter("@Total",entity.Total),
                new SqlParameter("@DeliverDate",entity.DeliverDate),
                new SqlParameter("@LockPrice",entity.LockPrice),
                new SqlParameter("@State",entity.State),
                new SqlParameter("@AvailableTotal",entity.AvailableTotal),
                new SqlParameter("@CreateDate",entity.CreateDate),
                new SqlParameter("@FromFlag",entity.FromFlag),
                new SqlParameter("@OperationUserID",entity.OperationUserID),
                new SqlParameter("@UserID",entity.UserID),
               new SqlParameter("@Account",entity.Account) 
            };
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("insert into Deliver_BZJ({0})", Fields.Deliver_FIELD_LIST);
            strSql.AppendFormat(" values ({0})", "@" + Fields.Deliver_FIELD_LIST.Replace(",", ",@"));
            object obj = DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);

        }


     /// <summary>
        /// 更新订单状态
     /// </summary>
     /// <param name="orderid"></param>
     /// <param name="agentId"></param>
     /// <param name="userId"></param>
     /// <param name="clerkId"></param>
     /// <param name="tran"></param>
     /// <returns></returns>
        public static int UpdateOrderState(string orderid, string agentId, string userId, string clerkId, YicelTransaction tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("UPDATE dbo.Order_BZJ SET State=2 ,AgentUserID='{1}' ,AgentID='{2}',ClerkID='{3}' ,OperationDate=GETDATE() where OrderId='{0} '", orderid, agentId, agentId, clerkId);

            object obj = DbHelper.ExecuteNonQuery(strSql.ToString(), null, tran.Transaction);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 后台用户库存入库(库存减少用的)
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="entity"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public static bool CreateStockDeliverAdmin(string operationId, OrderEntity entity, string loginId)
        {
            List<DeliverEntity> deliverList = new List<DeliverEntity>();//需要更新的交割单列表
            List<DeliverRecordEntity> deliverRecordList = new List<DeliverRecordEntity>();
            List<UpdateDataMember> updateList = new List<UpdateDataMember>();//需要更新的订单信息
            List<UpdateDataMember> userList = new List<UpdateDataMember>();//需要更新用户数量

            StockEntity stockEntity = LoadByAccount(entity.UserId);//获取订单所属用户信息
            entity.OrderType = 13;//默认提货单
            Direction d = Direction.库存调整;


            //获取交割单信息
            List<DeliverEntity> list = GetListByAccount(entity.Account, d, 1);
            if (list == null || list.Count <= 0)
                return false;

            #region 计算交割单信息

            Dictionary<GoodsType, decimal> dic = new Dictionary<GoodsType, decimal>();
            dic.Add(GoodsType.Au, entity.Au);
            dic.Add(GoodsType.Ag, entity.Ag);
            dic.Add(GoodsType.Pd, entity.Pd);
            dic.Add(GoodsType.Pt, entity.Pt);
            foreach (var key in dic.Keys)
            {
                var tmp = list.Where(m => m.Goods.Equals((int)key)).ToList();
                var values = dic[key];
                if (values >= 0) continue;
                foreach (var mm in tmp)
                {
                    if (values > mm.AvailableTotal)
                    {
                        values -= mm.AvailableTotal;
                        mm.AvailableTotal = 0;
                        mm.State = 0;
                        deliverList.Add(mm);
                    }
                    else
                    {
                        mm.State = values.Equals(mm.AvailableTotal) ? 0 : 1;
                        mm.AvailableTotal = mm.AvailableTotal + values;
                        if (mm.AvailableTotal == 0)
                        {
                            mm.State = 0;
                        }
                        deliverList.Add(mm);
                        break;
                    }
                }
            }

            #endregion

            List<DeliverAdjustmentEntity> listDeliver = new List<DeliverAdjustmentEntity>();

            DeliverAdjustmentEntity deliver = null;
            if (entity.Au < 0)
            {
                deliver = new DeliverAdjustmentEntity();
                deliver.DeliverAdjustmentID = Guid.NewGuid().ToString("n");
                deliver.DeliverAdjustmentNO = "JS" + DateTime.Now.ToString("yyMMddhhmmssffff");
                deliver.Goods = (int)GoodsType.Au;
                deliver.Direction = (int)OrderType.库存调整;
                deliver.Total = entity.Au;
                deliver.Account = entity.Account;
                deliver.LockPrice = 0;
                deliver.State = 0;
                deliver.AvailableTotal = 0;
                deliver.FromFlag = 1;
                deliver.OperationUserID = operationId;
                deliver.UserID = entity.UserId;
                listDeliver.Add(deliver);
            }
            if (entity.Ag < 0)
            {
                deliver = new DeliverAdjustmentEntity();
                deliver.DeliverAdjustmentID = Guid.NewGuid().ToString("n");
                deliver.DeliverAdjustmentNO = "JS" + DateTime.Now.ToString("yyMMddhhmmssffff");
                deliver.Goods = (int)GoodsType.Ag;
                deliver.Direction = (int)OrderType.库存调整;
                deliver.Total = entity.Ag;
                deliver.Account = entity.Account;
                deliver.LockPrice = 0;
                deliver.State = 0;
                deliver.AvailableTotal = 0;
                deliver.FromFlag = 1;
                deliver.OperationUserID = operationId;
                deliver.UserID = entity.UserId;
                listDeliver.Add(deliver);
            }
            if (entity.Pd < 0)
            {
                deliver = new DeliverAdjustmentEntity();
                deliver.DeliverAdjustmentID = Guid.NewGuid().ToString("n");
                deliver.DeliverAdjustmentNO = "JS" + DateTime.Now.ToString("yyMMddhhmmssffff");
                deliver.Goods = (int)GoodsType.Pd;
                deliver.Direction = (int)OrderType.库存调整;
                deliver.Total = entity.Pd;
                deliver.Account = entity.Account;
                deliver.LockPrice = 0;
                deliver.State = 0;
                deliver.AvailableTotal = 0;
                deliver.FromFlag = 1;
                deliver.OperationUserID = operationId;
                deliver.UserID = entity.UserId;
                listDeliver.Add(deliver);
            }
            if (entity.Pt < 0)
            {
                deliver = new DeliverAdjustmentEntity();
                deliver.DeliverAdjustmentID = Guid.NewGuid().ToString("n");
                deliver.DeliverAdjustmentNO = "JS" + DateTime.Now.ToString("yyMMddhhmmssffff");
                deliver.Goods = (int)GoodsType.Pt;
                deliver.Direction = (int)OrderType.库存调整;
                deliver.Total = entity.Pt;
                deliver.Account = entity.Account;
                deliver.LockPrice = 0;
                deliver.State = 0;
                deliver.AvailableTotal = 0;
                deliver.FromFlag = 1;
                deliver.OperationUserID = operationId;
                deliver.UserID = entity.UserId;
                listDeliver.Add(deliver);
            }


            UserLogEntity userLogEntity = new UserLogEntity();


            userLogEntity.Account = operationId;
            userLogEntity.DESC = string.Format(@"后台用户库存调整（减少)：{0}库存，减少.Ag:{1},Au:{2},Pt:{3},Pd:{4};", entity.Account, entity.Au, entity.Ag, entity.Pt, entity.Pd);
            userLogEntity.UserType = (int)LogType.用户库存调整减少;
            YicelTransaction tran = new YicelTransaction();
            try
            {
                tran.BeginTransaction();
                //  CreateOrder(order, tran);//创建订单信息
                deliverList.ForEach(m => UpdateDeliver(m, entity, operationId, tran));//更新交割单信息    
                listDeliver.ForEach(m => CreateDeliverAdjustment(m, tran));
                UpdateUserTotal(entity, stockEntity, tran);//更新用户库存数量
                CreateLog(userLogEntity, tran);
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                ManagerLog.WriteErr(ex + ":" + userLogEntity.DESC);
                tran.Rollback();
                return false;
            }
        }
        /// <summary>
        /// 金商库存修改减少
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int CreateDeliverAdjustment(DeliverAdjustmentEntity entity, YicelTransaction tran)
        {
            entity.DeliverDate = DateTime.Now.ToString();
            entity.CreateDate = entity.DeliverDate;
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@DeliverAdjustmentId",entity.DeliverAdjustmentID),
                new SqlParameter("@DeliverAdjustmentNo",entity.DeliverAdjustmentNO),
                new SqlParameter("@Goods",entity.Goods),
                new SqlParameter("@Direction",entity.Direction),
                new SqlParameter("@Total",entity.Total),
                new SqlParameter("@DeliverDate",entity.DeliverDate),
                new SqlParameter("@LockPrice",entity.LockPrice),
                new SqlParameter("@State",entity.State),
                new SqlParameter("@AvailableTotal",entity.AvailableTotal),
                new SqlParameter("@CreateDate",entity.CreateDate),
                new SqlParameter("@FromFlag",entity.FromFlag),
                new SqlParameter("@OperationUserID",entity.OperationUserID),
                new SqlParameter("@UserID",entity.UserID),
               new SqlParameter("@Account",entity.Account) 
            };
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("insert into DeliverAdjustment_BZJ({0})", Fields.DeliverAdjustment_List);
            strSql.AppendFormat(" values ({0})", "@" + Fields.DeliverAdjustment_List.Replace(",", ",@"));
            object obj = DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);

        }
        /// <summary>
        /// 金商库存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static bool CreateAgent(AgentDeliverEntity entity, YicelTransaction tran)
        {
            entity.AgentDeliverId = Guid.NewGuid().ToString("n");
            entity.CreateDate = DateTime.Now.ToString();
            entity.State = 1;
            entity.AvailableAg = entity.Ag;
            entity.AvailableAu = entity.Au;
            entity.AvailablePd = entity.Pd;
            entity.AvailablePt = entity.Pt;
            UpdateAgentTotal(entity.AgentInfoId, entity.Direction, GoodsType.Au, entity.Au, tran);
            UpdateAgentTotal(entity.AgentInfoId, entity.Direction, GoodsType.Ag, entity.Ag, tran);
            UpdateAgentTotal(entity.AgentInfoId, entity.Direction, GoodsType.Pt, entity.Pt, tran);
            UpdateAgentTotal(entity.AgentInfoId, entity.Direction, GoodsType.Pd, entity.Pd, tran);
            CreateAgentDeliver(entity, tran);
            return true;

        }

        /// <summary>
        /// 金商交割单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int CreateAgentDeliver(AgentDeliverEntity entity, YicelTransaction tran)
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@AgentDeliverId", entity.AgentDeliverId),
                new SqlParameter("@AgentInfoId", entity.AgentInfoId),
                new SqlParameter("@FromTo", entity.FromTo),
                new SqlParameter("@OrderId", entity.OrderId),
                new SqlParameter("@Direction", (int)entity.Direction),
                new SqlParameter("@Au", entity.Au),
                new SqlParameter("@Ag", entity.Ag),
                new SqlParameter("@Pt", entity.Pt),
                new SqlParameter("@Pd", entity.Pd),
                new SqlParameter("@AvailableAu", entity.AvailableAu),
                new SqlParameter("@AvailableAg", entity.AvailableAg),
                new SqlParameter("@AvailablePt", entity.AvailablePt),
                new SqlParameter("@AvailablePd", entity.AvailablePd),
                new SqlParameter("@CreateDate", entity.CreateDate),
                new SqlParameter("@State", entity.State) 
            };
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("insert into AgentDeliver_BZJ({0})", Fields.Agent_FIELD_LIST);
            strSql.AppendFormat(" values ({0})", "@" + Fields.Agent_FIELD_LIST.Replace(",", ",@"));
            object obj = DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }




        /// <summary>
        /// 更新用户库存数量
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="d">提货 卖</param>
        /// <param name="type">物品类型</param>
        /// <param name="total">数量</param>
        /// <param name="tran"></param>
        public static void UpdateAgentTotal(string uid, Direction d, GoodsType type, decimal total, YicelTransaction tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Trade_Agent set ");
            #region

            if (d == Direction.提货单)
            {
                switch (type)
                {
                    case GoodsType.Au:
                        strSql.Append("Au=Au+@Total ");
                        break;
                    case GoodsType.Ag:
                        strSql.Append("Ag=Ag+@Total ");
                        break;
                    case GoodsType.Pt:
                        strSql.Append("Pt=Pt+@Total ");
                        break;
                    case GoodsType.Pd:
                        strSql.Append("Pd=Pd+@Total ");
                        break;
                }
            }
            else if (d == Direction.卖单)
            {
                switch (type)
                {
                    case GoodsType.Au:
                        strSql.Append("Au_b=Au_b+@Total ");
                        break;
                    case GoodsType.Ag:
                        strSql.Append("Ag_b=Ag_b+@Total ");
                        break;
                    case GoodsType.Pt:
                        strSql.Append("Pt_b=Pt_b+@Total ");
                        break;
                    case GoodsType.Pd:
                        strSql.Append("Pd_b=Pd_b+@Total ");
                        break;
                }
            }
            else if (d == Direction.金店库存同步)
            {
                switch (type)
                {
                    case GoodsType.Au:
                        strSql.Append("Au=Au-@Total ");
                        break;
                    case GoodsType.Ag:
                        strSql.Append("Ag=ag-@Total ");
                        break;
                    case GoodsType.Pt:
                        strSql.Append("Pt=pt-@Total ");
                        break;
                    case GoodsType.Pd:
                        strSql.Append("Pd=pd-@Total ");
                        break;
                }
            }
            #endregion
            strSql.Append("where AgentId=@AgentId");
            SqlParameter[] paras = new SqlParameter[] 
            { 
            new SqlParameter("@AgentId", uid),
            new SqlParameter("@Total", total)
            };
            DbHelper.ExecuteNonQuery(strSql.ToString(), paras, tran.Transaction);
        }
        /// <summary>
        /// 更新订单状态为已提货或卖
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filter"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int UpdateColumns(string id, List<UpdateDataMember> filter, YicelTransaction tran)
        {
            Dictionary<string, object> parms;
            return DbHelper.ExecuteNonQuery(UpdateColumns("Order_BZJ", "OrderId", id, filter, out parms), parms);
        }

        /// <summary>
        /// 根据提货、卖号加载订单信息
        /// </summary>
        /// <param name="orderCode">提货、卖号</param>
        /// <returns></returns>
        public static OrderEntity LoadByOrderCode(string orderCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append("from vOrder_BZJ ");
            strSql.Append("where OrderCode=@OrderCode and State=1 and (OrderType=1 or OrderType=2)");
            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("@OrderCode", orderCode);
            using (DbDataReader reader = DbHelper.ExecuteReader(strSql.ToString(), parm))
            {
                if (reader.Read())
                {
                    OrderEntity entity = CreateEntity(reader);
                    entity.UserType = Convert.ToInt32(reader["UserType"]);
                    entity.IDNo = reader["IDNo"].ToString();
                    entity.IDType = Convert.ToInt32(reader["IDType"]);
                    entity.Name = reader["Name"].ToString();
                    entity.Phone = reader["Phone"].ToString();
                    entity.Remark = reader["Remark"].ToString();
                    entity.OrderType = (int)Convert.ToInt32(reader["OrderType"].ToString());
                    return entity;
                }
                else
                {
                    return null;
                }
                reader.Close();
                reader.Dispose();
            }
        }
        private static OrderEntity CreateEntity(DbDataReader reader)
        {
            OrderEntity entity = new OrderEntity();
            entity.OrderId = reader["OrderId"].ToString();
            entity.OrderNo = reader["OrderNo"].ToString();
            entity.OrderCode = reader["OrderCode"].ToString();
            entity.OrderType = (int)Convert.ToInt32(reader["OrderType"]);
            entity.UserId = reader["UserId"].ToString();
            entity.Account = reader["Account"].ToString();
            entity.JUserId = reader["JUserId"].ToString();
            entity.CarryWay = Convert.ToInt32(reader["CarryWay"]);
            entity.Au = Convert.ToDecimal(reader["Au"]);
            entity.Ag = Convert.ToDecimal(reader["Ag"]);
            entity.Pt = Convert.ToDecimal(reader["Pt"]);
            entity.Pd = Convert.ToDecimal(reader["Pd"]);
            entity.CreateDate = reader["CreateDate"].ToString();
            entity.EndDate = reader["EndDate"].ToString();
            entity.State = Convert.ToInt32(reader["State"]);
            //entity.TmpId = Convert.ToInt32(reader["TmpId"]);
            entity.Version = Convert.ToInt32(reader["Version"]);

            entity.AuQuantity = Math.Abs(Convert.ToDecimal(reader["AuQuantity"]));
            entity.AgQuantity = Math.Abs(Convert.ToDecimal(reader["AgQuantity"]));
            entity.PtQuantity = Math.Abs(Convert.ToDecimal(reader["PtQuantity"]));
            entity.PdQuantity = Math.Abs(Convert.ToDecimal(reader["PdQuantity"]));

            entity.AgP = Convert.ToDecimal(reader["AgP"]);
            entity.AuP = Convert.ToDecimal(reader["AuP"]);
            entity.PdP = Convert.ToDecimal(reader["PdP"]);
            entity.PtP = Convert.ToDecimal(reader["PtP"]);
            return entity;
        }
        /// <summary>
        /// 更新表的指定列，通常用于在列表中直接更新
        /// 而不是整页
        /// </summary>
        /// <param name="table">表名称</param>
        /// <param name="pk">主键名称</param>
        /// <param name="id">更新的ID</param>
        /// <param name="filter">更新的参数</param>
        /// <param name="filterParms">SQL参数</param>
        /// <returns>更新SQL语句</returns>
        public static string UpdateColumns(string table, string pk, string id, List<UpdateDataMember> filter, out Dictionary<string, object> filterParms)
        {
            StringBuilder sb = new StringBuilder();
            filterParms = new Dictionary<string, object>();
            sb.AppendFormat("update {0} set", table);
            int i = 0;
            foreach (var m in filter)
            {
                if (i != 0)
                    sb.AppendFormat(",{0}=@{0}", m.field);
                else
                    sb.AppendFormat(" {0}=@{0} ", m.field);
                filterParms.Add(string.Format("@{0}", m.field), m.value);
                i++;
            }
            sb.AppendFormat(" where {0}=@{0}", pk);
            filterParms.Add(string.Format("@{0}", pk), id);
            return sb.ToString();
        }
        /// <summary>
        /// 金商用户信息
        /// </summary>
        /// <param name="agentUserID"></param>
        /// <returns></returns>
        public static AgentInfoEntity LoadAgentUser(string agentUserID)
        {
            string strSql = string.Format(@"select * from Trade_Agent where AgentId='{0}'", agentUserID);
            DataTable dt = DbHelper.ExecQuery(strSql);
            if (dt.Rows.Count > 0)
            {
                AgentInfoEntity agentInfoEntity = CreateAgentInfoEntity(dt);
                return agentInfoEntity;
            }
            return null;
        }
        private static AgentInfoEntity CreateAgentInfoEntity(DataTable dt)
        {
            AgentInfoEntity entity = new AgentInfoEntity();
            entity.AgentInfoId = dt.Rows[0]["AgentId"].ToString();
            entity.AgentCode = dt.Rows[0]["AgentCode"].ToString();
            entity.AgentName = dt.Rows[0]["comname"].ToString();
            entity.Contact = dt.Rows[0]["coperson"].ToString();
            entity.AgentType = System.DBNull.Value != dt.Rows[0]["AgentType"] ? Convert.ToInt32(dt.Rows[0]["AgentType"].ToString()) : 0;  // Convert.ToInt32(dt.Rows[0]["AgentType"]);

            entity.Au = Convert.ToDecimal(dt.Rows[0]["Au"]);
            entity.Ag = Convert.ToDecimal(dt.Rows[0]["Ag"]);
            entity.Pt = Convert.ToDecimal(dt.Rows[0]["Pt"]);
            entity.Pd = Convert.ToDecimal(dt.Rows[0]["Pd"]);
            entity.Au_b = Convert.ToDecimal(dt.Rows[0]["Au_b"]);
            entity.Ag_b = Convert.ToDecimal(dt.Rows[0]["Ag_b"]);
            entity.Pt_b = Convert.ToDecimal(dt.Rows[0]["Pt_b"]);
            entity.Pd_b = Convert.ToDecimal(dt.Rows[0]["Pd_b"]);
            entity.Tel = dt.Rows[0]["phoneNum"].ToString();
            entity.Address = dt.Rows[0]["address"].ToString();
            entity.CreateDate = dt.Rows[0]["AddTime"].ToString();
            entity.IsEnable = Convert.ToInt32(dt.Rows[0]["Enable"]);
            entity.UserID = dt.Rows[0]["UserID"].ToString();
            return entity;
        }
        /// <summary>
        /// 金商提货受理明细
        /// </summary>
        /// <param name="agentID"></param>
        /// <param name="order"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static DataTable GetAgentOrderList(string agentID, OrderEntity order, int pageindex, int pagesize, ref int page)
        {
            SqlParameter[] paras = null;
            StringBuilder sb = new StringBuilder();
            StringBuilder sbSearch = new StringBuilder();

            sbSearch.AppendFormat(@" State=2 and AgentInfoId='{0}'", agentID);

            if (!string.IsNullOrEmpty(order.OrderCode))
            {
                sbSearch.AppendFormat(@" and OrderCode='{0}'", order.OrderCode);
            }
            if (!string.IsNullOrEmpty(order.Account))
            {
                sbSearch.AppendFormat(@" and Account like '{0}%'", order.Account);
            }
            if (!string.IsNullOrEmpty(order.Name))
            {
                sbSearch.AppendFormat(@" and userName like '{0}%'", order.Name);
            }

            if (!string.IsNullOrEmpty(order.OrderNo))
            {
                sbSearch.AppendFormat(@" and OrderNo = '{0}'", order.OrderNo);
            }

            if (!string.IsNullOrEmpty(order.CreateDate))
            {
                sbSearch.AppendFormat(@" and CreateDate BETWEEN '{0}' AND '{1}'", order.CreateDate, order.EndDate);
            }

            paras = new SqlParameter[] 
            { 
                new SqlParameter("@selectlist", "AgentInfoId,OrderId, OrderCode, OrderNo, OrderType, UserId, Account, CarryWay, Au, Ag, Pt, Pd, CreateDate, AvailableAu, AvailableAg, AvailablePt,AvailablePd, userName,AgQuantity,AuQuantity,PtQuantity,PdQuantity,AuP,AgP,PdP,PtP,OperationDate,tradeAccount,AgentId,ClerkId"),
                new SqlParameter("@SubSelectList", "AgentInfoId,OrderId, OrderCode, OrderNo, OrderType, UserId, Account, CarryWay, Au, Ag, Pt, Pd, CreateDate, AvailableAu, AvailableAg, AvailablePt,AvailablePd, userName,AgQuantity,AuQuantity,PtQuantity,PdQuantity,AuP,AgP,PdP,PtP,OperationDate,tradeAccount,AgentId,ClerkId"),
                new SqlParameter("@TableSource", "V_Agent"),
                new SqlParameter("@TableOrder", "a"),
                new SqlParameter("@SearchCondition",sbSearch.ToString()),
                new SqlParameter("@OrderExpression", "order by CreateDate desc"),             
                new SqlParameter("@PageIndex", pageindex),
                new SqlParameter("@PageSize", pagesize),
                new SqlParameter("@PageCount", page)
            };

            paras[8].Direction = ParameterDirection.Output;
            DataTable dt = DbHelper.RunProcedure("GetRecordFromPage", paras, "V_Agent").Tables[0];
            page = Convert.ToInt32(paras[8].Value);
            return dt;
        }
        #endregion

        /// <summary>
        /// 验证码生成
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="verifyCode">验证码</param>
        /// <param name="orderType">订单类别</param>
        /// <returns></returns>
        public static int CreateVerifyCode(string userID, string verifyCode, int orderType)
        {
            StringBuilder sb = new StringBuilder(50);
            sb.AppendFormat(@"INSERT INTO VerifyCode(VerifyCodeID,VerifyCode,UserID,VerifyState,CreateTime,EndTime,OrderType)  VALUES (REPLACE(NEWID() ,'-' ,'') ,'{0}','{1}',1,GETDATE(),GETDATE()+7,'{2}')", verifyCode, userID, orderType);
            return DbHelper.ExecuteNonQuery(sb.ToString());
        }
        /// <summary>
        /// 回购验证码检查
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="verifyCode"></param>
        /// <param name="OrderType"></param>
        /// <returns></returns>
        public static bool CheckVerifyCode(string userID, string verifyCode, int OrderType)
        {
            DataTable dt = new DataTable();
            string strSql = string.Format(@" SELECT *    FROM   dbo.VerifyCode      WHERE  VerifyCode = '{0}' AND UserID = '{1}' AND VerifyState = 1 AND EndTime - GETDATE() >= 0 and OrderType={2}", verifyCode, userID, OrderType);
            dt = DbHelper.ExecQuery(strSql);
            if (dt.Rows.Count > 0)
            {
                SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@UserID", userID), new SqlParameter("@VerifyCode", verifyCode) };
                DbHelper.RunProcedure("P_CheckVerifyCode", paras, "VerifyCode"); //更新状态
                return true;
            }
            return false;
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int CreateLog(UserLogEntity entity, YicelTransaction tran)
        {
            entity.OperTime = DateTime.Now.ToString();
            SqlParameter[] parms = new SqlParameter[] {
                 new SqlParameter("@OperTime",entity.OperTime),
                new SqlParameter("@Account",entity.Account),
                new SqlParameter("@Remark",entity.DESC),
                new SqlParameter("@UserType",entity.UserType)
            };
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("insert into Base_OperrationLog({0})", Fields.Log_FIELD_List);
            strSql.AppendFormat(" values ({0})", "@" + Fields.Log_FIELD_List.Replace(",", ",@"));
            object obj = DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }


        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ipmac"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int CreateLogEx(UserLogEntity entity,string ipmac, YicelTransaction tran)
        {
            entity.OperTime = DateTime.Now.ToString();
            SqlParameter[] parms = new SqlParameter[] {
                 new SqlParameter("@OperTime",entity.OperTime),
                new SqlParameter("@Account",entity.Account),
                new SqlParameter("@Remark",ipmac+entity.DESC),
                new SqlParameter("@UserType",entity.UserType)
            };
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("insert into Base_OperrationLog({0})", Fields.Log_FIELD_List);
            strSql.AppendFormat(" values ({0})", "@" + Fields.Log_FIELD_List.Replace(",", ",@"));
            object obj = DbHelper.ExecuteNonQuery(strSql.ToString(), parms, tran.Transaction);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }
    }
}
