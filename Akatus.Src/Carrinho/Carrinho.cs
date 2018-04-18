/*
 * Akatus API - C# Implementation
 * Caique Dourado
 * http://www.caiquedourado.com.br/
 * Date: 2013-02-26
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Akatus.Carrinho
{
    /// <summary>
    /// Carrinho
    /// </summary>
    public class Carrinho
    {
        //URL's
        private string urlProducao = "https://www.akatus.com/api/v1/carrinho.xml";
        private string urlTestes = "https://dev.akatus.com/api/v1/carrinho.xml";

        #region Campos

        private Pagador _pagador;
        private List<Produto> _produtos;
        private Transacao _transacao;

        #endregion

        #region Propriedades

        /// <summary>
        /// Dados do cliente que efetuará o pagamento da compra, denominado Pagador
        /// </summary>
        public Pagador Pagador
        {
            get
            {
                if (_pagador == null)
                    _pagador = new Pagador();

                return _pagador;
            }
            set
            {
                _pagador = value;
            }

        }

        /// <summary>
        /// Lista de Produtos no carrinho (Min 1 / Máx 100)
        /// </summary>
        public List<Produto> Produtos
        {
            get
            {
                if (_produtos == null)
                    _produtos = new List<Produto>();

                return _produtos;
            }
            set
            {
                _produtos = value;
            }

        }

        /// <summary>
        /// Armazena todos os dados relacionados a transação
        /// </summary>
        public Transacao Transacao
        {
            get
            {
                if (_transacao == null)
                    _transacao = new Transacao();

                return _transacao;
            }
            set
            {
                _transacao = value;
            }

        }

        #endregion     

        #region Methods

        #region Processa Transação

        public Akatus.Carrinho.Retorno processaTransacao()
        {
            //Armazena dados de retorno
            Akatus.Carrinho.Retorno retorno;

            //Monta XML
            string xml = montaXML();

            #region Envia XML

            //URL de Destino (https://www.akatus.com/api/v1/carrinho.xml)
            string urlDestino = Akatus.Config.Ambiente == Akatus.Enums.Ambiente.producao ? urlProducao : urlTestes;

            //Envia Dados
            string resultado = Akatus.Rest.post(urlDestino, xml);

            //Verifica se o XML é válido
            bool isValidXml = Akatus.Util.IsValidXML(resultado);

            if (isValidXml == true)
            {
                //Cria XML
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();

                //Carrega XML
                xmlDoc.LoadXml(resultado);

                //Pega dados
                System.Xml.XmlNodeList xmlResults = xmlDoc.GetElementsByTagName("resposta");

                if (xmlResults.Count > 0)
                {
                    System.Xml.XmlNode xmlResult = xmlResults[0];

                    //Dados de Resposta
                    retorno = new Akatus.Carrinho.Retorno();

                    #region Seta propriedades

                    retorno.Carrinho = xmlResult["carrinho"].InnerText;
                    retorno.Transacao = xmlResult["transacao"].InnerText;

                    string status = xmlResult["status"].InnerText.ToString();

                    if (status == "Aguardando Pagamento")
                        retorno.Status = Enums.StatusTransacao.aguardandoPagamento;
                    else if (status == "Em Análise")
                        retorno.Status = Enums.StatusTransacao.emAnalise;
                    else if (status == "Aprovado")
                        retorno.Status = Enums.StatusTransacao.aprovado;
                    else if (status == "Cancelado")
                        retorno.Status = Enums.StatusTransacao.cancelado;
                    else if (status == "Processando")
                        retorno.Status = Akatus.Enums.StatusTransacao.processando;
                    else if (status == "Completo")
                        retorno.Status = Akatus.Enums.StatusTransacao.completo;
                    else if (status == "Devolvido")
                        retorno.Status = Akatus.Enums.StatusTransacao.devolvido;
                    else if (status == "Estornado")
                        retorno.Status = Akatus.Enums.StatusTransacao.estornado;
                    else if (status == "Chargeback")
                        retorno.Status = Akatus.Enums.StatusTransacao.chargeback;

                    if (this.Transacao.MeioDePagamento == Akatus.Enums.MeioDePagamento.boleto
                        || this.Transacao.MeioDePagamento == Akatus.Enums.MeioDePagamento.tef_itau
                        || this.Transacao.MeioDePagamento == Akatus.Enums.MeioDePagamento.tef_bradesco)
                    {
                        //URL do boleto ou a página do banco, no caso de TEF
                        retorno.UrlRetorno = xmlResult["url_retorno"].InnerText;
                    }
                    else if (this.Transacao.MeioDePagamento == Akatus.Enums.MeioDePagamento.cartao_amex
                        || this.Transacao.MeioDePagamento == Akatus.Enums.MeioDePagamento.cartao_diners
                        || this.Transacao.MeioDePagamento == Akatus.Enums.MeioDePagamento.cartao_elo
                        || this.Transacao.MeioDePagamento == Akatus.Enums.MeioDePagamento.cartao_master
                        || this.Transacao.MeioDePagamento == Akatus.Enums.MeioDePagamento.cartao_visa)
                    {
                        //Identificador da transação, usado para controle interno do seu sistema (Retornado somente quando o meio de pagamento é cartão de crédito)
                        retorno.Referencia = xmlResult["referencia"].InnerText;
                    }

                    #endregion
                }
                else
                {
                    //Erro
                    throw new System.ArgumentException("O XML não retornou nós filhos", resultado);
                }

            }
            else
            {
                //Erro
                throw new System.ArgumentException("Formato de XML inválido", resultado);
            }

            #endregion

            //Retorna resposta
            return retorno;
        }

        #endregion

        #region Monta XML

        private string montaXML()
        {
            //Armazena o XML final
            string xml;

            #region Seta configurações de encoding (UTF-8)

            Encoding utf8noBOM = new UTF8Encoding(false);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = utf8noBOM;

            #endregion

            #region Monta XML

            using (MemoryStream output = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(output, settings))
                {
                    //Início do Documento
                    writer.WriteStartDocument();
                    {
                        //Início do Elemento 'carrinho'
                        writer.WriteStartElement("carrinho");
                        {
                            #region Recebedor

                            //Início 'recebedor'
                            writer.WriteStartElement("recebedor");
                            {
                                //Adiciona elemento 'api_key'
                                writer.WriteElementString("api_key", Akatus.Config.ApiKey);

                                //Adiciona elemento 'email'
                                writer.WriteElementString("email", Akatus.Config.Email);
                            };

                            //Fim do Elemento 'recebedor'
                            writer.WriteEndElement();

                            #endregion

                            #region Pagador

                            //Início 'pagador'
                            writer.WriteStartElement("pagador");
                            {
                                //Adiciona elemento 'nome'
                                writer.WriteElementString("nome", this.Pagador.Nome);

                                //Adiciona elemento 'email'
                                writer.WriteElementString("email", this.Pagador.Email);

                                #region Endereços

                                //Início 'enderecos'
                                writer.WriteStartElement("enderecos");
                                {
                                    #region Varre endereços

                                    foreach (Akatus.Carrinho.PagadorEndereco objEndereco in this.Pagador.Enderecos)
                                    {
                                        //Início 'endereco'
                                        writer.WriteStartElement("endereco");
                                        {
                                            //Adiciona elemento 'tipo' (entrega ou comercial)
                                            writer.WriteElementString("tipo", objEndereco.Tipo.ToString().ToLower());

                                            //Adiciona elemento 'logradouro'
                                            writer.WriteElementString("logradouro", objEndereco.Logradouro);

                                            //Adiciona elemento 'numero'
                                            if (objEndereco.Numero != null)
                                                writer.WriteElementString("numero", Akatus.Util.parseString(objEndereco.Numero));

                                            //Adiciona elemento 'bairro'
                                            writer.WriteElementString("bairro", objEndereco.Bairro);

                                            //Adiciona elemento 'cidade'
                                            writer.WriteElementString("cidade", objEndereco.Cidade);

                                            //Adiciona elemento 'estado' (Somente sigla)
                                            writer.WriteElementString("estado", objEndereco.Estado);

                                            //Adiciona elemento 'pais' (Aceita somente o valor “BRA”)
                                            writer.WriteElementString("pais", objEndereco.Pais);

                                            //Adiciona elemento 'cep'
                                            writer.WriteElementString("cep", objEndereco.CEP);

                                        }

                                        //Fim do Elemento 'endereco'
                                        writer.WriteEndElement();

                                    }

                                    #endregion
                                }

                                //Fim do Elemento 'enderecos'
                                writer.WriteEndElement();

                                #endregion

                                #region Telefones

                                #region Varre telefones

                                foreach (Akatus.Carrinho.PagadorTelefone objTelefone in this.Pagador.Telefones)
                                {
                                    //Início 'telefones'
                                    writer.WriteStartElement("telefones");
                                    {
                                        //Início 'telefone'
                                        writer.WriteStartElement("telefone");
                                        {
                                            //Adiciona elemento 'tipo' (comercial,residencial ou celular)
                                            writer.WriteElementString("tipo", objTelefone.Tipo.ToString().ToLower());

                                            //Adiciona elemento 'numero'
                                            writer.WriteElementString("numero", objTelefone.Numero);
                                        }

                                        //Fim do Elemento 'telefone'
                                        writer.WriteEndElement();
                                    }

                                    //Fim do Elemento 'telefones'
                                    writer.WriteEndElement();
                                }

                                #endregion

                                #endregion
                            };

                            //Fim do Elemento 'pagador'
                            writer.WriteEndElement();

                            #endregion

                            #region Produtos

                            //Início 'produtos'
                            writer.WriteStartElement("produtos");
                            {
                                #region Produto

                                #region Varre produtos

                                foreach (Akatus.Carrinho.Produto obProduto in this.Produtos)
                                {
                                    //Início 'produto'
                                    writer.WriteStartElement("produto");
                                    {
                                        //Adiciona elemento 'codigo'
                                        writer.WriteElementString("codigo", obProduto.Codigo);

                                        //Adiciona elemento 'descricao'
                                        writer.WriteElementString("descricao", obProduto.Descricao);

                                        //Adiciona elemento 'quantidade'
                                        writer.WriteElementString("quantidade", Akatus.Util.parseString(obProduto.Quantidade));

                                        //Adiciona elemento 'preco' (casas decimais separadas por ponto)
                                        writer.WriteElementString("preco", Akatus.Util.formatCurrency(obProduto.Preco));

                                        //Adiciona elemento 'peso' (casas decimais separadas por ponto)
                                        writer.WriteElementString("peso", Akatus.Util.formatCurrency(obProduto.Peso));

                                        //Adiciona elemento 'frete' (casas decimais separadas por ponto)
                                        writer.WriteElementString("frete", Akatus.Util.formatCurrency(obProduto.Frete));

                                        //Adiciona elemento 'desconto' (casas decimais separadas por ponto)
                                        writer.WriteElementString("desconto", Akatus.Util.formatCurrency(obProduto.Desconto));
                                    }

                                    //Fim do Elemento 'produto'
                                    writer.WriteEndElement();
                                }

                                #endregion

                                #endregion
                            };

                            //Fim do Elemento 'produtos'
                            writer.WriteEndElement();

                            #endregion

                            #region Transação

                            //Início 'transacao'
                            writer.WriteStartElement("transacao");
                            {
                                if (this.Transacao.MeioDePagamento == Akatus.Enums.MeioDePagamento.boleto
                                    || this.Transacao.MeioDePagamento == Akatus.Enums.MeioDePagamento.tef_itau
                                    || this.Transacao.MeioDePagamento == Akatus.Enums.MeioDePagamento.tef_bradesco)
                                {
                                    #region Boleto ou Cartão de Débito

                                    //Adiciona elemento 'desconto'
                                    writer.WriteElementString("desconto", Akatus.Util.formatCurrency(this.Transacao.Desconto));

                                    //Adiciona elemento 'peso'
                                    writer.WriteElementString("peso", Akatus.Util.formatCurrency(this.Transacao.Peso));

                                    //Adiciona elemento 'frete'
                                    writer.WriteElementString("frete", Akatus.Util.formatCurrency(this.Transacao.Frete));

                                    //Adiciona elemento 'moeda' (O único valor aceito hoje é BRL)
                                    writer.WriteElementString("moeda", this.Transacao.Moeda);

                                    //Adiciona elemento 'referencia'
                                    writer.WriteElementString("referencia", this.Transacao.Referencia);

                                    //Adiciona elemento 'meio_de_pagamento' (boleto, tef_itau e tef_bradesco)
                                    writer.WriteElementString("meio_de_pagamento", this.Transacao.MeioDePagamento.ToString().ToLower());

                                    #endregion
                                }
                                else if (this.Transacao.MeioDePagamento == Akatus.Enums.MeioDePagamento.cartao_amex
                                    || this.Transacao.MeioDePagamento == Akatus.Enums.MeioDePagamento.cartao_diners
                                    || this.Transacao.MeioDePagamento == Akatus.Enums.MeioDePagamento.cartao_elo
                                    || this.Transacao.MeioDePagamento == Akatus.Enums.MeioDePagamento.cartao_master
                                    || this.Transacao.MeioDePagamento == Akatus.Enums.MeioDePagamento.cartao_visa)
                                {
                                    #region Cartão de Crédito

                                    //Adiciona elemento 'numero'
                                    writer.WriteElementString("numero", this.Transacao.Cartao.Numero);

                                    //Adiciona elemento 'parcelas'
                                    writer.WriteElementString("parcelas", Akatus.Util.parseString(this.Transacao.Cartao.NumeroParcelas));

                                    //Adiciona elemento 'codigo_de_seguranca'
                                    writer.WriteElementString("codigo_de_seguranca", this.Transacao.Cartao.CodigoSeguranca);

                                    //Adiciona elemento 'expiracao'
                                    writer.WriteElementString("expiracao", this.Transacao.Cartao.Expiracao);

                                    //Adiciona elemento 'desconto'
                                    writer.WriteElementString("desconto", Akatus.Util.formatCurrency(this.Transacao.Desconto));

                                    //Adiciona elemento 'peso'
                                    writer.WriteElementString("peso", Akatus.Util.formatCurrency(this.Transacao.Peso));

                                    //Adiciona elemento 'frete'
                                    writer.WriteElementString("frete", Akatus.Util.formatCurrency(this.Transacao.Frete));

                                    //Adiciona elemento 'moeda' (O único valor aceito hoje é BRL)
                                    writer.WriteElementString("moeda", this.Transacao.Moeda);

                                    //Adiciona elemento 'referencia'
                                    writer.WriteElementString("referencia", this.Transacao.Referencia);

                                    //Adiciona elemento 'meio_de_pagamento' (cartao_visa, cartao_master, cartao_amex, cartao_elo e cartao_diners)
                                    writer.WriteElementString("meio_de_pagamento", this.Transacao.MeioDePagamento.ToString().ToLower());

                                    //Adiciona elemento 'portador'
                                    writer.WriteStartElement("portador");
                                    {
                                        //Adiciona elemento 'nome'
                                        writer.WriteElementString("nome", this.Transacao.Cartao.Portador.Nome);

                                        //Adiciona elemento 'cpf'
                                        writer.WriteElementString("cpf", this.Transacao.Cartao.Portador.CPF);

                                        //Adiciona elemento 'telefone'
                                        writer.WriteElementString("telefone", this.Transacao.Cartao.Portador.Telefone);
                                    }

                                    //Fim do Elemento 'portador'
                                    writer.WriteEndElement();

                                    #endregion
                                }
                            };

                            //Fim do Elemento 'transacao'
                            writer.WriteEndElement();

                            #endregion
                        }

                        //Fim do Elemento 'carrinho'
                        writer.WriteEndElement();
                    }

                    //Fim do Documento
                    writer.WriteEndDocument();
                }

                //Retorna string em 'UTF-8'
                xml = Encoding.UTF8.GetString(output.ToArray());
            }

            #endregion

            #region DEBUG

            /*
            System.Web.HttpContext.Current.Response.ContentType = "text/xml";
            System.Web.HttpContext.Current.Response.Write(xml);
            System.Web.HttpContext.Current.Response.End();
            */

            #endregion

            return xml;
        }

        #endregion

        #endregion
    }
}
