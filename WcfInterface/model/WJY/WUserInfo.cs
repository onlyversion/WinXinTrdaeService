namespace WcfInterface.model.WJY
{
    /// <summary>
    /// 微交易用户信息
    /// </summary>
    public  class WUserInfo:ResultDesc
    {
        /// <summary>
        /// 微交易用户信息
        /// </summary>
        public Base_WUser WUser { get; set; }
        /// <summary>
        /// 交易账户信息
        /// </summary>
        public TradeUser TdUser { get; set; }

        public WUserInfo()
        {
            WUser = new Base_WUser();
            TdUser = new TradeUser();
        }
    }
}
