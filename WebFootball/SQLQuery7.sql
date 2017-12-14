CREATE TABLE [dbo].[Game] (
    [Id]              INT  IDENTITY (1, 1) NOT NULL,
    [FirstTeam]       INT  NOT NULL,
    [SecondTeam]      INT  NOT NULL,
    [FirstTeamScore]  INT  DEFAULT ((0)) NOT NULL,
    [SecondTeamScore] INT  DEFAULT ((0)) NOT NULL,
    [Date]            datetime NOT NULL,
    [IsFinished]      BIT  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Game_Team] FOREIGN KEY ([FirstTeam]) REFERENCES [dbo].[Team] ([Id]),
    CONSTRAINT [FK1_Game_Team] FOREIGN KEY ([SecondTeam]) REFERENCES [dbo].[Team] ([Id])
);


