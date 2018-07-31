using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mac = string.Empty;
            this.textBox1.Text.Split('-').ToList().ForEach(o => mac += o);
            var macMd5Code = MD5Helper.ComputeMD5Hash(AESHelper.Encrypt($"<Mac>{mac}</Mac>", "zhongtaiTimerDo1", "zhongtaiTimerDo1"));//System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault().GetPhysicalAddress().ToString()
            var LicenceCode = MD5Helper.ComputeMD5Hash(AESHelper.Encrypt($"<MachineCode>{macMd5Code}</MachineCode>", "zhongtaiTimerDo1", "zhongtaiTimerDo1"));
            textBox1.Text = LicenceCode;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            #region MyRegion
            //wirte(codelist, AppDomain.CurrentDomain.BaseDirectory + "\\" + "obj.obj");
            //read(AppDomain.CurrentDomain.BaseDirectory + "\\" + "obj.obj");
            //int i = 0;
            //int sheshi = 0;
            //double huashi = 0;
            //if (sheshi%20==0)
            //{
            //    do
            //    {
            //        huashi = sheshi * 9 / 5.0 + 32;
            //        Console.WriteLine($"{sheshi}|||{huashi}");
            //        sheshi = sheshi + 20;
            //        i++;

            //    } while (i<9&&sheshi<250);
            //} 
            #endregion
            
            
        }
        List<LicenseCode> codelist = new List<LicenseCode> {
            new LicenseCode() {Code=123,expriseTime=new DateTime(2018,1,23,16,0,0)},
            new LicenseCode() {Code=123489,expriseTime=new DateTime(2018,1,23,16,0,0)},
            new LicenseCode() {Code=123456789,expriseTime=new DateTime(2018,1,23,16,0,0)},
            new LicenseCode() {Code=1234789,expriseTime=new DateTime(2018,1,23,16,0,0)}
        };
        void wirte(object master,string path)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Append))
            {
                var myByte = Serializer.SerializeBytes(master);
                fs.Write(myByte, 0, myByte.Length);
                fs.Close();
            };
        }
        void read(string path)
        {
            using (System.IO.FileStream fsRead = new System.IO.FileStream(path, System.IO.FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                var myObj = Serializer.DeserializeBytes(heByte) as List<LicenseCode>;
                Console.WriteLine("编号---时间");
                myObj.ForEach(m =>
                    Console.WriteLine(m.Code + "---" + m.expriseTime)
                );
            }
        }

    }
    [Serializable]
    public class LicenseCode {
        public object Code { get; set; }
        public DateTime expriseTime { get; set; }
    }
    public class Serializer
    {
        /// <summary> 
        /// 使用二进制序列化对象。 
        /// </summary> 
        /// <param name="value"></param> 
        /// <returns></returns> 
        public static byte[] SerializeBytes(object value)
        {
            if (value == null) return null;

            var stream = new System.IO.MemoryStream();
            new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Serialize(stream, value);

            //var dto = Encoding.UTF8.GetString(stream.GetBuffer()); 
            var bytes = stream.ToArray();
            return bytes;
        }

        /// <summary> 
        /// 使用二进制反序列化对象。 
        /// </summary> 
        /// <param name="bytes"></param> 
        /// <returns></returns> 
        public static object DeserializeBytes(byte[] bytes)
        {
            if (bytes == null) return null;

            //var bytes = Encoding.UTF8.GetBytes(dto as string); 
            var stream = new System.IO.MemoryStream(bytes);

            var result = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Deserialize(stream);

            return result;
        }
    }


}
