/*
 * Akatus API - C# Implementation
 * Caique Dourado
 * http://www.caiquedourado.com.br/
 * Date: 2013-02-26
*/

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;

namespace Akatus
{
    public static class Config
    {
        #region API Key

        /// <summary>
        /// API Key (Chave de conexão com a API)
        /// </summary>
        public static string ApiKey
        {
            get
            {
                //Takes value of configuration key 'AkatusApiKey' in Web.Config
                string apiKey = System.Configuration.ConfigurationManager.AppSettings["AkatusApiKey"];

                return apiKey;
            }
        }

        #endregion

        #region Tóken NIP

        /// <summary>
        /// Tóken NIP (Tóken da Notificação Instantânea de Pagamento)
        /// </summary>
        public static string tokenNIP
        {
            get
            {
                //Takes value of configuration key 'AkatusTokenNIP' in Web.Config
                string tokenNIP = System.Configuration.ConfigurationManager.AppSettings["AkatusTokenNIP"];

                return tokenNIP;
            }
        }

        #endregion

        #region Email

        /// <summary>
        /// Email
        /// </summary>
        public static string Email
        {
            get
            {
                //Takes value of configuration key 'AkatusEmail' in Web.Config
                string email = System.Configuration.ConfigurationManager.AppSettings["AkatusEmail"];

                return email;
            }
        }

        #endregion

        #region Ambiente

        /// <summary>
        /// Ambiente (Produção ou Testes)
        /// </summary>
        public static Akatus.Enums.Ambiente Ambiente
        {
            get
            {
                //Takes value of configuration key 'AkatusAmbiente' in Web.Config
                string ambiente = System.Configuration.ConfigurationManager.AppSettings["AkatusAmbiente"];

                return (ambiente != null && ambiente == "producao") ? Akatus.Enums.Ambiente.producao : Akatus.Enums.Ambiente.testes;
            }
        }

        #endregion
    }
}
