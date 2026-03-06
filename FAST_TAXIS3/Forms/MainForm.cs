using FAST_TAXIS3.Data;
using FAST_TAXIS3.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FAST_TAXIS3.Forms
{
    public partial class MainForm : Form
    {
        private Panel sidePanel;
        private Panel headerPanel;
        private Panel contentPanel;
        private Button btnDashboard;
        private Button btnOffices;
        private Button btnStaff;
        private Button btnOwners;
        private Button btnTaxis;
        private Button btnAllocation;
        private Button btnClients;
        private Button btnContracts;
        private Button btnJobs;
        private Button btnReports;
        private Button btnLogout;
        private Label lblWelcome;
        private Label lblDateTime;

        public MainForm()
        {
            InitializeComponent();
            SetupForm();
            SetupSideMenu();
            SetupHeader();
            SetupTimer();
            LoadDashboard();
        }

        private void SetupForm()
        {
            this.Text = "Fast Taxis - Main Dashboard";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 245);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(1200, 700);
        }

        private void SetupHeader()
        {
            // Header Panel
            headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 60;
            headerPanel.BackColor = Color.FromArgb(45, 45, 48);
            this.Controls.Add(headerPanel);

            // Logo Label
            Label lblLogo = new Label();
            lblLogo.Text = "FAST TAXIS";
            lblLogo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(255, 128, 0);
            lblLogo.Size = new Size(200, 40);
            lblLogo.Location = new Point(20, 10);
            headerPanel.Controls.Add(lblLogo);

            // Welcome Label
            lblWelcome = new Label();
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Font = new Font("Segoe UI", 11);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Size = new Size(400, 30);
            lblWelcome.Location = new Point(250, 15);
            lblWelcome.Text = "Welcome, " + SessionManager.CurrentStaffName;
            headerPanel.Controls.Add(lblWelcome);

            // DateTime Label
            lblDateTime = new Label();
            lblDateTime.Name = "lblDateTime";
            lblDateTime.Font = new Font("Segoe UI", 10);
            lblDateTime.ForeColor = Color.LightGray;
            lblDateTime.Size = new Size(250, 25);
            lblDateTime.Location = new Point(headerPanel.Width - 270, 18);
            lblDateTime.TextAlign = ContentAlignment.MiddleRight;
            lblDateTime.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            headerPanel.Controls.Add(lblDateTime);
        }

        private void SetupSideMenu()
        {
            // Side Panel
            sidePanel = new Panel();
            sidePanel.Dock = DockStyle.Left;
            sidePanel.Width = 250;
            sidePanel.BackColor = Color.FromArgb(50, 50, 55);
            this.Controls.Add(sidePanel);

            int buttonY = 30;
            int buttonHeight = 45;
            int spacing = 5;

            // Dashboard Button
            btnDashboard = CreateMenuButton("📊  Dashboard", buttonY);
            btnDashboard.Click += (s, e) => LoadDashboard();
            sidePanel.Controls.Add(btnDashboard);
            buttonY += buttonHeight + spacing;

            // Offices Button
            btnOffices = CreateMenuButton("🏢  Offices", buttonY);
            btnOffices.Click += (s, e) => OpenForm(new OfficeForm());
            sidePanel.Controls.Add(btnOffices);
            buttonY += buttonHeight + spacing;

            // Staff Button
            btnStaff = CreateMenuButton("👥  Staff", buttonY);
            btnStaff.Click += (s, e) => OpenForm(new StaffForm());
            sidePanel.Controls.Add(btnStaff);
            buttonY += buttonHeight + spacing;

            // Owners Button
            btnOwners = CreateMenuButton("👤  Owners", buttonY);
            btnOwners.Click += (s, e) => OpenForm(new OwnerForm());
            sidePanel.Controls.Add(btnOwners);
            buttonY += buttonHeight + spacing;

            // Taxis Button
            btnTaxis = CreateMenuButton("🚖  Taxis", buttonY);
            btnTaxis.Click += (s, e) => OpenForm(new TaxiForm());
            sidePanel.Controls.Add(btnTaxis);
            buttonY += buttonHeight + spacing;

            // Allocation Button
            btnAllocation = CreateMenuButton("🔗  Allocation", buttonY);
            btnAllocation.Click += (s, e) => OpenForm(new AllocationForm());
            sidePanel.Controls.Add(btnAllocation);
            buttonY += buttonHeight + spacing;

            // Clients Button
            btnClients = CreateMenuButton("🧑‍🤝‍🧑  Clients", buttonY);
            btnClients.Click += (s, e) => OpenForm(new ClientForm());
            sidePanel.Controls.Add(btnClients);
            buttonY += buttonHeight + spacing;

            // Contracts Button
            btnContracts = CreateMenuButton("📄  Contracts", buttonY);
            btnContracts.Click += (s, e) => OpenForm(new ContractForm());
            sidePanel.Controls.Add(btnContracts);
            buttonY += buttonHeight + spacing;

            // Jobs Button
            btnJobs = CreateMenuButton("📦  Jobs", buttonY);
            btnJobs.Click += (s, e) => OpenForm(new JobForm());
            sidePanel.Controls.Add(btnJobs);
            buttonY += buttonHeight + spacing;

            // Reports Button
            btnReports = CreateMenuButton("📊  Reports", buttonY);
            btnReports.Click += (s, e) => OpenForm(new ReportsForm());
            sidePanel.Controls.Add(btnReports);
            buttonY += buttonHeight + spacing;

            // Separator Line
            Panel separator = new Panel();
            separator.Height = 1;
            separator.Width = sidePanel.Width - 40;
            separator.BackColor = Color.Gray;
            separator.Location = new Point(20, buttonY + 10);
            sidePanel.Controls.Add(separator);
            buttonY += 30;

            // Logout Button
            btnLogout = CreateMenuButton("🚪  Logout", buttonY);
            btnLogout.BackColor = Color.FromArgb(255, 128, 0);
            btnLogout.Click += BtnLogout_Click;
            sidePanel.Controls.Add(btnLogout);

            // User Info Panel
            Panel userPanel = new Panel();
            userPanel.Dock = DockStyle.Bottom;
            userPanel.Height = 80;
            userPanel.BackColor = Color.FromArgb(40, 40, 45);
            sidePanel.Controls.Add(userPanel);

            Label lblUser = new Label();
            lblUser.Text = SessionManager.GetCurrentUserDisplay();
            lblUser.Font = new Font("Segoe UI", 10);
            lblUser.ForeColor = Color.LightGray;
            lblUser.Size = new Size(sidePanel.Width - 20, 40);
            lblUser.Location = new Point(10, 20);
            lblUser.TextAlign = ContentAlignment.MiddleCenter;
            userPanel.Controls.Add(lblUser);
        }

        private Button CreateMenuButton(string text, int yLocation)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            btn.ForeColor = Color.White;
            btn.BackColor = Color.FromArgb(60, 60, 65);
            btn.Size = new Size(sidePanel.Width - 30, 40);
            btn.Location = new Point(15, yLocation);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);

            // Hover effect
            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(80, 80, 85);
            btn.MouseLeave += (s, e) => btn.BackColor = Color.FromArgb(60, 60, 65);

            return btn;
        }

        private void SetupTimer()
        {
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void LoadDashboard()
        {
            // Remove old content
            if (contentPanel != null)
                this.Controls.Remove(contentPanel);

            // Create new Panel
            contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.FromArgb(240, 240, 245);
            contentPanel.AutoScroll = true;
            this.Controls.Add(contentPanel);
            contentPanel.BringToFront();

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = "📊 Fast Taxis - Dashboard";
            lblTitle.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(45, 45, 48);
            lblTitle.Size = new Size(500, 50);
            lblTitle.Location = new Point(30, 20);
            contentPanel.Controls.Add(lblTitle);

            // Welcome Message
            Label lblWelcome = new Label();
            lblWelcome.Text = "Welcome to Fast Taxis Management System";
            lblWelcome.Font = new Font("Segoe UI", 14);
            lblWelcome.ForeColor = Color.Gray;
            lblWelcome.Size = new Size(500, 30);
            lblWelcome.Location = new Point(30, 70);
            contentPanel.Controls.Add(lblWelcome);

            // Separator Line
            Label lblLine = new Label();
            lblLine.Text = "────────────────────────────────────";
            lblLine.Font = new Font("Segoe UI", 12);
            lblLine.ForeColor = Color.LightGray;
            lblLine.Size = new Size(800, 20);
            lblLine.Location = new Point(30, 110);
            contentPanel.Controls.Add(lblLine);

            // === Statistics (Public Info - No Actions) ===
            try
            {
                DataTable dt = ReportData.GetDashboardSummary();
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];

                    // Row 1 - Company Stats
                    AddStatCard("🏢 Offices", r["TotalOffices"].ToString(), 30, 150, Color.FromArgb(52, 73, 94));
                    AddStatCard("👥 Staff", r["TotalStaff"].ToString(), 260, 150, Color.FromArgb(52, 73, 94));
                    AddStatCard("🚖 Drivers", r["TotalDrivers"].ToString(), 490, 150, Color.FromArgb(52, 73, 94));
                    AddStatCard("🚗 Taxis", r["TotalTaxis"].ToString(), 720, 150, Color.FromArgb(52, 73, 94));

                    // Row 2 - Clients & Contracts
                    AddStatCard("🧑 Clients", r["TotalClients"].ToString(), 30, 230, Color.FromArgb(39, 174, 96));
                    AddStatCard("📄 Active Contracts", r["ActiveContracts"].ToString(), 260, 230, Color.FromArgb(39, 174, 96));
                    AddStatCard("📅 Today's Jobs", r["TodayJobs"].ToString(), 490, 230, Color.FromArgb(39, 174, 96));

                    // Row 3 - Revenue
                    Label lblRevenueTitle = new Label();
                    lblRevenueTitle.Text = "💰 Total Revenue";
                    lblRevenueTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                    lblRevenueTitle.ForeColor = Color.FromArgb(45, 45, 48);
                    lblRevenueTitle.Size = new Size(250, 30);
                    lblRevenueTitle.Location = new Point(30, 320);
                    contentPanel.Controls.Add(lblRevenueTitle);

                    Label lblRevenueValue = new Label();
                    lblRevenueValue.Text = $"RM {Convert.ToDecimal(r["TotalRevenue"]):N2}";
                    lblRevenueValue.Font = new Font("Segoe UI", 24, FontStyle.Bold);
                    lblRevenueValue.ForeColor = Color.Green;
                    lblRevenueValue.Size = new Size(300, 50);
                    lblRevenueValue.Location = new Point(30, 350);
                    contentPanel.Controls.Add(lblRevenueValue);

                    // Average Job Fee
                    Label lblAvgTitle = new Label();
                    lblAvgTitle.Text = "📊 Average Job Fee";
                    lblAvgTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                    lblAvgTitle.ForeColor = Color.FromArgb(45, 45, 48);
                    lblAvgTitle.Size = new Size(250, 30);
                    lblAvgTitle.Location = new Point(450, 320);
                    contentPanel.Controls.Add(lblAvgTitle);

                    Label lblAvgValue = new Label();
                    lblAvgValue.Text = $"RM {Convert.ToDecimal(r["AverageJobFee"]):N2}";
                    lblAvgValue.Font = new Font("Segoe UI", 20, FontStyle.Bold);
                    lblAvgValue.ForeColor = Color.FromArgb(255, 128, 0);
                    lblAvgValue.Size = new Size(250, 40);
                    lblAvgValue.Location = new Point(450, 355);
                    contentPanel.Controls.Add(lblAvgValue);
                }
            }
            catch (Exception ex)
            {
                Label lblError = new Label();
                lblError.Text = "⚠️ Sorry, unable to load statistics at this time";
                lblError.ForeColor = Color.Red;
                lblError.Font = new Font("Segoe UI", 12);
                lblError.Location = new Point(30, 150);
                lblError.Size = new Size(500, 30);
                contentPanel.Controls.Add(lblError);
            }

            // Footer
            Label lblFooter = new Label();
            lblFooter.Text = "Fast Taxis © 2026 - Integrated Management System";
            lblFooter.Font = new Font("Segoe UI", 10);
            lblFooter.ForeColor = Color.Gray;
            lblFooter.Size = new Size(500, 30);
            lblFooter.Location = new Point(30, 550);
            contentPanel.Controls.Add(lblFooter);
        }

        // Helper function to create stat card
        private void AddStatCard(string title, string value, int x, int y, Color color)
        {
            // Card background
            Panel card = new Panel();
            card.Size = new Size(200, 70);
            card.Location = new Point(x, y);
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.None;
            contentPanel.Controls.Add(card);

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 10);
            lblTitle.ForeColor = Color.Gray;
            lblTitle.Size = new Size(180, 20);
            lblTitle.Location = new Point(10, 10);
            card.Controls.Add(lblTitle);

            // Value
            Label lblValue = new Label();
            lblValue.Text = value;
            lblValue.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblValue.ForeColor = color;
            lblValue.Size = new Size(180, 30);
            lblValue.Location = new Point(10, 30);
            card.Controls.Add(lblValue);
        }

       private void AddStatLabel(string prefix, string value, int x, int y, int index)
        {
            Label lbl = new Label();
            lbl.Text = $"{prefix} {value}";
            lbl.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lbl.ForeColor = Color.Black;
            lbl.Size = new Size(180, 30);
            lbl.Location = new Point(x, y);
            contentPanel.Controls.Add(lbl);
        }
        private void OpenForm(Form form)
        {
            // ✅ منع الوصول حسب الصلاحية
            if (form is OfficeForm && !SessionManager.HasPermission("CanViewOffices"))
            {
                MessageBox.Show("You don't have permission to access Offices.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (form is StaffForm && !SessionManager.HasPermission("CanViewStaff"))
            {
                MessageBox.Show("You don't have permission to access Staff.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (form is OwnerForm && !SessionManager.HasPermission("CanViewOwners"))
            {
                MessageBox.Show("You don't have permission to access Owners.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (form is TaxiForm && !SessionManager.HasPermission("CanViewTaxis"))
            {
                MessageBox.Show("You don't have permission to access Taxis.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (form is ClientForm && !SessionManager.HasPermission("CanViewClients"))
            {
                MessageBox.Show("You don't have permission to access Clients.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (form is ContractForm && !SessionManager.HasPermission("CanViewContracts"))
            {
                MessageBox.Show("You don't have permission to access Contracts.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (form is JobForm && !SessionManager.HasPermission("CanViewJobs"))
            {
                MessageBox.Show("You don't have permission to access Jobs.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (form is ReportsForm && !SessionManager.HasPermission("CanViewReports"))
            {
                MessageBox.Show("You don't have permission to access Reports.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            form.ShowDialog();
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?",
                "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // ✅ تسجيل خروج من الجلسة
                SessionManager.Logout();

                // ✅ إخفاء الـ MainForm الحالي
                this.Hide();

                // ✅ فتح LoginForm جديد
                LoginForm loginForm = new LoginForm();
                loginForm.ShowDialog();

                // ✅ بعد ما يرجع من LoginForm، قفل التطبيق إذا لغى أو سجل دخول غلط
                if (loginForm.DialogResult != DialogResult.OK)
                {
                    Application.Exit();
                }
                else
                {
                    // ✅ إذا سجل دخول ناجح، يرجع يظهر MainForm
                    this.Show();
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (SessionManager.IsLoggedIn)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to exit?",
                    "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}