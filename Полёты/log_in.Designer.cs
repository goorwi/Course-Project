namespace Полёты
{
    partial class StartMenu
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.log_in_label = new System.Windows.Forms.Label();
            this.login = new System.Windows.Forms.TextBox();
            this.getConnect_button = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.TextBox();
            this.password_label = new System.Windows.Forms.Label();
            this.newUser_link = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // log_in_label
            // 
            this.log_in_label.AutoSize = true;
            this.log_in_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.log_in_label.Location = new System.Drawing.Point(114, 46);
            this.log_in_label.Name = "log_in_label";
            this.log_in_label.Size = new System.Drawing.Size(59, 20);
            this.log_in_label.TabIndex = 0;
            this.log_in_label.Text = "Логин:";
            // 
            // login
            // 
            this.login.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.login.Location = new System.Drawing.Point(191, 43);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(205, 26);
            this.login.TabIndex = 1;
            // 
            // getConnect_button
            // 
            this.getConnect_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.getConnect_button.Location = new System.Drawing.Point(175, 136);
            this.getConnect_button.Name = "getConnect_button";
            this.getConnect_button.Size = new System.Drawing.Size(165, 33);
            this.getConnect_button.TabIndex = 3;
            this.getConnect_button.Text = "Подключиться";
            this.getConnect_button.UseVisualStyleBackColor = true;
            this.getConnect_button.Click += new System.EventHandler(this.getConnect_button_Click);
            // 
            // password
            // 
            this.password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.password.Location = new System.Drawing.Point(191, 75);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(205, 26);
            this.password.TabIndex = 2;
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.password_label.Location = new System.Drawing.Point(114, 78);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(71, 20);
            this.password_label.TabIndex = 0;
            this.password_label.Text = "Пароль:";
            // 
            // newUser_link
            // 
            this.newUser_link.AutoSize = true;
            this.newUser_link.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.newUser_link.Location = new System.Drawing.Point(10, 176);
            this.newUser_link.Name = "newUser_link";
            this.newUser_link.Size = new System.Drawing.Size(163, 17);
            this.newUser_link.TabIndex = 4;
            this.newUser_link.TabStop = true;
            this.newUser_link.Text = "Создать новый аккаунт";
            this.newUser_link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.newUser_link_LinkClicked);
            // 
            // StartMenu
            // 
            this.ClientSize = new System.Drawing.Size(504, 211);
            this.Controls.Add(this.newUser_link);
            this.Controls.Add(this.password);
            this.Controls.Add(this.password_label);
            this.Controls.Add(this.getConnect_button);
            this.Controls.Add(this.login);
            this.Controls.Add(this.log_in_label);
            this.Name = "StartMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StartMenu_FormClosing);
            this.Load += new System.EventHandler(this.StartMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label log_in_label;
        private System.Windows.Forms.TextBox login;
        private System.Windows.Forms.Button getConnect_button;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.LinkLabel newUser_link;
    }
}

