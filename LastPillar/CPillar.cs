//*******************************************************************************
//  文 件 名：CPillar.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/07/01
//
//  修改标识：
//  修改说明：
//*******************************************************************************
using System;
using System.ServiceModel;
using System.Configuration;
using System.Collections.Generic;
using com.gss.common;
using WcfInterface;

using WcfInterface.ServiceReference1;
using WcfInterface.model;

namespace LastPillar
{
    /// <summary>
    /// 获取最后一根柱子的信息
    /// </summary>
    public partial class CPillar : IPillar
    {
        /// <summary>
        /// 获取最后一根柱子的信息
        /// </summary>
        /// <param name="pcode">商品行情编码</param>
        /// <param name="dtype">周期类型</param>
        /// <returns>获取最后一根柱子的信息(逗号隔开的行情数据)</returns>
        public string GetLastPillar(string pcode, datatype dtype)
        {
            string strtmp = String.Empty;
            TradeClient tc = new TradeClient();
            try
            {
                switch (dtype)
                {
                    case datatype.M1: strtmp = "M1";
                        break;
                    case datatype.M5: strtmp = "M5";
                        break;
                    case datatype.M15: strtmp = "M15";
                        break;
                    case datatype.M30: strtmp = "M30";
                        break;
                    case datatype.H1: strtmp = "H1";
                        break;
                    case datatype.H4: strtmp = "H4";
                        break;
                    case datatype.D1: strtmp = "D1";
                        break;
                    case datatype.W1: strtmp = "W1";
                        break;
                    case datatype.MN: strtmp = "MN";
                        break;
                    default:
                        break;
                }
                strtmp = tc.GetLastPillarData(strtmp, pcode);
                tc.Close();
            }
            catch (Exception ex)
            {
                tc.Abort();
                ComFunction.WriteErr(ex);
            }
            return strtmp;
        }

        /// <summary>
        /// 获取实时价
        /// </summary>
        /// <param name="pcode">商品行情编码</param>
        /// <returns>获取实际价格</returns>
        public double GetRealPrice(string pcode)
        {
            TradeClient tc = new TradeClient();
            double price = 0;
            try
            {
                price = tc.GetRealprice(pcode);
            }
            catch (Exception ex)
            {
                tc.Abort();
                ComFunction.WriteErr(ex);
            }
            return price;
        }

        /// <summary>
        /// 获取实时卖价和买价
        /// </summary>
        /// <returns>获取实际价格(黄金,白银,铂金,钯金)</returns>
        public ProductRealPrice GetProductRealPrice()
        {
            TradeClient tc = new TradeClient();
            ProductRealPrice PRealPrice = new ProductRealPrice();
            try
            {
                PRealPrice = ComFunction.GetProductRealPrice();
                PRealPrice.au_realprice = tc.GetRealprice("XAUUSD");
                PRealPrice.ag_realprice = tc.GetRealprice("XAGUSD");
                PRealPrice.pt_realprice = tc.GetRealprice("XPTUSD");
                PRealPrice.pd_realprice = tc.GetRealprice("XPDUSD");
                PRealPrice.agb_price = PRealPrice.ag_realprice - PRealPrice.agb_price;
                PRealPrice.aub_price = PRealPrice.au_realprice - PRealPrice.aub_price;
                PRealPrice.ptb_price = PRealPrice.pt_realprice - PRealPrice.ptb_price;
                PRealPrice.pdb_price = PRealPrice.pd_realprice - PRealPrice.pdb_price;
            }
            catch (Exception ex)
            {
                tc.Abort();
                ComFunction.WriteErr(ex);
            }
            return PRealPrice;
        }
    }
}
