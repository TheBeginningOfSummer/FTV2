using FTV2.View;
using Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTV2
{
    public partial class MainForm : Form
    {
        DataRepeater<Message<object>> dataRepeater = new DataRepeater<Message<object>>();
        Random rnd = new Random();

        public MainForm()
        {
            InitializeComponent();
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
            var Buttons1 = JsonManager.Load<List<ControlConfig<Button>>>("Config", "[Buttons]示教.json");
            foreach (var Button in Buttons1)
                Button.AddControl(TP示教, new System.Drawing.Size(100, 24), new System.Drawing.Font("Times New Roman", 7));
            var Buttons2 = JsonManager.Load<List<ControlConfig<Button>>>("Config", "[Buttons]手动气缸.json");
            foreach (var Button in Buttons2)
                Button.AddControl(TP上料, new System.Drawing.Size(110, 24), new System.Drawing.Font("Times New Roman", 8));
        }

    }
}
