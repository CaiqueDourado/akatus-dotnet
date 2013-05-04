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
    /// Retorno da Notificação de Pagamento
    /// </summary>
    [Serializable]
    public class Retorno
    {
        #region Fields

        private string _transacao_id;
        private Akatus.Enums.StatusTransacao _status;
        private string _referencia;

        #endregion

        #region Properties

        /// <summary>
        /// ID da transação na Akatus.
        /// </summary>
        public string TransacaoId
        {
            get
            {
                return (_transacao_id) ?? string.Empty;
            }
            set
            {
                _transacao_id = value;
            }
        }

        /// <summary>
        /// Status do pagamento.
        /// </summary>
        public Akatus.Enums.StatusTransacao Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        /// <summary>
        /// ID da transação em sua loja (Caso tenha sido enviado)
        /// </summary>
        public string Referencia
        {
            get
            {
                return (_referencia) ?? string.Empty;
            }
            set
            {
                _referencia = value;
            }
        }

        #endregion
    }
}
