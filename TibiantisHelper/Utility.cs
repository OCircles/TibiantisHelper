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
            string[] userData = { "", "", "", "", "", "", "" };

            try
            {

                string webSafe = System.Net.WebUtility.UrlEncode(username);

                HttpResponseMessage response = await webClient.GetAsync("https://tibiantis.online/?page=character&name=" + webSafe);
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
                            var ni = line.IndexOf("Name:") + 5 + 8;
                            var li = line.IndexOf("Level:") + 6 + 8;
                            var vi = line.IndexOf("Vocation:") + 9 + 8;
                            var gi = line.IndexOf("showguild&id");

                            if (gi != -1)
                                gi += 12;

                            var namS = GetBetweenChars(line.Substring(ni), '>', '<');
                            var levS = GetBetweenChars(line.Substring(li), '>', '<');
                            var vocS = GetBetweenChars(line.Substring(vi), '>', '<');

                            if (vocS == "none") vocS = "No Vocation";

                            var prem = "Free Account";
                            if (line.Contains("Premium")) prem = "Premium Account";


                            var guild = "None";
                            var guildID = "";
                            if (gi != -1)
                            {
                                guild = GetBetweenChars(line.Substring(gi), '>', '<');
                                guildID = GetBetweenChars(line.Substring(gi), '=', '\'');
                            }

                            var chars = new List<(string, string)>();
                            var ci = line.IndexOf("<b>Characters</b>");

                            if (ci != -1)
                            {
                                line = line.Substring(ci);
                                ci = line.IndexOf("character&name=");

                                while (ci != -1)
                                {
                                    line = line.Substring(ci);

                                    var chrS = GetBetweenChars(line, '>', '<');

                                    var etd = line.IndexOf("</td>") + 5;
                                    line = line.Substring(etd);

                                    var chlS = GetBetweenChars(line, '>', '<');

                                    (string, string) tup = (chrS, chlS);

                                    if (!username.Equals(chrS,StringComparison.OrdinalIgnoreCase))
                                    chars.Add(tup);

                                    ci = line.IndexOf("character&name=");
                                    if (ci == -1) break;
                                }
                            }

                            userData[0] = namS;
                            userData[1] = levS;
                            userData[2] = CapitalizeString(vocS).TrimEnd(' ');
                            userData[3] = prem;
                            userData[4] = guild;
                            userData[5] = guildID;

                            foreach ( var tup in chars )
                                userData[6] += tup.Item1 + "," + tup.Item2 + ";";

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
