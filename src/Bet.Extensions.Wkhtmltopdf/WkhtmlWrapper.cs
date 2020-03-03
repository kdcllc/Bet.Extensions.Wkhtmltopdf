using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Bet.Extensions.Wkhtmltopdf
{
    public sealed class WkhtmlWrapper
    {
        /// <summary>
        /// Converts HTML input into PDF.
        /// </summary>
        /// <param name="switches">Switches that will be passed to wkhtmltopdf binary.</param>
        /// <param name="html">String containing HTML code that should be converted to PDF.</param>
        /// <param name="wkhtmlPath">Path to WkhtmlWrapper\.</param>
        /// <returns>PDF as byte array.</returns>
        public static byte[] Convert(string switches, string html, string? wkhtmlPath = null)
        {
            // switches:
            //     "-q"  - silent output, only errors - no progress messages
            //     " -"  - switch output to stdout
            //     "- -" - switch input to stdin and output to stdout
            switches = $"-q {switches} -";

            // generate PDF from given HTML string, not from URL
            if (!string.IsNullOrEmpty(html))
            {
                switches += " -";
                html = SpecialCharsEncode(html);
            }

            string location;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                location = GetEmbededResource("Windows", "wkhtmltopdf.exe", wkhtmlPath);
                if (string.IsNullOrEmpty(wkhtmlPath))
                {
                    location = location.Replace(".exe", string.Empty);
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                location = GetEmbededResource("Mac", "wkhtmltopdf", wkhtmlPath);
            }
            else
            {
                location = GetEmbededResource("Linux", "wkhtmltopdf", wkhtmlPath);
            }

            if (!File.Exists(location))
            {
                throw new Exception($"wkhtmltopdf not found, searched for {location}");
            }

            using var proc = new Process();
            try
            {
                proc.StartInfo = new ProcessStartInfo
                {
                    FileName = location,
                    Arguments = switches,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = true
                };

                proc.Start();
            }
            catch (Exception ex)
            {
                throw;
            }

            // generate PDF from given HTML string, not from URL
            if (!string.IsNullOrEmpty(html))
            {
                using (var sIn = proc.StandardInput)
                {
                    sIn.WriteLine(html);
                }
            }

            using var ms = new MemoryStream();
            using (var sOut = proc.StandardOutput.BaseStream)
            {
                var buffer = new byte[4096];
                int read;

                while ((read = sOut.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
            }

            var error = proc.StandardError.ReadToEnd();

            if (ms.Length == 0)
            {
                throw new Exception(error);
            }

            proc.WaitForExit();

            return ms.ToArray();
        }

        /// <summary>
        /// Encode all special chars.
        /// </summary>
        /// <param name="text">Html text.</param>
        /// <returns>Html with special chars encoded.</returns>
        private static string SpecialCharsEncode(string text)
        {
            var chars = text.ToCharArray();
            var result = new StringBuilder(text.Length + (int)(text.Length * 0.1));

            foreach (var c in chars)
            {
                var value = System.Convert.ToInt32(c);
                if (value > 127)
                {
                    result.AppendFormat("&#{0};", value);
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        private static string GetEmbededResource(string osSystem, string exeName, string? wkhtmlPath = null)
        {
            if (!string.IsNullOrEmpty(wkhtmlPath))
            {
                return Path.Combine(wkhtmlPath, osSystem, exeName);
            }

            var localBin = Path.Combine(AppContext.BaseDirectory, nameof(WkhtmlWrapper), osSystem, exeName);
            var fixedPath = localBin = $"{localBin.Replace(".exe", string.Empty)}.dr";

            if (File.Exists(fixedPath))
            {
                return fixedPath;
            }

            var dir = new DirectoryInfo(fixedPath);
            if (!dir.Exists)
            {
                dir.Create();
            }

            var assembly = typeof(WkhtmlWrapper).GetTypeInfo().Assembly;

            using (var resource = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{nameof(WkhtmlWrapper)}.{osSystem}.{exeName}"))
            using (var memoryStream = new MemoryStream())
            {
                resource.CopyTo(memoryStream);
                File.WriteAllBytes(fixedPath, memoryStream.ToArray());
            }

            return fixedPath;
        }
    }
}
