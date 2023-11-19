using Assignment2.dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2
{
    public partial class Form1 : Form
    {
        DataSet myDS = new DataSet();
        //make connection
        SqlConnection cnStr = DBHelper.GetConnection();
        SqlDataAdapter dAdapt = null;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadDataInComboBox();
        }
        public void loadDataInComboBox()
        {
            try
            {
                SinhVienDAO dao = new SinhVienDAO();
                myDS = dao.getAllMSSV();
                comboBoxMssv.DisplayMember = "MASV";
                comboBoxMssv.ValueMember = "MASV";
                comboBoxMssv.DataSource = myDS.Tables[0];
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot load data " + e.Message);
                dataGrid.DataSource = null;
            }
        }

        private void comboBoxMssv_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SinhVienDAO dao = new SinhVienDAO();
                myDS = dao.getSV(int.Parse(comboBoxMssv.Text));
                txtHo.Text = myDS.Tables[0].Rows[0][1].ToString();
                txtTen.Text = myDS.Tables[0].Rows[0][2].ToString();
                txtGioiTinh.Text = myDS.Tables[0].Rows[0][3].ToString();
                txtNamSinh.Text = myDS.Tables[0].Rows[0][4].ToString();
                txtMaKhoa.Text = myDS.Tables[0].Rows[0][5].ToString();

                myDS = dao.getMonHocByMSSV(int.Parse(comboBoxMssv.Text));
                dataGrid.DataSource = myDS.Tables[0];
                dataGrid.Columns["TENMH"].Width = 200;
                if (dataGrid.Columns.Count < 5) {
                    DataGridViewTextBoxColumn newColumn = new DataGridViewTextBoxColumn() { Name = "DIEM TB" };
                    dataGrid.Columns.Add(newColumn);
                }
                double divAve = 0;
                double totalAve = 0; 
                foreach (DataGridViewRow row in dataGrid.Rows)
                {
                    var score1 = row.Cells["DIEM"].Value;
                    var score2 = row.Cells["DIEM 2"].Value;

                    if (score1 != null && score2 != null)
                    {
                        double score_1 = double.Parse(score1.ToString());
                        double score_2 = double.Parse(score2.ToString());
                        double ave = (score_1 + score_2) / 2;
                        row.Cells["DIEM TB"].Value = ave;
                        divAve++;
                        totalAve += ave;
                    }
                }

                txtDiemTB.Text = String.Format("{0:0.00}", (totalAve / divAve));
            }
            catch (Exception ex)
            {
                MessageBox.Show("wrong " + ex.Message);
            }
        }
    }
}
