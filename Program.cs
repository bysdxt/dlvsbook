using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.IO;
//http://services.mtps.microsoft.com/ServiceAPI/catalogs/Dev14/zh-CN
namespace dlvsbook {
    class Program {
        static readonly Action<string> echo = Console.Write;
        static readonly Action<string> echoln = Console.WriteLine;
        static readonly Func<string> readln = Console.ReadLine;
        static readonly Regex catalogs = new Regex(@"<body class=""catalogs"">", RegexOptions.Compiled);
        static void Main(string[] args) {
            int index;
            string books = null;
            var urlfound = new List<string>();
            for (;;) {
                for(var i = 0; i < urlfound.Count; ++i) {
                    echo(i.ToString());
                    echo("\t");
                    echoln(urlfound[i]);
                }
                echo(Directory.GetCurrentDirectory());
                echo(">");
                var cmd = readln();
                if (int.TryParse(cmd, out index)) {
                    try {
                        cmd = urlfound[index];
                    } catch (Exception e) {
                        echo("Error:\t");
                        echoln(e.Message);
                        continue;
                    }
                }
                if (cmd[0] == '-') {
                    cmd = cmd.Substring(1);
                    switch (cmd) {
                        case "clear":
                            urlfound.Clear();
                            break;
                    }
                } else {
                    try {
                        var wr = WebRequest.Create(cmd);
                        echoln("getting...");
                        var r = wr.GetResponse();
                        echoln("done.");

                    } catch (Exception e) {
                        echo("Error:\t");
                        echoln(e.Message);
                    }
                }
            }
            //Console.Write("any key to begin");
            //Console.ReadKey(true);
            //Console.WriteLine();
            ////((Action)(async () => {
            ////    using (var client = new HttpClient()) {
            ////        var r = await client.GetAsync("http://services.mtps.microsoft.com/ServiceAPI/catalogs/Dev14/zh-CN");
            ////        var st = await r.Content.ReadAsStreamAsync();
            ////        Console.WriteLine(st.CanRead);
            ////        Console.WriteLine(st.CanSeek);
            ////        Console.WriteLine(st.Length);
            ////    }
            ////}))();
            //var c = WebRequest.CreateDefault(new Uri(
            //    "https://www.google.com.hk/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png"));
            //c.Method = "HEAD";
            //c.Timeout = 5000;
            //var r = c.GetResponse();
            //Console.WriteLine(r.ContentLength);
            //Console.WriteLine("any key to exit");
            //r.Close();
            //Console.ReadKey(true);
        }
    }
}
