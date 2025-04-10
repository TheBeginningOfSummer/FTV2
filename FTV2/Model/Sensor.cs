using Services;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Text.Json.Serialization;

namespace Model
{
    public class DisplayLabel
    {
        public Label Display = new Label()
        {
            Width = 24,
            Height = 24,
            ForeColor = Color.Black,
            Font = new Font("宋体", 9)
        };

        public DisplayLabel()
        {

        }
        /// <summary>
        /// 设置标签位置并添加到控件上
        /// </summary>
        /// <param name="canvasControl"></param>
        /// <param name="location"></param>
        public void SetLabel(Control canvasControl, Point location)
        {
            FormKit.InvokeOnThread(canvasControl, () =>
            {
                Display.Location = location;
                canvasControl.Controls.Add(Display);
            });
        }
        /// <summary>
        /// 设置标签内容
        /// </summary>
        /// <param name="text">内容</param>
        public void SetLabelText(string text)
        {
            FormKit.InvokeOnThread(Display, () =>
            {
                Display.Text = text;
            });
        }
    }

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
                    FormKit.InvokeOnThread(Display, () => Display.BackColor = Color.White);
                if (sensorCode == "noData")
                    FormKit.InvokeOnThread(Display, () => Display.BackColor = Color.Gray);
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
                        FormKit.InvokeOnThread(Display, new Action(() => { Display.BackColor = Color.LightGray; }));
                    }
                    else if (sensorQuality == 1)
                    {
                        FormKit.InvokeOnThread(Display, new Action(() => { Display.BackColor = Color.Lime; }));
                    }
                    else if (sensorQuality == 2)
                    {
                        FormKit.InvokeOnThread(Display, new Action(() => { Display.BackColor = Color.OrangeRed; }));
                    }
                    else if (sensorQuality == 3)
                    {
                        FormKit.InvokeOnThread(Display, new Action(() => { Display.BackColor = Color.Yellow; }));
                    }
                }
            }
        }
        //所在托盘编号
        public string TrayNumber { get; set; }
        //所在托盘中的位置
        private int posInTray;
        public int PosInTray
        {
            get { return posInTray; }
            set
            {
                posInTray = value;
                FormKit.InvokeOnThread(Display, new Action(() => Display.Text = posInTray.ToString()));
            }
        }
        //外观
        public string Appearance { get; set; }
        //开始测试时间
        public string StartTime { get; set; }
        //测试完成时间
        public string EndTime { get; set; }

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
        private string number = "";
        public string TrayNumber
        {
            get { return number; }
            set
            {
                number = value;
                if (Display.IsHandleCreated)
                {
                    Display.Invoke(new Action(() => Display.Text = number));
                }
                else
                {
                    Display.Text = number;
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

        //[JsonConstructor]
        public Tray(string trayNumber, int trayLength, int trayWidth, Point posOnPanel, Dictionary<string, Sensor> sensors = null)
        {
            Display.AutoSize = true;
            Display.Name = trayNumber;
            Display.Text = trayNumber;

            TrayNumber = trayNumber;
            TrayLength = trayLength;
            TrayWidth = trayWidth;
            PosOnPanel = posOnPanel;
            if (sensors != null && sensors.Count > 0)
                Sensors = sensors;
            else
                for (int i = 0; i < trayLength * trayWidth; i++)
                    Sensors.Add((i + 1).ToString(), new Sensor("noData", trayNumber: TrayNumber, posInTray: i + 1));
        }

        public Tray(Tray trayData)
        {
            Display.AutoSize = true;
            Display.Name = trayData.TrayNumber;
            Display.Text = trayData.TrayNumber;

            foreach (var item in trayData.Sensors)
                Sensors.Add(item.Value.PosInTray.ToString(), item.Value);

            TrayNumber = trayData.TrayNumber;
            TrayLength = trayData.TrayLength;
            TrayWidth = trayData.TrayWidth;
            PosOnPanel = trayData.PosOnPanel;
        }

        public Tray()
        {

        }
        /// <summary>
        /// 在指定控件上添加标签控件
        /// </summary>
        /// <param name="canvasControl">目标控件</param>
        public void UpdateTrayLabel(Control canvasControl)
        {
            //之前的显示控件清除
            //WinformTool.InvokeOnThread(canvasControl, new Action(() => { canvasControl.Controls.Clear(); }));
            //设置托盘编号显示控件的位置
            SetLabel(canvasControl, new Point(PosOnPanel.X, PosOnPanel.Y - 25));
            //计算控件位置
            List<Point> location = FormKit.SetLocation(PosOnPanel.X, PosOnPanel.Y, Sensors.Count, TrayLength, 25, 25);
            //传感器信息显示
            for (int i = 0; i < Sensors.Count; i++)
                Sensors[(i + 1).ToString()].SetLabel(canvasControl, location[i]);
        }
        /// <summary>
        /// 更新此托盘中所有传感器的托盘码
        /// </summary>
        /// <param name="trayNumber">托盘码</param>
        public void UpdateSensorsTrayNumber(string trayNumber)
        {
            foreach (var sensor in Sensors)
                sensor.Value.TrayNumber = trayNumber;
        }
    }

    public class TrayManager
    {
        public string ConfigPath = Environment.CurrentDirectory + "\\Config";
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
            MappingLayout = JsonManager.ReadJsonString<List<Point>>(ConfigPath, "Layout.json");
            if (MappingLayout == null)
            {
                MappingLayout = new List<Point>
                {
                    new Point(20, 30),
                    new Point(220, 30),
                    new Point(420, 30)
                };
                JsonManager.SaveJsonString(ConfigPath, "Layout.json", MappingLayout);
            }
            //读取托盘种类配置文件
            TrayType = JsonManager.ReadJsonString<Dictionary<string, TypeOfTray>>(ConfigPath, "TypeOfTray.json");
            if (TrayType == null)
            {
                TrayType = new Dictionary<string, TypeOfTray>();
                TrayType.Add("Default", new TypeOfTray() { Index = 9999, TrayType = "Default", Length = 5, Width = 5 });
                JsonManager.SaveJsonString(ConfigPath, "TypeOfTray.json", TrayType);
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
                    Trays.Add(new Tray(i.ToString(), type.Length, type.Width, MappingLayout[i]));
            }
        }
        /// <summary>
        /// 保存托盘数据
        /// </summary>
        public void SaveTraysData()
        {
            JsonManager.SaveJsonString(CachePath, "TraysData.json", Trays);
        }
        /// <summary>
        /// 加载托盘数据，加载上次保存的托盘数据
        /// </summary>
        /// <param name="trayType">托盘类型</param>
        public void LoadTraysData(string trayType)
        {
            var trays = JsonManager.ReadJsonString<List<Tray>>(CachePath, "TraysData.json");
            if (trays == null) InitializeTrays(trayType);
            else Trays = trays;
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
            Trays[CurrentTrayIndex - 1].TrayNumber = trayNumber;
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
            JsonManager.SaveJsonString(ConfigPath, "TypeOfTray.json", TrayType);
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
            FormKit.InvokeOnThread(canvasControl, () => canvasControl.Controls.Clear());
            foreach (var tray in Trays) tray.UpdateTrayLabel(canvasControl);
        }
    }

    public class SensorDataManager
    {
        public static string ConnectionString = "Data Source=SensorData.db;Pooling=true;FailIfMissing=false";
        public static List<string> TableList = new List<string>();

        public static void InitializeDatabase()
        {
            //得到数据库表信息
            DataTable schemaTable = SQLiteTool.GetTableList(ConnectionString);
            for (int i = 0; i < schemaTable.Rows.Count; i++)
            {
                TableList.Add(schemaTable.Rows[i]["TABLE_NAME"].ToString());
            }
            //如果表不存在则创建
            if (!TableList.Contains("Sensors"))
            {
                string sql = "create table Sensors (ID INTEGER primary key autoincrement,编码 TEXT,类型 TEXT,测试工位 TEXT,测试结果 INTEGER,托盘编号 TEXT,位置 INTEGER,外观 TEXT,开始时间 TEXT,完成时间 TEXT)";
                SQLiteTool.ExecuteSQL(ConnectionString, sql);
            }
        }

        public static string AddCondition(string tableHeader, string codition)
        {
            return " and " + tableHeader + " = '" + codition + "'";
        }
        //向数据库添加数据
        public static bool AddSensor(Sensor sensor)
        {
            string sql = "insert into Sensors(编码,类型,测试工位,测试结果,托盘编号,位置,外观,开始时间,完成时间) " +
                "values(@编码,@类型,@测试工位,@测试结果,@托盘编号,@位置,@外观,@开始时间,@完成时间)";
            SQLiteParameter[] paras = new SQLiteParameter[]
            {
                new SQLiteParameter("@编码",sensor.SensorCode),
                new SQLiteParameter("@类型",sensor.SensorType),
                new SQLiteParameter("@测试工位",sensor.TestStation),
                new SQLiteParameter("@测试结果",sensor.SensorQuality),
                new SQLiteParameter("@托盘编号",sensor.TrayNumber),
                new SQLiteParameter("@位置",sensor.PosInTray),
                new SQLiteParameter("@外观",sensor.Appearance),
                new SQLiteParameter("@开始时间",sensor.StartTime),
                new SQLiteParameter("@完成时间",sensor.EndTime)
            };
            if (SQLiteTool.ExecuteSQL(ConnectionString, sql, paras))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //查询数据
        public static DataTable InquireSensor(string sensorCode, string sensorType, string minTime, string maxTime)
        {
            string sql;
            if (sensorCode == "" && sensorType == "")
            {
                //sql = "select * from Sensors where 开始时间 between datetime('now','start of day','-1 day') and datetime('now','start of day','1 day')";
                sql = "select * from Sensors where 开始时间 >='" + minTime + "' and 开始时间<='" + maxTime + "'";
            }
            else if (sensorCode != "" && sensorType == "")
            {
                sql = "select * from Sensors where 开始时间 >= '" + minTime + "' and 开始时间<= '" + maxTime + "'" + AddCondition("编码", sensorCode);
            }
            else if (sensorCode == "" && sensorType != "")
            {
                sql = "select * from Sensors where 开始时间 >= '" + minTime + "' and 开始时间<= '" + maxTime + "'" + AddCondition("类型", sensorType);
            }
            else
            {
                sql = "select * from Sensors where 开始时间 >= '" + minTime + "' and 开始时间<= '" + maxTime + "'" + AddCondition("编码", sensorCode) + AddCondition("类型", sensorType);
            }
            return SQLiteTool.ExecuteQuery(SensorDataManager.ConnectionString, sql).Tables[0];
        }
    }

}
