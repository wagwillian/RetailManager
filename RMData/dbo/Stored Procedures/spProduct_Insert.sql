CREATE PROCEDURE [dbo].[spProduct_Insert]
	@ProductName nvarchar (100),
	@Description nvarchar (MAX),
	@RetailPrice MONEY,
	@QuantityInStock int,
	@IsTaxable bit,
	@ProductImage nvarchar (500)

	
AS
begin
	set nocount on;

	insert into dbo.Product(ProductName, [Description], RetailPrice, QuantityInStock, IsTaxable, ProductImage)
	values (@ProductName, @Description, @RetailPrice, @QuantityInStock, @IsTaxable, @ProductImage)

	return SCOPE_IDENTITY()
end
