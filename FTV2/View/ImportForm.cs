using Microsoft.VisualBasic;
using Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FTV2.View
{
    public partial class ImportForm : Form
    {
        public List<ControlConfig> Imports { get; private set; } = new List<ControlConfig>();

        Control CurrentControl;

        public ImportForm()
        {
            InitializeComponent();
            CurrentControl = PN控件预览;
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

        #region 方法
        public Point GetPoint(string info)
        {
            string[] pos = info.Split(':');
            if (pos.Length < 2) return new Point();
            if (int.TryParse(pos[0], out int x) && int.TryParse(pos[1], out int y))
            {
                return new Point(x, y);
            }
            return new Point();
        }

        public void ImportControl(string importInfo, Point iniPos, Point offset, Point wh, int row, List<ControlConfig> target)
        {
            int width = wh.X; int height = wh.Y;
            int x = iniPos.X; int y = iniPos.Y;
            int count = 0;

            string[] importData = importInfo.Split(Environment.NewLine.ToCharArray());

            foreach (var info in importData)
            {
                if (info == "") continue;
                if (!info.Contains("\t")) continue;
                var controlInfo = info.Split('\t');
                string address = controlInfo[0];
                string text = controlInfo[1];
                string name = controlInfo[2];
                string type = controlInfo[3];

                if (count % row == 0 && count != 0)
                {
                    x += offset.X;
                    y = iniPos.Y;
                }
                ControlConfig addControl = new LabelConfig(new Point(x, y), name, text, address, width, height);
                switch (type)
                {
                    case "button":
                        addControl = new ButtonConfig(new Point(x, y), name, text, address, width, height);
                        break;
                    case "label":
                        addControl = new LabelConfig(new Point(x, y), name, text, address, width, height);
                        break;
                    default: MessageBox.Show("数据结构未知", "提示"); break;
                }
                if (CurrentControl is GroupBox)
                {
                    addControl.AddTo(CurrentControl, CMS设置右键, BTN按钮测试_Click, true);
                    var groupControl = target.Where(t => t.SourceControl.Name == CurrentControl.Name).First();
                    groupControl?.Configs.Add(addControl);
                }
                if (CurrentControl is Panel)
                {
                    addControl.AddTo(CurrentControl, CMS设置右键, BTN按钮测试_Click, true);
                    target.Add(addControl);
                }
                y += offset.Y;
                count++;
            }
        }

        public void Save(List<ControlConfig> target)
        {
            try
            {
                foreach (var control in target)
                    control.SyncControl();//将按钮信息同步到控件信息类中
                JsonManager.Save("Config", $"{TTB文件名.Text}.json", target);
                MessageBox.Show("保存完成", "提示");
            }
            catch (Exception e)
            {
                MessageBox.Show($"保存失败。{e.Message}", "提示");
            }
        }

        public void LoadControl()
        {
            PN控件预览.Controls.Clear();
            Imports.Clear();
            string path = OFD打开.FileName.Replace($"\\{OFD打开.FileName.Split('\\').Last()}", "");
            string name = OFD打开.FileName.Split('\\').Last();
            TTB文件名.Text = name.Split('.')[0];

            var controls = JsonManager.Load<List<ControlConfig>>(path, name);
            if (controls != null && controls.Count > 0) Imports.AddRange(controls);
            foreach (var control in Imports)
            {
                foreach (var config in control.Configs)
                {
                    if (config is ButtonConfig bnConfig)
                        bnConfig.DataProcessed += BnConfig_DataProcessed;
                }
                control.AddTo(PN控件预览, CMS设置右键, BTN按钮测试_Click, true);
            }
        }

        public void AddControl(string type, string name, string text, string tag, Point iniPos, Point wh)
        {
            ControlConfig addControl = new LabelConfig(iniPos, name, text, tag, wh.X, wh.Y);
            switch (type)
            {
                case "group":
                    addControl = new GroupConfig(iniPos, name, text, tag, wh.X, wh.Y);
                    break;
                case "button":
                    addControl = new ButtonConfig(iniPos, name, text, tag, wh.X, wh.Y);
                    break;
                case "label":
                    addControl = new LabelConfig(iniPos, name, text, tag, wh.X, wh.Y);
                    break;
                default: MessageBox.Show("未知控件", "提示"); break;
            }
            if (CurrentControl is GroupBox)
            {
                addControl.AddTo(CurrentControl, CMS设置右键, BTN按钮测试_Click, true);
                var groupControl = Imports.Where(t => t.SourceControl.Name == CurrentControl.Name).First();
                groupControl?.Configs.Add(addControl);
            }
            if (CurrentControl is Panel)
            {
                addControl.AddTo(CurrentControl, CMS设置右键, BTN按钮测试_Click, true);
                Imports.Add(addControl);
            }
        }

        public void AddControl(Point iniPos, Point wh)
        {
            string input = Interaction.InputBox($"请输入要添加的控件信息：", "提示", "group;name;text;tag");
            if (input == "") return;
            string[] controlInfo = input.Split(';');
            string name = controlInfo[1];
            string text = controlInfo[2];
            string address = controlInfo[3];
            AddControl(controlInfo[0], name, text, address, iniPos, wh);
        }

        public void AddControl(string type, Point iniPos, Point wh)
        {
            string input = Interaction.InputBox($"请输入要添加的控件信息：", "提示", "name;text;tag");
            if (input == "") return;
            string[] controlInfo = input.Split(';');
            string name = controlInfo[0];
            string text = controlInfo[1];
            string address = controlInfo[2];
            AddControl(type, name, text, address, iniPos, wh);
        }
        #endregion

        #region 导入
        private void BTN导入_Click(object sender, EventArgs e)
        {
            try
            {
                Point iniPos = GetPoint(TTB初始位置.Text);
                Point offsetPos = GetPoint(TTB偏移量.Text);
                Point wh = GetPoint(TCB宽高.Text);
                int row = int.Parse(TTB行数.Text);

                ImportControl(RTB数据.Text, iniPos, offsetPos, wh, row, Imports);

                MessageBox.Show("导入完成", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show("导入失败," + ex.Message, "提示");
            }
        }

        private void BTN保存_Click(object sender, EventArgs e)
        {
            Save(Imports);
        }

        private void BTN加载_Click(object sender, EventArgs e)
        {
            try
            {
                if (OFD打开.ShowDialog() == DialogResult.OK)
                {
                    LoadControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
        #endregion

        #region 右键
        private void TMI清除导入数据_Click(object sender, EventArgs e)
        {
            RTB数据.Clear();
        }

        private void TMI清除_Click(object sender, EventArgs e)
        {
            if (CurrentControl is GroupBox)
            {
                var groupControl = Imports.Where(t => t.SourceControl.Name == CurrentControl.Name).First();
                groupControl?.Configs.Clear();
                CurrentControl.Controls.Clear();
            }
            if (CurrentControl is Panel)
            {
                Imports.Clear();
                CurrentControl.Controls.Clear();
            }
        }

        private void TMI选择_Click(object sender, EventArgs e)
        {
            CurrentControl = CMS设置右键.SourceControl;
            FSB状态.Text = CurrentControl.Name;
        }

        private void TMI添加_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem tmi)
            {
                Point iniPos = GetPoint(TTB初始位置.Text);
                Point wh = GetPoint(TCB宽高.Text);
                if (tmi.Tag is "group")
                {
                    CurrentControl = PN控件预览;
                    FSB状态.Text = CurrentControl.Name;
                }
                AddControl(tmi.Tag.ToString(), iniPos, wh);
            }
        }

        private void TMI设置_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem tmi)
            {
                switch (tmi.Tag)
                {
                    case "size":
                        string inputSize = Interaction.InputBox($"请输入要更改的控件大小：", "提示", TCB宽高.Text);
                        if (inputSize == "") return;
                        string[] strings = inputSize.Split(':');
                        if (strings.Length != 2) return;

                        if (int.TryParse(strings[0], out int x) && int.TryParse(strings[1], out int y))
                            CMS设置右键.SourceControl.Size = new Size(x, y);
                        break;
                    case "text":
                        string inputText = Interaction.InputBox($"请输入要更改的文本：", "提示", CMS设置右键.SourceControl.Text);
                        if (inputText == "") return;
                        CMS设置右键.SourceControl.Text = inputText;
                        break;
                    case "tag":
                        string tag = CMS设置右键.SourceControl.Tag == null ? "" : CMS设置右键.SourceControl.Tag.ToString();
                        string inputTag = Interaction.InputBox($"请输入要更改的文本：", "提示", tag);
                        if (inputTag == "") return;
                        CMS设置右键.SourceControl.Tag = inputTag;
                        break;
                    case "fontsize":
                        string input = Interaction.InputBox($"请输入要更改的大小：", "提示", CMS设置右键.SourceControl.Font.Size.ToString());
                        if (input == "") return;
                        if (float.TryParse(input, out float fSize))
                            CMS设置右键.SourceControl.Font = new Font(CMS设置右键.SourceControl.Font.Name, fSize);
                        break;
                    default: break;
                }
            }
        }
        #endregion

        private void BTN按钮测试_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button button)
                    FSB状态.Text = $"{button.Name};{button.Tag}";
                if (sender is Label label)
                    FSB状态.Text = $"{label.Name};{label.Tag}";
            }
            catch (Exception)
            {

            }
        }

        private void BnConfig_DataProcessed(object sender, DataEventArgs e)
        {
            foreach (ControlConfig config in e.MyControlList)
            {
                MessageBox.Show($"{config.SourceControl.Name}:{config.SourceControl.Tag}");
            }
        }

        private void BTN测试_Click(object sender, EventArgs e)
        {
            //List<ControlConfig> L = new List<ControlConfig>();
            //var v = JsonManager.Load<Dictionary<string, string>>("Config", "TextBoxInfo.json");
            //var caliGroups = GetControls<GroupBox>(PN控件预览);
            //foreach (var item in caliGroups.Values)
            //{
            //    var g = new GroupConfig(item.Location, item.Text, item.Text, item.Text, item.Size.Width, item.Size.Height);
            //    var tb = GetControls<TextBox>(item);
            //    foreach (var citem in tb.Values)
            //    {
            //        v.TryGetValue(citem.Name, out var c);
            //        if (c == null) c = citem.Name;
            //        var ttb = new TextBoxConfig(citem.Location, citem.Name.Substring(3), citem.Text, c, citem.Size.Width, citem.Size.Height);
            //        g.Configs.Add(ttb);
            //    }
            //    var bn = GetControls<Button>(item);
            //    foreach (var citem in bn.Values)
            //    {
            //        var bttn = new ButtonConfig(citem.Location, citem.Name.Substring(3), citem.Text, citem.Tag, citem.Size.Width, citem.Size.Height);
            //        g.Configs.Add(bttn);
            //    }
            //    var lb = GetControls<Label>(item);
            //    foreach (var citem in lb.Values)
            //    {
            //        var lab = new LabelConfig(citem.Location, citem.Text, citem.Text, citem.Tag, citem.Size.Width, citem.Size.Height);
            //        g.Configs.Add(lab);
            //    }
            //    L.Add(g);
            //}

            //JsonManager.Save("Config", $"{TB文件名.Text}.json", L);
            //MessageBox.Show("保存完成", "提示");
            try
            {
                if (OFD打开.ShowDialog() == DialogResult.OK)
                {
                    PN控件预览.Controls.Clear();
                    Imports.Clear();
                    string path = OFD打开.FileName.Replace($"\\{OFD打开.FileName.Split('\\').Last()}", "");
                    string name = OFD打开.FileName.Split('\\').Last();
                    TTB文件名.Text = name.Split('.')[0];
                    var controls = JsonManager.Load<List<ControlConfig>>(path, name);
                    if (controls != null && controls.Count > 0) Imports.AddRange(controls);
                    foreach (var control in Imports)
                    {
                        if (control.Configs != null)
                        {
                            foreach (var config in control.Configs)
                            {
                                config.BindingEvent();
                                config.CtrlName = Regex.Match(config.CtrlName, @"\[(.*?)\]").Groups[1].Value;
                            }
                        }
                        if (control is GroupConfig)
                            control.CtrlName = control.CtrlName;
                        else
                            control.CtrlName = Regex.Match(control.CtrlName, @"\[(.*?)\]").Groups[1].Value;
                        //control.AddControl(PN控件预览, null, new Font("Times New Roman", 8));

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        
    }
}
