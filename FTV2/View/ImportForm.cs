using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FTV2.View
{
    public partial class ImportForm : Form
    {
        public List<ControlConfig<Button>> Buttons { get; private set; } = new List<ControlConfig<Button>>();
        public List<ControlConfig<Button>> LoadButtons { get; private set; } = new List<ControlConfig<Button>>();

        public ImportForm()
        {
            InitializeComponent();
        }

        private void TMI清除_Click(object sender, EventArgs e)
        {
            RTB数据.Clear();
        }

        private void BTN保存_Click(object sender, EventArgs e)
        {
            try
            {
                int x = 15;
                int y = 20;
                int count = 0;
                string message = RTB数据.Text;
                string[] strings = message.Split(Environment.NewLine.ToCharArray());
                switch (CCB类型.Text)
                {
                    case "Button":
                        foreach (var stringItem in strings)
                        {
                            if (stringItem == "") continue;
                            if (!stringItem.Contains("\t")) continue;
                            var buttonInfo = stringItem.Split('\t');
                            if (buttonInfo[1] == "") continue;
                            if (buttonInfo[1] == "备用") continue;
                            
                            if (count % 15 == 0 && count != 0)
                            {
                                x += 150;
                                y = 20;
                            }
                            Buttons.Add(new ControlConfig<Button>(buttonInfo[1], new System.Drawing.Point(x, y), buttonInfo[0]));
                            y += 30;
                            count++;
                        }
                        JsonManager.Save("Config", $"{TB文件名.Text}.json", Buttons);
                        MessageBox.Show("保存完成", "提示");
                        break;
                    default: MessageBox.Show("数据结构未知", "提示"); break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        private void BTN加载_Click(object sender, EventArgs e)
        {
            try
            {
                if (OFD打开.ShowDialog() == DialogResult.OK)
                {
                    PN控件预览.Controls.Clear();
                    LoadButtons.Clear();
                    string path = OFD打开.FileName.Replace($"\\{OFD打开.FileName.Split('\\').Last()}", "");
                    string name = OFD打开.FileName.Split('\\').Last();
                    TB文件名.Text = name.Split('.')[0];
                    var buttons = JsonManager.Load<List<ControlConfig<Button>>>(path, name);
                    if (buttons != null && buttons.Count > 0) LoadButtons.AddRange(buttons);
                    foreach (var button in LoadButtons)
                        button.AddControl(PN控件预览, new System.Drawing.Size(110, 24), new System.Drawing.Font("Times New Roman", 8));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        private void BTN位置保存_Click(object sender, EventArgs e)
        {
            try
            {
                JsonManager.Save("Config", $"{TB文件名.Text}.json", LoadButtons);
                MessageBox.Show("保存完成", "提示");
            }
            catch (Exception)
            {

            }
        }

    }
}
