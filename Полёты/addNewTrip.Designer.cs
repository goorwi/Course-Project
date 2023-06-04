
namespace Полёты
{
    partial class addNewTrip
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
            this.createButton = new System.Windows.Forms.Button();
            this.tripNoTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.idCompTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.townFromTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.namePlaneTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.timeInTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.timeOutTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.townToTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // createButton
            // 
            this.createButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.createButton.Location = new System.Drawing.Point(175, 310);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(100, 30);
            this.createButton.TabIndex = 8;
            this.createButton.Text = "Сохранить";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // tripNoTextBox
            // 
            this.tripNoTextBox.Location = new System.Drawing.Point(200, 79);
            this.tripNoTextBox.Name = "tripNoTextBox";
            this.tripNoTextBox.Size = new System.Drawing.Size(180, 20);
            this.tripNoTextBox.TabIndex = 1;
            this.tripNoTextBox.TextChanged += new System.EventHandler(this.tripNoTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(95, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Номер рейса:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(25, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Создание записи:\r\nРейс";
            // 
            // idCompTextBox
            // 
            this.idCompTextBox.Location = new System.Drawing.Point(200, 105);
            this.idCompTextBox.Name = "idCompTextBox";
            this.idCompTextBox.Size = new System.Drawing.Size(180, 20);
            this.idCompTextBox.TabIndex = 2;
            this.idCompTextBox.TextChanged += new System.EventHandler(this.idCompTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(101, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "ID компании:";
            // 
            // townFromTextBox
            // 
            this.townFromTextBox.Location = new System.Drawing.Point(200, 157);
            this.townFromTextBox.Name = "townFromTextBox";
            this.townFromTextBox.Size = new System.Drawing.Size(180, 20);
            this.townFromTextBox.TabIndex = 4;
            this.townFromTextBox.TextChanged += new System.EventHandler(this.townFromTextBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(52, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Город отправления:";
            // 
            // namePlaneTextBox
            // 
            this.namePlaneTextBox.Location = new System.Drawing.Point(200, 131);
            this.namePlaneTextBox.Name = "namePlaneTextBox";
            this.namePlaneTextBox.Size = new System.Drawing.Size(180, 20);
            this.namePlaneTextBox.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(125, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Самолёт:";
            // 
            // timeInTextBox
            // 
            this.timeInTextBox.Location = new System.Drawing.Point(200, 235);
            this.timeInTextBox.Name = "timeInTextBox";
            this.timeInTextBox.Size = new System.Drawing.Size(180, 20);
            this.timeInTextBox.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(71, 236);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Время прибытия:";
            // 
            // timeOutTextBox
            // 
            this.timeOutTextBox.Location = new System.Drawing.Point(200, 209);
            this.timeOutTextBox.Name = "timeOutTextBox";
            this.timeOutTextBox.Size = new System.Drawing.Size(180, 20);
            this.timeOutTextBox.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(50, 210);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(144, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "Время отправления:";
            // 
            // townToTextBox
            // 
            this.townToTextBox.Location = new System.Drawing.Point(200, 183);
            this.townToTextBox.Name = "townToTextBox";
            this.townToTextBox.Size = new System.Drawing.Size(180, 20);
            this.townToTextBox.TabIndex = 5;
            this.townToTextBox.TextChanged += new System.EventHandler(this.townToTextBox_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(73, 184);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "Город прибытия:";
            // 
            // addNewTrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 367);
            this.Controls.Add(this.timeInTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.timeOutTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.townToTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.townFromTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.namePlaneTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.idCompTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.tripNoTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "addNewTrip";
            this.Text = "Создание новой записи";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.TextBox tripNoTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox idCompTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox townFromTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox namePlaneTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox timeInTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox timeOutTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox townToTextBox;
        private System.Windows.Forms.Label label9;
    }
}