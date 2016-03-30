using Microsoft.Win32;

using ResourcesComparer.Operations;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace ResourcesComparer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseEnBtn_Click(object sender, RoutedEventArgs e)
        {
            var btnnm = ((Button)sender).Name;
            var openFlDlg = new OpenFileDialog();
            openFlDlg.Filter =  "xaml files (*.xaml)|*.xaml";
            if (openFlDlg.ShowDialog()==true)
            {
                switch(btnnm)
                {
                    case "BrowseEnBtn":
                        {
                            EnFlTxt.Text = openFlDlg.FileName;
                            break;
                        }
                    case "BrowseCnBtn":
                        {
                            CnFlTxt.Text = openFlDlg.FileName;
                            break;
                        }
                    
                }
               
            }
           

        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            CnFlTxt.Text = "";
            EnFlTxt.Text = "";
           
        }

        private void NextBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(CnFlTxt.Text)
                ||
                string.IsNullOrEmpty(EnFlTxt.Text)
                )
            {
                var tlTip = new ToolTip();
                tlTip.Content = ResourceOps.GetResourceString("ErrorFlMissing");
                NextBtn.ToolTip = tlTip;
            }
            else
                NextBtn.ToolTip = null;
            
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(CnFlTxt.Text)
                &&
                !string.IsNullOrEmpty(EnFlTxt.Text)
                )
            {
                var doc = new XDocument();
                var errMessage = "";
                var result= XmlOps.GetUncontainedInCN(EnFlTxt.Text.Trim(),CnFlTxt.Text.Trim(),out doc,out errMessage);
                PublishResults(doc,errMessage);
            }
        }

        void PublishResults(XDocument doc,string error="")
        {
            if (string.IsNullOrEmpty(error))
            {
               if(string.IsNullOrEmpty(XmlFlTxt.Text))
                {
                    doc.Save("Strings.xml");
                    
                }
               else
                {
                    try
                    {
                        doc.Save(XmlFlTxt.Text);
                    }
                    catch
                    {

                    }
                }
                Application.Current.Shutdown();
                Environment.Exit(0);
            }
        
        }

        private void BrowseXmlBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFlDlg = new OpenFileDialog();
            openFlDlg.Filter = "xml files (*.xml)|*.xml";
            if (openFlDlg.ShowDialog() == true)
            {
                XmlFlTxt.Text = openFlDlg.FileName;
            }
        }
    }
}
