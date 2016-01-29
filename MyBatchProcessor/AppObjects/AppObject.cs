using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBatchProcessor.AppObjects
{
    public abstract class AppObject
    {
        public abstract String appName();
        public abstract int appID();

        public abstract Boolean connect();
        public abstract void release();

        public abstract Boolean openDocument(FileObj fileObj);
        public abstract void closeDocument();

        public abstract Boolean updateProperty(String name, String value);
        public abstract Boolean calculateMass(FileObj fileObj);

        public static int determineAppID(int boxIndex)
        {
            if (boxIndex == 0)
                return SolidWorksDocMgr.APP_ID;
            if (boxIndex == 1)
                return WordXML.APP_ID;

            return -1;
        }
    }
}
