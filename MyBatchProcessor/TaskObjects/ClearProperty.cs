using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBatchProcessor.AppObjects;

namespace MyBatchProcessor.TaskObjects
{
    public class ClearProperty : TaskObject
    {
        public ClearProperty(String fileInput, int appID, Message msgObj)
        {           
            applicationID = appID;
            msg = msgObj;
            hasValidFiles = this.processFileInput(fileInput);
        }

        public override bool isTaskMissingInputs()
        {
            return !this.hasValidFiles;
        }

        public override List<String> fileTypesForTask()
        {
            List<String> fileTypes = new List<string>();

            if (applicationID == WordXML.APP_ID)
            {
                // word files not currenlty supported
                //fileTypes.Add("docx");
            }
            else if (applicationID == SolidWorksDocMgr.APP_ID)
            {
                fileTypes.Add("prt");
                fileTypes.Add("sldprt");
                fileTypes.Add("asm");
                fileTypes.Add("sldasm");
                fileTypes.Add("drw");
                fileTypes.Add("slddrw");
            }            
            
            return fileTypes;
        }

        public override Boolean performTask(FileObj fileObj)
        {
            if (appObject.clearProperty())
            {
                msg.doMsg("Properties cleared for file.");
                return true;
            }
            else
            {
                msg.doErrorMsg("Property could not be cleared.");
                return false;
            }
        }

        public override string resultHeader()
        {
            return null;
        }


    }
}
