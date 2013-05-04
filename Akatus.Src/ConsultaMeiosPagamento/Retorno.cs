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
    /// Retorno das Bandeiras disponíveis como opção de Meio de Pagamento
    /// </summary>
    [Serializable]
    public class Retorno
    {
        #region Fields

        private string _codigo;
        private string _descricao;
        private int _parcelas;

        #endregion

        #region Properties

        /// <summary>
        /// Código (chave)
        /// </summary>
        public string Codigo
        {
            get
            {
                return (_codigo) ?? string.Empty;
            }
            set
            {
                _codigo = value;
            }
        }

        /// <summary>
        /// Descrição
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
        /// Quantidade de Parcelas
        /// </summary>
        public int Parcelas
        {
            get
            {
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
