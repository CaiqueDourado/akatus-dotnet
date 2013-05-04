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

namespace Akatus.ConsultaParcelamento
{
    /// <summary>
    /// Retorno das opções de parcelamento
    /// </summary>
    [Serializable]
    public class RetornoParcela
    {
        #region Fields

        private int _quantidade;
        private decimal _valor;
        private decimal _total;

        #endregion

        #region Properties

        /// <summary>
        /// Quantidade de Parcelas
        /// </summary>
        public int Quantidade
        {
            get
            {
                return _quantidade;
            }
            set
            {
                _quantidade = value;
            }
        }

        /// <summary>
        /// Valor da Parcela
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
        /// Valor total
        /// </summary>
        public decimal Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
            }
        }

        #endregion
    }
}
