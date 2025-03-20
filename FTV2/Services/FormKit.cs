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

        public T ControlInstance;
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

            ControlInstance = new T
            {
                Location = Location,
                Tag = Tag,
                Text = Text
            };
            if (ControlInstance is Button)
                ControlInstance.Name = $"BTN[{CtrlName}]";
            if (ControlInstance is Label)
                ControlInstance.Name = $"LB[{CtrlName}]";
            if (ControlInstance is TextBox)
                ControlInstance.Name = $"TB[{CtrlName}]";
            if (ControlInstance is RichTextBox)
                ControlInstance.Name = $"RTB[{CtrlName}]";
        }

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

        public void AddControl(Control parent, Size size, Font font)
        {
            if (size != null)
                ControlInstance.Size = size;
            if (font != null)
                ControlInstance.Font = font;
            ParentControl = parent;
            parent.Controls.Add(ControlInstance);
        }

    }
}
