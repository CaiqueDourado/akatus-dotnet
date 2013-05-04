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

namespace Akatus.ConsultaMeiosPagamento
{
    /// <summary>
    /// Consulta Meios de pagamento disponíveis para a conta Akatus
    /// </summary>
    public class Consulta
    {
        //URL's
        private string urlProducao = "https://www.akatus.com/api/v1/meios-de-pagamento.xml";
        private string urlTestes = "https://dev.akatus.com/api/v1/meios-de-pagamento.xml";
        
        #region Methods

        #region Consulta Meios de pagamento disponíveis para a conta Akatus

        /// <summary>
        /// Consulta Meios de pagamento disponíveis para a conta Akatus
        /// </summary>
        public List<Akatus.ConsultaMeiosPagamento.Retorno> consultaMeiosDePagamentoDisponiveis()
        {
            //Armazena dados de retorno
            List<Akatus.ConsultaMeiosPagamento.Retorno> retorno;

            //Monta XML
            string xml = montaXML();

            #region Envia XML

            //URL de Destino (https://www.akatus.com/api/v1/meios-de-pagamento.xml)
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

                //Preenche dados de retorno
                retorno = new List<Akatus.ConsultaMeiosPagamento.Retorno>();

                //Pega dados
                System.Xml.XmlNodeList xmlBandeiras = xmlDoc.GetElementsByTagName("bandeira");

                if (xmlBandeiras.Count > 0)
                {
                    foreach (System.Xml.XmlNode xmlResult in xmlBandeiras)
                    {
                        #region Seta propriedades

                        //Preenche dados
                        Akatus.ConsultaMeiosPagamento.Retorno retornoBandeira = new Akatus.ConsultaMeiosPagamento.Retorno();
                        retornoBandeira.Codigo = xmlResult["codigo"].InnerText;
                        retornoBandeira.Descricao = xmlResult["descricao"].InnerText;
                        retornoBandeira.Parcelas = Akatus.Util.parseInt(xmlResult["parcelas"].InnerText);

                        //Adiciona bandeira
                        retorno.Add(retornoBandeira);

                        #endregion
                    }
                }
                else
                {
                    //Erro
                    throw new System.ArgumentException("O XML de bandeiras não retornou nós filhos", resultado);
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
                        //Início do Elemento 'meios_de_pagamento'
                        writer.WriteStartElement("meios_de_pagamento");
                        {
                            //Início do Elemento 'correntista'
                            writer.WriteStartElement("correntista");
                            {
                                //Adiciona elemento 'api_key'
                                writer.WriteElementString("api_key", Akatus.Config.ApiKey);

                                //Adiciona elemento 'email'
                                writer.WriteElementString("email", Akatus.Config.Email);
                            }

                            //Fim do Elemento 'correntista'
                            writer.WriteEndElement();
                        }

                        //Fim do Elemento 'meios_de_pagamento'
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
