using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBatchProcessor.AppObjects;

namespace MyBatchProcessor.TaskObjects
{
    public class PropertyUpdate : TaskObject
    {
        public String propName;
        public String propValue;

        public PropertyUpdate(String fileInput, String propertyName, String propertyValue, int appID, Message msgObj)
        {           
            propName = propertyName;
            propValue = propertyValue;
            applicationID = appID;
            msg = msgObj;
            hasValidFiles = this.processFileInput(fileInput);
        }

        public override bool isTaskMissingInputs()
        {
            if (propName != null && propName.Trim().Length > 0)
            {
                if (propValue != null && propValue.Trim().Length > 0)
                {
                    propName = propName.Trim();
                    propValue = propValue.Trim();
                    return !this.hasValidFiles;
                }
            }
            return true;
        }

        public override List<String> fileTypesForTask()
        {
            List<String> fileTypes = new List<string>();

            if (applicationID == WordXML.APP_ID)
            {
                fileTypes.Add("docx");
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
            if (appObject.updateProperty(propName, propValue))
            {
                msg.doMsg("Property updated for file.");
                return true;
            }
            else
            {
                msg.doErrorMsg("Property could not be updated.");
                return false;
            }
        }

        public override string resultHeader()
        {
            return null;
        }


    }
}
