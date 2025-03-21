using System.Drawing;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace Services
{
    public class FormKit
    {

    }

    [JsonDerivedType(typeof(ButtonConfig), typeDiscriminator: "button")]
    [JsonDerivedType(typeof(LabelConfig), typeDiscriminator: "label")]
    public class ControlConfig
    {
        #region 控件的属性
        public Point Location { get; set; }
        public string CtrlName { get; set; }
        public string Text { get; set; }
        public object Tag { get; set; }
        public int Width { get; set; } = 110;
        public int Height { get; set; } = 24;
        #endregion

        public Control ControlInstance;
        public Control ParentControl;
        private bool isDragging = false;
        private Point offset;
        private const int gridSize = 10;

        [JsonConstructor]
        public ControlConfig(Point location, string ctrlName, string text, object tag, int width = 110, int height = 24)
        {
            Location = location;
            CtrlName = ctrlName;
            Text = text;
            Tag = tag;
            Width = width;
            Height = height;
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
}
