CREATE TABLE [dbo].[Person] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (150)   NULL,
    [Address]   NVARCHAR (150) NULL,
    [PhoneNumber] NVARCHAR(25) NULL,
    [EmailAddress] NVARCHAR(150) NULL,
    CONSTRAINT [PK_PERSON] PRIMARY KEY CLUSTERED ([Id] ASC)
);

