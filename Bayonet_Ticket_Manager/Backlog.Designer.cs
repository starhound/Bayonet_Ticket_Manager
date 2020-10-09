namespace Bayonet_Ticket_Manager
{
    partial class Backlog
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
            this.label1 = new System.Windows.Forms.Label();
            this.pendingListBox = new System.Windows.Forms.ListBox();
            this.expandButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.approvedCheckBox = new System.Windows.Forms.CheckBox();
            this.deniedCheckBox = new System.Windows.Forms.CheckBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.reasonTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pending Tickets:";
            // 
            // pendingListBox
            // 
            this.pendingListBox.FormattingEnabled = true;
            this.pendingListBox.Location = new System.Drawing.Point(106, 12);
            this.pendingListBox.Name = "pendingListBox";
            this.pendingListBox.Size = new System.Drawing.Size(357, 121);
            this.pendingListBox.TabIndex = 1;
            // 
            // expandButton
            // 
            this.expandButton.Location = new System.Drawing.Point(106, 140);
            this.expandButton.Name = "expandButton";
            this.expandButton.Size = new System.Drawing.Size(75, 23);
            this.expandButton.TabIndex = 2;
            this.expandButton.Text = "Expand";
            this.expandButton.UseVisualStyleBackColor = true;
            this.expandButton.Click += new System.EventHandler(this.expandButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Description:";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(106, 188);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ReadOnly = true;
            this.descriptionTextBox.Size = new System.Drawing.Size(357, 132);
            this.descriptionTextBox.TabIndex = 5;
            // 
            // approvedCheckBox
            // 
            this.approvedCheckBox.AutoSize = true;
            this.approvedCheckBox.Location = new System.Drawing.Point(106, 326);
            this.approvedCheckBox.Name = "approvedCheckBox";
            this.approvedCheckBox.Size = new System.Drawing.Size(72, 17);
            this.approvedCheckBox.TabIndex = 6;
            this.approvedCheckBox.Text = "Approved";
            this.approvedCheckBox.UseVisualStyleBackColor = true;
            // 
            // deniedCheckBox
            // 
            this.deniedCheckBox.AutoSize = true;
            this.deniedCheckBox.Location = new System.Drawing.Point(184, 326);
            this.deniedCheckBox.Name = "deniedCheckBox";
            this.deniedCheckBox.Size = new System.Drawing.Size(60, 17);
            this.deniedCheckBox.TabIndex = 7;
            this.deniedCheckBox.Text = "Denied";
            this.deniedCheckBox.UseVisualStyleBackColor = true;
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(12, 415);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 8;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(388, 415);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 9;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 374);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Reason:";
            // 
            // reasonTextBox
            // 
            this.reasonTextBox.Location = new System.Drawing.Point(106, 371);
            this.reasonTextBox.Name = "reasonTextBox";
            this.reasonTextBox.Size = new System.Drawing.Size(357, 20);
            this.reasonTextBox.TabIndex = 11;
            // 
            // Backlog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 450);
            this.Controls.Add(this.reasonTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.deniedCheckBox);
            this.Controls.Add(this.approvedCheckBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.expandButton);
            this.Controls.Add(this.pendingListBox);
            this.Controls.Add(this.label1);
            this.Name = "Backlog";
            this.Text = "Backlog";
            this.Load += new System.EventHandler(this.Backlog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox pendingListBox;
        private System.Windows.Forms.Button expandButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.CheckBox approvedCheckBox;
        private System.Windows.Forms.CheckBox deniedCheckBox;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox reasonTextBox;
    }
}