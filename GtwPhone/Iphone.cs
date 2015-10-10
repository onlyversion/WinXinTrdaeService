using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace JtwPhone
{
    [ServiceContract(Name = "Iphone", Namespace = "www.Iphone.com")]
    public partial interface Iphone
    {
        [OperationContract]
        [XmlSerializerFormatAttribute]
        String Process(String ReqXml);
    }
}
