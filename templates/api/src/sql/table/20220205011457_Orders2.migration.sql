PRINT 'Before TRY'
BEGIN TRY
	BEGIN TRAN
	PRINT 'First Statement in the TRY block'
BEGIN TRANSACTION;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205011457_Orders2')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[Order]') AND [c].[name] = N'CustomerId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [dbo].[Order] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [dbo].[Order] ALTER COLUMN [CustomerId] int NULL;
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205011457_Orders2')
BEGIN
    ALTER TABLE [dbo].[Order] ADD [AddressId] int NULL;
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205011457_Orders2')
BEGIN
    ALTER TABLE [dbo].[Order] ADD [Status] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205011457_Orders2')
BEGIN
    CREATE TABLE [dbo].[Address] (
        [AddressId] int NOT NULL IDENTITY,
        [Street] nvarchar(max) NULL,
        [City] nvarchar(max) NULL,
        [State] nvarchar(max) NULL,
        [Country] nvarchar(max) NULL,
        [ZipCode] nvarchar(max) NULL,
        CONSTRAINT [PK_Address] PRIMARY KEY ([AddressId])
    );
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205011457_Orders2')
BEGIN
    CREATE INDEX [IX_Order_AddressId] ON [dbo].[Order] ([AddressId]);
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205011457_Orders2')
BEGIN
    ALTER TABLE [dbo].[Order] ADD CONSTRAINT [FK_Order_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([AddressId]);
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205011457_Orders2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220205011457_Orders2', N'6.0.1');
END;

COMMIT;

	PRINT 'Last Statement in the TRY block'
	COMMIT TRAN
END TRY
BEGIN CATCH
    PRINT 'In CATCH Block'
    IF(@@TRANCOUNT > 0)
        ROLLBACK TRAN;

    THROW; -- Raise error to the client.
END CATCH
PRINT 'After END CATCH'
GO
