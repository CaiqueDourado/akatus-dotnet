/*
 * Akatus API - C# Implementation
 * Caique Dourado
 * http://www.caiquedourado.com.br/
 * Date: 2013-02-26
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Akatus
{
    [Serializable]
    public class RestExcepction : System.Exception
    {
        #region Fields

        private int _statusCode;
        private string _statusDescription;
        private List<string> _errorMessages;

        #endregion

        #region Properties

        /// <summary>
        /// HTTP Status Code
        /// </summary>
        public int StatusCode
        {
            get
            {
                return _statusCode;
            }
            set
            {
                _statusCode = value;
            }
        }

        /// <summary>
        /// HTTP Status Description
        /// </summary>
        public string StatusDesciption
        {
            get
            {
                return (_statusDescription) ?? string.Empty;
            }
            set
            {
                _statusDescription = value;
            }
        }

        /// <summary>
        /// Error Messages
        /// </summary>
        public List<string> ErrorMessages
        {
            get
            {
                if (_errorMessages == null)
                    _errorMessages = new List<string>();

                return _errorMessages;
            }
            set
            {
                _errorMessages = value;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Rest Exception
        /// </summary>
        /// <param name="xmlData">XML string</param>
        /// <param name="statusCode">HTTP Status Code</param>
        /// <param name="statusDesciption">HTTP Status Description</param>
        public RestExcepction(string errorMessage, int statusCode, string statusDesciption)
        {
            this.StatusCode = statusCode;
            this.StatusDesciption = statusDesciption;

            //Checks if the xml is valid
            if (Akatus.Util.IsValidXML(errorMessage))
            {
                //Create XML
                XmlDocument xmlDoc = new XmlDocument();

                //Load XML
                xmlDoc.LoadXml(errorMessage);

                //Get errors in XML format
                XmlNodeList xmlErrors = xmlDoc.GetElementsByTagName("descricao");

                //Fill ErrorsList
                foreach (XmlNode error in xmlErrors)
                    this.ErrorMessages.Add(error.InnerText);
            }
            else
            {
                //Fill ErrorsList
                this.ErrorMessages.Add(errorMessage);
            }

        }

        #endregion
    }
}