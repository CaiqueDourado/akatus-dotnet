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
    /// Retorno da Consulta de Status
    /// </summary>
    [Serializable]
    public class Retorno
    {
        #region Fields

        private decimal _valor;
        private DateTime? _data_criacao;
        private DateTime? _data_status_atual;
        private Akatus.Enums.StatusTransacao _status;
        private string _referencia;

        #endregion

        #region Properties

        /// <summary>
        /// Valor do Pedido
        /// </summary>
        public decimal Valor
        {
            get
            {
                return _valor;
            }
            set
            {
                _valor = value;
            }
        }

        /// <summary>
        /// Data de criação
        /// </summary>
        public DateTime? DataCriacao
        {
            get
            {
                return _data_criacao;
            }
            set
            {
                _data_criacao = value;
            }
        }

        /// <summary>
        /// Data do Status atual
        /// </summary>
        public DateTime? DataStatusAtual
        {
            get
            {
                return _data_status_atual;
            }
            set
            {
                _data_status_atual = value;
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
