using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TibiantisHelper
{
    class Utility
    {
        public static void StringToClipboard(string str)
        {
            Clipboard.SetText(str);
            SystemSounds.Asterisk.Play();
        }

        public static string GetBetweenChars(string input, char start, char end)
        {
            string between = "";
            bool began = false;

            foreach (char c in input)
            {
                if (began)
                {
                    if (c == end) break;
                    between += c;
                }
                if (c == start) began = true;
            }


            return between;
        }

        public static void OpenInBrowser(string target)
        {
            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }
    
    
    }
}
