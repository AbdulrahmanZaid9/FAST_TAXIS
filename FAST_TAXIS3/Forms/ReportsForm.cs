using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FAST_TAXIS3.Data;
using FAST_TAXIS3.Helpers;

namespace FAST_TAXIS3.Forms
{
    public partial class ReportsForm : Form
    {
        private DataGridView dgvReport;
        private ComboBox cmbReportType;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private ComboBox cmbDriver;
        private ComboBox cmbTaxi;
        private ComboBox cmbContract;
        private Button btnGenerate;
        private Button btnExport;
        private Button btnPrint;
        private Label lblTitle;
        private Panel pnlFilters;

        public ReportsForm()
        {
            InitializeComponent();
            SetupForm();
            LoadComboBoxes();
        }

        private void SetupForm()
        {
            this.Text = "Fast Taxis - Reports Dashboard";
            this.Size = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 245);
            this.MinimumSize = new Size(1200, 700);

            // Title Label
            lblTitle = new Label();
            lblTitle.Text = "REPORTS DASHBOARD";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(50, 50, 55);
            lblTitle.Size = new Size(400, 50);
            lblTitle.Location = new Point(30, 20);
            this.Controls.Add(lblTitle);

            // Filters Panel
            pnlFilters = new Panel();
            pnlFilters.Size = new Size(350, 600);
            pnlFilters.Location = new Point(30, 80);
            pnlFilters.BackColor = Color.White;
            pnlFilters.BorderStyle = BorderStyle.None;
            this.Controls.Add(pnlFilters);

            // Report Type Section
            Label lblReportType = new Label();
            lblReportType.Text = "SELECT REPORT";
            lblReportType.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblReportType.ForeColor = Color.FromArgb(255, 128, 0);
            lblReportType.Size = new Size(300, 30);
            lblReportType.Location = new Point(20, 20);
            pnlFilters.Controls.Add(lblReportType);

            cmbReportType = new ComboBox();
            cmbReportType.Name = "cmbReportType";
            cmbReportType.Font = new Font("Segoe UI", 11);
            cmbReportType.Size = new Size(300, 25);
            cmbReportType.Location = new Point(20, 60);
            cmbReportType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReportType.SelectedIndexChanged += CmbReportType_SelectedIndexChanged;
            pnlFilters.Controls.Add(cmbReportType);

            // Parameters Panel
            Panel pnlParams = new Panel();
            pnlParams.Name = "pnlParams";
            pnlParams.Size = new Size(300, 400);
            pnlParams.Location = new Point(20, 100);
            pnlParams.BackColor = Color.FromArgb(250, 250, 252);
            pnlFilters.Controls.Add(pnlParams);

            // Generate Button
            btnGenerate = new Button();
            btnGenerate.Text = "GENERATE REPORT";
            btnGenerate.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnGenerate.BackColor = Color.FromArgb(0, 123, 255);
            btnGenerate.ForeColor = Color.White;
            btnGenerate.Size = new Size(300, 45);
            btnGenerate.Location = new Point(20, 500);  // ✅ أول زر
            btnGenerate.FlatStyle = FlatStyle.Flat;
            btnGenerate.FlatAppearance.BorderSize = 0;
            btnGenerate.Click += BtnGenerate_Click;
            pnlFilters.Controls.Add(btnGenerate);

            // Export Button
            btnExport = new Button();
            btnExport.Text = "EXPORT TO EXCEL";
            btnExport.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnExport.BackColor = Color.FromArgb(40, 167, 69);
            btnExport.ForeColor = Color.White;
            btnExport.Size = new Size(145, 40);
            btnExport.Location = new Point(20, 560);  // ✅ تحت Generate (بعد 60 بكسل)
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.Click += BtnExport_Click;
            pnlFilters.Controls.Add(btnExport);

            // Print Button
            btnPrint = new Button();
            btnPrint.Text = "PRINT";
            btnPrint.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnPrint.BackColor = Color.FromArgb(108, 117, 125);
            btnPrint.ForeColor = Color.White;
            btnPrint.Size = new Size(145, 40);
            btnPrint.Location = new Point(175, 560);  // ✅ جمب Export (نفس الصف)
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.Click += BtnPrint_Click;
            pnlFilters.Controls.Add(btnPrint);

            // DataGridView
            dgvReport = new DataGridView();
            dgvReport.Name = "dgvReport";
            dgvReport.Size = new Size(950, 600);
            dgvReport.Location = new Point(410, 80);
            dgvReport.BackgroundColor = Color.White;
            dgvReport.BorderStyle = BorderStyle.None;
            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReport.ReadOnly = true;
            dgvReport.RowHeadersVisible = false;
            dgvReport.AllowUserToAddRows = false;
            dgvReport.AllowUserToDeleteRows = false;

            // DataGridView Style
            dgvReport.EnableHeadersVisualStyles = false;
            dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 58, 64);
            dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvReport.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvReport.ColumnHeadersHeight = 40;

            dgvReport.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvReport.DefaultCellStyle.ForeColor = Color.Black;
            dgvReport.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgvReport.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvReport.DefaultCellStyle.Padding = new Padding(5);
            dgvReport.RowTemplate.Height = 35;

            this.Controls.Add(dgvReport);

            LoadReportTypes();
        }

        private void LoadReportTypes()
        {
            cmbReportType.Items.Clear();
            cmbReportType.Items.Add("a) Managers at each office");
            cmbReportType.Items.Add("b) Female drivers - Cyberjaya");
            cmbReportType.Items.Add("c) Staff count per office");
            cmbReportType.Items.Add("d) Taxis at Cyberjaya office");
            cmbReportType.Items.Add("e) Total registered taxis");
            cmbReportType.Items.Add("f) Drivers allocated per taxi");
            cmbReportType.Items.Add("g) Owners with multiple taxis");
            cmbReportType.Items.Add("h) Business clients in Cyberjaya");
            cmbReportType.Items.Add("i) Current contracts in Kuala Lumpur");
            cmbReportType.Items.Add("j) Private clients per city");
            cmbReportType.Items.Add("k) Jobs by driver on given day");
            cmbReportType.Items.Add("l) Drivers over 25 years old");
            cmbReportType.Items.Add("m) Private clients - Nov 2025");
            cmbReportType.Items.Add("n) Private clients with >3 hires");
            cmbReportType.Items.Add("o) Average fee - private clients");
            cmbReportType.Items.Add("p) Total jobs per car");
            cmbReportType.Items.Add("q) Total jobs per driver");
            cmbReportType.Items.Add("r) Total charged per car - Nov 2025");
            cmbReportType.Items.Add("s) Contract job summary");
            cmbReportType.Items.Add("t) Taxis due for maintenance - Jan 1, 2026");
            cmbReportType.Items.Add("--- NESTED QUERIES ---");
            cmbReportType.Items.Add("1) Drivers above average jobs");
            cmbReportType.Items.Add("2) Taxis never used");
            cmbReportType.Items.Add("3) Clients above average spending");
            cmbReportType.Items.Add("4) Offices above average staff");
            cmbReportType.Items.Add("5) Owners above average revenue");
            cmbReportType.Items.Add("--- SUMMARY REPORTS ---");
            cmbReportType.Items.Add("Dashboard Summary");
            cmbReportType.Items.Add("Monthly Revenue");
            cmbReportType.Items.Add("Top Performing Drivers");
        }

        private void LoadComboBoxes()
        {
            // Load Drivers for report k
            DataTable dtDrivers = StaffData.GetAllDrivers();
            dtDrivers.Columns.Add("FullName", typeof(string), "FName + ' ' + LName");

            cmbDriver = new ComboBox();
            cmbDriver.Name = "cmbDriver";
            cmbDriver.Font = new Font("Segoe UI", 11);
            cmbDriver.Size = new Size(260, 25);
            cmbDriver.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDriver.DisplayMember = "FullName";
            cmbDriver.ValueMember = "StaffID";
            cmbDriver.DataSource = dtDrivers.Copy();
            cmbDriver.Visible = false;

            // Load Taxis for report r
            DataTable dtTaxis = TaxiData.GetAllTaxis();
            dtTaxis.Columns.Add("DisplayName", typeof(string), "PlateNo + ' - ' + Model");

            cmbTaxi = new ComboBox();
            cmbTaxi.Name = "cmbTaxi";
            cmbTaxi.Font = new Font("Segoe UI", 11);
            cmbTaxi.Size = new Size(260, 25);
            cmbTaxi.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTaxi.DisplayMember = "DisplayName";
            cmbTaxi.ValueMember = "TaxiID";
            cmbTaxi.DataSource = dtTaxis.Copy();
            cmbTaxi.Visible = false;

            // Load Contracts for report s
            DataTable dtContracts = ContractData.GetAllContracts();
            dtContracts.Columns.Add("DisplayName", typeof(string), "ContractID + ' - ' + CompanyName");

            cmbContract = new ComboBox();
            cmbContract.Name = "cmbContract";
            cmbContract.Font = new Font("Segoe UI", 11);
            cmbContract.Size = new Size(260, 25);
            cmbContract.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbContract.DisplayMember = "DisplayName";
            cmbContract.ValueMember = "ContractID";
            cmbContract.DataSource = dtContracts.Copy();
            cmbContract.Visible = false;

            // Date Pickers
            dtpStartDate = new DateTimePicker();
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Font = new Font("Segoe UI", 11);
            dtpStartDate.Size = new Size(260, 25);
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpStartDate.Visible = false;

            dtpEndDate = new DateTimePicker();
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Font = new Font("Segoe UI", 11);
            dtpEndDate.Size = new Size(260, 25);
            dtpEndDate.Format = DateTimePickerFormat.Short;
            dtpEndDate.Visible = false;
        }

        private void CmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel pnlParams = pnlFilters.Controls["pnlParams"] as Panel;
            pnlParams.Controls.Clear();

            string selectedReport = cmbReportType.SelectedItem?.ToString();

            if (selectedReport == "k) Jobs by driver on given day")
            {
                // Driver selection
                Label lblDriver = new Label();
                lblDriver.Text = "Select Driver:";
                lblDriver.Font = new Font("Segoe UI", 11);
                lblDriver.Size = new Size(260, 25);
                lblDriver.Location = new Point(20, 20);
                pnlParams.Controls.Add(lblDriver);

                cmbDriver.Visible = true;
                cmbDriver.Location = new Point(20, 50);
                pnlParams.Controls.Add(cmbDriver);

                // Date selection
                Label lblDate = new Label();
                lblDate.Text = "Select Date:";
                lblDate.Font = new Font("Segoe UI", 11);
                lblDate.Size = new Size(260, 25);
                lblDate.Location = new Point(20, 90);
                pnlParams.Controls.Add(lblDate);

                dtpStartDate.Visible = true;
                dtpStartDate.Location = new Point(20, 120);
                pnlParams.Controls.Add(dtpStartDate);
            }
            else if (selectedReport == "s) Contract job summary")
            {
                // Contract selection
                Label lblContract = new Label();
                lblContract.Text = "Select Contract:";
                lblContract.Font = new Font("Segoe UI", 11);
                lblContract.Size = new Size(260, 25);
                lblContract.Location = new Point(20, 20);
                pnlParams.Controls.Add(lblContract);

                cmbContract.Visible = true;
                cmbContract.Location = new Point(20, 50);
                pnlParams.Controls.Add(cmbContract);
            }
            else if (selectedReport == "Monthly Revenue")
            {
                // Year selection
                Label lblYear = new Label();
                lblYear.Text = "Select Year:";
                lblYear.Font = new Font("Segoe UI", 11);
                lblYear.Size = new Size(260, 25);
                lblYear.Location = new Point(20, 20);
                pnlParams.Controls.Add(lblYear);

                NumericUpDown nudYear = new NumericUpDown();
                nudYear.Name = "nudYear";
                nudYear.Font = new Font("Segoe UI", 11);
                nudYear.Size = new Size(260, 25);
                nudYear.Location = new Point(20, 50);
                nudYear.Minimum = 2020;
                nudYear.Maximum = 2030;
                nudYear.Value = DateTime.Now.Year;
                pnlParams.Controls.Add(nudYear);
            }
            else if (selectedReport == "Top Performing Drivers")
            {
                // Top N selection
                Label lblTop = new Label();
                lblTop.Text = "Number of Drivers:";
                lblTop.Font = new Font("Segoe UI", 11);
                lblTop.Size = new Size(260, 25);
                lblTop.Location = new Point(20, 20);
                pnlParams.Controls.Add(lblTop);

                NumericUpDown nudTop = new NumericUpDown();
                nudTop.Name = "nudTop";
                nudTop.Font = new Font("Segoe UI", 11);
                nudTop.Size = new Size(260, 25);
                nudTop.Location = new Point(20, 50);
                nudTop.Minimum = 1;
                nudTop.Maximum = 50;
                nudTop.Value = 5;
                pnlParams.Controls.Add(nudTop);
            }
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            string selectedReport = cmbReportType.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedReport))
            {
                MessageBox.Show("Please select a report type.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dt = null;

            try
            {
                switch (selectedReport)
                {
                    // Basic Queries (a-t)
                    case "a) Managers at each office":
                        dt = ReportData.GetManagersByOffice();
                        lblTitle.Text = "Managers at Each Office";
                        break;
                    case "b) Female drivers - Cyberjaya":
                        dt = ReportData.GetFemaleDriversCyberjaya();
                        lblTitle.Text = "Female Drivers - Cyberjaya";
                        break;
                    case "c) Staff count per office":
                        dt = ReportData.GetStaffCountPerOffice();
                        lblTitle.Text = "Staff Count per Office";
                        break;
                    case "d) Taxis at Cyberjaya office":
                        dt = ReportData.GetTaxisCyberjaya();
                        lblTitle.Text = "Taxis at Cyberjaya Office";
                        break;
                    case "e) Total registered taxis":
                        dt = ReportData.GetTotalRegisteredTaxis();
                        lblTitle.Text = "Total Registered Taxis";
                        break;
                    case "f) Drivers allocated per taxi":
                        dt = ReportData.GetDriverCountPerTaxi();
                        lblTitle.Text = "Drivers Allocated per Taxi";
                        break;
                    case "g) Owners with multiple taxis":
                        dt = ReportData.GetOwnersWithMultipleTaxis();
                        lblTitle.Text = "Owners with Multiple Taxis";
                        break;
                    case "h) Business clients in Cyberjaya":
                        dt = ReportData.GetBusinessClientsCyberjaya();
                        lblTitle.Text = "Business Clients in Cyberjaya";
                        break;
                    case "i) Current contracts in Kuala Lumpur":
                        dt = ReportData.GetCurrentContractsKualaLumpur();
                        lblTitle.Text = "Current Contracts - Kuala Lumpur";
                        break;
                    case "j) Private clients per city":
                        dt = ReportData.GetPrivateClientCountByCity();
                        lblTitle.Text = "Private Clients per City";
                        break;
                    case "k) Jobs by driver on given day":
                        if (cmbDriver.SelectedValue == null || dtpStartDate.Value == null)
                        {
                            MessageBox.Show("Please select driver and date.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        dt = ReportData.GetJobsByDriverAndDate((int)cmbDriver.SelectedValue, dtpStartDate.Value);
                        lblTitle.Text = $"Jobs by Driver - {dtpStartDate.Value:dd/MM/yyyy}";
                        break;
                    case "l) Drivers over 25 years old":
                        dt = ReportData.GetDriversOver25();
                        lblTitle.Text = "Drivers Over 25 Years Old";
                        break;
                    case "m) Private clients - Nov 2025":
                        dt = ReportData.GetPrivateClientsHiredNov2025();
                        lblTitle.Text = "Private Clients - November 2025";
                        break;
                    case "n) Private clients with >3 hires":
                        dt = ReportData.GetPrivateClientsWithMoreThan3Hires();
                        lblTitle.Text = "Private Clients with More Than 3 Hires";
                        break;
                    case "o) Average fee - private clients":
                        dt = ReportData.GetAverageFeePrivateClients();
                        lblTitle.Text = "Average Fee - Private Clients";
                        break;
                    case "p) Total jobs per car":
                        dt = ReportData.GetJobCountPerTaxi();
                        lblTitle.Text = "Total Jobs per Car";
                        break;
                    case "q) Total jobs per driver":
                        dt = ReportData.GetJobCountPerDriver();
                        lblTitle.Text = "Total Jobs per Driver";
                        break;
                    case "r) Total charged per car - Nov 2025":
                        dt = ReportData.GetTotalChargedPerCarNov2025();
                        lblTitle.Text = "Total Charged per Car - November 2025";
                        break;
                    case "s) Contract job summary":
                        if (cmbContract.SelectedValue == null)
                        {
                            MessageBox.Show("Please select a contract.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        dt = ReportData.GetContractJobSummary((int)cmbContract.SelectedValue);
                        lblTitle.Text = $"Contract Job Summary - #{cmbContract.SelectedValue}";
                        break;
                    case "t) Taxis due for maintenance - Jan 1, 2026":
                        dt = ReportData.GetTaxisDueForMaintenanceJan2026();
                        lblTitle.Text = "Taxis Due for Maintenance - January 1, 2026";
                        break;

                    // Nested Queries
                    case "1) Drivers above average jobs":
                        dt = ReportData.GetDriversAboveAverageJobs();
                        lblTitle.Text = "Drivers Above Average Jobs";
                        break;
                    case "2) Taxis never used":
                        dt = ReportData.GetTaxisNeverUsed();
                        lblTitle.Text = "Taxis Never Used";
                        break;
                    case "3) Clients above average spending":
                        dt = ReportData.GetClientsAboveAverageSpending();
                        lblTitle.Text = "Clients Above Average Spending";
                        break;
                    case "4) Offices above average staff":
                        dt = ReportData.GetOfficesAboveAverageStaff();
                        lblTitle.Text = "Offices Above Average Staff";
                        break;
                    case "5) Owners above average revenue":
                        dt = ReportData.GetOwnersAboveAverageRevenue();
                        lblTitle.Text = "Owners Above Average Revenue";
                        break;

                    // Summary Reports
                    case "Dashboard Summary":
                        dt = ReportData.GetDashboardSummary();
                        lblTitle.Text = "Dashboard Summary Statistics";
                        break;
                    case "Monthly Revenue":
                        NumericUpDown nudYear = pnlFilters.Controls["pnlParams"].Controls["nudYear"] as NumericUpDown;
                        int year = nudYear != null ? (int)nudYear.Value : DateTime.Now.Year;
                        dt = ReportData.GetMonthlyRevenue(year);
                        lblTitle.Text = $"Monthly Revenue - {year}";
                        break;
                    case "Top Performing Drivers":
                        NumericUpDown nudTop = pnlFilters.Controls["pnlParams"].Controls["nudTop"] as NumericUpDown;
                        int topCount = nudTop != null ? (int)nudTop.Value : 5;
                        dt = ReportData.GetTopDrivers(topCount);
                        lblTitle.Text = $"Top {topCount} Performing Drivers";
                        break;
                }

                if (dt != null)
                {
                    dgvReport.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error generating report");
                MessageBox.Show("Error generating report: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadReportData(DataTable dt, string reportTitle)
        {
            dgvReport.DataSource = dt;
            lblTitle.Text = reportTitle;
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (dgvReport.DataSource == null || dgvReport.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel Files|*.xlsx|CSV Files|*.csv";
            saveDialog.Title = "Export Report";
            saveDialog.FileName = $"FastTaxis_Report_{DateTime.Now:yyyyMMdd_HHmmss}";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Report exported successfully!", "Export Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (dgvReport.DataSource == null || dgvReport.Rows.Count == 0)
            {
                MessageBox.Show("No data to print.", "Print Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Print functionality will be implemented here.", "Print",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            this.Dispose();
        }
    }
}