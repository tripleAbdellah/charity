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


        public IEnumerable<Donor> GetDonors(SearchDonorRequest searchRequest)
        {

            Donor donor = new Donor();
            donor.Code = searchRequest.Code;
            donor.PostCode = searchRequest.PostCode;
            donor.Email = searchRequest.Email;
            //there are 3 telephone fields, so check all three for potential donors. 
            donor.TelephoneMobile = searchRequest.PhoneNumber;
            donor.TelephoneWork = searchRequest.PhoneNumber;
            donor.TelephoneHome = searchRequest.PhoneNumber;

            return GetDonors(donor);
        }

        public IEnumerable<Donor> GetDonors(Donor donor)
        {
            List<Donor> donors = new List<Donor>();
            SqlConnection myConnection = null;
            SqlCommand oCmd = null;

            var con = ConfigurationManager.ConnectionStrings["October2001"].ToString();

            try
            {
                using (myConnection = new SqlConnection(con))
                {
                    //building the SQL string
                    string SQLWhereString = "";


                    if (donor.Code > 0)
                    {
                        SQLWhereString = " Code = " + donor.Code;
                    }

                    if (donor.PostCode != null && donor.PostCode != "")
                    {
                        if (SQLWhereString != "") { SQLWhereString = SQLWhereString + " and "; }
                        SQLWhereString = SQLWhereString + " pcode like '%" + donor.PostCode + "%'";
                    }

                    if (donor.Email != null && donor.Email != "")
                    {
                        if (SQLWhereString != "") { SQLWhereString = SQLWhereString + " and "; }
                        SQLWhereString = SQLWhereString + " Email = '" + donor.Email + "'";
                    }

                    if (donor.TelephoneHome != null && donor.TelephoneHome != "")
                    {
                        if (SQLWhereString != "") { SQLWhereString = SQLWhereString + " or "; }
                        SQLWhereString = SQLWhereString + " Tel like '%" + donor.TelephoneHome + "%'";
                    }

                    if (donor.TelephoneMobile != null && donor.TelephoneMobile != "")
                    {
                        if (SQLWhereString != "") { SQLWhereString = SQLWhereString + " or "; }
                        SQLWhereString = SQLWhereString + " Mobile like '%" + donor.TelephoneMobile + "%'";
                    }


                    if (donor.TelephoneWork != null && donor.TelephoneWork != "")
                    {
                        if (SQLWhereString != "") { SQLWhereString = SQLWhereString + " or "; }
                        SQLWhereString = SQLWhereString + " Tel_Work like '%" + donor.TelephoneWork + "%'";
                    }

                    if (donor.Name != null && donor.Name != "")
                    {
                        if (SQLWhereString != "") { SQLWhereString = SQLWhereString + " and "; }
                        SQLWhereString = SQLWhereString + " Name like '%" + donor.Name + "%'";
                    }

                    if (donor.Address1 != null && donor.Address1 != "")
                    {
                        if (SQLWhereString != "") { SQLWhereString = SQLWhereString + " and "; }
                        SQLWhereString = SQLWhereString + " Add1 like '%" + donor.Address1 + "%'";
                    }

                    if (donor.City != null && donor.City != "")
                    {
                        if (SQLWhereString != "") { SQLWhereString = SQLWhereString + " and "; }
                        SQLWhereString = SQLWhereString + " City = '" + donor.City + "'";
                    }

                    if (SQLWhereString != "")
                    {

                        string oString = "Select * from mail where " + SQLWhereString;
                        oCmd = new SqlCommand(oString, myConnection);
                        myConnection.Open();
                        using (SqlDataReader oReader = oCmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                Donor matchingDonor = new Donor();

                                matchingDonor = GetMatchingDonor((IDataRecord)oReader); ;

                                donors.Add(matchingDonor);
                            }

                            myConnection.Close();
                        }
                    }

                }
            }
            finally
            {
                if (myConnection != null)
                    myConnection.Dispose();

                if (oCmd != null)
                    oCmd.Dispose();
            }

            if (!donors.Any())
                throw new NoDonorsFoundException();

            return donors;
        }

        public Donor GetDonor(int donorID)
        {
            Donor matchingDonor = null;
            SqlConnection myConnection = null;
            SqlCommand oCmd = null;

            var con = ConfigurationManager.ConnectionStrings["October2001"].ToString();

            try
            {
                using (myConnection = new SqlConnection(con))
                {
                    string oString = "Select * from mail where Code=@DonorCode";
                    oCmd = new SqlCommand(oString, myConnection);
                    oCmd.Parameters.AddWithValue("@DonorCode", donorID);
                    myConnection.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {

                        while (oReader.Read())
                        {
                            matchingDonor = GetMatchingDonor((IDataRecord)oReader);
                        }

                        if (matchingDonor == null)
                        {
                            throw new DonorNotFoundException();
                        }

                        myConnection.Close();
                    }
                }

            }
            finally
            {
                if (myConnection != null)
                    myConnection.Dispose();

                if (oCmd != null)
                    oCmd.Dispose();
            }

            return matchingDonor;
        }

        private Donor GetMatchingDonor(IDataRecord oReader)
        {
            /*AAA: Reason for this method is to reuse the same logic in getDonor and getDonors */

            Donor matchingDonor = new Donor();

            matchingDonor.Code = (int)oReader["CODE"];
            matchingDonor.Title = oReader["TITLE"].ToString();
            matchingDonor.Name = oReader["NAME"].ToString();

            //address
            matchingDonor.PostCode = oReader["PCODE"].ToString();
            matchingDonor.Address1 = oReader["ADD1"].ToString();
            matchingDonor.Address2 = oReader["ADD2"].ToString();
            matchingDonor.City = oReader["CITY"].ToString();
            matchingDonor.Country = oReader["COUNTRY"].ToString();
            matchingDonor.County = oReader["COUNTY"].ToString();

            //communication 
            matchingDonor.Email = oReader["EMAIL"].ToString();
            if (oReader["ERECEIPT"] != null && oReader["ERECEIPT"].ToString() != "")
                matchingDonor.CommunicationByEmail = (bool)oReader["ERECEIPT"];

            matchingDonor.TelephoneMobile = oReader["MOBILE"].ToString();
            matchingDonor.TelephoneHome = oReader["TEL"].ToString();
            matchingDonor.TelephoneWork = oReader["TEL_WORK"].ToString();
            matchingDonor.LanguageSpoken = oReader["ARB_ENG"].ToString();

            //GAD
            matchingDonor.GAD = (bool)oReader["GAD"];
            matchingDonor.GADName = oReader["GAD NAME"].ToString();
            if (oReader["GADDate"] != null && oReader["GADDate"].ToString() != "")
                matchingDonor.GADDate = (DateTime)oReader["GADDate"];

            if (oReader["VerbalGAD"] != null && oReader["VerbalGAD"].ToString() != "")
                matchingDonor.GADVerbal = (bool)oReader["VerbalGAD"];

            //Audit
            matchingDonor.CreatedBy = oReader["NTUSERWHOADDED"].ToString();

            return matchingDonor;
        }


        public Donor AddDonor(Donor aDonor)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            string connectionString = ConfigurationManager.ConnectionStrings["October2001"].ConnectionString;
            if (aDonor != null)
            {

                /*mandatory fields check, otherwise throw: InvalidDonorException, or should this validation check  done in stored procedure?? 
                 * Name
                 * Address1
                 * City
                 * PostCode
                 * Country
                 * DonorType
                 * GAD
                 */
                if ((aDonor.Name == null || aDonor.Name.Length < 2) ||
                    (aDonor.Address1 == null || aDonor.Address1.Length < 2) ||
                    (aDonor.City == null || aDonor.City.Length < 2) ||
                    (aDonor.PostCode == null || aDonor.PostCode.Length < 5) || (!isDonorTypeValid(aDonor.Type))
                    )
                    throw new InvalidDonorException();

                //if GAD is set then also GADName is required 
                if (aDonor.GAD) { 
                  if (aDonor.GADName == null || aDonor.GADName.Length < 2)
                      throw new InvalidDonorException();
                }

                try
                {
                    using (conn = new SqlConnection(connectionString))
                    {
                        cmd = new SqlCommand("insert_donor", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        //set up input parameters
                        cmd.Parameters.Add("@title", SqlDbType.NVarChar, 255).Value = aDonor.Title;
                        cmd.Parameters.Add("@name", SqlDbType.NVarChar, 255).Value = aDonor.Name;
                        cmd.Parameters.Add("@type", SqlDbType.Float).Value = aDonor.Type;
                        cmd.Parameters.Add("@add1", SqlDbType.NVarChar, 255).Value = aDonor.Address1;
                        cmd.Parameters.Add("@add2", SqlDbType.NVarChar, 255).Value = aDonor.Address2;
                        cmd.Parameters.Add("@city", SqlDbType.NVarChar, 255).Value = aDonor.City;
                        cmd.Parameters.Add("@pcode", SqlDbType.NVarChar, 255).Value = aDonor.PostCode;
                        cmd.Parameters.Add("@county", SqlDbType.NVarChar, 255).Value = aDonor.County;
                        cmd.Parameters.Add("@country", SqlDbType.NVarChar, 255).Value = aDonor.Country;
                        cmd.Parameters.Add("@tel", SqlDbType.NVarChar, 255).Value = aDonor.TelephoneHome;
                        cmd.Parameters.Add("@tel_work", SqlDbType.NVarChar, 255).Value = aDonor.TelephoneWork;
                        cmd.Parameters.Add("@mobile", SqlDbType.NVarChar, 50).Value = aDonor.TelephoneMobile;
                        cmd.Parameters.Add("@ntuserwhoadded", SqlDbType.NVarChar, 50).Value = aDonor.CreatedBy;
                        cmd.Parameters.Add("@ereceipt", SqlDbType.Bit).Value = aDonor.CommunicationByEmail;
                        cmd.Parameters.Add("@gad", SqlDbType.Bit).Value = aDonor.GAD;
                        cmd.Parameters.Add("@gadVerbal", SqlDbType.Bit).Value = aDonor.GADVerbal;
                        cmd.Parameters.Add("@gadName", SqlDbType.NVarChar, 255).Value = aDonor.GADName;
                        cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = aDonor.Email;
                        cmd.Parameters.Add("@ARB_ENG", SqlDbType.NVarChar, 50).Value = aDonor.LanguageSpoken;

                        //set up output parameters
                        SqlParameter newDonorCode = new SqlParameter("@o_donor_code", SqlDbType.Int);
                        newDonorCode.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(newDonorCode);

                        //execute the query
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        aDonor.Code = (int)newDonorCode.Value;
                    }
                }
                finally
                {
                    if (conn != null)
                        conn.Dispose();

                    if (cmd != null)
                        cmd.Dispose();
                }
            }
            else {
                throw new InvalidDonorException(); 
            }

            return aDonor;
        }

        private Boolean isDonorTypeValid(int donorType) {
            SqlConnection myConnection = null;
            SqlCommand oCmd = null;
            Boolean isValid = false;
           
            var con = ConfigurationManager.ConnectionStrings["October2001"].ToString();
            using (myConnection = new SqlConnection(con))
            {
                string oString = string.Format("Select * from ADDRESS_CATEGORY WHERE ADDRESS_CATEGORY.[ADDRESS TYPE] = {0}", donorType);
                try
                {
                    oCmd = new SqlCommand(oString, myConnection);
                    myConnection.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        oReader.Read();
                        isValid = oReader.HasRows;
                        oReader.Close();
                    }
                    myConnection.Close();
                }
                finally
                {
                    if (myConnection != null)
                        myConnection.Dispose();

                    if (oCmd != null)
                        oCmd.Dispose();

                }
            }
            return isValid; 
        }


        public void UpdateDonor(int donorID, Donor aDonor)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            if (aDonor != null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["October2001"].ConnectionString;

                try
                {
                    using (conn = new SqlConnection(connectionString))
                    {
                        cmd = new SqlCommand("update_donor", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        //set up input parameters
                        cmd.Parameters.Add("@donor_code", SqlDbType.Int, 255).Value = donorID;
                        cmd.Parameters.Add("@title", SqlDbType.NVarChar, 255).Value = aDonor.Title;
                        cmd.Parameters.Add("@name", SqlDbType.NVarChar, 255).Value = aDonor.Name;
                        cmd.Parameters.Add("@type", SqlDbType.Float).Value = aDonor.Type;
                        cmd.Parameters.Add("@add1", SqlDbType.NVarChar, 255).Value = aDonor.Address1;
                        cmd.Parameters.Add("@add2", SqlDbType.NVarChar, 255).Value = aDonor.Address2;
                        cmd.Parameters.Add("@city", SqlDbType.NVarChar, 255).Value = aDonor.City;
                        cmd.Parameters.Add("@pcode", SqlDbType.NVarChar, 255).Value = aDonor.PostCode;
                        cmd.Parameters.Add("@county", SqlDbType.NVarChar, 255).Value = aDonor.County;
                        cmd.Parameters.Add("@country", SqlDbType.NVarChar, 255).Value = aDonor.Country;
                        cmd.Parameters.Add("@tel", SqlDbType.NVarChar, 255).Value = aDonor.TelephoneHome;
                        cmd.Parameters.Add("@tel_work", SqlDbType.NVarChar, 255).Value = aDonor.TelephoneWork;
                        cmd.Parameters.Add("@mobile", SqlDbType.NVarChar, 50).Value = aDonor.TelephoneMobile;
                        cmd.Parameters.Add("@ntuserwhoadded", SqlDbType.NVarChar, 50).Value = aDonor.CreatedBy;
                        cmd.Parameters.Add("@ereceipt", SqlDbType.Bit).Value = aDonor.CommunicationByEmail;
                        cmd.Parameters.Add("@gad", SqlDbType.Bit).Value = aDonor.GAD;
                        cmd.Parameters.Add("@gadVerbal", SqlDbType.Bit).Value = aDonor.GADVerbal;
                        cmd.Parameters.Add("@gadName", SqlDbType.NVarChar, 255).Value = aDonor.GADName;
                        cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = aDonor.Email;
                        cmd.Parameters.Add("@ARB_ENG", SqlDbType.NVarChar, 50).Value = aDonor.LanguageSpoken;

                        //set up output parameters
                        SqlParameter donorNotFound = new SqlParameter("@o_donorNotFound", SqlDbType.Bit);
                        donorNotFound.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(donorNotFound);

                        //execute the query
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        Debug.Write("wat is de value;" + donorNotFound.Value);

                        if ((bool)donorNotFound.Value)
                            throw new DonorNotFoundException();

                    }
                }
                finally
                {
                    if (conn != null)
                        conn.Dispose();

                    if (cmd != null)
                        cmd.Dispose();
                }

            }
            else
            {
                throw new InvalidDonorException();
            }
        }


        public void DeleteDonor(int donorID)
        {
            SqlConnection myConnection = null;
            SqlCommand oCmd = null;

            var con = ConfigurationManager.ConnectionStrings["October2001"].ToString();
            using (myConnection = new SqlConnection(con))
            {
                string oString = string.Format("DELETE FROM mail WHERE CODE = {0}", donorID);
                try
                {
                    using (oCmd = new SqlCommand(oString, myConnection))
                    {
                        myConnection.Open();
                        var success = oCmd.ExecuteNonQuery();
                        myConnection.Close();
                        //if not 1 then record is not found/deleted
                        if (success != 1)
                            throw new DonorNotFoundException();

                    }
                }
                finally
                {
                    if (myConnection != null)
                        myConnection.Dispose();

                    if (oCmd != null)
                        oCmd.Dispose();

                }
            }
        }

    }
}
