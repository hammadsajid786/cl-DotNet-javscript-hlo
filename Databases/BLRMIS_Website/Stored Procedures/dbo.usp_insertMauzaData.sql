SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO


CREATE PROCEDURE [dbo].[usp_insertMauzaData] --'2019-01-11','2019-07-12' 

    @mauza_id INT,
    @mauza_name NVARCHAR(200),
    @district_id INT,
    @tehsil_id INT,
    @active BIT,
    @OperationStatus INT
AS
BEGIN

    IF (@OperationStatus = 0)
    BEGIN

        IF NOT EXISTS
        (
            SELECT *
            FROM dbo.lrmis_web_mauza
            WHERE mauza_id = @mauza_id
        )
        BEGIN
            INSERT INTO [dbo].[lrmis_web_mauza]
            (
                [mauza_id],
                [mauza_name],
                [tehsil_id],
                [active],
                [created_date]
            )
            VALUES
            (@mauza_id, @mauza_name, @tehsil_id, @active, GETDATE());
        END;
    END;
    ELSE
    BEGIN
        UPDATE [dbo].[lrmis_web_mauza]
        SET [mauza_name] = @mauza_name,
            [tehsil_id] = @tehsil_id,
            [active] = @active,
            [modified_date] = GETDATE()
        WHERE [mauza_id] = @mauza_id;
    END;
END;
GO
