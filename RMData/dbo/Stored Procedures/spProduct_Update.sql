CREATE PROCEDURE [dbo].[spProduct_Update]
	@Id INT,           
    @ProductName NVARCHAR (100), 
    @Description NVARCHAR (MAX),
    @RetailPrice MONEY,
    @QuantityInStock INT,            
    @ProductImage NVARCHAR(500)
AS
Begin    
    update dbo.Product
    set ProductName = @ProductName,
        [Description] = @Description,
        RetailPrice = @RetailPrice,
        QuantityInStock = @QuantityInStock,
        ProductImage = @ProductImage
    where Id = @Id
end
           
        
