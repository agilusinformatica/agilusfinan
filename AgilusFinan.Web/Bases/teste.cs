
public class Rootobject
{
    public Class1[] Property1 { get; set; }
}

public class Class1
{
    public string itemExtratoId { get; set; }
    public bool selectionado { get; set; }
    public bool isNew { get; set; }
    public Titulosselecionado[] titulosSelecionados { get; set; }
    public object[] titulosIncluidos { get; set; }
}

public class Titulosselecionado
{
    public object TituloId { get; set; }
    public string TituloRecorrenteId { get; set; }
    public string Descricao { get; set; }
    public int Valor { get; set; }
    public string Direcao { get; set; }
    public string DataVencimento { get; set; }
    public int Acrescimo { get; set; }
    public int Desconto { get; set; }
}
