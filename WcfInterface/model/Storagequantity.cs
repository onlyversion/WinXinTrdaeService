//*******************************************************************************
//  文 件 名：Storagequantity.cs
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
    /// 库存
    /// </summary>
    public class Storagequantity
    {

        /// <summary>
        /// Gets or sets 黄金克数(包含未办理业务的黄金)
        /// </summary>
        public double xau
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 钯金克数(包含未办理业务的钯金)
        /// </summary>
        public double xpt
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 铂金克数(包含未办理业务的铂金)
        /// </summary>
        public double xpd
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 白银千克数(包含未办理业务的白银)
        /// </summary>
        public double xag
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 未办理业务的黄金克数
        /// </summary>
        public double nxau
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 未办理业务的钯金克数
        /// </summary>
        public double nxpt
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 未办理业务的铂金克数
        /// </summary>
        public double nxpd
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 未办理业务的白银千克数
        /// </summary>
        public double nxag
        {
            set;
            get;
        }
    }
}
