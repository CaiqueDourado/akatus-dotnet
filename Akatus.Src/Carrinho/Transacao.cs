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

        private decimal _desconto_total;
        private decimal _peso_total;
        private decimal _frete_total;
        private string _moeda;
        private string _referencia;
        private Akatus.Enums.MeioDePagamento _meioDePagamento;
        private TransacaoCartao _cartao;

        #endregion

        #region Properties

        /// <summary>
        /// Soma de todos os descontos de todos os produtos
        /// </summary>
        public decimal DescontoTotal
        {
            get
            {
                return _desconto_total;
            }
            set
            {
                _desconto_total = value;
            }
        }

        /// <summary>
        /// Soma do peso de todos os produtos
        /// </summary>
        public decimal PesoTotal
        {
            get
            {
                return _peso_total;
            }
            set
            {
                _peso_total = value;
            }
        }

        /// <summary>
        /// Soma do valor de todos os fretes dos produtos
        /// </summary>
        public decimal FreteTotal
        {
            get
            {
                return _frete_total;
            }
            set
            {
                _frete_total = value;
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
