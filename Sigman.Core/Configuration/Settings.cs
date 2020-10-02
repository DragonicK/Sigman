using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections;

namespace Sigman.Core.Configuration {
    public class Settings {
        private readonly Hashtable cache = new Hashtable();

        public void ParseConfigFile(string fileName) {
            if (!File.Exists(fileName))
                throw new Exception("cannot find server configuration file");

            cache.Clear();

            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8)) {
                string[] validLines = reader.ReadToEnd().Split('\n').Where(l => !l.StartsWith("//")).ToArray();
                foreach (string line in validLines) {
                    if (line == "\r")
                        continue;

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parameters = line.Split('=');
                    cache.Add(parameters[0].Trim(), parameters[1].Trim());
                }
            }
        }

        public bool GetBoolean(string key) {
            var text = ((string)cache[key]).ToLower();
            var isNumber = false;

            foreach (var letter in text) {
                if (char.IsDigit(letter)) {
                    isNumber = true;
                    break;
                }
            }

            if (!isNumber) {
                return (text == "true");
            }

            return Convert.ToBoolean(Convert.ToInt32(cache[key]));
        }

        public byte GetByte(string key) {
            return Convert.ToByte(cache[key]);
        }

        public short GetInt16(string key) {
            return Convert.ToInt16(cache[key]);
        }

        public int GetInt32(string key) {
            return Convert.ToInt32(cache[key]);
        }

        public long GetInt64(string key) {
            return Convert.ToInt64(cache[key]);
        }

        public string GetString(string key) {
            if (cache[key] == null) return "command not found";

            return cache[key].ToString();
        }

        public void Clear() {
            cache.Clear();
        }
    }
}