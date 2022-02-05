PRINT 'Before TRY'
BEGIN TRY
	BEGIN TRAN
	PRINT 'First Statement in the TRY block'
BEGIN TRANSACTION;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205015124_Orders3')
BEGIN
    ALTER TABLE [dbo].[Address] ADD [CreateSubjectId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205015124_Orders3')
BEGIN
    ALTER TABLE [dbo].[Address] ADD [CreatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205015124_Orders3')
BEGIN
    ALTER TABLE [dbo].[Address] ADD [LastModifiedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205015124_Orders3')
BEGIN
    ALTER TABLE [dbo].[Address] ADD [LastModifiedSubjectId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205015124_Orders3')
BEGIN
    CREATE INDEX [IX_Address_CreateSubjectId] ON [dbo].[Address] ([CreateSubjectId]);
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205015124_Orders3')
BEGIN
    CREATE INDEX [IX_Address_LastModifiedSubjectId] ON [dbo].[Address] ([LastModifiedSubjectId]);
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205015124_Orders3')
BEGIN
    ALTER TABLE [dbo].[Address] ADD CONSTRAINT [FK_Address_Subject_CreateSubjectId] FOREIGN KEY ([CreateSubjectId]) REFERENCES [dbo].[Subject] ([SubjectId]);
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205015124_Orders3')
BEGIN
    ALTER TABLE [dbo].[Address] ADD CONSTRAINT [FK_Address_Subject_LastModifiedSubjectId] FOREIGN KEY ([LastModifiedSubjectId]) REFERENCES [dbo].[Subject] ([SubjectId]);
END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205015124_Orders3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220205015124_Orders3', N'6.0.1');
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
