
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/24/2015 18:26:24
-- Generated from EDMX file: V:\Documents\Visual Studio 2013\Source\Repos\HouseOfClans\HouseOfClans\HouseOfClans\Models\HouseOfClansModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HouseOfClans];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ClanGroup_Clan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClanGroups] DROP CONSTRAINT [FK_ClanGroup_Clan];
GO
IF OBJECT_ID(N'[dbo].[FK_ClanUser_Clan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClanUsers] DROP CONSTRAINT [FK_ClanUser_Clan];
GO
IF OBJECT_ID(N'[dbo].[FK_ClanUser_UserRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClanUsers] DROP CONSTRAINT [FK_ClanUser_UserRole];
GO
IF OBJECT_ID(N'[dbo].[FK_ClanWar_Clan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClanWars] DROP CONSTRAINT [FK_ClanWar_Clan];
GO
IF OBJECT_ID(N'[dbo].[FK_ClanWar_WarType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClanWars] DROP CONSTRAINT [FK_ClanWar_WarType];
GO
IF OBJECT_ID(N'[dbo].[FK_Rules_ToClan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rules] DROP CONSTRAINT [FK_Rules_ToClan];
GO
IF OBJECT_ID(N'[dbo].[FK_UserBlackList_Clan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserBlackLists] DROP CONSTRAINT [FK_UserBlackList_Clan];
GO
IF OBJECT_ID(N'[dbo].[FK_UserBlackList_ClanUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserBlackLists] DROP CONSTRAINT [FK_UserBlackList_ClanUser];
GO
IF OBJECT_ID(N'[dbo].[FK_WarLog_ClanUser_Attack]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WarLogs] DROP CONSTRAINT [FK_WarLog_ClanUser_Attack];
GO
IF OBJECT_ID(N'[dbo].[FK_WarLog_ClanUser_Defense]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WarLogs] DROP CONSTRAINT [FK_WarLog_ClanUser_Defense];
GO
IF OBJECT_ID(N'[dbo].[FK_WarLog_ClanWar]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WarLogs] DROP CONSTRAINT [FK_WarLog_ClanWar];
GO
IF OBJECT_ID(N'[dbo].[FK_WarRankings_ClanUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WarRankings] DROP CONSTRAINT [FK_WarRankings_ClanUser];
GO
IF OBJECT_ID(N'[dbo].[FK_WarRankings_ClanGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WarRankings] DROP CONSTRAINT [FK_WarRankings_ClanGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_WarRankings_ClanUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WarRankings] DROP CONSTRAINT [FK_WarRankings_ClanUser];
GO
IF OBJECT_ID(N'[dbo].[FK_WarRankings_ClanWar]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WarRankings] DROP CONSTRAINT [FK_WarRankings_ClanWar];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ActionLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionLogs];
GO
IF OBJECT_ID(N'[dbo].[Actions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Actions];
GO
IF OBJECT_ID(N'[dbo].[ClanGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClanGroups];
GO
IF OBJECT_ID(N'[dbo].[Clans]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clans];
GO
IF OBJECT_ID(N'[dbo].[ClanUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClanUsers];
GO
IF OBJECT_ID(N'[dbo].[ClanWarPicks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClanWarPicks];
GO
IF OBJECT_ID(N'[dbo].[ClanWars]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClanWars];
GO
IF OBJECT_ID(N'[dbo].[Rules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rules];
GO
IF OBJECT_ID(N'[dbo].[UserBlackLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserBlackLists];
GO
IF OBJECT_ID(N'[dbo].[UserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRoles];
GO
IF OBJECT_ID(N'[dbo].[WarLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WarLogs];
GO
IF OBJECT_ID(N'[dbo].[WarTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WarTypes];
GO
IF OBJECT_ID(N'[dbo].[WarRankings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WarRankings];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ActionLogs'
CREATE TABLE [dbo].[ActionLogs] (
    [id] int IDENTITY(1,1) NOT NULL,
    [userClanId] int  NOT NULL,
    [actionId] smallint  NOT NULL,
    [objectChanged] varchar(50)  NOT NULL,
    [oldValue] varchar(max)  NULL,
    [newValue] varchar(max)  NULL,
    [addedOn] datetime  NOT NULL,
    [propertyChanged] varchar(100)  NULL
);
GO

-- Creating table 'Actions'
CREATE TABLE [dbo].[Actions] (
    [id] smallint IDENTITY(1,1) NOT NULL,
    [name] varchar(50)  NOT NULL
);
GO

-- Creating table 'ClanGroups'
CREATE TABLE [dbo].[ClanGroups] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(20)  NOT NULL,
    [clanId] int  NOT NULL,
	[groupLeaderId] int NOT NULL DEFAULT 0,
    [addedOn] datetime  NOT NULL,
    [updatedOn] datetime  NULL
);
GO

-- Creating table 'Clans'
CREATE TABLE [dbo].[Clans] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(100)  NOT NULL,
    [shieldLogoLocation] varchar(100)  NULL,
    [addedOn] datetime  NOT NULL,
    [updatedOn] datetime  NULL,
    [description] varchar(max)  NULL,
    [tag] varchar(30)  NULL,
	[level]	tinyint NOT NULL DEFAULT 0
);
GO

-- Creating table 'ClanUsers'
CREATE TABLE [dbo].[ClanUsers] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(100)  NOT NULL,
    [userRoleId] smallint  NOT NULL,
    [clanId] int  NULL,
    [pictureLocation] varchar(100)  NULL,
    [villageBasePictureLocation] varchar(100)  NULL,
    [warBasePictureLocation] varchar(100)  NULL,
    [addedOn] datetime  NOT NULL,
    [updatedOn] datetime  NULL,
    [aspnetUserId] nvarchar(128)  NULL,
    [clanGroupId] int  NULL
);
GO

-- Creating table 'ClanWarPicks'
CREATE TABLE [dbo].[ClanWarPicks] (
    [id] int IDENTITY(1,1) NOT NULL,
    [clanWarId] int  NOT NULL,
	[clanUserId] int NOT NULL DEFAULT 0,
    [clanMemberWarPosition] smallint  NOT NULL,
    [pickVS] smallint  NULL,
    [note] varchar(max)  NULL,
    [addedOn] datetime  NOT NULL,
    [updatedOn] datetime  NULL
);
GO

-- Creating table 'ClanWars'
CREATE TABLE [dbo].[ClanWars] (
    [id] int IDENTITY(1,1) NOT NULL,
    [warTypeId] smallint  NOT NULL,
    [clanId] int  NOT NULL,
    [clanVS] varchar(100)  NOT NULL,
	[clanVSTag] varchar(30)  NULL,
    [warStartedOn] datetime  NOT NULL,
    [note] varchar(max)  NULL,
	[warNote] varchar(max) NULL,
	[isFinalized] bit NOT NULL DEFAULT 0,
	[xp] smallint NOT NULL DEFAULT 0,
    [addedOn] datetime  NOT NULL,
    [updatedOn] datetime  NULL
);
GO

-- Creating table 'Requests'
CREATE TABLE [dbo].[Requests] (
    [id] int IDENTITY(1,1) NOT NULL,
    [clanId] int  NOT NULL,
	[aspnetUserId] int NOT NULL,
    [processed] bit  NOT NULL DEFAULT 0,
    [accepted] bit  NOT NULL DEFAULT 0 
);
GO

-- Creating table 'Rules'
CREATE TABLE [dbo].[Rules] (
    [id] smallint IDENTITY(1,1) NOT NULL,
	[clanId] int  NOT NULL,
    [description] varchar(150)  NOT NULL,
    [addedOn] datetime  NOT NULL,
    [updatedOn] datetime  NULL
);
GO

-- Creating table 'UserBlackLists'
CREATE TABLE [dbo].[UserBlackLists] (
    [id] int IDENTITY(1,1) NOT NULL,
    [clanUserId] int  NOT NULL,
    [clanId] int  NOT NULL,
    [note] varchar(max)  NULL,
    [addedOn] datetime  NOT NULL
);
GO

-- Creating table 'UserRoles'
CREATE TABLE [dbo].[UserRoles] (
    [id] smallint IDENTITY(1,1) NOT NULL,
    [name] varchar(20)  NOT NULL
);
GO

-- Creating table 'WarLogs'
CREATE TABLE [dbo].[WarLogs] (
    [id] int IDENTITY(1,1) NOT NULL,
    [clanWarId] int  NOT NULL,
    [clanStars] smallint  NOT NULL,
    [clanAttacksWon] smallint  NOT NULL,
    [clanAttacksUsed] smallint  NOT NULL,
    [clanAverageDestruction] decimal(18,0)  NOT NULL,
    [clan3Stars] smallint  NOT NULL,
    [clan2Stars] smallint  NOT NULL,
    [clan1Stars] smallint  NOT NULL,
    [clanHeroicDefense] int  NOT NULL,
    [clanHeroicAttack] int  NOT NULL,
    [clanVSStars] smallint  NOT NULL,
    [clanVSAttacksWon] smallint  NOT NULL,
    [clanVSAttacksUsed] smallint  NOT NULL,
    [clanVSAverageDestruction] decimal(18,0)  NOT NULL,
    [clanVS3Stars] smallint  NOT NULL,
    [clanVS2Stars] smallint  NOT NULL,
    [clanVS1Stars] smallint  NOT NULL,
    [clanVSHeroicDefense] varchar(20)  NULL,
    [clanVSHeroicAttack] varchar(20)  NULL,
    [note] varchar(max)  NULL,
    [addedOn] datetime  NOT NULL,
    [updatedOn] datetime  NULL
);
GO

-- Creating table 'WarRankings'
CREATE TABLE [dbo].[WarRankings] (
    [id] int IDENTITY(1,1) NOT NULL,
	[clanUserId] int  NOT NULL,
    [clanWarId] int  NOT NULL,
    [clanGroupId] int  NULL,
	[stars] smallint  NOT NULL DEFAULT 0,
	[xp] smallint NOT NULL DEFAULT 0,
	[attackWin] bit NOT NULL DEFAULT 0,
    [heroicDefense] bit  NOT NULL DEFAULT 0,
    [heroicAttack] bit  NOT NULL DEFAULT 0,
    [attacksDefended] smallint  NOT NULL DEFAULT 0,
    [attacksOn] smallint  NOT NULL DEFAULT 0,
    [note] varchar(max)  NULL,
	[attackOn] datetime NULL,
    [addedOn] datetime  NOT NULL,
    [updatedOn] datetime  NULL
);
GO

-- Creating table 'WarTypes'
CREATE TABLE [dbo].[WarTypes] (
    [id] smallint IDENTITY(1,1) NOT NULL,
    [name] varchar(20)  NOT NULL,
    [value] tinyint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'ActionLogs'
ALTER TABLE [dbo].[ActionLogs]
ADD CONSTRAINT [PK_ActionLogs]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Actions'
ALTER TABLE [dbo].[Actions]
ADD CONSTRAINT [PK_Actions]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'ClanGroups'
ALTER TABLE [dbo].[ClanGroups]
ADD CONSTRAINT [PK_ClanGroups]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Clans'
ALTER TABLE [dbo].[Clans]
ADD CONSTRAINT [PK_Clans]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'ClanUsers'
ALTER TABLE [dbo].[ClanUsers]
ADD CONSTRAINT [PK_ClanUsers]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'ClanWarPicks'
ALTER TABLE [dbo].[ClanWarPicks]
ADD CONSTRAINT [PK_ClanWarPicks]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'ClanWars'
ALTER TABLE [dbo].[ClanWars]
ADD CONSTRAINT [PK_ClanWars]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Rules'
ALTER TABLE [dbo].[Rules]
ADD CONSTRAINT [PK_Rules]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'UserBlackLists'
ALTER TABLE [dbo].[UserBlackLists]
ADD CONSTRAINT [PK_UserBlackLists]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [PK_UserRoles]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'WarLogs'
ALTER TABLE [dbo].[WarLogs]
ADD CONSTRAINT [PK_WarLogs]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id], [clanUserId] in table 'WarRankings'
ALTER TABLE [dbo].[WarRankings]
ADD CONSTRAINT [PK_WarRankings]
    PRIMARY KEY CLUSTERED ([id], [clanUserId] ASC);
GO

-- Creating primary key on [id] in table 'WarTypes'
ALTER TABLE [dbo].[WarTypes]
ADD CONSTRAINT [PK_WarTypes]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [actionId] in table 'ActionLogs'
ALTER TABLE [dbo].[ActionLogs]
ADD CONSTRAINT [FK_ActionLog_Action]
    FOREIGN KEY ([actionId])
    REFERENCES [dbo].[Actions]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionLog_Action'
CREATE INDEX [IX_FK_ActionLog_Action]
ON [dbo].[ActionLogs]
    ([actionId]);
GO

-- Creating foreign key on [userClanId] in table 'ActionLogs'
ALTER TABLE [dbo].[ActionLogs]
ADD CONSTRAINT [FK_ActionLog_UserClan]
    FOREIGN KEY ([userClanId])
    REFERENCES [dbo].[ClanUsers]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionLog_UserClan'
CREATE INDEX [IX_FK_ActionLog_UserClan]
ON [dbo].[ActionLogs]
    ([userClanId]);
GO

-- Creating foreign key on [clanId] in table 'ClanGroups'
ALTER TABLE [dbo].[ClanGroups]
ADD CONSTRAINT [FK_ClanGroup_Clan]
    FOREIGN KEY ([clanId])
    REFERENCES [dbo].[Clans]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClanGroup_Clan'
CREATE INDEX [IX_FK_ClanGroup_Clan]
ON [dbo].[ClanGroups]
    ([clanId]);
GO

-- Creating foreign key on [clanGroupId] in table 'WarRankings'
ALTER TABLE [dbo].[WarRankings]
ADD CONSTRAINT [FK_WarRankings_ClanGroup]
    FOREIGN KEY ([clanGroupId])
    REFERENCES [dbo].[ClanGroups]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WarRankings_ClanGroup'
CREATE INDEX [IX_FK_WarRankings_ClanGroup]
ON [dbo].[WarRankings]
    ([clanGroupId]);
GO

-- Creating foreign key on [clanId] in table 'ClanUsers'
ALTER TABLE [dbo].[ClanUsers]
ADD CONSTRAINT [FK_ClanUser_Clan]
    FOREIGN KEY ([clanId])
    REFERENCES [dbo].[Clans]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClanUser_Clan'
CREATE INDEX [IX_FK_ClanUser_Clan]
ON [dbo].[ClanUsers]
    ([clanId]);
GO

-- Creating foreign key on [clanId] in table 'ClanWars'
ALTER TABLE [dbo].[ClanWars]
ADD CONSTRAINT [FK_ClanWar_Clan]
    FOREIGN KEY ([clanId])
    REFERENCES [dbo].[Clans]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClanWar_Clan'
CREATE INDEX [IX_FK_ClanWar_Clan]
ON [dbo].[ClanWars]
    ([clanId]);
GO

-- Creating foreign key on [clanId] in table 'UserBlackLists'
ALTER TABLE [dbo].[UserBlackLists]
ADD CONSTRAINT [FK_UserBlackList_Clan]
    FOREIGN KEY ([clanId])
    REFERENCES [dbo].[Clans]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserBlackList_Clan'
CREATE INDEX [IX_FK_UserBlackList_Clan]
ON [dbo].[UserBlackLists]
    ([clanId]);
GO

-- Creating foreign key on [userRoleId] in table 'ClanUsers'
ALTER TABLE [dbo].[ClanUsers]
ADD CONSTRAINT [FK_ClanUser_UserRole]
    FOREIGN KEY ([userRoleId])
    REFERENCES [dbo].[UserRoles]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClanUser_UserRole'
CREATE INDEX [IX_FK_ClanUser_UserRole]
ON [dbo].[ClanUsers]
    ([userRoleId]);
GO

-- Creating foreign key on [clanUserId] in table 'UserBlackLists'
ALTER TABLE [dbo].[UserBlackLists]
ADD CONSTRAINT [FK_UserBlackList_ClanUser]
    FOREIGN KEY ([clanUserId])
    REFERENCES [dbo].[ClanUsers]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserBlackList_ClanUser'
CREATE INDEX [IX_FK_UserBlackList_ClanUser]
ON [dbo].[UserBlackLists]
    ([clanUserId]);
GO

-- Creating foreign key on [clanHeroicAttack] in table 'WarLogs'
ALTER TABLE [dbo].[WarLogs]
ADD CONSTRAINT [FK_WarLog_ClanUser_Attack]
    FOREIGN KEY ([clanHeroicAttack])
    REFERENCES [dbo].[ClanUsers]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WarLog_ClanUser_Attack'
CREATE INDEX [IX_FK_WarLog_ClanUser_Attack]
ON [dbo].[WarLogs]
    ([clanHeroicAttack]);
GO

-- Creating foreign key on [clanHeroicDefense] in table 'WarLogs'
ALTER TABLE [dbo].[WarLogs]
ADD CONSTRAINT [FK_WarLog_ClanUser_Defense]
    FOREIGN KEY ([clanHeroicDefense])
    REFERENCES [dbo].[ClanUsers]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WarLog_ClanUser_Defense'
CREATE INDEX [IX_FK_WarLog_ClanUser_Defense]
ON [dbo].[WarLogs]
    ([clanHeroicDefense]);
GO

-- Creating foreign key on [warTypeId] in table 'ClanWars'
ALTER TABLE [dbo].[ClanWars]
ADD CONSTRAINT [FK_ClanWar_WarType]
    FOREIGN KEY ([warTypeId])
    REFERENCES [dbo].[WarTypes]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClanWar_WarType'
CREATE INDEX [IX_FK_ClanWar_WarType]
ON [dbo].[ClanWars]
    ([warTypeId]);
GO

-- Creating foreign key on [clanWarId] in table 'WarLogs'
ALTER TABLE [dbo].[WarLogs]
ADD CONSTRAINT [FK_WarLog_ClanWar]
    FOREIGN KEY ([clanWarId])
    REFERENCES [dbo].[ClanWars]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WarLog_ClanWar'
CREATE INDEX [IX_FK_WarLog_ClanWar]
ON [dbo].[WarLogs]
    ([clanWarId]);
GO

-- Creating foreign key on [clanWarId] in table 'WarRankings'
ALTER TABLE [dbo].[WarRankings]
ADD CONSTRAINT [FK_WarRankings_ClanWar]
    FOREIGN KEY ([clanWarId])
    REFERENCES [dbo].[ClanWars]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WarRankings_ClanWar'
CREATE INDEX [IX_FK_WarRankings_ClanWar]
ON [dbo].[WarRankings]
    ([clanWarId]);
GO

-- Creating foreign key on [clanId] in table 'Rules'
ALTER TABLE [dbo].[Rules]
ADD CONSTRAINT [FK_Rules_ToClan]
    FOREIGN KEY ([clanId])
    REFERENCES [dbo].[Clans]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Rules_ToClan'
CREATE INDEX [IX_FK_Rules_ToClan]
ON [dbo].[Rules]
    ([clanId]);
GO

-- Creating foreign key on [clanUserId] in table 'WarRankings'
ALTER TABLE [dbo].[WarRankings]
ADD CONSTRAINT [FK_WarRankings_ClanUser]
    FOREIGN KEY ([clanUserId])
    REFERENCES [dbo].[ClanUsers]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WarRankings_ClanUser'
CREATE INDEX [IX_FK_WarRankings_ClanUser]
ON [dbo].[WarRankings]
    ([clanUserId]);
GO

-- Creating foreign key on [aspnetUserId] in table 'ClanUsers'
ALTER TABLE [dbo].[ClanUsers]
ADD CONSTRAINT [FK_ClanUsers_AspNetUserId]
    FOREIGN KEY ([aspnetUserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClanUsers_AspNetUserId'
CREATE INDEX [IX_FK_ClanUsers_AspNetUserId]
ON [dbo].[ClanUsers]
    ([aspnetUserId]);
GO

-- --------------------------------------------------
-- Populate some tables
-- --------------------------------------------------

-- Populate WarTypes, since it should be a UI for it (for now at least)
INSERT INTO WarTypes (name, value)
SELECT '10 vs 10', 10 UNION
SELECT '15 vs 15', 15 UNION
SELECT '20 vs 20', 20 UNION
SELECT '25 vs 25', 25 UNION
SELECT '30 vs 30', 30 UNION
SELECT '35 vs 35', 35 UNION
SELECT '40 vs 40', 40 UNION
SELECT '45 vs 45', 45 UNION
SELECT '50 vs 50', 50
ORDER BY 2

-- Populate UserRoles
SET IDENTITY_INSERT UserRoles ON
INSERT INTO UserRoles (id, name)
SELECT 1, 'Leader' UNION
SELECT 2, 'CoLeader' UNION
SELECT 3, 'Elder' UNION
SELECT 4, 'Member'
SET IDENTITY_INSERT UserRoles OFF

-- Populate Actions
SET IDENTITY_INSERT Actions ON
INSERT INTO Actions (id, name)
SELECT 1, 'Create' UNION
SELECT 2, 'Read' UNION
SELECT 3, 'Update' UNION
SELECT 4, 'Delete'
SET IDENTITY_INSERT Actions OFF

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------