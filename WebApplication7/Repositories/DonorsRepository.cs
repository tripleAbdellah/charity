using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using DonorApp.Models;

namespace DonorApp.Repositories
{
    public class DonorsRepository : IDonorsRepository
    {
        public IEnumerable<Donor> GetAllDonors()
        {
            Donor donor = new Donor();
            donor.CODE = 100;
            return GetDonors(donor);
        }

        public IEnumerable<Donor> GetDonors(SearchDonorRequest searchRequest)
        {
            Donor donor = new Donor();
            donor.CODE = searchRequest.Code;
            donor.PCODE = searchRequest.PostCode;
            donor.EMAIL = searchRequest.Email;
            //donor.TEL = searchRequest.PhoneNumber;
            return GetDonors(donor);
        }

        public IEnumerable<Donor> GetDonors(Donor donor)
        {
            List<Donor> donors = new List<Donor>();

            var con = ConfigurationManager.ConnectionStrings["October2001"].ToString();
           
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                //building the SQL string
                string SQLWhereString = "";

                if (donor.CODE > 0)
                {
                    SQLWhereString = " Code = " + donor.CODE ;
                }

                if (donor.NAME != null && donor.NAME != "")
                {
                    if (SQLWhereString != "") { SQLWhereString = SQLWhereString + " and "; }
                    SQLWhereString = SQLWhereString + " Name like '%" + donor.NAME + "%'";
                }

                if (donor.ADD1 != null && donor.ADD1 != "")
                {
                    if (SQLWhereString != "") { SQLWhereString = SQLWhereString + " and "; }
                    SQLWhereString = SQLWhereString + " Add1 like '%" + donor.ADD1 + "%'";
                }
               
                if (donor.CITY != null && donor.CITY != "")
                {
                    if (SQLWhereString != "") { SQLWhereString = SQLWhereString + " and "; }
                    SQLWhereString = SQLWhereString + " City = '" + donor.CITY + "'";
                }

                if (donor.PCODE != null && donor.PCODE != "")
                {
                    if (SQLWhereString != "") { SQLWhereString = SQLWhereString + " and "; }
                    SQLWhereString = SQLWhereString + " pcode like '%" + donor.PCODE + "%'";
                }

                if (SQLWhereString != "") { 

                    string oString = "Select * from mail where " + SQLWhereString ;
                    SqlCommand oCmd = new SqlCommand(oString, myConnection);
                    //oCmd.Parameters.AddWithValue("@Fname", "abdellah");
                    myConnection.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            Donor matchingPerson = new Donor();
                            matchingPerson.TITLE = oReader["TITLE"].ToString();
                            matchingPerson.NAME = oReader["NAME"].ToString();
                            matchingPerson.CODE = (int)oReader["CODE"];
                            matchingPerson.PCODE = oReader["PCODE"].ToString();
                            matchingPerson.ADD1 = oReader["ADD1"].ToString();
                            matchingPerson.ADD2 = oReader["ADD2"].ToString();
                            matchingPerson.CITY = oReader["CITY"].ToString();
                            matchingPerson.COUNTRY = oReader["COUNTRY"].ToString();
                            matchingPerson.COUNTY = oReader["COUNTY"].ToString();
                            matchingPerson.EMAIL = oReader["EMAIL"].ToString();
                            matchingPerson.ERECEIPT = (bool)oReader["ERECEIPT"];
                            matchingPerson.GAD = (bool)oReader["GAD"];
                            matchingPerson.MOBILE = oReader["MOBILE"].ToString();

                            donors.Add(matchingPerson);
                        }

                        myConnection.Close();
                    }
                }
            }

            return donors;
        }

        public Donor GetDonor(int donorID)
        {
            Donor matchingPerson = new Donor();

            var con = ConfigurationManager.ConnectionStrings["October2001"].ToString();

            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "Select * from mail where Code=@DonorCode";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                oCmd.Parameters.AddWithValue("@DonorCode", donorID);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        matchingPerson.NAME     = oReader["NAME"].ToString();
                        matchingPerson.CODE     = (int)oReader["CODE"];
                        matchingPerson.PCODE    = oReader["PCODE"].ToString();
                        matchingPerson.ADD1 = "test";
                        matchingPerson.ADD2     = oReader["ADD2"].ToString();
                        matchingPerson.CITY     = oReader["CITY"].ToString();
                        matchingPerson.COUNTRY  = oReader["COUNTRY"].ToString();
                        matchingPerson.COUNTY   = oReader["COUNTY"].ToString();
                        matchingPerson.EMAIL    = oReader["EMAIL"].ToString();
                        matchingPerson.ERECEIPT = (bool)oReader["ERECEIPT"];
                        matchingPerson.GAD      = (bool)oReader["GAD"];
                        matchingPerson.MOBILE   = oReader["MOBILE"].ToString();
                    }

                    myConnection.Close();
                }
            }

            return matchingPerson;
        }

        //public Donor AddDonor(Donor aDonor)
        public int AddDonor(Donor aDonor)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["October2001"].ConnectionString;
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("insert_donor", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //set up input parameters
                cmd.Parameters.Add("@title", SqlDbType.NVarChar, 255).Value = aDonor.TITLE;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 255).Value = aDonor.NAME;
                cmd.Parameters.Add("@type", SqlDbType.Float).Value = aDonor.TYPE;
                cmd.Parameters.Add("@add1", SqlDbType.NVarChar, 255).Value = aDonor.ADD1;
                cmd.Parameters.Add("@add2", SqlDbType.NVarChar, 255).Value = aDonor.ADD2;
                cmd.Parameters.Add("@city", SqlDbType.NVarChar, 255).Value = aDonor.CITY;
                cmd.Parameters.Add("@pcode", SqlDbType.NVarChar, 255).Value = aDonor.PCODE;
                cmd.Parameters.Add("@county", SqlDbType.NVarChar, 255).Value = aDonor.COUNTY;
                cmd.Parameters.Add("@country", SqlDbType.NVarChar, 255).Value = aDonor.COUNTRY;
                cmd.Parameters.Add("@tel", SqlDbType.NVarChar, 255).Value = aDonor.TEL;
                cmd.Parameters.Add("@tel_work", SqlDbType.NVarChar, 255).Value = aDonor.TEL_WORK;
                cmd.Parameters.Add("@mobile", SqlDbType.NVarChar, 50).Value = aDonor.MOBILE;
                cmd.Parameters.Add("@ntuserwhoadded", SqlDbType.NVarChar, 50).Value = aDonor.NTUSERWHOADDED;
                cmd.Parameters.Add("@ereceipt", SqlDbType.Bit).Value = aDonor.ERECEIPT;
                cmd.Parameters.Add("@gad", SqlDbType.Bit).Value = aDonor.GAD;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = aDonor.EMAIL;

                //set up output parameters
                SqlParameter newDonorCode = new SqlParameter("@o_donor_code", SqlDbType.Int);
                newDonorCode.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(newDonorCode);

                //execute the query
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                aDonor.CODE = (int)newDonorCode.Value;
            }
            //return aDonor;
            return aDonor.CODE;
        }

        public void DeleteDonor(int donorID)
        {
            var con = ConfigurationManager.ConnectionStrings["October2001"].ToString();
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = string.Format("DELETE FROM mail WHERE CODE = {0}", donorID); 

                using (SqlCommand oCmd = new SqlCommand(oString, myConnection))
                {
                    myConnection.Open();
                    oCmd.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
        }

        private int getDonorID() {

            var con = ConfigurationManager.ConnectionStrings["October2001"].ToString();
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = string.Format("select max(code) AS CODE from mail");
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        return  (Convert.ToInt32(oReader[0]) + 1);
                    }

                    myConnection.Close();
                }
            }

            return 0;
        }

        public void UpdateDonor(int donorID, Donor donor)
        {
            /*string query = string.Format("UPDATE [Northwind].[dbo].[Customers] " +
                    " SET [CustomerID] = '{0}'," +
                    " [CompanyName] = '{1}', " +
                    " [ContactName] = '{2}', " +
                    " [ContactTitle] = '{3}', " +
                    " [Address] = '{4}', " +
                    " [City] = '{5}', " +
                    " [Region] = '{6}', " +
                    " [PostalCode] = '{7}', " +
                    " [Country] = '{8}', " +
                    " [Phone] = '{9}', " +
                    " [Fax] = '{10}' " +
                    " WHERE CustomerID LIKE '{11}'", donor.CustomerID, donor.CompanyName, donor.ContactName, donor.ContactTitle, donor.Address, donor.City, donor.Region,
                     donor.PostalCode, donor.Country, donor.Phone, donor.Fax, donor.CustomerID);                    
            
            using (SqlConnection con =
                    new SqlConnection("your connection string"))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            */
        }
    }
}