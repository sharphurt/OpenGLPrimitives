using System.ComponentModel;

namespace OpenGLPrimitives
{
    partial class AboutWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutWindow));
            this.labelHeader = new System.Windows.Forms.Label();
            this.labelAbout = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.labelControl = new System.Windows.Forms.Label();
            this.labelControlHeader = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelCredits = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeader
            // 
            this.labelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.labelHeader.Location = new System.Drawing.Point(0, 0);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(693, 56);
            this.labelHeader.TabIndex = 0;
            this.labelHeader.Text = "OpenGL Primitives Demostration";
            this.labelHeader.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // labelAbout
            // 
            this.labelAbout.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.labelAbout.Location = new System.Drawing.Point(0, 110);
            this.labelAbout.Margin = new System.Windows.Forms.Padding(50, 0, 50, 0);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.labelAbout.Size = new System.Drawing.Size(693, 179);
            this.labelAbout.TabIndex = 1;
            this.labelAbout.Text = resources.GetString("labelAbout.Text");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Controls.Add(this.labelControl);
            this.panel1.Controls.Add(this.labelControlHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 289);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(693, 200);
            this.panel1.TabIndex = 4;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.linkLabel1.Location = new System.Drawing.Point(0, 171);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.linkLabel1.Size = new System.Drawing.Size(693, 29);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://github.com/sharphurt/OpenGLPrimitives";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelControl
            // 
            this.labelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.labelControl.Location = new System.Drawing.Point(0, 44);
            this.labelControl.Name = "labelControl";
            this.labelControl.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.labelControl.Size = new System.Drawing.Size(693, 156);
            this.labelControl.TabIndex = 1;
            this.labelControl.Text = resources.GetString("labelControl.Text");
            // 
            // labelControlHeader
            // 
            this.labelControlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControlHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.labelControlHeader.Location = new System.Drawing.Point(0, 0);
            this.labelControlHeader.Name = "labelControlHeader";
            this.labelControlHeader.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.labelControlHeader.Size = new System.Drawing.Size(693, 44);
            this.labelControlHeader.TabIndex = 0;
            this.labelControlHeader.Text = "Управление";
            this.labelControlHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelCredits);
            this.panel2.Controls.Add(this.labelHeader);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(693, 110);
            this.panel2.TabIndex = 5;
            // 
            // labelCredits
            // 
            this.labelCredits.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelCredits.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.labelCredits.Location = new System.Drawing.Point(0, 56);
            this.labelCredits.Name = "labelCredits";
            this.labelCredits.Size = new System.Drawing.Size(693, 27);
            this.labelCredits.TabIndex = 1;
            this.labelCredits.Text = "Лукоянов Павел Валерьевич, РИ-390012, 2021";
            this.labelCredits.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelAbout);
            this.Controls.Add(this.panel2);
            this.Name = "AboutWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label labelAbout;
        private System.Windows.Forms.Label labelControl;
        private System.Windows.Forms.Label labelControlHeader;
        private System.Windows.Forms.Label labelCredits;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;

        #endregion
    }
}