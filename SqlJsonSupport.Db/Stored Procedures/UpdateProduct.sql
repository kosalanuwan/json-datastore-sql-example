-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.UpdateProduct 
	-- Add the parameters for the stored procedure here
	@Charges NTEXT = NULL, 
	@Features NTEXT = NULL,
	@Documents NTEXT = NULL,
	@Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE dbo.Products SET
	Charges = @Charges,
	Features = @Features,
	Documents = @Documents
	WHERE Id = @Id;

END