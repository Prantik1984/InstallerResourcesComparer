using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceComparerConsole
{
    class Program
    {
        #region ***vars
        static string _enXaml = "";// the English version of the xaml file.
        static string _cnXaml = "";// the Chinese version of the xaml file. 
        static string _flXml = "";//the xml file to which the output is to be saved
        #endregion

        static void Main(string[] args)
        {
            ReadArgs(args);
        }

        /// <summary>
        /// reads the arguments
        /// </summary>
        /// <param name="args"></param>
        static void ReadArgs(string[] args)
        {
            foreach(var arg in args)
            {
                if (arg.StartsWith("-e"))
                     _enXaml = arg.Trim().Substring(2);

                if (arg.StartsWith("-c"))
                    _cnXaml = arg.Trim().Substring(2);

                if (arg.StartsWith("-x"))
                    _flXml = arg.Trim().Substring(2);

            }
        }
    }
}
