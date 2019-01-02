using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;


namespace MyFTPClient
{
    public partial class FTPClient : Form
    {
        private const int ftpport = 21;//端口为21
        private string ftpUristring = null;//要访问的资源
        private NetworkCredential networkCredential;//验证身份状态
        private string currentDir = "/";//客户端当前目录

        public FTPClient()
        {
            InitializeComponent();
            //IPAddress[] ips = Dns.GetHostAddresses("");//获取IP
          //  ServerAddress.Text = ips[0].ToString();
           // ServerAddress.SelectAll();
            //初始换按钮
            Out.Enabled = false;//退出
            ServerList.Enabled = false;//ftp目录
            LocalList.Enabled = false;//本地目录
            Upload.Enabled = false;//上传
            Download.Enabled = false;//下载
            btnDelete.Enabled = false;//删除

        }

        //ip按钮 1
        private void ServerAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            //用户输入回车后,跳转用户框
            if (e.KeyChar == (char)13)
            {
                bUser.Focus();
            }
        }

        //用户按钮 1
        private void bUser_KeyPress(object sender, KeyPressEventArgs e)
        {   //输入回车后,跳转密码
            if (e.KeyChar == (char)13)
            {
                PassWord.Focus();
            }

        }

        //密码按钮 1
        private void PassWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Login.Focus();
            }
        }

        //匿名按钮 1
        private void cbAnonymous_Click(object sender, EventArgs e)
        {
            if (cbAnonymous.Checked == false)//未选中
            {
                bUser.Enabled = true;
                bUser.Text = "";
                PassWord.Enabled = true;
                PassWord.Text = "";
                bUser.Focus();
            }
            //匿名
            else
            {
                bUser.Text = "Anonymous";
                bUser.Enabled = false;
                PassWord.Text = "";
                PassWord.Enabled = false;
            }
        }

        //登录按钮 1
        private void Login_Click(object sender, EventArgs e)
        {
            if (ServerAddress.Text == string.Empty)//为空
            {
                MessageBox.Show("请先填写服务器IP地址", "提示");//弹出提示
                return;
            }
            ftpUristring = "ftp://" + ServerAddress.Text;//ftp信息
            networkCredential = new NetworkCredential(bUser.Text, PassWord.Text);//验证身份
            //ftp目录不为空(已登录),登录按钮无效,退出有效,目录为真,ip无效(无法再次输入)
            if (showFtpFileAndDirectory() == true)
            {
                Login.Enabled = false;
                Out.Enabled = true;
                ServerList.Enabled = true;
                LocalList.Enabled = true;
                ServerAddress.Enabled = false;
                //匿名按钮状态
                if (cbAnonymous.Checked == false)//未选中
                {
                    bUser.Enabled = false;
                    PassWord.Enabled = false;
                    cbAnonymous.Enabled = false;
                }
                else
                {
                    cbAnonymous.Enabled = false;
                }
                tbLoginMessage.Text = "登录成功";
                Upload.Enabled = true;
                Download.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                ServerList.Enabled = true;
                tbLoginMessage.Text = "登陆失败";
            }
        }

        //退出按钮 1
        private void Out_Click(object sender, EventArgs e)
        {
            Login.Enabled = true;
            Out.Enabled = false;
            ServerAddress.Enabled = true;
            ServerAddress.SelectAll();
            ServerAddress.Focus();
            cbAnonymous.Enabled = true;
            if (cbAnonymous.Checked == false)
            {
                bUser.Enabled = true;
                PassWord.Enabled = true;
            }
            tbLoginMessage.Text = "你已退出";
            ServerList.Items.Clear();
            ServerList.Enabled = false;
            LocalList.Items.Clear();
            LocalList.Enabled = false;
            Upload.Enabled = false;
            Download.Enabled = false;
            btnDelete.Enabled = false;
        }

        //上传按钮 1
        private void Upload_Click(object sender, EventArgs e)
        {
            // 选择要上传的文件
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = openFileDialog.FileNames.ToString();
            openFileDialog.Filter = "所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            FileInfo fileinfo = new FileInfo(openFileDialog.FileName);
            try
            {
                string uri = GetUriString(fileinfo.Name);
                FtpWebRequest request = CreateFtpWebRequest(uri, WebRequestMethods.Ftp.UploadFile);
                request.ContentLength = fileinfo.Length;
                int buflength = 8196;
                byte[] buffer = new byte[buflength];
                FileStream filestream = fileinfo.OpenRead();
                Stream responseStream = request.GetRequestStream();
                LocalList.Items.Add("打开上传流，文件上传中...");
                int contenlength = filestream.Read(buffer, 0, buflength);
                while (contenlength != 0)
                {
                    responseStream.Write(buffer, 0, contenlength);
                    contenlength = filestream.Read(buffer, 0, buflength);
                }

                responseStream.Close();
                filestream.Close();
                FtpWebResponse response = GetFtpResponse(request);
                if (response == null)
                {
                    LocalList.Items.Add("服务器未响应...");
                    LocalList.TopIndex = LocalList.Items.Count - 1;
                    return;
                }

                LocalList.Items.Add("上传完毕，服务器返回：" + response.StatusCode + " " + response.StatusDescription);
                LocalList.TopIndex = LocalList.Items.Count - 1;
                MessageBox.Show("上传成功！");

                // 上传成功后，立即刷新服务器目录列表
                showFtpFileAndDirectory();
            }
            catch (WebException ex)
            {
                LocalList.Items.Add("上传发生错误，返回信息为：" + ex.Status);
                LocalList.TopIndex =  LocalList.Items.Count - 1;
                MessageBox.Show(ex.Message, "上传失败");
            }


        }

        //下载按钮 1
        private void Download_Click(object sender, EventArgs e)
        {
            string fileName = GetSelectedFile();

            if (fileName.Length == 0)
            {
                MessageBox.Show("请先选择要下载的文件");
                return;
            }
            string filePath = "D:\\ftpdownload\\DownLoad";
            //  string filePath = Application.StartupPath 

            if (Directory.Exists(filePath) == false)
            {
                Directory.CreateDirectory(filePath);
            }

            Stream responseStream = null;

            FileStream fileStream = null;

            StreamReader reader = null;
            try
            {

                string uri = GetUriString(fileName);

                MessageBox.Show(uri);

                FtpWebRequest request = CreateFtpWebRequest(uri, WebRequestMethods.Ftp.DownloadFile);
                FtpWebResponse response = GetFtpResponse(request);

                if (response == null)
                {
                    return;
                }
                responseStream = response.GetResponseStream();
                string path = filePath + "\\" + fileName;
                MessageBox.Show(path);
                fileStream = File.Create(path);
                byte[] buffer = new byte[8196];
                int bytesRead;
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break;

                    }
                    fileStream.Write(buffer, 0, bytesRead);
                }
                MessageBox.Show("下载完毕");
            }
            catch (UriFormatException err)
            {
                MessageBox.Show(err.Message);
            }
            catch (WebException err)
            {

                MessageBox.Show(err.Message);
            }
            catch (IOException err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {

                if (reader != null)
                {
                    reader.Close();
                }
                else if (responseStream != null)
                {

                    responseStream.Close();
                }
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        //删除按钮 1
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string filename = GetSelectedFile();
            if (filename.Length == 0)
            {
                MessageBox.Show("请选择要删除的文件！", "提示");
                return;
            }

            try
            {
                string uri = GetUriString(filename);
                if (MessageBox.Show("确定要删除文件 " + filename + " 吗?", "确认文件删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FtpWebRequest request = CreateFtpWebRequest(uri, WebRequestMethods.Ftp.DeleteFile);
                    FtpWebResponse response = GetFtpResponse(request);
                    if (response == null)
                    {
                        LocalList.Items.Add("服务器未响应...");
                        LocalList.TopIndex = LocalList.Items.Count - 1;
                        return;
                    }

                    LocalList.Items.Add("文件删除成功，服务器返回：" + response.StatusCode + " " + response.StatusDescription);
                    showFtpFileAndDirectory();
                }
                else
                {
                    return;
                }
            }
            catch (WebException ex)
            {

                LocalList.Items.Add("发生错误，返回状态为：" + ex.Status);
                LocalList.TopIndex = LocalList.Items.Count - 1;
                MessageBox.Show(ex.Message, "删除失败");
            }
        }

        //创建连接
        private FtpWebRequest CreateFtpWebRequest(string uri, string requestMethod)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(uri);
            request.Credentials = networkCredential;//获取用户凭证
            request.KeepAlive = true;//请求完成后是否关闭连接
            request.UseBinary = true;//指定传输数据类型;
            request.Method = requestMethod;//获取或发送ftp命令
            return request;
        }

        // 获取服务器返回的响应体 1
        private FtpWebResponse GetFtpResponse(FtpWebRequest request)
        {
            FtpWebResponse response = null;
            try
            {
                response = (FtpWebResponse)request.GetResponse();
                LocalList.Items.Add("验证完毕，服务器回应信息：[" + response.WelcomeMessage + "]");
                LocalList.Items.Add("正在连接：[ " + response.BannerMessage + "]");
                LocalList.TopIndex = LocalList.Items.Count - 1;
                return response;
            }
            catch (WebException ex)
            {
                LocalList.Items.Add("发生错误,返回信息为：" + ex.Message);
                LocalList.TopIndex = LocalList.Items.Count - 1;
                return null;
            }
        }

        private string GetUriString(string filename)
        {
            string uri = string.Empty;
            if (currentDir.EndsWith("/"))
            {
                uri = ftpUristring + currentDir + filename;
            }
            else
            {
                uri = ftpUristring + currentDir + "/" + filename;
            }
            return uri;
        }
        private bool showFtpFileAndDirectory()
        {
            ServerList.Items.Clear();
            string uri = string.Empty;
            if (currentDir == "/")
            {
                uri = ftpUristring;
            }
            else
            {
                uri = ftpUristring + currentDir;
            }

            string[] urifield = uri.Split(' ');
            uri = urifield[0];
            FtpWebRequest request = CreateFtpWebRequest(uri, WebRequestMethods.Ftp.ListDirectoryDetails);

            // 获得服务器返回的响应信息
            FtpWebResponse response = GetFtpResponse(request);
            if (response == null)
            {
                return false;
            }
            LocalList.Items.Add("连接成功，服务器返回的是：" + response.StatusCode + " " + response.StatusDescription);

            // 读取网络流数据
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
            LocalList.Items.Add("获取响应流....");
            string s = streamReader.ReadToEnd();
            streamReader.Close();
            stream.Close();
            response.Close();
            LocalList.Items.Add("传输完成");

            // 处理并显示文件目录列表
            string[] ftpdir = s.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            ServerList.Items.Add("↑返回上层目录");
            int length = 0;
            for (int i = 0; i < ftpdir.Length; i++)
            {
                if (ftpdir[i].EndsWith("."))
                {
                    length = ftpdir[i].Length - 2;
                    break;
                }
            }

            for (int i = 0; i < ftpdir.Length; i++)
            {
                s = ftpdir[i];
                int index = s.LastIndexOf('\t');
                if (index == -1)
                {
                    if (length < s.Length)
                    {
                        index = length;
                    }
                    else
                    {
                        continue;
                    }
                }

                string name = s.Substring(index + 1);
                if (name == "." || name == "..")
                {
                    continue;
                }

                // 判断是否为目录，在名称前加"目录"来表示
                if (s[0] == 'd' || (s.ToLower()).Contains("<dir>"))
                {
                    string[] namefield = name.Split(' ');
                    int namefieldlength = namefield.Length;
                    string dirname;
                    dirname = namefield[namefieldlength - 1];

                    // 对齐
                    dirname = dirname.PadRight(34, ' ');
                    name = dirname;
                    // 显示目录
                    ServerList.Items.Add("[目录]" + name);
                }
            }

            for (int i = 0; i < ftpdir.Length; i++)
            {
                s = ftpdir[i];
                int index = s.LastIndexOf('\t');
                if (index == -1)
                {
                    if (length < s.Length)
                    {
                        index = length;
                    }
                    else
                    {
                        continue;
                    }
                }

                string name = s.Substring(index + 1);
                if (name == "." || name == "..")
                {
                    continue;
                }

                // 判断是否为文件
                if (!(s[0] == 'd' || (s.ToLower()).Contains("<dir>")))
                {
                    string[] namefield = name.Split(' ');
                    int namefieldlength = namefield.Length;
                    string filename;

                    filename = namefield[namefieldlength - 1];

                    // 对齐
                    filename = filename.PadRight(34, ' ');
                    name = filename;

                    // 显示文件
                    ServerList.Items.Add(name);
                }
            }

            return true;
        }

        // 获得选择的文件
        // 如果选择的是目录或者是返回上层目录，则返回null
        private string GetSelectedFile()
        {
            string filename = string.Empty;
            if (!(ServerList.SelectedIndex == -1 || ServerList.SelectedItem.ToString().Substring(0, 4) == "[目录]"))
            {
                string[] namefield = ServerList.SelectedItem.ToString().Split(' ');
                filename = namefield[0];
            }
            return filename;

        }

        private void ServerList_DoubleClick(object sender, EventArgs e)
        {
            // 点击返回上层目录
            if (ServerList.SelectedIndex == 0)
            {
                if (currentDir == "/")
                {
                    MessageBox.Show("当前目录已经是顶层目录", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                int index = currentDir.LastIndexOf("/");
                if (index == 0)
                {
                    currentDir = "/";
                }
                else
                {
                    currentDir = currentDir.Substring(0, index);
                }

                // 每次更改目录后立即刷新资源列表
                showFtpFileAndDirectory();
            }
            else
            {
                if (ServerList.SelectedIndex > 0 && ServerList.SelectedItem.ToString().Contains("[目录]"))
                {
                    if (currentDir == "/")
                    {
                        currentDir = "/" + ServerList.SelectedItem.ToString().Substring(4);

                    }
                    else
                    {
                        currentDir = currentDir + "/" + ServerList.SelectedItem.ToString().Substring(4);
                    }

                    string[] currentDirfield = currentDir.Split(' ');
                    currentDir = currentDirfield[0];
                    // 每次更改目录后立即刷新资源列表
                    showFtpFileAndDirectory();
                }
            }
        }

        private void FTPClient_Load(object sender, EventArgs e)
        {

        }

        private void FTPClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("将要关闭窗体,是否继续?", "询问", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void ServerList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
