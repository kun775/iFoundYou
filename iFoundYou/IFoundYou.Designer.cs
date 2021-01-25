
namespace iFoundYou
{
    partial class IFoundYou
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IFoundYou));
            this.label_Position = new System.Windows.Forms.Label();
            this.label_Run = new System.Windows.Forms.Label();
            this.label_Type = new System.Windows.Forms.Label();
            this.label_Title = new System.Windows.Forms.Label();
            this.label_ID = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Button_Close = new System.Windows.Forms.Button();
            this.Button_Rename = new System.Windows.Forms.Button();
            this.Button_Resume = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_Position
            // 
            this.label_Position.AutoSize = true;
            this.label_Position.Location = new System.Drawing.Point(160, 6);
            this.label_Position.MinimumSize = new System.Drawing.Size(100, 12);
            this.label_Position.Name = "label_Position";
            this.label_Position.Size = new System.Drawing.Size(100, 12);
            this.label_Position.TabIndex = 15;
            this.label_Position.Text = "(0, 0)";
            this.label_Position.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Run
            // 
            this.label_Run.AutoSize = true;
            this.label_Run.Location = new System.Drawing.Point(3, 72);
            this.label_Run.Name = "label_Run";
            this.label_Run.Size = new System.Drawing.Size(65, 12);
            this.label_Run.TabIndex = 14;
            this.label_Run.Text = "运行路径：";
            this.label_Run.Click += new System.EventHandler(this.Label_Run_Click);
            // 
            // label_Type
            // 
            this.label_Type.AutoSize = true;
            this.label_Type.Location = new System.Drawing.Point(3, 50);
            this.label_Type.Name = "label_Type";
            this.label_Type.Size = new System.Drawing.Size(65, 12);
            this.label_Type.TabIndex = 13;
            this.label_Type.Text = "窗口类型：";
            // 
            // label_Title
            // 
            this.label_Title.AutoSize = true;
            this.label_Title.Location = new System.Drawing.Point(3, 28);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(65, 12);
            this.label_Title.TabIndex = 12;
            this.label_Title.Text = "窗口标题：";
            // 
            // label_ID
            // 
            this.label_ID.AutoSize = true;
            this.label_ID.Location = new System.Drawing.Point(3, 6);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(65, 12);
            this.label_ID.TabIndex = 11;
            this.label_ID.Text = "窗口句柄：";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Button_Close
            // 
            this.Button_Close.Location = new System.Drawing.Point(265, 1);
            this.Button_Close.Name = "Button_Close";
            this.Button_Close.Size = new System.Drawing.Size(75, 23);
            this.Button_Close.TabIndex = 16;
            this.Button_Close.Text = "关闭进程";
            this.Button_Close.UseVisualStyleBackColor = true;
            this.Button_Close.Click += new System.EventHandler(this.Button_Close_Click);
            // 
            // Button_Rename
            // 
            this.Button_Rename.Location = new System.Drawing.Point(346, 1);
            this.Button_Rename.Name = "Button_Rename";
            this.Button_Rename.Size = new System.Drawing.Size(75, 23);
            this.Button_Rename.TabIndex = 17;
            this.Button_Rename.Text = "重命名";
            this.Button_Rename.UseVisualStyleBackColor = true;
            this.Button_Rename.Click += new System.EventHandler(this.Button_Rename_Click);
            // 
            // Button_Resume
            // 
            this.Button_Resume.Location = new System.Drawing.Point(427, 1);
            this.Button_Resume.Name = "Button_Resume";
            this.Button_Resume.Size = new System.Drawing.Size(75, 23);
            this.Button_Resume.TabIndex = 18;
            this.Button_Resume.Text = "恢复";
            this.Button_Resume.UseVisualStyleBackColor = true;
            this.Button_Resume.Click += new System.EventHandler(this.Button_Resume_Click);
            // 
            // IFoundYou
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(203)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(504, 91);
            this.Controls.Add(this.Button_Resume);
            this.Controls.Add(this.Button_Rename);
            this.Controls.Add(this.Button_Close);
            this.Controls.Add(this.label_Position);
            this.Controls.Add(this.label_Run);
            this.Controls.Add(this.label_Type);
            this.Controls.Add(this.label_Title);
            this.Controls.Add(this.label_ID);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IFoundYou";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "我找到你了-获取弹窗信息(按F9暂停，按F10打开运行路径)";
            this.Load += new System.EventHandler(this.IFoundYou_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Position;
        private System.Windows.Forms.Label label_Run;
        private System.Windows.Forms.Label label_Type;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Label label_ID;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button Button_Close;
        private System.Windows.Forms.Button Button_Rename;
        private System.Windows.Forms.Button Button_Resume;
    }
}

