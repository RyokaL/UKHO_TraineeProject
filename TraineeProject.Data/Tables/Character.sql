CREATE TABLE [Character] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [CharacterName] NVARCHAR (25) NOT NULL,
    [WorldServer]   NVARCHAR (50) NOT NULL,
    [UserId]           INT           NULL,
    [Private]        BIT           DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

