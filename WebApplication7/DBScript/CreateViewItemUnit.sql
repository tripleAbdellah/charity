CREATE VIEW [dbo].[vw_ItemUnit]
AS
SELECT        dbo.tbItem.Name AS ItemName, dbo.tbItemUnit.ItemUnitID, dbo.tbItemUnit.Qty, dbo.tbItemUnit.UnitPrice, dbo.tbItemUnit.ItemID, dbo.tbItemUnit.UnitID, 
                         dbo.tbUnit.Name AS UnitName, dbo.tbItemUnit.Weight, dbo.tbItemUnit.Dimensions, dbo.tbItemUnit.Description
FROM            dbo.tbItem INNER JOIN
                         dbo.tbItemUnit ON dbo.tbItem.ItemID = dbo.tbItemUnit.ItemID INNER JOIN
                         dbo.tbUnit ON dbo.tbItemUnit.UnitID = dbo.tbUnit.UnitID