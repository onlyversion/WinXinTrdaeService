using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 系统报价
    /// </summary>   
    public class PriceEntity : EntityBase
    {
        #region 表字段

        private string _priceId;
        /// <summary>
        /// 价格编号GUID
        /// </summary>
        public string PriceId
        {
            get { return _priceId; }
            set { _priceId = value; }
        }

        private string _createDate;
        /// <summary>
        /// 发布日期
        /// </summary>
        public string CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }

        private decimal _au;
        /// <summary>
        /// 黄金报价
        /// </summary>
        public decimal Au
        {
            get { return _au; }
            set { _au = value; }
        }

        private decimal _ag;
        /// <summary>
        /// 白银报价
        /// </summary>
        public decimal Ag
        {
            get { return _ag; }
            set { _ag = value; }
        }

        private decimal _pt;
        /// <summary>
        /// 铂金报价
        /// </summary>
        public decimal Pt
        {
            get { return _pt; }
            set { _pt = value; }
        }

        private decimal _pd;
        /// <summary>
        /// 钯金报价
        /// </summary>
        public decimal Pd
        {
            get { return _pd; }
            set { _pd = value; }
        }

        private decimal _au_b;
        /// <summary>
        /// 黄金回购价
        /// </summary>
        public decimal Au_b
        {
            get { return _au_b; }
            set { _au_b = value; }
        }

        private decimal _ag_b;
        /// <summary>
        /// 白银回购价
        /// </summary>
        public decimal Ag_b
        {
            get { return _ag_b; }
            set { _ag_b = value; }
        }

        private decimal _pt_b;
        /// <summary>
        /// 铂金回购价
        /// </summary>
        public decimal Pt_b
        {
            get { return _pt_b; }
            set { _pt_b = value; }
        }

        private decimal _pd_b;
        /// <summary>
        /// 钯金回购价
        /// </summary>
        public decimal Pd_b
        {
            get { return _pd_b; }
            set { _pd_b = value; }
        }

        private Enable _isEnable;
        /// <summary>
        /// 1正常 0禁用
        /// </summary>
        public Enable IsEnable
        {
            get { return _isEnable; }
            set { _isEnable = value; }
        }

        #endregion

        #region 外键字段

        #endregion

    }

}
