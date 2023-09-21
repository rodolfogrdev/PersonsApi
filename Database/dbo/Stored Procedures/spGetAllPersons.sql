CREATE PROCEDURE [dbo].[spGetAllPersons]
AS
BEGIN

    SELECT
	[Name],
    [Address],
    [PhoneNumber],
    [EmailAddress] 
	FROM Person;

END