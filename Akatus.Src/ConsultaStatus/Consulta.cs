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

namespace Akatus.ConsultaStatus
{
    /// <summary>
    /// Consulta de Status de uma transação
    /// </summary>
    public  class Consulta
    {
        //URL's
        private string urlProducao = "http://www.akatus.com/api/v1/transacao-simplificada";
        private string urlTestes = "http://dev.akatus.com/api/v1/transacao-simplificada";
       
        #region Methods

        #region Consulta status da Transação

        /// <summary>
        /// Consulta status da Transação
        /// </summary>
        /// <param name="idTransacao">ID da transação na Akatus.</param>
        public Akatus.ConsultaStatus.Retorno consultaStatusTransacao(string idTransacao)
        {
            //Armazena dados de retorno
            Akatus.ConsultaStatus.Retorno retorno;

            #region Obtém XML

            //URL de Destino (http://www.akatus.com/api/v1/transacao-simplificada/ID_TRANSACAO_AKATUS.xml?email=EMAIL_RECEBEDOR&api_key=TOKEN_GERADO_AKATUS)
            string urlDestino = string.Format("{0}/{1}.xml?email={2}&api_key={3}", Akatus.Config.Ambiente == Akatus.Enums.Ambiente.producao ? urlProducao : urlTestes, idTransacao, Akatus.Config.Email, Akatus.Config.ApiKey);

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

                //Pega dados
                System.Xml.XmlNodeList xmlResults = xmlDoc.GetElementsByTagName("resposta");

                if (xmlResults.Count > 0)
                {
                    System.Xml.XmlNode xmlResult = xmlResults[0];

                    //Preenche dados de retorno
                    retorno = new Akatus.ConsultaStatus.Retorno();

                    #region Seta propriedades

                    retorno.Valor = Akatus.Util.parseDecimalWithoutSeparator(xmlResult["valor"].InnerText);
                    retorno.DataCriacao = Akatus.Util.parseDateTime(xmlResult["data_criacao"].InnerText);
                    retorno.DataStatusAtual = Akatus.Util.parseDateTime(xmlResult["data_status_atual"].InnerText);
                    
                    string status = xmlResult["status"].InnerText;

                    //Seta status
                    if (status == "Aguardando Pagamento")
                        retorno.Status = Akatus.Enums.StatusTransacao.aguardandoPagamento;
                    else if (status == "Em Análise")
                        retorno.Status = Akatus.Enums.StatusTransacao.emAnalise;
                    else if (status == "Aprovado")
                        retorno.Status = Akatus.Enums.StatusTransacao.aprovado;
                    else if (status == "Cancelado")
                        retorno.Status = Akatus.Enums.StatusTransacao.cancelado;
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

                    retorno.Referencia = xmlResult["referencia"].InnerText;

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

        #endregion
    }
}
