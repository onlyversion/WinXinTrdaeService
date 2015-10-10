using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JinTong.Jyrj.Common
{
    public class VerifyCode
    {
        #region 验证码长度(默认6个验证码的长度)
        int length = 6;
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        #endregion
        #region 验证码字体大小(为了显示扭曲效果，默认40像素，可以自行修改)
        int fontSize = 40;
        public int FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }
        #endregion
        #region 边框补(默认1像素)
        int padding = 2;
        public int Padding
        {
            get { return padding; }
            set { padding = value; }
        }
        #endregion
        #region 是否输出燥点(默认输出)
        bool chaos = true;
        public bool Chaos
        {
            get { return chaos; }
            set { chaos = value; }
        }
        #endregion
        #region 自定义字体数组
        string[] fonts = { "Arial", "Georgia" };
        public string[] Fonts
        {
            get { return fonts; }
            set { fonts = value; }
        }
        #endregion
        #region 自定义随机码字符串序列(使用逗号分隔)
        string codeSerial = "3,4,5,6,7,8,a,b,c,d,e,f,h,j,k,m,n,p,r,s,t,u,v,w,x,y,A,B,C,D,E,F,G,H,J,K,M,N,P,Q,R,S,T,U,V,W,X,Y,Z";
        public string CodeSerial
        {
            get { return codeSerial; }
            set { codeSerial = value; }
        }
        #endregion
        #region 产生波形滤镜效果
        private const double PI = 3.1415926535897932384626433832795;
        private const double PI2 = 6.283185307179586476925286766559;
        #endregion
        #region 生成随机字符码
        public string CreateVerifyCode(int codeLen)
        {
            if (codeLen == 0)
            {
                codeLen = Length;
            }
            string[] arr = CodeSerial.Split(',');
            string code = "";
            int randValue = -1;
            Random rand = new Random(unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < codeLen; i++)
            {
                randValue = rand.Next(0, arr.Length - 1);
                code += arr[randValue];
            }
            return code;
        }
        public string CreateVerifyCode()
        {
            return CreateVerifyCode(0);
        }
        #endregion
    }
}
