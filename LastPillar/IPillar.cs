//*******************************************************************************
//  文 件 名：IPillar.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/07/01
//
//  修改标识：
//  修改说明：
//*******************************************************************************

using System.ServiceModel;
using com.gss.common;
using System.Collections.Generic;
using WcfInterface.model;

namespace LastPillar
{
    /// <summary>
    /// 接口
    /// </summary>
    [ServiceContract(Name = "IPillar", Namespace = "www.IPillar.com")]
    public partial interface IPillar
    {
        /// <summary>
        /// 获取最后一根柱子的信息
        /// </summary>
        /// <param name="pcode">商品行情编码</param>
        /// <param name="dtype">周期类型</param>
        /// <returns>获取最后一根柱子的信息(逗号隔开的行情数据)</returns>
        [OperationContract]
        string GetLastPillar(string pcode, datatype dtype);

        /// <summary>
        /// 获取实时价
        /// </summary>
        /// <param name="pcode">商品行情编码</param>
        /// <returns>获取实际价格</returns>
        [OperationContract]
        double GetRealPrice(string pcode);

        /// <summary>
        /// 获取实时价
        /// </summary>
        /// <returns>获取实际价格(黄金,白银,铂金,钯金)</returns>
        [OperationContract]
        ProductRealPrice GetProductRealPrice();
    }
}
