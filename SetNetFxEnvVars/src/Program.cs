using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Win32;
using System.Runtime.InteropServices; 

namespace SetNetFxEnvVars
{
    class Program
    {
        static void Main(string[] args)
        {
            WinApi.SYSTEM_INFO inf = new WinApi.SYSTEM_INFO();
            WinApi.GetSystemInfo(ref inf);
            bool is_x64 = inf.uProcessorInfo.wProcessorArchitecture == (ushort)9;
            List<string> winfx_x86_dirs = new List<string>(Directory.GetDirectories(
                Environment.GetEnvironmentVariable("windir") + "\\Microsoft.NET\\Framework\\", "v*"));
            int tmp;
            for (int i = winfx_x86_dirs.Count - 1; i >= 0; i--)
                if (!int.TryParse(Path.GetFileName(winfx_x86_dirs[i]).Substring(1, 1), out tmp))
                    winfx_x86_dirs.RemoveAt(i);
            winfx_x86_dirs.Sort();
            List<string> winfx_x64_dirs = null;
            if (is_x64)
            {
                winfx_x64_dirs = new List<string>(Directory.GetDirectories(
                    Environment.GetEnvironmentVariable("windir") + "\\Microsoft.NET\\Framework64\\", "v*"));
                for (int i = winfx_x64_dirs.Count - 1; i >= 0; i--)
                    if (!int.TryParse(Path.GetFileName(winfx_x64_dirs[i]).Substring(1, 1), out tmp))
                        winfx_x64_dirs.RemoveAt(i);
                winfx_x64_dirs.Sort();
            }
            string clrdir = FindDefClrPath(winfx_x64_dirs, winfx_x86_dirs);
            Console.WriteLine("Default CLR runtime assumed at: " + clrdir);
            RegistryKey reg = Registry.LocalMachine.OpenSubKey(
                @"SYSTEM\CurrentControlSet\Control\Session Manager\Environment", true);
            List<string> names = new List<string>(reg.GetValueNames());
            List<string> names_nocase = new List<string>();
            names.ForEach(delegate(string name) { names_nocase.Add(name.ToUpper()); });
            if (names_nocase.Contains("NETFX") && !ConfirmForEnvVarExists("NETFX")) return;
            if (is_x64 && names_nocase.Contains("NETFXX64") && !ConfirmForEnvVarExists("NETFXX64")) return;
            if (names_nocase.Contains("NETFXX86") && !ConfirmForEnvVarExists("NETFXX86")) return;
            if (names_nocase.Contains("DOTNET") && !ConfirmForEnvVarExists("DOTNET")) return;
            if (names_nocase.Contains("CLRDIR") && !ConfirmForEnvVarExists("CLRDIR")) return;
            if (reg.GetValue("PATH", "", RegistryValueOptions.DoNotExpandEnvironmentNames)
                .ToString().ToUpper().Contains("%NETFX%") && !ConfirmForEnvVarExists("%NETFX% in PATH")) return;

            StringBuilder sb = new StringBuilder();
            for (int i = winfx_x86_dirs.Count - 1; i >= 0; i--)
            {
                if (sb.Length > 0) sb.Append(";");
                sb.Append(winfx_x86_dirs[i]);
            }
            reg.SetValue("NETFXX86", sb.ToString(), RegistryValueKind.ExpandString);
            //Environment.SetEnvironmentVariable("NETFXX86", sb.ToString(), EnvironmentVariableTarget.Machine);
            Console.WriteLine("Set: NETFXX86 = " + reg.GetValue("NETFXX86").ToString());
            string netfxx86 = reg.GetValue("NETFXX86").ToString();
            sb.Length=0;
            if (is_x64)
            {
                for (int i = winfx_x64_dirs.Count - 1; i >= 0; i--)
                {
                    if (sb.Length > 0) sb.Append(";");
                    sb.Append(winfx_x64_dirs[i]);
                }
                reg.SetValue("NETFXX64", sb.ToString(), RegistryValueKind.ExpandString);
                //Environment.SetEnvironmentVariable("NETFXX64", sb.ToString() //+ ";%NETFXX86%"
                //    + netfxx86, EnvironmentVariableTarget.Machine);
                Console.WriteLine("Set: NETFXX64 = " + reg.GetValue("NETFXX64", "",
                    RegistryValueOptions.DoNotExpandEnvironmentNames).ToString());
                reg.SetValue("NETFX", "%NETFXX64%;%NETFXX86%", RegistryValueKind.ExpandString);
                //Environment.SetEnvironmentVariable("NETFX", //"%NETFXX64%"
                //    reg.GetValue("NETFXX64").ToString(), EnvironmentVariableTarget.Machine);
            }
            else reg.SetValue("NETFX", "%NETFXX86%", RegistryValueKind.ExpandString);
                //Environment.SetEnvironmentVariable("NETFX", //"%NETFXX86%", 
                //    reg.GetValue("NETFXX86").ToString(), EnvironmentVariableTarget.Machine);
            Console.WriteLine("Set: NETFX = " + reg.GetValue("NETFX", "",
                RegistryValueOptions.DoNotExpandEnvironmentNames).ToString());
            reg.SetValue("DOTNET", "%NETFX%", RegistryValueKind.ExpandString);
            //Environment.SetEnvironmentVariable("DOTNET", "%NETFX%", EnvironmentVariableTarget.Machine);
            Console.WriteLine("Set: DOTNET = " + reg.GetValue("DOTNET", "",
                RegistryValueOptions.DoNotExpandEnvironmentNames).ToString());
            reg.SetValue("CLRDIR", clrdir, RegistryValueKind.ExpandString);
            //Environment.SetEnvironmentVariable("CLRDIR", clrdir, EnvironmentVariableTarget.Machine);
            Console.WriteLine("Set: CLRDIR = " + reg.GetValue("CLRDIR").ToString());
            string path = reg.GetValue("PATH", "", RegistryValueOptions.DoNotExpandEnvironmentNames)
                .ToString().Replace(";%NETFX%", "").Replace("%NETFX%;", "") + ";%NETFX%";
            reg.SetValue("PATH", path, RegistryValueKind.ExpandString);
            //Environment.SetEnvironmentVariable("PATH", path, EnvironmentVariableTarget.Machine);
            Console.WriteLine("PATH = " + reg.GetValue("PATH", "", 
                RegistryValueOptions.None
                ).ToString());
            Console.WriteLine("Restart Windows immediately for the changes to be applied.");
            //if (System.Diagnostics.Debugger.IsAttached)
            //{
                //Console.WriteLine("Press ENTER to exit...");
                //Console.ReadLine();
            //}
            Console.WriteLine("Press ENTER to restart Windows. Press ESC to restart manually.");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Enter)
            {
                System.Diagnostics.Process.Start("shutdown", "-r -t 0 -f");
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("Your system is currently UNSTABLE. Please reboot immediately.");
            }
        }

        static bool ConfirmForEnvVarExists(string envVar)
        {
            Console.Write("Environment variable {0} exists. Continue and overwrite? [n] ", envVar);
            bool ret = Console.ReadKey().KeyChar.ToString().ToLower() == "y";
            Console.WriteLine();
            return ret;
        }

        public class WinApi
        {
            [DllImport("kernel32.dll")]
            public static extern void GetSystemInfo(
                [MarshalAs(UnmanagedType.Struct)] 
                ref SYSTEM_INFO lpSystemInfo);

            [StructLayout(LayoutKind.Sequential)]
            public struct SYSTEM_INFO
            {
                internal _PROCESSOR_INFO_UNION uProcessorInfo;
                public uint dwPageSize;
                public IntPtr lpMinimumApplicationAddress;
                public IntPtr lpMaximumApplicationAddress;
                public IntPtr dwActiveProcessorMask;
                public uint dwNumberOfProcessors;
                public uint dwProcessorType;
                public uint dwAllocationGranularity;
                public ushort dwProcessorLevel;
                public ushort dwProcessorRevision;
            }

            [StructLayout(LayoutKind.Explicit)]
            public struct _PROCESSOR_INFO_UNION
            {
                [FieldOffset(0)]
                internal uint dwOemId;
                [FieldOffset(0)]
                internal ushort wProcessorArchitecture;
                [FieldOffset(2)]
                internal ushort wReserved;
            }

        }
        static string FindDefClrPath(params List<string>[] paths)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                if (paths[i] != null)
                {
                    string path = null; // paths[i].Find(delegate(string p)
                    for (int d=paths[i].Count - 1; d>=0; d--)
                    {
                        //string p = paths[i][d];
                        if (File.Exists(paths[i][d] + "\\mscorsvw.exe"))
                            return paths[i][d];
                    }//);
                    //if (path != null) return path;
                }
            }
            return null;
        }
    }
}
