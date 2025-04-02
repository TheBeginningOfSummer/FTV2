namespace FTV2
{
    partial class CheckForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LB_Log = new System.Windows.Forms.ListBox();
            this.CMS_菜单 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSM_查看 = new System.Windows.Forms.ToolStripMenuItem();
            this.DTP_CheckDate = new System.Windows.Forms.DateTimePicker();
            this.BTN_加载 = new System.Windows.Forms.Button();
            this.CB_条件 = new System.Windows.Forms.ComboBox();
            this.CMS_菜单.SuspendLayout();
            this.SuspendLayout();
            // 
            // LB_Log
            // 
            this.LB_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LB_Log.ContextMenuStrip = this.CMS_菜单;
            this.LB_Log.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LB_Log.FormattingEnabled = true;
            this.LB_Log.HorizontalScrollbar = true;
            this.LB_Log.ItemHeight = 21;
            this.LB_Log.Location = new System.Drawing.Point(12, 74);
            this.LB_Log.Name = "LB_Log";
            this.LB_Log.Size = new System.Drawing.Size(776, 361);
            this.LB_Log.TabIndex = 0;
            // 
            // CMS_菜单
            // 
            this.CMS_菜单.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSM_查看});
            this.CMS_菜单.Name = "CMS_菜单";
            this.CMS_菜单.Size = new System.Drawing.Size(101, 26);
            // 
            // TSM_查看
            // 
            this.TSM_查看.Name = "TSM_查看";
            this.TSM_查看.Size = new System.Drawing.Size(100, 22);
            this.TSM_查看.Text = "查看";
            this.TSM_查看.Click += new System.EventHandler(this.TSM_查看_Click);
            // 
            // DTP_CheckDate
            // 
            this.DTP_CheckDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DTP_CheckDate.Location = new System.Drawing.Point(202, 26);
            this.DTP_CheckDate.Name = "DTP_CheckDate";
            this.DTP_CheckDate.Size = new System.Drawing.Size(200, 21);
            this.DTP_CheckDate.TabIndex = 1;
            // 
            // BTN_加载
            // 
            this.BTN_加载.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_加载.Location = new System.Drawing.Point(464, 25);
            this.BTN_加载.Name = "BTN_加载";
            this.BTN_加载.Size = new System.Drawing.Size(75, 23);
            this.BTN_加载.TabIndex = 4;
            this.BTN_加载.Tag = "全部";
            this.BTN_加载.Text = "加载";
            this.BTN_加载.UseVisualStyleBackColor = true;
            this.BTN_加载.Click += new System.EventHandler(this.BTN_错误日志加载_Click);
            // 
            // CB_条件
            // 
            this.CB_条件.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CB_条件.FormattingEnabled = true;
            this.CB_条件.Items.AddRange(new object[] {
            "全部",
            "报警",
            "提示"});
            this.CB_条件.Location = new System.Drawing.Point(408, 27);
            this.CB_条件.Name = "CB_条件";
            this.CB_条件.Size = new System.Drawing.Size(50, 20);
            this.CB_条件.TabIndex = 5;
            this.CB_条件.Text = "全部";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CB_条件);
            this.Controls.Add(this.BTN_加载);
            this.Controls.Add(this.DTP_CheckDate);
            this.Controls.Add(this.LB_Log);
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报警记录";
            this.CMS_菜单.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox LB_Log;
        private System.Windows.Forms.DateTimePicker DTP_CheckDate;
        private System.Windows.Forms.Button BTN_加载;
        private System.Windows.Forms.ComboBox CB_条件;
        private System.Windows.Forms.ContextMenuStrip CMS_菜单;
        private System.Windows.Forms.ToolStripMenuItem TSM_查看;
    }
}