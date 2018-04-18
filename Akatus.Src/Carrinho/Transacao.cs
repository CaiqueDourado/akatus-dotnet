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
    public class Transacao
    {
        #region Fields

        private decimal _desconto;
        private decimal _peso;
        private decimal _frete;
        private string _moeda;
        private string _referencia;
        private Akatus.Enums.MeioDePagamento _meioDePagamento;
        private TransacaoCartao _cartao;

        #endregion

        #region Properties

        /// <summary>
        /// Desconto dado para toda a transação (ao invés de desconto por produto)
        /// </summary>
        public decimal Desconto
        {
            get
            {
                return _desconto;
            }
            set
            {
                _desconto = value;
            }
        }

        /// <summary>
        /// Peso geral para toda a transação (ao invés de peso específico por produto)
        /// </summary>
        public decimal Peso
        {
            get
            {
                return _peso;
            }
            set
            {
                _peso = value;
            }
        }

        /// <summary>
        /// Frete para toda a transação (ao invés de ser específico por produto)
        /// </summary>
        public decimal Frete
        {
            get
            {
                return _frete;
            }
            set
            {
                _frete = value;
            }
        }

        /// <summary>
        /// Moeda usada na transação (O único valor aceito hoje é BRL)
        /// </summary>
        public string Moeda
        {
            get
            {
                return (_moeda) ?? string.Empty;
            }
            set
            {
                _moeda = value;
            }
        }

        /// <summary>
        /// Identificador da transação que pode ser usada para controle interno do seu sistema (Máx. 20 caracteres)
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

        /// <summary>
        /// Tipo de pagamento que será feito (boleto, tef_itau e tef_bradesco)
        /// </summary>
        public Akatus.Enums.MeioDePagamento MeioDePagamento
        {
            get
            {
                return _meioDePagamento;
            }
            set
            {
                _meioDePagamento = value;
            }
        }

        /// <summary>
        /// Armazena os dados do cartão de crédito
        /// </summary>
        public TransacaoCartao Cartao
        {
            get
            {
                if (_cartao == null)
                    _cartao = new TransacaoCartao();

                return _cartao;
            }
            set
            {
                _cartao = value;
            }
        }

        #endregion        
    }
}
