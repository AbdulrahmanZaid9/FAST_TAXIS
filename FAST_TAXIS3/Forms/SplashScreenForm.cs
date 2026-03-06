using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FAST_TAXIS3.Forms
{
    public partial class SplashScreenForm : Form
    {
        private Timer timer;
        private int progressValue = 0;

        public SplashScreenForm()
        {
            InitializeComponent();
            SetupForm();
            SetupTimer();
        }

        private void SetupForm()
        {
            this.Text = "";
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(600, 400);
            this.BackColor = Color.FromArgb(45, 45, 48); // خلفية رمادية زي الأول
            this.DoubleBuffered = true;
            this.Paint += SplashScreenForm_Paint;

            // Progress bar
            ProgressBar progressBar = new ProgressBar();
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(400, 10);
            progressBar.Location = new Point(100, 280);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.ForeColor = Color.FromArgb(255, 128, 0);
            progressBar.BackColor = Color.FromArgb(64, 64, 64);
            this.Controls.Add(progressBar);

            // Status label
            Label lblStatus = new Label();
            lblStatus.Name = "lblStatus";
            lblStatus.Text = "Loading...";
            lblStatus.Font = new Font("Segoe UI", 10);
            lblStatus.ForeColor = Color.LightGray;
            lblStatus.Size = new Size(400, 20);
            lblStatus.Location = new Point(100, 300);
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            lblStatus.BackColor = Color.Transparent;
            this.Controls.Add(lblStatus);

            // Version label
            Label lblVersion = new Label();
            lblVersion.Text = "Version 1.0.0";
            lblVersion.Font = new Font("Segoe UI", 8);
            lblVersion.ForeColor = Color.Gray;
            lblVersion.Size = new Size(100, 20);
            lblVersion.Location = new Point(480, 360);
            lblVersion.TextAlign = ContentAlignment.MiddleRight;
            lblVersion.BackColor = Color.Transparent;
            this.Controls.Add(lblVersion);

            // Copyright label
            Label lblCopyright = new Label();
            lblCopyright.Text = "© 2026 Fast Taxis Inc.";
            lblCopyright.Font = new Font("Segoe UI", 8);
            lblCopyright.ForeColor = Color.Gray;
            lblCopyright.Size = new Size(150, 20);
            lblCopyright.Location = new Point(20, 360);
            lblCopyright.TextAlign = ContentAlignment.MiddleLeft;
            lblCopyright.BackColor = Color.Transparent;
            this.Controls.Add(lblCopyright);
        }

        private void SplashScreenForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int centerX = this.ClientSize.Width / 2;
            int centerY = 150;

            // خلفية دائرية
            using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(25, 255, 128, 0)))
            {
                g.FillEllipse(circleBrush, centerX - 70, centerY - 70, 140, 140);
            }

            // تاكسي بسيط بخطوط
            int carX = centerX - 35;
            int carY = centerY - 15;

            using (Pen carPen = new Pen(Color.FromArgb(255, 128, 0), 3))
            {
                // جسم السيارة
                g.DrawRectangle(carPen, carX, carY, 70, 20);
                // سقف
                g.DrawRectangle(carPen, carX + 20, carY - 10, 30, 10);
                // عجلات
                g.DrawEllipse(carPen, carX + 5, carY + 15, 12, 12);
                g.DrawEllipse(carPen, carX + 53, carY + 15, 12, 12);
            }

            // خطوط سرعة
            using (Pen speedPen = new Pen(Color.FromArgb(80, 255, 128, 0), 2))
            {
                for (int i = 0; i < 4; i++)
                {
                    g.DrawLine(speedPen,
                        carX + 70 + i * 15, carY + 5,
                        carX + 90 + i * 20, carY - 5 - i);
                }
            }

            // نقاط تكنولوجية
            using (SolidBrush dotBrush = new SolidBrush(Color.FromArgb(255, 128, 0)))
            {
                for (int i = 0; i < 5; i++)
                {
                    g.FillEllipse(dotBrush, centerX - 80 + i * 25, centerY - 45, 3, 3);
                }
            }

            // نص FAST TAXIS مائل
            using (Font textFont = new Font("Impact", 20, FontStyle.Italic))
            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(255, 128, 0)))
            {
                string text = "FAST TAXIS";
                SizeF textSize = g.MeasureString(text, textFont);
                float textX = centerX - textSize.Width / 2;
                float textY = centerY + 50;
                g.DrawString(text, textFont, textBrush, textX, textY);
            }
        }
        private void SetupTimer()
        {
            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ProgressBar progressBar = this.Controls["progressBar"] as ProgressBar;
            Label lblStatus = this.Controls["lblStatus"] as Label;

            if (progressBar == null || lblStatus == null)
                return;

            progressValue += 2;

            if (progressValue <= 100)
            {
                progressBar.Value = progressValue;

                switch (progressValue)
                {
                    case 10:
                        lblStatus.Text = "Initializing system...";
                        break;
                    case 30:
                        lblStatus.Text = "Loading modules...";
                        break;
                    case 50:
                        lblStatus.Text = "Connecting to database...";
                        break;
                    case 70:
                        lblStatus.Text = "Loading configurations...";
                        break;
                    case 90:
                        lblStatus.Text = "Preparing interface...";
                        break;
                }
            }

            if (progressValue >= 100)
            {
                timer.Stop();
                lblStatus.Text = "Starting application...";
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}