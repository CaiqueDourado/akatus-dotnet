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

namespace Akatus.NotificacaoPagamento
{
    /// <summary>
    /// NIP – Notificação Instantânea de Pagamento
    /// </summary>
    public class Notificacao
    {
        #region Methods

        #region Processa retorno

        /// <summary>
        /// Processa notificação da transação
        /// </summary>
        /// <param name="token">Tóken do NIP usado para validar se o NIP foi realmente enviado pela Akatus.</param>
        /// <param name="transacao_id">ID da transação na Akatus.</param>
        /// <param name="status">Status do pagamento.</param>
        /// <param name="referencia">ID da transação em sua loja (Caso tenha sido enviado)</param>
        public static Akatus.NotificacaoPagamento.Retorno processaRetorno(string token, string transacao_id, string status, string referencia)
        {
            //Armazena dados de retorno
            Akatus.NotificacaoPagamento.Retorno retorno;

            //Verifica se o 'Tóken', 'Id da transação' e 'Status' foram postados (o campo 'referencia' é opcional)
            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(transacao_id) && !string.IsNullOrEmpty(status))
            {
                //Verifica se o Tóken está correto
                if (token == Akatus.Config.tokenNIP)
                {
                    #region Se o Tóken postado é o mesmo Tóken que está configurado em sua conta

                    //Preenche dados de retorno
                    retorno = new Akatus.NotificacaoPagamento.Retorno();
                    retorno.TransacaoId = transacao_id;
                    retorno.Referencia = referencia;

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

                    #endregion
                }
                else
                {
                    #region Se o Tóken postado é diferente do Tóken que está configurado em sua conta

                    //Erro
                    throw new System.ArgumentException("Tóken inválido", token);

                    #endregion
                }
            }
            else
            {
                #region Se foram postados parâmetros

                //Erro
                throw new System.ArgumentException("Não foram postados parâmetros");

                #endregion
            }

            return retorno;
        }

        #endregion

        #endregion
    }
}
