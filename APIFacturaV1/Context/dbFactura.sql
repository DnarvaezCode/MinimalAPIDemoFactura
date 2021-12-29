CREATE DATABASE APIFactura

USE APIFactura

CREATE TABLE Categoria (
    [Id] int NOT NULL IDENTITY,
	[Nombre] VARCHAR(500) not null,
	[Estado] bit not null
    CONSTRAINT [Id_Categoria] PRIMARY KEY ([Id])
);
GO

CREATE TABLE Producto (
    [Id] int NOT NULL IDENTITY,
	[CategoriaId] int  not null,
	[Nombre] VARCHAR(max) not null,
	[Descripcion] VARCHAR(max) not null,
    [Precio] decimal not null,
    [Stock] float not null,
    [Imagen] varbinary(max) null,
    [Estado] bit not null
    CONSTRAINT [Id_Producto] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Producto_Categoria] FOREIGN KEY ([CategoriaId]) REFERENCES [Categoria] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE Cliente (
    [Id] int NOT NULL IDENTITY,
	[Nombres] VARCHAR(max) not null,
	[Apellidos] VARCHAR(max) not null,
    [Direccion] VARCHAR(max) not null,
    [Telefono] VARCHAR(20) not null,
    [Correo] VARCHAR(100) not null,
    [FechaNacimiento] Datetime not null,
    [Estado] bit not null
    CONSTRAINT [Id_Cliente] PRIMARY KEY ([Id])
);
GO

CREATE TABLE Factura (
    [Id] int NOT NULL IDENTITY,
	[ClienteId] int  not null,
	[NumeroFactura] VARCHAR(max) not null,
	[Fecha] Datetime not null
    CONSTRAINT [Id_Factura] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Factura_Cliente] FOREIGN KEY ([ClienteId]) REFERENCES [Cliente] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE DetalleFactura (
    [Id] int NOT NULL IDENTITY,
	[FacturaId] int  not null,
	[ProductoId] int  not null,
	[Descripcion] VARCHAR(max) not null,
	[Cantidad] float not null,
    [Precio] decimal not null,
    [SubTotal] decimal not null,
    CONSTRAINT [Id_DetalleFactura] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DetalleFactura_Factura] FOREIGN KEY ([FacturaId]) REFERENCES [Factura] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DetalleFactura_Producto] FOREIGN KEY ([ProductoId]) REFERENCES [Producto] ([Id]) ON DELETE NO ACTION
);
GO

CREATE PROCEDURE [dbo].[spInsertarCliente]
    @Id int output,
	@Nombres VARCHAR(max),
	@Apellidos VARCHAR(max), 
    @Direccion VARCHAR(max),
    @Telefono VARCHAR(20),
    @Correo VARCHAR(100),
    @FechaNacimiento Datetime
AS
BEGIN
	INSERT INTO Cliente (Nombres, Apellidos, Direccion, Telefono, Correo, FechaNacimiento, Estado)
	VALUES(@Nombres, @Apellidos, @Direccion, @Telefono, @Correo, @FechaNacimiento, 1)
    set @Id = CAST(SCOPE_IDENTITY() as int)
END
GO

CREATE PROCEDURE [dbo].[spActualizarCliente]
    @Id int,
	@Nombres VARCHAR(max),
	@Apellidos VARCHAR(max), 
    @Direccion VARCHAR(max),
    @Telefono VARCHAR(20),
    @Correo VARCHAR(100),
    @FechaNacimiento Datetime
AS
BEGIN
	UPDATE Cliente 
    SET Nombres = @Nombres, Apellidos = @Apellidos, Direccion = @Direccion, Telefono = @Telefono, Correo = @Correo, FechaNacimiento = @FechaNacimiento 
    where Id = @Id
END
GO

CREATE PROCEDURE [dbo].[spEliminarCliente]
    @Id int
AS
BEGIN
    DELETE FROM Cliente 
    where Id = @Id
END
GO

CREATE PROCEDURE [dbo].[spObtenerClientes]
AS
BEGIN
    SELECT * FROM Cliente c
	WHERE c.Estado = 1
END
GO

CREATE PROCEDURE [dbo].[spObtenerCliente]
@Id int
AS
BEGIN
    SELECT * FROM Cliente c
	WHERE c.Id = @Id
END
GO