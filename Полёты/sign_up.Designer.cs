namespace Полёты
{
    partial class sign_up
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
            this.password = new System.Windows.Forms.TextBox();
            this.password_label = new System.Windows.Forms.Label();
            this.create_button = new System.Windows.Forms.Button();
            this.login = new System.Windows.Forms.TextBox();
            this.log_in_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // password
            // 
            this.password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.password.Location = new System.Drawing.Point(188, 74);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(205, 26);
            this.password.TabIndex = 2;
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.password_label.Location = new System.Drawing.Point(111, 77);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(71, 20);
            this.password_label.TabIndex = 0;
            this.password_label.Text = "Пароль:";
            // 
            // create_button
            // 
            this.create_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.create_button.Location = new System.Drawing.Point(172, 135);
            this.create_button.Name = "create_button";
            this.create_button.Size = new System.Drawing.Size(165, 33);
            this.create_button.TabIndex = 3;
            this.create_button.Text = "Создать";
            this.create_button.UseVisualStyleBackColor = true;
            this.create_button.Click += new System.EventHandler(this.create_button_Click);
            // 
            // login
            // 
            this.login.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.login.Location = new System.Drawing.Point(188, 42);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(205, 26);
            this.login.TabIndex = 1;
            // 
            // log_in_label
            // 
            this.log_in_label.AutoSize = true;
            this.log_in_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.log_in_label.Location = new System.Drawing.Point(111, 45);
            this.log_in_label.Name = "log_in_label";
            this.log_in_label.Size = new System.Drawing.Size(59, 20);
            this.log_in_label.TabIndex = 0;
            this.log_in_label.Text = "Логин:";
            // 
            // sign_up
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 211);
            this.Controls.Add(this.password);
            this.Controls.Add(this.password_label);
            this.Controls.Add(this.create_button);
            this.Controls.Add(this.login);
            this.Controls.Add(this.log_in_label);
            this.Name = "sign_up";
            this.Text = "Регистрация";
            this.Load += new System.EventHandler(this.sign_up_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.Button create_button;
        private System.Windows.Forms.TextBox login;
        private System.Windows.Forms.Label log_in_label;
    }
}