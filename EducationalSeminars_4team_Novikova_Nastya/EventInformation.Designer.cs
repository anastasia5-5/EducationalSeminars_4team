namespace EducationalSeminars_4team_Novikova_Nastya
{
    partial class EventInformation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewInformation = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInformation)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewInformation
            // 
            this.dataGridViewInformation.AllowUserToResizeColumns = false;
            this.dataGridViewInformation.AllowUserToResizeRows = false;
            this.dataGridViewInformation.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridViewInformation.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewInformation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewInformation.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInformation.Location = new System.Drawing.Point(269, 67);
            this.dataGridViewInformation.Name = "dataGridViewInformation";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewInformation.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewInformation.RowHeadersVisible = false;
            this.dataGridViewInformation.RowHeadersWidth = 82;
            this.dataGridViewInformation.RowTemplate.Height = 33;
            this.dataGridViewInformation.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridViewInformation.Size = new System.Drawing.Size(846, 101);
            this.dataGridViewInformation.TabIndex = 2;
            // 
            // EventInformation
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1387, 450);
            this.Controls.Add(this.dataGridViewInformation);
            this.Name = "EventInformation";
            this.Text = "EventInformation";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInformation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridViewInformation;
    }
}