//*******************************************************************************
//  文 件 名：TradeInventoryChange.cs
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
    /// 库存变动查询 返回类型
    /// </summary>
    public class TradeInventoryChange
    {

        /// <summary>
        /// Gets or sets 材料编码
        /// </summary>
        public string GoodsCode
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 变动前库存
        /// </summary>
        public double OldStorage
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 变动后库存
        /// </summary>
        public double NewStorage
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 变动值
        /// </summary>
        public double ChangeValue
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 变动时间
        /// </summary>
        public DateTime ChangTime
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 变动原因
        /// </summary>
        public string ChangeReason
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 关联单号
        /// </summary>
        public string RelaID
        {
            set;
            get;
        }
    }
}
