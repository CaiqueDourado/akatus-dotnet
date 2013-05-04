Akatus - Integração com .NET
=================
Biblioteca para integração da API Akatus com .NET
por Caique Dourado - http://www.caiquedourado.com.br


Configuração
=================
Adicione no Web.Config sua API Key, o Tóken NIP, o E-mail utilizado no cadastro da Akatus, e o Ambiente ('producao'
ou 'testes'), ficando dessa forma:
```c#
<appSettings>

  <!-- Akatus - Ambiente ('producao' ou 'testes')-->
  <add key="AkatusAmbiente" value="testes"/>
  
  <!-- Akatus - API Key -->
  <add key="AkatusApiKey" value="SUA-API-KEY"/>
  
  <!-- Akatus - Tóken NIP -->
  <add key="AkatusTokenNIP" value="SEU-TOKEN-NIP"/>
  
  <!-- Akatus - E-mail da Conta -->
  <add key="AkatusEmail" value="emaildecadastro@empresa.com.br"/>
  
</appSettings>
```

CARRINHO
=================

PROCESSAR TRANSAÇÃO
-----------------
```c#
Akatus.Carrinho.Carrinho carrinho = new Akatus.Carrinho.Carrinho();

//Nome e E-mail do Comprador
carrinho.Pagador.Nome = "NOME CLIENTE";
carrinho.Pagador.Email = "email@cliente.com";

//Adiciona Endereço do Comprador
Akatus.Carrinho.PagadorEndereco endereco = new Akatus.Carrinho.PagadorEndereco();
endereco.Tipo = Akatus.Enums.TipoEndereco.entrega;
endereco.Logradouro = "Rua Teste da Silva";
endereco.Numero = 0;
endereco.Bairro = "CENTRO";
endereco.Cidade = "Salvador";
endereco.Estado = "BA";
endereco.Pais = "BRA";
endereco.CEP = "40000000";
carrinho.Pagador.Enderecos.Add(endereco);

//Adiciona Telefone do Comprador
Akatus.Carrinho.PagadorTelefone telefone = new Akatus.Carrinho.PagadorTelefone();
telefone.Tipo = Akatus.Enums.TipoTelefone.celular;
telefone.Numero = "7199990000";
carrinho.Pagador.Telefones.Add(telefone);

//Adiciona Produto
Akatus.Carrinho.Produto produto = new Akatus.Carrinho.Produto();
produto.Codigo = "ABC1234567";
produto.Descricao = "Caixa de bombons sortidos";
produto.Quantidade = 1;
produto.Preco = 32.25m;
produto.Frete = 0;
produto.Peso = 0;
produto.Desconto = 0;
carrinho.Produtos.Add(produto);

//Forma de Pagamento (Boleto)
//carrinho.Transacao.MeioDePagamento = Akatus.Enums.MeioDePagamento.boleto;
//carrinho.Transacao.DescontoTotal = 0;
//carrinho.Transacao.PesoTotal = 0;
//carrinho.Transacao.FreteTotal = 0;
//carrinho.Transacao.Moeda = "BRL";
//carrinho.Transacao.Referencia = "OFP12345";

//Forma de Pagamento (Cartão de Crédito)
carrinho.Transacao.MeioDePagamento = Akatus.Enums.MeioDePagamento.cartao_master;
carrinho.Transacao.Referencia = "OFP12345";
carrinho.Transacao.Cartao.Numero = "5453010000066167";
carrinho.Transacao.Cartao.NumeroParcelas = 2;
carrinho.Transacao.Cartao.CodigoSeguranca = "123";
carrinho.Transacao.Cartao.Expiracao = "05/2018";
carrinho.Transacao.Cartao.Portador.Nome = "AUTORIZAR";
carrinho.Transacao.Cartao.Portador.CPF = "721.726.663-78";
carrinho.Transacao.Cartao.Portador.Portador.Telefone = "7199990000";

carrinho.Transacao.DescontoTotal = 0;
carrinho.Transacao.PesoTotal = 0;
carrinho.Transacao.FreteTotal = 0;
carrinho.Transacao.Moeda = "BRL";

//Envia carrinho
Akatus.Carrinho.Retorno retorno = carrinho.processaTransacao();
```

MEIOS DE PAGAMENTO
=================

OBTER OS MEIOS DE PAGAMENTO ATIVOS EM SUA CONTA AKATUS
-----------------
```c#

Akatus.ConsultaMeiosPagamento.Consulta consulta = new Akatus.ConsultaMeiosPagamento.Consulta();

//Consulta meios de pagamento
List<Akatus.ConsultaMeiosPagamento.Retorno> retorno = consulta.consultaMeiosDePagamentoDisponiveis();
```


OPÇÕES DE PARCELAMENTO
=================

OBTER AS OPÇÕES DE PARCELAMENTO PARA UM DETERMINADO MEIO DE PAGAMENTO E VALOR DO PEDIDO
-----------------
```c#
Akatus.ConsultaParcelamento.Consulta consulta = new Akatus.ConsultaParcelamento.Consulta();

//Consulta opções de parcelamento
Akatus.ConsultaParcelamento.Retorno retorno = consulta.consultaParcelamento(10.39m, Akatus.Enums.MeioDePagamento.cartao_visa);
```

CONSULTA STATUS
=================

OBTER O STATUS DE UMA TRANSAÇÃO
-----------------
```c#

Akatus.ConsultaStatus.Consulta consulta = new Akatus.ConsultaStatus.Consulta();

//Consulta status da transação
Akatus.ConsultaStatus.Retorno retorno = consulta.consultaStatusTransacao("00000000-0000-0000-0000-0000000000");
```

NIP – NOTIFICAÇÃO INSTANTÂNEA DE PAGAMENTO
=================

RECEBE UMA NOTIFICAÇÃO DA AKATUS INFORMANDO A MUDANÇA DO STATUS DE PAGAMENTO DE UMA TRANSAÇÃO
-----------------
```c#
//Pega parâmetros postados
string token = Request.Form["token"];
string transacao_id = Request.Form["transacao_id"];
string status = Request.Form["status"];
string referencia = Request.Form["referencia"];

//Processa retono da transação
Akatus.NotificacaoPagamento.Retorno retorno = Akatus.NotificacaoPagamento.Notificacao.processaRetorno(token, transacao_id, status, referencia);
```

TRATAMENTO DE ERROS
=================

-----------------
```c#
try
{
  //CODE
}
catch (Akatus.RestExcepction ex)
{
	//Show http status
	Response.Write(ex.StatusCode);
	Response.Write(ex.StatusDesciption);

	//Show error messages
	foreach (string error in ex.ErrorMessages)
	{
		Response.Write("<br />" + error);
	}
}
catch (Exception ex) {
	//Show exception message
	Response.Write(ex);
}
```

