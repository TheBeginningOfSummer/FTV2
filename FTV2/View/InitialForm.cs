using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services;

namespace FTV2
{
    public partial class InitialForm : Form
    {
        Communication connection = Communication.Singleton;

        public Dictionary<string, string> SignalsInfo = new Dictionary<string, string>();
        public List<Label> Labels = new List<Label>();

        private bool isRefresh = true;
        private readonly string completeAddress = "PlcOutIO[156]";

        public InitialForm()
        {
            InitializeComponent();
            AddSignals();
            UpdateSignals();
        }

        public void LoadSignalsInfo()
        {
            SignalsInfo.Add("上料X轴", "FW[101]");
            SignalsInfo.Add("上料Y轴", "FW[102]");
            SignalsInfo.Add("托盘实盘轴", "FW[103]");
            SignalsInfo.Add("托盘倒实盘轴", "FW[105]");
            SignalsInfo.Add("托盘NG盘轴", "FW[104]");
            SignalsInfo.Add("托盘倒NG盘轴", "FW[106]");
            SignalsInfo.Add("上料吸嘴1轴", "FW[119]");
            SignalsInfo.Add("上料吸嘴2轴", "FW[120]");
            SignalsInfo.Add("实盘卡盘缩回", "PlcOutIO[130]");
            SignalsInfo.Add("NG盘卡盘缩回", "PlcOutIO[132]");
            SignalsInfo.Add("上料取托盘上升", "PlcOutIO[16]");
            SignalsInfo.Add("上料取托盘松开", "PlcOutIO[19]");
            SignalsInfo.Add("上料吸嘴1上升", "PlcOutIO[32]");
            SignalsInfo.Add("上料吸嘴2上升", "PlcOutIO[35]");
            SignalsInfo.Add("next", "next");
            SignalsInfo.Add("搬运平移轴", "FW[107]");
            SignalsInfo.Add("增广夹爪1", "PlcOutIO[50]");
            SignalsInfo.Add("增广夹爪2", "PlcOutIO[57]");
            SignalsInfo.Add("翻转翻0°", "PlcOutIO[48]");
            SignalsInfo.Add("平移吸嘴1/2下降", "PlcOutIO[41]");
            SignalsInfo.Add("平移吸嘴3/4下降", "PlcOutIO[43]");
            //JsonManager.SaveJsonString(Environment.CurrentDirectory + "\\Configuration", "InitialSignals", SignalsInfo);
        }

        public void SetLabelColor(bool io, Label indicator)
        {
            if (io)
            {
                indicator.Invoke(new Action(() => indicator.BackColor = Color.LawnGreen));
            }
            else
            {
                indicator.Invoke(new Action(() => indicator.BackColor = Color.Red));
            }
        }

        public void AddLabel(Point point, string name)
        {
            Label label = new Label
            {
                Name = $"{name}",
                Location = point,
                Text = name,
                AutoSize = true
            };
            Controls.Add(label);
        }

        public Label AddLabel(Point point, string name, string text, Size size, Color color, string tag)
        {
            Label label = new Label
            {
                Name = $"LB{name}",
                Location = point,
                Text = text,
                Size = size,
                BackColor = color,
                Tag = tag,
                AutoSize = false,
            };
            Controls.Add(label);
            return label;
        }

        public void AddSignal(Point point, string name, Size size, Color color, string address = "", int xOffset = 100)
        {
            AddLabel(point, name);
            Label label = AddLabel(new Point(point.X + xOffset, point.Y), name, "    ", size, color, address);
            Labels.Add(label);
        }

        public void AddSignals(int x = 15, int y = 60, int xInterval = 160, int yInterval = 25)
        {
            int count = 0;
            AddSignal(new Point(x, y + yInterval * count), "上料X轴", new Size(20, 15), Color.Red, "PlcOutIO[166]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "上料Y轴", new Size(20, 15), Color.Red, "PlcOutIO[167]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "托盘实盘轴", new Size(20, 15), Color.Red, "PlcOutIO[168]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "托盘倒实盘轴", new Size(20, 15), Color.Red, "PlcOutIO[170]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "托盘NG盘轴", new Size(20, 15), Color.Red, "PlcOutIO[169]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "托盘倒NG盘轴", new Size(20, 15), Color.Red, "PlcOutIO[171]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "上料吸嘴1轴", new Size(20, 15), Color.Red, "PlcOutIO[184]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "上料吸嘴2轴", new Size(20, 15), Color.Red, "PlcOutIO[185]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "实盘卡盘缩回", new Size(20, 15), Color.Red, "PlcOutIO[130]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "NG盘卡盘缩回", new Size(20, 15), Color.Red, "PlcOutIO[132]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "上料取托盘上升", new Size(20, 15), Color.Red, "PlcOutIO[16]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "上料取托盘松开", new Size(20, 15), Color.Red, "PlcOutIO[19]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "上料吸嘴1上升", new Size(20, 15), Color.Red, "PlcOutIO[32]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "上料吸嘴2上升", new Size(20, 15), Color.Red, "PlcOutIO[35]");
            x += xInterval; count = 0;
            AddSignal(new Point(x, y + yInterval * count), "翻转平移轴", new Size(20, 15), Color.Red, "PlcOutIO[172]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "增广夹爪1", new Size(20, 15), Color.Red, "PlcOutIO[50]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "增广夹爪2", new Size(20, 15), Color.Red, "PlcOutIO[57]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "翻转翻0°", new Size(20, 15), Color.Red, "PlcOutIO[48]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "平移吸嘴1/2下降", new Size(20, 15), Color.Red, "PlcOutIO[41]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "平移吸嘴3/4下降", new Size(20, 15), Color.Red, "PlcOutIO[43]");
            x += xInterval; count = 0;
            AddSignal(new Point(x, y + yInterval * count), "搬运X轴", new Size(20, 15), Color.Red, "PlcOutIO[173]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "搬运Y轴", new Size(20, 15), Color.Red, "PlcOutIO[174]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "搬运Z轴", new Size(20, 15), Color.Red, "PlcOutIO[175]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "钧舵1激活完成", new Size(20, 15), Color.Red, "PlcOutIO[190]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "钧舵2激活完成", new Size(20, 15), Color.Red, "PlcOutIO[191]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "钧舵3激活完成", new Size(20, 15), Color.Red, "PlcOutIO[192]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "钧舵4激活完成", new Size(20, 15), Color.Red, "PlcOutIO[193]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "钧舵夹爪1上升", new Size(20, 15), Color.Red, "PlcOutIO[64]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "钧舵夹爪2上升", new Size(20, 15), Color.Red, "PlcOutIO[66]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "钧舵夹爪3上升", new Size(20, 15), Color.Red, "PlcOutIO[68]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "钧舵夹爪4上升", new Size(20, 15), Color.Red, "PlcOutIO[70]");
            x += xInterval; count = 0;
            AddSignal(new Point(x, y + yInterval * count), "工位平移1轴", new Size(20, 15), Color.Red, "PlcOutIO[176]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位平移2轴", new Size(20, 15), Color.Red, "PlcOutIO[177]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位平移3轴", new Size(20, 15), Color.Red, "PlcOutIO[178]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位平移4轴", new Size(20, 15), Color.Red, "PlcOutIO[179]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "黑体升降1轴", new Size(20, 15), Color.Red, "PlcOutIO[180]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "黑体升降2轴", new Size(20, 15), Color.Red, "PlcOutIO[181]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "黑体升降3轴", new Size(20, 15), Color.Red, "PlcOutIO[182]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "黑体升降4轴", new Size(20, 15), Color.Red, "PlcOutIO[183]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "热辐射版1轴", new Size(20, 15), Color.Red, "PlcOutIO[186]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "热辐射版2轴", new Size(20, 15), Color.Red, "PlcOutIO[187]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "热辐射版3轴", new Size(20, 15), Color.Red, "PlcOutIO[188]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "热辐射版4轴", new Size(20, 15), Color.Red, "PlcOutIO[189]");
            x += xInterval; count = 0;
            AddSignal(new Point(x, y + yInterval * count), "工位1光阑左缩回", new Size(20, 15), Color.Red, "PlcOutIO[73]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位1光阑右伸出", new Size(20, 15), Color.Red, "PlcOutIO[112]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位1光阑左上升", new Size(20, 15), Color.Red, "PlcOutIO[74]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位1光阑右上升", new Size(20, 15), Color.Red, "PlcOutIO[104]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位1辐射版下降", new Size(20, 15), Color.Red, "PlcOutIO[77]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位1翻转翻0°", new Size(20, 15), Color.Red, "PlcOutIO[78]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位2光阑左缩回", new Size(20, 15), Color.Red, "PlcOutIO[81]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位2光阑右伸出", new Size(20, 15), Color.Red, "PlcOutIO[114]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位2光阑左上升", new Size(20, 15), Color.Red, "PlcOutIO[82]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位2光阑右上升", new Size(20, 15), Color.Red, "PlcOutIO[106]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位2辐射版下降", new Size(20, 15), Color.Red, "PlcOutIO[85]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位2翻转翻0", new Size(20, 15), Color.Red, "PlcOutIO[86]");
            x += xInterval; count = 0;
            AddSignal(new Point(x, y + yInterval * count), "工位3光阑左缩回", new Size(20, 15), Color.Red, "PlcOutIO[89]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位3光阑右伸出", new Size(20, 15), Color.Red, "PlcOutIO[116]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位3光阑左上升", new Size(20, 15), Color.Red, "PlcOutIO[90]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位3光阑右上升", new Size(20, 15), Color.Red, "PlcOutIO[108]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位3辐射版下降", new Size(20, 15), Color.Red, "PlcOutIO[93]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位3翻转翻0°", new Size(20, 15), Color.Red, "PlcOutIO[94]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位4光阑左缩回", new Size(20, 15), Color.Red, "PlcOutIO[97]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位4光阑右伸出", new Size(20, 15), Color.Red, "PlcOutIO[118]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位4光阑左上升", new Size(20, 15), Color.Red, "PlcOutIO[98]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位4光阑右上升", new Size(20, 15), Color.Red, "PlcOutIO[110]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位4辐射版下降", new Size(20, 15), Color.Red, "PlcOutIO[101]"); count++;
            AddSignal(new Point(x, y + yInterval * count), "工位4翻转翻0", new Size(20, 15), Color.Red, "PlcOutIO[102]");
        }

        public void UpdateSignals()
        {
            Task.Run(() =>
            {
                while (isRefresh)
                {
                    Thread.Sleep(200);
                    foreach (Label item in Labels)
                    {
                        if (connection.PLCOutput.ContainsKey(item.Tag.ToString()))
                            SetLabelColor(connection.PLCOutput[item.Tag.ToString()], item);
                        if (connection.PLCFW.ContainsKey(item.Tag.ToString()))
                            SetLabelColor(connection.PLCFW[item.Tag.ToString()], item);
                    }
                    if (connection.PLCOutput.ContainsKey(completeAddress))
                    {
                        if (connection.PLCOutput[completeAddress])
                        {
                            Thread.Sleep(1000);
                            Invoke(new Action(() => Close()));
                            break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("读取初始化完成信号失败", "提示");
                        break;
                    }
                }
            });
        }

        private void InitialForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isRefresh = false;
        }

    }
}
