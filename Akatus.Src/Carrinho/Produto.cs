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
    /// Produto
    /// </summary>
    public class Produto
    {
        #region Fields

        private string _codigo;
        private string _descricao;
        private int _quantidade;
        private decimal _preco;
        private decimal _peso;
        private decimal _frete;
        private decimal _desconto;

        #endregion

        #region Properties

        /// <summary>
        /// Código do Produto (Máx. 70 caracteres.)
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
        /// Descrição do Produto (Máx. 255 caracteres.)
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
        /// Quantidade
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
        /// Preço
        /// </summary>
        public decimal Preco
        {
            get
            {
                return _preco;
            }
            set
            {
                _preco = value;
            }
        }

        /// <summary>
        /// Peso
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
        /// Valor do Frete
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
        /// Desconto
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

        #endregion
        
    }
}
