/*
 * Akatus API - C# Implementation
 * Caique Dourado
 * http://www.caiquedourado.com.br/
 * Date: 2013-02-26
*/

using System;
using System.Xml;
using System.IO;
using System.Net;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;

namespace Akatus
{
    public static class Util
    {
        #region Conversions

        /// <summary>
        /// Converts Integer
        /// </summary>
        /// <param name="obj">Object</param>
        public static int parseInt(object obj)
        {
            int ret;

            try
            {
                string str = parseString(obj);
                ret = int.Parse(str);
            }
            catch
            {
                ret = 0;
            }

            return ret;
        }

        /// <summary>
        /// Converts Float
        /// </summary>
        /// <param name="obj">Object</param>
        public static float parseFloat(object obj)
        {
            float ret;

            try
            {
                string str = parseString(obj);
                ret = float.Parse(str);
            }
            catch
            {
                ret = 0;
            }

            return ret;
        }

        /// <summary>
        /// Converts Decimal
        /// </summary>
        /// <param name="obj">Object</param>
        public static decimal parseDecimal(object obj)
        {
            decimal ret;

            try
            {
                string str = parseString(obj);

                System.Globalization.CultureInfo culInfo = new System.Globalization.CultureInfo("en-US");

                //Separador de decimais: '.'
                ret = decimal.Parse(str, System.Globalization.NumberStyles.Number, culInfo.NumberFormat);
            }
            catch
            {
                ret = 0;
            }

            return ret;
        }

        /// <summary>
        /// Converts Decimal (Without decimal point)
        /// </summary>
        /// <param name="obj">Object</param>
        public static decimal parseDecimalWithoutSeparator(object obj)
        {
            decimal ret;

            try
            {
                string str = parseString(obj);

                //Formats with decimal separator
                str = string.Format("{0,0:N2}", int.Parse(str) / 100.0);

                //Separador de decimais: '.'
                ret = decimal.Parse(str);
            }
            catch
            {
                ret = 0;
            }

            return ret;
        }

        /// <summary>
        /// Converts String
        /// </summary>
        /// <param name="obj">Object</param>
        public static String parseString(object obj)
        {
            return (obj != null) ? obj.ToString() : String.Empty;
        }

        /// <summary>
        /// Converts Boolean
        /// </summary>
        /// <param name="obj">Object</param>
        public static bool parseBool(object obj)
        {
            bool retorno;

            try
            {
                if ((obj.ToString().ToLower() == "0") || (obj.ToString().ToLower() == "false") || (obj.ToString().ToLower() == String.Empty))
                    retorno = false;
                else
                    retorno = true;
            }
            catch
            {
                retorno = false;
            }

            return retorno;
        }

        /// <summary>
        /// Converts Datetime
        /// </summary>
        /// <param name="obj">Object</param>
        public static DateTime? parseDateTime(object obj)
        {
            try
            {
                string str = parseString(obj);
                return DateTime.Parse(str);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Format a Decimal Value (returns a dot instead of a comma to separate decimals)
        /// </summary>
        public static string formatCurrency(object obj)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");

            //Format value (default '0000.00')
            return string.Format(culture, "{0:0.00}", obj);
        }

        #endregion

        #region Validation

        /// <summary>
        /// Validate XML Format
        /// </summary>
        /// <param name="text">XML string</param>
        public static bool IsValidXML(string text)
        {
            if (text == null)
                return false;

            bool errored;

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(text);
            MemoryStream stream = new MemoryStream(byteArray);

            XmlTextReader xmlr = new XmlTextReader(stream);
            XmlValidatingReader reader = new XmlValidatingReader(xmlr);

            try
            {
                while (reader.Read()) { ; }
                errored = false;
            }
            catch
            {
                errored = true;
            }
            finally
            {
                reader.Close();
            }

            return !errored;
        }

        #endregion
    }
}
