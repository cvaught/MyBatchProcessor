using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBatchProcessor.AppObjects;

namespace MyBatchProcessor.TaskObjects
{
    public class MassOutput : TaskObject
    {

        public MassOutput(String fileInput, Message msgObj)
        {
            applicationID = SolidWorksDocMgr.APP_ID;
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
            fileTypes.Add("prt");
            fileTypes.Add("sldprt");
            fileTypes.Add("asm");
            fileTypes.Add("sldasm");
            return fileTypes;
        }

        public override Boolean performTask(FileObj fileObj)
        {
            if (appObject.calculateMass(fileObj))
            {
                String message = "Mass calculated for file";
                if (fileObj.resultCode == -2)
                    message += " but one or more configurations failed. Update SW performance settings to update mass properties while saving document";

                msg.doMsg(message + ".");
                return true;
            }
            else
            {
                String message = "Mass could not be obtained";
                if (fileObj.resultCode == -2)
                    message += " due to missing mass property. Update SW performance settings to update mass properties while saving document";

                msg.doErrorMsg(message + ".");
                return false;
            }
        }

        public override string resultHeader()
        {
            String result = Environment.NewLine + Message.DIVIDER + Environment.NewLine + "RESULT SUMMARY" + Environment.NewLine + Message.DIVIDER + Environment.NewLine;
            result += Environment.NewLine + "Filename, Config, Value";
            return result;
        }


    }
}
