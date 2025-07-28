using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Samsung_Smart_Parser
{
    public class IniFile
    {
        private readonly string path;

        public IniFile(string iniPath)
        {
            path = iniPath;
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
            string section, string key, string defaultValue,
            StringBuilder returnValue, int size, string filePath);

        public void Write(string key, string value, string section)
        {
            WritePrivateProfileString(section, key, value, path);
        }

        public string Read(string key, string section)
        {
            var returnValue = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", returnValue, 255, path);
            return returnValue.ToString();
        }
    }
}



