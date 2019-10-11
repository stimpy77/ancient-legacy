using System;
using System.Collections.Generic;
using System.Text;
using Google;

namespace Google
{
    class Program
    {
        static Google.GoogleSearchService Googler = new Google.GoogleSearchService();
        const string GOOGLE_KEY = "YtrTZLpQFHLLfyQ5/GjKXYH8bBumLUdj";
        static bool bRunning = true;
        static string lastQuery = "";

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (!ProcessArgs(args)) return;
            }
            while (bRunning)
            {
                Console.WriteLine("Enter a Google query: [query] [-pagesize / -size / -s [##]] [-page / -p [##]]");
                Console.Write("? ");
                string input = Console.ReadLine();
                if (input.ToLower().Trim() != "exit")
                {
                    if (!ProcessArgs(input.Split(' '))) return;
                }
                else
                {
                    bRunning = false;
                }
            }
        }

        static bool ProcessArgs(string[] args)
        {
            string query = "";
            int page = 1;
            int pageSize = 5;
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                switch (arg.ToLower())
                {
                    case "exit":
                    case "quit":
                    case "bye":
                    case "x":
                    case "q":
                        return false;
                    case "-p":
                    case "-page":
                    case "/p":
                    case "/page":
                        i++;
                        page = int.Parse(args[i]);
                        break;
                    case "-s":
                    case "-size":
                    case "-pagesize":
                    case "/s":
                    case "/size":
                    case "/pagesize":
                        i++;
                        pageSize = int.Parse(args[i]);
                        break;
                    case "-?":
                    case "-help":
                    case "/?":
                    case "/help":
                        ShowHelp();
                        return false;
                    default:
                        if (query != "") query += " ";
                        query += arg;
                        break;
                }
            }
            if (query != "")
            {
                if (!QueryGoogle(query, page, pageSize)) return false;
            }
            return true;
        }

        static void ShowHelp()
        {
            Console.WriteLine("Google [query] [-pagesize / -size / -s [##]] [-page / -p [##]]");
        }

        static List<global::Google.Google.ResultElement> resultElementsList = new List<global::Google.Google.ResultElement>();

        static bool QueryGoogle(string query, int page, int pageSize)
        {
            if (page == 1)
            {
                resultElementsList.Clear();
                Console.WriteLine("Querying for " + query + " ...");
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.CursorTop -= 1;
                    ClearLine();
                }
            }
            Google.GoogleSearchResult results = QueryGoogle(
                query, (page - 1) * pageSize, pageSize, true, "", true, "");

            lastQuery = query;

            //for (int r=0; r<results.resultElements.Length; r++)
            resultElementsList.AddRange(results.resultElements);

            global::Google.Google.ResultElement[] resultElements = results.resultElements;
            for (int i = 0; i < resultElements.Length; i++)
            {
                global::Google.Google.ResultElement resultElement = resultElements[i];
                int p = ((page - 1) * pageSize) + (i + 1);
                Console.WriteLine();
                Console.Write(p.ToString());
                TabifyPara(HtmlToText(resultElement.title), 2);
                TabifyPara(HtmlToText(resultElement.summary), 2);
                TabifyPara(HtmlToText(resultElement.snippet), 2);
                TabifyPara(HtmlToText(resultElement.URL), 2);
            }
            bool bReturn = false;
            while (!bReturn)
            {
                Console.WriteLine();
                Console.WriteLine("# = Open URL; [ENTER] = Next " + pageSize.ToString() + "; [query] = New Search; r = Restart; x/q = Exit");
                Console.Write("> ");
                bReturn = true;
                string cmd = Console.ReadLine();
                if (cmd.ToLower().Trim() == "exit") return false;
                int item = 0;
                if (int.TryParse(cmd.Trim(), out item))
                {
                    bReturn = false;
                    LaunchBrowser(resultElementsList[item - 1].URL);
                    //try
                    //{
                    //    item = item - ((page - 1) * pageSize) - 1;
                    //    LaunchBrowser(resultElements[item].URL);
                    //}
                    //catch (IndexOutOfRangeException e)
                    //{
                    //    Console.WriteLine(e.Message);
                    //}
                }
                else if (cmd.Trim() == "")
                {
                    return QueryGoogle(query, page + 1, pageSize); 
                }
                else if (cmd.ToLower().Trim() == "r") return true;
                else if (cmd.ToLower().Trim() == "x" || 
                    cmd.ToLower().Trim() == "q" ||
                    cmd.ToLower().Trim() == "exit") return false;
                else
                {
                    if (cmd != lastQuery) page = 1;
                    return QueryGoogle(cmd, page, pageSize);
                }
            }
            return true;
        }

        static void ClearLine()
        {
            int curLeft = Console.CursorLeft;
            Console.CursorLeft = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                sb.Append(" ");
            }
            Console.Write(sb.ToString());
            Console.CursorTop -= 1; // cursor "suffered" a line break after falling off the edge
            Console.CursorLeft = curLeft;
        }

        static void DrawLine()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                sb.Append("_");
            }
            Console.WriteLine(sb);
        }

        static void TabifyPara(string para, int tabs)
        {
            para = para.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
            int len = Console.BufferWidth - (tabs * 5) - 1;
            while (para.Length > 0)
            {
                Console.CursorLeft = tabs * 5;
                if (para.Length > len)
                {
                    string line = para.Substring(0, len);
                    if (line.Contains(" "))
                    {
                        line = line.Substring(0, line.LastIndexOf(' ')).Trim();
                    }

                    // render
                    Console.Write(line + "\r\n");

                    para = para.Substring(line.Length);
                }
                else
                {
                    // render
                    Console.Write(para.Trim() + "\r\n");

                    para = "";
                }
            }
        }

        static string HtmlToText(string html)
        {
            System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
            try
            {
                xdoc.LoadXml("<html>" + html.Replace("<br>", "<br />") + "</html>");
                return xdoc.InnerText;
            }
            catch
            {
                return html;
            }
        }

        static void LaunchBrowser(string url)
        {
            Console.WriteLine("Launching " + url + "...");
            System.Diagnostics.Process.Start(url);
        }

        /// <summary>
        /// Executes a Google search.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="start">Zero-based index of the first desired result.</param>
        /// <param name="maxResults">Number of results desired per query. <remarks>The maximum value per query is 10. Note: If you do a query that doesn't have many matches, the actual number of results you get may be smaller than what you request.</remarks></param>
        /// <param name="filter">Activates or deactivates automatic results filtering, which hides very similar results and results that all come from the same Web host. Filtering tends to improve the end user experience on Google, but for your application you may prefer to turn it off.</param>
        /// <param name="restrict">Restricts the search to a subset of the Google Web index, such as a country like "Ukraine" or a topic like "Linux."</param>
        /// <param name="safeSearch">A Boolean value which enables filtering of adult content in the search results.</param>
        /// <param name="langRestrict">Restricts the search to documents within one or more languages. </param>
        /// <returns></returns>
        static Google.GoogleSearchResult QueryGoogle(
            string query, int start, int maxResults, bool filter, string restrict, bool safeSearch,
            string langRestrict)
        {
            bool retry = true;
            while (retry)
            {
                try
                {
                    return Googler.doGoogleSearch(GOOGLE_KEY, query, start, maxResults,
                        filter, restrict, safeSearch, langRestrict, "", "");
                }
                catch (Exception e)
                {
                    if (e.Message == "The request failed with HTTP status 502: Bad Gateway.")
                    {
                        Console.WriteLine("Retrying ...");
                        retry = true;
                    }
                    else
                    {
                        retry = false;
                        throw e;
                    }
                }
            }
            return null;
        }
    }
}
