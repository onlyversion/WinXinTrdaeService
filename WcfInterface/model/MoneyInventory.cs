//*******************************************************************************
//  文 件 名：MoneyInventory.cs
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
    /// 资金库存查询接口 返回类型
    /// </summary>
    public class MoneyInventory
    {

        /// <summary>
        /// Gets or sets a value indicating whether 结果(true成功 false失败)
        /// </summary>
        public bool Result
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 描述
        /// </summary>
        public string Desc
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets 返回代码
        /// </summary>
        public string ReturnCode
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets 库存
        /// </summary>
        public Storagequantity StorageQuantity
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 用户资金信息
        /// </summary>
        public Fundinfo FdInfo
        {
            set;
            get;
        }
        
    }
}
