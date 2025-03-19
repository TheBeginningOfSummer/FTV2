using System.Drawing;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace Services
{
    public class FormKit
    {

    }

    public class ControlConfig<T> where T : Control, new()
    {
        public Point Location { get; set; }
        public string CtrlName { get; set; }
        public string Text { get; set; }
        public object Tag { get; set; }

        public T Control;
        public Control ParentControl;
        private bool isDragging = false;
        private Point offset;
        private const int gridSize = 10;

        [JsonConstructor]
        public ControlConfig(Point location, string ctrlName, string text, object tag)
        {
            Location = location;
            CtrlName = ctrlName;
            Text = text;
            Tag = tag;

            Control = new T
            {
                Location = Location,
                Tag = Tag,
                Text = Text
            };
            if (Control is Button)
                Control.Name = $"BTN[{CtrlName}]";
            if (Control is Label)
                Control.Name = $"LB[{CtrlName}]";
            if (Control is TextBox)
                Control.Name = $"TB[{CtrlName}]";
            if (Control is RichTextBox)
                Control.Name = $"RTB[{CtrlName}]";
            Control.MouseDown += Control_MouseDown;
            Control.MouseMove += Control_MouseMove;
            Control.MouseUp += Control_MouseUp;
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
                Point newLocation = Control.PointToScreen(e.Location);
                newLocation = ParentControl.PointToClient(newLocation);
                newLocation.Offset(-offset.X, -offset.Y);
                Control.Location = newLocation;
            }
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                int alignedX = (Control.Left + gridSize / 2) / gridSize * gridSize;
                int alignedY = (Control.Top + gridSize / 2) / gridSize * gridSize;
                Location = new Point(alignedX, alignedY);
                Control.Location = Location;
            }
        }

        public ControlConfig()
        {

        }

        public void AddControl(Control parent, Size size, Font font)
        {
            if (size != null)
                Control.Size = size;
            if (font != null)
                Control.Font = font;
            ParentControl = parent;
            parent.Controls.Add(Control);
        }

    }
}
