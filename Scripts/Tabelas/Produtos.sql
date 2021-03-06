CREATE TABLE [dbo].[Produtos](
	[ProdutoId] [bigint] IDENTITY(1,1) NOT NULL,	
	[Codigo] [varchar](30) NOT NULL,
	[Nome] [varchar](200) NOT NULL,
    [Valor] [decimal](18, 4) NOT NULL,
	[DataCadastro] [datetime] NOT NULL
	
PRIMARY KEY CLUSTERED 
(
	[ProdutoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
GO
ALTER TABLE [dbo].[Produtos] ADD DEFAULT (getdate()) FOR [DataCadastro]
GO