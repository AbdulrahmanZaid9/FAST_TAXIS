using FAST_TAXIS3.Data;
using FAST_TAXIS3.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FAST_TAXIS3.Forms
{
    public partial class AllocationForm : Form
    {
        private DataGridView dgvAllocations;
        private Panel pnlForm;
        private ComboBox cmbTaxi;
        private ComboBox cmbDriver;
        private Button btnAllocate;
        private Button btnRemove;
        private Button btnClear;
        private Label lblTaxiId;
        private Label lblDriverId;

        public AllocationForm()
        {
            InitializeComponent();
            SetupForm();
            LoadTaxis();
            LoadDrivers();
            LoadAllocations();
        }

        private void SetupForm()
        {
            this.Text = "Fast Taxis - Taxi Driver Allocation";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 245);
            this.MinimumSize = new Size(1000, 600);

            // Title Label
            Label lblTitle = new Label();
            lblTitle.Text = "TAXI DRIVER ALLOCATION";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(50, 50, 55);
            lblTitle.Size = new Size(500, 50);
            lblTitle.Location = new Point(30, 20);
            this.Controls.Add(lblTitle);

            // Form Panel
            pnlForm = new Panel();
            pnlForm.Size = new Size(450, 300);
            pnlForm.Location = new Point(30, 80);
            pnlForm.BackColor = Color.White;
            pnlForm.BorderStyle = BorderStyle.None;
            this.Controls.Add(pnlForm);

            // Form Title
            Label lblFormTitle = new Label();
            lblFormTitle.Text = "New Allocation";
            lblFormTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblFormTitle.Size = new Size(200, 30);
            lblFormTitle.Location = new Point(20, 20);
            pnlForm.Controls.Add(lblFormTitle);

            // Taxi Selection
            Label lblTaxi = new Label();
            lblTaxi.Text = "Select Taxi:";
            lblTaxi.Font = new Font("Segoe UI", 11);
            lblTaxi.ForeColor = Color.Black;
            lblTaxi.Size = new Size(120, 25);
            lblTaxi.Location = new Point(20, 70);
            pnlForm.Controls.Add(lblTaxi);

            cmbTaxi = new ComboBox();
            cmbTaxi.Name = "cmbTaxi";
            cmbTaxi.Font = new Font("Segoe UI", 11);
            cmbTaxi.Size = new Size(280, 25);
            cmbTaxi.Location = new Point(140, 70);
            cmbTaxi.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTaxi.SelectedIndexChanged += CmbTaxi_SelectedIndexChanged;
            pnlForm.Controls.Add(cmbTaxi);

            // Driver Selection
            Label lblDriver = new Label();
            lblDriver.Text = "Select Driver:";
            lblDriver.Font = new Font("Segoe UI", 11);
            lblDriver.ForeColor = Color.Black;
            lblDriver.Size = new Size(120, 25);
            lblDriver.Location = new Point(20, 110);
            pnlForm.Controls.Add(lblDriver);

            cmbDriver = new ComboBox();
            cmbDriver.Name = "cmbDriver";
            cmbDriver.Font = new Font("Segoe UI", 11);
            cmbDriver.Size = new Size(280, 25);
            cmbDriver.Location = new Point(140, 110);
            cmbDriver.DropDownStyle = ComboBoxStyle.DropDownList;
            pnlForm.Controls.Add(cmbDriver);

            // Hidden Labels for IDs
            lblTaxiId = new Label();
            lblTaxiId.Name = "lblTaxiId";
            lblTaxiId.Visible = false;
            pnlForm.Controls.Add(lblTaxiId);

            lblDriverId = new Label();
            lblDriverId.Name = "lblDriverId";
            lblDriverId.Visible = false;
            pnlForm.Controls.Add(lblDriverId);

            // Buttons Panel
            Panel pnlButtons = new Panel();
            pnlButtons.Size = new Size(400, 100);
            pnlButtons.Location = new Point(20, 160);
            pnlButtons.BackColor = Color.Transparent;
            pnlForm.Controls.Add(pnlButtons);

            // Allocate Button
            btnAllocate = new Button();
            btnAllocate.Text = "ALLOCATE";
            btnAllocate.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnAllocate.BackColor = Color.FromArgb(40, 167, 69);
            btnAllocate.ForeColor = Color.White;
            btnAllocate.Size = new Size(120, 40);
            btnAllocate.Location = new Point(0, 0);
            btnAllocate.FlatStyle = FlatStyle.Flat;
            btnAllocate.FlatAppearance.BorderSize = 0;
            btnAllocate.Click += BtnAllocate_Click;
            pnlButtons.Controls.Add(btnAllocate);

            // Remove Button
            btnRemove = new Button();
            btnRemove.Text = "REMOVE";
            btnRemove.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnRemove.BackColor = Color.FromArgb(220, 53, 69);
            btnRemove.ForeColor = Color.White;
            btnRemove.Size = new Size(120, 40);
            btnRemove.Location = new Point(130, 0);
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.Enabled = false;
            btnRemove.Click += BtnRemove_Click;
            pnlButtons.Controls.Add(btnRemove);

            // Clear Button
            btnClear = new Button();
            btnClear.Text = "CLEAR";
            btnClear.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnClear.BackColor = Color.FromArgb(108, 117, 125);
            btnClear.ForeColor = Color.White;
            btnClear.Size = new Size(120, 40);
            btnClear.Location = new Point(260, 0);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Click += BtnClear_Click;
            pnlButtons.Controls.Add(btnClear);

            // DataGridView
            dgvAllocations = new DataGridView();
            dgvAllocations.Name = "dgvAllocations";
            dgvAllocations.Size = new Size(650, 500);
            dgvAllocations.Location = new Point(500, 80);
            dgvAllocations.BackgroundColor = Color.White;
            dgvAllocations.BorderStyle = BorderStyle.None;
            dgvAllocations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAllocations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAllocations.MultiSelect = false;
            dgvAllocations.ReadOnly = true;
            dgvAllocations.RowHeadersVisible = false;
            dgvAllocations.AllowUserToAddRows = false;
            dgvAllocations.CellClick += DgvAllocations_CellClick;

            // DataGridView Style
            dgvAllocations.EnableHeadersVisualStyles = false;
            dgvAllocations.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 58, 64);
            dgvAllocations.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvAllocations.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvAllocations.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAllocations.ColumnHeadersHeight = 40;

            dgvAllocations.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvAllocations.DefaultCellStyle.ForeColor = Color.Black;
            dgvAllocations.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgvAllocations.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvAllocations.DefaultCellStyle.Padding = new Padding(5);
            dgvAllocations.RowTemplate.Height = 35;

            this.Controls.Add(dgvAllocations);

            // Statistics Panel
            Panel pnlStats = new Panel();
            pnlStats.Size = new Size(650, 80);
            pnlStats.Location = new Point(500, 590);
            pnlStats.BackColor = Color.FromArgb(52, 58, 64);
            this.Controls.Add(pnlStats);

            Label lblStatsTitle = new Label();
            lblStatsTitle.Text = "Allocation Summary";
            lblStatsTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblStatsTitle.ForeColor = Color.White;
            lblStatsTitle.Size = new Size(200, 30);
            lblStatsTitle.Location = new Point(20, 10);
            pnlStats.Controls.Add(lblStatsTitle);

            Label lblTotalAllocations = new Label();
            lblTotalAllocations.Name = "lblTotalAllocations";
            lblTotalAllocations.Text = "Total Allocations: 0";
            lblTotalAllocations.Font = new Font("Segoe UI", 11);
            lblTotalAllocations.ForeColor = Color.LightGray;
            lblTotalAllocations.Size = new Size(200, 25);
            lblTotalAllocations.Location = new Point(20, 40);
            pnlStats.Controls.Add(lblTotalAllocations);

            Label lblUniqueTaxis = new Label();
            lblUniqueTaxis.Name = "lblUniqueTaxis";
            lblUniqueTaxis.Text = "Taxis with Drivers: 0";
            lblUniqueTaxis.Font = new Font("Segoe UI", 11);
            lblUniqueTaxis.ForeColor = Color.LightGray;
            lblUniqueTaxis.Size = new Size(200, 25);
            lblUniqueTaxis.Location = new Point(250, 40);
            pnlStats.Controls.Add(lblUniqueTaxis);
        }

        private void LoadTaxis()
        {
            DataTable dt = TaxiData.GetAllTaxis();
            dt.Columns.Add("DisplayName", typeof(string), "PlateNo + ' - ' + Model");
            cmbTaxi.DisplayMember = "DisplayName";
            cmbTaxi.ValueMember = "TaxiID";
            cmbTaxi.DataSource = dt;
        }

        private void LoadDrivers()
        {
            DataTable dt = StaffData.GetAllDrivers();
            dt.Columns.Add("FullName", typeof(string), "FName + ' ' + LName");
            cmbDriver.DisplayMember = "FullName";
            cmbDriver.ValueMember = "StaffID";
            cmbDriver.DataSource = dt;
        }

        private void LoadAllocations()
        {
            DataTable dt = AllocationData.GetAllAllocations();
            dgvAllocations.DataSource = dt;
            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            DataTable dt = AllocationData.GetAllAllocations();
            Label lblTotalAllocations = this.Controls.Find("lblTotalAllocations", true).FirstOrDefault() as Label;
            Label lblUniqueTaxis = this.Controls.Find("lblUniqueTaxis", true).FirstOrDefault() as Label;

            if (lblTotalAllocations != null)
            {
                lblTotalAllocations.Text = $"Total Allocations: {dt.Rows.Count}";
            }

            if (lblUniqueTaxis != null)
            {
                DataTable taxiCount = TaxiData.GetDriverCountPerTaxi();
                int taxisWithDrivers = taxiCount.Select("DriverCount > 0").Length;
                lblUniqueTaxis.Text = $"Taxis with Drivers: {taxisWithDrivers}";
            }
        }

        private void CmbTaxi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTaxi.SelectedValue != null && cmbTaxi.SelectedValue is int)
            {
                int taxiId = (int)cmbTaxi.SelectedValue;
                lblTaxiId.Text = taxiId.ToString();

                // ✅ لا تغير مصدر بيانات السائقين هنا، خلهم على كل السائقين
                // مجرد تحديث lblTaxiId فقط
            }
        }
        private void LoadAllDrivers()
        {
            DataTable dt = StaffData.GetAllDrivers();  // ✅ ترجع كل السائقين
            dt.Columns.Add("FullName", typeof(string), "FName + ' ' + LName");
            cmbDriver.DisplayMember = "FullName";
            cmbDriver.ValueMember = "StaffID";
            cmbDriver.DataSource = dt;
        }
        private void DgvAllocations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvAllocations.Rows[e.RowIndex];

                if (row.Cells["TaxiID"].Value != null && row.Cells["DriverID"].Value != null)
                {
                    int taxiId = Convert.ToInt32(row.Cells["TaxiID"].Value);
                    int driverId = Convert.ToInt32(row.Cells["DriverID"].Value);

                    // ✅ تعيين السيارة أولاً
                    cmbTaxi.SelectedValue = taxiId;

                    // ✅ إعادة تحميل كل السائقين (مو بس المتاحين)
                    LoadAllDrivers();

                    // ✅ تعيين السائق بعد ما يتغير الـ DataSource
                    cmbDriver.SelectedValue = driverId;

                    // ✅ تخزين الـ IDs (اختياري)
                    lblTaxiId.Text = taxiId.ToString();
                    lblDriverId.Text = driverId.ToString();

                    btnRemove.Enabled = true;
                    btnAllocate.Enabled = false;
                }
            }
        }

        private void BtnAllocate_Click(object sender, EventArgs e)
        {
            if (!ValidateSelection())
                return;

            try
            {
                int taxiId = Convert.ToInt32(cmbTaxi.SelectedValue);
                int driverId = Convert.ToInt32(cmbDriver.SelectedValue);

                // Check if allocation already exists
                if (AllocationData.IsAllocationExists(taxiId, driverId))
                {
                    MessageBox.Show("This driver is already allocated to this taxi.",
                        "Duplicate Allocation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool result = AllocationData.AddAllocation(taxiId, driverId);

                if (result)
                {
                    MessageBox.Show("Driver allocated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadAllocations();

                    // Refresh available drivers
                    CmbTaxi_SelectedIndexChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error allocating driver");
                MessageBox.Show("Error allocating driver: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (!ValidateSelection())
                return;

            DialogResult result = MessageBox.Show("Are you sure you want to remove this allocation?",
                "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int taxiId = Convert.ToInt32(cmbTaxi.SelectedValue);
                    int driverId = Convert.ToInt32(cmbDriver.SelectedValue);

                    bool deleted = AllocationData.DeleteAllocation(taxiId, driverId);

                    if (deleted)
                    {
                        MessageBox.Show("Allocation removed successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearForm();
                        LoadAllocations();

                        // Refresh available drivers
                        CmbTaxi_SelectedIndexChanged(null, null);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Error removing allocation");
                    MessageBox.Show("Error removing allocation: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            if (cmbTaxi.Items.Count > 0)
                cmbTaxi.SelectedIndex = -1;

            // ✅ استخدم LoadAllDrivers بدل LoadDrivers
            LoadAllDrivers();

            lblTaxiId.Text = "";
            lblDriverId.Text = "";

            btnAllocate.Enabled = true;
            btnRemove.Enabled = false;
        }

        private bool ValidateSelection()
        {
            if (cmbTaxi.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a taxi.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTaxi.Focus();
                return false;
            }

            if (cmbDriver.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a driver.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDriver.Focus();
                return false;
            }

            return true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            this.Dispose();
        }
    }
}