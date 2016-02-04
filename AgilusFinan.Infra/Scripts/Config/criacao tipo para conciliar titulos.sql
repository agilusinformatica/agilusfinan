drop procedure pr_conciliar_titulos
drop type titulossemvinculo
drop type titulosnaocriados

Create Type TitulosSemVinculo as Table  
(  
 id int primary key clustered,
 TituloId int,
 TituloRecorrenteId int,
 Descricao varchar(100),
 PessoaId int not null,
 ContaId int not null,
 Valor money not null,
 CategoriaId int not null,
 DataVencimento smalldatetime not null,
 Acrescimo money,
 Desconto money,
 DataLancamento smalldatetime not null,
 ConciliacaoExtratoId varchar(100)
)  

go

Create Type TitulosNaoCriados as Table  
(  
 id int primary key clustered,
 ContaId int not null,
 DataVencimento smalldatetime not null,
 Descricao varchar(100),
 Valor money not null,
 CategoriaId int not null,
 PessoaId int not null,
 CentroCustoId int,
 Competencia smalldatetime,
 Observacao varchar(8000),
 DataLancamento smalldatetime not null,
 Acrescimo money,
 Desconto money,
 ConciliacaoExtratoId varchar(100)
)  
