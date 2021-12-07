CREATE TABLE [CharacterLog] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [JobClass]            NVARCHAR (50) NOT NULL,
    [RaidDPS]             FLOAT (53)    NOT NULL,
    [ActualDPS]             FLOAT (53)    NOT NULL,
    [TotalDamage]     FLOAT (53)    NOT NULL,
    [PercentActive]   FLOAT (53)    NOT NULL,
    [HPS]              FLOAT (53)    NOT NULL,
    [OverhealPercent] FLOAT (53)    NOT NULL,
    [DamageTaken]     FLOAT (53)    NOT NULL,
    [CharacterId]     INT           NOT NULL,
    [LogParseId]           INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CharacterLog_CharacterId] FOREIGN KEY ([CharacterId]) REFERENCES [dbo].[Character] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_CharacterLog_LogId] FOREIGN KEY ([LogParseId]) REFERENCES [dbo].[LogParse] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

