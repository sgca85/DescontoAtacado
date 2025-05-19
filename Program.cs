var produtos = new List<Produto>()
{
    new Produto { Gtin = 7891024110348, Descricao = "SABONETE OLEO DE ARGAN 90G PALMOLIVE", PrecoVarejo = 2.88m, PrecoAtacado = 2.51m, UnidadesAtacado = 12 },
    new Produto { Gtin = 7891048038017, Descricao = "CHÁ DE CAMOMILA DR.OETKER", PrecoVarejo = 4.40m, PrecoAtacado = 4.37m, UnidadesAtacado = 3 },
    new Produto { Gtin = 7896066334509, Descricao = "TORRADA TRADICIONAL WICKBOLD PACOTE", PrecoVarejo = 5.19m, PrecoAtacado = null, UnidadesAtacado = null },
    new Produto { Gtin = 7891700203142, Descricao = "BEBIDA À BASE DE SOJA MAÇÃ ADES CAIXA 200ML", PrecoVarejo = 2.39m, PrecoAtacado = 2.38m, UnidadesAtacado = 6 },
    new Produto { Gtin = 7894321711263, Descricao = "ACHOCOLATADO PÓ ORIGINAL TODDY POTE 400G", PrecoVarejo = 9.79m, PrecoAtacado = null, UnidadesAtacado = null, },
    new Produto { Gtin = 7896001250611, Descricao = "ADOÇANTE LÍQUIDO SUCRALOSE LINEA CAIXA 25ML", PrecoVarejo = 9.89m, PrecoAtacado = 9.10m, UnidadesAtacado = 10, },
    new Produto { Gtin = 7793306013029, Descricao = "CEREAL MATINAL CHOCOLATE KELLOGGS SUCRILHOS CAIXA 320G", PrecoVarejo = 12.79m, PrecoAtacado = 12.35m, UnidadesAtacado = 3, },
    new Produto { Gtin = 7896004400914, Descricao = "COCO RALADO SOCOCO 50G", PrecoVarejo = 4.20m, PrecoAtacado = 4.05m, UnidadesAtacado = 6, },
    new Produto { Gtin = 7898080640017, Descricao = "LEITE UHT INTEGRAL 1L COM TAMPA ITALAC", PrecoVarejo = 6.99m, PrecoAtacado = 6.89m, UnidadesAtacado = 12, },
    new Produto { Gtin = 7891025301516, Descricao = "DANONINHO PETIT SUISSE COM POLPA DE MORANGO 360G DANONE", PrecoVarejo = 12.99m, PrecoAtacado = null, UnidadesAtacado = null, },
    new Produto { Gtin = 7891030003115, Descricao = "CREME DE LEITE LEVE 200G MOCOCA", PrecoVarejo = 3.12m, PrecoAtacado = 3.09m, UnidadesAtacado = 4, },
};

var itens = new List<ItemVenda>
{
    new ItemVenda { Id = 1, Gtin = 7891048038017, Quantidade = 1, Parcial = 4.40m, },
    new ItemVenda { Id = 2, Gtin = 7896004400914, Quantidade = 4, Parcial = 16.80m, },
    new ItemVenda { Id = 3, Gtin = 7891030003115, Quantidade = 1, Parcial = 3.12m, },
    new ItemVenda { Id = 4, Gtin = 7891024110348, Quantidade = 6, Parcial = 17.28m, },
    new ItemVenda { Id = 5, Gtin = 7898080640017, Quantidade = 24, Parcial = 167.76m, },
    new ItemVenda { Id = 6, Gtin = 7896004400914, Quantidade = 8, Parcial = 33.60m, },
    new ItemVenda { Id = 7, Gtin = 7891700203142, Quantidade = 8, Parcial = 19.12m, },
    new ItemVenda { Id = 8, Gtin = 7891048038017, Quantidade = 1, Parcial = 4.40m, },
    new ItemVenda { Id = 9, Gtin = 7793306013029, Quantidade = 3, Parcial = 38.37m, },
    new ItemVenda { Id = 10, Gtin = 7896066334509, Quantidade = 2, Parcial = 10.38m, },
};

var descontos = new List<DescontoItem>();
decimal subtotal, totalDescontos, total;

Console.WriteLine("--- Desconto no Atacado ---\n");

foreach (var grupo in itens.GroupBy(i => i.Gtin))
{
    var produto = produtos.Single(p => p.Gtin == grupo.Key);

    int qtdItens = (int)itens
        .Where(i => i.Gtin == grupo.Key)
        .Sum(i => i.Quantidade);

    if (qtdItens >= produto.UnidadesAtacado)
        descontos.Add(new DescontoItem
        {
            Gtin = grupo.Key,
            Desconto = Math.Abs((produto.PrecoAtacado ?? 0) - produto.PrecoVarejo) * qtdItens,
        });
}

Console.WriteLine("Descontos:");
foreach (var desconto in descontos)
    Console.WriteLine($"{desconto.Gtin,13}   {desconto.Desconto,12:C2}");

subtotal = itens.Sum(i => i.Parcial);
totalDescontos = descontos.Sum(d => d.Desconto);
total = subtotal - totalDescontos;

Console.WriteLine();
Console.WriteLine($"(+) Subtotal  = {subtotal,12:C2}");
Console.WriteLine($"(-) Descontos = {totalDescontos,12:C2}");
Console.WriteLine($"(=) Total     = {total,12:C2}");

class Produto
{
    public long Gtin { get; set; }
    public string Descricao { get; set; } = default!;
    public decimal PrecoVarejo { get; set; }
    public decimal? PrecoAtacado { get; set; }
    public int? UnidadesAtacado { get; set; }
}

class ItemVenda
{
    public int Id { get; set; }
    public long Gtin { get; set; }
    public long Quantidade { get; set; }
    public decimal Parcial { get; set; }
}

class DescontoItem
{
    public long Gtin { get; set; }
    public decimal Desconto { get; set; }
}