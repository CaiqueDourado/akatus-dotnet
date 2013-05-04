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
    /// Dados do cliente que efetuará o pagamento da compra, denominado Pagador
    /// </summary>
    public class Pagador
    {
        #region Fields

        private string _nome;
        private string _email;
        private List<PagadorEndereco> _enderecos;
        private List<PagadorTelefone> _telefones;

        #endregion

        #region Properties

        /// <summary>
        /// Nome (Máx. 255 caracteres)
        /// </summary>
        public string Nome
        {
            get
            {
                return (_nome) ?? string.Empty;
            }
            set
            {
                _nome = value;
            }
        }

        /// <summary>
        /// Email (Máx. 255 caracteres)
        /// </summary>
        public string Email
        {
            get
            {
                return (_email) ?? string.Empty;
            }
            set
            {
                _email = value;
            }
        }

        /// <summary>
        /// Endereços
        /// </summary>
        public List<PagadorEndereco> Enderecos
        {
            get
            {
                if (_enderecos == null)
                    _enderecos = new List<PagadorEndereco>();

                return _enderecos;
            }
            set
            {
                _enderecos = value;
            }

        }

        /// <summary>
        /// Telefones
        /// </summary>
        public List<PagadorTelefone> Telefones
        {
            get
            {
                if (_telefones == null)
                    _telefones = new List<PagadorTelefone>();

                return _telefones;
            }
            set
            {
                _telefones = value;
            }

        }

        #endregion
        
    }
}
