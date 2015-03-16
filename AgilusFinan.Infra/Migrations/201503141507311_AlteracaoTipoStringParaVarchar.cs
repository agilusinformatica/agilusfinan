namespace AgilusFinan.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoTipoStringParaVarchar : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Banco", "Nome", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Empresa", "Nome", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Empresa", "EmailContato", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Empresa", "EmailFinanceiro", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Empresa", "Endereco_Logradouro", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Empresa", "Endereco_Numero", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Empresa", "Endereco_Complemento", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Empresa", "Endereco_Bairro", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Empresa", "Endereco_Cidade", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Empresa", "Endereco_Uf", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Empresa", "Endereco_Cep", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Empresa", "Endereco_Pais", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Categoria", "Nome", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.CentroCusto", "Nome", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Conta", "Nome", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Conta", "Agencia", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Conta", "ContaCorrente", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Conta", "Carteira", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Conta", "Convenio", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Conta", "CodigoCedente", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Pagamento", "Descricao", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Pagamento", "Observacao", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Pessoa", "Nome", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Pessoa", "Cpf", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Pessoa", "Rg", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Pessoa", "Endereco_Logradouro", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Pessoa", "Endereco_Numero", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Pessoa", "Endereco_Complemento", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Pessoa", "Endereco_Bairro", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Pessoa", "Endereco_Cidade", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Pessoa", "Endereco_Uf", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Pessoa", "Endereco_Cep", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Pessoa", "Endereco_Pais", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Pessoa", "ContaBancaria_Agencia", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Pessoa", "ContaBancaria_Numero", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Pessoa", "ContaBancaria_NomeTitular", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Pessoa", "ContaBancaria_CpfTitular", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Recebimento", "Descricao", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Recebimento", "Observacao", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.TelefonePessoa", "Telefone_Ddd", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.TelefonePessoa", "Telefone_Numero", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.TipoPessoa", "Nome", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.TipoTelefone", "Nome", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Transferencia", "Descricao", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transferencia", "Descricao", c => c.String());
            AlterColumn("dbo.TipoTelefone", "Nome", c => c.String());
            AlterColumn("dbo.TipoPessoa", "Nome", c => c.String());
            AlterColumn("dbo.TelefonePessoa", "Telefone_Numero", c => c.String());
            AlterColumn("dbo.TelefonePessoa", "Telefone_Ddd", c => c.String());
            AlterColumn("dbo.Recebimento", "Observacao", c => c.String());
            AlterColumn("dbo.Recebimento", "Descricao", c => c.String());
            AlterColumn("dbo.Pessoa", "ContaBancaria_CpfTitular", c => c.String());
            AlterColumn("dbo.Pessoa", "ContaBancaria_NomeTitular", c => c.String());
            AlterColumn("dbo.Pessoa", "ContaBancaria_Numero", c => c.String());
            AlterColumn("dbo.Pessoa", "ContaBancaria_Agencia", c => c.String());
            AlterColumn("dbo.Pessoa", "Endereco_Pais", c => c.String());
            AlterColumn("dbo.Pessoa", "Endereco_Cep", c => c.String());
            AlterColumn("dbo.Pessoa", "Endereco_Uf", c => c.String());
            AlterColumn("dbo.Pessoa", "Endereco_Cidade", c => c.String());
            AlterColumn("dbo.Pessoa", "Endereco_Bairro", c => c.String());
            AlterColumn("dbo.Pessoa", "Endereco_Complemento", c => c.String());
            AlterColumn("dbo.Pessoa", "Endereco_Numero", c => c.String());
            AlterColumn("dbo.Pessoa", "Endereco_Logradouro", c => c.String());
            AlterColumn("dbo.Pessoa", "Rg", c => c.String());
            AlterColumn("dbo.Pessoa", "Cpf", c => c.String());
            AlterColumn("dbo.Pessoa", "Nome", c => c.String());
            AlterColumn("dbo.Pagamento", "Observacao", c => c.String());
            AlterColumn("dbo.Pagamento", "Descricao", c => c.String());
            AlterColumn("dbo.Conta", "CodigoCedente", c => c.String());
            AlterColumn("dbo.Conta", "Convenio", c => c.String());
            AlterColumn("dbo.Conta", "Carteira", c => c.String());
            AlterColumn("dbo.Conta", "ContaCorrente", c => c.String());
            AlterColumn("dbo.Conta", "Agencia", c => c.String());
            AlterColumn("dbo.Conta", "Nome", c => c.String());
            AlterColumn("dbo.CentroCusto", "Nome", c => c.String());
            AlterColumn("dbo.Categoria", "Nome", c => c.String());
            AlterColumn("dbo.Empresa", "Endereco_Pais", c => c.String());
            AlterColumn("dbo.Empresa", "Endereco_Cep", c => c.String());
            AlterColumn("dbo.Empresa", "Endereco_Uf", c => c.String());
            AlterColumn("dbo.Empresa", "Endereco_Cidade", c => c.String());
            AlterColumn("dbo.Empresa", "Endereco_Bairro", c => c.String());
            AlterColumn("dbo.Empresa", "Endereco_Complemento", c => c.String());
            AlterColumn("dbo.Empresa", "Endereco_Numero", c => c.String());
            AlterColumn("dbo.Empresa", "Endereco_Logradouro", c => c.String());
            AlterColumn("dbo.Empresa", "EmailFinanceiro", c => c.String());
            AlterColumn("dbo.Empresa", "EmailContato", c => c.String());
            AlterColumn("dbo.Empresa", "Nome", c => c.String());
            AlterColumn("dbo.Banco", "Nome", c => c.String());
        }
    }
}
