using System;
using System.Drawing;
using System.Windows.Forms;
using FAST_TAXIS3.Helpers;

namespace FAST_TAXIS3.Forms
{
    public partial class UserManualForm : Form
    {
        private TabControl tabControl;
        private WebBrowser webBrowser;
        private Panel pnlHeader;
        private Button btnPrint;
        private Button btnClose;

        public UserManualForm()
        {
            InitializeComponent();
            SetupForm();
            LoadManualContent();
        }

        private void SetupForm()
        {
            this.Text = "Fast Taxis - User Manual";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 245);
            this.MinimumSize = new Size(900, 600);

            // Header Panel
            pnlHeader = new Panel();
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 80;
            pnlHeader.BackColor = Color.FromArgb(45, 45, 48);
            this.Controls.Add(pnlHeader);

            // Title Label
            Label lblTitle = new Label();
            lblTitle.Text = "FAST TAXIS - USER MANUAL";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblTitle.Size = new Size(500, 50);
            lblTitle.Location = new Point(30, 15);
            pnlHeader.Controls.Add(lblTitle);

            // Version Label
            Label lblVersion = new Label();
            lblVersion.Text = "Version 1.0.0";
            lblVersion.Font = new Font("Segoe UI", 10);
            lblVersion.ForeColor = Color.LightGray;
            lblVersion.Size = new Size(100, 25);
            lblVersion.Location = new Point(850, 45);
            pnlHeader.Controls.Add(lblVersion);

            // Button Panel
            Panel pnlButtons = new Panel();
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Height = 60;
            pnlButtons.BackColor = Color.FromArgb(52, 58, 64);
            this.Controls.Add(pnlButtons);

            // Print Button
            btnPrint = new Button();
            btnPrint.Text = "PRINT MANUAL";
            btnPrint.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnPrint.BackColor = Color.FromArgb(0, 123, 255);
            btnPrint.ForeColor = Color.White;
            btnPrint.Size = new Size(150, 40);
            btnPrint.Location = new Point(700, 10);
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.Click += BtnPrint_Click;
            pnlButtons.Controls.Add(btnPrint);

            // Close Button
            btnClose = new Button();
            btnClose.Text = "CLOSE";
            btnClose.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnClose.BackColor = Color.FromArgb(108, 117, 125);
            btnClose.ForeColor = Color.White;
            btnClose.Size = new Size(150, 40);
            btnClose.Location = new Point(860, 10);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();
            pnlButtons.Controls.Add(btnClose);

            // Tab Control
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 11);
            tabControl.Padding = new Point(15, 10);
            this.Controls.Add(tabControl);
        }

        private void LoadManualContent()
        {
            // Tab 1: Introduction
            TabPage tabIntro = new TabPage("INTRODUCTION");
            tabIntro.Controls.Add(CreateIntroductionPanel());
            tabControl.TabPages.Add(tabIntro);

            // Tab 2: Getting Started
            TabPage tabGettingStarted = new TabPage("GETTING STARTED");
            tabGettingStarted.Controls.Add(CreateGettingStartedPanel());
            tabControl.TabPages.Add(tabGettingStarted);

            // Tab 3: Modules
            TabPage tabModules = new TabPage("MODULES");
            tabModules.Controls.Add(CreateModulesPanel());
            tabControl.TabPages.Add(tabModules);

            // Tab 4: Reports
            TabPage tabReports = new TabPage("REPORTS");
            tabReports.Controls.Add(CreateReportsPanel());
            tabControl.TabPages.Add(tabReports);

            // Tab 5: Troubleshooting
            TabPage tabTroubleshooting = new TabPage("TROUBLESHOOTING");
            tabTroubleshooting.Controls.Add(CreateTroubleshootingPanel());
            tabControl.TabPages.Add(tabTroubleshooting);

            // Tab 6: About
            TabPage tabAbout = new TabPage("ABOUT");
            tabAbout.Controls.Add(CreateAboutPanel());
            tabControl.TabPages.Add(tabAbout);
        }

        private Panel CreateIntroductionPanel()
        {
            Panel panel = new Panel();
            panel.AutoScroll = true;
            panel.BackColor = Color.White;
            panel.Dock = DockStyle.Fill;
            panel.Padding = new Padding(30);

            int yPos = 20;

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = "Welcome to Fast Taxis Database System";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblTitle.Size = new Size(800, 40);
            lblTitle.Location = new Point(20, yPos);
            panel.Controls.Add(lblTitle);
            yPos += 50;

            // Overview
            Label lblOverview = new Label();
            lblOverview.Text = "OVERVIEW";
            lblOverview.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblOverview.ForeColor = Color.FromArgb(52, 58, 64);
            lblOverview.Size = new Size(800, 30);
            lblOverview.Location = new Point(20, yPos);
            panel.Controls.Add(lblOverview);
            yPos += 35;

            Label lblOverviewText = new Label();
            lblOverviewText.Text = "Fast Taxis Database System is a comprehensive management solution " +
                                 "designed specifically for Fast Taxis company. This application helps " +
                                 "streamline operations, manage resources, and provide valuable insights " +
                                 "through detailed reporting.\n\n" +
                                 "The system was developed to address the growing administrative challenges " +
                                 "faced by Fast Taxis, including paperwork overload, poor information sharing, " +
                                 "and operational inefficiencies.";
            lblOverviewText.Font = new Font("Segoe UI", 11);
            lblOverviewText.ForeColor = Color.Black;
            lblOverviewText.Size = new Size(900, 150);
            lblOverviewText.Location = new Point(30, yPos);
            lblOverviewText.TextAlign = ContentAlignment.TopLeft;
            panel.Controls.Add(lblOverviewText);
            yPos += 170;

            // Key Features
            Label lblFeatures = new Label();
            lblFeatures.Text = "KEY FEATURES";
            lblFeatures.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblFeatures.ForeColor = Color.FromArgb(52, 58, 64);
            lblFeatures.Size = new Size(800, 30);
            lblFeatures.Location = new Point(20, yPos);
            panel.Controls.Add(lblFeatures);
            yPos += 35;

            string[] features = {
                "• Office Management - Manage multiple office locations",
                "• Staff Management - Handle employees, managers, drivers, and admin staff",
                "• Owner & Taxi Management - Track vehicle owners and fleet",
                "• Client Management - Support for private and business clients",
                "• Contract Management - Handle business contracts",
                "• Job Management - Track all taxi bookings and trips",
                "• Comprehensive Reporting - 20+ predefined reports",
                "• Advanced Analytics - Nested queries and performance metrics"
            };

            foreach (string feature in features)
            {
                Label lblFeature = new Label();
                lblFeature.Text = feature;
                lblFeature.Font = new Font("Segoe UI", 11);
                lblFeature.ForeColor = Color.Black;
                lblFeature.Size = new Size(900, 25);
                lblFeature.Location = new Point(30, yPos);
                panel.Controls.Add(lblFeature);
                yPos += 30;
            }

            return panel;
        }

        private Panel CreateGettingStartedPanel()
        {
            Panel panel = new Panel();
            panel.AutoScroll = true;
            panel.BackColor = Color.White;
            panel.Dock = DockStyle.Fill;
            panel.Padding = new Padding(30);

            int yPos = 20;

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = "Getting Started";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblTitle.Size = new Size(800, 40);
            lblTitle.Location = new Point(20, yPos);
            panel.Controls.Add(lblTitle);
            yPos += 50;

            // System Requirements
            Label lblRequirements = new Label();
            lblRequirements.Text = "SYSTEM REQUIREMENTS";
            lblRequirements.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblRequirements.ForeColor = Color.FromArgb(52, 58, 64);
            lblRequirements.Size = new Size(800, 30);
            lblRequirements.Location = new Point(20, yPos);
            panel.Controls.Add(lblRequirements);
            yPos += 35;

            string[] requirements = {
                "• Windows 10 or Windows 11",
                "• .NET Framework 4.7.2 or higher",
                "• SQL Server 2016 or higher",
                "• 4GB RAM minimum (8GB recommended)",
                "• 100MB free disk space"
            };

            foreach (string req in requirements)
            {
                Label lblReq = new Label();
                lblReq.Text = req;
                lblReq.Font = new Font("Segoe UI", 11);
                lblReq.ForeColor = Color.Black;
                lblReq.Size = new Size(900, 25);
                lblReq.Location = new Point(30, yPos);
                panel.Controls.Add(lblReq);
                yPos += 30;
            }
            yPos += 20;

            // Login Instructions
            Label lblLogin = new Label();
            lblLogin.Text = "LOGIN INSTRUCTIONS";
            lblLogin.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblLogin.ForeColor = Color.FromArgb(52, 58, 64);
            lblLogin.Size = new Size(800, 30);
            lblLogin.Location = new Point(20, yPos);
            panel.Controls.Add(lblLogin);
            yPos += 35;

            string[] loginSteps = {
                "1. Launch the Fast Taxis application",
                "2. Enter your Staff ID",
                "3. Enter your password",
                "4. Click 'LOGIN' button",
                "5. The system will verify your credentials and redirect to Main Dashboard"
            };

            foreach (string step in loginSteps)
            {
                Label lblStep = new Label();
                lblStep.Text = step;
                lblStep.Font = new Font("Segoe UI", 11);
                lblStep.ForeColor = Color.Black;
                lblStep.Size = new Size(900, 25);
                lblStep.Location = new Point(30, yPos);
                panel.Controls.Add(lblStep);
                yPos += 30;
            }

            return panel;
        }

        private Panel CreateModulesPanel()
        {
            Panel panel = new Panel();
            panel.AutoScroll = true;
            panel.BackColor = Color.White;
            panel.Dock = DockStyle.Fill;
            panel.Padding = new Padding(30);

            int yPos = 20;

            Label lblTitle = new Label();
            lblTitle.Text = "System Modules";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblTitle.Size = new Size(800, 40);
            lblTitle.Location = new Point(20, yPos);
            panel.Controls.Add(lblTitle);
            yPos += 50;

            // Module descriptions
            string[,] modules = {
                { "OFFICE MANAGEMENT", "Manage company offices, locations, and contact information. View managers per office and staff distribution." },
                { "STAFF MANAGEMENT", "Handle all employees including managers, administrative staff, and drivers. Assign roles and manage driver licenses." },
                { "OWNER MANAGEMENT", "Register vehicle owners, track ownership details, and identify owners with multiple vehicles." },
                { "TAXI MANAGEMENT", "Maintain fleet information, assign taxis to offices, track service schedules, and monitor usage." },
                { "ALLOCATION", "Assign drivers to taxis, manage allocations, and track driver-vehicle relationships." },
                { "CLIENT MANAGEMENT", "Manage private and business clients, store contact details, and track client history." },
                { "CONTRACT MANAGEMENT", "Handle business contracts, track agreed jobs, and monitor contract fulfillment." },
                { "JOB MANAGEMENT", "Core module for booking management, track pickups/dropoffs, record mileage and charges, handle job status." },
                { "REPORTS DASHBOARD", "Comprehensive reporting system with 20+ predefined reports and advanced analytics." }
            };

            for (int i = 0; i < modules.GetLength(0); i++)
            {
                Label lblModule = new Label();
                lblModule.Text = modules[i, 0];
                lblModule.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                lblModule.ForeColor = Color.FromArgb(0, 123, 255);
                lblModule.Size = new Size(900, 25);
                lblModule.Location = new Point(30, yPos);
                panel.Controls.Add(lblModule);
                yPos += 30;

                Label lblDesc = new Label();
                lblDesc.Text = modules[i, 1];
                lblDesc.Font = new Font("Segoe UI", 11);
                lblDesc.ForeColor = Color.Black;
                lblDesc.Size = new Size(900, 40);
                lblDesc.Location = new Point(50, yPos);
                panel.Controls.Add(lblDesc);
                yPos += 50;
            }

            return panel;
        }

        private Panel CreateReportsPanel()
        {
            Panel panel = new Panel();
            panel.AutoScroll = true;
            panel.BackColor = Color.White;
            panel.Dock = DockStyle.Fill;
            panel.Padding = new Padding(30);

            int yPos = 20;

            Label lblTitle = new Label();
            lblTitle.Text = "Reports & Analytics";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblTitle.Size = new Size(800, 40);
            lblTitle.Location = new Point(20, yPos);
            panel.Controls.Add(lblTitle);
            yPos += 50;

            Label lblSubtitle = new Label();
            lblSubtitle.Text = "AVAILABLE REPORTS";
            lblSubtitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblSubtitle.ForeColor = Color.FromArgb(52, 58, 64);
            lblSubtitle.Size = new Size(800, 30);
            lblSubtitle.Location = new Point(20, yPos);
            panel.Controls.Add(lblSubtitle);
            yPos += 40;

            string[] reports = {
                "• Managers at each office",
                "• Female drivers in Cyberjaya",
                "• Staff count per office",
                "• Taxi details by office",
                "• Total registered vehicles",
                "• Driver allocation per taxi",
                "• Owners with multiple vehicles",
                "• Business client addresses",
                "• Active contracts by location",
                "• Private client statistics",
                "• Driver job history",
                "• Driver age analysis",
                "• Client hiring patterns",
                "• Revenue and fee analysis",
                "• Vehicle and driver performance",
                "• Contract fulfillment tracking",
                "• Maintenance scheduling"
            };

            foreach (string report in reports)
            {
                Label lblReport = new Label();
                lblReport.Text = report;
                lblReport.Font = new Font("Segoe UI", 11);
                lblReport.ForeColor = Color.Black;
                lblReport.Size = new Size(900, 25);
                lblReport.Location = new Point(30, yPos);
                panel.Controls.Add(lblReport);
                yPos += 30;
            }

            yPos += 20;

            Label lblNested = new Label();
            lblNested.Text = "ADVANCED ANALYTICS (NESTED QUERIES)";
            lblNested.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblNested.ForeColor = Color.FromArgb(220, 53, 69);
            lblNested.Size = new Size(900, 30);
            lblNested.Location = new Point(20, yPos);
            panel.Controls.Add(lblNested);
            yPos += 35;

            string[] nested = {
                "• Drivers performing above average",
                "• Taxis with no usage history",
                "• High-value clients",
                "• Offices with above-average staffing",
                "• Owners with above-average revenue"
            };

            foreach (string n in nested)
            {
                Label lblN = new Label();
                lblN.Text = n;
                lblN.Font = new Font("Segoe UI", 11);
                lblN.ForeColor = Color.Black;
                lblN.Size = new Size(900, 25);
                lblN.Location = new Point(30, yPos);
                panel.Controls.Add(lblN);
                yPos += 30;
            }

            return panel;
        }

        private Panel CreateTroubleshootingPanel()
        {
            Panel panel = new Panel();
            panel.AutoScroll = true;
            panel.BackColor = Color.White;
            panel.Dock = DockStyle.Fill;
            panel.Padding = new Padding(30);

            int yPos = 20;

            Label lblTitle = new Label();
            lblTitle.Text = "Troubleshooting";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblTitle.Size = new Size(800, 40);
            lblTitle.Location = new Point(20, yPos);
            panel.Controls.Add(lblTitle);
            yPos += 50;

            // Common Issues
            string[,] issues = {
                { "Cannot connect to database", "• Verify SQL Server is running\n• Check connection string in App.config\n• Ensure firewall allows SQL Server port\n• Confirm login credentials" },
                { "Login failed", "• Verify Staff ID is correct\n• Check password\n• Ensure account is active\n• Contact system administrator" },
                { "Cannot save record", "• Check all required fields are filled\n• Verify data format (phone, email, etc.)\n• Ensure no duplicate records\n• Check database permissions" },
                { "Report shows no data", "• Verify filters are correct\n• Ensure data exists for selected criteria\n• Check date ranges\n• Refresh the report" },
                { "Application crashes", "• Check Windows updates\n• Verify .NET Framework version\n• Clear application cache\n• Reinstall application" }
            };

            for (int i = 0; i < issues.GetLength(0); i++)
            {
                Label lblIssue = new Label();
                lblIssue.Text = issues[i, 0];
                lblIssue.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                lblIssue.ForeColor = Color.FromArgb(220, 53, 69);
                lblIssue.Size = new Size(900, 25);
                lblIssue.Location = new Point(20, yPos);
                panel.Controls.Add(lblIssue);
                yPos += 30;

                Label lblSolution = new Label();
                lblSolution.Text = issues[i, 1];
                lblSolution.Font = new Font("Segoe UI", 11);
                lblSolution.ForeColor = Color.Black;
                lblSolution.Size = new Size(900, 80);
                lblSolution.Location = new Point(40, yPos);
                panel.Controls.Add(lblSolution);
                yPos += 90;
            }

            return panel;
        }

        private Panel CreateAboutPanel()
        {
            Panel panel = new Panel();
            panel.AutoScroll = true;
            panel.BackColor = Color.White;
            panel.Dock = DockStyle.Fill;
            panel.Padding = new Padding(30);

            int yPos = 20;

            Label lblTitle = new Label();
            lblTitle.Text = "About Fast Taxis";
            lblTitle.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblTitle.Size = new Size(800, 50);
            lblTitle.Location = new Point(20, yPos);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(lblTitle);
            yPos += 70;

            Label lblSubtitle = new Label();
            lblSubtitle.Text = "Database Management System";
            lblSubtitle.Font = new Font("Segoe UI", 16);
            lblSubtitle.ForeColor = Color.FromArgb(52, 58, 64);
            lblSubtitle.Size = new Size(800, 30);
            lblSubtitle.Location = new Point(20, yPos);
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(lblSubtitle);
            yPos += 60;

            Label lblVersion = new Label();
            lblVersion.Text = "Version 1.0.0";
            lblVersion.Font = new Font("Segoe UI", 14);
            lblVersion.ForeColor = Color.FromArgb(108, 117, 125);
            lblVersion.Size = new Size(800, 30);
            lblVersion.Location = new Point(20, yPos);
            lblVersion.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(lblVersion);
            yPos += 50;

            Label lblCopyright = new Label();
            lblCopyright.Text = "© 2026 Fast Taxis Inc. All rights reserved.";
            lblCopyright.Font = new Font("Segoe UI", 12);
            lblCopyright.ForeColor = Color.FromArgb(108, 117, 125);
            lblCopyright.Size = new Size(800, 25);
            lblCopyright.Location = new Point(20, yPos);
            lblCopyright.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(lblCopyright);
            yPos += 50;

            Label lblDeveloped = new Label();
            lblDeveloped.Text = "Developed for Fast Taxis Corporation";
            lblDeveloped.Font = new Font("Segoe UI", 12);
            lblDeveloped.ForeColor = Color.Black;
            lblDeveloped.Size = new Size(800, 25);
            lblDeveloped.Location = new Point(20, yPos);
            lblDeveloped.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(lblDeveloped);
            yPos += 80;

            Label lblContact = new Label();
            lblContact.Text = "For support and inquiries:";
            lblContact.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblContact.ForeColor = Color.Black;
            lblContact.Size = new Size(800, 25);
            lblContact.Location = new Point(20, yPos);
            lblContact.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(lblContact);
            yPos += 30;

            Label lblEmail = new Label();
            lblEmail.Text = "support@fasttaxis.com";
            lblEmail.Font = new Font("Segoe UI", 12);
            lblEmail.ForeColor = Color.FromArgb(0, 123, 255);
            lblEmail.Size = new Size(800, 25);
            lblEmail.Location = new Point(20, yPos);
            lblEmail.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(lblEmail);
            yPos += 30;

            Label lblPhone = new Label();
            lblPhone.Text = "03-1234 5678";
            lblPhone.Font = new Font("Segoe UI", 12);
            lblPhone.ForeColor = Color.FromArgb(0, 123, 255);
            lblPhone.Size = new Size(800, 25);
            lblPhone.Location = new Point(20, yPos);
            lblPhone.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(lblPhone);
            yPos += 80;

            Label lblDirector = new Label();
            lblDirector.Text = "Project Director: Nani Johar";
            lblDirector.Font = new Font("Segoe UI", 11);
            lblDirector.ForeColor = Color.FromArgb(108, 117, 125);
            lblDirector.Size = new Size(800, 25);
            lblDirector.Location = new Point(20, yPos);
            lblDirector.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(lblDirector);
            yPos += 30;

            Label lblDate = new Label();
            lblDate.Text = "Last Updated: February 2026";
            lblDate.Font = new Font("Segoe UI", 11);
            lblDate.ForeColor = Color.FromArgb(108, 117, 125);
            lblDate.Size = new Size(800, 25);
            lblDate.Location = new Point(20, yPos);
            lblDate.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(lblDate);

            return panel;
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Print functionality will send the user manual to the default printer.",
                "Print Manual", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            this.Dispose();
        }
    }
}