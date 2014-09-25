
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 09/20/2014 17:26:38
-- Generated from EDMX file: C:\Users\hvukovic\Documents\Visual Studio 2012\Projects\TrainingPlanner\TrainingPlanner\TrainingPlanner\TreningModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Trening];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ClanTrening]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Trening] DROP CONSTRAINT [FK_ClanTrening];
GO
IF OBJECT_ID(N'[dbo].[FK_TreningZagrijavanje]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Zagrijavanje] DROP CONSTRAINT [FK_TreningZagrijavanje];
GO
IF OBJECT_ID(N'[dbo].[FK_TreningIstezanje]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Istezanje] DROP CONSTRAINT [FK_TreningIstezanje];
GO
IF OBJECT_ID(N'[dbo].[FK_TreningVjezba]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vjezba] DROP CONSTRAINT [FK_TreningVjezba];
GO
IF OBJECT_ID(N'[dbo].[FK_ClanTest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Test] DROP CONSTRAINT [FK_ClanTest];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Clan]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clan];
GO
IF OBJECT_ID(N'[dbo].[Test]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Test];
GO
IF OBJECT_ID(N'[dbo].[Trening]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trening];
GO
IF OBJECT_ID(N'[dbo].[IstezanjePopis]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IstezanjePopis];
GO
IF OBJECT_ID(N'[dbo].[VjezbePopis]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VjezbePopis];
GO
IF OBJECT_ID(N'[dbo].[ZagrijavanjePopis]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ZagrijavanjePopis];
GO
IF OBJECT_ID(N'[dbo].[Zagrijavanje]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Zagrijavanje];
GO
IF OBJECT_ID(N'[dbo].[Vjezba]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vjezba];
GO
IF OBJECT_ID(N'[dbo].[Istezanje]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Istezanje];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Clan'
CREATE TABLE [dbo].[Clan] (
    [ClanId] int IDENTITY(1,1) NOT NULL,
    [Ime] nvarchar(max)  NOT NULL,
    [Prezime] nvarchar(max)  NOT NULL,
    [GodinaRodenja] datetime  NOT NULL,
    [Visina] nvarchar(max)  NOT NULL,
    [Tezina] nvarchar(max)  NOT NULL,
    [GodineStarosti] smallint  NOT NULL,
    [Sport] nvarchar(max)  NULL,
    [Amneza] nvarchar(max)  NULL,
    [Napomena] nvarchar(max)  NULL
);
GO

-- Creating table 'Test'
CREATE TABLE [dbo].[Test] (
    [TestId] int IDENTITY(1,1) NOT NULL,
    [Dijagnostika] varbinary(max)  NULL,
    [DatumTesta] datetime  NOT NULL,
    [Ergometar] smallint  NOT NULL,
    [Zgibovi] smallint  NOT NULL,
    [Sklekovi] smallint  NOT NULL,
    [Trbusnjaci] smallint  NOT NULL,
    [Cucnjevi] smallint  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DijagnostikaType] nvarchar(50)  NULL,
    [ClanId] int  NOT NULL
);
GO

-- Creating table 'Trening'
CREATE TABLE [dbo].[Trening] (
    [TreningId] int IDENTITY(1,1) NOT NULL,
    [TipTreninga] nvarchar(max)  NULL,
    [DatumTreninga] datetime  NULL,
    [BrojKrugova] smallint  NULL,
    [ImeTreninga] nvarchar(max)  NULL,
    [ClanId] int  NOT NULL,
    [Napomena] nvarchar(max)  NULL
);
GO

-- Creating table 'IstezanjePopis'
CREATE TABLE [dbo].[IstezanjePopis] (
    [IstezanjeId] int IDENTITY(1,1) NOT NULL,
    [Naziv] nvarchar(max)  NOT NULL,
    [Info] nvarchar(max)  NULL
);
GO

-- Creating table 'VjezbePopis'
CREATE TABLE [dbo].[VjezbePopis] (
    [VjezbeId] int IDENTITY(1,1) NOT NULL,
    [ImeVjezbe] nvarchar(max)  NOT NULL,
    [Slika] varbinary(max)  NULL,
    [Info] nvarchar(max)  NULL
);
GO

-- Creating table 'ZagrijavanjePopis'
CREATE TABLE [dbo].[ZagrijavanjePopis] (
    [ZagrijavanjeId] int IDENTITY(1,1) NOT NULL,
    [Naziv] nvarchar(max)  NOT NULL,
    [Info] nvarchar(max)  NULL
);
GO

-- Creating table 'Zagrijavanje'
CREATE TABLE [dbo].[Zagrijavanje] (
    [ZagrijavanjeId] int IDENTITY(1,1) NOT NULL,
    [Naziv] nvarchar(max)  NOT NULL,
    [Info] nvarchar(max)  NULL,
    [TreningId] int  NOT NULL
);
GO

-- Creating table 'Vjezba'
CREATE TABLE [dbo].[Vjezba] (
    [VjezbaId] int IDENTITY(1,1) NOT NULL,
    [ImeVjezbe] nvarchar(max)  NOT NULL,
    [Slika] varbinary(max)  NULL,
    [Info] nvarchar(max)  NULL,
    [TreningId] int  NOT NULL,
    [BrojPonavljanja] nvarchar(max)  NULL,
    [BrojSerija] nvarchar(max)  NULL,
    [Kilogrami] nvarchar(max)  NULL,
    [Odmor] nvarchar(max)  NULL
);
GO

-- Creating table 'Istezanje'
CREATE TABLE [dbo].[Istezanje] (
    [IstezanjeId] int IDENTITY(1,1) NOT NULL,
    [Naziv] nvarchar(max)  NOT NULL,
    [Info] nvarchar(max)  NULL,
    [TreningId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ClanId] in table 'Clan'
ALTER TABLE [dbo].[Clan]
ADD CONSTRAINT [PK_Clan]
    PRIMARY KEY CLUSTERED ([ClanId] ASC);
GO

-- Creating primary key on [TestId] in table 'Test'
ALTER TABLE [dbo].[Test]
ADD CONSTRAINT [PK_Test]
    PRIMARY KEY CLUSTERED ([TestId] ASC);
GO

-- Creating primary key on [TreningId] in table 'Trening'
ALTER TABLE [dbo].[Trening]
ADD CONSTRAINT [PK_Trening]
    PRIMARY KEY CLUSTERED ([TreningId] ASC);
GO

-- Creating primary key on [IstezanjeId] in table 'IstezanjePopis'
ALTER TABLE [dbo].[IstezanjePopis]
ADD CONSTRAINT [PK_IstezanjePopis]
    PRIMARY KEY CLUSTERED ([IstezanjeId] ASC);
GO

-- Creating primary key on [VjezbeId] in table 'VjezbePopis'
ALTER TABLE [dbo].[VjezbePopis]
ADD CONSTRAINT [PK_VjezbePopis]
    PRIMARY KEY CLUSTERED ([VjezbeId] ASC);
GO

-- Creating primary key on [ZagrijavanjeId] in table 'ZagrijavanjePopis'
ALTER TABLE [dbo].[ZagrijavanjePopis]
ADD CONSTRAINT [PK_ZagrijavanjePopis]
    PRIMARY KEY CLUSTERED ([ZagrijavanjeId] ASC);
GO

-- Creating primary key on [ZagrijavanjeId] in table 'Zagrijavanje'
ALTER TABLE [dbo].[Zagrijavanje]
ADD CONSTRAINT [PK_Zagrijavanje]
    PRIMARY KEY CLUSTERED ([ZagrijavanjeId] ASC);
GO

-- Creating primary key on [VjezbaId] in table 'Vjezba'
ALTER TABLE [dbo].[Vjezba]
ADD CONSTRAINT [PK_Vjezba]
    PRIMARY KEY CLUSTERED ([VjezbaId] ASC);
GO

-- Creating primary key on [IstezanjeId] in table 'Istezanje'
ALTER TABLE [dbo].[Istezanje]
ADD CONSTRAINT [PK_Istezanje]
    PRIMARY KEY CLUSTERED ([IstezanjeId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ClanId] in table 'Trening'
ALTER TABLE [dbo].[Trening]
ADD CONSTRAINT [FK_ClanTrening]
    FOREIGN KEY ([ClanId])
    REFERENCES [dbo].[Clan]
        ([ClanId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClanTrening'
CREATE INDEX [IX_FK_ClanTrening]
ON [dbo].[Trening]
    ([ClanId]);
GO

-- Creating foreign key on [TreningId] in table 'Zagrijavanje'
ALTER TABLE [dbo].[Zagrijavanje]
ADD CONSTRAINT [FK_TreningZagrijavanje]
    FOREIGN KEY ([TreningId])
    REFERENCES [dbo].[Trening]
        ([TreningId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TreningZagrijavanje'
CREATE INDEX [IX_FK_TreningZagrijavanje]
ON [dbo].[Zagrijavanje]
    ([TreningId]);
GO

-- Creating foreign key on [TreningId] in table 'Istezanje'
ALTER TABLE [dbo].[Istezanje]
ADD CONSTRAINT [FK_TreningIstezanje]
    FOREIGN KEY ([TreningId])
    REFERENCES [dbo].[Trening]
        ([TreningId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TreningIstezanje'
CREATE INDEX [IX_FK_TreningIstezanje]
ON [dbo].[Istezanje]
    ([TreningId]);
GO

-- Creating foreign key on [TreningId] in table 'Vjezba'
ALTER TABLE [dbo].[Vjezba]
ADD CONSTRAINT [FK_TreningVjezba]
    FOREIGN KEY ([TreningId])
    REFERENCES [dbo].[Trening]
        ([TreningId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TreningVjezba'
CREATE INDEX [IX_FK_TreningVjezba]
ON [dbo].[Vjezba]
    ([TreningId]);
GO

-- Creating foreign key on [ClanId] in table 'Test'
ALTER TABLE [dbo].[Test]
ADD CONSTRAINT [FK_ClanTest]
    FOREIGN KEY ([ClanId])
    REFERENCES [dbo].[Clan]
        ([ClanId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClanTest'
CREATE INDEX [IX_FK_ClanTest]
ON [dbo].[Test]
    ([ClanId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------