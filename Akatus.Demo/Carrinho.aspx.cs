using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Carrinho : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Akatus.Carrinho.Carrinho carrinho = new Akatus.Carrinho.Carrinho();

        #region Preenche dados

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
        carrinho.Transacao.Cartao.Portador.Telefone = "7199990000";
        carrinho.Transacao.DescontoTotal = 0;
        carrinho.Transacao.PesoTotal = 0;
        carrinho.Transacao.FreteTotal = 0;
        carrinho.Transacao.Moeda = "BRL";

        #endregion

        #region Processa transação

        try
        {

            //Envia carrinho
            Akatus.Carrinho.Retorno retorno = carrinho.processaTransacao();

            //Se os dados foram postados com sucesso
            if (retorno != null)
            {
                Response.Write("carrinho = " + retorno.Carrinho + "<br />");
                Response.Write("status = " + retorno.Status + "<br />");
                Response.Write("transacao = " + retorno.Transacao + "<br />");
                Response.Write("url_retorno = " + retorno.UrlRetorno + "<br />");
                Response.Write("referencia = " + retorno.Referencia + "<br />");
            }
            else
            {
                Response.Write("Não foram retornados dados");
            }
        }
        catch (Akatus.RestExcepction ex)
        {
            //Show http status
            Response.Write(string.Format("statusCode =  {0} <br /> statusDescription = {1}", ex.StatusCode, ex.StatusDesciption));

            //Show error messages
            foreach (string error in ex.ErrorMessages)
                Response.Write("<br />" + error);
        }
        catch (Exception ex) {

            Response.Write(ex);

        }

        #endregion

    }

    
}