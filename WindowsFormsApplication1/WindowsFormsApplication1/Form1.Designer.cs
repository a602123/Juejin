namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.loginbtn = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.doworkbtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Refreshbtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.exitbtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loginbtn
            // 
            this.loginbtn.Location = new System.Drawing.Point(23, 13);
            this.loginbtn.Name = "loginbtn";
            this.loginbtn.Size = new System.Drawing.Size(75, 23);
            this.loginbtn.TabIndex = 0;
            this.loginbtn.Text = "一键登录";
            this.loginbtn.UseVisualStyleBackColor = true;
            this.loginbtn.Click += new System.EventHandler(this.loginbtn_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(784, 562);
            this.webBrowser1.TabIndex = 1;
            // 
            // doworkbtn
            // 
            this.doworkbtn.Location = new System.Drawing.Point(115, 13);
            this.doworkbtn.Name = "doworkbtn";
            this.doworkbtn.Size = new System.Drawing.Size(75, 23);
            this.doworkbtn.TabIndex = 2;
            this.doworkbtn.Text = "定期处理";
            this.doworkbtn.UseVisualStyleBackColor = true;
            this.doworkbtn.Click += new System.EventHandler(this.doworkbtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(408, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // Refreshbtn
            // 
            this.Refreshbtn.Location = new System.Drawing.Point(318, 13);
            this.Refreshbtn.Name = "Refreshbtn";
            this.Refreshbtn.Size = new System.Drawing.Size(75, 23);
            this.Refreshbtn.TabIndex = 4;
            this.Refreshbtn.Text = "刷新当前页面";
            this.Refreshbtn.UseVisualStyleBackColor = true;
            this.Refreshbtn.Click += new System.EventHandler(this.Refreshbtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(196, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "终止定期处理";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // exitbtn
            // 
            this.exitbtn.Location = new System.Drawing.Point(685, 13);
            this.exitbtn.Margin = new System.Windows.Forms.Padding(3, 30, 3, 3);
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.Size = new System.Drawing.Size(73, 23);
            this.exitbtn.TabIndex = 6;
            this.exitbtn.Text = "退出程序";
            this.exitbtn.UseVisualStyleBackColor = true;
            this.exitbtn.Click += new System.EventHandler(this.exitbtn_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(546, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "程序文件安装目录";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.exitbtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Refreshbtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.doworkbtn);
            this.Controls.Add(this.loginbtn);
            this.Controls.Add(this.webBrowser1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "众泰掘金自动化处理";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loginbtn;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button doworkbtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Refreshbtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button exitbtn;
        private System.Windows.Forms.Button button2;
    }
}

