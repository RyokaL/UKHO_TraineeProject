CREATE TABLE [LogParse] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [InstanceName] NVARCHAR (1000) NOT NULL,
    [TimeTaken]    INT             NOT NULL,
    [Succeeded]     BIT             NULL,
    [DateUploaded] DATETIME        NOT NULL,
    [Private] BIT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

