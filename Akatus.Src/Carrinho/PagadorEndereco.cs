/*
 * Akatus API - C# Implementation
 * Caique Dourado
 * http://www.caiquedourado.com.br/
 * Date: 2013-02-26
*/

using System;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace Akatus.Carrinho
{
    /// <summary>
    /// Endereço do Pagador
    /// </summary>
    public class PagadorEndereco
    {
        #region Fields

        private Akatus.Enums.TipoEndereco _tipo;
        private string _logradouro;
        private int? _numero;
        private string _bairro;
        private string _complemento;
        private string _cidade;
        private string _estado;
        private string _pais;
        private string _cep;

        #endregion

        #region Properties

        /// <summary>
        /// Tipo de endereço
        /// </summary>
        public Akatus.Enums.TipoEndereco Tipo
        {
            get
            {
                return _tipo;
            }
            set
            {
                _tipo = value;
            }
        }

        /// <summary>
        /// Logradouro (Máx. 255 caracteres)
        /// </summary>
        public string Logradouro
        {
            get
            {
                return (_logradouro) ?? string.Empty;
            }
            set
            {
                _logradouro = value;
            }
        }

        /// <summary>
        /// Número
        /// </summary>
        public int? Numero
        {
            get
            {
                return _numero;
            }
            set
            {
                _numero = value;
            }
        }

        /// <summary>
        /// Bairro (Máx. 50 caracteres)
        /// </summary>
        public string Bairro
        {
            get
            {
                return (_bairro) ?? string.Empty;
            }
            set
            {
                _bairro = value;
            }
        }

        /// <summary>
        /// Complemento (Máx 255 caracteres)
        /// </summary>
        public string Complemento
        {
            get
            {
                return (_complemento) ?? string.Empty;
            }
            set
            {
                _complemento = value;
            }
        }     

        /// <summary>
        /// Cidade (Máx 50 caracteres)
        /// </summary>
        public string Cidade
        {
            get
            {
                return (_cidade) ?? string.Empty;
            }
            set
            {
                _cidade = value;
            }
        }

        /// <summary>
        /// Estado (Máx 2 caracteres)
        /// </summary>
        public string Estado
        {
            get
            {
                return (_estado) ?? string.Empty;
            }
            set
            {
                _estado = value;
            }
        }

        /// <summary>
        /// Pais (3 caracteres)
        /// </summary>
        public string Pais
        {
            get
            {
                return (_pais) ?? string.Empty;
            }
            set
            {
                _pais = value;
            }
        }

        /// <summary>
        /// CEP (8 caracteres)
        /// </summary>
        public string CEP
        {
            get
            {
                return (_cep) ?? string.Empty;
            }
            set
            {
                _cep = value;
            }
        }

        #endregion
        
    }
}
