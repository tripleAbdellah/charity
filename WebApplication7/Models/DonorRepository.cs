using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;


namespace DonorApp.Models
{
    public class DonorRepository : IDonorRepository
    {
        public IEnumerable<Donor> GetAll()
        {
            List<Donor> donors = new List<Donor>();

            var con = ConfigurationManager.ConnectionStrings["October2001"].ToString();

            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "Select * from mail where Name=@fName";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                oCmd.Parameters.AddWithValue("@Fname", "abdellah");
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        Donor matchingPerson = new Donor();
                        matchingPerson.NAME = oReader["NAME"].ToString();
                        matchingPerson.CODE = (int)oReader["CODE"];
                        matchingPerson.PCODE = oReader["PCODE"].ToString();
                        donors.Add(matchingPerson);
                    }

                    myConnection.Close();
                }
            }

            return donors;
        }

        public Donor Get(String name)
        {
            Donor matchingPerson = new Donor();

            var con = ConfigurationManager.ConnectionStrings["October2001"].ToString();

            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "Select * from mail where Name=@Name";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                oCmd.Parameters.AddWithValue("@Name", name);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        matchingPerson.NAME = oReader["NAME"].ToString();
                        matchingPerson.CODE = (int)oReader["CODE"];
                        matchingPerson.PCODE = oReader["PCODE"].ToString();
                    }

                    myConnection.Close();
                }
            }
            return matchingPerson; 
        }

        public Donor Get(int donorID)
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
                        matchingPerson.NAME = oReader["NAME"].ToString();
                        matchingPerson.CODE = (int)oReader["CODE"];
                        matchingPerson.PCODE = oReader["PCODE"].ToString();
                    }

                    myConnection.Close();
                }
            }

            return matchingPerson;
        }

        public Donor Add(Donor donor)
        {
            //First get the latest donorcode, this is not nicest part, but using their logic for now: 
            donor.CODE = getDonorID();
            //System.Threading.Thread.Sleep(10000);
            var con = ConfigurationManager.ConnectionStrings["October2001"].ToString();
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = string.Format("INSERT INTO MAIL " +
                        " ( [CODE],[TITLE], [NAME], [TYPE], [ADD1], [ADD2], [City], [PCODE], [COUNTY], [COUNTRY], [TEL],  " +
                        " [TEL_WORK], [MOBILE ],[NTUSERWHOADDED], [ERECEIPT],[GAD],[EMAIL] ) VALUES " +
                       " ( '{0}', '{1}', '{2}', '{3}', '{4}',  '{5}', '{6}', '{7}', '{8}',  '{9}', '{10}','{11}', '{12}', '{13}', '{14}', '{15}', '{16}')",
                       donor.CODE, donor.TITLE, donor.NAME, donor.TYPE, donor.ADD1, donor.ADD2, donor.CITY, donor.PCODE, donor.COUNTY, donor.COUNTRY, donor.TEL,
                       donor.TEL_WORK,donor.MOBILE,donor.NTUSERWHOADDED,donor.ERECEIPT,donor.GAD,donor.EMAIL
                      );

                using (SqlCommand oCmd = new SqlCommand(oString, myConnection))
                {
                    myConnection.Open();
                    oCmd.ExecuteNonQuery();
                    myConnection.Close();
                }
            }

            return donor;
        }

        public Donor AddDonor(Donor aDonor)
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
            return aDonor;
        }

        public bool Remove(int donorID)
        {
            var con = ConfigurationManager.ConnectionStrings["October2001"].ToString();
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = string.Format("DELETE FROM mail WHERE CODE = '{0}", donorID); 

                using (SqlCommand oCmd = new SqlCommand(oString, myConnection))
                {
                    myConnection.Open();
                    oCmd.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
            return true;
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

        public bool Update(Donor donor)
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
            return true;
        }
    }
}