using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.dao
{
    class SinhVienDAO
    {
        SqlConnection cnStr;
        DataSet myDS;
        SqlDataAdapter dAdapt;
        SqlCommandBuilder invBuilder;

        private void close()
        {
            if (cnStr != null) cnStr.Close();
        }
        public DataSet getAllMSSV()
        {
            cnStr = DBHelper.GetConnection();
            try
            {
                dAdapt = new SqlDataAdapter("Select MASV from SVIEN",cnStr);
                invBuilder = new SqlCommandBuilder(dAdapt);
                myDS = new DataSet();
                dAdapt.Fill(myDS);
                return myDS;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                close();
            }
            
            return null;
        }

        public DataSet getSV(int mssv)
        {
            cnStr = DBHelper.GetConnection();
            try
            {
                dAdapt = new SqlDataAdapter("Select * from SVIEN where MASV = " + mssv, cnStr);
                invBuilder = new SqlCommandBuilder(dAdapt);
                myDS = new DataSet();
                dAdapt.Fill(myDS);
                return myDS;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                close();
            }
            return null;
        }
        public DataSet getMonHocByMSSV(int mssv)
        {
            cnStr = DBHelper.GetConnection();
            try
            {
                dAdapt = new SqlDataAdapter("Select m.MAMH, TENMH, DIEM, [DIEM 2] " +
                    " from MHOC m, HPHAN h, KQUA k " +
                    " where k.MASV = " + mssv + " AND k.MAHP = h.MAHP AND h.MAMH = m.MAMH",cnStr);
                invBuilder = new SqlCommandBuilder(dAdapt);
                myDS = new DataSet();
                dAdapt.Fill(myDS);
                return myDS;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                close();
            }
            return null;
        }
    }
}
