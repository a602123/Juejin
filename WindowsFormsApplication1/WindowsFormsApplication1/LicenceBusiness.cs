using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    

    
        public class LicenceBusiness
        {
            private static LicenceBusiness _instance;


            private string _machineCode;
            private bool _licenceState;

            public static LicenceBusiness GetInstance()
            {
                if (_instance == null)
                {
                    _instance = new LicenceBusiness();
                }
                return _instance;
            }

            private LicenceBusiness()
            {
                _machineCode = CreateMachineCode();
//#if DEBUG
//            _machineCode = "123";
//#endif
                TestLicenceCode();
            }

            public void TestLicenceCode()
            {
                //Dictionary<string, string> config = null;// new ConfigBusiness().GetConfigFromDB(" AND `ConfigName`='SysCode' OR `ConfigName`='LicenceNumber'");
                var LicenceCode= ConfigurationManager.AppSettings["LicenceCode"];
                if (!string.IsNullOrEmpty(LicenceCode))
                {
                    if (TestLicenceCode(LicenceCode))
                    {
                        _licenceState = true;
                    }
                    else
                    {
                        _licenceState = false;
                    }
                }
                else
                {
                    _licenceState = false;
                }
            }

            private bool TestLicenceCode(string licenceCode)
            {
                string aseStr = AESHelper.Encrypt($"<MachineCode>{_machineCode}</MachineCode>", "zhongtaiTimerDo1", "zhongtaiTimerDo1");
                string temp = MD5Helper.ComputeMD5Hash(aseStr);
                return MD5Helper.ComputeMD5Hash(aseStr) == licenceCode;
            }

            private string CreateMachineCode()
            {
                string temp = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault().GetPhysicalAddress().ToString();
                return MD5Helper.ComputeMD5Hash(AESHelper.Encrypt($"<Mac>{System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault().GetPhysicalAddress().ToString()}</Mac>", "zhongtaiTimerDo1", "zhongtaiTimerDo1"));
            }

            /// <summary>
            /// 机器码
            /// </summary>
            public string MachineCode { get { return _machineCode; } }
        
            //物理地址: ‎3C-46-D8-D8-CD-10

            public bool LicenceState { get { return _licenceState; } }

        }
    

}
