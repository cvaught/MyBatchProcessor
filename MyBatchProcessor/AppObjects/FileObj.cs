using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyBatchProcessor.AppObjects
{
    public class FileObj
    {
        public String path;
        public String filename;
        public String filenameNoExt;
        public String ext;

        public Dictionary<String, Double> massResult; // key is config name and value is mass value

        public int resultCode;

        public FileObj(String filePath)
        {
            path = filePath;
            filename = Path.GetFileName(filePath);
            filenameNoExt = Path.GetFileNameWithoutExtension(filePath);
            ext = Path.GetExtension(filePath);
        }

        public string determineTextResult()
        {
            String result = null;
            if (massResult != null && massResult.Keys.Count > 0)
            {
                result = "";     
                foreach (String configName in massResult.Keys)
                {
                    double value;
                    if (massResult.TryGetValue(configName, out value))
                    {
                        String valStr = value.ToString();
                        if (value == -2)
                            valStr = "No Data";
                        result += this.filename + ", " + configName + ", " + valStr + Environment.NewLine;
                    }       
                }
            }

            return result;
        }
    }
}
