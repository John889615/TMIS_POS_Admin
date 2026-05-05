USE [TMIS_BlueSafaris]
GO

IF OBJECT_ID('dbo.POS_Images_insert_replace', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Images_insert_replace;
GO

CREATE PROCEDURE dbo.POS_Images_insert_replace
    @FK_ImageCategoryID INT,
    @FK_ItemID INT,
    @FileSystemPath VARCHAR(255),
    @RelativePath VARCHAR(255),
    @ImageName VARCHAR(255),
    @FileExtension VARCHAR(255),
    @ImageUrl VARCHAR(255),
    @LocalUrl VARCHAR(255),
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ExistingID INT;

    SELECT TOP 1 @ExistingID = ImageID
    FROM dbo.POS_Images
    WHERE FK_ItemID = @FK_ItemID
      AND RelativePath = @RelativePath;

    IF @ExistingID IS NOT NULL
    BEGIN
        UPDATE dbo.POS_Images
        SET
            FK_ImageCategoryID = @FK_ImageCategoryID,
            FileSystemPath     = @FileSystemPath,
            ImageName          = @ImageName,
            FileExtension      = @FileExtension,
            ImageUrl           = @ImageUrl,
            LocalUrl           = @LocalUrl,
            DateUpdated        = ISNULL(@DateUpdated, GETDATE())
            -- keep DateCreated as-is on updates
        WHERE ImageID = @ExistingID;

        SELECT *
        FROM dbo.POS_Images
        WHERE ImageID = @ExistingID;

        RETURN;
    END

    INSERT INTO dbo.POS_Images
    (
        FK_ImageCategoryID,
        FK_ItemID,
        FileSystemPath,
        RelativePath,
        ImageName,
        FileExtension,
        ImageUrl,
        LocalUrl,
        DateCreated,
        DateUpdated
    )
    VALUES
    (
        @FK_ImageCategoryID,
        @FK_ItemID,
        @FileSystemPath,
        @RelativePath,
        @ImageName,
        @FileExtension,
        @ImageUrl,
        @LocalUrl,
        ISNULL(@DateCreated, GETDATE()),
        @DateUpdated
    );

    DECLARE @NewID INT = SCOPE_IDENTITY();

    SELECT *
    FROM dbo.POS_Images
    WHERE ImageID = @NewID;
END
GO