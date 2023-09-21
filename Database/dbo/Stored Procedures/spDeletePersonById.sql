CREATE PROCEDURE [dbo].[spDeletePersonById]
	@Id int
AS
BEGIN

	Delete Person
	where id = @Id

END
