/*
 * Akatus API - C# Implementation
 * Caique Dourado
 * http://www.caiquedourado.com.br/
 * Date: 2013-02-26
*/

using System;

namespace Akatus
{
    public class RestParameter
    {
        #region Fields

        private string _name;
        private object _value;

        #endregion

        #region Properties

        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get { return (_name) ?? string.Empty; }
            set { _name = value; }
        }

        /// <summary>
        /// Value
        /// </summary>
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        #endregion

        #region Constructor

        public RestParameter() { }

        public RestParameter(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        #endregion
    }
}
