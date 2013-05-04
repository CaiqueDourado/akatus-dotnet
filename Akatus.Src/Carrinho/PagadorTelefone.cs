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
    /// Telefone do Pagador
    /// </summary>
    public class PagadorTelefone
    {
        #region Fields

        private Akatus.Enums.TipoTelefone _tipo;
        private string _numero;

        #endregion

        #region Properties

        /// <summary>
        /// Tipo de telefone
        /// </summary>
        public Akatus.Enums.TipoTelefone Tipo
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
        /// Número (Código de área 2 dígito + telefone 8 digítos sem espaços e somente múmeros, Em breve serão aceitos 9 dígitos para o telefone.)
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

        #endregion
        
    }
}
