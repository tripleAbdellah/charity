using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using DonorApp.Repositories;
using DonorApp.Models;

namespace DonarApp.Repositories
{
    public class AllocationsRepository : IAllocationsRepository
    {
        public List<Allocation> allocations = new List<Allocation>();
        /*
        public AllocationsRepository()
        {
            
            //Static / general Allocations
            //GetStaticAllocation(); 

            //Shopping List
           // GetAllocationShopping(); 

            //Qurbani list
           // GetAllocationQurbani(); 


            //Orphan List
           int DonorCode = 3343; 
           GetAllocationOrphans(DonorCode); 

        }
         */ 


        public IEnumerable<Allocation> GetAllAllocations()
        {
            //Static / general Allocations
            GetStaticAllocations(); 

            //Shopping List
            GetShoppingAllocations(); 

            //Qurbani list
            GetQurbaniAllocations(); 

            return allocations.AsEnumerable();
        }

        public IEnumerable<Allocation> GetAllAllocations(int DonorCode)
        {
            //Donor code is required to retrieve the associated Orphans

            //Static / general Allocations
            GetStaticAllocations();

            //Shopping List
            GetShoppingAllocations();

            //Qurbani list
            GetQurbaniAllocations();

            GetOrphanAllocations(DonorCode); 


            return allocations.AsEnumerable();
        }


        public void GetStaticAllocations()
        {
            /*static General fields*/
            allocations.Add(new Allocation("General"));
            allocations.Add(new Allocation("Zakat"));
            allocations.Add(new Allocation("Sadaqat"));
            allocations.Add(new Allocation("Fitrana"));
            allocations.Add(new Allocation("ChildrenGen"));
            allocations.Add(new Allocation("FamiliesGen"));
            allocations.Add(new Allocation("Food"));
            allocations.Add(new Allocation("Lillah"));
            allocations.Add(new Allocation("Admin"));
            allocations.Add(new Allocation("Interest"));
            allocations.Add(new Allocation("SchoolKit"));
            allocations.Add(new Allocation("EidGift"));
            allocations.Add(new Allocation("Other"));
        
        }

        public void GetShoppingAllocations()
        {

            SqlConnection myConnection = null;
            SqlDataReader oReader = null;
            SqlCommand oCmd = null;

            var con = ConfigurationManager.ConnectionStrings["October2001"].ToString();
            try
            {

                using (myConnection = new SqlConnection(con))
                {

                   String oString = "select vw_itemUnit.ItemName as Category, vw_ItemUnit.UnitName as SubCategory , vw_ItemUnit.UnitPrice as Price from vw_itemUnit inner join tbItem on vw_ItemUnit.ItemID = tbItem.ItemID where tbitem.Show = 1; ";
                    oCmd = new SqlCommand(oString, myConnection);

                    myConnection.Open();
                    using (oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            allocations.Add(new ShoppingAllocation(oReader["Category"].ToString(), oReader["SubCategory"].ToString(), (float)Convert.ToSingle(oReader["Price"])));
                        }
                    }
                    myConnection.Close();
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


        public void GetOrphanAllocations(int DonorCode)
        {

            SqlConnection myConnection = null;
            SqlDataReader oReader = null;
            SqlCommand oCmd = null;

            var con = ConfigurationManager.ConnectionStrings["October2001"].ToString();
            try
            {
                
                using (myConnection = new SqlConnection(con))
                {

                    String oString = "select ORN_CODE as OrphanID, ORN_NAME as OrphanName, Case Category when 3 then 'Family' else 'Child' end as category , Due as Due from orphan where OriginalDonorCode = @donor_code ";
                    oCmd = new SqlCommand(oString, myConnection);

                    oCmd.Parameters.Add("@donor_code", SqlDbType.Int, 255).Value = DonorCode;
                    myConnection.Open();

                    using (oReader = oCmd.ExecuteReader())
                    {

                        // if (!oReader.HasRows)
                        //    throw new NoOrphansFoundException();

                        while (oReader.Read())
                        {
                            allocations.Add(new OrphanAllocation((int)Convert.ToSingle(oReader["OrphanID"]), oReader["OrphanName"].ToString(), oReader["Category"].ToString(), (DateTime)oReader["Due"]));
                        }
                    }
                    myConnection.Close();
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


        public void GetQurbaniAllocations()
        {

            SqlConnection myConnection = null;
            SqlDataReader oReader = null;
            SqlCommand oCmd = null;

            var con = ConfigurationManager.ConnectionStrings["October2001"].ToString();
            try
            {

                using (myConnection = new SqlConnection(con))
                {

                    String oString = "SELECT Code, Description FROM [System Codes] WHERE (Area = N'Qurbani')";
                    oCmd = new SqlCommand(oString, myConnection);
                    myConnection.Open();
                    using (oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {

                           allocations.Add(new QurbaniAllocation(GetQurbaniCountry(oReader["Description"].ToString()), GetQurbaniAnimalType(oReader["Description"].ToString()), GetQurbaniAmount(oReader["Description"].ToString())));
                        }
                    }
                    myConnection.Close();
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



        private float GetQurbaniAmount(string rawText)
        {
            float qurbaniAmount = 0;

            qurbaniAmount = float.Parse(Regex.Match(rawText, @"\d+").Value);

            return qurbaniAmount;
        }

        private string GetQurbaniCountry(string rawText)
        {
            string qurbaniCountry = "";

            if (!rawText.Contains("Standard"))
            {
                qurbaniCountry = Regex.Match(rawText, @"(?<=in ).*").Value;
            }
            else
            {
                qurbaniCountry = "General";
            }

            return qurbaniCountry;
        }

        private string GetQurbaniAnimalType(string rawText)
        {
            string qurbaniAnimalType = "";

            if (!rawText.Contains("Standard"))
            {
                qurbaniAnimalType = Regex.Match(rawText, @"\s(?<=\) ).*(?=in )").Value;
            }
            else
            {
                qurbaniAnimalType = "Standard";
            }

            return qurbaniAnimalType.Trim();
        }


    }
}
