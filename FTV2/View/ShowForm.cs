using System.Windows.Forms;

namespace FTV2
{
    public partial class ShowForm : Form
    {
        public ShowForm(string text)
        {
            InitializeComponent();
            TB_Message.Text = text;
            Select();
        }
    }
}
