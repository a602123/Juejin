using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using System.Timers;
using System.IO;
using mshtml;
using System.Media;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string workurl = "http://ztcrm.zotye.com/saleClue_queryPagedList.action";
        string loginurl = "http://ztcrm.zotye.com/userLogin.jsp";
        static string str_user = ConfigurationManager.AppSettings["User"];
        static double interval = Convert.ToDouble(ConfigurationManager.AppSettings["interval"]);
        const string logintitle = "众泰客户关系管理系统";
        const string worktitle = "销售线索信息维护";
        bool falg=false;
        List<string> logList;
        public Dictionary<string,string> user
        {
            get {
                return new Dictionary<string, string>() {
                    {"loginName",str_user.Split('$')[0] } ,{"password",str_user.Split('$')[1]}
                };
            }
        }
        public Form1()
        {
            InitializeComponent();
        }       

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            //var acceptLanguageHeader = "Accept-Language:en-US,q=0.5\nUser-Agent:MyCoustomBrowser";
            this.webBrowser1.Navigate(loginurl);
            
            //System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            //timer1.Interval = 20000;
            //timer1.Enabled = true;
            //MessageBox.Show("试用期为1小时，欢迎体验!");
            //timer1.Tick += new EventHandler(timer1EventProcessor);//添加事件

        }
        //public void timer1EventProcessor(object source, EventArgs e)
        //{
        //    MessageBox.Show("试用时间已到！");            
        //    Stop();
        //    Application.Exit();
        //}

        void login()
        {
            
                this.webBrowser1.Document.GetElementById("userName").InnerText = user["loginName"];
                this.webBrowser1.Document.GetElementById("password").InnerText = user["password"];
                this.webBrowser1.Document.GetElementsByTagName("input").GetElementsByName("")[0].InvokeMember("click");              
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            #region MyRegion
            //var param = new loginModel{ loginName = "18530005668", password = "123457" };           
            //using (HttpClient httpClient = new HttpClient())
            //{
            //    var result = await httpClient.PostAsync<loginModel>("http://ztcrm.zotye.com/user_login.do?", param);
            //}
            //HttpClient httpclient = new HttpClient();
            //List<NameValuePair> param = new List<NameValuePair>();
            //var data = await httpclient.GetAsync(loginurl);
            //var stringdata=await data.Content.
            //httpclient.PostAsJsonAsync();
            /*var content = new FormUrlEncodedContent(new KeyValuePair<string, string>()
            {

            });*/
            //MessageBox.Show(stringdata);
            //httpclient
            //HttpWebRequest wbrequest = (HttpWebRequest)WebRequest.Create(loginurl);            

            //var d = wbrequest.Headers;            

            //wbrequest.Headers.Set(HttpRequestHeader.Accept, "application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*");
            //wbrequest.Headers.Set(HttpRequestHeader.AcceptLanguage, "zh-CN");
            //wbrequest.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            //wbrequest.Headers.Set(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/7.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; InfoPath.2; Shuame)");
            //wbrequest.Headers.Set(HttpRequestHeader.Connection, "Keep-Alive");
            //wbrequest.Headers.Set(HttpRequestHeader.Host, "Keep-Alive");

            //HttpWebResponse wbrsponse = (HttpWebResponse)wbrequest.GetResponse();
            //HttpWebResponse wbrsponse=(HttpWebResponse)wbrequest.GetResponse();
            //var data = await httpclient.PostAsync(loginurl, new FormUrlEncodedContent(user));
            //var stringdata = await data.Content.ReadAsStringAsync();
            //MessageBox.Show(wbrequest.Headers.ToString()); 
            #endregion
            if (webBrowser1.ReadyState == WebBrowserReadyState.Complete)
            {
                var title = this.webBrowser1.Document.GetElementsByTagName("title")[0].InnerText;
                if (title == logintitle)
                {
                    login();
                    MessageBox.Show("登录成功！");
                    this.webBrowser1.Navigate(workurl);                    
                }
                else if (title == worktitle)
                {
                    MessageBox.Show("用户已登录，勿重复登录！");
                }
                else
                {
                    MessageBox.Show("程序异常！请重启系统");
                    Application.Exit();
                }
            }

        }


        private void doworkbtn_Click(object sender, EventArgs e)
        {
            if (LicenceBusiness.GetInstance().LicenceState)
            {
                Task.Factory.StartNew(() =>
                {
                    this.BeginInvoke(new workmethod(() =>
                    {


                        label1.Text = "定期执行已开始...";
                        label1.ForeColor = Color.GreenYellow; ;
                        log();
                        dowork();
                    }));
                    doworkInit();
                    Start();

                });
            }
            else
            {
                MessageBox.Show("请联系技术人员，升级权限操作！（q群:572670808,联系群主及管理。）");
            }

        }

        #region 轮询
        private System.Timers.Timer _timer;

        public void doworkInit()
        {
            if (_timer == null)
            {                
                _timer = new System.Timers.Timer();
                _timer.Interval = interval>0?TimeSpan.FromMinutes(interval).TotalMilliseconds: TimeSpan.FromMinutes(5).TotalMilliseconds;
                _timer.Elapsed += Timer_Elapsed;
                //_timer.                
            }
        }
        private void Start()
        {
            if (_timer != null)
            {
                _timer.Start();
            }
        }
        private void Stop()
        {
            if (_timer != null)
            {
                _timer.Stop();
            }
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {            
            this.BeginInvoke(new workmethod(() =>
            {
                //this.webBrowser1.Refresh();

                log();
                dowork();


            }));           
            


        }

        #endregion

        void dowork()
        {            
            var document =this.webBrowser1.Document;
            if (document.Title == "销售线索信息维护")
            {
                IHTMLDocument2 vDocument = (IHTMLDocument2)webBrowser1.Document.DomDocument;
                vDocument.parentWindow.execScript("function confirm(str){return true;} ", "javascript");
                vDocument.parentWindow.execScript("function alert(str){return true;} ", "javaScript");//弹出提示

                var status = document.GetElementById("status");
                status.SetAttribute("value", "0");

                var cx = document.GetElementById("cx");
                cx.InvokeMember("click");

                if (IsHasResult())
                {
                    #region 全选_LastVersion
                    var idall = document.GetElementById("idall");
                    //i.SetAttribute("checked","checked");
                    //idall.InvokeMember("click");
                    #endregion
                    foreach (HtmlElement item in document.GetElementsByTagName("input"))
                    {
                        if (item.Id == "urls")
                        {
                            item.SetAttribute("checked", "checked");
                        }
                    }
                    var a = document.GetElementById("defeatReasonSelect");
                    a.SetAttribute("value", "战败");
                    var c = document.GetElementsByTagName("input").GetEnumerator();
                    while (c.MoveNext())
                    {
                        if (((HtmlElement)c.Current).Name == "defeat")
                        {
                            ((HtmlElement)c.Current).InvokeMember("click");
                            break;
                        }
                    }

                }

                vDocument.parentWindow.execScript("function alert(str){return true;} ", "javaScript");//弹出提示
            }
            else if (document.Title == logintitle)
            {
                this.BeginInvoke(new workmethod(() => { MessageBox.Show("请重新登录", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }));

            }
            else  if (webBrowser1.DocumentText.Contains("无法") || webBrowser1.DocumentText.Contains("404") || webBrowser1.DocumentText.ToLower().Contains("not found")|| webBrowser1.DocumentText.ToLower().Contains("此程序"))
                //webBrowser1.DocumentTitle == "无法找到资源。"|| webBrowser1.DocumentTitle == "无法找到该页"||webBrowser1.DocumentTitle== "HTTP 404 未找到")
            {
                
                this.BeginInvoke(new workmethod(() =>
                {
                    label1.Text = "";
                    System.Media.SystemSounds.Hand.Play();
                    MessageBox.Show("请稍后重新启动该程序，有可能是掘金服务器出现问题！", "未知错误", MessageBoxButtons.OK,MessageBoxIcon.Error);                                      
                }));
                
            }



        }
        delegate void workmethod();
        void log()
        {
            var doc = this.webBrowser1.Document;            
            logList = new List<string>();
            var b=doc.GetElementsByTagName("input");
            var weblogCount = 0;
            if (IsHasResult())
            {
                foreach (HtmlElement item in b)
                {
                    if (item.Id == "urls")
                    {
                        weblogCount++;
                        HtmlElement parentElement = item.Parent.Parent;
                        StringBuilder log = new StringBuilder();
                        log.Append($"处理时间:{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")},")
                           .Append($"初始来源:{parentElement.Children[1].InnerText} ,")
                           .Append($"销售顾问:{parentElement.Children[2].InnerText} ,")
                           .Append($"客户姓名:{parentElement.Children[3].Children[0].InnerText} ,")
                           .Append($"联系方式:{parentElement.Children[4].InnerText} ,")
                           .Append($"状态:{parentElement.Children[7].InnerText} ,")
                           .Append($"创建时间:{parentElement.Children[8].InnerText} ,");
                        logList.Add(log.ToString());
                    }
                }
            }

            if (logList.Count>0)
            {
                string logresult = string.Empty;
                logList.ForEach((i) =>
                {
                    logresult += $"{i} \r\n ";
                });
                logresult += $"本次处理{logList.Count}条线索{weblogCount}\r\n";
                writelog(logresult,"log.txt");
                PlaySound();
            }

            else
            {
                writelog($"---{DateTime.Now} ,没有未处理线索","logSerach.txt");
            }
        }

        bool IsHasResult()
        {
            bool falg = false;
            var table = this.webBrowser1.Document.GetElementsByTagName("table").GetEnumerator();
            while (table.MoveNext())
            {
                if (((HtmlElement)table.Current).GetAttribute("className") == "tbl_")
                {
                    falg = true;
                    break;
                }
            }
            return falg;
        }
        #region 操作Txt
        public void writelog(string log,string filename)
        {
            if (!File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + $"\\{filename}"))
            {
                FileStream fs1 = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + $"\\{filename}", FileMode.Create, FileAccess.Write);//创建写入文件
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(log);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + $"\\{filename}", FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(log);//开始写入值
                sr.Close();
                fs.Close();
            }
        }
        #endregion

        private void Refreshbtn_Click(object sender, EventArgs e)
        {            
            this.webBrowser1.Refresh();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (LicenceBusiness.GetInstance().LicenceState)
            {
                Stop();
                label1.Text = "定期执行已停止...";
                label1.ForeColor = Color.Red;
            }
            else
            {
                MessageBox.Show("请联系技术人员，升级权限操作！");
            }
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", System.AppDomain.CurrentDomain.BaseDirectory);
            //MessageBox.Show(System.AppDomain.CurrentDomain.BaseDirectory);
        }
        
        private static void PlaySound()
        {
            //以流的方式,从资源文件读取背景音乐文件
            Stream stream = global::WindowsFormsApplication1.Music.prompt;//global::Music.Properties.Resources.music;

            //初始化一个音乐播放器类
            SoundPlayer player = new SoundPlayer(stream);
            //SoundPlayer player = new SoundPlayer();

            //player.SoundLocation = "prompt.wav";

            //异步载入文件
            player.LoadAsync();

            //播放音乐
            player.Play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
              PlaySound();                  
            //ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            writelog(System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault().GetPhysicalAddress().ToString(), "LicenceMac");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate(loginurl);
        }
    }     
}
