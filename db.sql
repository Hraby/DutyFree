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

insert into Products(DateCreated, CreatedBy, DateUpdated, UpdatedBy, IsDeleted, Name, Price, Quantity)
values (GETDATE(), 1, GETDATE(), 1, 0, N'Kofola', 29, 5),
       (GETDATE(), 1, GETDATE(), 1, 0, N'Cola', 29,4)

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

CREATE TABLE Users(
    UserId int primary key identity,
    Name nvarchar(256),
    Email nvarchar(256),
    ImageUrl varchar(1024),
    Role int
);

CREATE TABLE Orders(
    OrderId int primary key identity,
    DateCreated datetime,
    Name int,
    Price int,
    UserId int,
    ProductId int
)

alter table Orders add foreign key (UserId) references Users(UserId);
alter table Orders add foreign key (ProductId) references Products(ProductId);

insert into Users(Name, Email, ImageUrl, Role) values (N'Filip', N'filip.dlouhy@gmail.com', null, 1 ),
                                                      (N'Vašek', N'vasek.dolezal@gmail.com', null, 0),
                                                      (N'Aneta', N'aneta.kralova@gmail.com',null,0);
CREATE PROCEDURE ProcUsers
AS
BEGIN
    SELECT *
    FROM Users
END