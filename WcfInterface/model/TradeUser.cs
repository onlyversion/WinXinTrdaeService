//*******************************************************************************
//  文 件 名：TradeUser.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/07/01
//
//  修改标识：
//  修改说明：
//*******************************************************************************
using System;

namespace WcfInterface.model
{
    /// <summary>
    /// 交易用户信息
    /// </summary>
    public class TradeUser
    {
        public string UserID
        {
            get;
            set;
        }
        public string CashUser
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 用户名称
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 用户状态
        /// 1 正常状态
        /// 0 禁用状态
        /// </summary>
        public string Status
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 账号类型 
        /// 0 个人用户
        /// 1 机构用户
        /// </summary>
        public string AccountType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 登陆账号
        /// </summary>
        public string Account
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 登陆秘密
        /// </summary>
        public string LoginPwd
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 资金秘密
        /// </summary>
        public string CashPwd
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 证件类型
        /// 1 身份证
        /// 2 营业执照
        /// </summary>
        public string CardType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 证件号码
        /// </summary>
        public string CardNum
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 所属组织ID
        /// </summary>
        public string OrgId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 手机号码
        /// </summary>
        public string PhoneNum
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 固定电话
        /// </summary>
        public string TelNum
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 邮箱
        /// </summary>
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 联系人
        /// </summary>
        public string LinkMan
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 联系地址
        /// </summary>
        public string LinkAdress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 性别 1男 0女
        /// </summary>
        public string Sex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开户人
        /// </summary>
        public string OpenMan
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开户时间
        /// </summary>
        public DateTime? OpenTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 最后更新时间
        /// </summary>
        public DateTime? LastUpdateTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 最后更新人(指哪个管理员修改了该用户基本信息)
        /// </summary>
        public string LastUpdateID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets ip地址
        /// </summary>
        public string Ip
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets mac地址
        /// </summary>
        public string Mac
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 最后登陆时间
        /// </summary>
        public DateTime? LastLoginTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// 是否在线
        /// </summary>
        public bool Online
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 
        /// 最小交易数量
        /// </summary>
        public double MinTrade
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 
        /// 下单单位 比如最小1手、0.1手 等
        /// </summary>
        public double OrderUnit 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 
        /// 最大交易数量
        /// </summary>
        public double MaxTrade
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// 是否允许入金
        /// </summary>
        public bool PermitRcash 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// 是否允许出金
        /// </summary>
        public bool PermitCcash
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// 是否启用买
        /// </summary>
        public bool PermitDhuo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// 是否启用卖
        /// </summary>
        public bool PermitHshou
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// 是否启用入库
        /// </summary>
        public bool PermitRstore
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// 是否启用平仓
        /// </summary>
        public bool PermitDelOrder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 法人姓名(机构或企业时必填)
        /// </summary>
        public string CorporationName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 下单电话 用户确认 用打电话下单时 是不是这个电话
        /// </summary>
        public string OrderPhone
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 子账号
        /// </summary>
        public string SubUser
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 摊位号
        /// </summary>
        public string TanUser
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 
        /// 0 代表未绑定银行状态
        /// 1 代表银行审核中
        /// 2 代表银行已经绑定成功
        /// 3 代表银行审核失败
        /// 4 已解约
        /// </summary>
        public string BankState
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 帐户余额
        /// </summary>
        public double Money
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 保证金
        /// </summary>
        public double OccMoney
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 预付款
        /// </summary>
        public double FrozenMoney
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets 冻结资金
        /// </summary>
        public double DongJieMoney
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 签约银行(1表示华夏银行2表示农业银行)
        /// </summary>
        public string ConBankType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开户银行
        /// </summary>
        public string OpenBank
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开户行银行行号
        /// </summary>
        public string BankAccount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开户行银行卡户名
        /// </summary>
        public string AccountName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开户行银行卡卡号
        /// </summary>
        public string BankCard
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开户行地址
        /// </summary>
        public string OpenBankAddress
        {
            get;
            set;
        }
        /// <summary>  
        /// 金商绑定状态
        /// </summary>
        public int IsEnable
        {
            get;
            set;
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UType
        {
            get;
            set;
        }

        /// <summary>
        /// 绑定的普通帐户
        /// </summary>
        public string BindAccount
        {
            get;
            set;
        }
        /// <summary>
        /// 所属组织名称
        /// </summary>
        public string OrgName
        {
            get;
            set;
        }

        /// <summary>
        /// 所属组织编码
        /// </summary>
        public string Telephone
        {
            get;
            set;
        }

        /// <summary>
        /// 组织负责人
        /// </summary>
        public string Reperson
        {
            get;
            set;
        }

        /// <summary>
        /// 是否经纪人
        /// </summary>
        public bool IsBroker
        {
            get;
            set;
        }

        /// <summary>
        /// 经纪人账户名
        /// </summary>
        public string PAccount
        {
            get;
            set;
        }

        /// <summary>
        /// 经纪人用户名
        /// </summary>
        public string PUserName
        {
            get;
            set;
        }

        /// <summary>
        /// 经纪人用户ID
        /// </summary>
        public string PUserId
        {
            get;
            set;
        }
    }
}
