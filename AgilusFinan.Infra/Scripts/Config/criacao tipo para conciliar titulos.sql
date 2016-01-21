Create Type TitulosSemVinculo as Table  
(  
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
 ConciliacaoExtratoId int
)  

go

Create Type TitulosNaoCriados as Table  
(  
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
 ConciliacaoExtratoId int
)  
