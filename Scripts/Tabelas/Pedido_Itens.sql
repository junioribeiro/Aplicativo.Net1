CREATE TABLE [dbo].[Pedido_Itens](
	[Pedido_ItensId] [bigint] IDENTITY(1,1) NOT NULL,
    [PedidoId] [bigint] NOT NULL,	
	[ProdutoId] [bigint] NOT NULL    	
	
PRIMARY KEY CLUSTERED 
(
	[Pedido_ItensId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
GO
ALTER TABLE [dbo].[Pedido_Itens]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Itens_Produtos] FOREIGN KEY([ProdutoId])
REFERENCES [dbo].[Produtos] ([ProdutoId])
GO

ALTER TABLE [dbo].[Pedido_Itens] CHECK CONSTRAINT [FK_Pedido_Itens_Produtos]
GO

ALTER TABLE [dbo].[Pedido_Itens]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Itens_Pedidos] FOREIGN KEY([PedidoId])
REFERENCES [dbo].[Pedidos] ([PedidoId])
GO

ALTER TABLE [dbo].[Pedido_Itens] CHECK CONSTRAINT [FK_Pedido_Itens_Pedidos]
GO