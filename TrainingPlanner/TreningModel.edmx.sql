
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 09/29/2014 16:38:46
-- Generated from EDMX file: C:\Users\hvukovic\Documents\Visual Studio 2012\Projects\NoviTrening\TrainingPlanner\TrainingPlanner\TreningModel.edmx
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
IF OBJECT_ID(N'[dbo].[FK_TestSlika]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Slika] DROP CONSTRAINT [FK_TestSlika];
GO
IF OBJECT_ID(N'[dbo].[FK_ZagrijavanjeSlikeZagrijavanjePopis]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ZagrijavanjeSlike] DROP CONSTRAINT [FK_ZagrijavanjeSlikeZagrijavanjePopis];
GO
IF OBJECT_ID(N'[dbo].[FK_VjezbeSlikeVjezbePopis]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VjezbeSlike] DROP CONSTRAINT [FK_VjezbeSlikeVjezbePopis];
GO
IF OBJECT_ID(N'[dbo].[FK_IstezanjeSlikeIstezanjePopis]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IstezanjeSlike] DROP CONSTRAINT [FK_IstezanjeSlikeIstezanjePopis];
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
IF OBJECT_ID(N'[dbo].[Slika]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Slika];
GO
IF OBJECT_ID(N'[dbo].[ZagrijavanjeSlike]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ZagrijavanjeSlike];
GO
IF OBJECT_ID(N'[dbo].[VjezbeSlike]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VjezbeSlike];
GO
IF OBJECT_ID(N'[dbo].[IstezanjeSlike]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IstezanjeSlike];
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
    [DatumTesta] datetime  NOT NULL,
    [Ergometar] smallint  NOT NULL,
    [Zgibovi] smallint  NOT NULL,
    [Sklekovi] smallint  NOT NULL,
    [Trbusnjaci] smallint  NOT NULL,
    [Cucnjevi] smallint  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
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
    [TreningId] int  NOT NULL,
    [Tempo] nvarchar(max)  NULL,
    [Puls] nvarchar(max)  NULL,
    [ZagrijavanjeNapomena] nvarchar(max)  NULL
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
    [TreningId] int  NOT NULL,
    [VrijemeIzdrzaja] nvarchar(max)  NULL,
    [VrstaIstezanja] nvarchar(max)  NULL
);
GO

-- Creating table 'Slika'
CREATE TABLE [dbo].[Slika] (
    [SlikaId] int IDENTITY(1,1) NOT NULL,
    [TestTestId] int  NOT NULL,
    [SlikaIme] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ZagrijavanjeSlike'
CREATE TABLE [dbo].[ZagrijavanjeSlike] (
    [ZagrijavanjeSlikeId] int IDENTITY(1,1) NOT NULL,
    [ZagrijavanjePopisZagrijavanjeId] int  NOT NULL,
    [ZagrijavanjeSlikaIme] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'VjezbeSlike'
CREATE TABLE [dbo].[VjezbeSlike] (
    [VjezbeSlikeId] int IDENTITY(1,1) NOT NULL,
    [VjezbeSlikaIme] nvarchar(max)  NOT NULL,
    [VjezbePopisVjezbeId] int  NOT NULL
);
GO

-- Creating table 'IstezanjeSlike'
CREATE TABLE [dbo].[IstezanjeSlike] (
    [IstezanjeSlikeId] int IDENTITY(1,1) NOT NULL,
    [IstezanjeSlikaIme] nvarchar(max)  NOT NULL,
    [IstezanjePopisIstezanjeId] int  NOT NULL
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

-- Creating primary key on [SlikaId] in table 'Slika'
ALTER TABLE [dbo].[Slika]
ADD CONSTRAINT [PK_Slika]
    PRIMARY KEY CLUSTERED ([SlikaId] ASC);
GO

-- Creating primary key on [ZagrijavanjeSlikeId] in table 'ZagrijavanjeSlike'
ALTER TABLE [dbo].[ZagrijavanjeSlike]
ADD CONSTRAINT [PK_ZagrijavanjeSlike]
    PRIMARY KEY CLUSTERED ([ZagrijavanjeSlikeId] ASC);
GO

-- Creating primary key on [VjezbeSlikeId] in table 'VjezbeSlike'
ALTER TABLE [dbo].[VjezbeSlike]
ADD CONSTRAINT [PK_VjezbeSlike]
    PRIMARY KEY CLUSTERED ([VjezbeSlikeId] ASC);
GO

-- Creating primary key on [IstezanjeSlikeId] in table 'IstezanjeSlike'
ALTER TABLE [dbo].[IstezanjeSlike]
ADD CONSTRAINT [PK_IstezanjeSlike]
    PRIMARY KEY CLUSTERED ([IstezanjeSlikeId] ASC);
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

-- Creating foreign key on [TestTestId] in table 'Slika'
ALTER TABLE [dbo].[Slika]
ADD CONSTRAINT [FK_TestSlika]
    FOREIGN KEY ([TestTestId])
    REFERENCES [dbo].[Test]
        ([TestId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TestSlika'
CREATE INDEX [IX_FK_TestSlika]
ON [dbo].[Slika]
    ([TestTestId]);
GO

-- Creating foreign key on [ZagrijavanjePopisZagrijavanjeId] in table 'ZagrijavanjeSlike'
ALTER TABLE [dbo].[ZagrijavanjeSlike]
ADD CONSTRAINT [FK_ZagrijavanjeSlikeZagrijavanjePopis]
    FOREIGN KEY ([ZagrijavanjePopisZagrijavanjeId])
    REFERENCES [dbo].[ZagrijavanjePopis]
        ([ZagrijavanjeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ZagrijavanjeSlikeZagrijavanjePopis'
CREATE INDEX [IX_FK_ZagrijavanjeSlikeZagrijavanjePopis]
ON [dbo].[ZagrijavanjeSlike]
    ([ZagrijavanjePopisZagrijavanjeId]);
GO

-- Creating foreign key on [VjezbePopisVjezbeId] in table 'VjezbeSlike'
ALTER TABLE [dbo].[VjezbeSlike]
ADD CONSTRAINT [FK_VjezbeSlikeVjezbePopis]
    FOREIGN KEY ([VjezbePopisVjezbeId])
    REFERENCES [dbo].[VjezbePopis]
        ([VjezbeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_VjezbeSlikeVjezbePopis'
CREATE INDEX [IX_FK_VjezbeSlikeVjezbePopis]
ON [dbo].[VjezbeSlike]
    ([VjezbePopisVjezbeId]);
GO

-- Creating foreign key on [IstezanjePopisIstezanjeId] in table 'IstezanjeSlike'
ALTER TABLE [dbo].[IstezanjeSlike]
ADD CONSTRAINT [FK_IstezanjeSlikeIstezanjePopis]
    FOREIGN KEY ([IstezanjePopisIstezanjeId])
    REFERENCES [dbo].[IstezanjePopis]
        ([IstezanjeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_IstezanjeSlikeIstezanjePopis'
CREATE INDEX [IX_FK_IstezanjeSlikeIstezanjePopis]
ON [dbo].[IstezanjeSlike]
    ([IstezanjePopisIstezanjeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------