using FTV2.View;
using Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace FTV2
{
    public partial class MainForm : Form
    {
        DataRepeater<Message<object>> dataRepeater = new DataRepeater<Message<object>>();
        readonly Communication com = Communication.Singleton;
        List<ControlConfig<Button>> 上料Buttons;

        public MainForm()
        {
            InitializeComponent();

            LoadControls();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Task.Run(async () =>
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        Message<object> d1 = new Message<object>() { Key = i.ToString(), Value = rnd.Next(0, 100), Date = DateTime.Now.ToString() };
            //        await dataRepeater.WriteDataAsync(d1);
            //        Thread.Sleep(1000);
            //    }
            //});
            //Task.Run(async () =>
            //{
            //    await dataRepeater.ParseMessageAsync(Display);
            //});
        }

        public void LoadControls()
        {
            上料Buttons = JsonManager.Load<List<ControlConfig<Button>>>("Config", "上料界面bak.json");
            foreach (var button in 上料Buttons)
            {
                button.AddControl(TP主界面, new System.Drawing.Size(100, 24), new System.Drawing.Font("Times New Roman", 8));
                button.ControlInstance.MouseDown += Output_MouseDown;
                button.ControlInstance.MouseUp += Output_MouseUp;
            }
        }

        private void Output_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                FSB状态.Text = $"{button.Name}:{button.Tag}";
                com.WriteVariable(true, button.Tag.ToString());
            }
        }

        private void Output_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                com.WriteVariable(false, button.Tag.ToString());
            }
        }

        public void Display(Message<object> data)
        {
            Debug.WriteLine(data.ToString());
        }

        private void TSB导入_Click(object sender, EventArgs e)
        {
            new ImportForm().Show();
        }

        private void TSB测试_Click(object sender, EventArgs e)
        {
            try
            {
                com.Compolet.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
