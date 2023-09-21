CREATE PROCEDURE [dbo].[spGetPersonsByName]
	@Name NVarchar(30)
AS
BEGIN

	SELECT [Name],
    [Address],
    [PhoneNumber],
    [EmailAddress] 
	FROM Person
	WHERE [Name] Like '%' + @Name + '%';


END
