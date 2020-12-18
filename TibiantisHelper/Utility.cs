using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TibiantisHelper
{
    class Utility
    {
        public static readonly HttpClient webClient = new HttpClient();

        public static async Task<string[]> GetUserData(string username)
        {
            string[] userData = { "", "", "" };

            try
            {

                HttpResponseMessage response = await webClient.GetAsync("https://tibiantis.online/?page=character&name=" + username.Replace(' ', '+'));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                string line = "";

                StringReader strReader = new StringReader(responseBody);
                while (true)
                {
                    line = strReader.ReadLine();
                    if (line == null) break;
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.Contains("Name:"))
                        {
                            var li = line.IndexOf("Level:") + 6 + 8;
                            var vi = line.IndexOf("Vocation:") + 9 + 8;

                            var levS = GetBetweenChars(line.Substring(li), '>', '<');
                            var vocS = GetBetweenChars(line.Substring(vi), '>', '<');

                            if (vocS == "none") vocS = "No Vocation";

                            userData[0] = levS;
                            userData[1] = CapitalizeString(vocS);
                        }
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }

            if (string.IsNullOrEmpty(userData[0]))
                return null;

            return userData;
        }


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

        public static string CapitalizeString(string name)
        {
            string newName = "";
            int i = 0;
            string[] split = name.Split(' ');

            foreach (string s in split)
            {

                if (s.Length != 0)
                {

                    if (i == 0) newName += char.ToUpper(s[0]) + s.Substring(1);
                    else if (s == "of" || s == "the") newName += s;
                    else newName += char.ToUpper(s[0]) + s.Substring(1);

                    if (i < split.Length) newName += ' ';
                    i++;
                }
            }

            return newName;
        }
    }
}
