using Microsoft.VisualBasic;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FTV2.View
{
    public partial class ImportForm : Form
    {
        public List<ControlConfig> Imports { get; private set; } = new List<ControlConfig>();
        public List<ControlConfig> Loads { get; private set; } = new List<ControlConfig>();

        public ImportForm()
        {
            InitializeComponent();
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

        #region 导入
        private void TMI清除_Click(object sender, EventArgs e)
        {
            RTB数据.Clear();
        }

        private void BTN导入_Click(object sender, EventArgs e)
        {
            string[] iniPos = TB初始位置.Text.Split(':');
            string[] offsetPos = TB偏移量.Text.Split(':');
            string[] wh = TB宽高.Text.Split(':');
            if (iniPos.Length != 2)
                MessageBox.Show("初始位置设置错误", "提示");
            if (offsetPos.Length != 2)
                MessageBox.Show("偏移量设置错误", "提示");
            if (wh.Length != 2)
                MessageBox.Show("宽高设置错误", "提示");
            if (int.TryParse(iniPos[0], out int iniX) && int.TryParse(iniPos[1], out int iniY) && int.TryParse(offsetPos[0], out int offsetX) && int.TryParse(offsetPos[1], out int offsetY))
            {
                int width = int.Parse(wh[0]); int height = int.Parse(wh[1]);
                int x = iniX; int y = iniY;
                int count = 0;
                string[] importData = RTB数据.Text.Split(Environment.NewLine.ToCharArray());

                foreach (var info in importData)
                {
                    if (info == "") continue;
                    if (!info.Contains("\t")) continue;
                    var buttonInfo = info.Split('\t');
                    if (buttonInfo[1] == "") continue;
                    if (buttonInfo[1] == "备用") continue;
                    if (buttonInfo[2] == "") continue;
                    if (buttonInfo[2] == "备用") continue;

                    if (count % 15 == 0 && count != 0)
                    {
                        x += offsetX;
                        y = iniY;
                    }

                    switch (CCB类型.Text)
                    {
                        case "Button":
                            Imports.Add(new ButtonConfig(new System.Drawing.Point(x, y), buttonInfo[2], buttonInfo[1], buttonInfo[0], width, height));
                            break;
                        case "Label":
                            Imports.Add(new LabelConfig(new System.Drawing.Point(x, y), buttonInfo[2], buttonInfo[1], buttonInfo[0], width, height));
                            break;
                        default: MessageBox.Show("数据结构未知", "提示"); break;
                    }

                    y += offsetY;
                    count++;
                }
            }
            else
            {
                MessageBox.Show("参数设置错误", "提示");
            }
        }

        private void BTN保存_Click(object sender, EventArgs e)
        {
            try
            {
                JsonManager.Save("Config", $"{TB文件名.Text}.json", Imports);
                MessageBox.Show("保存完成", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
        #endregion

        #region 编辑
        private void BTN添加_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox($"请输入要添加的控件信息：", "提示", "Button;name;text;PlcInIO[30]");
            if (input == "") return;
            string[] controlInfo = input.Split(';');
            switch (controlInfo[0])
            {
                case "Button":
                    if (controlInfo.Length != 4) return;
                    ButtonConfig button = new ButtonConfig(new System.Drawing.Point(0, 0), controlInfo[1], controlInfo[2], controlInfo[3]);
                    Loads.Add(button);
                    button.AddControl(PN控件预览, new System.Drawing.Size(110, 24), new System.Drawing.Font("Times New Roman", 8));
                    button.BindingEvent();
                    button.ControlInstance.Click += BTN按钮测试_Click;
                    button.ControlInstance.ContextMenuStrip = CMS设置右键;
                    break;
                case "Label":
                    if (controlInfo.Length < 3) return;
                    LabelConfig label = new LabelConfig(new System.Drawing.Point(0, 0), controlInfo[1], controlInfo[2], controlInfo[3]);
                    Loads.Add(label);
                    label.AddControl(PN控件预览, new System.Drawing.Size(110, 24), new System.Drawing.Font("Times New Roman", 8));
                    label.BindingEvent();
                    label.ControlInstance.Click += BTN按钮测试_Click;
                    label.ControlInstance.ContextMenuStrip = CMS设置右键;
                    break;
                default: MessageBox.Show("未知控件", "提示"); break;
            }
        }

        private void BTN加载_Click(object sender, EventArgs e)
        {
            try
            {
                if (OFD打开.ShowDialog() == DialogResult.OK)
                {
                    PN控件预览.Controls.Clear();
                    Loads.Clear();
                    string path = OFD打开.FileName.Replace($"\\{OFD打开.FileName.Split('\\').Last()}", "");
                    string name = OFD打开.FileName.Split('\\').Last();
                    TB文件名.Text = name.Split('.')[0];
                    var controls = JsonManager.Load<List<ControlConfig>>(path, name);
                    if (controls != null && controls.Count > 0) Loads.AddRange(controls);
                    foreach (var control in Loads)
                    {
                        control.AddControl(PN控件预览, null, new System.Drawing.Font("Times New Roman", 8));
                        control.BindingEvent();
                        control.ControlInstance.Click += BTN按钮测试_Click;
                        control.ControlInstance.ContextMenuStrip = CMS设置右键;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        private void BTN信息保存_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var control in Loads)
                    control.SyncControl();//将按钮信息同步到控件信息类中
                JsonManager.Save("Config", $"{TB文件名.Text}.json", Loads);
                MessageBox.Show("保存完成", "提示");
            }
            catch (Exception)
            {

            }
        }

        private void BTN按钮测试_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button button)
                    LB信息.Text = $"{button.Name};{button.Tag}";
                if (sender is Label label)
                    LB信息.Text = $"{label.Name};{label.Tag}";
            }
            catch (Exception)
            {

            }
        }

        private void TMI控件设置_Click(object sender, EventArgs e)
        {
            if(sender is ToolStripMenuItem tmi)
            {
                switch (tmi.Tag)
                {
                    case "size":
                        if (CMS设置右键.SourceControl is Button buttonSize)
                        {
                            string input = Interaction.InputBox($"请输入要更改的控件大小：", "提示", "110:24");
                            if (input == "") return;
                            string[] strings = input.Split(':');
                            if (strings.Length != 2) return;
                            if (int.TryParse(strings[0], out int x) && int.TryParse(strings[1], out int y))
                            {
                                buttonSize.Size = new System.Drawing.Size(x, y);
                            }
                        }
                        break;
                    case "text":
                        if (CMS设置右键.SourceControl is Button buttonText)
                        {
                            string input = Interaction.InputBox($"请输入要更改的文本：", "提示", "按钮的Text");
                            if (input == "") return;
                            buttonText.Text = input;
                        }
                        else if(CMS设置右键.SourceControl is Label label)
                        {
                            string input = Interaction.InputBox($"请输入要更改的文本：", "提示", "标签的Text");
                            if (input == "") return;
                            label.Text = input;
                        }
                        break;
                    default:break;
                }
            }
        }
        #endregion

        private void BTN测试_Click(object sender, EventArgs e)
        {
            List<ControlConfig> L = new List<ControlConfig>();
            var v = JsonManager.Load<Dictionary<string, string>>("Config", "TextBoxInfo.json");
            var caliGroups = GetControls<GroupBox>(PN控件预览);
            foreach (var item in caliGroups.Values)
            {
                var g = new GroupConfig(item.Location, item.Text, item.Text, item.Text, item.Size.Width, item.Size.Height);
                var tb = GetControls<TextBox>(item);
                foreach (var citem in tb.Values)
                {
                    v.TryGetValue(citem.Name, out var c);
                    if (c == null) c = citem.Name;
                    var ttb = new TextBoxConfig(citem.Location, $"TXB[{citem.Name.Substring(3)}]", citem.Text, c, citem.Size.Width, citem.Size.Height);
                    g.Configs.Add(ttb);
                }
                var bn = GetControls<Button>(item);
                foreach (var citem in bn.Values)
                {
                    var bttn = new ButtonConfig(citem.Location, $"BTN[{citem.Name.Substring(3)}]", citem.Text, citem.Tag, citem.Size.Width, citem.Size.Height);
                    g.Configs.Add(bttn);
                }
                var lb = GetControls<Label>(item);
                foreach (var citem in lb.Values)
                {
                    var lab = new LabelConfig(citem.Location, $"LB[{citem.Text}]", citem.Text, citem.Tag, citem.Size.Width, citem.Size.Height);
                    g.Configs.Add(lab);
                }
                L.Add(g);
            }

            JsonManager.Save("Config", $"{TB文件名.Text}.json", L);
            MessageBox.Show("保存完成", "提示");
        }

    }
}
