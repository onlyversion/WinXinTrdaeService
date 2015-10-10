//*******************************************************************************
//  文 件 名：ProductConfig.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/07/01
//
//  修改标识：
//  修改说明：
//*******************************************************************************

namespace WcfInterface.model
{
    /// <summary>
    /// 商品配置接口 返回类型
    /// </summary>
    public class ProductConfig
    {

       /// <summary>
        /// Gets or sets 卖单工费计算公式(未用)
       /// </summary>
        public string SellFee
        {
            get;
            set;
        }

       /// <summary>
        /// Gets or sets 买单仓储费计算公式
       /// </summary>
        public string BuyStorageFee
        {
            get;
            set;
        }

       /// <summary>
        /// Gets or sets 卖单仓储费计算公式
       /// </summary>
        public string SellStorageFee
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 交易工费公式
        /// </summary>
        public string ExpressionFee
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 
        /// 单位数(相对于报价单位。
        /// 比如 100g 黄金 单位数 是100
        /// 5g黄金的单位数是 5)
        /// </summary>
        public double Unit
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 状态(0 正常交易, 1 只报价, 2 只买, 3 只卖, 4 全部禁止)
        /// </summary>
        public string State
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 允许交易的最大时间差
        /// </summary>
        public double MaxTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 允许最高价格
        /// </summary>
        public double MaxPrice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 允许最低价格
        /// </summary>
        public double MinPrice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 商品编码
        /// </summary>
        public string ProductCode
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 商品名称
        /// </summary>
        public string ProductName
        {
            set;
            get;
        }

       /// <summary>
        /// Gets or sets 行情编码
       /// </summary>
        public string PriceCode
        {
            set;
            get;

        }

       /// <summary>
        /// Gets or sets 原料编码
       /// </summary>
        public string GoodsCode
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 点差基值
        /// </summary>
        public double AdjustBase
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 点差基数
        /// </summary>
        public int AdjustCount
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 点差
        /// </summary>
        public int PriceDot
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 点值
        /// </summary>
        public double ValueDot
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 止损止盈点差
        /// </summary>
        public int SetBase
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 挂单价点差
        /// </summary>
        public int HoldBase
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 定金百分比(未用)
        /// </summary>
        public double OrderMoney
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 定金公式
        /// </summary>
        public string OrderMoneyFee
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 开始时间,时间存储格式为: HH:mm:ss
        /// </summary>
        public string Starttime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 结束时间,时间存储格式为: HH:mm:ss
        /// </summary>
        public string Endtime
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets 商品价格下浮值
        /// </summary>
        public double LowerPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 精确到秒的时间数字串
        /// </summary>
        public string weektime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 商品实时价格
        /// </summary>
        public double realprice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开盘
        /// </summary>
        public double OpenPrice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 最高
        /// </summary>
        public double HighPrice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 最低
        /// </summary>
        public double LowPrice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 昨收盘
        /// </summary>
        public double YesterdayPrice
        {
            get;
            set;
        }
    }
}
