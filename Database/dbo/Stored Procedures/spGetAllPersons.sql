CREATE PROCEDURE [dbo].[spGetAllPersons]
AS
BEGIN

    SELECT
    Id,
	[Name],
    [Address],
    [PhoneNumber],
    [EmailAddress] 
	FROM Person;

END