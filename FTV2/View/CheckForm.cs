using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Services;

namespace FTV2
{
    public partial class CheckForm : Form
    {
        readonly List<string> messageList = new List<string>();
        public string FileName { get; set; } = "报警记录";
        public string FilePath { get; set; } = "Warning";

        public CheckForm(string type = "报警记录")
        {
            InitializeComponent();

            Text = type;
            switch (type)
            {
                case "报警记录":
                    FileName = "报警记录";
                    FilePath = "Warning";
                    break;
                case "更改记录":
                    FileName = "更改记录";
                    FilePath = "Modification";
                    CB_条件.Items.Clear();
                    CB_条件.Items.Add("全部");
                    break;
                case "监视记录":
                    FileName = "监视记录";
                    FilePath = "Monitor";
                    CB_条件.Items.Clear();
                    CB_条件.Items.Add("全部");
                    break;
                default:
                    FileName = "报警记录";
                    FilePath = "Warning";
                    break;
            }
        }

        private void UpdateLog(ListBox listBox, string type = "报警")
        {
            listBox.DataSource = null;
            messageList.Clear();
            var message = LogFile.ReadLog($"{DTP_CheckDate.Value:yyyyMMdd}{FileName}", FilePath);
            if (type != null && message[0] != "没有数据")
            {
                var selectMessage = message.Where(x => x.Split(']').Length >= 2 && new string(x.Split(']')[1].Take(2).ToArray()) == type).ToArray();
                messageList.AddRange(selectMessage);
            }
            else
            {
                messageList.AddRange(message);
            }
            listBox.DataSource = messageList;
        }

        private void BTN_错误日志加载_Click(object sender, EventArgs e)
        {
            try
            {
                switch (CB_条件.Text)
                {
                    case "报警":
                        UpdateLog(LB_Log, "报警");
                        break;
                    case "提示":
                        UpdateLog(LB_Log, "提示");
                        break;
                    case "工位":
                        UpdateLog(LB_Log, "工位");
                        break;
                    case "全部":
                        UpdateLog(LB_Log, null);
                        break;
                    default:
                        UpdateLog(LB_Log, null);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载文件失败。" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BTN_错误日志删除_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB_Log.SelectedItems.Count == 0) return;
                foreach (object item in LB_Log.SelectedItems)
                    messageList.RemoveAll(s => s.Contains(item.ToString()));
                LB_Log.DataSource = null;
                LB_Log.DataSource = messageList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败。" + ex.Message, "日志删除");
            }
        }

        private void TSM_查看_Click(object sender, EventArgs e)
        {
            ShowForm showForm = new ShowForm(LB_Log.SelectedItem.ToString());
            showForm.Show();
        }
    }
}
