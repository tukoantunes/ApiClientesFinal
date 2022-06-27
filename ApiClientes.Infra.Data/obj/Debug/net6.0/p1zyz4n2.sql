IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [CLIENTE] (
    [IDCLIENTE] uniqueidentifier NOT NULL,
    [NOME] nvarchar(100) NOT NULL,
    [EMAIL] nvarchar(50) NOT NULL,
    [CPF] nvarchar(15) NOT NULL,
    [DATANASCIMENTO] datetime2 NOT NULL,
    CONSTRAINT [PK_CLIENTE] PRIMARY KEY ([IDCLIENTE])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220531131725_Initial', N'6.0.5');
GO

COMMIT;
GO

