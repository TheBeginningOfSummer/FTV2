using System.Collections.Generic;
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

    [JsonDerivedType(typeof(ButtonConfig), typeDiscriminator: "button")]
    [JsonDerivedType(typeof(LabelConfig), typeDiscriminator: "label")]
    [JsonDerivedType(typeof(TextBoxConfig), typeDiscriminator: "textbox")]
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
        #endregion

        #region 控件管理
        public Control ControlInstance;
        public Control ParentControl;
        private bool isDragging = false;
        private Point offset;
        private const int gridSize = 10;
        #endregion

        public List<ControlConfig> Configs { get; set; } = new List<ControlConfig>();

        [JsonConstructor]
        public ControlConfig(Point location, string ctrlName, string text, object tag, int width = 110, int height = 24, List<ControlConfig> configs = null)
        {
            Location = location;
            CtrlName = ctrlName;
            Text = text;
            Tag = tag;
            Width = width;
            Height = height;
            if (configs != null) Configs = configs;
            Initialize();
        }

        #region 控件移动事件
        public void BindingEvent()
        {
            ControlInstance.MouseDown += Control_MouseDown;
            ControlInstance.MouseMove += Control_MouseMove;
            ControlInstance.MouseUp += Control_MouseUp;
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
                Point newLocation = ControlInstance.PointToScreen(e.Location);
                newLocation = ParentControl.PointToClient(newLocation);
                newLocation.Offset(-offset.X, -offset.Y);
                ControlInstance.Location = newLocation;
            }
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                int alignedX = (ControlInstance.Left + gridSize / 2) / gridSize * gridSize;
                int alignedY = (ControlInstance.Top + gridSize / 2) / gridSize * gridSize;
                Location = new Point(alignedX, alignedY);
                ControlInstance.Location = Location;
            }
        }
        #endregion

        public virtual void Initialize()
        {
            ControlInstance = new Button
            {
                Location = Location,
                Tag = Tag,
                Text = Text,
                Size = new Size(Width, Height),
                Name = $"[{CtrlName}]"
            };
        }

        public void AddControl(Control parent, Size? size, Font font)
        {
            if (size != null)
                ControlInstance.Size = (Size)size;
            else
            {
                if (Width != 0 && Height != 0)
                    ControlInstance.Size = new Size(Width, Height);
                else
                    ControlInstance.Size = new Size(110, 24);
            }
            if (font != null)
                ControlInstance.Font = font;

            ParentControl = parent;
            ParentControl.Controls.Add(ControlInstance);
        }

        public void SyncControl()
        {
            Width = ControlInstance.Width;
            Height = ControlInstance.Height;
            Text = ControlInstance.Text;
        }

        public void SetText(string message)
        {
            FormKit.InvokeOnThread(ControlInstance, () => ControlInstance.Text = message);
        }

        public void SetColor(Color color)
        {
            FormKit.InvokeOnThread(ControlInstance, () => ControlInstance.BackColor = color);
        }
    }

    public class ButtonConfig : ControlConfig
    {
        public ButtonConfig(Point location, string ctrlName, string text, object tag, int width = 110, int height = 24) : base(location, ctrlName, text, tag, width, height)
        {
            Initialize();
        }

        public override void Initialize()
        {
            ControlInstance = new Button
            {
                Location = Location,
                Tag = Tag,
                Text = Text,
                Size = new Size(Width, Height),
                Name = $"BTN[{CtrlName}]"
            };
        }
    }

    public class LabelConfig : ControlConfig
    {
        public LabelConfig(Point location, string ctrlName, string text, object tag, int width = 110, int height = 24) : base(location, ctrlName, text, tag, width, height)
        {
            Initialize();
        }

        public override void Initialize()
        {
            ControlInstance = new Label
            {
                Location = Location,
                Tag = Tag,
                Text = Text,
                Size = new Size(Width, Height),
                Name = $"LB[{CtrlName}]",
                AutoSize = true
            };
        }
    }

    public class TextBoxConfig : ControlConfig
    {
        public TextBoxConfig(Point location, string ctrlName, string text, object tag, int width = 110, int height = 24) : base(location, ctrlName, text, tag, width, height)
        {
            Initialize();
        }

        public override void Initialize()
        {
            ControlInstance = new TextBox
            {
                Location = Location,
                Tag = Tag,
                Text = Text,
                Size = new Size(Width, Height),
                Name = $"TXB[{CtrlName}]",
            };
        }
    }

    public class GroupConfig : ControlConfig
    {
        public GroupConfig(Point location, string ctrlName, string text, object tag, int width = 110, int height = 24, List<ControlConfig> configs = null) : base(location, ctrlName, text, tag, width, height, configs)
        {
            Initialize();
        }

        public override void Initialize()
        {
            ControlInstance = new GroupBox
            {
                Location = Location,
                Tag = Tag,
                Text = Text,
                Size = new Size(Width, Height),
                Name = $"GPB[{CtrlName}]"
            };
            if (Configs != null)
            {
                foreach (var item in Configs)
                    ControlInstance.Controls.Add(item.ControlInstance);
            }
        }
    }

}
