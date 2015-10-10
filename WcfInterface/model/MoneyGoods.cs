//*******************************************************************************
//  文 件 名：MoneyGoods.cs
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
    /// 账户余额和库存变动记录
    /// </summary>
    public struct MoneyGoods
    {
        /// <summary>
        /// 帐户变动金额(输出)
        /// </summary>
        public double UserChangeMoney;//帐户变动金额

        /// <summary>
        /// 新的总库存(输出)
        /// </summary>
        public double StorageQuantity;//总库存

        /// <summary>
        /// 新的未办理业务的库存(输出)
        /// </summary>
        public double NoUseStorage;//未办理业务的库存

        /// <summary>
        /// 原来总的库存(输出)
        /// </summary>
        public double OldStorageQuantity;//原来总的库存

        /// <summary>
        /// 变动的库存(输出)
        /// </summary>
        public double ChangeStorage;//变动的库存

        /// <summary>
        /// 实时报价(输入)
        /// </summary>
        public double RealPrice;//实时报价

        /// <summary>
        /// 回购重量(输入)
        /// </summary>
        public double Quantity;//回购重量

        /// <summary>
        /// 原来总的库存(输入)
        /// </summary>
        public double Xusd;//原来总的库存

        /// <summary>
        /// 原来未办理业务的库存(输入)
        /// </summary>
        public double NXusd;//原来未办理业务的库存

        /// <summary>
        /// 下浮价(输入)
        /// </summary>
        public double LowerPrice;//下浮价
    }
}
