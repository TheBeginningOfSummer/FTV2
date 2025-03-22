using FTV2.View;
using Services;
using System;
using System.Collections.Concurrent;
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
        readonly Communication com = Communication.Singleton;
        readonly ConcurrentDictionary<string, ControlConfig> ControlDic = new ConcurrentDictionary<string, ControlConfig>();

        public MainForm()
        {
            InitializeComponent();

            LoadControls();
            Task.Run(UpdateInterface);
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
            List<ControlConfig>  上料Controls = JsonManager.Load<List<ControlConfig>>("Config", "上料界面.json");
            foreach (var control in 上料Controls)
            {
                ControlDic.TryAdd(control.Tag.ToString(), control);
                control.AddControl(TP上料, null, new System.Drawing.Font("Times New Roman", 8));
                control.ControlInstance.MouseDown += Output_MouseDown;
                control.ControlInstance.MouseUp += Output_MouseUp;
            }
        }

        public void UpdateData()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(50);
                    com.RefreshData();
                }
                catch (Exception)
                {

                }
            }
        }

        public void UpdateInterface()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(100);
                    foreach (var pos in com.Location)
                    {
                        ControlDic.AddOrUpdate(pos.Key, new ControlConfig(new System.Drawing.Point(0, 0), pos.Key, pos.Key, pos.Key),
                            (key, oldValue) => { oldValue.SetText(pos.Value.ToString()); return oldValue; });
                    }
                }
                catch (Exception)
                {

                }
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
                Task.Run(UpdateData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
