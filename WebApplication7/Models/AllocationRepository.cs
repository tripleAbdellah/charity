using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Diagnostics;

namespace DonorApp.Models
{
    public class AllocationRepository : IAllocationRepository
    {

        public IEnumerable<ShoppingCategory> GetShoppingCategoryList()
        {

            List<ShoppingCategory> shoppingCategoryList = new List<ShoppingCategory>();

            var con = ConfigurationManager.ConnectionStrings["October2001"].ToString();

            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "Select * from tbItem WHERE (Show = 1)";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);

                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        ShoppingCategory shoppingCategory = new ShoppingCategory();
                        shoppingCategory.itemID = (int)oReader["itemID"];
                        shoppingCategory.name = oReader["Name"].ToString();
                        shoppingCategory.dimensions = oReader["Dimensions"].ToString();
                        shoppingCategory.weight = oReader["Weight"].ToString();
                        shoppingCategory.description = oReader["Description"].ToString();
                        shoppingCategory.shoppingSubCategoryList = GetShoppingSubCategoryList((int)oReader["itemID"]);
                        shoppingCategoryList.Add(shoppingCategory);
                    }
                    
                    myConnection.Close();
                }
            }
            return shoppingCategoryList;
        }


        private IEnumerable<ShoppingSubCategory> GetShoppingSubCategoryList(int itemID)
        {
            List<ShoppingSubCategory> shoppingSubCategoryList = new List<ShoppingSubCategory>();

            var con = ConfigurationManager.ConnectionStrings["October2001"].ToString();

            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "select * from vw_itemUnit where itemID =  @itemID";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                oCmd.Parameters.AddWithValue("@itemID", itemID);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        ShoppingSubCategory shoppingSubCategory = new ShoppingSubCategory();
                        shoppingSubCategory.unitID = (int)oReader["UnitID"];
                        shoppingSubCategory.unitName = oReader["UnitName"].ToString();
                        shoppingSubCategory.unitPrice = (decimal)oReader["UnitPrice"];
                        shoppingSubCategoryList.Add(shoppingSubCategory);
                    }

                    myConnection.Close();
                }
            }

            return shoppingSubCategoryList;
        }

    }
}