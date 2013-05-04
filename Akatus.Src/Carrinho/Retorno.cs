/*
 * Akatus API - C# Implementation
 * Caique Dourado
 * http://www.caiquedourado.com.br/
 * Date: 2013-02-26
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Akatus.Carrinho
{
    /// <summary>
    ///  Armazena todos os dados relacionados a transação.
    /// </summary>
    public class Retorno
    {
        #region Fields

        private string _carrinho;
        private Akatus.Enums.StatusTransacao _status;
        private string _transacao;
        private string _url_retorno;
        private string _referencia;

        #endregion

        #region Properties

        /// <summary>
        /// Identificador do Carrinho
        /// </summary>
        public string Carrinho
        {
            get
            {
                return (_carrinho) ?? string.Empty;
            }
            set
            {
                _carrinho = value;
            }
        }

        /// <summary>
        /// Status da transação
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
        /// Identificador da transação
        /// </summary>
        public string Transacao
        {
            get
            {
                return (_transacao) ?? string.Empty;
            }
            set
            {
                _transacao = value;
            }
        }

        /// <summary>
        /// URL do boleto ou a página do banco, no caso de TEF.
        /// </summary>
        public string UrlRetorno
        {
            get
            {
                return (_url_retorno) ?? string.Empty;
            }
            set
            {
                _url_retorno = value;
            }
        }

        /// <summary>
        /// Identificador da transação, usado para controle interno do seu sistema (Retornado somente quando o meio de pagamento é cartão de crédito)
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
