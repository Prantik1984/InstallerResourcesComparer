using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System;

namespace ResourcesComparer.Operations
{
    /// <summary>
    /// performs various xml operations
    /// </summary>
    internal static class XmlOps
    {
        /// <summary>
        /// Gets the string values not contained in CN file
        /// </summary>
        internal static bool GetUncontainedInCN(string enfl,string cnfl,out XDocument resourcesnotfound,out string errormessage)
        {
            resourcesnotfound = new XDocument();
            XDocument enxml = null;
            XDocument cnxml = null;
            errormessage = "";
            XElement rtNotFound = new XElement("Strings");
            if (!File.Exists(enfl))
            {
                errormessage = string.Format("{0}:{1}", ResourceOps.GetResourceString("NoExistFl"), enfl);
                return false;
            }
            if (!File.Exists(cnfl))
            {
                errormessage = string.Format("{0}:{1}", ResourceOps.GetResourceString("NoExistFl"), cnfl);
                return false;
            }

            try
            {
                enxml = XDocument.Parse(File.ReadAllText(enfl));
            }
            catch
            {
                errormessage = string.Format("{0}:{1}", ResourceOps.GetResourceString("InvFl"), enfl);
                return false;
            }

            try
            {
                cnxml = XDocument.Parse(File.ReadAllText(cnfl));
            }
            catch
            {
                errormessage = string.Format("{0}:{1}", ResourceOps.GetResourceString("InvFl"), cnfl);
                return false;
            }
            try
            {
                foreach (var enstr in enxml.Root.Descendants())
                {
                    var nm = enstr.Attributes().FirstOrDefault().Value;
                    if (cnxml.Root.Descendants()
                        .Any(a => a.Attributes().FirstOrDefault().Value == nm)
                        )
                        continue;
                    XElement elem = new XElement("String", new XAttribute("Key", nm));
                    elem.Value = enstr.Value;
                    rtNotFound.Add(elem);
                }
            }
           catch(Exception ex)
            {
                errormessage = string.Format("{0}:{1}",ResourceOps.GetResourceString("Error"),ex.Message);
                return false;
            }
            resourcesnotfound.Add(rtNotFound);
            return true;
        }
    }
}
