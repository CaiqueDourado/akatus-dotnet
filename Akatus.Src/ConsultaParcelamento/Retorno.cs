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
    /// Retorno da Consulta de opções de parcelamento
    /// </summary>
    [Serializable]
    public class Retorno
    {
        #region Fields

        private string _descricao;
        private int _parcelas_assumidas;
        private List<RetornoParcela> _parcelas;

        #endregion

        #region Properties

        /// <summary>
        /// Descrição (Percentual de Júros)
        /// </summary>
        public string Descricao
        {
            get
            {
                return (_descricao) ?? string.Empty;
            }
            set
            {
                _descricao = value;
            }
        }

        /// <summary>
        /// Quantidade de parcelas assumidas
        /// </summary>
        public int ParcelasAssumidas
        {
            get
            {
                return _parcelas_assumidas;
            }
            set
            {
                _parcelas_assumidas = value;
            }
        }

        /// <summary>
        /// Quantidade e Valor das Parcelas
        /// </summary>
        public List<RetornoParcela> Parcelas
        {
            get
            {
                if (_parcelas == null)
                    _parcelas = new List<RetornoParcela>();

                return _parcelas;
            }
            set
            {
                _parcelas = value;
            }

        }

        #endregion
    }
}
