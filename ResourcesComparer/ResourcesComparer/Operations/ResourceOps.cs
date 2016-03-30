using System.Windows;

namespace ResourcesComparer.Operations
{
    /// <summary>
    /// Performs the various operations 
    /// pertaining to resources
    /// </summary>
    internal static  class ResourceOps
    {
        /// <summary>
        /// Gets the string from resource
        /// one line of code to avoid duplicacies
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static string GetResourceString(string name)
        {
            try
            {
                return Application.Current.Resources[name].ToString();
            }
            catch
            {
                return "";
            }
        }
    }
}
