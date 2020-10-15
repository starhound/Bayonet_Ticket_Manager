namespace Bayonet_Ticket_Manager
{
    partial class Form1
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.activeTicketsListBox = new System.Windows.Forms.ListBox();
            this.ticketDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.notesTextBox = new System.Windows.Forms.TextBox();
            this.expandButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.completedCheckBox = new System.Windows.Forms.CheckBox();
            this.inProgressCheckBox = new System.Windows.Forms.CheckBox();
            this.pendingCheckBox = new System.Windows.Forms.CheckBox();
            this.updateButton = new System.Windows.Forms.Button();
            this.discardButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.inProgressTicketBox = new System.Windows.Forms.ListBox();
            this.backlogButton = new System.Windows.Forms.Button();
            this.remoteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Active Tickets:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ticket Description:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 466);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Notes:";
            // 
            // activeTicketsListBox
            // 
            this.activeTicketsListBox.FormattingEnabled = true;
            this.activeTicketsListBox.Location = new System.Drawing.Point(119, 12);
            this.activeTicketsListBox.Name = "activeTicketsListBox";
            this.activeTicketsListBox.Size = new System.Drawing.Size(276, 95);
            this.activeTicketsListBox.TabIndex = 4;
            this.activeTicketsListBox.SelectedIndexChanged += new System.EventHandler(this.activeTicketsListBox_SelectedIndexChanged);
            // 
            // ticketDescriptionTextBox
            // 
            this.ticketDescriptionTextBox.Location = new System.Drawing.Point(119, 142);
            this.ticketDescriptionTextBox.Multiline = true;
            this.ticketDescriptionTextBox.Name = "ticketDescriptionTextBox";
            this.ticketDescriptionTextBox.ReadOnly = true;
            this.ticketDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ticketDescriptionTextBox.Size = new System.Drawing.Size(633, 318);
            this.ticketDescriptionTextBox.TabIndex = 5;
            // 
            // notesTextBox
            // 
            this.notesTextBox.Location = new System.Drawing.Point(119, 466);
            this.notesTextBox.Multiline = true;
            this.notesTextBox.Name = "notesTextBox";
            this.notesTextBox.Size = new System.Drawing.Size(633, 70);
            this.notesTextBox.TabIndex = 6;
            // 
            // expandButton
            // 
            this.expandButton.Location = new System.Drawing.Point(119, 113);
            this.expandButton.Name = "expandButton";
            this.expandButton.Size = new System.Drawing.Size(75, 23);
            this.expandButton.TabIndex = 7;
            this.expandButton.Text = "Expand";
            this.expandButton.UseVisualStyleBackColor = true;
            this.expandButton.Click += new System.EventHandler(this.expandButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(677, 113);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 8;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // completedCheckBox
            // 
            this.completedCheckBox.AutoSize = true;
            this.completedCheckBox.Location = new System.Drawing.Point(119, 542);
            this.completedCheckBox.Name = "completedCheckBox";
            this.completedCheckBox.Size = new System.Drawing.Size(76, 17);
            this.completedCheckBox.TabIndex = 10;
            this.completedCheckBox.Text = "Completed";
            this.completedCheckBox.UseVisualStyleBackColor = true;
            // 
            // inProgressCheckBox
            // 
            this.inProgressCheckBox.AutoSize = true;
            this.inProgressCheckBox.Location = new System.Drawing.Point(201, 542);
            this.inProgressCheckBox.Name = "inProgressCheckBox";
            this.inProgressCheckBox.Size = new System.Drawing.Size(79, 17);
            this.inProgressCheckBox.TabIndex = 11;
            this.inProgressCheckBox.Text = "In Progress";
            this.inProgressCheckBox.UseVisualStyleBackColor = true;
            // 
            // pendingCheckBox
            // 
            this.pendingCheckBox.AutoSize = true;
            this.pendingCheckBox.Location = new System.Drawing.Point(286, 542);
            this.pendingCheckBox.Name = "pendingCheckBox";
            this.pendingCheckBox.Size = new System.Drawing.Size(65, 17);
            this.pendingCheckBox.TabIndex = 12;
            this.pendingCheckBox.Text = "Pending";
            this.pendingCheckBox.UseVisualStyleBackColor = true;
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(12, 577);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 13;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // discardButton
            // 
            this.discardButton.Location = new System.Drawing.Point(104, 577);
            this.discardButton.Name = "discardButton";
            this.discardButton.Size = new System.Drawing.Size(75, 23);
            this.discardButton.TabIndex = 14;
            this.discardButton.Text = "Discard";
            this.discardButton.UseVisualStyleBackColor = true;
            this.discardButton.Click += new System.EventHandler(this.discardButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(677, 577);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 15;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(407, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "In Progress:";
            // 
            // inProgressTicketBox
            // 
            this.inProgressTicketBox.FormattingEnabled = true;
            this.inProgressTicketBox.Location = new System.Drawing.Point(476, 12);
            this.inProgressTicketBox.Name = "inProgressTicketBox";
            this.inProgressTicketBox.Size = new System.Drawing.Size(276, 95);
            this.inProgressTicketBox.TabIndex = 17;
            this.inProgressTicketBox.SelectedIndexChanged += new System.EventHandler(this.inProgressTicketBox_SelectedIndexChanged);
            // 
            // backlogButton
            // 
            this.backlogButton.Location = new System.Drawing.Point(596, 578);
            this.backlogButton.Name = "backlogButton";
            this.backlogButton.Size = new System.Drawing.Size(75, 23);
            this.backlogButton.TabIndex = 18;
            this.backlogButton.Text = "Backlog";
            this.backlogButton.UseVisualStyleBackColor = true;
            this.backlogButton.Click += new System.EventHandler(this.backlogButton_Click);
            // 
            // remoteButton
            // 
            this.remoteButton.Location = new System.Drawing.Point(286, 577);
            this.remoteButton.Name = "remoteButton";
            this.remoteButton.Size = new System.Drawing.Size(109, 23);
            this.remoteButton.TabIndex = 19;
            this.remoteButton.Text = "Remote Connect";
            this.remoteButton.UseVisualStyleBackColor = true;
            this.remoteButton.Click += new System.EventHandler(this.remoteButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 625);
            this.Controls.Add(this.remoteButton);
            this.Controls.Add(this.backlogButton);
            this.Controls.Add(this.inProgressTicketBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.discardButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.pendingCheckBox);
            this.Controls.Add(this.inProgressCheckBox);
            this.Controls.Add(this.completedCheckBox);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.expandButton);
            this.Controls.Add(this.notesTextBox);
            this.Controls.Add(this.ticketDescriptionTextBox);
            this.Controls.Add(this.activeTicketsListBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Bayonet IT Ticket Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox activeTicketsListBox;
        private System.Windows.Forms.TextBox ticketDescriptionTextBox;
        private System.Windows.Forms.TextBox notesTextBox;
        private System.Windows.Forms.Button expandButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.CheckBox completedCheckBox;
        private System.Windows.Forms.CheckBox inProgressCheckBox;
        private System.Windows.Forms.CheckBox pendingCheckBox;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button discardButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox inProgressTicketBox;
        private System.Windows.Forms.Button backlogButton;
        private System.Windows.Forms.Button remoteButton;
    }
}

