using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WcfInterface;
using WcfInterface.model;
using System.Diagnostics;
using WcfInterface;
namespace JtwPhone.Code
{
    /// <summary>
    /// 检测更新
    /// </summary>
    public class UC017
    {
        class Apkinfo
        {
            /// <summary>
            /// 包名
            /// </summary>
            public string packageName
            {
                get;
                set;
            }
            /// <summary>
            /// 版本名
            /// </summary>
            public string versionName
            {
                get;
                set;
            }
            /// <summary>
            /// 版本号
            /// </summary>
            public int versionCode
            {
                get;
                set;
            }
        }
        public String AnalysisXml(string ReqXml)
        {
            string ResXml = string.Empty;
            string ReqCode = string.Empty;
            try
            {
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
                xmldoc.LoadXml(ReqXml);

                //获取版本号
                int VersionCode = Convert.ToInt32(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/VersionCode").InnerText);

                //请求指令
                ReqCode = xmldoc.SelectSingleNode("JTW91G/MsgData/ReqHeader/ReqCode").InnerText;
                if (!string.IsNullOrEmpty(ComFunction.UpdateFile))
                {
                    //FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo(ComFunction.UpdateFile);
                    Apkinfo apkinfo = GetApkInfo();
                    if (apkinfo.versionCode <= VersionCode)
                    {
                        ResXml = GssResXml.GetResXml(ReqCode, ResCode.UL035, ResCode.UL035Desc, string.Format("<DataBody></DataBody>"));
                    }
                    else
                    {
                        ResXml = GssResXml.GetResXml(ReqCode, ResCode.UL036, ResCode.UL036Desc,
                            string.Format("<DataBody><UpdateAddr>{0}</UpdateAddr><PackageName>{1}</PackageName><VersionCode>{2}</VersionCode><VersionName>{3}</VersionName></DataBody>",
                            ComFunction.UpdateAddr, apkinfo.packageName, apkinfo.versionCode, apkinfo.versionName));
                    }
                }
                else
                {
                    ResXml = GssResXml.GetResXml(ReqCode, ResCode.UL035, ResCode.UL035Desc, string.Format("<DataBody></DataBody>"));
                }
            }
            catch (Exception ex)
            {
                com.individual.helper.LogNet4.WriteErr(ex);

                //业务处理失败
                ResXml = GssResXml.GetResXml(ReqCode, ResCode.UL005, ResCode.UL005Desc, string.Format("<DataBody></DataBody>"));
            }
            return ResXml;
        }

        Apkinfo GetApkInfo()
        {
            try
            {
                Apkinfo apkinfo = new Apkinfo();

                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe");
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.FileName = System.AppDomain.CurrentDomain.BaseDirectory + "aapt\\aapt.exe";
                psi.Arguments = string.Format(" d badging {0}", ComFunction.UpdateFile);
                System.Diagnostics.Process proc = System.Diagnostics.Process.Start(psi);

                string package = proc.StandardOutput.ReadLine();
                proc.Close();
                //package: name='com.higgses.jtw' versionCode='1' versionName='beta_0.0.1'
                //Console.WriteLine(package);

                string[] lists = package.Split(' ');
                string nameKey = "name='";
                string versionNameKey = "versionName='";
                string versionCodeKey = "versionCode='";
                string versionCode = string.Empty;

                foreach (string str in lists)
                {
                    if (str.Contains(nameKey))
                    {
                        apkinfo.packageName = str.Substring(str.IndexOf(nameKey) + nameKey.Length);
                        apkinfo.packageName = apkinfo.packageName.Substring(0, apkinfo.packageName.Length - 1);
                    }
                    else if (str.Contains(versionCodeKey))
                    {
                        versionCode = str.Substring(str.IndexOf(versionCodeKey) + versionCodeKey.Length);
                        versionCode = versionCode.Substring(0, versionCode.Length - 1);
                        apkinfo.versionCode = Convert.ToInt32(versionCode);
                    }
                    else if (str.Contains(versionNameKey))
                    {
                        apkinfo.versionName = str.Substring(str.IndexOf(versionNameKey) + versionNameKey.Length);
                        apkinfo.versionName = apkinfo.versionName.Substring(0, apkinfo.versionName.Length - 1);
                    }
                }
                return apkinfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
