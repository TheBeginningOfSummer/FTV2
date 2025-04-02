using FTV2.View;
using Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTV2
{
    public partial class MainForm : Form
    {
        //DataRepeater<Message<object>> dataRepeater = new DataRepeater<Message<object>>();
        readonly Communication com = Communication.Singleton;

        #region 加载的控件(地址作为键值更新数据)
        readonly ConcurrentDictionary<string, ControlConfig> UploadInterface = new ConcurrentDictionary<string, ControlConfig>();
        readonly ConcurrentDictionary<string, ControlConfig> CalibInterface = new ConcurrentDictionary<string, ControlConfig>();
        readonly ConcurrentDictionary<string, ControlConfig> TestInterface = new ConcurrentDictionary<string, ControlConfig>();
        #endregion

        public MainForm()
        {
            InitializeComponent();

            LoadControls();
            Task.Run(UpdateInterface);
        }

        private void Form1_Load(object sender, EventArgs e)
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

        public string GetBracketString(string message)
        {
            string value = Regex.Match(message, @"\[(.*?)\]").Groups[1].Value;
            if (value == "")
                return message;
            else
                return value;
        }

        public void RecordAndShow(string message, LogType logType, RichTextBox textBox = null, bool isShowUser = true)
        {
            //if (isShowUser)
            //    message = $"{loginForm.CurrentUser}  {message}";
            LogManager.WriteLog(message, logType);
            textBox?.Invoke(new Action(() =>
            textBox.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}")));
        }

        public void SwitchButtonColor(Button button, Color color, bool colorSwitch)
        {
            string index = Regex.Match((string)button.Tag, @"(?i)(?<=\[)(.*)(?=\])").Value;
            if (colorSwitch)
            {
                com.WriteVariable(true, $"PlcInIO1[{index}]");
                button.BackColor = color;
            }
            else
            {
                com.WriteVariable(false, $"PlcInIO1[{index}]");
                button.BackColor = Color.Transparent;
            }
        }

        public void IsWrite(bool isOK, string message)
        {
            if (!isOK)
            {
                MessageBox.Show($"参数写入失败。请检查连接状态。State:{message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteLog($"参数写入失败。请检查连接状态。State:{message}", LogType.Error);
            }
        }

        public int GetPlcInID(string info)
        {
            switch (info)
            {
                case "单目SK1位置": return 3;
                case "单目SK2位置": return 4;
                case "单目SK3位置": return 5;
                case "单目SK4位置": return 6;

                case "四目SK1-1位置": return 7;
                case "四目SK1-2位置": return 8;
                case "四目SK2-1位置": return 9;
                case "四目SK2-2位置": return 10;
                case "四目SK3-1位置": return 11;
                case "四目SK3-2位置": return 12;
                case "四目SK4-1位置": return 13;
                case "四目SK4-2位置": return 14;

                case "单目sk1激光点X1": return 15;
                case "单目sk1激光点X2": return 16;
                case "单目sk2激光点X1": return 17;
                case "单目sk2激光点X2": return 18;
                case "单目sk3激光点X1": return 19;
                case "单目sk3激光点X2": return 20;
                case "单目sk4激光点X1": return 21;
                case "单目sk4激光点X2": return 22;

                case "四目sk1激光点X1": return 23;
                case "四目sk1激光点X2": return 24;
                case "四目sk1激光点X3": return 25;
                case "四目sk1激光点X4": return 26;
                case "四目sk2激光点X1": return 27;
                case "四目sk2激光点X2": return 28;
                case "四目sk2激光点X3": return 29;
                case "四目sk2激光点X4": return 30;
                case "四目sk3激光点X1": return 31;
                case "四目sk3激光点X2": return 32;
                case "四目sk3激光点X3": return 33;
                case "四目sk3激光点X4": return 34;
                case "四目sk4激光点X1": return 35;
                case "四目sk4激光点X2": return 36;
                case "四目sk4激光点X3": return 37;
                case "四目sk4激光点X4": return 38;

                case "BYY_下视觉夹爪1=3": return 2;
                case "BYY_下视觉夹爪2=4": return 3;
                case "BYY_下视觉夹爪3=5": return 4;
                case "BYY_下视觉夹爪4=6": return 5;

                case "BYY_夹爪1相对视觉移动PB": return 1;
                case "BYY_夹爪2相对视觉移动PB": return 2;
                case "BYY_夹爪3相对视觉移动PB": return 3;
                case "BYY_夹爪4相对视觉移动PB": return 4;

                case "单目sk1激光点Y1": return 15;
                case "单目sk1激光点Y2": return 16;
                case "单目sk2激光点Y1": return 17;
                case "单目sk2激光点Y2": return 18;
                case "单目sk3激光点Y1": return 19;
                case "单目sk3激光点Y2": return 20;
                case "单目sk4激光点Y1": return 21;
                case "单目sk4激光点Y2": return 22;
                case "四目sk1激光点Y1": return 23;
                case "四目sk1激光点Y2": return 24;
                case "四目sk1激光点Y3": return 25;
                case "四目sk1激光点Y4": return 26;
                case "四目sk2激光点Y1": return 27;
                case "四目sk2激光点Y2": return 28;
                case "四目sk2激光点Y3": return 29;
                case "四目sk2激光点Y4": return 30;
                case "四目sk3激光点Y1": return 31;
                case "四目sk3激光点Y2": return 32;
                case "四目sk3激光点Y3": return 33;
                case "四目sk3激光点Y4": return 34;
                case "四目sk4激光点Y1": return 35;
                case "四目sk4激光点Y2": return 36;
                case "四目sk4激光点Y3": return 37;
                case "四目sk4激光点Y4": return 38;

                default: return -1;
            }
        }
        #endregion

        #region 按钮方法
        /// <summary>
        /// 主界面按钮方法
        /// </summary>
        /// <param name="question"></param>
        /// <param name="message"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool MainButton(string question, string message, string address)
        {
            DialogResult result = MessageBox.Show(question, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                RecordAndShow(message, LogType.Modification, RTB修改);
                bool isWrite = com.WriteVariable(true, address);
                IsWrite(isWrite, message);
                return isWrite;
            }
            return false;
        }
        /// <summary>
        /// 示教按钮方法
        /// </summary>
        /// <param name="button">传入的按钮</param>
        /// <param name="delay">按钮弹起延时时间</param>
        /// <param name="question">提示</param>
        /// <param name="message">信息</param>
        /// <returns></returns>
        public async Task CaliButtonAsync(Button button, int delay = 1000, string question = "您是否确定此操作？", string message = "哪个按钮按下")
        {
            DialogResult result = MessageBox.Show(question, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //MessageBox.Show(message);
                string address = button.Tag.ToString();
                if (com.WriteVariable(true, address))
                {
                    await Task.Delay(delay);
                    RecordAndShow($"{message}", LogType.Modification, RTB修改);
                    bool isWrite = com.WriteVariable(false, address);
                    SwitchButtonColor(button, Color.LightGreen, true);
                    IsWrite(isWrite, "示教中");
                }
                else
                    IsWrite(false, "示教中");
            }
        }
        /// <summary>
        /// 示教值写入
        /// </summary>
        /// <param name="valueBox"></param>
        /// <param name="message"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public bool CaliWriteValue(Control valueBox, string message = "输入错误请检查", int min = 0, int max = 26)
        {
            if (!double.TryParse(valueBox.Text, out double value))
            {
                MessageBox.Show(message, "提示");
                return false;
            }
            if (value >= min && value <= max)
            {
                if (com.WriteVariable(value, valueBox.Tag.ToString()))
                {
                    valueBox.Text = "";
                    return true;
                }
                else
                {
                    MessageBox.Show("参数写入失败，请检查链接。", "提示");
                    return false;
                }
            }
            else
            {
                MessageBox.Show($"输入错误请检查,请输入{min}-{max}之间的整数", "提示");
                return false;
            }
        }
        #endregion

        #region 加载更新
        public void LoadControls()
        {
            List<ControlConfig> 上料Controls = JsonManager.Load<List<ControlConfig>>("Config", "上料界面.json");
            foreach (var control in 上料Controls)
            {
                control.AddTo(TP上料, null, null);
                control.SourceControl.MouseDown += Output_MouseDown;
                control.SourceControl.MouseUp += Output_MouseUp;
                UploadInterface.TryAdd(control.SourceControl.Tag.ToString(), control);
            }

            List<ControlConfig> 示教Controls = JsonManager.Load<List<ControlConfig>>("Config", "示教界面.json");
            foreach (var control in 示教Controls)
            {
                foreach (var child in control.Configs)
                {
                    if (child is ButtonConfig button)
                    {
                        if (child.Configs.Count == 0)
                            button.DataProcessed += BTN示教界面_Click;
                        else//有需要写入值的textbox
                        {
                            button.MouseDown += BTN示教值写入_MouseDown;
                            button.MouseUp += BTN示教值写入_MouseUp;
                        }
                    }
                    else if (child is LabelConfig) continue;
                    CalibInterface.TryAdd(child.SourceControl.Tag.ToString(), child);
                }
                control.AddTo(TP示教, null, null);
                //CalibInterface.TryAdd(control.ControlInstance.Name, control);
            }

            List<ControlConfig> 测试Controls = JsonManager.Load<List<ControlConfig>>("Config", "测试界面.json");
            foreach (var control in 测试Controls)
            {
                foreach (var child in control.Configs)
                {
                    if (child is ButtonConfig button)
                    {
                        child.SourceControl.MouseDown += Output_MouseDown;
                        child.SourceControl.MouseUp += Output_MouseUp;
                    }
                    if (child is ComboBoxConfig combo)
                    {
                        combo.DataProcessed += Combo_DataProcessed;
                    }
                    TestInterface.TryAdd(child.SourceControl.Tag.ToString(), child);
                }
                if (control is ButtonConfig)
                {
                    control.SourceControl.MouseDown += Output_MouseDown;
                    control.SourceControl.MouseUp += Output_MouseUp;
                }
                control.AddTo(TP测试, null, null);
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

        public void UpdateInterface(KeyValuePair<string, double> pair, ConcurrentDictionary<string, ControlConfig> Interface)
        {
            if (!Interface.ContainsKey(pair.Key)) return;
            Interface.AddOrUpdate(pair.Key, new ControlConfig(new Point(0, 0), pair.Key, pair.Key, pair.Key),
                            (key, oldValue) => { oldValue.SetText(pair.Value.ToString()); return oldValue; });
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
                        UpdateInterface(pos, UploadInterface);
                        UpdateInterface(pos, TestInterface);
                    }
                }
                catch (Exception)
                {

                }
            }
        }
        #endregion

        #region 窗口加载
        private void TSB导入_Click(object sender, EventArgs e)
        {
            new ImportForm().Show();
        }

        private void TSB测试_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 界面按钮事件
        private void Combo_DataProcessed(object sender, EventArgs e)
        {
            if (sender is ComboBoxConfig combo)
            {
                FSB状态.Text = $"{combo.SourceControl.Text}:{combo.Address} code:{GetPlcInID(combo.SourceControl.Text)}";
                com.WriteVariable(GetPlcInID(combo.SourceControl.Text), combo.Address);
            }
        }

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
                    case "reset":
                        MainButton("报警复位？", "报警复位", "PlcInIO[0]");
                        break;
                    case "ringstop":
                        MainButton("蜂鸣停止？", "蜂鸣停止", "PlcInIO[1]");
                        break;
                    case "manual":
                        MainButton("是否切换手动模式？", "切换为手动模式", "PlcInIO[2]");
                        break;
                    case "auto":
                        MainButton("人工确认：工作盘数选择无误后再确认是否切换自动模式？", "切换为自动模式", "PlcInIO[3]");
                        break;
                    case "run":
                        //if (CB_TypeOfTray.Text == "" || CB_Socket类.Text == "" || CB_工位.Text == "" || CB_工作盘数.Text == "")
                        //{
                        //    MessageBox.Show("类别未选择完成，不能启动", "提示");
                        //    return;
                        //}
                        MainButton("是否开始自动运行？", "自动运行开始", "PlcInIO[5]");
                        break;
                    case "stop":
                        MainButton("是否停止自动？", "自动运行停止", "PlcInIO[6]");
                        break;
                    case "ini":
                        //if (CB_TypeOfTray.Text == "" || CB_Socket类.Text == "" || CB_工位.Text == "" || CB_工作盘数.Text == "")
                        //{
                        //    MessageBox.Show("类别未选择完成，不能启动", "提示");
                        //    return;
                        //}
                        if (MainButton("是否初始化？", "初始化", "PlcInIO[7]"))
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

        private async void BTN示教界面_Click(object sender, DataEventArgs e)
        {
            if (sender is ButtonConfig button)
            {
                FSB状态.Text = $"{button.SourceControl.Name}:{button.SourceControl.Tag}";
                if (CalibInterface.TryGetValue($"TXB[{button.CtrlName}]", out ControlConfig controlConfig))//此处有问题，考虑示教控件的键如何设置
                    await CaliButtonAsync((Button)button.SourceControl, message: $"示教 [{button.CtrlName}] 按下，当前值:{controlConfig.SourceControl.Text}");
                else
                    await CaliButtonAsync((Button)button.SourceControl, message: $"示教 [{button.CtrlName}] 按下");
            }
        }

        private void BTN示教值写入_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is ButtonConfig button)
            {
                FSB状态.Text = $"{button.SourceControl.Name}:{button.SourceControl.Tag}";
                string[] info = button.SourceControl.Tag.ToString().Split(';');
                foreach (ControlConfig textBox in button.Configs)
                {
                    bool isOK;
                    if (button.CtrlName == "钧舵打开小位置设置")
                        isOK = CaliWriteValue(textBox.SourceControl, "输入错误请检查,请输入钧舵夹爪打开小位置值[40-（夹爪夹持方向产品的尺寸+4）。4指的是左右各留2mm的夹持余量，可根据实际情况进行调整]。 如：W9产品-夹爪夹持方向产品的尺寸为24mm，则输入钧舵夹爪打开小位置值为12mm；W7产品-夹爪夹持方向产品的尺寸为18mm，则输入钧舵夹爪打开小位置值为18mm。");
                    else if (button.CtrlName == "判断范围写入")
                        isOK = CaliWriteValue(textBox.SourceControl, max: int.MaxValue);
                    else
                        isOK = CaliWriteValue(textBox.SourceControl, min: int.MinValue, max: int.MaxValue);
                    if (!isOK) return;
                }
                com.WriteVariable(true, info[0]);
            }
        }

        private void BTN示教值写入_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is ButtonConfig button)
            {
                string[] info = button.SourceControl.Tag.ToString().Split(';');
                com.WriteVariable(false, info[0]);
                RecordAndShow($"示教值写入 [{button.CtrlName}]", LogType.Modification, RTB修改);
            }
        }
        #endregion

    }
}
