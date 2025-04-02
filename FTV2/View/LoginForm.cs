using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Services;

namespace FTV2
{
    public partial class LoginForm : Form
    {
        public MainForm MainForm;
        public Dictionary<string, UserData> Users = new Dictionary<string, UserData>();
        public string CurrentUser { get; set; } = "操作员";

        public LoginForm()
        {
            InitializeComponent();
            CB_UserName.Text = "操作员";
            CB_UserName.Items.Add("操作员");
            CB_UserName.Items.Add("工程师");
            CB_UserName.Items.Add("管理员");

            Users.Add("操作员", new UserData() { UserType = 2, UserName = "操作员", Password = "" });
            Users.Add("管理员", new UserData() { UserType = 0, UserName = "管理员", Password = "666666" });
            UserData engineer = JsonManager.ReadJsonString<UserData>(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FTData", "engineerData");
            if (engineer == null)
                Users.Add("工程师", new UserData() { UserType = 1, UserName = "工程师", Password = "" });
            else
                Users.Add(engineer.UserName, engineer);
        }

        private void Login()
        {
            if (MainForm == null)
            {
                MainForm = new MainForm(this); MainForm.LB_实时权限显示.Text = CurrentUser; MainForm.Show();
                MainForm.HTP主界面.Selecting += new TabControlCancelEventHandler(TC_Main_Selecting);
                this.Hide();
            }
            else
            {
                MainForm.LB_实时权限显示.Text = CurrentUser;
                MainForm.Show();
                this.Hide();
            }
        }

        void TC_Main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (CurrentUser == "操作员")//禁用某个Tab
            {
                //if (e.TabPageIndex == 4) e.Cancel = true;
                //if (e.TabPageIndex == 5) e.Cancel = true;
                //if (e.TabPageIndex == 9) e.Cancel = true;
            }
        }

        private void BTN_Login_Click(object sender, EventArgs e)
        {
            if (!Users.ContainsKey(CB_UserName.Text)) return;
            if (CB_UserName.Text == "操作员")
            {
                CurrentUser = "操作员";
                Login();
            }
            else if (Users[CB_UserName.Text].Password == TB_Password.Text)
            {
                CurrentUser = Users[CB_UserName.Text].UserName;
                Login();
            }
            else
            {
                MessageBox.Show("密码错误", "登录", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }

    public class UserData
    {
        public int UserType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
