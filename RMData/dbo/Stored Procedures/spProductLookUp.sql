CREATE PROCEDURE [dbo].[spProductLookUp]
	@Id	int,
	@ProductName nvarchar(100),
	@Description nvarchar(max),
	@RetailPrice money,
	@QuantityInStock int,
	@IsTaxable bit,
	@ProductImage nvarchar(100)
	

AS
begin
	set nocount on;

	SELECT @Id, @ProductName, @Description, @RetailPrice, @QuantityInStock, @IsTaxable, @ProductImage
	from [dbo].[Product]
	where ProductName = @ProductName;
end
