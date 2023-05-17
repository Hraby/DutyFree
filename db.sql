create database dutyfree;
use dutyfree;

Create table Products(
    ProductId INT Primary Key identity,
    DateCreated DATETIME,
    CreatedBy INT,
    DateUpdated DATETIME,
    UpdatedBy INT,
    IsDeleted BIT,
    Name NVARCHAR(512),
    Price INT,
    Quantity INT,
    ImageUrl NVARCHAR(1024)
);

CREATE PROCEDURE ProcProducts
AS
BEGIN
    SELECT *
    FROM Products
    WHERE IsDeleted = 0
END

CREATE PROCEDURE ProcProductInsert
    @Name NVARCHAR(512),
    @ImageUrl NVARCHAR(1024),
    @Quantity INT,
    @Price INT
AS
BEGIN
    INSERT INTO Products (DateCreated, CreatedBy, DateUpdated, UpdatedBy, IsDeleted, Name, Price, Quantity, ImageUrl)
    VALUES (GETDATE(), 0, GETDATE(), 0, 0, @Name, @Price, @Quantity, @ImageUrl)
END

CREATE PROCEDURE ProcProductEdit
    @ProductId INT,
    @Name NVARCHAR(512),
    @ImageUrl NVARCHAR(1024),
    @Quantity INT,
    @Price INT
AS
BEGIN
    UPDATE Products
    SET
        Name = @Name,
        ImageUrl = ISNULL(@ImageUrl, ImageUrl),
        Quantity = @Quantity,
        Price = @Price,
        DateUpdated = GETDATE(),
        UpdatedBy = 0
    WHERE ProductId = @ProductId
END

CREATE PROCEDURE ProcProductDelete
    @ProductId INT
AS
BEGIN
    UPDATE Products
    SET
        IsDeleted = 1,
        DateUpdated = GETDATE()
    WHERE ProductId = @ProductId
END

