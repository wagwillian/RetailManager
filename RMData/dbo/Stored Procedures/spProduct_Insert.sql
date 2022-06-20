CREATE PROCEDURE [dbo].[spProduct_Insert]
	@Id int,
	@ProductName nvarchar (100),
	@Description nvarchar (MAX),
	@RetailPrice MONEY,
	@QuantityInStock int,
	@IsTaxable bit,
	@ProductImage nvarchar (500)

	
AS
begin
	set nocount on;

	insert into dbo.Product(Id, ProductName, [Description], RetailPrice, QuantityInStock, IsTaxable, ProductImage)
	values (@Id, @ProductName, @Description, @RetailPrice, @QuantityInStock, @IsTaxable, @ProductImage)
end
