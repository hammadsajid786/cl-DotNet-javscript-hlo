SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO


CREATE PROCEDURE [dbo].[usp_insertDistrictData] --'2019-01-11','2019-07-12' 
    @location_id INT,
    @location_name NVARCHAR(500),
    @active BIT,
    @OperationStatus INT
AS
BEGIN

    IF (@OperationStatus = 0)
    BEGIN

        IF NOT EXISTS
        (
            SELECT *
            FROM dbo.lrmis_web_location
            WHERE location_id = @location_id
        )
        BEGIN
            INSERT INTO [dbo].[lrmis_web_location]
            (
                [location_id],
                [location_name],
                [digitization_progress_percentage],
                [active],
                [created_date]
            )
            VALUES
            (@location_id, @location_name, 0, @active, GETDATE());
        END;
    END;
    ELSE
    BEGIN
        UPDATE [dbo].[lrmis_web_location]
        SET [location_name] = @location_name,
            [active] = @active,
            [modified_date] = GETDATE()
        WHERE location_id = @location_id;
    END;
END;
GO
