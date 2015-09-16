﻿SET IDENTITY_INSERT [dbo].[Funcao] ON
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (1, N'Movimentações', NULL, NULL)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (2, N'Pagamento', N'Pagamento/Index', 1)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (3, N'Recebimento', N'Recebimento/Index', 1)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (4, N'Transferência', N'Transferencia/Index', 1)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (5, N'Título Recorrente', N'TituloRecorrente/Index', 1)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (6, N'Incluir', N'Pagamento/Create', 2)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (7, N'Alterar', N'Pagamento/Edit', 2)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (8, N'Excluir', N'Pagamento/Delete', 2)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (9, N'Liquidar', N'Pagamento/Liquidar', 2)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (10, N'Incluir', N'Recebimento/Create', 3)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (11, N'Alterar', N'Recebimento/Edit', 3)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (12, N'Excluir', N'Recebimento/Delete', 3)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (13, N'Liquidar', N'Recebimento/Liquidar', 3)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (14, N'Incluir', N'Transferencia/Create', 4)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (15, N'Alterar', N'Transferencia/Edit', 4)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (16, N'Excluir', N'Transferencia/Delete', 4)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (17, N'Incluir', N'TituloRecorrente/Create', 5)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (18, N'Alterar', N'TituloRecorrente/Edit', 5)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (19, N'Excluir', N'TituloRecorrente/Delete', 5)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (20, N'Cadastros', NULL, NULL)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (21, N'Conta', N'Conta/Index', 20)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (22, N'Banco', N'Banco/Index', 20)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (23, N'Categoria', N'Categoria/Index', 20)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (24, N'Centro de Custo', N'CentroCusto/Index', 20)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (25, N'Tipo de Pessoa', N'TipoPessoa/Index', 20)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (26, N'Pessoa', N'Pessoa/Index', 20)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (27, N'Alterar', N'Conta/Edit', 21)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (28, N'Excluir', N'Conta/Delete', 21)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (29, N'Incluir', N'Conta/Create', 21)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (30, N'Alterar', N'Banco/Edit', 22)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (31, N'Excluir', N'Banco/Delete', 22)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (32, N'Incluir', N'Banco/Create', 22)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (33, N'Alterar', N'Categoria/Edit', 23)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (34, N'Excluir', N'Categoria/Delete', 23)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (35, N'Incluir', N'Categoria/Create', 23)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (36, N'Alterar', N'CentroCusto/Edit', 24)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (37, N'Excluir', N'CentroCusto/Delete', 24)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (38, N'Incluir', N'CentroCusto/Create', 24)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (39, N'Alterar', N'TipoPessoa/Edit', 25)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (40, N'Excluir', N'TipoPessoa/Delete', 25)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (41, N'Incluir', N'TipoPessoa/Create', 25)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (42, N'Alterar', N'Pessoa/Edit', 26)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (43, N'Excluir', N'Pessoa/Delete', 26)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (44, N'Incluir', N'Pessoa/Create', 26)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (45, N'Segurança', NULL, NULL)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (46, N'Usuários', N'Usuario/Index', 45)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (47, N'Perfil', N'Perfil/Index', 45)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (48, N'Alterar', N'Usuario/Edit', 46)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (49, N'Excluir', N'Usuario/Delete', 46)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (50, N'Incluir', N'Usuario/Create', 46)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (51, N'Alterar', N'Perfil/Edit', 47)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (52, N'Excluir', N'Perfil/Delete', 47)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (53, N'Incluir', N'Perfil/Create', 47)
SET IDENTITY_INSERT [dbo].[Funcao] OFF