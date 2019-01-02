namespace MyFTPClient
{
    partial class FTPClient
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ServerAddress = new System.Windows.Forms.TextBox();
            this.bUser = new System.Windows.Forms.TextBox();
            this.PassWord = new System.Windows.Forms.TextBox();
            this.ServerList = new System.Windows.Forms.ListBox();
            this.LocalList = new System.Windows.Forms.ListBox();
            this.Login = new System.Windows.Forms.Button();
            this.Out = new System.Windows.Forms.Button();
            this.Download = new System.Windows.Forms.Button();
            this.Upload = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbAnonymous = new System.Windows.Forms.CheckBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tbLoginMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "FTP服务器地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "用户名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(88, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "密码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "FTP根目录文件列表";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(378, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "上传文件名";
            // 
            // ServerAddress
            // 
            this.ServerAddress.Location = new System.Drawing.Point(194, 30);
            this.ServerAddress.Name = "ServerAddress";
            this.ServerAddress.Size = new System.Drawing.Size(284, 21);
            this.ServerAddress.TabIndex = 5;
            this.ServerAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ServerAddress_KeyPress);
            // 
            // bUser
            // 
            this.bUser.Location = new System.Drawing.Point(194, 59);
            this.bUser.Name = "bUser";
            this.bUser.Size = new System.Drawing.Size(284, 21);
            this.bUser.TabIndex = 6;
            this.bUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.bUser_KeyPress);
            // 
            // PassWord
            // 
            this.PassWord.Location = new System.Drawing.Point(194, 92);
            this.PassWord.Name = "PassWord";
            this.PassWord.Size = new System.Drawing.Size(284, 21);
            this.PassWord.TabIndex = 7;
            this.PassWord.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PassWord_KeyPress);
            // 
            // ServerList
            // 
            this.ServerList.FormattingEnabled = true;
            this.ServerList.ItemHeight = 12;
            this.ServerList.Location = new System.Drawing.Point(58, 162);
            this.ServerList.Name = "ServerList";
            this.ServerList.Size = new System.Drawing.Size(284, 160);
            this.ServerList.TabIndex = 8;
            this.ServerList.SelectedIndexChanged += new System.EventHandler(this.ServerList_SelectedIndexChanged);
            this.ServerList.DoubleClick += new System.EventHandler(this.ServerList_DoubleClick);
            // 
            // LocalList
            // 
            this.LocalList.FormattingEnabled = true;
            this.LocalList.ItemHeight = 12;
            this.LocalList.Location = new System.Drawing.Point(380, 162);
            this.LocalList.Name = "LocalList";
            this.LocalList.Size = new System.Drawing.Size(284, 160);
            this.LocalList.TabIndex = 9;
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(565, 57);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(75, 23);
            this.Login.TabIndex = 10;
            this.Login.Text = "登录";
            this.Login.UseVisualStyleBackColor = true;
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // Out
            // 
            this.Out.Location = new System.Drawing.Point(565, 96);
            this.Out.Name = "Out";
            this.Out.Size = new System.Drawing.Size(75, 23);
            this.Out.TabIndex = 11;
            this.Out.Text = "退出";
            this.Out.UseVisualStyleBackColor = true;
            this.Out.Click += new System.EventHandler(this.Out_Click);
            // 
            // Download
            // 
            this.Download.Location = new System.Drawing.Point(194, 343);
            this.Download.Name = "Download";
            this.Download.Size = new System.Drawing.Size(75, 23);
            this.Download.TabIndex = 12;
            this.Download.Text = "下载";
            this.Download.UseVisualStyleBackColor = true;
            this.Download.Click += new System.EventHandler(this.Download_Click);
            // 
            // Upload
            // 
            this.Upload.Location = new System.Drawing.Point(489, 343);
            this.Upload.Name = "Upload";
            this.Upload.Size = new System.Drawing.Size(75, 23);
            this.Upload.TabIndex = 13;
            this.Upload.Text = "上传";
            this.Upload.UseVisualStyleBackColor = true;
            this.Upload.Click += new System.EventHandler(this.Upload_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 0;
            // 
            // cbAnonymous
            // 
            this.cbAnonymous.AutoSize = true;
            this.cbAnonymous.Location = new System.Drawing.Point(565, 21);
            this.cbAnonymous.Name = "cbAnonymous";
            this.cbAnonymous.Size = new System.Drawing.Size(48, 16);
            this.cbAnonymous.TabIndex = 16;
            this.cbAnonymous.Text = "匿名";
            this.cbAnonymous.UseVisualStyleBackColor = true;
            this.cbAnonymous.Click += new System.EventHandler(this.cbAnonymous_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(344, 343);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 17;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(64, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "登录信息";
            // 
            // tbLoginMessage
            // 
            this.tbLoginMessage.Location = new System.Drawing.Point(194, 0);
            this.tbLoginMessage.Name = "tbLoginMessage";
            this.tbLoginMessage.Size = new System.Drawing.Size(284, 21);
            this.tbLoginMessage.TabIndex = 19;
            // 
            // FTPClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 378);
            this.Controls.Add(this.tbLoginMessage);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.cbAnonymous);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Upload);
            this.Controls.Add(this.Download);
            this.Controls.Add(this.Out);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.LocalList);
            this.Controls.Add(this.ServerList);
            this.Controls.Add(this.PassWord);
            this.Controls.Add(this.bUser);
            this.Controls.Add(this.ServerAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FTPClient";
            this.Text = "FTPClient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FTPClient_FormClosing);
            this.Load += new System.EventHandler(this.FTPClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ServerAddress;
        private System.Windows.Forms.TextBox bUser;
        private System.Windows.Forms.TextBox PassWord;
        private System.Windows.Forms.ListBox ServerList;
        private System.Windows.Forms.ListBox LocalList;
        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.Button Out;
        private System.Windows.Forms.Button Download;
        private System.Windows.Forms.Button Upload;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbAnonymous;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbLoginMessage;
    }
}

