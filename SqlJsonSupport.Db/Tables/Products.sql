CREATE TABLE [dbo].[Products] (
    [Id]			INT		IDENTITY (1, 1) NOT NULL,
    [Charges]       NTEXT   NULL,
    [Features]		NTEXT   NULL,
    [Documents]	NTEXT   NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC)
);

