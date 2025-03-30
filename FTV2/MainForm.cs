using FTV2.View;
using Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTV2
{
    public partial class MainForm : Form
    {
        DataRepeater<Message<object>> dataRepeater = new DataRepeater<Message<object>>();
        readonly Communication com = Communication.Singleton;
        readonly ConcurrentDictionary<string, ControlConfig> ControlDic = new ConcurrentDictionary<string, ControlConfig>();

        public MainForm()
        {
            InitializeComponent();

            LoadControls();
            Task.Run(UpdateInterface);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Task.Run(async () =>
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        Message<object> d1 = new Message<object>() { Key = i.ToString(), Value = rnd.Next(0, 100), Date = DateTime.Now.ToString() };
            //        await dataRepeater.WriteDataAsync(d1);
            //        Thread.Sleep(1000);
            //    }
            //});
            //Task.Run(async () =>
            //{
            //    await dataRepeater.ParseMessageAsync(Display);
            //});
            
        }

        #region 方法
        public void RecordAndShow(string message, LogType logType, RichTextBox textBox = null, bool isShowUser = true)
        {
            //if (isShowUser)
            //    message = $"{loginForm.CurrentUser}  {message}";
            LogManager.WriteLog(message, logType);
            textBox?.Invoke(new Action(() =>
            textBox.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}")));
        }

        public bool MainButton(string question, string message, string address)
        {
            DialogResult result = MessageBox.Show(question, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                RecordAndShow(message, LogType.Modification, RTB修改);
                com.WriteVariable(true, address);
                return true;
            }
            return false;
        }

        public Dictionary<string, T> GetControls<T>(params Control[] mainControl)
        {
            Dictionary<string, T> controlDic = new Dictionary<string, T>();
            foreach (Control item in mainControl)
            {
                foreach (Control child in item.Controls)
                {
                    if (child is T control)
                        controlDic.Add(child.Name, control);
                }
            }
            return controlDic;
        }
        #endregion

        public void LoadControls()
        {
            List<ControlConfig>  上料Controls = JsonManager.Load<List<ControlConfig>>("Config", "上料界面.json");
            foreach (var control in 上料Controls)
            {
                ControlDic.TryAdd(control.CtrlName, control);
                control.AddControl(TP上料, null, new System.Drawing.Font("Times New Roman", 8));
                control.ControlInstance.MouseDown += Output_MouseDown;
                control.ControlInstance.MouseUp += Output_MouseUp;
            }
        }

        public void UpdateData()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(50);
                    com.RefreshData();
                }
                catch (Exception)
                {

                }
            }
        }

        public void UpdateInterface()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(100);
                    foreach (var pos in com.Location)
                    {
                        ControlDic.AddOrUpdate(pos.Key, new ControlConfig(new System.Drawing.Point(0, 0), pos.Key, pos.Key, pos.Key),
                            (key, oldValue) => { oldValue.SetText(pos.Value.ToString()); return oldValue; });
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        #region 窗口
        private void TSB导入_Click(object sender, EventArgs e)
        {
            new ImportForm().Show();
        }

        private void TSB测试_Click(object sender, EventArgs e)
        {
            try
            {
                com.Compolet.Open();
                Task.Run(UpdateData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void Output_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                FSB状态.Text = $"{button.Name}:{button.Tag}";
                com.WriteVariable(true, button.Tag.ToString());
            }
        }

        private void Output_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                com.WriteVariable(false, button.Tag.ToString());
            }
        }

        private void BTN主界面_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                switch (button.Tag)
                {
                    case "manual":
                        MainButton("是否切换手动模式？", "切换为手动模式", "PlcInIO[2]");
                        break;
                    case "auto":
                        MainButton("人工确认：工作盘数选择无误后再确认是否切换自动模式？", "切换为自动模式", "PlcInIO[28]");
                        break;
                    case "run":
                        //if (CB_TypeOfTray.Text == "" || CB_Socket类.Text == "" || CB_工位.Text == "" || CB_工作盘数.Text == "")
                        //{
                        //    MessageBox.Show("类别未选择完成，不能启动", "提示");
                        //    return;
                        //}
                        MainButton("是否开始自动运行？", "自动运行开始", "PlcInIO[4]");
                        break;
                    case "stop":
                        MainButton("是否停止自动？", "自动运行停止", "PlcInIO[5]");
                        break;
                    case "ini":
                        //if (CB_TypeOfTray.Text == "" || CB_Socket类.Text == "" || CB_工位.Text == "" || CB_工作盘数.Text == "")
                        //{
                        //    MessageBox.Show("类别未选择完成，不能启动", "提示");
                        //    return;
                        //}
                        if (MainButton("是否初始化？", "初始化", "PlcInIO[6]"))
                        {
                            Thread.Sleep(2000);
                            InitialForm initialForm = new InitialForm();
                            initialForm.ShowDialog();
                        }
                        break;
                    default: break;
                }
            }
        }


    }
}
