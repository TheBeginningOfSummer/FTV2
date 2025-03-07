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
        public string Name { get; set; }
        public Point Location { get; set; }
        public object Tag { get; set; }

        public T Control;

        [JsonConstructor]
        public ControlConfig(string name, Point location, object tag)
        {
            Name = name;
            Location = location;
            Tag = tag;
            Control = new T
            {
                Location = Location,
                Tag = Tag,
                Text = Name
            };
            if (Control is Button)
                Control.Name = $"BTN[{Name}]";
            if (Control is Label)
                Control.Name = $"LB[{Name}]";
            if (Control is TextBox)
                Control.Name = $"TB[{Name}]";
            if (Control is RichTextBox)
                Control.Name = $"RTB[{Name}]";
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
            parent.Controls.Add(Control);
        }
    }
}
