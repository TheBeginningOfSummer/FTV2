using Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Model
{
    public class Sensor : DisplayLabel
    {
        //探测器编码
        private string sensorCode;
        public string SensorCode
        {
            get { return sensorCode; }
            set
            {
                sensorCode = value;
                if (sensorCode == "null")
                    WinformTool.InvokeOnThread(Display, () => Display.BackColor = Color.White);
                if (sensorCode == "noData")
                    WinformTool.InvokeOnThread(Display, () => Display.BackColor = Color.Gray);
            }
        }
        //探测器类型
        public string SensorType { get; set; }
        //测试工位
        public string TestStation { get; set; }
        //测试结果
        private int sensorQuality;
        public int SensorQuality
        {
            get { return sensorQuality; }
            set
            {
                sensorQuality = value;
                if (SensorCode != "null" && SensorCode != "noData")
                {
                    if (sensorQuality == 0)
                    {
                        WinformTool.InvokeOnThread(Display, new Action(() => { Display.BackColor = Color.LightGray; }));
                    }
                    else if (sensorQuality == 1)
                    {
                        WinformTool.InvokeOnThread(Display, new Action(() => { Display.BackColor = Color.Lime; }));
                    }
                    else if (sensorQuality == 2)
                    {
                        WinformTool.InvokeOnThread(Display, new Action(() => { Display.BackColor = Color.OrangeRed; }));
                    }
                    else if (sensorQuality == 3)
                    {
                        WinformTool.InvokeOnThread(Display, new Action(() => { Display.BackColor = Color.Yellow; }));
                    }
                }
            }
        }
        //所在托盘编号
        public string TrayNumber { get; set; }
        //所在托盘中的位置
        public int PosInTray { get; set; }
        //外观
        public string Appearance { get; set; }
        //开始测试时间
        public string StartTime { get; set; }
        //测试完成时间
        public string EndTime { get; set; }

        //状态显示控件
        public Label SensorStatusLabel = new Label()
        {
            Width = 24,
            Height = 24,
        };

        /// <summary>
        /// 此处初始化Sensor的属性值
        /// </summary>
        /// <param name="sensorCode">传感器编码</param>
        /// <param name="sensorType">传感器类型</param>
        /// <param name="testStation">测试工位</param>
        /// <param name="sensorQuality">测试结果</param>
        /// <param name="trayNumber">托盘编号</param>
        /// <param name="posInTray">在托盘中的位置</param>
        /// <param name="appearance">外观</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public Sensor(string sensorCode, string sensorType = "", string testStation = "", int sensorQuality = -1, string trayNumber = "", int posInTray = -1, string appearance = "", string startTime = "", string endTime = "")
        {
            Display.Name = $"{sensorCode}{PosInTray}";
            Display.Text = PosInTray.ToString();
            
            SensorCode = sensorCode;
            SensorType = sensorType;
            TestStation = testStation;
            SensorQuality = sensorQuality;
            TrayNumber = trayNumber;
            PosInTray = posInTray;
            Appearance = appearance;
            StartTime = DateConverter(startTime);
            EndTime = DateConverter(endTime);
        }
        /// <summary>
        /// 此处初始化用来存储的Sensor数据
        /// </summary>
        /// <param name="sensor">要存储的Sensor数据</param>
        public Sensor(Sensor sensor)
        {
            Display.Name = $"{sensorCode}{PosInTray}";
            Display.Text = PosInTray.ToString();

            SensorCode = sensor.SensorCode;
            SensorType = sensor.SensorType;
            TestStation = sensor.TestStation;
            SensorQuality = sensor.SensorQuality;
            TrayNumber = sensor.TrayNumber;
            PosInTray = sensor.PosInTray;
            Appearance = sensor.Appearance;
            StartTime = sensor.StartTime;
            EndTime = sensor.EndTime;
        }

        public Sensor()
        {

        }

        public void SetTrayData(string trayNumber, int posInTray)
        {
            TrayNumber = trayNumber;
            PosInTray = posInTray;
            Display.Text = PosInTray.ToString();
        }

        public string DateConverter(string date)
        {
            if (date.Length == 14)
                return (date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2) + " " + date.Substring(8, 2) + ":" + date.Substring(10, 2) + ":" + date.Substring(12, 2));
            else
                return (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    public class Tray : DisplayLabel
    {
        //托盘编号
        private string number;
        public string Number
        {
            get { return number; }
            set
            {
                number = value;
                if (TrayLabel.IsHandleCreated)
                {
                    TrayLabel.Invoke(new Action(() => TrayLabel.Text = number));
                }
                else
                {
                    TrayLabel.Text = number;
                }
                if (Sensors != null) UpdateSensorsTrayNumber(number);
            }
        }
        //托盘长方向孔位
        public int TrayLength { get; set; }
        //托盘宽方向孔位
        public int TrayWidth { get; set; }
        //托盘中探测器的数据
        public virtual Dictionary<string, Sensor> Sensors { get; set; } = new Dictionary<string, Sensor>();

        //控件位置
        public Point PosOnPanel { get; set; }
        //显示托盘号的控件
        public Label TrayLabel = new Label();

        public Tray(int length, int width, Point posOnPanel, string trayNumber = "")
        {
            Display.AutoSize = true;
            Display.Name = trayNumber;
            Display.Text = trayNumber;

            for (int i = 0; i < length * width; i++)
                Sensors.Add((i + 1).ToString(), new Sensor("noData", trayNumber: trayNumber, posInTray: i + 1));

            Number = trayNumber;
            TrayLength = length;
            TrayWidth = width;
            PosOnPanel = posOnPanel;
        }

        public Tray(Tray trayData)
        {
            Display.AutoSize = true;
            Display.Name = trayData.Number;
            Display.Text = trayData.Number;

            foreach (var item in trayData.Sensors)
                Sensors.Add(item.Value.PosInTray.ToString(), item.Value);

            Number = trayData.Number;
            TrayLength = trayData.TrayLength;
            TrayWidth = trayData.TrayWidth;
            PosOnPanel = trayData.PosOnPanel;
        }

        public Tray()
        {

        }

        public void UpdateTrayLabel(Control canvasControl)
        {
            //之前的显示控件清除
            //WinformTool.InvokeOnThread(canvasControl, new Action(() => { canvasControl.Controls.Clear(); }));
            //设置托盘编号显示控件的位置
            SetLabel(canvasControl, new Point(PosOnPanel.X, PosOnPanel.Y - 25));
            //计算控件位置
            List<Point> location = WinformTool.SetLocation(PosOnPanel.X, PosOnPanel.Y, Sensors.Count, TrayLength, 25, 25);
            //传感器信息显示
            for (int i = 0; i < Sensors.Count; i++)
                Sensors[(i + 1).ToString()].SetLabel(canvasControl, location[i]);
        }

        public void UpdateSensorsTrayNumber(string trayNumber)
        {
            foreach (var sensor in Sensors)
                sensor.Value.TrayNumber = trayNumber;
        }
    }

    public class TrayManager
    {
        public string ConfigPath = Environment.CurrentDirectory + "\\Configuration";
        public string CachePath = Environment.CurrentDirectory + "\\Cache";

        //Mapping图布局
        public List<Point> MappingLayout;
        //托盘种类
        public Dictionary<string, TypeOfTray> TrayType;

        //托盘索引，区分托盘是第几盘，之后可以按托盘编号来寻找（Linq）
        public int CurrentTrayIndex;
        //当前所有的上料盘数据
        public List<Tray> Trays = new List<Tray>();

        public TrayManager()
        {
            try
            {
                Initialze();
            }
            catch (Exception e)
            {
                LogManager.WriteLog("读取托盘配置数据发生错误。" + e.Message, LogType.Error);
            }
        }

        /// <summary>
        /// 初始化Mapping图的位置配置文件，如果没有则创建默认配置
        /// </summary>
        public void Initialze()
        {
            //读取Mapping图布局配置文件
            MappingLayout = JsonManager.ReadJsonString<List<Point>>(ConfigPath, "MappingLayout");
            if (MappingLayout == null)
            {
                MappingLayout = new List<Point>
                {
                    new Point(20, 30),
                    new Point(220, 30),
                    new Point(420, 30)
                };
                JsonManager.SaveJsonString(ConfigPath, "MappingLayout", MappingLayout);
            }
            //读取托盘种类配置文件
            TrayType = JsonManager.ReadJsonString<Dictionary<string, TypeOfTray>>(ConfigPath, "TypeOfTray");
            if (TrayType == null)
            {
                TrayType = new Dictionary<string, TypeOfTray>();
                TrayType.Add("Default", new TypeOfTray() { Index = 9999, TrayType = "Default", Length = 5, Width = 5 });
                JsonManager.SaveJsonString(ConfigPath, "TypeOfTray", TrayType);
            }
        }

        /// <summary>
        /// 按托盘种类初始化托盘，每个盘在界面上的位置是固定的
        /// 按照每个盘在界面的位置来初始化
        /// </summary>
        /// <param name="trayType">托盘种类</param>
        public void InitializeTrays(string trayType)
        {
            if (TrayType.TryGetValue(trayType, out var type))
            {
                Trays.Clear();
                for (int i = 0; i < MappingLayout.Count; i++)
                    Trays.Add(new Tray(type.Length, type.Width, MappingLayout[i], i.ToString()));
            }
        }
        /// <summary>
        /// 保存托盘数据
        /// </summary>
        public void SaveTraysData()
        {
            JsonManager.SaveJsonString(CachePath, "TraysData", Trays);
        }
        /// <summary>
        /// 加载托盘数据，加载上次保存的托盘数据
        /// </summary>
        /// <param name="trayType">托盘类型</param>
        public void LoadTraysData(string trayType)
        {
            Trays = JsonManager.ReadJsonString<List<Tray>>(CachePath, "TraysData");
            if (Trays == null) InitializeTrays(trayType);
        }

        /// <summary>
        /// 设置托盘的编号
        /// </summary>
        /// <param name="index">列表索引</param>
        /// <param name="trayNumber">托盘编号</param>
        public void UpdateTrayNumber(string trayNumber)
        {
            if (CurrentTrayIndex <= 0) return;
            if (Trays == null) return;
            if (CurrentTrayIndex > Trays.Count) return;
            Trays[CurrentTrayIndex - 1].Number = trayNumber;
        }
        /// <summary>
        /// 更新托盘类型
        /// </summary>
        /// <param name="typeOfTray"></param>
        public void UpdateTrayTypeDic(TypeOfTray typeOfTray)
        {
            if (TrayType.ContainsKey(typeOfTray.TrayType))
                TrayType[typeOfTray.TrayType] = typeOfTray;
            else
                TrayType.Add(typeOfTray.TrayType, typeOfTray);
            JsonManager.SaveJsonString(ConfigPath, "TypeOfTray", TrayType);
        }
        /// <summary>
        /// 设置指定编号的托盘内探测器的数据,参数sensorData包含了托盘探测器在盘中的位置
        /// </summary>
        /// <param name="sensorData">收到的探测器数据</param>
        public void UpdateSensorDataInTray(Sensor sensorData)
        {
            if (CurrentTrayIndex <= 0) return;
            if (Trays == null) return;
            if (CurrentTrayIndex > Trays.Count) return;
            if (!Trays[CurrentTrayIndex - 1].Sensors.ContainsKey(sensorData.PosInTray.ToString())) return;
            Sensor sensor = Trays[CurrentTrayIndex - 1].Sensors[sensorData.PosInTray.ToString()];

            sensor.SensorCode = sensorData.SensorCode;
            sensor.SensorType = sensorData.SensorType;
            sensor.TestStation = sensorData.TestStation;
            sensor.SensorQuality = sensorData.SensorQuality;

            sensor.Appearance = sensorData.Appearance;
            sensor.StartTime = sensorData.StartTime;
            sensor.EndTime = sensorData.EndTime;
        }

        /// <summary>
        /// 更新托盘显示
        /// </summary>
        /// <param name="canvasControl">显示托盘的控件</param>
        public void UpdateTrayLabels(Control canvasControl)
        {
            WinformTool.InvokeOnThread(canvasControl, () => canvasControl.Controls.Clear());
            foreach (var tray in Trays) tray.UpdateTrayLabel(canvasControl);
        }
    }

    public static class WinformTool
    {
        public static void DrawToBitmap(Control control, string path, string name)
        {
            Bitmap bitmap = new Bitmap(control.Width, control.Height);
            control.DrawToBitmap(bitmap, new Rectangle(0, 0, control.Width, control.Height));
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            bitmap.Save(path + "\\" + name + ".bmp");
        }

        public static void InvokeOnThread(Control control, Action method)
        {
            if (control.IsHandleCreated)
            {
                control.Invoke(method);
            }
            else
            {
                method();
            }
        }
        /// <summary>
        /// 得到一个矩形阵列的坐标
        /// </summary>
        /// <param name="x">阵列起始X坐标</param>
        /// <param name="y">阵列起始Y坐标</param>
        /// <param name="count">阵列元素个数</param>
        /// <param name="length">每行的元素个数</param>
        /// <param name="xInterval">阵列坐标x方向间距</param>
        /// <param name="yInterval">阵列坐标y方向间距</param>
        /// <returns>阵列坐标列表</returns>
        public static List<Point> SetLocation(int x, int y, int count, int length, int xInterval, int yInterval)
        {
            int o = x;
            List<Point> locationList = new List<Point>();
            for (int i = 0; i < count; i++)
            {
                locationList.Add(new Point(x, y));
                x = x + xInterval;
                if ((i + 1) % length == 0)
                {
                    x = o;
                    y = y + yInterval;
                }
            }
            return locationList;
        }
        /// <summary>
        /// 设置一个Label组成的矩形阵列
        /// </summary>
        /// <param name="labelsLocation">阵列坐标列表</param>
        /// <param name="labelSize">Label大小（方形）</param>
        /// <param name="code">每个阵列的标记</param>
        /// <param name="offset">标记相对于起始坐标的偏移</param>
        /// <returns>包含标记的Label阵列列表</returns>
        public static List<Label> SetLabel(List<Point> labelsLocation, int labelSize, string code, Point offset)
        {
            List<Label> labelList = new List<Label>();
            Label title = new Label();
            title.Name = code;
            title.Width = 150;
            title.ForeColor = Color.OrangeRed;
            title.Text = code;
            title.Location = new Point(labelsLocation[0].X, labelsLocation[0].Y - offset.Y);
            labelList.Add(title);
            for (int i = 0; i < labelsLocation.Count; i++)
            {
                Label slot = new Label();
                slot.Name = code + i.ToString();
                slot.Width = labelSize;
                slot.Height = labelSize;
                slot.ForeColor = Color.Blue;
                slot.BackColor = Color.LightSkyBlue;
                slot.Text = (i + 1).ToString();
                slot.Location = labelsLocation[i];
                labelList.Add(slot);
            }
            return labelList;
        }
        /// <summary>
        /// 在控件上绘制Label列表
        /// </summary>
        /// <param name="canvasControl">需要绘制的控件</param>
        /// <param name="labels">Label列表</param>
        public static void DrawLabel(Control canvasControl, List<Label> labels)
        {
            for (int i = 0; i < labels.Count; i++)
            {
                canvasControl.Controls.Add(labels[i]);
            }
        }

        public static void ClearLabel(Control canvasControl, List<Label> labels)
        {
            foreach (var item in labels)
            {
                canvasControl.Controls.Remove(item);
            }
        }
    }

    public class DisplayLabel
    {
        public Label Display = new Label()
        {
            Width = 24,
            Height = 24,
            ForeColor = Color.Black,
        };

        public DisplayLabel()
        {
            
        }

        public void SetLabel(Control canvasControl, Point location)
        {
            WinformTool.InvokeOnThread(canvasControl, () =>
            {
                Display.Location = location;
                canvasControl.Controls.Add(Display);
            });
        }
    }



}
