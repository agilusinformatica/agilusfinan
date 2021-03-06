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
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (54, N'Tipo de Telefone', N'TipoTelefone/Index', 20)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (55, N'Alterar', N'TipoTelefone/Edit', 54)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (56, N'Excluir', N'TipoTelefone/Delete', 54)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (57, N'Incluir', N'TipoTelefone/Create', 54)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (58, N'Liquidar Título Pendente', N'TituloPendente/Liquidar', null)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (59, N'FeriadoRegional', N'FeriadoRegional/Index', 20)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (60, N'Incluir', N'FeriadoRegional/Create', 59)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (61, N'Alterar', N'FeriadoRegional/Edit', 59)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (62, N'Excluir', N'FeriadoRegional/Delete', 59)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (63, N'Relatórios', NULL, NULL)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (64, N'Extrato', N'Extrato/Index', 63)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (65, N'Fluxo de Caixa', N'FluxoCaixa/Index', 63)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (66, N'Liquidar Diretamente', N'Pagamento/LiquidarDiretamente', 2)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (67, N'Liquidar Diretamente', N'Recebimento/LiquidarDiretamente', 3)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (68, N'Liquidar Diretamente Titulo Pendente', 'TituloPendente/LiquidarDiretamente', NULL)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (69, N'Gerador', 'LoteBoleto/Index', 83)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (70, N'Modelos', 'ModeloBoleto/Index', 83)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (71, N'Incluir', 'ModeloBoleto/Create', 70)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (72, N'Alterar', 'ModeloBoleto/Edit', 70)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (73, N'Excluir', 'ModeloBoleto/Delete', 70)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (74, N'Gerar Boleto', N'Recebimento/GerarBoleto', 3)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (75, N'Boletos Gerados', N'BoletoGerado/Index', 83)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (76, N'Alterar', N'BoletoGerado/Edit', 75)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (77, N'Excluir', N'BoletoGerado/Delete', 75)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (78, N'Conciliação de Extrato', N'Conciliacao/Index', 1)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (79, N'Gerar Boleto', 'TituloPendente/GerarBoleto', NULL)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (80, N'Empresa', 'Empresa/Edit', NULL)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (81, N'Duplicar', N'Pagamento/Duplicar', 2)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (82, N'Duplicar', N'Recebimento/Duplicar', 3)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (83, N'Boletos', NULL, NULL)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (84, N'Parametros', NULL, NULL)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (85, N'Editar', 'Parametro/Edit', 84)
INSERT INTO [dbo].[Funcao] ([Id], [Descricao], [Endereco], [FuncaoSuperiorId]) VALUES (86, N'Faturas', 'FaturaGerada/Index', 83)

SET IDENTITY_INSERT [dbo].[Funcao] ON

SET IDENTITY_INSERT [dbo].[Funcao] OFF

update funcao
set Descricao = 'Fluxo de Caixa'
where id = 65

update funcao
set Descricao = 'Feriado Regional'
where id = 59

UPDATE FUNCAO
set FuncaoSuperiorId = 83,
Descricao = 'Gerador'
where id = 69

UPDATE FUNCAO
set FuncaoSuperiorId = 83,
Descricao = 'Modelos'
where id = 70

UPDATE FUNCAO
set FuncaoSuperiorId = 83
where id = 75

