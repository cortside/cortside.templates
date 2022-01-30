PRINT 'Before TRY'
BEGIN TRY
	BEGIN TRAN
	PRINT 'First Statement in the TRY block'
BEGIN TRANSACTION;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220130045319_Orders')
BEGIN
    CREATE TABLE [dbo].[Order] (
        [OrderId] int NOT NULL IDENTITY,
        [CustomerId] int NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreateSubjectId] uniqueidentifier NOT NULL,
        [LastModifiedDate] datetime2 NOT NULL,
        [LastModifiedSubjectId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Order] PRIMARY KEY ([OrderId]),
        CONSTRAINT [FK_Order_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId]),
        CONSTRAINT [FK_Order_Subject_CreateSubjectId] FOREIGN KEY ([CreateSubjectId]) REFERENCES [dbo].[Subject] ([SubjectId]),
        CONSTRAINT [FK_Order_Subject_LastModifiedSubjectId] FOREIGN KEY ([LastModifiedSubjectId]) REFERENCES [dbo].[Subject] ([SubjectId])
    );
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220130045319_Orders')
BEGIN
    CREATE TABLE [dbo].[OrderItem] (
        [OrderItemId] int NOT NULL IDENTITY,
        [Sku] nvarchar(10) NULL,
        [Quantity] int NOT NULL,
        [OrderId] int NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreateSubjectId] uniqueidentifier NOT NULL,
        [LastModifiedDate] datetime2 NOT NULL,
        [LastModifiedSubjectId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_OrderItem] PRIMARY KEY ([OrderItemId]),
        CONSTRAINT [FK_OrderItem_Order_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([OrderId]),
        CONSTRAINT [FK_OrderItem_Subject_CreateSubjectId] FOREIGN KEY ([CreateSubjectId]) REFERENCES [dbo].[Subject] ([SubjectId]),
        CONSTRAINT [FK_OrderItem_Subject_LastModifiedSubjectId] FOREIGN KEY ([LastModifiedSubjectId]) REFERENCES [dbo].[Subject] ([SubjectId])
    );
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220130045319_Orders')
BEGIN
    CREATE INDEX [IX_Order_CreateSubjectId] ON [dbo].[Order] ([CreateSubjectId]);
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220130045319_Orders')
BEGIN
    CREATE INDEX [IX_Order_CustomerId] ON [dbo].[Order] ([CustomerId]);
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220130045319_Orders')
BEGIN
    CREATE INDEX [IX_Order_LastModifiedSubjectId] ON [dbo].[Order] ([LastModifiedSubjectId]);
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220130045319_Orders')
BEGIN
    CREATE INDEX [IX_OrderItem_CreateSubjectId] ON [dbo].[OrderItem] ([CreateSubjectId]);
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220130045319_Orders')
BEGIN
    CREATE INDEX [IX_OrderItem_LastModifiedSubjectId] ON [dbo].[OrderItem] ([LastModifiedSubjectId]);
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220130045319_Orders')
BEGIN
    CREATE INDEX [IX_OrderItem_OrderId] ON [dbo].[OrderItem] ([OrderId]);
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220130045319_Orders')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220130045319_Orders', N'6.0.1');
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
