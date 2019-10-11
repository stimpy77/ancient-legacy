// ====================================
//          AssemblyLister
// ====================================
//
// Author:	Jon Davis <jon@jondavis.net>
// 		    http://www.jondavis.net/
//
// Date:	4/10/2006
// Updated: 4/30/2008
//
// ====================================
// Abstract:
// ====================================
// This is a console application that will search for
// CLR-loadable assemblies in a specified path and
// also optionally execute ngen.exe which should be
// located in the path identified by the "CLRDIR"
// environment variable.
//
// ====================================
// To build:
// ====================================
// Create a console app in Visual Studio 2005 and
// replace the contents of Program.cs with the entire
// content of this web page. In the project properties,
// I recommend naming the assembly "AssemLst.exe".
// Once built, I also recommend copying it to your
// Framework directory, e.g.
// C:\Windows\Microsoft.net\Framework\v2.*\
//
// ====================================
// To use:
// ====================================
// Just execute ...
//
//   AssemLst.exe /?
//
// ... to learn how to use the console application.
//
// ====================================
// Your license:
// ====================================
// You may compile this console app for personal
// use. You may not redistribute the compiled
// application, but you may redistribute this source
// as long as author credit is recognized.
//


using System;
using System.Collections.Specialized;
using System.Reflection;
using System.IO;

namespace AssemblyLister
{
    class Program
    {
        static int NGEN_TIMEOUT = 30000;

        static System.IO.FileInfo fi = new FileInfo(Assembly.GetExecutingAssembly().Location);
        static string BaseArgHelp = "\r\n"
                + fi.Name + " [ -s ] [ -ngen [ -uninstall ] ] [ directory or file ]\r\n\r\n"
                + "Evaluates the specified file(s) or directory(ies) to determine whether they are CLR-loadable assemblies.\r\n\r\n"
                + "-s\t\t\tScans subdirectories. (Default is DISABLED.)\r\n\n"
                + "-v\t\t\tVerbose mode. (Default is DISABLED.)\r\n\n"
                + "-ngen [-uninstall]\tInstalls [or uninstalls] the assembly by pre-JIT'ing\r\n\t\t\twith ngen.exe."
                + " (Default is DISABLED.)\r\n\n"
                + "[directory]\t\tScans the specified path for CLR assembly files.\r\n"
                + "\t\t\tMatches are listed.\r\n\n"
                + "[file]\t\t\tScans the specified file to determine if it is an\r\n"
                + "\t\t\tassembly file. If it is, the path is repeated.\r\n\n";
        static void Main(string[] args)
        {
            bool bFollowSubDirs = false;
            bool bNgen = false;
            bool bVerbose = false;
            bool bNgenUninstall = false;
            int f = 0;
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                if (arg.StartsWith("-") || arg.StartsWith("/"))
                {
                    switch (arg.ToLower())
                    {
                        case "-s":
                        case "/s":
                            bFollowSubDirs = true;
                            break;
                        case "-v":
                        case "/v":
                            bVerbose = true;
                            break;
                        case "-ngen":
                        case "/ngen":
                            bNgen = true;
                            try
                            {
                                if (args[i + 1].ToLower() == "-uninstall" || args[i + 1].ToLower() == "/uninstall")
                                {
                                    bNgenUninstall = true;
                                    i++;
                                }
                            }
                            catch { }
                            break;
                        case "-?":
                        case "/?":
                        case "-help":
                        case "/help":
                        default:
                            Console.WriteLine(BaseArgHelp);
                            return;
                    }
                }
                else f++;
            }
            if (f == 0)
            {
                Console.WriteLine(BaseArgHelp);
                return;
            }
            if (bVerbose) Console.WriteLine();
            for (int i = 0; i < args.Length; i++)
            {
			string arg = args[i];
				if (arg == ".") arg = Directory.GetCurrentDirectory();
				if (!(arg.StartsWith("-") || arg.StartsWith("/")))
				{
					if (File.Exists(arg))
					{
						try {
	                        EvalFileForCLR(arg, true);
						} catch { }
						if (bNgen)
						{
							try
							{
								RunNgen(arg, bNgenUninstall, bVerbose);
							}
							catch (Exception e)
							{
								System.Console.WriteLine(e.Message);
								return; // abort
							}
						}
					}
					else if (Directory.Exists(arg))
					{
						string[] matches = EvalDirForCLR(arg, bFollowSubDirs, true, bNgen);
						if (matches.Length > 0)
						{
							if (bVerbose) Console.WriteLine("\r\nFound " + matches.Length + " matches in " + arg + ".\r\n");
						}
						//foreach (string m in matches)
						//{
						//    Console.WriteLine(m);
						//}
						if (bNgen)
						{
							if (bVerbose)
							{
								Console.WriteLine();
								Console.WriteLine("Executing NGen ...");
								Console.WriteLine();
							}
							foreach (string m in matches)
							{
								try
								{
									RunNgen(m, bNgenUninstall, bVerbose);
								}
								catch (Exception ex)
								{
									System.Console.WriteLine("Error executing NGen: " + ex.Message);
									return; // abort
								}
							}
						}
					}
				}
            }
#if DEBUG
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
#endif
        }

        static bool EvalFileForCLR(string file, bool bOutputToConsole)
        {
            try
            {
                AssemblyName.GetAssemblyName(file);
                if (bOutputToConsole) Console.WriteLine(file);
                return true;
            }
            catch
            {
                return false;
            }
        }

        static string[] EvalDirForCLR(string dir, bool bSubdirs, bool bOutputToConsole, bool forNgen)
        {
            StringCollection sc = new StringCollection();
            StringCollection files = new StringCollection();
            try
            {
                files.AddRange(Directory.GetFiles(dir, "*.exe"));
                files.AddRange(Directory.GetFiles(dir, "*.dll"));
            }
            catch { }
            foreach (string file in files)
            {
                if (EvalFileForCLR(file, bOutputToConsole))
                {
                    sc.Add(file);
                }
            }
            if (bSubdirs)
            {
                string[] subfolders = new string[] {};
                try
                {
                    subfolders = Directory.GetDirectories(dir);
                }
                catch { }
                foreach (string sf in subfolders)
                {
                    sc.AddRange(Program.EvalDirForCLR(sf, bSubdirs, true, forNgen));
                    if (forNgen)
                    {
                        for (int f = sc.Count - 1; f >= 0; f--)
                        {
                            if ((new DirectoryInfo(sc[f])).Attributes == FileAttributes.Hidden)
                            {
                                Console.WriteLine("Removing hidden directory " + sc[f]);
                                sc.RemoveAt(f);
                            }
                            else if (sc[f].ToLower().StartsWith(Environment.GetEnvironmentVariable("windir").ToLower() + "\\assembly") ||
                                sc[f].ToLower().StartsWith(Environment.GetEnvironmentVariable("windir").ToLower() + "\\microsoft.net") ||
                                sc[f].ToLower().Contains("$recycle"))
                            {
                                Console.WriteLine("Removing system resource " + sc[f]);
                                sc.RemoveAt(f);
                            }
                        }
                    }
                }
            }
            string[] ret = new string[sc.Count];
            sc.CopyTo(ret, 0);
            return ret;
        }

        static void RunNgen(string assem, bool bUninstall, bool bVerbose)
        {
            string curdir = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(Directory.GetParent(assem).FullName);
            string dnpath = System.Environment.GetEnvironmentVariable("CLRDIR");
            string sNgen = dnpath + "\\ngen.exe";
            string sNgenArgs = "";
            if (bUninstall) sNgenArgs += "uninstall ";
            else sNgenArgs += "install ";
            sNgenArgs += "\"" + assem + "\"";
            if (bVerbose) Console.WriteLine("Executing: [ " + sNgen + " " + sNgenArgs + " ]");
            else Console.WriteLine(sNgen + " " + sNgenArgs);
            System.Diagnostics.ProcessStartInfo spi = new System.Diagnostics.ProcessStartInfo(sNgen, sNgenArgs);
            spi.CreateNoWindow = true;
            spi.RedirectStandardError = true;
            spi.RedirectStandardInput = true;
            spi.RedirectStandardOutput = true;
            spi.UseShellExecute = false;
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(spi);
            if (bVerbose)
            {
                p.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(NGen_OutputDataReceived);
                p.BeginOutputReadLine();
            }
            //p.WaitForExit();
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Reset();
            sw.Start();
            while (!p.HasExited)
            {
                if (sw.ElapsedMilliseconds > NGEN_TIMEOUT)
                {
                    Console.Error.WriteLine("NGEN has not responded in " 
                        + ((decimal)NGEN_TIMEOUT / 1000).ToString() + " seconds, killing...");
                    p.Kill();
                    break;
                }
                System.Threading.Thread.Sleep(100);
            }
            sw.Stop();
            Directory.SetCurrentDirectory(curdir);
        }

        static void NGen_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
    }
}


