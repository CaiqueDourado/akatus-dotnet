/*
 * Akatus API - C# Implementation
 * Caique Dourado
 * http://www.caiquedourado.com.br/
 * Date: 2013-02-26
*/

using System;
using System.Web.Caching;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Akatus.ConsultaParcelamento
{
    /// <summary>
    /// Consulta Opções de parcelamento para um determinado valor e meio de pagamento
    /// </summary>
    public  class Consulta
    {
        //URL's
        private string urlProducao = "http://www.akatus.com/api/v1/parcelamento/simulacao.xml";
        private string urlTestes = "http://dev.akatus.com/api/v1/parcelamento/simulacao.xml";

        #region Methods

        #region Consulta opções de parcelamento para o valor e meio de pagamentos informados

        /// <summary>
        /// Consulta opções de parcelamento para o valor e meio de pagamentos informados (usando CACHE)
        /// </summary>
        /// <param name="amount">Valor.</param>
        /// <param name="payment_method">Meio de Pagamento.</param>
        /// <param name="dataExpiracaoEmCache">Data para o recurso expirar do CACHE.</param>
        public Akatus.ConsultaParcelamento.Retorno consultaParcelamento(decimal amount, Akatus.Enums.MeioDePagamento payment_method, DateTime dataExpiracaoEmCache, out bool retornouDoCache)
        {
            //Chave para armazenar no cache
            string chaveCache = string.Format("AkatusConsultaParcelamento-{0}-{1:0.00}", payment_method, amount);

            //Tenta pegar as imagens do Cache
            Akatus.ConsultaParcelamento.Retorno opcoesParcelamento = System.Web.HttpContext.Current.Cache[chaveCache] as Akatus.ConsultaParcelamento.Retorno;

            if (opcoesParcelamento != null)
            {
                /* Se os dados estão no CACHE */

                //Seta que os dados foram retornados do CACHE
                retornouDoCache = true;

            }
            else {

                /* Se os dados não estão no CACHE */

                //Pega dados do Banco
                opcoesParcelamento = consultaParcelamento(amount, payment_method);

                //Se retornou dados
                if (opcoesParcelamento != null)
                {
                    //Insere dados no Cache
                    System.Web.HttpContext.Current.Cache.Insert(chaveCache, opcoesParcelamento, null, dataExpiracaoEmCache, Cache.NoSlidingExpiration);
                }

                //Seta que os dados não foram retornados do CACHE
                retornouDoCache = false;
            }
    
            
            return opcoesParcelamento;
        }

        /// <summary>
        /// Consulta opções de parcelamento para o valor e meio de pagamentos informados
        /// </summary>
        /// <param name="amount">Valor.</param>
        /// <param name="payment_method">Meio de Pagamento.</param>
        public Akatus.ConsultaParcelamento.Retorno consultaParcelamento(decimal amount, Akatus.Enums.MeioDePagamento payment_method)
        {
            //Armazena dados de retorno
            Akatus.ConsultaParcelamento.Retorno retorno;

            #region Obtém XML

            //URL de Destino (https://www.akatus.com/api/v1/parcelamento/simulacao.xml?email=XXXXXXXXXXXXXXXXXX&amount=XXXXXX&payment_method=XXXXXXX&api_key=XXXXXXXXXXXXXXXXX)
            string urlDestino = string.Format("{0}?email={1}&amount={2}&payment_method={3}&api_key={4}", Akatus.Config.Ambiente == Akatus.Enums.Ambiente.producao ? urlProducao : urlTestes, Akatus.Config.Email, Akatus.Util.formatCurrency(amount), payment_method.ToString(), Akatus.Config.ApiKey);

            //Pega Dados
            string resultado = Akatus.Rest.get(urlDestino);

            //Verifica se o XML é válido
            bool isValidXml = Akatus.Util.IsValidXML(resultado);

            if (isValidXml == true)
            {
                //Cria XML
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();

                //Carrega XML
                xmlDoc.LoadXml(resultado);

                //Preenche dados de retorno
                retorno = new Akatus.ConsultaParcelamento.Retorno();


                //Pega dados
                System.Xml.XmlNodeList xmlResults = xmlDoc.GetElementsByTagName("resposta");

                if (xmlResults.Count > 0)
                {
                    System.Xml.XmlNode xmlResult = xmlResults[0];

                    #region Seta propriedades

                    retorno.Descricao = xmlResult["descricao"].InnerText;
                    retorno.ParcelasAssumidas = Akatus.Util.parseInt(xmlResult["parcelas_assumidas"].InnerText);

                    #endregion
                }
                else
                {
                    //Erro
                    throw new System.ArgumentException("O XML não retornou nós filhos", resultado);
                }


                //Pega dados
                System.Xml.XmlNodeList xmlParcelamento = xmlDoc.GetElementsByTagName("parcela");

                if (xmlParcelamento.Count > 0)
                {
                    foreach (System.Xml.XmlNode xmlResult in xmlParcelamento)
                    {
                        #region Seta propriedades

                        //Preenche dados
                        RetornoParcela retornoParcela = new RetornoParcela();
                        retornoParcela.Quantidade = Akatus.Util.parseInt(xmlResult["quantidade"].InnerText);
                        retornoParcela.Valor = Akatus.Util.parseDecimal(xmlResult["valor"].InnerText);
                        retornoParcela.Total = Akatus.Util.parseDecimal(xmlResult["total"].InnerText);

                        //Adiciona opção de parcelamento
                        retorno.Parcelas.Add(retornoParcela);

                        #endregion
                    }
                }
                else
                {
                    //Erro
                    throw new System.ArgumentException("O XML de parcelas não retornou nós filhos", resultado);
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

        #endregion
    }
}
