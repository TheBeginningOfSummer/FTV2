using FTV2.View;
using Model;
using Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTV2
{
    public partial class MainForm : Form
    {
        readonly Communication com = Communication.Singleton;
        LoginForm loginForm;
        public TrayManager trayManager = new TrayManager();

        //托盘类型
        string currentTrayType = "";
        //临时变量存储，切换型号时临时存储上一型号
        string tempTrayType = "";
        //加载的帮助文档信息
        readonly Dictionary<string, string> helpInfo;
        //报警数据
        readonly Dictionary<string, string> alarmInfo;
        //当前报警信息
        readonly List<string> currentWarning = new List<string>();

        #region 加载的控件(地址作为键值更新数据)
        readonly ConcurrentDictionary<string, ControlConfig> UploadInterface = new ConcurrentDictionary<string, ControlConfig>();
        readonly ConcurrentDictionary<string, ControlConfig> CalibInterface = new ConcurrentDictionary<string, ControlConfig>();
        readonly ConcurrentDictionary<string, ControlConfig> TestInterface = new ConcurrentDictionary<string, ControlConfig>();
        readonly ConcurrentDictionary<string, ControlConfig> IOInterface = new ConcurrentDictionary<string, ControlConfig>();
        #endregion

        #region 监视变量
        private int monitorValue1;
        public int MonitorValue1
        {
            get { return monitorValue1; }
            set
            {
                if (monitorValue1 != value)
                {
                    RecordMonitorInfo(value, "工装移入低温");
                }
                monitorValue1 = value;
            }
        }

        private int monitorValue2;
        public int MonitorValue2
        {
            get { return monitorValue2; }
            set
            {
                if (monitorValue2 != value)
                {
                    RecordMonitorInfo(value, "工装移入高温");
                }
                monitorValue2 = value;
            }
        }

        private int monitorValue3;
        public int MonitorValue3
        {
            get { return monitorValue3; }
            set
            {
                if (monitorValue3 != value)
                {
                    RecordMonitorInfo(value, "工装移入");
                }
                monitorValue3 = value;
            }
        }

        private int monitorValue4;
        public int MonitorValue4
        {
            get { return monitorValue4; }
            set
            {
                if (monitorValue4 != value)
                {
                    RecordMonitorInfo(value, "工装移出");
                }
                monitorValue4 = value;
            }
        }

        private int monitorValue5;
        public int MonitorValue5
        {
            get { return monitorValue5; }
            set
            {
                if (monitorValue5 != value)
                {
                    RecordMonitorInfo(value, "测试完成");
                }
                monitorValue5 = value;
            }
        }

        private int monitorValue6;
        public int MonitorValue6
        {
            get { return monitorValue6; }
            set
            {
                if (monitorValue6 != value)
                {
                    RecordMonitorInfo(value, "热辐射板移动");
                }
                monitorValue6 = value;
            }
        }
        #endregion

        public MainForm(LoginForm loginForm)
        {
            InitializeComponent();
            this.loginForm = loginForm;

            try
            {
                #region 数据加载
                //帮助文档信息加载
                helpInfo = JsonManager.ReadJsonString<Dictionary<string, string>>(Environment.CurrentDirectory + "\\Config", "HelpInfo.json");
                alarmInfo = JsonManager.ReadJsonString<Dictionary<string, string>>(Environment.CurrentDirectory + "\\Config", "Alarm.json");
                #endregion

                #region 数据库界面
                //数据库初始化
                SensorDataManager.InitializeDatabase();
                COBSensorType.Items.Add("");
                //设置查询时间上下限
                DTPMin.Value = Convert.ToDateTime(DateTime.Now.AddDays(-7));
                DTPMax.Value = Convert.ToDateTime(DateTime.Now.AddDays(1));
                #endregion

                #region 托盘数据加载
                //下拉列表托盘类型加载
                UpdateTypeToCOB(trayManager);
                //加载上次的托盘状态
                trayManager.LoadTraysData(currentTrayType);
                //更新托盘状态到界面
                trayManager.UpdateTrayLabels(PNMapping);
                #endregion

                LoadHelpMenu();
                LoadControls();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "程序初始化", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteLog("程序初始化：" + e.Message, LogType.Error);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                com.Compolet.Open();
                Task.Run(UpdateData);
                Task.Run(UpdateInterface);
                Task.Run(UpdateAlarm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region 其他方法
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
            if (isShowUser)
                message = $"{loginForm.CurrentUser}  {message}";
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

        public void RecordMonitorInfo(int value, string message)
        {
            switch (value)
            {
                case 1:
                    RecordAndShow($"{message}-触发第一个通道。", LogType.Monitor, RTBMonitor);
                    break;
                case 2:
                    RecordAndShow($"{message}-触发第二个通道。", LogType.Monitor, RTBMonitor);
                    break;
                case 3:
                    RecordAndShow($"{message}-触发第三个通道。", LogType.Monitor, RTBMonitor);
                    break;
                case 4:
                    RecordAndShow($"{message}-触发第四个通道。", LogType.Monitor, RTBMonitor);
                    break;
                case 12:
                    RecordAndShow($"{message}-触发第一、二个通道。", LogType.Monitor, RTBMonitor);
                    break;
                case 13:
                    RecordAndShow($"{message}-触发第一、三个通道。", LogType.Monitor, RTBMonitor);
                    break;
                case 14:
                    RecordAndShow($"{message}-触发第一、四个通道。", LogType.Monitor, RTBMonitor);
                    break;
                case 23:
                    RecordAndShow($"{message}-触发第二、三个通道。", LogType.Monitor, RTBMonitor);
                    break;
                case 24:
                    RecordAndShow($"{message}-触发第二、四个通道。", LogType.Monitor, RTBMonitor);
                    break;
                case 34:
                    RecordAndShow($"{message}-触发第三、四个通道。", LogType.Monitor, RTBMonitor);
                    break;
                case 123:
                    RecordAndShow($"{message}-触发第一、二、三个通道。", LogType.Monitor, RTBMonitor);
                    break;
                case 234:
                    RecordAndShow($"{message}-触发第二、三、四个通道。", LogType.Monitor, RTBMonitor);
                    break;
                case 134:
                    RecordAndShow($"{message}-触发第一、三、四个通道。", LogType.Monitor, RTBMonitor);
                    break;
                case 124:
                    RecordAndShow($"{message}-触发第一、二、四个通道。", LogType.Monitor, RTBMonitor);
                    break;
                case 166:
                    RecordAndShow($"{message}-触发全部通道。", LogType.Monitor, RTBMonitor);
                    break;
                default:
                    break;
            }
        }

        public void DataToExcel(DataGridView m_DataView)
        {
            SaveFileDialog kk = new SaveFileDialog
            {
                Title = "保存EXCEL文件",
                Filter = "表格文件(*.xls) |*.xls |所有文件(*.*) |*.*",
                FilterIndex = 1,
                FileName = DateTime.Now.ToString("D")
            };
            if (kk.ShowDialog() == DialogResult.OK)
            {
                string FileName = kk.FileName;
                if (File.Exists(FileName))
                    File.Delete(FileName);
                FileStream objFileStream;
                StreamWriter objStreamWriter;
                string strLine = "";
                objFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
                objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
                for (int i = 0; i < m_DataView.Columns.Count; i++)
                {
                    if (m_DataView.Columns[i].Visible == true)
                    {
                        strLine = strLine + m_DataView.Columns[i].HeaderText.ToString() + Convert.ToChar(9);
                    }
                }
                objStreamWriter.WriteLine(strLine);
                strLine = "";

                for (int i = 0; i < m_DataView.Rows.Count; i++)
                {
                    if (m_DataView.Columns[0].Visible == true)
                    {
                        if (m_DataView.Rows[i].Cells[0].Value == null)
                            strLine = strLine + " " + Convert.ToChar(9);
                        else
                            strLine = strLine + m_DataView.Rows[i].Cells[0].Value.ToString() + Convert.ToChar(9);
                    }
                    for (int j = 1; j < m_DataView.Columns.Count; j++)
                    {
                        if (m_DataView.Columns[j].Visible == true)
                        {
                            if (m_DataView.Rows[i].Cells[j].Value == null)
                                strLine = strLine + " " + Convert.ToChar(9);
                            else
                            {
                                string rowstr = m_DataView.Rows[i].Cells[j].Value.ToString();
                                if (rowstr.IndexOf("\r\n") > 0)
                                    rowstr = rowstr.Replace("\r\n", " ");
                                if (rowstr.IndexOf("\t") > 0)
                                    rowstr = rowstr.Replace("\t", " ");
                                strLine = strLine + rowstr + Convert.ToChar(9);
                            }
                        }
                    }
                    objStreamWriter.WriteLine(strLine);
                    strLine = "";
                }
                objStreamWriter.Close();
                objFileStream.Close();
                MessageBox.Show(this, "保存表格成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //吸嘴使用次数检测
        public void CheckCount(double count, double times, ref bool isShow, string message = "上料吸嘴1使用次数已达上限，请及时更换")
        {
            double Vac1Count = count - times;
            double Vac1Mod = Vac1Count % 100;//每100次提醒一次
            if (Vac1Count < 0)
            {
                isShow = true;
            }
            else
            {
                if (Vac1Count != 0 && Vac1Mod != 0)
                {
                    isShow = true;
                }
            }

            if (isShow)
            {
                if (Vac1Count >= 0)
                {
                    if (Vac1Count == 0 || Vac1Mod == 0)
                    {
                        DialogResult result = MessageBox.Show(message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes) isShow = false;
                    }
                }
            }
        }

        //更新界面传感器类型下拉列表
        public void UpdateTypeToCOB(TrayManager trayManager)
        {
            if (trayManager == null) return;
            CB_TypeOfTray.Items.Clear();
            foreach (var trayType in trayManager.TrayType)
                CB_TypeOfTray.Items.Add(trayType.Key);
        }

        public void UpdateListToRTB(List<string> list, RichTextBox textBox)
        {
            textBox.Invoke(new Action(() =>
            {
                textBox.Clear();
                foreach (var item in list)
                    textBox.AppendText(item + Environment.NewLine);
            }));
        }

        public void UpdateLabel(Label label, Color color1, Color color2, string text1 = "  ", string text2 = "  ")
        {
            if (com.PLCOutput.TryGetValue(label.Tag.ToString(), out bool value))
            {
                if (label.IsHandleCreated)
                {
                    label.Invoke(new Action(() =>
                    {
                        if (value)
                        {
                            label.BackColor = color1;
                            label.Text = text1;
                        }
                        else
                        {
                            label.BackColor = color2;
                            label.Text = text2;
                        }
                    }));
                }
                else
                {
                    if (value)
                    {
                        label.BackColor = color1;
                        label.Text = text1;
                    }
                    else
                    {
                        label.BackColor = color2;
                        label.Text = text2;
                    }
                }
            }
        }

        public void UpdateTextBox(TextBox textBox, ConcurrentDictionary<string, double> pairs)
        {
            if (pairs.TryGetValue(textBox.Tag.ToString(), out double value))
            {
                if (textBox.IsHandleCreated)
                {
                    textBox.Invoke(new Action(() =>
                    {
                        textBox.Text = value.ToString();
                    }));
                }
                else
                {
                    textBox.Text = value.ToString();
                }
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
        public bool MainButton(string question, string message, string address, bool value = true)
        {
            DialogResult result = MessageBox.Show(question, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                RecordAndShow(message, LogType.Modification, RTBModify);
                bool isWrite = com.WriteVariable(value, address);
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
                    RecordAndShow($"{message}", LogType.Modification, RTBModify);
                    bool isWrite = com.WriteVariable(false, address);
                    SwitchButtonColor(button, Color.LightGreen, true);
                    //IsWrite(isWrite, "示教中");
                }
                else
                    IsWrite(false, "按钮未触发");
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
        private void LoadHelpMenu()
        {
            if (helpInfo == null) return;
            foreach (var item in helpInfo)
            {
                if (item.Key == "BrowserPath") continue;
                ToolStripMenuItem menu = new ToolStripMenuItem()
                {
                    Name = $"TSM{item.Key}",
                    Tag = item.Key,
                    Text = item.Key
                };
                menu.Click += TSM打开文档_Click;
                TDBHelp.DropDownItems.Add(menu);
            }
        }

        private void TSM打开文档_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem Menu = (ToolStripMenuItem)sender;
                string path = $"{Environment.CurrentDirectory}\\Documents\\{helpInfo[(string)Menu.Tag]}";
                path = path.Replace(" ", "\" \"");
                Process.Start(helpInfo["BrowserPath"], path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载帮助文件失败。{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

            //List<ControlConfig> 示教Controls = JsonManager.Load<List<ControlConfig>>("Config", "示教界面.json");
            //foreach (var control in 示教Controls)
            //{
            //    foreach (var child in control.Configs)
            //    {
            //        if (child is ButtonConfig button)
            //        {
            //            if (child.Configs.Count == 0)
            //                button.DataProcessed += BTN示教界面_Click;
            //            else//有需要写入值的textbox
            //            {
            //                button.MouseDown += BTN示教值写入_MouseDown;
            //                button.MouseUp += BTN示教值写入_MouseUp;
            //            }
            //        }
            //        else if (child is LabelConfig) continue;
            //        CalibInterface.TryAdd(child.SourceControl.Tag.ToString(), child);
            //    }
            //    control.AddTo(TP示教, null, null);
            //}

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

            List<ControlConfig> IOControls = JsonManager.Load<List<ControlConfig>>("Config", "IO界面.json");
            foreach (var ioItem in IOControls)
            {
                ioItem.AddTo(TPIO信息, null, null);
                IOInterface.TryAdd(ioItem.SourceControl.Tag.ToString(), ioItem);
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
                    #region 数据处理
                    //索引更新
                    if (!string.IsNullOrEmpty(com.TestInformation["PLC测试信息[20]"]))
                    {
                        //当前托盘索引更新
                        trayManager.CurrentTrayIndex = int.Parse(com.TestInformation["PLC测试信息[20]"]);
                    }
                    else
                    {
                        //托盘索引为0时，无法为托盘附码
                        trayManager.CurrentTrayIndex = 0;
                    }
                    //托盘数据初始化
                    if (com.FlagBits["PLC标志位[2]"])//20个托盘已摆好
                    {
                        if (currentTrayType != "" && currentTrayType != " ")
                        {
                            //初始化
                            trayManager.InitializeTrays(currentTrayType);
                            trayManager.UpdateTrayLabels(PNMapping);
                            com.WriteVariable(false, "PLC标志位[2]");
                            //托盘初始化完成,PLC检测到此值为true后，将PLC标志位[2]置为false
                            com.WriteVariable(true, "PC标志位[2]");
                        }
                    }
                    //托盘扫码完成
                    if (com.FlagBits["PLC标志位[0]"])//托盘扫码完成
                    {
                        //将扫到的托盘码赋值给托盘管理类
                        trayManager.UpdateTrayNumber(com.TestInformation["PLC测试信息[4]"]);
                        //托盘扫码完成,PLC检测到此值为true后，将PLC标志位[0]置为false
                        com.WriteVariable(false, "PLC标志位[0]");
                        com.WriteVariable(true, "PC标志位[0]");
                    }
                    //产品测试完成
                    if (com.FlagBits["PLC标志位[1]"])
                    {
                        Sensor sensor = new Sensor(
                            com.TestInformation["PLC测试信息[0]"],
                            com.TestInformation["PLC测试信息[1]"],
                            com.TestInformation["PLC测试信息[2]"],
                            int.Parse(com.TestInformation["PLC测试信息[3]"]),
                            com.TestInformation["PLC测试信息[4]"],
                            int.Parse(com.TestInformation["PLC测试信息[5]"]),
                            com.TestInformation["PLC测试信息[6]"],
                            com.TestInformation["PLC测试信息[7]"],
                            com.TestInformation["PLC测试信息[8]"]);
                        //Mapping图更新
                        trayManager.UpdateSensorDataInTray(sensor);
                        //数据存储到缓存文件
                        trayManager.SaveTraysData();
                        //数据库数据存储
                        SensorDataManager.AddSensor(sensor);
                        //产品信息录入完成。将PLC标志位[1]置为false，PC标志位置true，表示准备好下一次的录入
                        com.WriteVariable(false, "PLC标志位[1]");
                        com.WriteVariable(true, "PC标志位[1]");
                    }
                    //数据监视
                    MonitorValue1 = com.MonitorValue["PCwrite[2]"];
                    MonitorValue2 = com.MonitorValue["PCwrite[3]"];
                    MonitorValue3 = com.MonitorValue["PCwrite[4]"];
                    MonitorValue4 = com.MonitorValue["PCwrite[5]"];
                    MonitorValue5 = com.MonitorValue["PCwrite[14]"];
                    MonitorValue6 = com.MonitorValue["PCwrite[15]"];
                    #endregion
                }
                catch (Exception e)
                {
                    LogManager.WriteLog($"数据更新错误。{e.Message}", LogType.Error);
                }
            }
        }

        public void UpdateInterfaceText(KeyValuePair<string, double> pair, ConcurrentDictionary<string, ControlConfig> Interface)
        {
            if (!Interface.ContainsKey(pair.Key)) return;
            Interface.AddOrUpdate(pair.Key, new ControlConfig(new Point(0, 0), pair.Key, pair.Key, pair.Key),
                            (key, oldValue) => { oldValue.SetText(pair.Value.ToString()); return oldValue; });
        }

        public void UpdateInterfaceColor(KeyValuePair<string, bool> pair, ConcurrentDictionary<string, ControlConfig> Interface)
        {
            if (!Interface.ContainsKey(pair.Key)) return;
            Color color = Color.Gray;
            if (pair.Value)
                color = Color.Lime;
            else
                color = Color.LightGray;
            Interface.AddOrUpdate(pair.Key, new ControlConfig(new Point(0, 0), pair.Key, pair.Key, pair.Key),
                            (key, oldValue) => { oldValue.SetColor(color); return oldValue; });
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
                        UpdateInterfaceText(pos, UploadInterface);
                        UpdateInterfaceText(pos, TestInterface);
                    }
                    foreach (var output in com.PLCOutput)
                    {
                        UpdateInterfaceColor(output, IOInterface);
                    }
                    UpdateLabel(LB_Connection, Color.Lime, Color.White, "本机已连接", "本机未连接");
                    UpdateLabel(LB_初始化状态, Color.Lime, Color.White, "初始化完成", "初始化未完成");
                    UpdateLabel(LB_手动状态, Color.Lime, Color.White, "自动模式", "手动模式");
                    UpdateLabel(LB_自动运行状态, Color.Lime, Color.White, "运行中", "停止中");
                    UpdateLabel(LB_初始化提示, Color.Lime, Color.Red, "无需初始化，可自动运行", "请先将机台初始化，再自动运行");
                    UpdateTextBox(TXBSokt上料个数, com.Location);
                    UpdateTextBox(TXBSokt下料个数, com.Location);
                    UpdateTextBox(TXB托盘上料个数, com.Location);
                    UpdateTextBox(TXB托盘下料个数, com.Location);
                }
                catch (Exception)
                {

                }
            }
        }

        public void UpdateAlarm()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(300);
                    for (int i = 0; i < com.Alarm.Count; i++)
                    {
                        string key = $"PlcOutAlarm[{i}]";
                        if (!alarmInfo.ContainsKey(i.ToString())) continue;
                        if ((bool)com.Alarm[key])
                        {
                            //如果当前报警列表不包含检测到的字符串
                            if (!currentWarning.Contains(alarmInfo[i.ToString()]))
                            {
                                currentWarning.Add(alarmInfo[i.ToString()]);
                                UpdateListToRTB(currentWarning, RTBWarning);
                                LogManager.WriteLog(alarmInfo[i.ToString()], LogType.Warning);
                            }
                        }
                        else
                        {
                            if (currentWarning.Contains(alarmInfo[i.ToString()]))
                            {
                                currentWarning.Remove(alarmInfo[i.ToString()]);
                                UpdateListToRTB(currentWarning, RTBWarning);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    LogManager.WriteLog("报警监测循环：" + e.Message, LogType.Warning);
                }
            }
        }
        #endregion

        #region 界面按钮事件
        private void Combo_DataProcessed(object sender, EventArgs e)
        {
            if (sender is ComboBoxConfig combo)
            {
                FSB状态.Text = $"{combo.SourceControl.Text}:{combo.Address}  code:{GetPlcInID(combo.SourceControl.Text)}";
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
                RecordAndShow($"{GetBracketString(button.Name)}", LogType.Modification, RTBModify);
                com.WriteVariable(false, button.Tag.ToString());
            }
        }

        private void BTN主界面_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                switch (button.Tag)
                {
                    case "modifyhistory":
                        new CheckForm("更改记录").Show();
                        break;
                    case "modifyclear":
                        RTBModify.Clear();
                        break;
                    case "monitorhistory":
                        new CheckForm("监视记录").Show();
                        break;
                    case "monitorclear":
                        RTBMonitor.Clear();
                        break;
                    case "warninghistory":
                        new CheckForm().Show();
                        break;
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
                        if (CB_TypeOfTray.Text == "" || CB_Socket类.Text == "" || CB_工位.Text == "" || CB_工作盘数.Text == "")
                        {
                            MessageBox.Show("类别未选择完成，不能启动", "提示");
                            return;
                        }
                        MainButton("是否开始自动运行？", "自动运行开始", "PlcInIO[5]");
                        break;
                    case "stop":
                        MainButton("是否停止自动？", "自动运行停止", "PlcInIO[6]");
                        break;
                    case "ini":
                        if (CB_TypeOfTray.Text == "" || CB_Socket类.Text == "" || CB_工位.Text == "" || CB_工作盘数.Text == "")
                        {
                            MessageBox.Show("类别未选择完成，不能启动", "提示");
                            return;
                        }
                        if (MainButton("是否初始化？", "初始化", "PlcInIO[7]"))
                        {
                            Thread.Sleep(2000);
                            InitialForm initialForm = new InitialForm();
                            initialForm.ShowDialog();
                        }
                        break;
                    case "autolocal":
                        DialogResult r1 = MessageBox.Show("是否选择自动模式本地？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (r1 == DialogResult.Yes)
                        {
                            RecordAndShow($"选择自动模式本地", LogType.Modification, RTBModify);
                            com.WriteVariable(true, "PlcInIO[3]");
                            com.WriteVariable(false, "PlcInIO[27]");
                            com.WriteVariable(false, "PlcInIO[29]");
                        }
                        break;
                    case "autoremote":
                        DialogResult r2 = MessageBox.Show("是否选择自动模式远程-测试？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (r2 == DialogResult.Yes)
                        {
                            RecordAndShow($"选择自动模式远程-测试", LogType.Modification, RTBModify);
                            com.WriteVariable(true, "PlcInIO[29]");
                            com.WriteVariable(false, "PlcInIO[3]");
                            com.WriteVariable(false, "PlcInIO[27]");
                        }
                        break;
                    case "avoidpos":
                        MainButton("上料X轴是否移动到取托盘避让位置？", "上料X轴移动到取托盘避让位置", "PlcInIO[210]");
                        break;
                    case "blackbody":
                        MainButton("互锁条件：1黑体手动平移轴在指定位置，且黑体到位信号X13有信号；请确认设备满足以上条件，再开启黑体一键上升功能！", "黑体一键上升", "PlcInIO[334]");
                        break;
                    case "clamp":
                        MainButton("互锁条件：1搬运Z轴在上升位置；2钧舵夹爪1、2、3、4气缸都在上升位置；3工位1、2、3、4翻转气缸都在翻0°位置；请确认设备满足以上条件，再开启钧舵夹爪一键下料功能！", "钧舵夹爪一键下料", "PlcInIO[150]");
                        break;
                    default: break;
                }
            }
        }

        private void BTN提示报警跳过_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            DialogResult result = MessageBox.Show($"{button.Text}？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                RecordAndShow($"{button.Text}", LogType.Modification, RTBModify);
                com.WriteVariable(true, (string)button.Tag);
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
                RecordAndShow($"示教值写入 [{button.CtrlName}]", LogType.Modification, RTBModify);
            }
        }
        #endregion

        #region 生产配方选择
        private int GetAngleValue(int angle)
        {
            switch (angle)
            {
                case -90: return 1;
                case 0: return 2;
                case 90: return 3;
                default: return -1;
            }
        }

        private int GetSensorTypeValue(string type)
        {
            switch (type)
            {
                case "晶圆": return 1;
                case "金属": return 2;
                case "陶瓷": return 3;
                default: return -1;
            }
        }

        private void CB_TypeOfTray_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CB_TypeOfTray.SelectedIndex >= 0)
                {
                    DialogResult result = MessageBox.Show($"您是否选择 “{CB_TypeOfTray.Text}” 型号？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        currentTrayType = CB_TypeOfTray.Text;
                        IsWrite(com.WriteVariable(currentTrayType.Substring(2), "PLC测试信息[55]"), "切换型号中……");
                        IsWrite(com.WriteVariable(currentTrayType.Substring(2).Length, "PlcInPmt[72]"), "切换型号中……");
                        RecordAndShow($"{tempTrayType}切换为{currentTrayType}", LogType.Modification, RTBModify);
                        tempTrayType = currentTrayType;

                        bool isWrite = false;
                        var trayType = trayManager.TrayType[currentTrayType];
                        isWrite = com.WriteVariable(trayType.Length, "PLCInPmt[45]");
                        isWrite = com.WriteVariable(Convert.ToDouble(trayType.Width), "PLCInPmt[46]");
                        isWrite = com.WriteVariable(trayType.LineSpacing, "PLCInPmt[47]");
                        isWrite = com.WriteVariable(trayType.ColumnSpacing, "PLCInPmt[48]");
                        isWrite = com.WriteVariable(trayType.Height, "PLCInPmt[49]");
                        isWrite = com.WriteVariable(Convert.ToDouble(trayType.Length * trayType.Width), "PLCInPmt[50]");
                        isWrite = com.WriteVariable(trayType.Index, "PlcInID[1]");
                        isWrite = com.WriteVariable(GetAngleValue(trayType.VacAngle), "PlcInID[7]");
                        isWrite = com.WriteVariable(GetAngleValue(trayType.ClawsAngle), "PlcInID[8]");
                        string sensorType = trayType.TrayType.Substring(0, 2);
                        isWrite = com.WriteVariable(GetSensorTypeValue(sensorType), "PlcInID[6]");
                        IsWrite(isWrite, "信息写入");
                        CB_Socket类.Text = "单目";
                    }
                    else
                    {
                        CB_TypeOfTray.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CB_Socket类_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_Socket类.SelectedIndex >= 0)
            {
                DialogResult result = MessageBox.Show($"您是否选择 “{CB_Socket类.Text}” ？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    RecordAndShow($"当前型号为：{CB_TypeOfTray.Text}", LogType.Modification, RTBModify);
                    var trayType = trayManager.TrayType[currentTrayType];
                    if (this.CB_Socket类.SelectedItem.ToString() == "四目")
                    {
                        IsWrite(com.WriteVariable(GetAngleValue(trayType.VacAngleFour), "PlcInID[7]"), "切换为四目。");
                        com.WriteVariable(1, "PlcInID[2]");
                        RecordAndShow($"当前产品为{currentTrayType}，更改为四目", LogType.Modification, RTBModify);
                    }
                    if (this.CB_Socket类.SelectedItem.ToString() == "单目")
                    {
                        IsWrite(com.WriteVariable(GetAngleValue(trayType.VacAngle), "PlcInID[7]"), "切换为单目。");
                        com.WriteVariable(2, "PlcInID[2]");
                        RecordAndShow($"当前产品为{currentTrayType}，更改为单目", LogType.Modification, RTBModify);
                    }
                }
                else
                {
                    CB_Socket类.SelectedIndex = -1;
                }
            }
        }

        private void CB_工位_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_工位.SelectedIndex >= 0)
            {
                DialogResult result = MessageBox.Show($"您是否选择 “{CB_工位.Text}” ？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string number = Regex.Replace(CB_工位.Text, @"[^0-9]+", "");
                    if (int.TryParse(number, out int pos))
                    {
                        com.WriteVariable(pos, "PlcInID[3]");
                        RecordAndShow($"切换为第{pos}个工位", LogType.Modification, RTBModify);
                    }
                }
                else
                {
                    CB_工位.SelectedIndex = -1;
                }
            }
        }

        private void CB_工作盘数_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_工作盘数.SelectedIndex >= 0)
            {
                DialogResult result = MessageBox.Show($"您是否选择 “{CB_工作盘数.Text}” ？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string number = Regex.Replace(CB_工作盘数.Text, @"[^0-9]+", "");
                    if (int.TryParse(number, out int pos))
                    {
                        com.WriteVariable(pos, "PlcInID[5]");
                        RecordAndShow($"工作盘数{pos}盘", LogType.Modification, RTBModify);
                    }
                }
                else
                {
                    CB_工作盘数.SelectedIndex = -1;
                }
            }
        }
        #endregion

        #region 登录与窗口
        private void BTN_SwitchUser_Click(object sender, EventArgs e)
        {
            loginForm.Show();
            this.Hide();
        }

        private void BTN_Modify_Click(object sender, EventArgs e)
        {
            if (loginForm.CB_UserName.Text == "操作员" || loginForm.CB_UserName.Text == "管理员")
            {
                MessageBox.Show("未授权用户组", "修改密码");
                TB_Password.Text = "";
                TB_NewPassword.Text = "";
                return;
            }
            if (TB_Password.Text == "" || TB_NewPassword.Text == "")
            {
                MessageBox.Show("请输入密码", "修改密码");
                return;
            }
            if (TB_Password.Text != TB_NewPassword.Text)
            {
                MessageBox.Show("两次输入不一样", "修改密码");
                TB_Password.Text = "";
                TB_NewPassword.Text = "";
                return;
            }
            JsonManager.SaveJsonString(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FTData", "engineerData",
                new UserData() { UserType = 1, UserName = "工程师", Password = TB_Password.Text });
            MessageBox.Show("修改成功", "修改密码");
            TB_Password.Text = "";
            TB_NewPassword.Text = "";
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            loginForm.Close();
            //关闭通信端口
            com.Compolet.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //让用户选择点击
            DialogResult result = MessageBox.Show("是否确认关闭？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //判断是否取消事件
            if (result == DialogResult.No)
                //取消退出
                e.Cancel = true;
        }

        private void TSB导入_Click(object sender, EventArgs e)
        {
            if (loginForm.CurrentUser == "管理员")
                new ImportForm().Show();
        }

        private void TSBSetting_Click(object sender, EventArgs e)
        {
            if (loginForm.CurrentUser == "管理员" || loginForm.CurrentUser == "工程师")
            {
                new SettingForm(this).ShowDialog();
            }
        }

        private void TSB测试_Click(object sender, EventArgs e)
        {
            try
            {
                //trayManager.LoadTraysData(currentTrayType);
                //trayManager.SaveTraysData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 数据库操作
        private void BTNInquire_Click(object sender, EventArgs e)
        {
            try
            {
                DGVData.DataSource = SensorDataManager.InquireSensor(TXBSensorCode.Text, COBSensorType.Text, DTPMin.Value.ToString("yyyy-MM-dd HH:mm:ss"), DTPMax.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "查询数据");
            }
        }

        private void BTNOutput_Click(object sender, EventArgs e)
        {
            DataToExcel(DGVData);
        }
        #endregion

        
    }
}
