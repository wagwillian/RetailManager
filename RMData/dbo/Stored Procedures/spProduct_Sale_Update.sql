CREATE PROCEDURE [dbo].[spProduct_Sale_Update]
	@Id int,
	@QuantityInStock int
	
AS
Begin
UPDATE Product
set
	QuantityInStock = @QuantityInStock

where @Id = Id
end
