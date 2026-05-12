USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_Images_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Images_insert;
GO

CREATE PROCEDURE dbo.POS_Images_insert
    @FK_ImageCategoryID INT,
    @FK_ItemID INT,
    @FileSystemPath VARCHAR(255),
    @RelativePath VARCHAR(255),
    @ImageName VARCHAR(255),
    @FileExtension VARCHAR(255),
    @ImageUrl VARCHAR(255),
    @LocalUrl VARCHAR(255),
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (ImageID INT);

    INSERT INTO POS_Images (FK_ImageCategoryID, FK_ItemID, FileSystemPath, RelativePath, ImageName, FileExtension, ImageUrl, LocalUrl, DateCreated, DateUpdated)
    OUTPUT INSERTED.ImageID INTO @Inserted
    VALUES (@FK_ImageCategoryID, @FK_ItemID, @FileSystemPath, @RelativePath, @ImageName, @FileExtension, @ImageUrl, @LocalUrl, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_Images
    WHERE ImageID = 
    (
        SELECT TOP 1 ImageID
        FROM @Inserted
    );
END
GO