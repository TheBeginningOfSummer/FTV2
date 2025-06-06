﻿using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace Services
{
    public class FormKit
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
                control.Invoke(method);
            else
                method();
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

    public class DataEventArgs : EventArgs
    {
        public List<ControlConfig> MyControlList { get; set; } = new List<ControlConfig>();

        public DataEventArgs(List<ControlConfig> controls)
        {
            MyControlList = controls;
        }
    }

    [JsonDerivedType(typeof(ButtonConfig), typeDiscriminator: "button")]
    [JsonDerivedType(typeof(LabelConfig), typeDiscriminator: "label")]
    [JsonDerivedType(typeof(TextBoxConfig), typeDiscriminator: "textbox")]
    [JsonDerivedType(typeof(ComboBoxConfig), typeDiscriminator: "combobox")]
    [JsonDerivedType(typeof(GroupConfig), typeDiscriminator: "group")]
    public class ControlConfig
    {
        #region 控件属性
        public Point Location { get; set; }
        public string CtrlName { get; set; }
        public string Text { get; set; }
        public object Tag { get; set; }
        public int Width { get; set; } = 110;
        public int Height { get; set; } = 24;
        public string FontName { get; set; } = "Times New Roman";
        public float FontSize { get; set; } = 8;
        #endregion

        #region 控件管理
        public Control SourceControl;
        public Control ParentControl;
        private bool isDragging = false;
        private Point offset;
        public int GridSize = 10;
        #endregion

        public List<ControlConfig> Configs { get; set; } = new List<ControlConfig>();

        [JsonConstructor]
        public ControlConfig(Point location, string ctrlName, string text, object tag, int width = 110, int height = 24,
           string fontName = "宋体", float fontSize = 9, List<ControlConfig> configs = null)
        {
            Location = location;
            CtrlName = ctrlName;
            Text = text;
            Tag = tag;
            Width = width;
            Height = height;
            FontName = fontName;
            FontSize = fontSize;
            if (configs != null) Configs = configs;
            Initialize();
        }

        #region 控件移动事件
        public void BindingEvent()
        {
            SourceControl.MouseDown += Control_MouseDown;
            SourceControl.MouseMove += Control_MouseMove;
            SourceControl.MouseUp += Control_MouseUp;
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                offset = e.Location;//相对于按钮左上角的位置
            }
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point newLocation = SourceControl.PointToScreen(e.Location);
                newLocation = ParentControl.PointToClient(newLocation);
                newLocation.Offset(-offset.X, -offset.Y);
                SourceControl.Location = newLocation;
            }
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                int alignedX = (SourceControl.Left + GridSize / 2) / GridSize * GridSize;
                int alignedY = (SourceControl.Top + GridSize / 2) / GridSize * GridSize;
                Location = new Point(alignedX, alignedY);
                SourceControl.Location = Location;
            }
        }
        #endregion

        public virtual void Initialize()
        {
            SourceControl = new Button
            {
                Location = Location,
                Tag = Tag,
                Text = Text,
                Size = new Size(Width, Height),
                Name = $"[{CtrlName}]"
            };
        }
        /// <summary>
        /// 将此控件添加到界面上
        /// </summary>
        /// <param name="parent">父控件</param>
        /// <param name="menuStrip">右键是否绑定</param>
        /// <param name="click">点击事件</param>
        /// <param name="isMove">是否可以移动</param>
        public virtual void AddTo(Control parent, ContextMenuStrip menuStrip, EventHandler click, bool isMove = false)
        {
            ParentControl = parent;
            ParentControl.Controls.Add(SourceControl);
            if (menuStrip != null)
                SourceControl.ContextMenuStrip = menuStrip;
            if (click != null)
                SourceControl.Click += click;
            if (isMove)
                BindingEvent();
        }

        public void SyncControl()
        {
            Text = SourceControl.Text;
            Tag = SourceControl.Tag;
            Width = SourceControl.Width;
            Height = SourceControl.Height;
            FontName = SourceControl.Font.Name;
            FontSize = SourceControl.Font.Size;
            if (Configs != null)
            {
                foreach (var config in Configs)
                    config.SyncControl();
            }
        }

        public void SetText(string message)
        {
            FormKit.InvokeOnThread(SourceControl, () => SourceControl.Text = message);
        }

        public void SetColor(Color color)
        {
            FormKit.InvokeOnThread(SourceControl, () => SourceControl.BackColor = color);
        }
    }

    public class ButtonConfig : ControlConfig
    {
        public event EventHandler<DataEventArgs> DataProcessed;
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseUp;

        public ButtonConfig(Point location, string ctrlName, string text, object tag, int width = 110, int height = 24, string fontName = "宋体", float fontSize = 9, List<ControlConfig> configs = null)
            : base(location, ctrlName, text, tag, width, height, fontName, fontSize, configs)
        {
            Initialize();
        }

        public override void Initialize()
        {
            SourceControl = new Button
            {
                Location = Location,
                Tag = Tag,
                Text = Text,
                Size = new Size(Width, Height),
                Name = $"BTN[{CtrlName}]",
                Font = new Font(FontName, FontSize)
            };
        }

        public override void AddTo(Control parent, ContextMenuStrip menuStrip, EventHandler click, bool isMove = false)
        {
            base.AddTo(parent, menuStrip, click, isMove);//此控件添加到指定控件
            for (int i = 0; i < Configs.Count; i++)
            {
                if (Configs[i] is TextBoxConfig)//子控件如果是textBox类型
                    Configs[i].AddTo(parent, menuStrip, null, isMove);//还是添加到指定控件
            }
            SourceControl.Click += SourceControl_Click;//此按钮绑定一个事件用来传出子控件数据
            SourceControl.MouseDown += SourceControl_MouseDown;
            SourceControl.MouseUp += SourceControl_MouseUp;
        }

        private void SourceControl_Click(object sender, EventArgs e)
        {
            DataProcessed?.Invoke(this, new DataEventArgs(Configs));
        }

        private void SourceControl_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDown?.Invoke(this, e);
        }

        private void SourceControl_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUp?.Invoke(this, e);
        }

    }

    public class LabelConfig : ControlConfig
    {
        public LabelConfig(Point location, string ctrlName, string text, object tag, int width = 110, int height = 24, string fontName = "宋体", float fontSize = 9)
            : base(location, ctrlName, text, tag, width, height, fontName, fontSize)
        {
            Initialize();
        }

        public override void Initialize()
        {
            SourceControl = new Label
            {
                Location = Location,
                Tag = Tag,
                Text = Text,
                Size = new Size(Width, Height),
                Name = $"LB[{CtrlName}]",
                AutoSize = true,
                Font = new Font(FontName, FontSize)
            };
        }

        public void AutoSizeOff(bool onOff = false)
        {
            if (SourceControl is Label label)
            {
                label.AutoSize = onOff;
            }
        }
    }

    public class TextBoxConfig : ControlConfig
    {
        public TextBoxConfig(Point location, string ctrlName, string text, object tag, int width = 110, int height = 24, string fontName = "宋体", float fontSize = 9)
            : base(location, ctrlName, text, tag, width, height, fontName, fontSize)
        {
            Initialize();
        }

        public override void Initialize()
        {
            SourceControl = new TextBox
            {
                Location = Location,
                Tag = Tag,
                Text = Text,
                Size = new Size(Width, Height),
                Name = $"TXB[{CtrlName}]",
                Font = new Font(FontName, FontSize)
            };
        }

        public void SendValue<T>(Action<T, string> action)
        {
            action?.Invoke((T)Convert.ChangeType(SourceControl.Text, typeof(T)), SourceControl.Tag.ToString());
        }
    }

    public class ComboBoxConfig : ControlConfig
    {
        public event EventHandler DataProcessed;
        public string Address;

        public ComboBoxConfig(Point location, string ctrlName, string text, object tag, int width = 110, int height = 24, string fontName = "宋体", float fontSize = 9, List<ControlConfig> configs = null)
            : base(location, ctrlName, text, tag, width, height, fontName, fontSize, configs)
        {
            Initialize();
        }

        public override void Initialize()
        {
            SourceControl = new ComboBox
            {
                Location = Location,
                Tag = Tag,
                Text = Text,
                Size = new Size(Width, Height),
                Name = $"COB[{CtrlName}]",
                Font = new Font(FontName, FontSize)
            };
            string info = SourceControl.Tag.ToString();
            string[] items = info.Split(';');
            Address = items[0];
            if (items.Length > 1)
            {
                if (SourceControl is ComboBox comboBox)
                {
                    for (int i = 1; i < items.Length; i++)
                        comboBox.Items.Add(items[i]);
                    comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
                }
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataProcessed?.Invoke(this, e);
        }
    }

    public class GroupConfig : ControlConfig
    {
        public GroupConfig(Point location, string ctrlName, string text, object tag, int width = 110, int height = 24, string fontName = "宋体", float fontSize = 9, List<ControlConfig> configs = null)
            : base(location, ctrlName, text, tag, width, height, fontName, fontSize, configs)
        {
            Initialize();
        }

        public override void Initialize()
        {
            SourceControl = new GroupBox
            {
                Location = Location,
                Tag = Tag,
                Text = Text,
                Size = new Size(Width, Height),
                Name = $"GPB[{CtrlName}]",
                Font = new Font(FontName, FontSize)
            };
        }

        public override void AddTo(Control parent, ContextMenuStrip menuStrip, EventHandler click, bool isMove = false)
        {
            base.AddTo(parent, menuStrip, click, isMove);//此控件添加到指定的控件上
            for (int i = 0; i < Configs.Count; i++)
                Configs[i].AddTo(SourceControl, menuStrip, click, isMove);//子控件添加到此控件本身上
        }
    }

    

}
