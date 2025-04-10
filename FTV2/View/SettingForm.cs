using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Services;

namespace FTV2.View
{
    public partial class SettingForm : Form
    {
        readonly Communication com = Communication.Singleton;
        readonly MainForm mainForm;
        bool isUpdate = true;

        public SettingForm(MainForm form)
        {
            InitializeComponent();
            this.mainForm = form;
            Task.Run(UpdateInterface);
        }

        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isUpdate = false;
        }

        public void UpdateInterface()
        {
            while (isUpdate)
            {
                try
                {
                    Thread.Sleep(100);
                    #region 参数设置界面
                    UpdateTextBox(txt上料X轴定位速度, com.PLCPmt["PlcInPmt[0]"]);
                    UpdateTextBox(txt上料Y轴定位速度, com.PLCPmt["PlcInPmt[1]"]);
                    UpdateTextBox(txt升降轴定位速度, com.PLCPmt["PlcInPmt[2]"]);
                    UpdateTextBox(txt平移轴定位速度, com.PLCPmt["PlcInPmt[3]"]);
                    UpdateTextBox(txt中空轴定位速度, com.PLCPmt["PlcInPmt[4]"]);
                    UpdateTextBox(txt搬运X轴定位速度, com.PLCPmt["PlcInPmt[5]"]);
                    UpdateTextBox(txt搬运Y轴定位速度, com.PLCPmt["PlcInPmt[6]"]);
                    UpdateTextBox(txt搬运Z轴定位速度, com.PLCPmt["PlcInPmt[7]"]);
                    UpdateTextBox(txtSocket轴定位速度, com.PLCPmt["PlcInPmt[8]"]);
                    UpdateTextBox(txt黑体轴定位速度, com.PLCPmt["PlcInPmt[9]"]);
                    UpdateTextBox(txt热辐射轴定位速度, com.PLCPmt["PlcInPmt[11]"]);
                    UpdateTextBox(txt上料X轴手动速度, com.PLCPmt["PlcInPmt[15]"]);
                    UpdateTextBox(txt上料Y轴手动速度, com.PLCPmt["PlcInPmt[16]"]);
                    UpdateTextBox(txt升降轴手动速度, com.PLCPmt["PlcInPmt[17]"]);
                    UpdateTextBox(txt平移轴手动速度, com.PLCPmt["PlcInPmt[18]"]);
                    UpdateTextBox(txt中空轴手动速度, com.PLCPmt["PlcInPmt[19]"]);
                    UpdateTextBox(txt搬运X轴手动速度, com.PLCPmt["PlcInPmt[20]"]);
                    UpdateTextBox(txt搬运Y轴手动速度, com.PLCPmt["PlcInPmt[21]"]);
                    UpdateTextBox(txt搬运Z轴手动速度, com.PLCPmt["PlcInPmt[22]"]);
                    UpdateTextBox(txtSocket轴手动速度, com.PLCPmt["PlcInPmt[23]"]);
                    UpdateTextBox(txt黑体轴手动速度, com.PLCPmt["PlcInPmt[24]"]);
                    UpdateTextBox(txt热辐射轴手动速度, com.PLCPmt["PlcInPmt[25]"]);
                    #endregion
                }
                catch (Exception)
                {

                }
            }
        }

        public void UpdateTextBox<T>(TextBox textBox, T variable)
        {
            if (variable != null)
                textBox.Invoke(new Action(() => textBox.Text = variable.ToString()));
        }

        private void WriteSpeed(TextBox speed, TextBox currentSpeed, string address = "PLCInPmt[0]", string message = "")
        {
            if (!string.IsNullOrEmpty(speed.Text))
            {
                com.WriteVariable(Convert.ToDouble(speed.Text), address);
                mainForm.RecordAndShow($"{message}：{currentSpeed.Text}更改为{speed.Text}mm/s", LogType.Modification);
                currentSpeed.Text = speed.Text;
                speed.Text = null;
            }
        }

        private void BTN写入速度_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否写入对应速度？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //定位速度
                WriteSpeed(txt上料X轴定位速度设置, txt上料X轴定位速度, "PLCInPmt[0]", "上料X轴定位速度设置");
                WriteSpeed(txt上料Y轴定位速度设置, txt上料Y轴定位速度, "PLCInPmt[1]", "上料Y轴定位速度设置");
                WriteSpeed(txt升降轴定位速度设置, txt升降轴定位速度, "PLCInPmt[2]", "升降轴定位速度设置");
                WriteSpeed(txt平移轴定位速度设置, txt平移轴定位速度, "PLCInPmt[3]", "平移轴定位速度设置");
                WriteSpeed(txt中空轴定位速度设置, txt中空轴定位速度, "PLCInPmt[4]", "中空轴定位速度设置");
                WriteSpeed(txt搬运X轴定位速度设置, txt搬运X轴定位速度, "PLCInPmt[5]", "搬运X轴定位速度设置");
                WriteSpeed(txt搬运Y轴定位速度设置, txt搬运Y轴定位速度, "PLCInPmt[6]", "搬运Y轴定位速度设置");
                WriteSpeed(txt搬运Z轴定位速度设置, txt搬运Z轴定位速度, "PLCInPmt[7]", "搬运Z轴定位速度设置");
                WriteSpeed(txtSocket轴定位速度设置, txtSocket轴定位速度, "PLCInPmt[8]", "Socket轴定位速度设置");
                WriteSpeed(txt黑体轴定位速度设置, txt黑体轴定位速度, "PLCInPmt[9]", "黑体轴定位速度设置");
                WriteSpeed(txt热辐射轴定位速度设置, txt热辐射轴定位速度, "PLCInPmt[11]", "热辐射轴定位速度设置");

                //手动速度
                WriteSpeed(txt上料X轴手动速度设置, txt上料X轴手动速度, "PLCInPmt[15]", "上料X轴手动速度设置");
                WriteSpeed(txt上料Y轴手动速度设置, txt上料Y轴手动速度, "PLCInPmt[16]", "上料Y轴手动速度设置");
                WriteSpeed(txt升降轴手动速度设置, txt升降轴手动速度, "PLCInPmt[17]", "升降轴手动速度设置");
                WriteSpeed(txt平移轴手动速度设置, txt平移轴手动速度, "PLCInPmt[18]", "平移轴手动速度设置");
                WriteSpeed(txt中空轴手动速度设置, txt中空轴手动速度, "PLCInPmt[19]", "中空轴手动速度设置");
                WriteSpeed(txt搬运X轴手动速度设置, txt搬运X轴手动速度, "PLCInPmt[20]", "搬运X轴手动速度设置");
                WriteSpeed(txt搬运Y轴手动速度设置, txt搬运Y轴手动速度, "PLCInPmt[21]", "搬运Y轴手动速度设置");
                WriteSpeed(txt搬运Z轴手动速度设置, txt搬运Z轴手动速度, "PLCInPmt[22]", "搬运Z轴手动速度设置");
                WriteSpeed(txtSocket轴手动速度设置, txtSocket轴手动速度, "PLCInPmt[23]", "Socket轴手动速度设置");
                WriteSpeed(txt黑体轴手动速度设置, txt黑体轴手动速度, "PLCInPmt[24]", "黑体轴手动速度设置");
                WriteSpeed(txt热辐射轴手动速度设置, txt热辐射轴手动速度, "PLCInPmt[25]", "热辐射轴手动速度设置");
            }
        }

        private void TB速度设置_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字
                {
                    e.Handled = true;
                }
            }
        }

        private void SetSpeed_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text != "")
                if (double.Parse(textBox.Text) > 500)
                {
                    textBox.Text = "500";
                    MessageBox.Show("输入1-500的值", "提示");
                }
        }

        private void SetAngle_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text != "" && textBox.Text != "-")
                if (double.Parse(textBox.Text) > 90 || double.Parse(textBox.Text) < -90)
                {
                    textBox.Text = "90";
                    MessageBox.Show("输入0-90的值", "提示");
                }
        }

        private void BTN确认更改_Click(object sender, EventArgs e)
        {
            try
            {
                #region 输入参数校验
                if (string.IsNullOrEmpty(txt输入产品型号.Text))
                {
                    MessageBox.Show("请输入产品型号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (txt输入产品型号.Text.Length <= 2)
                {
                    MessageBox.Show("请输入正确的产品型号，如晶圆612W9");
                    return;
                }

                if (int.TryParse(txt托盘行数.Text, out int length))
                {
                    if (length <= 0 || length > 10)
                    {
                        MessageBox.Show("托盘行数输入错误请检查,请输入1-10之间的整数");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("托盘行数输入错误请检查,请输入1-10之间的整数");
                    return;
                }

                if (int.TryParse(txt托盘列数.Text, out int width))
                {
                    if (width <= 0 || width > 10)
                    {
                        MessageBox.Show("托盘列数输入错误请检查,请输入1-10之间的整数");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("托盘列数输入错误请检查,请输入1-10之间的整数");
                    return;
                }

                if (double.TryParse(txt托盘行间距.Text, out double 行间距))
                {
                    if (行间距 <= 0)
                    {
                        MessageBox.Show("输入错误请检查,托盘行间距应大于0");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("输入错误请检查,请输入托盘行间距");
                    return;
                }

                if (double.TryParse(txt托盘列间距.Text, out double 列间距))
                {
                    if (列间距 <= 0)
                    {
                        MessageBox.Show("输入错误请检查,托盘列间距应大于0");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("输入错误请检查,请输入托盘列间距");
                    return;
                }

                if (double.TryParse(txt托盘高度.Text, out double 托盘高度))
                {
                    if (托盘高度 <= 0)
                    {
                        MessageBox.Show("输入错误请检查,托盘间距应大于0");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("输入错误请检查,请输入托盘间距");
                    return;
                }

                if (int.TryParse(txt上料吸嘴旋转角度.Text, out int 上料吸嘴旋转角度))
                {
                    if (上料吸嘴旋转角度 != -90 && 上料吸嘴旋转角度 != 0 && 上料吸嘴旋转角度 != 90)
                    {
                        MessageBox.Show("输入错误请检查,请输入上料吸嘴旋转角度，如-90、0、90。 提示：吸嘴顺时针旋转时，输入90；吸嘴逆时针旋转时，输入-90。");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("输入错误请检查,请输入上料吸嘴旋转角度，如-90、0、90。 提示：吸嘴顺时针旋转时，输入90；吸嘴逆时针旋转时，输入-90。");
                    return;
                }

                if (int.TryParse(txt上料吸嘴旋转角度四目.Text, out int 上料吸嘴旋转角度四目))
                {
                    if (上料吸嘴旋转角度 != -90 && 上料吸嘴旋转角度 != 0 && 上料吸嘴旋转角度 != 90)
                    {
                        MessageBox.Show("输入错误请检查,请输入上料吸嘴旋转角度，如-90、0、90。 提示：吸嘴顺时针旋转时，输入90；吸嘴逆时针旋转时，输入-90。");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("输入错误请检查,请输入上料吸嘴旋转角度，如-90、0、90。 提示：吸嘴顺时针旋转时，输入90；吸嘴逆时针旋转时，输入-90。");
                    return;
                }

                if (int.TryParse(txt搬运夹爪旋转角度.Text, out int 搬运夹爪旋转角度))
                {
                    if (搬运夹爪旋转角度 != -90 && 搬运夹爪旋转角度 != 0 && 搬运夹爪旋转角度 != 90)
                    {
                        MessageBox.Show("输入错误请检查,请输入搬运夹爪旋转角度，如-90、0、90。 提示：夹爪顺时针旋转时，输入90；夹爪逆时针旋转时，输入-90。");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("输入错误请检查,请输入搬运夹爪旋转角度，如-90、0、90。 提示：夹爪顺时针旋转时，输入90；夹爪逆时针旋转时，输入-90。");
                    return;
                }
                #endregion
                var trayManager = mainForm.trayManager;
                DialogResult result = MessageBox.Show($"请确认产品型号无误 “{txt输入产品型号.Text}” ", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string 产品型号 = txt输入产品型号.Text;
                    if (trayManager.TrayType.ContainsKey(产品型号))//修改新型号
                    {
                        trayManager.TrayType[产品型号].Length = length;
                        trayManager.TrayType[产品型号].Width = width;
                        trayManager.TrayType[产品型号].LineSpacing = 行间距;
                        trayManager.TrayType[产品型号].ColumnSpacing = 列间距;
                        trayManager.TrayType[产品型号].Height = 托盘高度;
                        trayManager.TrayType[产品型号].VacAngle = 上料吸嘴旋转角度;
                        trayManager.TrayType[产品型号].VacAngleFour = 上料吸嘴旋转角度四目;
                        trayManager.TrayType[产品型号].ClawsAngle = 搬运夹爪旋转角度;
                        trayManager.UpdateTrayTypeDic(trayManager.TrayType[产品型号]);
                        mainForm.RecordAndShow($"{产品型号}参数修改，每行的产品数：{length} 每列的产品数：{width} 行间距：{行间距} 列间距：{列间距} 托盘高度：{托盘高度} " +
                            $"上料吸嘴旋转角度：{上料吸嘴旋转角度} 搬运夹爪旋转角度：{搬运夹爪旋转角度}", LogType.Modification);
                    }
                    else//添加新型号
                    {
                        TypeOfTray typeOfTray = new TypeOfTray()
                        {
                            Index = trayManager.TrayType.Count + 1,
                            TrayType = 产品型号,
                            Length = length,
                            Width = width,
                            LineSpacing = 行间距,
                            ColumnSpacing = 列间距,
                            Height = 托盘高度,
                            VacAngle = 上料吸嘴旋转角度,
                            VacAngleFour = 上料吸嘴旋转角度四目,
                            ClawsAngle = 搬运夹爪旋转角度
                        };
                        trayManager.UpdateTrayTypeDic(typeOfTray);
                        mainForm.RecordAndShow($"{产品型号}新增，每行的产品数：{length} 每列的产品数：{width} 行间距：{行间距} 列间距：{列间距} 托盘高度：{托盘高度} " +
                            $"上料吸嘴旋转角度：{上料吸嘴旋转角度} 搬运夹爪旋转角度：{搬运夹爪旋转角度}", LogType.Modification);
                    }
                    mainForm.UpdateTypeToCOB(trayManager);

                    MessageBox.Show("输入成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("输入错误请检查!");
            }
        }

        private async void BTN配方_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Tag.ToString())
            {
                case "PlcInIO[738]":
                    await mainForm.CaliButtonAsync(button, 1000, "确认是否将当前产品示教位置导出。", "配方导出");
                    break;
                case "PlcInIO[739]":
                    await mainForm.CaliButtonAsync(button, 1000, "确认是否将其他产品示教位置导入到当前产品。", "配方导入");
                    break;
                default: break;
            }
        }

        private void BTN按钮_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Name)
            {
                case "btn引脚检测打开":
                    mainForm.MainButton("引脚检测打开？", "引脚检测打开", button.Tag.ToString(), false);
                    break;
                case "btn引脚检测关闭":
                    mainForm.MainButton("引脚检测关闭？", "引脚检测关闭", button.Tag.ToString());
                    break;
                case "btn计数功能清零":
                    mainForm.MainButton("使用次数是否清零？", "计数清零", button.Tag.ToString());
                    break;
                default: break;
            }
        }
    }
}
