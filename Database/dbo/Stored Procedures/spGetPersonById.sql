Create proc spGetPersonById
@Id INT
AS
BEGIN
	
	SELECT [Name],
    [Address],
    [PhoneNumber],
    [EmailAddress] 
	FROM Person
	WHERE Id = @Id;

END