USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Images_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Images_update;
GO

CREATE PROCEDURE dbo.POS_Images_update
    @ImageID INT,
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
    UPDATE POS_Images
    SET     FK_ImageCategoryID = @FK_ImageCategoryID,
    FK_ItemID = @FK_ItemID,
    FileSystemPath = @FileSystemPath,
    RelativePath = @RelativePath,
    ImageName = @ImageName,
    FileExtension = @FileExtension,
    ImageUrl = @ImageUrl,
    LocalUrl = @LocalUrl,
    DateUpdated = @DateUpdated
    WHERE ImageID = @ImageID;

    SELECT *
    FROM POS_Images
    WHERE ImageID = @ImageID;
END
GO