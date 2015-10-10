using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WcfInterface;

namespace JtwPhone
{
   public class GssGetCode
    {
       public static string GetCode(string InCode,string desc, ref string CodeDesc)
       {
           string Code = string.Empty;
           CodeDesc = ResCode.UL005Desc;
           if (string.IsNullOrEmpty(InCode))
           {
               Code = ResCode.UL005;
           }
           switch (InCode)
           {
               case ResCode.UL003:
                   Code = ResCode.UL003;
                   CodeDesc = ResCode.UL003Desc;
                   break;
               case ResCode.UL007:
                   Code = ResCode.UL007;
                   CodeDesc = ResCode.UL007Desc;
                   break;
               case ResCode.UL008:
                   Code = ResCode.UL008;
                   CodeDesc = ResCode.UL008Desc;
                   break;
               case ResCode.UL009:
                   Code = ResCode.UL009;
                   CodeDesc = ResCode.UL009Desc;
                   break;
               case ResCode.UL010:
                   Code = ResCode.UL010;
                   CodeDesc = ResCode.UL010Desc;
                   break;
               case ResCode.UL011:
                   Code = ResCode.UL011;
                   CodeDesc = ResCode.UL011Desc;
                   break;
               case ResCode.UL012:
                   Code = ResCode.UL012;
                   CodeDesc = ResCode.UL012Desc;
                   break;
               case ResCode.UL013:
                   Code = ResCode.UL013;
                   CodeDesc = ResCode.UL013Desc;
                   break;
               case ResCode.UL014:
                   Code = ResCode.UL014;
                   CodeDesc = ResCode.UL014Desc;
                   break;
               case ResCode.UL015:
                   Code = ResCode.UL015;
                   CodeDesc = ResCode.UL015Desc;
                   break;
               case ResCode.UL016:
                   Code = ResCode.UL016;
                   CodeDesc = ResCode.UL016Desc;
                   break;
               case ResCode.UL017:
                   Code = ResCode.UL017;
                   CodeDesc = ResCode.UL017Desc;
                   break;
               case ResCode.UL018:
                   Code = ResCode.UL018;
                   CodeDesc = ResCode.UL018Desc;
                   break;
               case ResCode.UL019:
                   Code = ResCode.UL019;
                   CodeDesc = ResCode.UL019Desc;
                   break;
               case ResCode.UL020:
                   Code = ResCode.UL020;
                   CodeDesc = ResCode.UL020Desc;
                   break;
               case ResCode.UL021:
                   Code = ResCode.UL021;
                   CodeDesc = ResCode.UL021Desc;
                   break;
               case ResCode.UL022:
                   Code = ResCode.UL022;
                   CodeDesc = ResCode.UL022Desc;
                   break;
               case ResCode.UL023:
                   Code = ResCode.UL023;
                   CodeDesc = ResCode.UL023Desc;
                   break;
               case ResCode.UL024:
                   Code = ResCode.UL024;
                   CodeDesc = ResCode.UL024Desc;
                   break;
               case ResCode.UL025:
                   Code = ResCode.UL025;
                   CodeDesc = ResCode.UL025Desc;
                   break;
               case ResCode.UL026:
                   Code = ResCode.UL026;
                   CodeDesc = ResCode.UL026Desc;
                   break;
               case ResCode.UL027:
                   Code = ResCode.UL027;
                   CodeDesc = ResCode.UL027Desc;
                   break;
               case ResCode.UL028:
                   Code = ResCode.UL028;
                   CodeDesc = ResCode.UL028Desc;
                   break;
               case ResCode.UL029:
                   Code = ResCode.UL029;
                   CodeDesc = ResCode.UL029Desc;
                   break;
               case ResCode.UL030:
                   Code = ResCode.UL030;
                   CodeDesc = ResCode.UL030Desc;
                   break;
               case ResCode.UL031:
                   Code = ResCode.UL031;
                   CodeDesc = ResCode.UL031Desc;
                   break;
               case ResCode.UL038:
                   Code = ResCode.UL038;
                   CodeDesc = ResCode.UL038Desc;
                   break;
               case ResCode.UL044:
                   Code = ResCode.UL044;
                   //CodeDesc = ResCode.UL044Desc;
                   CodeDesc = desc;
                   break;
           }
           return Code;
       }
    }
}
