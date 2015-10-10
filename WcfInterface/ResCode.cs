using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface
{
    /// <summary>
    /// 响应结果代码
    /// </summary>
    public class ResCode
    {
        #region 返回代码
        /// <summary>
        /// 登陆成功
        /// </summary>
        public const string UL001 = "UL001";
        /// <summary>
        /// 登陆成功
        /// </summary>
        public const string UL001Desc = "登陆成功";

        /// <summary>
        /// 登陆失败 
        /// </summary>
        public const string UL002 = "UL002";
        /// <summary>
        /// 登陆失败
        /// </summary>
        public const string UL002Desc = "登陆失败";

        /// <summary>
        /// 登陆过期 
        /// </summary>
        public const string UL003 = "UL003";
        /// <summary>
        /// 登陆过期
        /// </summary>
        public const string UL003Desc = "登陆过期";

        /// <summary>
        /// 业务处理成功
        /// </summary>
        public const string UL004 = "UL004";

        /// <summary>
        /// 业务处理成功
        /// </summary>
        public const string UL004Desc = "业务处理成功";

        /// <summary>
        /// 业务处理失败
        /// </summary>
        public const string UL005 = "UL005";
        /// <summary>
        /// 业务处理失败
        /// </summary>
        public const string UL005Desc = "业务处理失败";

        /// <summary>
        /// 验证签名失败
        /// </summary>
        public const string UL006 = "UL006";
        /// <summary>
        /// 验证签名失败
        /// </summary>
        public const string UL006Desc = "验证签名失败";

        /// <summary>
        /// 订单不允许入库
        /// </summary>
        public const string UL007 = "UL007";        
        /// <summary>
        /// 订单不允许入库
        /// </summary>
        public const string UL007Desc = "订单不允许入库";

        /// <summary>
        /// 回收单入库，重量大于总重量
        /// </summary>
        public const string UL008 = "UL008";
        /// <summary>
        /// 回收单入库，重量大于总重量
        /// </summary>
        public const string UL008Desc = "回收单入库，重量大于总重量";


        /// <summary>
        /// 回收单入库，数量不等于实际数量
        /// </summary>
        public const string UL009 = "UL009";
        /// <summary>
        /// 回收单入库，数量不等于实际数量
        /// </summary>
        public const string UL009Desc = "回收单入库，数量不等于实际数量";

        /// <summary>
        /// 买涨单入库，数量大于剩余数量
        /// </summary>
        public const string UL010 = "UL010";
        /// <summary>
        /// 买涨单入库，数量大于剩余数量
        /// </summary>
        public const string UL010Desc = "买涨单入库，数量大于剩余数量";

        /// <summary>
        /// 平仓数量大于剩余数量
        /// </summary>
        public const string UL011 = "UL011";
        /// <summary>
        /// 平仓数量大于剩余数量
        /// </summary>
        public const string UL011Desc = "平仓数量大于剩余数量";

        /// <summary>
        /// 帐户余额不足
        /// </summary>
        public const string UL012 = "UL012";
        /// <summary>
        /// 帐户余额不足
        /// </summary>
        public const string UL012Desc = "帐户余额不足";

        /// <summary>
        /// 风险率过高
        /// </summary>
        public const string UL013 = "UL013";
        /// <summary>
        /// 风险率过高
        /// </summary>
        public const string UL013Desc = "风险率过高";

        /// <summary>
        /// 报价过期
        /// </summary>
        public const string UL014 = "UL014";
        /// <summary>
        /// 报价过期
        /// </summary>
        public const string UL014Desc = "报价过期";

        /// <summary>
        /// 报价不在服务器设定范围内
        /// </summary>
        public const string UL015 = "UL015";
        /// <summary>
        /// 报价不在服务器设定范围内
        /// </summary>
        public const string UL015Desc = "不在报价范围内!";

        /// <summary>
        /// 报价不在滑点范围内
        /// </summary>
        public const string UL016 = "UL016";
        /// <summary>
        /// 报价不在范围内
        /// </summary>
        public const string UL016Desc = "报价不在范围内";

        /// <summary>
        /// 帐户不允许买涨
        /// </summary> 
        public const string UL017 = "UL017";
        /// <summary>
        /// 帐户不允许买涨
        /// </summary>
        public const string UL017Desc = "帐户不允许买涨";

        /// <summary>
        /// 帐户不允许回收
        /// </summary>
        public const string UL018 = "UL018";
        /// <summary>
        /// 帐户不允许回收
        /// </summary>
        public const string UL018Desc = "帐户不允许回收";

        /// <summary>
        /// 帐户不允许平仓
        /// </summary>
        public const string UL019 = "UL019";
        /// <summary>
        /// 帐户不允许平仓
        /// </summary>
        public const string UL019Desc = "帐户不允许平仓";

        /// <summary>
        /// 帐户不允许入库
        /// </summary>
        public const string UL020 = "UL020";
        /// <summary>
        /// 帐户不允许入库
        /// </summary>
        public const string UL020Desc = "帐户不允许入库";

        /// <summary>
        /// 下单类型错误
        /// </summary>
        public const string UL021 = "UL021";
        /// <summary>
        /// 下单类型错误
        /// </summary>
        public const string UL021Desc = "下单类型错误";

        /// <summary>
        /// 服务器不允许交易
        /// </summary>
        public const string UL022 = "UL022";
        /// <summary>
        /// 服务器不允许交易
        /// </summary>
        public const string UL022Desc = "服务器不允许交易";

        /// <summary>
        /// 有效期设置错误
        /// </summary>
        public const string UL023 = "UL023";
        /// <summary>
        /// 有效期设置错误
        /// </summary>
        public const string UL023Desc = "有效期设置错误";

        /// <summary>
        /// 商品不存在
        /// </summary>
        public const string UL024 = "UL024";
        /// <summary>
        /// 商品不存在
        /// </summary>
        public const string UL024Desc = "商品不存在";

        /// <summary>
        /// 商品禁止交易
        /// </summary>
        public const string UL025 = "UL025";
        /// <summary>
        /// 商品禁止交易
        /// </summary>
        public const string UL025Desc = "商品禁止交易";

        /// <summary>
        /// 商品只允许买涨
        /// </summary>
        public const string UL026 = "UL026";
        /// <summary>
        /// 商品只允许买涨
        /// </summary>
        public const string UL026Desc = "商品只允许买涨";

        /// <summary>
        /// 商品只允许回收
        /// </summary>
        public const string UL027 = "UL027";
        /// <summary>
        /// 商品只允许回收
        /// </summary>
        public const string UL027Desc = "商品只允许回收";

        /// <summary>
        /// 订单不存在
        /// </summary>
        public const string UL028 = "UL028";
        /// <summary>
        /// 订单不存在
        /// </summary>
        public const string UL028Desc = "订单不存在";

        /// <summary>
        /// 注册信息不完整
        /// </summary>
        public const string UL029 = "UL029";
        /// <summary>
        /// 注册信息不完整
        /// </summary>
        public const string UL029Desc = "注册信息不完整";

        /// <summary>
        /// 账号已被占用
        /// </summary>
        public const string UL030 = "UL030";
        /// <summary>
        /// 账号已被占用
        /// </summary>
        public const string UL030Desc = "账号已被占用";

        /// <summary>
        /// 证件号已被占用
        /// </summary>
        public const string UL031 = "UL031";
        /// <summary>
        /// 证件号已被占用
        /// </summary>
        public const string UL031Desc = "证件号已被占用";

        /// <summary>
        /// 行情编码错误
        /// </summary>
        public const string UL032 = "UL032";
        /// <summary>
        /// 行情编码错误
        /// </summary>
        public const string UL032Desc = "行情编码错误";

        /// <summary>
        /// 周期编码错误
        /// </summary>
        public const string UL033 = "UL033";
        /// <summary>
        /// 周期编码错误
        /// </summary>
        public const string UL033Desc = "周期编码错误";

        /// <summary>
        /// 查询无记录
        /// </summary>
        public const string UL034 = "UL034";
        /// <summary>
        /// 查询无记录
        /// </summary>
        public const string UL034Desc = "查询无记录";

        /// <summary>
        /// 软件无更新
        /// </summary>
        public const string UL035 = "UL035";
        /// <summary>
        /// 软件无更新
        /// </summary>
        public const string UL035Desc = "软件无更新";

        /// <summary>
        /// 软件有更新
        /// </summary>
        public const string UL036 = "UL036";
        /// <summary>
        /// 软件有更新
        /// </summary>
        public const string UL036Desc = "软件有更新";

        /// <summary>
        /// OsType不正确
        /// </summary>
        public const string UL037 = "UL037";
        /// <summary>
        /// OsType不正确
        /// </summary>
        public const string UL037Desc = "OsType不正确";

        /// <summary>
        /// 不允许注册模拟账号
        /// </summary>
        public const string UL038 = "UL038";
        /// <summary>
        /// 不允许注册模拟账号
        /// </summary>
        public const string UL038Desc = "不允许注册模拟账号";

        /// <summary>
        /// 未能获取资金库存
        /// </summary>
        public const string UL039 = "UL039";
        /// <summary>
        /// 未能获取资金库存
        /// </summary>
        public const string UL039Desc = "未能获取资金库存";

        /// <summary>
        /// 数据库事务处理出错
        /// </summary>
        public const string UL040 = "UL040";
        /// <summary>
        /// 数据库事务处理出错
        /// </summary>
        public const string UL040Desc = "数据库事务处理出错";

        /// <summary>
        /// 操作失败
        /// </summary>
        public const string UL041 = "UL041";
        /// <summary>
        /// 操作失败
        /// </summary>
        public const string UL041Desc = "操作失败";

        /// <summary>
        /// 用户类型不正确
        /// </summary>
        public const string UL042 = "UL042";
        /// <summary>
        /// 用户类型不正确
        /// </summary>
        public const string UL042Desc = "用户类型不正确";


        /// <summary>
        /// 操作成功
        /// </summary>
        public const string UL043 = "UL043";
        /// <summary>
        /// 操作成功
        /// </summary>
        public const string UL043Desc = "操作成功";

        /// <summary>
        /// 交易手数必须是手数单位的倍数，并且不能为0
        /// </summary>
        public const string UL044 = "UL044";

        /// <summary>
        /// 交易手数必须是手数单位的倍数，并且不能为0
        /// </summary>
        public const string UL044Desc = "交易手数必须是手数单位的倍数，并且不能为0";

        #endregion
    }
}
