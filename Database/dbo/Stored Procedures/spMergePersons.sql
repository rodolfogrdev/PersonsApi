
CREATE proc [dbo].[spMergePersons]
	@Persons UTPerson READONLY
AS
BEGIN
	
	MERGE INTO Person AS T--Target
    USING @Persons AS S--Parameter
        ON T.Id = S.Id
    WHEN MATCHED
        THEN UPDATE SET
			T.[Name] = S.[Name],
			T.[Address] = S.[Address],
			T.[PhoneNumber] = S.[PhoneNumber],
			T.[EmailAddress] = S.[EmailAddress]
	WHEN NOT MATCHED
		THEN INSERT
		(
			[Name],[Address],[PhoneNumber],[EmailAddress]
		)
		VALUES (
			S.[Name],S.[Address],S.[PhoneNumber],S.[EmailAddress]
		);
	--Example of use:
	/*
		Declare @Persons UTPerson;

		Insert into @Persons
		(Id,[Name],[Address],[PhoneNumber],[EmailAddress])
		values
		(1,'Armando Zaragoza','55-12-34-56-78','mail@domain.gob'),
		(null,'Santiago Gutierrez','55-12-34-23-86','mail2@domain.gob')

		exec spMergePersons @Persons;
	*/
END