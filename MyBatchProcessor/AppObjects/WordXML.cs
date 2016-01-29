using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.CustomProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.VariantTypes;

namespace MyBatchProcessor.AppObjects
{
    public class WordXML : AppObject
    {
        public static int APP_ID = 2;
        private String filePath;

        public override int appID()
        {
            return APP_ID;
        }

        public override string appName()
        {
            return "OFFICE";
        }

        #region Implement connect and release  

        public override bool connect()
        {
            return true;
        }

        public override void release()
        {
            
        }

        #endregion

        #region Implement open and close methods

        public override bool openDocument(FileObj fileObj)
        {
            filePath = fileObj.path;
            return true;
        }

        public override void closeDocument()
        {

        }

        #endregion

        #region Implement Task Methods

        public override bool updateProperty(string name, string value)
        {
            if (filePath != null)
            {
                try
                {
                    using (WordprocessingDocument doc = WordprocessingDocument.Open(filePath, true))
                    {
                        String existVal = this.existingPropertyValue(name, doc);
                        if (existVal == null || !existVal.Equals(value, StringComparison.OrdinalIgnoreCase))
                        {
                            // set the custom property to the new value
                            return (this.setCustomProperty(name, value, doc));
                        }
                    }              

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public override bool calculateMass(FileObj fileObj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Detail Property Methods

        private String existingPropertyValue(String name, WordprocessingDocument doc)
        {
            try
            {
                var customProps = doc.CustomFilePropertiesPart;
                if (customProps == null)
                {
                    return null;
                }

                var props = customProps.Properties;
                if (props != null)
                {
                    // This will trigger an exception if the property's Name 
                    // property is null, but if that happens, the property is damaged, 
                    // and probably should raise an exception.
                    var prop = props.Where(p => ((CustomDocumentProperty)p).Name.Value == name).FirstOrDefault();

                    // Does the property exist? If so, get the return value, 
                    // and then delete the property.
                    if (prop != null)
                    {
                        return prop.InnerText;
                    }
                }
            }
            catch
            {

            }
            return null;
        }

        public Boolean setCustomProperty(string propertyName, object propertyValue, WordprocessingDocument doc)
        {
            // Given a document name, a property name/value, and the property type, 
            // add a custom property to a document. The method returns if it was successful or not

            var newProp = new CustomDocumentProperty();
            // Now that you have handled the parameters, start
            // working on the document.
            newProp.FormatId = "{D5CDD505-2E9C-101B-9397-08002B2CF9AE}";
            newProp.Name = propertyName;
            newProp.VTLPWSTR = new VTLPWSTR(propertyValue.ToString());

            var customProps = doc.CustomFilePropertiesPart;
            if (customProps == null)
            {
                // No custom properties? Add the part, and the
                // collection of properties now.
                customProps = doc.AddCustomFilePropertiesPart();
                customProps.Properties = new DocumentFormat.OpenXml.CustomProperties.Properties();
            }

            var props = customProps.Properties;
            if (props != null)
            {
                // This will trigger an exception if the property's Name 
                // property is null, but if that happens, the property is damaged, 
                // and probably should raise an exception.
                var prop = props.Where(p => ((CustomDocumentProperty)p).Name.Value == propertyName).FirstOrDefault();

                // Does the property exist? If so, get the return value, 
                // and then delete the property.
                if (prop != null)
                {
                    prop.Remove();
                }

                // Append the new property, and 
                // fix up all the property ID values. 
                // The PropertyId value must start at 2.
                props.AppendChild(newProp);
                int pid = 2;
                foreach (CustomDocumentProperty item in props)
                {
                    item.PropertyId = pid++;
                }
                props.Save();
                customProps.Properties.Save();
                return true;

            }
            return false;

        }

        #endregion
    }
}
