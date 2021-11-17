CREATE PROCEDURE [dbo].[spIventory_GetAll]
	
AS
begin
	set nocount on;
	select [ProductId], [Quantity], [PurchasePrice], [PurchaseDate]
	from dbo.Inventory;

end
