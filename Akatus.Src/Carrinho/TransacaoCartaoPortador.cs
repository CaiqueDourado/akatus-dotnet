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
    ///  Armazena todos os dados do titular do cartão de crédito utilizado na transação.
    /// </summary>
    public class TransacaoCartaoPortador
    {
        #region Fields

        private string _nome;
        private string _cpf;
        private string _telefone;

        #endregion

        #region Properties

        /// <summary>
        /// Nnome do titular do cartão de crédito exatamente como impresso no cartão.
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
        /// CPF do titular do cartão de crédito utilizado na transação (Formato: 99922233344).
        /// </summary>
        public string CPF
        {
            get
            {
                return (_cpf) ?? string.Empty;
            }
            set
            {
                _cpf = value;
            }
        }

        /// <summary>
        /// Telefone (Código de área 2 dígito + telefone 8 digítos sem espaços e somente múmeros, Em breve serão aceitos 9 dígitos para o telefone.)
        /// </summary>
        public string Telefone
        {
            get
            {
                return (_telefone) ?? string.Empty;
            }
            set
            {
                _telefone = value;
            }
        }

        #endregion
        
    }
}
