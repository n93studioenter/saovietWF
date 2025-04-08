using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Xml;
using System.Globalization;
using ClosedXML.Excel;
using OpenQA.Selenium.Chrome;
using System.Reflection;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using Keys = OpenQA.Selenium.Keys;
using System.IO.Compression;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Spreadsheet;
using static Org.BouncyCastle.Math.EC.ECCurve;
using static iText.IO.Image.Jpeg2000ImageData;
using System.Collections;
namespace SaovietWF
{
    public partial class frmMain : Form
    {
        public string NameCT { get; set; }
        public DataTable result { get; set; }
        #region Class  List databiding
        public class FileImportDetail
        {
            public string Ten { get; set; }
            public int ParentId { get; set; }
            public string SoHieu { get; set; }
            public double Soluong { get; set; }
            public double Dongia { get; set; }
            public string DVT { get; set; }
            public int IdTp { get; set; }


            public FileImportDetail(string ten, int parentId, string soHieu, double soluong, double dongia, string dVT)
            {
                Ten = ten;
                ParentId = parentId;
                SoHieu = soHieu;
                Soluong = soluong;
                Dongia = dongia;
                DVT = dVT;
            }
        }

        public class FileImport
        {
            public string Path { get; set; }
            public bool Checked { get; set; }
            public int ID { get; set; }
            public string SHDon { get; set; }
            public string KHHDon { get; set; }
            public DateTime NLap { get; set; }
            public string Ten { get; set; }
            public string Noidung { get; set; }
            public int TKCo { get; set; }
            public string TKNo { get; set; }
            public int TkThue { get; set; }
            public string Mst { get; set; }
            public double TongTien { get; set; }
            public int Vat { get; set; }
            public int Type { get; set; }

            public string SoHieuTP { get; set; }
            public List<FileImportDetail> fileImportDetails;
            public FileImport(string path, string shdon, string khhdon, DateTime nlap, string ten, string noidung, string tkno, int tkco, int tkthue, string mst, double tongTien, int vat, int type, string tenTP)
            {
                ID = Id;
                SHDon = shdon;
                KHHDon = khhdon;
                NLap = nlap;
                Ten = ten;
                Noidung = noidung;
                TKCo = tkco;
                TKNo = tkno;
                TkThue = tkthue;
                Mst = mst;
                TongTien = tongTien;
                Vat = vat;
                Id += 1;
                fileImportDetails = new List<FileImportDetail>();
                Type = type;
                Checked = true;
                Path = path;
                SoHieuTP = tenTP;
            }

        }
        private BindingList<FileImport> people = new BindingList<FileImport>();
        #endregion

        #region Load and setting
        string dbPath, password, connectionString;
        public static int Id = 1;
        public string MasterMST = "3502264379";
        public void InitData()
        {
            savedPath = ConfigurationManager.AppSettings["LastFilePath"];
            txtPath.Text = savedPath;
            txtPath.Enabled = false;
            //Thiết lập mặc định cho cbb Tháng
            for (int i = 1; i <= 12; i++)
            {
                cbbFrom.Items.Add(i);
                cbbTo.Items.Add(i);
                cbbselect.Items.Add(i);
            }
            cbbFrom.SelectedIndex = 0;
            cbbselect.SelectedIndex = 0;
            cbbTo.SelectedItem = DateTime.Now.Month;
        }
        private void InitDB()
        {
            // Đường dẫn đến cơ sở dữ liệu Access và mật khẩu
            dbPath = @"C:\S.T.E 25\S.T.E 25\DATA\KT2025.mdb"; // Thay đổi đường dẫn này
            password = "1@35^7*9)"; // Thay đổi mật khẩu này
            connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Jet OLEDB:Database Password={password};";
            //connectionString = $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={dbPath};Jet OLEDB:Database Password={password};";
            // connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Jet OLEDB:Database";
            //connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\S.T.E 25\S.T.E 25\DATA\importData.accdb;Persist Security Info=False";
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string tableName = "tbimport";
                    string tableNamedetail = "tbimportdetail";
                    string tableDinhdanh = "tbDinhdanhtaikhoan";
                    // Kiểm tra xem bảng đã tồn tại hay không
                    if (!TableExists(connection, tableDinhdanh))
                    {
                        // Tạo bảng nếu chưa tồn tại
                        CreateTableDinhDanh(connection, tableDinhdanh);
                        Console.WriteLine($"Bảng '{tableDinhdanh}' đã được tạo thành công.");
                    }
                    else
                    {
                        Console.WriteLine($"Bảng '{tableName}' đã tồn tại.");
                    }
                    if (!TableExists(connection, tableName))
                    {
                        // Tạo bảng nếu chưa tồn tại
                        CreateTable(connection, tableName);
                        Console.WriteLine($"Bảng '{tableName}' đã được tạo thành công.");
                    }
                    else
                    {
                        Console.WriteLine($"Bảng '{tableName}' đã tồn tại.");
                    }
                    // Kiểm tra xem bảng đã tồn tại hay không
                    if (!TableExists(connection, tableNamedetail))
                    {
                        // Tạo bảng nếu chưa tồn tại
                        CreateTableDetail(connection, tableNamedetail);
                        Console.WriteLine($"Bảng '{tableNamedetail}' đã được tạo thành công.");
                    }
                    else
                    {
                        Console.WriteLine($"Bảng '{tableNamedetail}' đã tồn tại.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi xảy ra: {ex.Message}");
            }

        }
        static bool TableExists(OleDbConnection connection, string tableName)
        {
            try
            {
                // Kiểm tra sự tồn tại của bảng
                DataTable schemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow row in schemaTable.Rows)
                {
                    if (row["TABLE_NAME"].ToString().Equals(tableName, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi kiểm tra bảng: {ex.Message}");
            }
            return false;
        }
        static void CreateTableDinhDanh(OleDbConnection connection, string tableName)
        {
            string createTableQuery = $@"
        CREATE TABLE {tableName} (
            ID AUTOINCREMENT PRIMARY KEY,
            Type TEXT,
            KeyValue TEXT,
            TKNo TEXT,  
            TKCo TEXT,
            TKThue TEXT 
        );";

            using (OleDbCommand command = new OleDbCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
        static void CreateTable(OleDbConnection connection, string tableName)
        {
            string createTableQuery = $@"
        CREATE TABLE {tableName} (
            ID AUTOINCREMENT PRIMARY KEY,
            SHDon TEXT,
            KHHDon TEXT,
            NLap TEXT,
            Ten TEXT,
            Noidung TEXT,
            TKCo TEXT,
            TKNo TEXT,
            TkThue TEXT,
            Mst TEXT,
            Status NUMBER,
            Ngaytao TEXT,
            TongTien NUMBER,
            Vat NUMBER,
            SohieuTP TEXT
        );";

            using (OleDbCommand command = new OleDbCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
        static void CreateTableDetail(OleDbConnection connection, string tableName)
        {
            string createTableQuery = $@"
        CREATE TABLE {tableName} (
            ID AUTOINCREMENT PRIMARY KEY,
            ParentId TEXT,
            SoHieu TEXT,
            SoLuong TEXT,
            DonGia TEXT,
            DVT TEXT,
            Ten TEXT 
        );";

            using (OleDbCommand command = new OleDbCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
        public frmMain()
        {
            InitializeComponent();
            InitData();
            InitDB();

        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrEmpty(savedPath))
                return;
            if (isClick)
                File.WriteAllText(savedPath + "\\status.txt", "ButtonClicked");
            else
                File.WriteAllText(savedPath + "\\status.txt", "");
        }
        string savedPath = "";
        public void CheckPathExist()
        {
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                cbbFrom.Enabled = false;
                cbbTo.Enabled = false;
                btnImport.Enabled = false;
                btnLoc.Enabled = false;
            }
        }
        private void btnCapnhat_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Chọn thư mục bạn muốn lưu.";
                // folderBrowserDialog.rootFolder = Environment.SpecialFolder.MyComputer; // Thay đổi thư mục gốc nếu cần

                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string selectedPath = folderBrowserDialog.SelectedPath;

                    // Lưu đường dẫn thư mục vào App.config
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["LastFilePath"].Value = selectedPath;
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");

                    savedPath = selectedPath;
                    txtPath.Text = savedPath;
                }
                else if (result == DialogResult.Cancel)
                {
                    // MessageBox.Show("Không có thư mục nào được chọn.");
                }
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            string querykh = @" SELECT *  FROM TP154 "; // Sử dụng ? thay cho @mst trong OleDb

            result = ExecuteQuery(querykh, new OleDbParameter("?", ""));

            //Kiểm tra có thiết lập đường dẫn chưa
            CheckPathExist();
            LoadXmlFiles(savedPath);
        }
        private void cbbFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbFrom.SelectedItem != null && cbbTo.SelectedItem != null && (int)cbbTo.SelectedItem < (int)cbbFrom.SelectedItem)
                cbbTo.SelectedItem = cbbFrom.SelectedItem;
        }
        #endregion

        #region Load XML,Load Excel, Load pdf, Load image
        BindingSource bindingSource = new BindingSource();
        bool isClick = false;
        private static bool IsFileInMonthRange(string filePath, string baseDirectory, int fromMonth, int toMonth)
        {
            // Lấy tên thư mục từ đường dẫn file
            string directoryName = Path.GetDirectoryName(filePath)?.Split(Path.DirectorySeparatorChar).Last();

            // Kiểm tra xem tên thư mục có thuộc khoảng tháng không
            if (int.TryParse(directoryName, out int month))
            {
                return month >= fromMonth && month <= toMonth;
            }

            return false; // Không phải thư mục tháng hợp lệ
        }
        private void LoadXmlFiles(string path)
        {
            
            if (string.IsNullOrEmpty(savedPath))
                return;
            people = new BindingList<FileImport>();
            path = path + "\\HDDauVao";
            int fromMonth = int.Parse(cbbFrom.SelectedItem.ToString()); // Thay đổi theo tháng bắt đầu (ví dụ: 3 cho tháng 3)
            int toMonth = int.Parse(cbbTo.SelectedItem.ToString());   // Thay đổi theo tháng kết thúc (ví dụ: 7 cho tháng 7)
            // Lấy tất cả các file XML từ các thư mục tháng từ fromMonth đến toMonth
            var files = Directory.EnumerateFiles(path, "*.xml", SearchOption.AllDirectories)
                .Where(file => IsFileInMonthRange(file, path, fromMonth, toMonth)) ; // Kiểm tra xem file có nằm trong khoảng tháng
            totalXMl = files.Count();
             //files = files.Skip(pageindex * pagelenght).Take(pagelenght);
            //           files = Directory.EnumerateFiles(path, "*.xml", SearchOption.AllDirectories)
            //.Where(file => !file.Contains("HDChonLoc"));  // Loại trừ đường dẫn chứa "HDChonLoc"
            foreach (string file in files)
            {
                // Lấy tên tệp từ đường dẫn
                string fileName = Path.GetFileName(file);
                //people.Add(new FileImport(file,10,"asdsa"));

                //Đọc từ XML
                XmlDocument xmlDoc = new XmlDocument();
                string fullPath = file;
                xmlDoc.Load(fullPath); // Tải file XML

                // Lấy phần tử gốc
                XmlNode root = xmlDoc.DocumentElement;

                // Lấy phần tử <NDHDon>
                XmlNode ndhDonNode = root.SelectSingleNode("//NDHDon");
                XmlNode nTTChungNode = root.SelectSingleNode("//TTChung");
                XmlNode nTTThanToan = root.SelectSingleNode("//LTSuat");
                var nThTien = root.SelectNodes("//LTSuat//ThTien");
                var nTSuat = root.SelectNodes("//LTSuat//TSuat");
                string SHDon = "";
                string KHHDon = "";
                string ten = "";
                string mst = "";
                string ten2 = "";
                string mst2 = "";
                string SoHD = "";
                int TkCo = 0;
                int TkNo = 0;
                int TkThue = 0;
                int Vat = 0;
                double Thanhtien = 0;
                string diengiai = "";
                DateTime NLap = new DateTime();
                if (nTTChungNode != null)
                {
                    SHDon = nTTChungNode.SelectSingleNode("SHDon")?.InnerText;
                    KHHDon = nTTChungNode.SelectSingleNode("KHHDon")?.InnerText;
                    NLap = DateTime.Parse(nTTChungNode.SelectSingleNode("NLap")?.InnerText);
                }
                //Kiểm tra trong database có hoa do nay chưa
                string query = "SELECT * FROM HoaDon WHERE KyHieu = ? AND SoHD LIKE ?";


                // Tạo mảng tham số với giá trị cho câu lệnh SQL
                OleDbParameter[] parameters = new OleDbParameter[]
                {
            new OleDbParameter("KyHieu", KHHDon),          // Sử dụng chỉ số mà không cần tên
            new OleDbParameter("SoHD", "%" + SHDon + "%")  // Thêm ký tự % cho LIKE
                };
                var kq = ExecuteQuery(query, parameters);
                if (kq.Rows.Count > 0)
                {
                    continue;
                }
                if (people.Any(m => m.SHDon.Contains(SHDon) && m.KHHDon == KHHDon))
                {
                    continue;
                }

                query = "SELECT * FROM tbimport WHERE KHHDon = ? AND SHDon LIKE ?";
                 parameters = new OleDbParameter[]
               {
            new OleDbParameter("KHHDon", KHHDon),          // Sử dụng chỉ số mà không cần tên
            new OleDbParameter("SHDon", "%" + SHDon + "%")  // Thêm ký tự % cho LIKE
               };
                 kq = ExecuteQuery(query, parameters);
                if (kq.Rows.Count > 0)
                {
                    continue;
                }


                XmlNode nBanNode = ndhDonNode.SelectSingleNode("NBan");
                if (nBanNode != null)
                {
                    ten = nBanNode.SelectSingleNode("Ten")?.InnerText;
                    mst = nBanNode.SelectSingleNode("MST")?.InnerText;
                    if (mst == MasterMST)
                    {
                        XmlNode nMuaNode = ndhDonNode.SelectSingleNode("NMua");
                        if (nBanNode != null)
                        {
                            ten = nMuaNode.SelectSingleNode("Ten")?.InnerText;
                            mst = nMuaNode.SelectSingleNode("MST")?.InnerText;
                        }
                    }
                }

                if (nTSuat != null)
                {
                    for(int i=0;i< nTSuat.Count; i++)
                    {
                        XmlNode item = nTSuat[i];
                        if (item.InnerText != "KKKNT" && item.InnerText != "KCT")
                            Vat = int.Parse(item.InnerText.Replace("%", ""));
                        else
                            Vat = 0;
                    } 
                   
                }
                else
                {
                    Vat = 0;
                }
                if (nThTien != null)
                {
                    if (nThTien.Count > 0)
                    {
                        for (int i = 0; i < nThTien.Count; i++)
                        {
                            if (nThTien[i].InnerText != "0")
                                Thanhtien = double.Parse(nThTien[i].InnerText);
                            else
                                Thanhtien = 0;
                        }
                    }
                    else
                    {
                        XmlNode TgTTTBSo = root.SelectSingleNode("//TToan//TgTTTBSo");
                        Thanhtien = double.Parse(TgTTTBSo.InnerText);
                    }
                   
                }
                else
                {
                    //Kiểm tra tiếp
                    XmlNode TgTTTBSo = root.SelectSingleNode("//TToan//TgTTTBSo");
                    Thanhtien = double.Parse(TgTTTBSo.InnerText);
                }

                //Kiểm tra thêm mới khách hàng
                string querykh = @" SELECT TOP 1 *  FROM KhachHang As kh
WHERE kh.MST = ?"; // Sử dụng ? thay cho @mst trong OleDb
                DataTable result = ExecuteQuery(querykh, new OleDbParameter("?", mst));
                if (result.Rows.Count == 0)
                {
                    string diachi = nBanNode.SelectSingleNode("DChi")?.InnerText;
                    var Sohieu = GetLastFourDigits(mst);
                    ten = Helper.ConvertUnicodeToVni(ten);
                    diachi = Helper.ConvertUnicodeToVni(diachi);
                    query = @" SELECT TOP 1 *  FROM KhachHang As kh
WHERE kh.SoHieu = ?";
                    DataTable result2 = ExecuteQuery(query, new OleDbParameter("?", Sohieu));
                    if (result2.Rows.Count > 0)
                        Sohieu = "0" + Sohieu;
                    if (string.IsNullOrEmpty(diachi))
                    {
                        diachi = Helper.ConvertUnicodeToVni("Bô sung địa chỉ");
                    }
                    InitCustomer(2, Sohieu, ten, diachi, mst);
                }



                query = @" SELECT TOP 1 *  FROM KhachHang AS kh  
INNER JOIN HoaDon AS hd ON kh.Maso = hd.MaKhachHang    
WHERE kh.MST = ?  
ORDER BY hd.MaSo DESC"; // Sử dụng ? thay cho @mst trong OleDb
                result = ExecuteQuery(query, new OleDbParameter("?", mst));
                if (result.Rows.Count > 0)
                {
                    SoHD = result.Rows[0]["SoHD"].ToString();

                    query = @"Select top 2 * from ChungTu 
where SoHieu = ?
ORDER BY  MaSo DESC";
                    result = ExecuteQuery(query, new OleDbParameter("?", SoHD));
                    var index = 0;
                    if (result.Rows.Count > 0)
                    {
                        foreach (DataRow row in result.Rows)
                        {
                            if (index == 0)
                            {
                                TkThue = int.Parse(row["MaTKNo"].ToString());  // Giả sử có cột "MaSo"; 
                            }
                            if (index == 1)
                            {
                                TkNo = int.Parse(row["MaTKNo"].ToString());  // Giả sử có cột "MaSo"; 
                                TkCo = int.Parse(row["MaTKCo"].ToString());  // Giả sử có cột "MaSo"; 
                                diengiai = Helper.ConvertVniToUnicode(row["DienGiai"].ToString());
                            }
                            // Lấy giá trị từ cột cụ thể trong hàng hiện tại

                            index += 1;
                        }
                    }
                    // Tra cứu từ bảng HeThongTK
                    query = @"Select   * from HeThongTK where MaTC = ?";
                    result = ExecuteQuery(query, new OleDbParameter("?", TkNo));
                    if (result.Rows.Count > 0)
                    {
                        TkNo = int.Parse(result.Rows[0]["SoHieu"].ToString());

                        query = @"Select   * from HeThongTK where MaTC = ?";
                        if (TkCo > 0)
                        {
                            result = ExecuteQuery(query, new OleDbParameter("?", TkCo));
                            TkCo = int.Parse(result.Rows[0]["SoHieu"].ToString());
                        }


                        query = @"Select   * from HeThongTK where MaTC = ?";
                        if (TkThue > 0)
                        {
                            result = ExecuteQuery(query, new OleDbParameter("?", TkThue));
                            TkThue = int.Parse(result.Rows[0]["SoHieu"].ToString());
                        }


                    }
                    else
                    {
                        //TkNo = 0;
                        //TkCo = 1111;
                        //TkThue = 1331;
                    }
                }
                else
                {
                    //TkNo = 0;
                    //TkCo = 1111;
                    //TkThue = 1331;
                }
                if (TkThue == 0)
                {
                    //if (TkCo != 5111)
                    //    TkThue = 1331;
                    //else
                    //    TkThue = 33311;
                }
                //Add detail
                var hhdVuList = xmlDoc.SelectNodes("//HHDVu");

                //Kiểm tra Đã tồn tại số hóa đơn và số hiệu
                if (!people.Any(m => m.SHDon.Contains(SHDon) && m.KHHDon == KHHDon))
                {
                    people.Add(new FileImport(file, SHDon, KHHDon, NLap, ten, diengiai, TkNo.ToString(), TkCo, TkThue, mst, Thanhtien, Vat, 1, ""));
                }
                for (int i = 0; i < hhdVuList.Count; i++)
                {
                    try
                    {
                        if (hhdVuList[i].SelectSingleNode("DVTinh") != null && !string.IsNullOrEmpty(hhdVuList[i].SelectSingleNode("DVTinh").ToString()))
                        {
                            var THHDVu = hhdVuList[i].SelectSingleNode("THHDVu").InnerText;
                            var DVTinh = hhdVuList[i].SelectSingleNode("DVTinh").InnerText;
                           
                            var SLuong = hhdVuList[i].SelectSingleNode("SLuong").InnerText;
                            var DGia = "";
                            if (hhdVuList[i].SelectSingleNode("DGia") != null)
                                DGia = hhdVuList[i].SelectSingleNode("DGia").InnerText;
                            else
                                DGia = "0";
                                string newName = Helper.ConvertUnicodeToVni(THHDVu);
                            //Kiểm tra trong database xem có sản phẩm chưa, nếu chưa có thì thêm mới
                            query = @"Select * from Vattu 
where TenVattu = ? AND DonVi = ? ";
                            //int rs = (int)ExecuteQuery(query, new OleDbParameter("?", "SAdsd")).Rows[0][0];
                            var getdata = ExecuteQuery(query, new OleDbParameter("?", newName), new OleDbParameter("?", Helper.ConvertUnicodeToVni(DVTinh)));

                            string sohieu = ""; 
                            if (getdata.Rows.Count == 0)
                                sohieu = GenerateResultString(THHDVu.Trim());
                            else
                                sohieu = getdata.Rows[0]["SoHieu"].ToString();

                            FileImportDetail fileImportDetail = new FileImportDetail(newName, people.LastOrDefault().ID, sohieu, double.Parse(SLuong), double.Parse(DGia), Helper.ConvertUnicodeToVni(DVTinh));
                            people.LastOrDefault().fileImportDetails.Add(fileImportDetail);
                            if (getdata.Rows.Count == 0)
                            {
                                if (DVTinh == "kWh")
                                {
                                   
                                    continue;
                                }
                                //Insert thêm vô database
                                query = @"
        INSERT INTO Vattu (MaPhanLoai,SoHieu,TenVattu,DonVi)
        VALUES (?,?,?,?)";
                                parameters = new OleDbParameter[]
                    {
        new OleDbParameter("?","1"),
          new OleDbParameter("?",sohieu),
           new OleDbParameter("?",newName),
            new OleDbParameter("?",Helper.ConvertUnicodeToVni(DVTinh))
                    };

                                // Thực thi truy vấn và lấy kết quả
                                int a = ExecuteQueryResult(query, parameters);
                            }
                        }
                        else
                        {
                            var THHDVu = hhdVuList[i].SelectSingleNode("THHDVu").InnerText;
                            if (THHDVu.ToLower().Contains("chiết khấu"))
                            {
                                var ThTien = hhdVuList[i].SelectSingleNode("ThTien")?.InnerText;
                                if (ThTien == null)
                                    ThTien = hhdVuList[i].SelectSingleNode("THTien")?.InnerText;
                                if (hhdVuList.Count == 1)
                                {
                                    FileImportDetail fileImportDetail = new FileImportDetail(THHDVu, people.LastOrDefault().ID, "711", 1, double.Parse(ThTien), "Exception");
                                    people.LastOrDefault().TKNo = "711";
                                    people.LastOrDefault().TKCo = 3311;
                                    people.LastOrDefault().TkThue = 1331;
                                    people.LastOrDefault().Noidung = "Cấn trừ";
                                    people.LastOrDefault().fileImportDetails.Add(fileImportDetail);
                                }
                                else
                                {
                                    FileImportDetail fileImportDetail = new FileImportDetail(THHDVu, people.LastOrDefault().ID, "711", 0, double.Parse(ThTien), "Exception");
                                    people.LastOrDefault().fileImportDetails.Add(fileImportDetail);
                                }
                              
                            }
                            else
                            {
                                var ThTien = hhdVuList[i].SelectSingleNode("ThTien")?.InnerText;
                                if (ThTien == null)
                                    ThTien = hhdVuList[i].SelectSingleNode("THTien")?.InnerText;
                                if (ThTien != null && double.Parse(ThTien) > 0)
                                {
                                    FileImportDetail fileImportDetail = new FileImportDetail(THHDVu, people.LastOrDefault().ID, "6422", 0, double.Parse(ThTien), "Exception");
                                    people.LastOrDefault().fileImportDetails.Add(fileImportDetail);
                                }

                            }

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    //Kiểm tra nếu ko có con thì tk cha sẽ là 6240
                    if (people.LastOrDefault().fileImportDetails.Count == 0)
                    {
                        people.LastOrDefault().TKNo = "6422";
                    }
                }
                //Kiểm tra lại lại mã với Định danh
                foreach(var item in people)
                {
                    //Lấy danh sách định danh
                    string querydinhdanh = @" SELECT *  FROM tbDinhdanhtaikhoan"; // Sử dụng ? thay cho @mst trong OleDb

                    result = ExecuteQuery(querydinhdanh, new OleDbParameter("?", ""));
                    if (item.TKNo =="0")
                    {
                        //Nếu có con
                        if (item.fileImportDetails.Count > 0)
                        {
                            foreach(DataRow row in result.Rows)
                            {
                                string name = Helper.ConvertUnicodeToVni((string)row["KeyValue"]);

                                if(item.fileImportDetails.Any(m=>m.Ten.Contains(name)))
                                {
                                    item.Noidung= row["Type"].ToString();
                                    item.TKNo = row["TKNo"].ToString();
                                    item.TKCo = int.Parse(row["TKCo"].ToString());
                                    item.TkThue = int.Parse(row["TKThue"].ToString());
                                }
                            }
                        }
                        else
                        {
                            item.TKCo = 1111;
                            item.TkThue = 1331;
                        }
                    }
                }
            }
            LoadExcel(savedPath);
            LoadDataGridview();
            CreatePageButtons();
        }
        int pageindex = 0;
        int pagelenght =60;
        int totalXMl = 0;
        private void CreatePageButtons()
        {
            panelPagination.Controls.Clear();
            int totalp = 0;
            totalp = totalXMl / pagelenght;
            int remain = totalXMl - totalp * pagelenght;
            if (remain > 0)
                totalp += 1;
            for (int i = 1; i <= totalp; i++)
            {
                Button btnPage = new Button
                {
                    Text = i.ToString(),
                    Height = 30,
                    Width = 40,
                    Tag = i // Gán số trang vào Tag để sử dụng sau
                };

                // Sự kiện Click cho nút trang
                btnPage.Click += BtnPage_Click;

                // Thêm nút vào panel
                panelPagination.Controls.Add(btnPage);
            }
            
        }

        private void BtnPage_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                pageindex = int.Parse(btn.Text) - 1;
                LoadXmlFiles(savedPath);

            }
        }
      

        public void LoadExcel(string filePath)
        {
            //       var excelFiles = Directory.EnumerateFiles(filePath, "*.xlsx", SearchOption.AllDirectories)
            //.Where(file => !file.Contains("HDChonLoc")).ToList();  // Loại trừ đường dẫn chứa "HDChonLoc"
            filePath = filePath + "\\HDDauVao";
            int fromMonth = int.Parse(cbbFrom.SelectedItem.ToString()); // Thay đổi theo tháng bắt đầu (ví dụ: 3 cho tháng 3)
            int toMonth = int.Parse(cbbTo.SelectedItem.ToString());   // Thay đổi theo tháng kết thúc (ví dụ: 7 cho tháng 7)

            // Lấy tất cả các file XML từ các thư mục tháng từ fromMonth đến toMonth
            var excelFiles = Directory.EnumerateFiles(filePath, "*.xlsx", SearchOption.AllDirectories)
                .Where(file => IsFileInMonthRange(file, filePath, fromMonth, toMonth)).ToList(); // Kiểm tra xem file có nằm trong khoảng tháng


            if (excelFiles.Count() == 0)
                return;
            // Kiểm tra xem file có tồn tại không
            for (int j = 0; j < excelFiles.Count; j++)
            {
                using (var workbook = new XLWorkbook(excelFiles[0]))
                {
                    // Lấy worksheet đầu tiên
                    var worksheet = workbook.Worksheet(1); // Hoặc bạn có thể dùng tên worksheet như worksheet = workbook.Worksheet("Sheet1");
                                                           // Lấy giá trị của ô A6
                    int rowCount = 0;
                    var currentCell = worksheet.Cell("A6"); // Bắt đầu từ ô A6

                    // Kiểm tra các ô bắt đầu từ A6 cho đến khi gặp ô trống
                    while (!currentCell.IsEmpty())
                    {
                        rowCount++; // Tăng số dòng
                        currentCell = currentCell.Worksheet.Row(currentCell.Address.RowNumber + 1).Cell("A"); // Chuyển xuống ô bên dưới

                    }
                    string SHDon = "";
                    string KHHDon = "";
                    string ten = "";
                    string mst = "";
                    string ten2 = "";
                    string mst2 = "";
                    string SoHD = "";
                    string diachi = "";
                    int TkCo = 0;
                    int TkNo = 0;
                    int TkThue = 0;
                    int Vat = 0;
                    double Thanhtien = 0;
                    DateTime NLap = new DateTime();
                    string diengiai = "";
                    int total = rowCount + 7;
                    for (int i = 7; i < (total - 1); i++)
                    {
                        mst = worksheet.Cell(i, 6).Value.ToString().Trim();
                        NLap = DateTime.Parse(worksheet.Cell(i, 5).Value.ToString().Trim());
                        ten = worksheet.Cell(i, 7).Value.ToString();
                        SHDon = worksheet.Cell(i, 4).Value.ToString().Trim();
                        KHHDon = worksheet.Cell(i, 3).Value.ToString();
                        string query = "SELECT * FROM HoaDon WHERE KyHieu = ? AND SoHD LIKE ?"; 

                        // Tạo mảng tham số với giá trị cho câu lệnh SQL
                        OleDbParameter[] parameters = new OleDbParameter[]
                        {
            new OleDbParameter("KyHieu", KHHDon),          // Sử dụng chỉ số mà không cần tên
            new OleDbParameter("SoHD", "%" + SHDon + "%")  // Thêm ký tự % cho LIKE
                        };
                        var kq = ExecuteQuery(query, parameters);
                        if (kq.Rows.Count > 0)
                        {
                            continue;
                        }
                        //Kiem tra trong tbimport
                        query = "SELECT * FROM tbimport WHERE KHHDon = ? AND SHDon LIKE ?";
                        parameters = new OleDbParameter[]
                      {
            new OleDbParameter("KHHDon", KHHDon),          // Sử dụng chỉ số mà không cần tên
            new OleDbParameter("SHDon", "%" + SHDon + "%")  // Thêm ký tự % cho LIKE
                      };
                        kq = ExecuteQuery(query, parameters);
                        if (kq.Rows.Count > 0)
                        {
                            continue;
                        }

                        double TienSauVAT = 0;
                        //Kiểm tra xem có phải trường hợp ko có thuế
                        if (worksheet.Cell(i, 9).Value.ToString() != "")
                        {
                            Thanhtien = double.Parse(worksheet.Cell(i, 9).Value.ToString().Replace(",", ""));
                            TienSauVAT = double.Parse(worksheet.Cell(i, 10).Value.ToString().Replace(",", ""));
                            Vat = int.Parse(Math.Round((TienSauVAT / Thanhtien * 100)).ToString());
                        }
                        else
                        {
                            Thanhtien = double.Parse(worksheet.Cell(i, 13).Value.ToString().Replace(",", ""));

                            Vat = 0;
                        }


                        //Kiểm tra thêm mới khách hàng
                         query = @" SELECT TOP 1 *  FROM KhachHang As kh
WHERE kh.MST = ?"; // Sử dụng ? thay cho @mst trong OleDb
                        DataTable result = ExecuteQuery(query, new OleDbParameter("?", mst));
                        if (result.Rows.Count == 0)
                        {
                            diachi = worksheet.Cell(i, 8).Value.ToString();
                            var Sohieu = GetLastFourDigits(mst);
                            ten = Helper.ConvertUnicodeToVni(ten);
                            diachi = Helper.ConvertUnicodeToVni(diachi);
                            //Kiểm tra sohieu có trùng nữa ko
                            query = @" SELECT TOP 1 *  FROM KhachHang As kh
WHERE kh.SoHieu = ?";
                            DataTable result2 = ExecuteQuery(query, new OleDbParameter("?", Sohieu));
                            if (result2.Rows.Count > 0)
                                Sohieu = "0" + Sohieu;
                            if (string.IsNullOrEmpty(diachi))
                            {
                                diachi = Helper.ConvertUnicodeToVni("Bô sung địa chỉ");
                            }
                            InitCustomer(2, Sohieu, ten, diachi, mst);
                        }

                        //Kiểm tra đã có hóa đơn trước đó chưa
                        query = @" SELECT TOP 1 *  FROM KhachHang AS kh  
INNER JOIN HoaDon AS hd ON kh.Maso = hd.MaKhachHang    
WHERE kh.MST = ?  
ORDER BY hd.MaSo DESC"; // Sử dụng ? thay cho @mst trong OleDb
                        result = ExecuteQuery(query, new OleDbParameter("?", mst));
                        if (result.Rows.Count > 0)
                        {
                            SoHD = result.Rows[0]["SoHD"].ToString();

                            query = @"Select top 2 * from ChungTu 
where SoHieu = ?
ORDER BY  MaSo DESC";
                            result = ExecuteQuery(query, new OleDbParameter("?", SoHD));
                            var index = 0;
                            if (result.Rows.Count > 0)
                            {
                                foreach (DataRow row in result.Rows)
                                {
                                    if (index == 0)
                                    {
                                        TkThue = int.Parse(row["MaTKNo"].ToString());  // Giả sử có cột "MaSo"; 
                                    }
                                    if (index == 1)
                                    {
                                        TkNo = int.Parse(row["MaTKNo"].ToString());  // Giả sử có cột "MaSo"; 
                                        TkCo = int.Parse(row["MaTKCo"].ToString());  // Giả sử có cột "MaSo"; 
                                        diengiai = Helper.ConvertVniToUnicode(row["DienGiai"].ToString());
                                    }
                                    // Lấy giá trị từ cột cụ thể trong hàng hiện tại

                                    index += 1;
                                }
                            }

                            // Tra cứu từ bảng HeThongTK
                            query = @"Select   * from HeThongTK where MaTC = ?";
                            result = ExecuteQuery(query, new OleDbParameter("?", TkNo));
                            if (result.Rows.Count > 0)
                            {
                                TkNo = int.Parse(result.Rows[0]["SoHieu"].ToString());

                                query = @"Select   * from HeThongTK where MaTC = ?";
                                result = ExecuteQuery(query, new OleDbParameter("?", TkCo));
                                TkCo = int.Parse(result.Rows[0]["SoHieu"].ToString());

                                query = @"Select   * from HeThongTK where MaTC = ?";
                                result = ExecuteQuery(query, new OleDbParameter("?", TkThue));
                                TkThue = int.Parse(result.Rows[0]["SoHieu"].ToString());
                            }

                        }
                        else
                        {
                            TkNo = 6422;
                            TkCo = 1111;
                            TkThue = 1331;
                        }

                        if (!people.Any(m => m.SHDon.Contains(SHDon) && m.KHHDon == KHHDon))
                        {
                            people.Add(new FileImport(excelFiles[j], SHDon, KHHDon, NLap, ten, diengiai, TkNo.ToString(), TkCo, TkThue, mst, Thanhtien, Vat, 2, ""));
                        }

                    }

                }
            }

        }
        public static string GetLastFourDigits(string input)
        {
            // Tìm vị trí của dấu '-'
            int dashIndex = input.IndexOf('-');

            // Nếu có dấu '-' trong chuỗi, lấy phần trước đó
            if (dashIndex != -1)
            {
                input = input.Substring(0, dashIndex);
            }

            // Lấy 4 ký tự cuối cùng
            if (input.Length >= 4)
            {
                return input.Substring(input.Length - 4);
            }
            else
            {
                return input; // Trả về toàn bộ chuỗi nếu độ dài nhỏ hơn 4
            }
        }
        public void InitCustomer(int Maphanloai, string Sohieu, string Ten, string Diachi, string Mst)
        {
            string query = @"
        INSERT INTO KhachHang (MaPhanLoai,SoHieu,Ten,DiaChi,MST)
        VALUES (?,?,?,?,?)";


            // Khai báo mảng tham số với đủ 10 tham số
            OleDbParameter[] parameters = new OleDbParameter[]
            {
        new OleDbParameter("?", Maphanloai),
          new OleDbParameter("?", Sohieu),
        new OleDbParameter("?", Ten),
        new OleDbParameter("?", Diachi),
        new OleDbParameter("?", Mst)
            };

            // Thực thi truy vấn và lấy kết quả
            int a = ExecuteQueryResult(query, parameters);
        }
        private void LoadDataGridview()
        {

            bindingSource.DataSource = people;
            dataGridView1.DataSource = bindingSource;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Visible = false;
            }



            dataGridView1.Columns["ID"].Visible = true;
            dataGridView1.Columns["ID"].HeaderText = "STT";
            dataGridView1.Columns["ID"].Width = 70;
            dataGridView1.Columns["ID"].DisplayIndex = 0;
            dataGridView1.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["ID"].ReadOnly = true;



            dataGridView1.Columns["NLap"].Visible = true;
            dataGridView1.Columns["NLap"].HeaderText = "Ngày";
            dataGridView1.Columns["NLap"].Width = 70;
            dataGridView1.Columns["NLap"].DisplayIndex = 1;
            dataGridView1.Columns["NLap"].ReadOnly = true;

            dataGridView1.Columns["SHDon"].Visible = true;
            dataGridView1.Columns["SHDon"].HeaderText = "Số hóa đơn";
            dataGridView1.Columns["SHDon"].Width = 90;
            dataGridView1.Columns["SHDon"].DisplayIndex = 2;
            dataGridView1.Columns["SHDon"].ReadOnly = true;

            dataGridView1.Columns["Ten"].Visible = true;
            dataGridView1.Columns["Ten"].HeaderText = "Tên Cty";
            dataGridView1.Columns["Ten"].Width = (int)(dataGridView1.Width * 0.35);
            dataGridView1.Columns["Ten"].DisplayIndex = 3;
            dataGridView1.Columns["Ten"].ReadOnly = true;


            dataGridView1.Columns["TongTien"].Visible = true;
            dataGridView1.Columns["TongTien"].HeaderText = "Tổng số tiền";
            dataGridView1.Columns["TongTien"].Width = 100;
            dataGridView1.Columns["TongTien"].DisplayIndex = 4;
            dataGridView1.Columns["TongTien"].ReadOnly = true;

            dataGridView1.Columns["TKNo"].Visible = true;
            dataGridView1.Columns["TKNo"].HeaderText = "Nợ";
            dataGridView1.Columns["TKNo"].Width = 50;
            dataGridView1.Columns["TKNo"].DisplayIndex = 5;


            dataGridView1.Columns["TKCo"].Visible = true;
            dataGridView1.Columns["TKCo"].HeaderText = "Có";
            dataGridView1.Columns["TKCo"].Width = 50;
            dataGridView1.Columns["TKCo"].DisplayIndex = 7;



            dataGridView1.Columns["Noidung"].Visible = true;
            dataGridView1.Columns["Noidung"].HeaderText = "Ghi chú";
            dataGridView1.Columns["Noidung"].Width = (int)(dataGridView1.Width * 0.25);
            dataGridView1.Columns["Noidung"].DisplayIndex = 8;
            dataGridView1.Columns["Checked"].Visible = true;

            dataGridView1.Columns["Checked"].HeaderText = "Chọn";
            dataGridView1.Columns["Checked"].Width = 100;
            dataGridView1.Columns["Checked"].DisplayIndex = 9;
            dataGridView1.Columns["Checked"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Checked"].ReadOnly = false;
            dataGridView1.RowHeadersVisible = false;

        }
        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadXmlFiles(savedPath);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            bool isNull = false;
            foreach (var item in people)
            {
                if (!item.Checked)
                {
                    continue;
                }
                if(item.TKCo==0 || string.IsNullOrEmpty(item.Noidung))
                {
                    isNull = true; 
                    MessageBox.Show("thông tin ko duoc de trống");
                    break;
                }
                //Xử lý 154 cho notk
                if (item.TKNo.Contains('|'))
                {
                    var getsplits = item.TKNo.Split('|');
                    item.TKNo = getsplits[0].Trim();
                    item.SoHieuTP = getsplits[1].Trim();
                }
                if (item.Type == 3)
                    continue;
                if (item.TkThue == 0)
                {
                    if (item.TKNo == "6422" || item.TKNo == "6421")
                        item.TkThue = 1331;
                    if (item.TKNo == "152")
                        item.TkThue = 1331;
                    if (item.TKNo == "5111" || item.TKNo == "5112" || item.TKNo == "5113")
                        item.TkThue = 33311;
                }
                // Câu truy vấn SQL với các tham số được đặt tên rõ ràng
                string query = @"
        INSERT INTO tbImport (SHDon,KHHDon, NLap, Ten, Noidung, TKCo, TKNo, TkThue, Mst, Status, Ngaytao,TongTien,Vat,SohieuTP)
        VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

                // Chuyển đổi Unicode sang VNI (nếu cần)
                string newTen = Helper.ConvertUnicodeToVni(item.Ten);
                string newNoidung = Helper.ConvertUnicodeToVni(item.Noidung);
                //Tìm id TP

                // Khai báo mảng tham số với đủ 10 tham số
                OleDbParameter[] parameters = new OleDbParameter[]
                {
        new OleDbParameter("?", item.SHDon),
          new OleDbParameter("?", item.KHHDon),
        new OleDbParameter("?", item.NLap),
        new OleDbParameter("?", newTen),
        new OleDbParameter("?", newNoidung),
        new OleDbParameter("?", item.TKCo),
        new OleDbParameter("?", item.TKNo),
        new OleDbParameter("?", item.TkThue),
        new OleDbParameter("?", item.Mst),
        new OleDbParameter("?","0"),
        new OleDbParameter("?",DateTime.Now.ToShortDateString()),
         new OleDbParameter("?",item.TongTien.ToString()),
          new OleDbParameter("?",item.Vat.ToString()),
            new OleDbParameter("?",item.SoHieuTP.ToString())
                };

                // Thực thi truy vấn và lấy kết quả
                int a = ExecuteQueryResult(query, parameters);

                // Kiểm tra kết quả
                if (a > 0)
                {
                    if (item.TKNo != "6422" && item.TKNo != "64221" && item.TKNo != "154")
                    {
                        string tableName = "tbImport";

                        query = $"SELECT MAX(ID) FROM {tableName}";

                        int parentID = (int)ExecuteQuery(query, new OleDbParameter("?", null)).Rows[0][0];
                        foreach (var it in item.fileImportDetails)
                        {
                            query = @"
        INSERT INTO tbimportdetail (ParentId,SoHieu, SoLuong, DonGia,DVT,Ten)
        VALUES (?,?,?,?,?,?)";
                            parameters = new OleDbParameter[]
                        {
        new OleDbParameter("?", parentID),
          new OleDbParameter("?", it.SoHieu),
        new OleDbParameter("?", it.Soluong),
        new OleDbParameter("?", it.Dongia),
        new OleDbParameter("?", it.DVT),
        new OleDbParameter("?", it.Ten)
                        };
                            int resl = ExecuteQueryResult(query, parameters);

                        }
                    }


                }
                else
                {
                    Console.WriteLine("Thêm dữ liệu thất bại.");
                }
                //Di chuyển file
                try
                {
                    var htmlPath = savedPath + "\\HDDauVao";
                    var month = "\\" + item.NLap.Month;
                    htmlPath += month;
                    var htmlFiles = Directory.EnumerateFiles(htmlPath, "*.html", SearchOption.AllDirectories);
                    foreach(var it in htmlFiles)
                    {
                        File.Delete(it);
                    }

                    try
                    {
                        File.Move(item.Path, item.Path.Replace("HDDauVao", "HDChonLoc"));
                    }
                    catch(Exception ex)
                    {
                        File.Delete(item.Path);
                    }
                }
                catch (Exception ex)
                {
                    var ass = ex.Message.ToString();
                }

            }
            //Tìm file html và xóa


            //Di chuyển file excel
            //var newfapth = savedPath + "\\HDDauVao";
            //var excelFiles = Directory.EnumerateFiles(newfapth, "*.xlsx", SearchOption.AllDirectories);
            //foreach (var item in excelFiles)
            //{
            //    try
            //    {
            //        var replatepath = item.Replace("HDDauVao", "HDChonLoc");
            //        File.Move(item, replatepath);
            //    }
            //    catch (Exception ex)
            //    {

            //    }

            //}
            if (isNull == false)
            {
                MessageBox.Show("Lấy dữ liệu thành công");
                isClick = true;
                this.Close();
            }
           
        }

        #endregion

        #region Database Excute, query
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
        private int ExecuteQueryResult(string query, params OleDbParameter[] parameters)
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

                    int rowsAffected = command.ExecuteNonQuery(); // Thực thi câu lệnh
                    return rowsAffected;
                }
            }

            return -1;
        }
        #endregion
        #region Another Function
        public static string GenerateResultString(string input)
        {
            // Tìm từ đầu tiên (không cần loại bỏ dấu toàn bộ)
            string firstWord = input.Split(' ')[0];

            // Loại bỏ dấu tiếng Việt cho từ đầu tiên
            string normalizedFirstWord = RemoveVietnameseDiacritics(firstWord).Replace("á", "a");

            // Tạo 4 số ngẫu nhiên từ 1 đến 9
            string randomNumbers = GenerateRandomNumbers(4);

            // Kết hợp từ đầu tiên với 4 số ngẫu nhiên
            return normalizedFirstWord + randomNumbers;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "TongTien")
            {
                if (e.Value != null)
                {
                    // Định dạng số
                    e.Value = string.Format(CultureInfo.InvariantCulture, "{0:N0}", e.Value);
                    e.FormattingApplied = true; // Đánh dấu rằng giá trị đã được định dạng
                }
            }
            //
            if (dataGridView1.Columns[e.ColumnIndex].Name == "TKNo")
            {
                e.CellStyle.ForeColor = System.Drawing.Color.DarkSlateBlue; // Tô màu đỏ cho chữ
                e.CellStyle.Font = new System.Drawing.Font(e.CellStyle.Font, FontStyle.Bold);
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "TKCo")
            {
                e.CellStyle.ForeColor = System.Drawing.Color.Green; // Tô màu đỏ cho chữ
                e.CellStyle.Font = new System.Drawing.Font(e.CellStyle.Font, FontStyle.Bold);
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Type")
            {
                if (e.Value.ToString() == "1")
                {
                    e.Value = "XML";
                    e.CellStyle.ForeColor = System.Drawing.Color.White;
                    e.CellStyle.BackColor = System.Drawing.Color.OrangeRed; // Tô màu đỏ cho chữ
                    e.CellStyle.Font = new System.Drawing.Font(e.CellStyle.Font, FontStyle.Bold);
                }
                if (e.Value.ToString() == "2")
                {
                    e.Value = "Excel";
                    e.CellStyle.ForeColor = System.Drawing.Color.White;
                    e.CellStyle.BackColor = System.Drawing.Color.Green; // Tô màu đỏ cho chữ
                    e.CellStyle.Font = new System.Drawing.Font(e.CellStyle.Font, FontStyle.Bold);
                }
                if (e.Value.ToString() == "3")
                {
                    e.Value = "Database";
                    e.CellStyle.BackColor = System.Drawing.Color.Yellow; // Tô màu đỏ cho chữ
                    e.CellStyle.Font = new System.Drawing.Font(e.CellStyle.Font, FontStyle.Bold);
                }
            }
        }
        public static ChromeDriver Driver { get; private set; }
        private void btnDownloadFromCQT_Click(object sender, EventArgs e)
        {
            if (Driver == null)
            {
                var options = new ChromeOptions();
                options.AddArguments(
                    "--disable-notifications",
                    "--start-maximized",
                    "--disable-extensions",
                    "--disable-infobars");
                //
                string downloadPath = @"C:\S.T.E 25\S.T.E 25\Hoadon\HDDauVao";
                options.AddUserProfilePreference("download.default_directory", downloadPath);
                options.AddUserProfilePreference("download.prompt_for_download", false);
                options.AddUserProfilePreference("disable-popup-blocking", "true");

                var driverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                Driver = new ChromeDriver(driverPath, options);

                //
                try
                {
                    Driver.Navigate().GoToUrl("https://hoadondientu.gdt.gov.vn");
                    IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                    js.ExecuteScript("window.scrollTo(0, 0);");
                    Thread.Sleep(1000);
                    var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
                    var closeButton = wait.Until(driver => driver.FindElement(By.XPath("//span[@class='ant-modal-close-x']")));
                    closeButton.Click();
                    //
                    var loginButton = wait.Until(driver => driver.FindElement(By.XPath("//div[@class='ant-col home-header-menu-item']/span[text()='Đăng nhập']")));
                    loginButton.Click();
                    // Nhập tên đăng nhập, mật khẩu và captcha
                    var usernameField = Driver.FindElement(By.Id("username"));
                    var passwordField = Driver.FindElement(By.Id("password"));
                    //usernameField.SendKeys("3502501171"); // Thay your_username bằng tên đăng nhập thực tế
                    //passwordField.SendKeys("PDVT12345678aA@");
                    usernameField.SendKeys("3501197169"); // Thay your_username bằng tên đăng nhập thực tế
                    passwordField.SendKeys("QLGd1aA@");
                    new Actions(Driver)
     .KeyDown(Keys.Tab).KeyUp(Keys.Tab)  // Tab lần 1
     .Pause(TimeSpan.FromMilliseconds(100))  // Đợi ngắn
     .KeyDown(Keys.Tab).KeyUp(Keys.Tab)  // Tab lần 2
     .Perform();
                    wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(200));
                    //chờ khi nao dang nhap xong
                    //                var button = wait.Until(d =>
                    //d.FindElement(By.CssSelector("button.ant-btn-icon-only i[aria-label='icon: user']"))
                    // .FindElement(By.XPath("./parent::button")));
                    wait.Until(d =>
                    d.FindElements(By.XPath("//div[contains(@class,'home-header-menu-item')]//span[text()='Đăng nhập']")).Count == 0);
                    DoTask = int.Parse(cbbFrom.SelectedItem.ToString());
                    Endtask = int.Parse(cbbTo.SelectedItem.ToString());

                    Xulysaudangnhap();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}");
                }
            }
        }
        int DoTask = 0;
        int Endtask = 0;
        public void Chonngaytheothang()
        {

        }
        private void Cucthuekhngnhanma(WebDriverWait wait)
        {
            var divElement = wait.Until(d => d.FindElements(By.Id("ttxly")));
            // Nhấp vào phần tử đó
            divElement[1].Click();
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
            var listItem = wait.Until(d => d.FindElements(By.XPath("//li[@role='option' and @class='ant-select-dropdown-menu-item']"))
            .FirstOrDefault(e => e.Text.Trim() == "Cục Thuế đã nhận không mã"));

            // Kiểm tra nếu phần tử được tìm thấy và nhấp vào nó
            if (listItem != null)
            {
                listItem.Click();
            }
            else
            {
                Console.WriteLine("Không tìm thấy phần tử với văn bản cụ thể.");
            }
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
            var tabElement = wait.Until(d => d.FindElements(By.XPath("//div[@role='tab']"))
              .FirstOrDefault(e => e.Text.Trim() == "Hóa đơn điện tử"));
            if (tabElement != null)
            {
                tabElement.Click();
                Console.WriteLine("Đã nhấp vào tab.");
            }
            var button = wait.Until(d => d.FindElement(By.XPath("(//button[contains(@class, 'ant-btn') and .//span[text()='Tìm kiếm']])[2]")));
            button.Click();
            //
            Thread.Sleep(2000);

            button = wait.Until(d =>
                d.FindElement(By.XPath("(//button[contains(@class, 'ant-btn-icon-only')])[18]")));


            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth'});", button);

            // Hover rồi mới click
            new Actions(Driver)
                .MoveToElement(button)
                .Pause(TimeSpan.FromSeconds(1))
                .Click()
                .Perform();

            //Tải file excel
        }
        //private void Cucthuekhngnhanma2(WebDriverWait wait)
        //{
        //    var divElement = wait.Until(d => d.FindElements(By.Id("ttxly")));
        //    // Nhấp vào phần tử đó
        //    divElement[1].Click();
        //    wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
        //    var listItem = wait.Until(d => d.FindElements(By.XPath("//li[@role='option' and @class='ant-select-dropdown-menu-item']"))
        //    .FirstOrDefault(e => e.Text.Trim() == "Cục Thuế đã nhận không mã"));

        //    // Kiểm tra nếu phần tử được tìm thấy và nhấp vào nó
        //    if (listItem != null)
        //    {
        //        listItem.Click(); 
        //    }
        //    else
        //    {
        //        Console.WriteLine("Không tìm thấy phần tử với văn bản cụ thể.");
        //    }
        //    wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
        //    var tabElement = wait.Until(d => d.FindElements(By.XPath("//div[@role='tab']"))
        //      .FirstOrDefault(e => e.Text.Trim() == "Hóa đơn điện tử"));
        //    if (tabElement != null)
        //    {
        //        tabElement.Click();
        //        Console.WriteLine("Đã nhấp vào tab.");
        //    }
        //    var button = wait.Until(d => d.FindElement(By.XPath("(//button[contains(@class, 'ant-btn') and .//span[text()='Tìm kiếm']])[2]")));
        //    button.Click();
        //    //
        //    Thread.Sleep(2000);
        //    wait.Until(d => d.FindElements(By.CssSelector("tr.ant-table-row")).Count > 0);

        //    IReadOnlyCollection<IWebElement> rows = Driver.FindElements(By.CssSelector("tr.ant-table-row"));
        //    int rowCount = rows.Count;

        //    Console.WriteLine($"Số dòng trong bảng: {rowCount}");


        //    int currentRow = 1;
        //    bool hasMoreRows = true;
        //    List<string> lstHas = new List<string>();
        //    int hasdata = 0;
        //    while ((currentRow) <= rowCount)
        //    {
        //        try
        //        {
        //            // Tìm dòng hiện tại
        //            var row = wait.Until(d =>
        //                d.FindElement(By.XPath($"(//tbody[@class='ant-table-tbody']/tr[contains(@class,'ant-table-row')])[{currentRow}]")));
        //            var cellC25TYY = row.FindElement(By.XPath("./td[3]/span")).Text; // C25TYY
        //            var cell22252 = row.FindElement(By.XPath("./td[4]")).Text; // 22252

        //            string query = "SELECT * FROM HoaDon WHERE KyHieu = ? AND SoHD LIKE ?";


        //            // Tạo mảng tham số với giá trị cho câu lệnh SQL
        //            OleDbParameter[] parameters = new OleDbParameter[]
        //            {
        //    new OleDbParameter("KyHieu", cellC25TYY),          // Sử dụng chỉ số mà không cần tên
        //    new OleDbParameter("SoHD", "%" + cell22252 + "%")  // Thêm ký tự % cho LIKE
        //            };
        //            var kq = ExecuteQuery(query, parameters);
        //            var a = people;
        //            var check = a.Any(m => m.KHHDon == cellC25TYY && m.SHDon.Contains(cell22252));
        //            if (check || kq.Rows.Count != 0)
        //            {
        //                //Xóa Excel
        //                var pp1 = "C:\\S.T.E 25\\S.T.E 25\\Hoadon\\HDDauVao";
        //                var pp21 = "C:\\S.T.E 25\\S.T.E 25\\Hoadon\\HDDauVao\\" + DoTask;
        //                // Lấy tất cả các file XML từ các thư mục tháng từ fromMonth đến toMonth
        //                string[] excelFiles1 = Directory.GetFiles(pp1, "*.xlsx");
        //                string fileName1 = Path.GetFileName(excelFiles1[0]); // Lấy tên file
        //                string destFilePath1 = Path.Combine(pp21, fileName1); // Tạo đường dẫn đích

        //                // Di chuyển file
        //                try
        //                {
        //                    File.Move(excelFiles1[0], destFilePath1);
        //                }
        //                catch (Exception ex)
        //                {
        //                    File.Delete(excelFiles1[0]);
        //                }
        //                currentRow++;
        //                hasdata++;
        //                continue;
        //            }

        //            // Click vào dòng
        //            row.Click();
        //            button = wait.Until(d =>
        //             d.FindElement(By.XPath("(//button[contains(@class, 'ant-btn-icon-only')])[19]")));
        //            button.Click();
        //            // Xử lý sau khi click (đợi tải, đóng popup,...)
        //            Thread.Sleep(50); // Đợi 1 giây giữa các lần click
        //            string fp = "";
        //            if (currentRow == 15)
        //            {
        //                int aas = 10;
        //            }
        //            if (currentRow == 1)
        //                fp = "C:\\S.T.E 25\\S.T.E 25\\Hoadon\\\\HDDauVao\\" + "invoice.zip";
        //            else
        //                fp = "C:\\S.T.E 25\\S.T.E 25\\Hoadon\\\\HDDauVao\\" + "invoice (" + (currentRow - 1 - hasdata) + ").zip";

        //            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
        //            wait.Until(d => File.Exists(fp));
        //            lstHas.Add(fp);
        //            currentRow++; // Chuyển sang dòng tiếp theo
        //        }
        //        catch (NoSuchElementException)
        //        {
        //            hasMoreRows = false; // Không còn dòng nào nữa


        //            Console.WriteLine($"Đã xử lý hết {currentRow - 1} dòng");
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Lỗi khi xử lý dòng {currentRow}: {ex.Message}");
        //            currentRow++; // Vẫn tiếp tục với dòng tiếp theo
        //        }
        //    }
        //    if (lstHas.Count == 0)
        //        return;
        //    var getlastlist = lstHas.LastOrDefault();
        //    wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(120));
        //    wait.Until(d => File.Exists(getlastlist));

        //    var pp = "C:\\S.T.E 25\\S.T.E 25\\Hoadon\\HDDauVao";
        //    var pp2 = "C:\\S.T.E 25\\S.T.E 25\\Hoadon\\HDDauVao\\" + DoTask;
        //    // Lấy tất cả các file XML từ các thư mục tháng từ fromMonth đến toMonth
        //    string[] excelFiles = Directory.GetFiles(pp, "*.xlsx");
        //    if (excelFiles.Length > 0)
        //    {
        //        string fileName = Path.GetFileName(excelFiles[0]); // Lấy tên file
        //        string destFilePath = Path.Combine(pp2, fileName); // Tạo đường dẫn đích
        //        try
        //        {
        //            File.Move(excelFiles[0], destFilePath);
        //        }
        //        catch (Exception ex)
        //        {
        //            File.Delete(excelFiles[0]);
        //        }
        //    }

        //    // Di chuyển file


        //    GiaiNenhoadon();
        //}
        private void Xulymaytinhtien(WebDriverWait wait)
        {
            //Xử lý hóa đơn từ máy tính tiền
            //By.Id("ttxly")
            var divElement = wait.Until(d => d.FindElements(By.Id("ttxly")));
            // Nhấp vào phần tử đó
            divElement[1].Click();
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
            var listItem = wait.Until(d => d.FindElements(By.XPath("//li[@role='option' and @class='ant-select-dropdown-menu-item']"))
            .FirstOrDefault(e => e.Text.Trim() == "Cục Thuế đã nhận hóa đơn có mã khởi tạo từ máy tính tiền"));

            // Kiểm tra nếu phần tử được tìm thấy và nhấp vào nó
            if (listItem != null)
            {
                listItem.Click();
                Console.WriteLine("Đã nhấp vào phần tử.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy phần tử với văn bản cụ thể.");
            }
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
            // Tìm input với class 'ant-calendar-input' và placeholder 'Chọn thời điểm'
//            var allInputs = Driver.FindElements(By.CssSelector("input.ant-calendar-picker-input"));
//            Thread.Sleep(1000);
//            allInputs[2].Click();
//            IWebElement monthSelect = Driver.FindElement(
//By.CssSelector("a.ant-calendar-month-select[title='Chọn tháng']"));
//            monthSelect.Click();
//            IWebElement monthItem = Driver.FindElement(
//By.XPath("//a[contains(@class,'ant-calendar-month-panel-month') and text()='Thg 0" + DoTask.ToString() + "']"));
//            monthItem.Click();

//            //
//            var elements = Driver.FindElements(By.CssSelector("div.ant-calendar-date"));

//            // Lọc các phần tử có text là "1"
//            var targetElement = elements.FirstOrDefault(div => div.Text.Trim() == "1");
//            targetElement.Click();
//            new Actions(Driver)
//.KeyDown(Keys.Enter) // Tab lần 2
//.Perform();
            
            //Tìm tab tính tiền
            var tabElement = wait.Until(d => d.FindElements(By.XPath("//div[@role='tab']"))
               .FirstOrDefault(e => e.Text.Trim() == "Hóa đơn có mã khởi tạo từ máy tính tiền"));
            if (tabElement != null)
            {
                tabElement.Click();
                Console.WriteLine("Đã nhấp vào tab.");
            }
            var button = wait.Until(d => d.FindElement(By.XPath("(//button[contains(@class, 'ant-btn') and .//span[text()='Tìm kiếm']])[2]")));
            button.Click();
            //
            wait.Until(d => d.FindElements(By.CssSelector("tr.ant-table-row")).Count > 0);
            Thread.Sleep(1000);
            IReadOnlyCollection<IWebElement> rows = Driver.FindElements(By.CssSelector("tr.ant-table-row"));
            int rowCount = rows.Count;

            Console.WriteLine($"Số dòng trong bảng: {rowCount}");

            //button = wait.Until(d =>
            //    d.FindElement(By.XPath("(//button[contains(@class, 'ant-btn-icon-only')])[18]")));


            //((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth'});", button);

            //// Hover rồi mới click
            //new Actions(Driver)
            //    .MoveToElement(button)
            //    .Pause(TimeSpan.FromSeconds(1))
            //    .Click()
            //    .Perform();

            int currentRow = 1;
            bool hasMoreRows = true;
            List<string> lstHas = new List<string>();
            int hasdata = 0;
            while ((currentRow) <= rowCount)
            {
                try
                {
                    // Tìm dòng hiện tại
                    var row = wait.Until(d =>
                        d.FindElement(By.XPath($"(//tbody[@class='ant-table-tbody']/tr[contains(@class,'ant-table-row')])[{currentRow}]")));
                    var cellC25TYY = row.FindElement(By.XPath("./td[3]/span")).Text; // C25TYY
                    var cell22252 = row.FindElement(By.XPath("./td[4]")).Text; // 22252

                    string query = "SELECT * FROM HoaDon WHERE KyHieu = ? AND SoHD LIKE ?";


                    // Tạo mảng tham số với giá trị cho câu lệnh SQL
                    OleDbParameter[] parameters = new OleDbParameter[]
                    {
            new OleDbParameter("KyHieu", cellC25TYY),          // Sử dụng chỉ số mà không cần tên
            new OleDbParameter("SoHD", "%" + cell22252 + "%")  // Thêm ký tự % cho LIKE
                    };
                    var kq = ExecuteQuery(query, parameters);
                    var a = people;
                    var check = a.Any(m => m.KHHDon == cellC25TYY && m.SHDon.Contains(cell22252));
                    if (check || kq.Rows.Count != 0)
                    {
                        //Xóa Excel
                        var pp1 = "C:\\S.T.E 25\\S.T.E 25\\Hoadon\\HDDauVao";
                        var pp21 = "C:\\S.T.E 25\\S.T.E 25\\Hoadon\\HDDauVao\\" + DoTask;
                        // Lấy tất cả các file XML từ các thư mục tháng từ fromMonth đến toMonth
                        string[] excelFiles1 = Directory.GetFiles(pp1, "*.xlsx");
                        string fileName1 = Path.GetFileName(excelFiles1[0]); // Lấy tên file
                        string destFilePath1 = Path.Combine(pp21, fileName1); // Tạo đường dẫn đích

                        // Di chuyển file
                        try
                        {
                            File.Move(excelFiles1[0], destFilePath1);
                        }
                        catch (Exception ex)
                        {
                            File.Delete(excelFiles1[0]);
                        }
                        currentRow++;
                        hasdata++;
                        continue;
                    }

                    // Click vào dòng
                    row.Click();
                    button = wait.Until(d =>
                     d.FindElement(By.XPath("(//button[contains(@class, 'ant-btn-icon-only')])[19]")));
                    button.Click();
                    // Xử lý sau khi click (đợi tải, đóng popup,...)
                    Thread.Sleep(50); // Đợi 1 giây giữa các lần click
                    string fp = "";
                    if (currentRow == 15)
                    {
                        int aas = 10;
                    }
                    if (currentRow == 1)
                        fp = "C:\\S.T.E 25\\S.T.E 25\\Hoadon\\\\HDDauVao\\" + "invoice.zip";
                    else
                        fp = "C:\\S.T.E 25\\S.T.E 25\\Hoadon\\\\HDDauVao\\" + "invoice (" + (currentRow - 1 - hasdata) + ").zip";

                    wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(120));
                    wait.Until(d => File.Exists(fp));
                    lstHas.Add(fp);
                    currentRow++; // Chuyển sang dòng tiếp theo
                }
                catch (NoSuchElementException)
                {
                    hasMoreRows = false; // Không còn dòng nào nữa


                    Console.WriteLine($"Đã xử lý hết {currentRow - 1} dòng");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi xử lý dòng {currentRow}: {ex.Message}");
                    currentRow++; // Vẫn tiếp tục với dòng tiếp theo
                }
            }
            if (lstHas.Count == 0)
                return;
            var getlastlist = lstHas.LastOrDefault();
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(120));
            wait.Until(d => File.Exists(getlastlist));
          
            var pp = "C:\\S.T.E 25\\S.T.E 25\\Hoadon\\HDDauVao";
            var pp2 = "C:\\S.T.E 25\\S.T.E 25\\Hoadon\\HDDauVao\\" + DoTask;
            // Lấy tất cả các file XML từ các thư mục tháng từ fromMonth đến toMonth
            string[] excelFiles = Directory.GetFiles(pp, "*.xlsx");
            if (excelFiles.Length > 0)
            {
                string fileName = Path.GetFileName(excelFiles[0]); // Lấy tên file
                string destFilePath = Path.Combine(pp2, fileName); // Tạo đường dẫn đích
                try
                {
                    File.Move(excelFiles[0], destFilePath);
                }
                catch (Exception ex)
                {
                    File.Delete(excelFiles[0]);
                }
            }

            // Di chuyển file


            GiaiNenhoadon();
            //  LoadXmlFiles(savedPath);
         
            //End Xử lý hóa đơn từ máy tính tiền
        }
        private void Xulysaudangnhap()
        {
            if (DoTask > Endtask)
            {
                Driver.Quit(); // Đóng WebDriver
                return;
            }
            Thread.Sleep(1000);
            if (Driver == null)
            {
                MessageBox.Show("Vui lòng mở trình duyệt trước!");
                return;
            }

            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));


                string targetUrl = "https://hoadondientu.gdt.gov.vn/tra-cuu/tra-cuu-hoa-don";

                // Cách 1: Chuyển trang đơn giản
                Driver.Navigate().GoToUrl(targetUrl);
                Thread.Sleep(2000);
                var tab = wait.Until(d => d.FindElement(
                    By.XPath("//div[@role='tab' and .//span[contains(text(),'Tra cứu hóa đơn điện tử mua vào')]]")
                ));
                tab.Click();

            
                Thread.Sleep(1000);
                wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));

                Thread.Sleep(1000);

                wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
                // Tìm input với class 'ant-calendar-input' và placeholder 'Chọn thời điểm'
                var allInputs = Driver.FindElements(By.CssSelector("input.ant-calendar-picker-input"));
                Thread.Sleep(1000);
                allInputs[2].Click();
                IWebElement monthSelect = Driver.FindElement(
By.CssSelector("a.ant-calendar-month-select[title='Chọn tháng']"));
                monthSelect.Click();
                IWebElement monthItem = Driver.FindElement(
By.XPath("//a[contains(@class,'ant-calendar-month-panel-month') and text()='Thg 0" + DoTask.ToString() + "']"));
                monthItem.Click();

                //
                var elements = Driver.FindElements(By.CssSelector("div.ant-calendar-date"));

                // Lọc các phần tử có text là "1"
                var targetElement = elements.FirstOrDefault(div => div.Text.Trim() == "1");
                targetElement.Click();
                new Actions(Driver)
.KeyDown(Keys.Enter) // Tab lần 2
.Perform();
                var button = wait.Until(d => d.FindElement(By.XPath("(//button[contains(@class, 'ant-btn') and .//span[text()='Tìm kiếm']])[2]")));
                button.Click();

                //Chọn 50 rows
                wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
                var divElement = wait.Until(d => d.FindElements(By.XPath("//div[@class='ant-select-selection-selected-value' and @title='15']")));

                // Kiểm tra nếu phần tử được tìm thấy và nhấp vào nó
                if (divElement != null && divElement[1].Displayed)
                {
                    divElement[1].Click();
                    Console.WriteLine("Đã nhấp vào phần tử.");
                }
                var dropdownMenu = wait.Until(d => d.FindElement(By.ClassName("ant-select-dropdown-menu")));

                // Tìm phần tử <li> có nội dung là "50" và nhấp vào nó
                var option50 = wait.Until(d => dropdownMenu.FindElements(By.XPath(".//li[text()='50']")));

                // Nhấp vào phần tử "50"
                if (option50 != null )
                {
                    option50[0].Click(); 
                }
                //Click download XML   
                Thread.Sleep(1000);


                // Cách 1: Target vào thẻ <i> có aria-label
                //   d.FindElement(By.CssSelector("button.ant-btn-icon-only i[aria-label='icon: user']")

                bool isPhantrang = false;
                while (isPhantrang == false)
                {
                    wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));

                    // Đợi cho đến khi có ít nhất 1 dòng xuất hiện
                    wait.Until(d => d.FindElements(By.CssSelector("tr.ant-table-row")).Count > 0);
                    IReadOnlyCollection<IWebElement> rows = Driver.FindElements(By.CssSelector("tr.ant-table-row"));
                    int rowCount = rows.Count;

                    Console.WriteLine($"Số dòng trong bảng: {rowCount}");


                    int currentRow = 1;
                    bool hasMoreRows = true;
                    List<string> lstHas = new List<string>();
                    int hasdata = 0;
                    while ((currentRow) <= rowCount)
                    {
                        try
                        {
                            // Tìm dòng hiện tại
                            var row = wait.Until(d =>
                                d.FindElement(By.XPath($"(//tbody[@class='ant-table-tbody']/tr[contains(@class,'ant-table-row')])[{currentRow}]")));
                            var cellC25TYY = row.FindElement(By.XPath("./td[3]/span")).Text; // C25TYY
                            var cell22252 = row.FindElement(By.XPath("./td[4]")).Text; // 22252

                            string query = "SELECT * FROM HoaDon WHERE KyHieu = ? AND SoHD LIKE ?";


                            // Tạo mảng tham số với giá trị cho câu lệnh SQL
                            OleDbParameter[] parameters = new OleDbParameter[]
                            {
            new OleDbParameter("KyHieu", cellC25TYY),          // Sử dụng chỉ số mà không cần tên
            new OleDbParameter("SoHD", "%" + cell22252 + "%")  // Thêm ký tự % cho LIKE
                            };
                            var kq = ExecuteQuery(query, parameters);
                            var a = people;
                            var check = a.Any(m => m.KHHDon == cellC25TYY && m.SHDon.Contains(cell22252));
                            if (check || kq.Rows.Count != 0)
                            {
                                currentRow++;
                                hasdata++;
                                continue;
                            }

                            // Click vào dòng
                            row.Click();
                            button = wait.Until(d =>
                             d.FindElement(By.XPath("(//button[contains(@class, 'ant-btn-icon-only')])[19]")));
                            button.Click();
                            // Xử lý sau khi click (đợi tải, đóng popup,...)
                            Thread.Sleep(50); // Đợi 1 giây giữa các lần click
                            string fp = "";
                            if (currentRow == 15)
                            {
                                int aas = 10;
                            }
                            if (currentRow == 1)
                                fp = "C:\\S.T.E 25\\S.T.E 25\\Hoadon\\\\HDDauVao\\" + "invoice.zip";
                            else
                                fp = "C:\\S.T.E 25\\S.T.E 25\\Hoadon\\\\HDDauVao\\" + "invoice (" + (currentRow - 1 - hasdata) + ").zip";

                            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(120));
                            wait.Until(d => File.Exists(fp));
                            lstHas.Add(fp);
                            currentRow++; // Chuyển sang dòng tiếp theo
                        }
                        catch (NoSuchElementException)
                        {
                            hasMoreRows = false; // Không còn dòng nào nữa


                            Console.WriteLine($"Đã xử lý hết {currentRow - 1} dòng");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Lỗi khi xử lý dòng {currentRow}: {ex.Message}");
                            currentRow++; // Vẫn tiếp tục với dòng tiếp theo
                        }
                    }
                    if (lstHas.Count > 0)
                    {
                        var getlastlist = lstHas.LastOrDefault();
                        wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(120));
                        wait.Until(d => File.Exists(getlastlist));

                        GiaiNenhoadon();
                    }
                    //Xử lý phần trang 
                    var buttonElement = Driver.FindElements(By.ClassName("ant-btn-primary"));

                    // Kiểm tra xem button có bị vô hiệu hóa không
                    bool isDisabled = !buttonElement[7].Enabled;
                    if (isDisabled == false)
                    {
                        buttonElement[7].Click();
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        isPhantrang = true;
                    }
                }
                
                Cucthuekhngnhanma(wait);
                Xulymaytinhtien(wait);
                DoTask += 1;  

                Xulysaudangnhap();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
        private void GiaiNenhoadon()
        {
            string pah = "C:\\S.T.E 25\\S.T.E 25\\Hoadon\\HDDauVao";

            string[] zipFiles = Directory.GetFiles(pah, "*.zip");
            var i = 0;
            foreach (var zipFile in zipFiles)
            {
                string filename = "";
                if (i == 0)
                {
                    filename = "invoice.zip";
                    ExtractZip(pah, filename);
                }
                else
                {
                    filename = "invoice (" + i + ").zip";
                    ExtractZip(pah, filename);
                }
                i++;
                // string filename= "C:\\TCP\\Saoviet\\Hoadontaive\\" + "invoice (" + (currentRow - 2) + ").zip"
            }
            //Giải nén xong đi vào thư mục vừa giải nén
        }
        private void ExtractZip(string path, string filename)
        {
            string downloadPath = path;
            string zipFileName = filename; // Thay bằng tên file thực tế


            string zipFilePath = Path.Combine(downloadPath, zipFileName);
            string extractPath = Path.Combine(downloadPath, Path.GetFileNameWithoutExtension(zipFileName));
            // Tạo thư mục giải nén nếu chưa tồn tại
            Directory.CreateDirectory(extractPath);

            // Giải nén file
            try
            {
                ZipFile.ExtractToDirectory(zipFilePath, extractPath);
                Console.WriteLine($"Đã giải nén thành công vào: {extractPath}");
                File.Delete(zipFilePath);
                //Vào thư mục mới tạo
                string invoiceFilePath = Path.Combine(extractPath, "invoice.xml");
                string htmlFilepath = Path.Combine(extractPath, "invoice.html");
                var newname = Getnewname(invoiceFilePath,1);
                var newnamehtml = Getnewname(invoiceFilePath, 2);
                var getsplit = (newname.Split('_'))[2];
                var getsplithtml = (newnamehtml.Split('_'))[2];
                newname = getsplit + "\\" + newname;
                newnamehtml = getsplithtml + "\\" + newnamehtml;
                string newFilePath = Path.Combine(path, newname);
                string newFilePathhtml = Path.Combine(path, newnamehtml);
                if (File.Exists(invoiceFilePath))
                {
                    File.Move(invoiceFilePath, newFilePath);
                    File.Move(htmlFilepath, newFilePathhtml);
                    Directory.Delete(extractPath, true); // true để xóa cả nội dung bên trong
                }
                else
                {
                    Console.WriteLine("Tệp invoice.xml không tồn tại trong thư mục giải nén.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi giải nén: {ex.Message}");
            }
        }
        private string Getnewname(string path,int type)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path); // Tải file XML

            // Lấy phần tử gốc
            XmlNode root = xmlDoc.DocumentElement;

            // Lấy phần tử <NDHDon>
            XmlNode ndhDonNode = root.SelectSingleNode("//NDHDon");
            XmlNode nTTChungNode = root.SelectSingleNode("//TTChung");
            XmlNode nBanNode = nTTChungNode.SelectSingleNode("Ten");
            XmlNode NgaylapNode = root.SelectSingleNode("//NLap");
            string SHDon = nTTChungNode.SelectSingleNode("SHDon")?.InnerText;
            string KHHDon = nTTChungNode.SelectSingleNode("KHHDon")?.InnerText;
            int month = 0;
            if (DateTime.TryParse(NgaylapNode.InnerText, out DateTime date))
            {
                // Lấy tháng từ DateTime
                month = date.Month;
            }
            string filename = "";
            if (type == 1)
                filename = ".xml";
            if (type == 2)
                filename = ".html";
            return "HD_" + "_" + month + "_" + SHDon + "_" + KHHDon + filename;
        }

        private void btnLoadFromDisk_Click(object sender, EventArgs e)
        {
            int getMonth = int.Parse(cbbselect.SelectedItem.ToString());
            string newpath = savedPath + "\\HDDauVao\\" + getMonth;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn tệp để nhập";
            openFileDialog.Filter = "Tất cả các tệp (*.*)|*.*"; // Bộ lọc tệp
                                                                // Hiển thị hộp thoại chọn tệp
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string sourceFilePath = openFileDialog.FileName;
                string destFilePath = Path.Combine(newpath, Path.GetFileName(sourceFilePath));

                try
                {

                    if (File.Exists(destFilePath))
                    {
                        MessageBox.Show("Đã tồn tại file!");
                        return;
                    }
                    // Sao chép tệp vào thư mục đích
                    File.Copy(sourceFilePath, destFilePath, true); // true để ghi đè nếu tệp đã tồn tại

                    MessageBox.Show("Tệp đã được nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadXmlFiles(savedPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi trong quá trình nhập tệp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        int rowsl;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void callback()
        {
            dataGridView1[9, rowsl].Value = dataGridView1[9, rowsl].Value.ToString().Split('|')[0];
            dataGridView1[9, rowsl].Value = dataGridView1[9, rowsl].Value + " | " + NameCT;
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // e.RowIndex sẽ là -1 nếu click vào header
            {
                // Lấy giá trị của ô đã click
                var cellValue = dataGridView1[9, e.RowIndex].Value.ToString();

                if (cellValue.Contains("154") && (e.ColumnIndex) == 9)
                {
                    FrmCongTrinh frmCongTrinh = new FrmCongTrinh(this.result);
                    rowsl = e.RowIndex;

                    // Thiết lập owner nếu cần
                    frmCongTrinh.Owner = this; // Thiết lập form hiện tại là owner của form mới

                    frmCongTrinh.frm = this;
                    // Mở form mới dưới dạng modal
                    frmCongTrinh.Show(); // Mở form

                    // Đưa form mới lên trên cùng
                    frmCongTrinh.BringToFront();
                    frmCongTrinh.Activate();
                }
                else
                {
                    if (e.ColumnIndex == 2)
                    {
                        WebBrowser  webBrowser1 = new WebBrowser
                        {
                            Dock = DockStyle.Fill // Đổ đầy không gian của form
                        };
                        cellValue = dataGridView1[2, e.RowIndex].Value.ToString();
                        var hiddenValue = dataGridView1.Rows[e.RowIndex].Cells["Path"].Value;
                        frmWebbrowser frmCongTrinh = new frmWebbrowser( );
                        rowsl = e.RowIndex;
                        frmCongTrinh.Text = hiddenValue.ToString().Replace(".xml",""); 
                        // Thiết lập owner nếu cần
                        frmCongTrinh.Owner = this; // Thiết lập form hiện tại là owner của form mới
                         
                        // Mở form mới dưới dạng modal
                        frmCongTrinh.Show(); // Mở form

                        // Đưa form mới lên trên cùng
                        frmCongTrinh.BringToFront();
                        frmCongTrinh.Activate();
                        // Thêm điều khiển WebBrowser vào Form
                        frmCongTrinh.Controls.Add(webBrowser1);

                        
                        string filePath = hiddenValue.ToString().Replace(".xml",".html");

                        webBrowser1.Navigate("file:///" + filePath.Replace("\\", "/"));

                    }
                }
                
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnKHCapnhat_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtKHUsername.Text) || string.IsNullOrEmpty(txtKHPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập thông tin");
                return;
            }
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["username"].Value = txtKHUsername.Text;
            config.AppSettings.Settings["password"].Value = txtKHPassword.Text;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            MessageBox.Show("Cập nhật tài khoản thành công");
        }

        private void LoadDataDinhDanh()
        {
            string querykh = @" SELECT *  FROM tbDinhdanhtaikhoan"; // Sử dụng ? thay cho @mst trong OleDb

            result = ExecuteQuery(querykh, new OleDbParameter("?", ""));
            grvDinhdanh.DataSource = result;
            foreach (DataGridViewColumn column in grvDinhdanh.Columns)
            {
                column.Visible = false;
            }

             
            grvDinhdanh.Columns["ID"].Visible = true;
            grvDinhdanh.Columns["ID"].HeaderText = "STT";
            grvDinhdanh.Columns["ID"].Width = 70;
            grvDinhdanh.Columns["ID"].DisplayIndex = 0;
            grvDinhdanh.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grvDinhdanh.Columns["ID"].ReadOnly = true;
            //
            grvDinhdanh.Columns["KeyValue"].Visible = true;
            grvDinhdanh.Columns["KeyValue"].HeaderText = "Tên";
            grvDinhdanh.Columns["KeyValue"].Width = 400;
            grvDinhdanh.Columns["KeyValue"].DisplayIndex = 1;
            grvDinhdanh.Columns["KeyValue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grvDinhdanh.Columns["KeyValue"].ReadOnly = true;

            grvDinhdanh.Columns["TKNo"].Visible = true;
            grvDinhdanh.Columns["TKNo"].HeaderText = "TK Nợ";
            grvDinhdanh.Columns["TKNo"].Width =120;
            grvDinhdanh.Columns["TKNo"].DisplayIndex = 2;
            grvDinhdanh.Columns["TKNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grvDinhdanh.Columns["TKNo"].ReadOnly = true;

            grvDinhdanh.Columns["TKCo"].Visible = true;
            grvDinhdanh.Columns["TKCo"].HeaderText = "TK Có";
            grvDinhdanh.Columns["TKCo"].Width = 120;
            grvDinhdanh.Columns["TKCo"].DisplayIndex = 3;
            grvDinhdanh.Columns["TKCo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grvDinhdanh.Columns["TKCo"].ReadOnly = true;

            grvDinhdanh.Columns["TKThue"].Visible = true;
            grvDinhdanh.Columns["TKThue"].HeaderText = "TK Thuế";
            grvDinhdanh.Columns["TKThue"].Width =120;
            grvDinhdanh.Columns["TKThue"].DisplayIndex = 4;
            grvDinhdanh.Columns["TKThue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grvDinhdanh.Columns["TKThue"].ReadOnly = true;

            grvDinhdanh.Columns["Type"].Visible = true;
            grvDinhdanh.Columns["Type"].HeaderText = "Diễn giải";
            grvDinhdanh.Columns["Type"].Width = 300;
            grvDinhdanh.Columns["Type"].DisplayIndex = 5;
            grvDinhdanh.Columns["Type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grvDinhdanh.Columns["Type"].ReadOnly = true;

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Delete",
                Text = "Xóa",
                UseColumnTextForButtonValue = true,
                DisplayIndex =6

            };
            grvDinhdanh.Columns.Add(deleteButtonColumn);


            grvDinhdanh.RowHeadersVisible = false;
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = tabControl1.SelectedIndex;
            if (selectedIndex == 1)
            {
                txtKHUsername.Text= ConfigurationManager.AppSettings["username"];
                txtKHPassword.Text = ConfigurationManager.AppSettings["password"];
            }
            if (selectedIndex == 2)
            {
                LoadDataDinhDanh();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNoidung.Text) || string.IsNullOrEmpty(txtTKNo.Text) || string.IsNullOrEmpty(txtTkCo.Text) || string.IsNullOrEmpty(txtTKThue.Text))
            {
                MessageBox.Show("Vui lòng nhập thông tin!");
                return;
            }
                string  query = @"
        INSERT INTO tbDinhdanhtaikhoan (KeyValue,TKNo,TKCo,TKThue,Type)
        VALUES (?,?,?,?,?)";
            OleDbParameter[] parameters = new OleDbParameter[]
{
        new OleDbParameter("?",txtNoidung.Text), 
           new OleDbParameter("?",txtTKNo.Text),
                 new OleDbParameter("?",txtTkCo.Text),
             new OleDbParameter("?",txtTKThue.Text),
              new OleDbParameter("?",txtGhichu.Text)
};

            // Thực thi truy vấn và lấy kết quả
            int a = ExecuteQueryResult(query, parameters);
            LoadDataDinhDanh();
        }

        private void grvDinhdanh_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = grvDinhdanh.Columns[e.ColumnIndex].Name;

            if (columnName=="")
            {
                // Xác nhận xóa với người dùng
                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa hàng này?",
                                     "Xác nhận xóa",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    var getid = grvDinhdanh.Rows[e.RowIndex].Cells[0].Value.ToString();
                    // Xóa hàng
                    string sql = "DELETE FROM tbDinhdanhtaikhoan WHERE ID = @AccountID";
                    OleDbParameter[] parameters = new OleDbParameter[]
                {
        new OleDbParameter("?", getid),
                };
                    int resl = ExecuteQueryResult(sql, parameters);
                    LoadDataDinhDanh();
                }
            }
        }

        private void cbbselect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "13")

            {
                //  e.SuppressKeyPress = true; // Ngăn chặn âm thanh "bing"

                // Lấy chỉ số hàng hiện tại
                int currentRowIndex = dataGridView1.CurrentCell.RowIndex;
                int currentColumnIndex = dataGridView1.CurrentCell.ColumnIndex;
                if (currentColumnIndex != 9)
                {
                    string cellValue = dataGridView1[currentColumnIndex, currentRowIndex - 1].Value?.ToString();
                    dataGridView1.Rows[currentRowIndex].Cells[currentColumnIndex].Value = cellValue;
                }
                else
                {
                    string cellValue = dataGridView1[currentColumnIndex, currentRowIndex - 1].Value?.ToString();
                    dataGridView1.Rows[currentRowIndex].Cells[currentColumnIndex].Value = cellValue;
                    cellValue = dataGridView1[(currentColumnIndex - 1), currentRowIndex - 1].Value?.ToString();
                    dataGridView1.Rows[currentRowIndex].Cells[currentColumnIndex-1].Value = cellValue;
                    cellValue = dataGridView1[(currentColumnIndex - 2), currentRowIndex - 1].Value?.ToString();
                    dataGridView1.Rows[currentRowIndex].Cells[currentColumnIndex - 2].Value = cellValue;
                }
               
            }
        }

        private static string RemoveVietnameseDiacritics(string text)
        {
            // Mảng chứa ký tự có dấu
            string[] arr1 = new string[]
            {
        "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
        "đ",
        "é", "è", "ẻ", "ẽ", "ẹ", "ê", "ế", "ề", "ể", "ễ", "ệ",
        "í", "ì", "ỉ", "ĩ", "ị",
        "ó", "ò", "ỏ", "õ", "ọ", "ô", "ố", "ồ", "ổ", "ỗ", "ộ", "ơ", "ớ", "ờ", "ở", "ỡ", "ợ",
        "ú", "ù", "ủ", "ũ", "ụ", "ư", "ứ", "ừ", "ử", "ữ", "ự",
        "ý", "ỳ", "ỷ", "ỹ", "ỵ"
            };

            // Mảng chứa ký tự không có dấu
            string[] arr2 = new string[]
            {
        "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
        "d",
        "e", "e", "e", "e", "e", "e", "e", "e", "e", "e", "e",
        "i", "i", "i", "i", "i",
        "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o",
        "u", "u", "u", "u", "u", "u", "u", "u", "u", "u", "u",
        "y", "y", "y", "y", "y"
            };

            // Thay thế ký tự có dấu bằng ký tự không có dấu
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]); // Thay thế chữ thường
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper()); // Thay thế chữ hoa
            }
            var returnvalue = text.Replace("Má", "Ma").Replace("má", "ma");
            return returnvalue;
        }
        private static string GenerateRandomNumbers(int length)
        {
            Random random = new Random();
            string randomNumbers = "";
            for (int i = 0; i < length; i++)
            {
                // Sinh số ngẫu nhiên từ 1 đến 9
                randomNumbers += random.Next(1, 10).ToString();
            }
            return randomNumbers;
        }
        #endregion
    }
}
