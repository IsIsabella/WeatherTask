namespace TestTask
{
    partial class History
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
            this.richTextBoxHistory = new System.Windows.Forms.RichTextBox();
            this.buttonReturn = new System.Windows.Forms.Button();
            this.buttonFilterOpDateAsc = new System.Windows.Forms.Button();
            this.buttonFilterCityAsc = new System.Windows.Forms.Button();
            this.buttonFilterDateAsc = new System.Windows.Forms.Button();
            this.buttonFilterTempAsc = new System.Windows.Forms.Button();
            this.buttonFilterOpDateDesc = new System.Windows.Forms.Button();
            this.buttonFilterCityDesc = new System.Windows.Forms.Button();
            this.buttonFilterDateDesc = new System.Windows.Forms.Button();
            this.buttonFilterTempDesc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBoxHistory
            // 
            this.richTextBoxHistory.Location = new System.Drawing.Point(32, 257);
            this.richTextBoxHistory.Name = "richTextBoxHistory";
            this.richTextBoxHistory.Size = new System.Drawing.Size(821, 487);
            this.richTextBoxHistory.TabIndex = 0;
            this.richTextBoxHistory.Text = "";
            // 
            // buttonReturn
            // 
            this.buttonReturn.Location = new System.Drawing.Point(386, 771);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(144, 46);
            this.buttonReturn.TabIndex = 96;
            this.buttonReturn.Text = "Вернуться на главный экран";
            this.buttonReturn.UseVisualStyleBackColor = true;
            this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
            // 
            // buttonFilterOpDateAsc
            // 
            this.buttonFilterOpDateAsc.Location = new System.Drawing.Point(126, 12);
            this.buttonFilterOpDateAsc.Name = "buttonFilterOpDateAsc";
            this.buttonFilterOpDateAsc.Size = new System.Drawing.Size(111, 96);
            this.buttonFilterOpDateAsc.TabIndex = 97;
            this.buttonFilterOpDateAsc.Text = "Даты операций по возрастанию";
            this.buttonFilterOpDateAsc.UseVisualStyleBackColor = true;
            this.buttonFilterOpDateAsc.Click += new System.EventHandler(this.buttonFilterOpDateAsc_Click);
            // 
            // buttonFilterCityAsc
            // 
            this.buttonFilterCityAsc.Location = new System.Drawing.Point(270, 12);
            this.buttonFilterCityAsc.Name = "buttonFilterCityAsc";
            this.buttonFilterCityAsc.Size = new System.Drawing.Size(111, 96);
            this.buttonFilterCityAsc.TabIndex = 98;
            this.buttonFilterCityAsc.Text = "Города в алфавитном порядке";
            this.buttonFilterCityAsc.UseVisualStyleBackColor = true;
            this.buttonFilterCityAsc.Click += new System.EventHandler(this.buttonFilterCityAsc_Click);
            // 
            // buttonFilterDateAsc
            // 
            this.buttonFilterDateAsc.Location = new System.Drawing.Point(419, 12);
            this.buttonFilterDateAsc.Name = "buttonFilterDateAsc";
            this.buttonFilterDateAsc.Size = new System.Drawing.Size(111, 96);
            this.buttonFilterDateAsc.TabIndex = 99;
            this.buttonFilterDateAsc.Text = "Даты прогнозов по возрастанию";
            this.buttonFilterDateAsc.UseVisualStyleBackColor = true;
            this.buttonFilterDateAsc.Click += new System.EventHandler(this.buttonFilterDateAsc_Click);
            // 
            // buttonFilterTempAsc
            // 
            this.buttonFilterTempAsc.Location = new System.Drawing.Point(561, 12);
            this.buttonFilterTempAsc.Name = "buttonFilterTempAsc";
            this.buttonFilterTempAsc.Size = new System.Drawing.Size(111, 96);
            this.buttonFilterTempAsc.TabIndex = 100;
            this.buttonFilterTempAsc.Text = "Температура по возрастанию";
            this.buttonFilterTempAsc.UseVisualStyleBackColor = true;
            this.buttonFilterTempAsc.Click += new System.EventHandler(this.buttonFilterTempAsc_Click);
            // 
            // buttonFilterOpDateDesc
            // 
            this.buttonFilterOpDateDesc.Location = new System.Drawing.Point(126, 133);
            this.buttonFilterOpDateDesc.Name = "buttonFilterOpDateDesc";
            this.buttonFilterOpDateDesc.Size = new System.Drawing.Size(111, 94);
            this.buttonFilterOpDateDesc.TabIndex = 101;
            this.buttonFilterOpDateDesc.Text = "Даты операций по убыванию";
            this.buttonFilterOpDateDesc.UseVisualStyleBackColor = true;
            this.buttonFilterOpDateDesc.Click += new System.EventHandler(this.buttonFilterOpDateDesc_Click);
            // 
            // buttonFilterCityDesc
            // 
            this.buttonFilterCityDesc.Location = new System.Drawing.Point(270, 131);
            this.buttonFilterCityDesc.Name = "buttonFilterCityDesc";
            this.buttonFilterCityDesc.Size = new System.Drawing.Size(111, 95);
            this.buttonFilterCityDesc.TabIndex = 102;
            this.buttonFilterCityDesc.Text = "Города в обратном алфавитном порядке";
            this.buttonFilterCityDesc.UseVisualStyleBackColor = true;
            this.buttonFilterCityDesc.Click += new System.EventHandler(this.buttonFilterCityDesc_Click);
            // 
            // buttonFilterDateDesc
            // 
            this.buttonFilterDateDesc.Location = new System.Drawing.Point(419, 132);
            this.buttonFilterDateDesc.Name = "buttonFilterDateDesc";
            this.buttonFilterDateDesc.Size = new System.Drawing.Size(111, 95);
            this.buttonFilterDateDesc.TabIndex = 103;
            this.buttonFilterDateDesc.Text = "Даты прогнозов по убыванию";
            this.buttonFilterDateDesc.UseVisualStyleBackColor = true;
            this.buttonFilterDateDesc.Click += new System.EventHandler(this.buttonFilterDateDesc_Click);
            // 
            // buttonFilterTempDesc
            // 
            this.buttonFilterTempDesc.Location = new System.Drawing.Point(561, 133);
            this.buttonFilterTempDesc.Name = "buttonFilterTempDesc";
            this.buttonFilterTempDesc.Size = new System.Drawing.Size(111, 95);
            this.buttonFilterTempDesc.TabIndex = 104;
            this.buttonFilterTempDesc.Text = "Температура по убыванию";
            this.buttonFilterTempDesc.UseVisualStyleBackColor = true;
            this.buttonFilterTempDesc.Click += new System.EventHandler(this.buttonFilterTempDesc_Click);
            // 
            // History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 853);
            this.Controls.Add(this.buttonFilterTempDesc);
            this.Controls.Add(this.buttonFilterDateDesc);
            this.Controls.Add(this.buttonFilterCityDesc);
            this.Controls.Add(this.buttonFilterOpDateDesc);
            this.Controls.Add(this.buttonFilterTempAsc);
            this.Controls.Add(this.buttonFilterDateAsc);
            this.Controls.Add(this.buttonFilterCityAsc);
            this.Controls.Add(this.buttonFilterOpDateAsc);
            this.Controls.Add(this.buttonReturn);
            this.Controls.Add(this.richTextBoxHistory);
            this.Name = "History";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "History";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxHistory;
        private System.Windows.Forms.Button buttonReturn;
        private System.Windows.Forms.Button buttonFilterOpDateAsc;
        private System.Windows.Forms.Button buttonFilterCityAsc;
        private System.Windows.Forms.Button buttonFilterDateAsc;
        private System.Windows.Forms.Button buttonFilterTempAsc;
        private System.Windows.Forms.Button buttonFilterOpDateDesc;
        private System.Windows.Forms.Button buttonFilterCityDesc;
        private System.Windows.Forms.Button buttonFilterDateDesc;
        private System.Windows.Forms.Button buttonFilterTempDesc;
    }
}