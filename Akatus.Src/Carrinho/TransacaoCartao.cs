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
    ///  Armazena todos os dados do cartão de crédito
    /// </summary>
    public class TransacaoCartao
    {
        #region Fields

        private string _numero;
        private int _numeroParcelas;
        private string _codigoSeguranca;
        private string _expiracao;
        private TransacaoCartaoPortador _portador;

        #endregion

        #region Properties

        /// <summary>
        /// Número do cartão de crédito usado na transação. (Máx. 20 caracteres)
        /// </summary>
        public string Numero
        {
            get
            {
                return (_numero) ?? string.Empty;
            }
            set
            {
                _numero = value;
            }
        }

        /// <summary>
        /// Número de parcelas deste pagamento.
        /// </summary>
        public int NumeroParcelas
        {
            get
            {
                return _numeroParcelas;
            }
            set
            {
                _numeroParcelas = value;
            }
        }

        /// <summary>
        /// Código de segurança do cartão de crédito usado na transação. Este campo também é conhecido como CVC2 e CVV2 (Máx. 5 dígitos).
        /// </summary>
        public string CodigoSeguranca
        {
            get
            {
                return (_codigoSeguranca) ?? string.Empty;
            }
            set
            {
                _codigoSeguranca = value;
            }
        }

        /// <summary>
        /// Data de validade/expiração do cartão de crédito usado na transação (Formato: MM/AAAA).
        /// </summary>
        public string Expiracao
        {
            get
            {
                return (_expiracao) ?? string.Empty;
            }
            set
            {
                _expiracao = value;
            }
        }

        /// <summary>
        /// Armazena todos os dados do titular do cartão de crédito utilizado na transação.
        /// </summary>
        public TransacaoCartaoPortador Portador
        {
            get
            {
                if (_portador == null)
                    _portador = new TransacaoCartaoPortador();

                return _portador;
            }
            set
            {
                _portador = value;
            }
        }

        #endregion
        
    }
}
