SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO


CREATE PROCEDURE [dbo].[usp_insertTehsilData] --'2019-01-11','2019-07-12' 

    @district_id INT,
    @tehsil_id INT,
    @tehsil_name NVARCHAR(500),
    @active BIT,
    @OperationStatus INT
AS
BEGIN

    IF (@OperationStatus = 0)
    BEGIN

        IF NOT EXISTS
        (
            SELECT *
            FROM dbo.lrmis_web_tehsil
            WHERE tehsil_id = @tehsil_id
        )
        BEGIN
            INSERT INTO [dbo].[lrmis_web_tehsil]
            (
                [tehsil_id],
                [tehsil_name],
                [active],
                [district_id],
                [created_date]
            )
            VALUES
            (@tehsil_id, @tehsil_name, @active, @district_id, GETDATE());
        END;
    END;
    ELSE
    BEGIN
        UPDATE [dbo].[lrmis_web_tehsil]
        SET [tehsil_name] = @tehsil_name,
            [active] = @active,
            [district_id] = @district_id,
            [modified_date] = GETDATE()
        WHERE [tehsil_id] = @tehsil_id;
    END;
END;
GO
