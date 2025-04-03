using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;

namespace SaovietWF
{
    public partial class FrmCongTrinh: Form
    {
        
        public FrmCongTrinh(DataTable rs)
        {
            result = rs;
            InitializeComponent();
        }
        string dbPath, password, connectionString;
        public DataTable result;
        public frmMain frm { get; set; }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // e.RowIndex sẽ là -1 nếu click vào header
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Lấy giá trị từ các ô trong hàng
                txtID.Text = selectedRow.Cells["MaSo"].Value.ToString();
                txtSohieu.Text = selectedRow.Cells["SoHieu"].Value.ToString();
                txtTen.Text = selectedRow.Cells["TenVattu"].Value.ToString();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

            // Lấy giá trị từ các ô mà bạn cần truyền
            string SoHieu = selectedRow.Cells["SoHieu"].Value.ToString();
            frm.NameCT = SoHieu;
            frm.callback();
            this.Hide();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void FrmCongTrinh_Load(object sender, EventArgs e)
        {
            //dbPath = @"C:\S.T.E 25\S.T.E 25\DATA\KT2025.mdb"; // Thay đổi đường dẫn này
            //password = "1@35^7*9)"; // Thay đổi mật khẩu này
            //connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Jet OLEDB:Database Password={password};";
            //string querykh = @" SELECT *  FROM TP154 "; // Sử dụng ? thay cho @mst trong OleDb
       
            dataGridView1.DataSource = result;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Visible = false;
            } 

            dataGridView1.Columns["SoHieu"].Visible = true;
            dataGridView1.Columns["SoHieu"].HeaderText = "Số hiệu";
            dataGridView1.Columns["SoHieu"].Width = 100;
            dataGridView1.Columns["SoHieu"].DisplayIndex = 0;
            dataGridView1.Columns["SoHieu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["SoHieu"].ReadOnly = true;

            dataGridView1.Columns["TenVattu"].Visible = true;
            dataGridView1.Columns["TenVattu"].HeaderText = "Tên";
            dataGridView1.Columns["TenVattu"].Width = 300;
            dataGridView1.Columns["TenVattu"].DisplayIndex = 1;
            dataGridView1.Columns["TenVattu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns["TenVattu"].ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Rows[0].Selected = true; // Chọn hàng đầu tiên
        }
        private DataTable ExecuteQuery(string query, params OleDbParameter[] parameters)
        {
            DataTable dataTable = new DataTable();

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Kết nối đến cơ sở dữ liệu thành công!");

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    // Thêm các tham số vào command
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                }
            }

            return dataTable; // Trả về DataTable chứa dữ liệu
        }
    }
}
