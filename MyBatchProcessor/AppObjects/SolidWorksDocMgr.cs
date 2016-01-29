using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwDocumentMgr;
using System.IO;

namespace MyBatchProcessor.AppObjects
{
    public class SolidWorksDocMgr : AppObject
    {
        public static int APP_ID = 1;
        private SwDMApplication app;
        private SwDMDocument10 doc;
        private SwDmDocumentType swType;

        public override string appName()
        {
            return "SOLIDWORKS";
        }
        public override int appID()
        {
            return APP_ID;
        }

        #region Implement connect and release

        public override Boolean connect()
        {
            try
            {
                // replace licenseKey with your license key
                SwDMClassFactory swClassFact = default(SwDMClassFactory);
                String licenseKey = "<Your License Key>";

                swClassFact = new SwDMClassFactory();
                app = (SwDMApplication)swClassFact.GetApplication(licenseKey);

            }
            catch
            {
                app = null;
            }
            return (app != null);
        }

        public override void release()
        {
            if (app != null)
            {
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(app);
                app = null;
            }
        }

        #endregion

        #region Implement open and close methods

        public override bool openDocument(FileObj file)
        {
            try
            {
                SwDmDocumentOpenError oError;
                swType = docTypeForExtension(file.ext);
                if (swType != SwDmDocumentType.swDmDocumentUnknown)
                {
                    doc = (SwDMDocument10)app.GetDocument(file.path, swType, false, out oError);
                    if (doc != null && 
                        (int)oError == (int)SwDmDocumentOpenError.swDmDocumentOpenErrorNone)
                        return true;
                }
            }
            catch {}

            doc = null;
            return false;
        }

        public override void closeDocument()
        {
            if (doc != null)
            {
                doc.CloseDoc();
                doc = null;
            }
        }

        private SwDmDocumentType docTypeForExtension(String ext)
        {
            if (ext == null)
                return SwDmDocumentType.swDmDocumentUnknown;

            ext = ext.ToLower();

            if (ext.EndsWith("prt"))
            {
                return SwDmDocumentType.swDmDocumentPart;
            }
            else if (ext.EndsWith("asm"))
            {
                return SwDmDocumentType.swDmDocumentAssembly;
            }
            else if (ext.EndsWith("drw"))
            {
                return SwDmDocumentType.swDmDocumentDrawing;
            }

            return SwDmDocumentType.swDmDocumentUnknown;
        }

        #endregion

        #region Implement Task Methods

        public override bool updateProperty(string name, string value)
        {
            if (doc != null)
            {
                try
                {
                    String existVal = this.existingPropertyValue(name);
                    if (existVal == null)
                    {
                        // add the new custom property
                        doc.AddCustomProperty(name, SwDmCustomInfoType.swDmCustomInfoText, value);
                        doc.Save();
                    }
                    else if (!existVal.Equals(value, StringComparison.OrdinalIgnoreCase))
                    {
                        // set the custom property to the new value
                        doc.SetCustomProperty(name, value);
                        doc.Save();
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

        public override bool clearProperty()
        {
            Boolean result = false;
            if (doc != null)
            {
                try
                {
                    Array docNames = doc.GetCustomPropertyNames();
                    Boolean didClear = this.clearProperty(docNames, null);

                    SwDMConfigurationMgr configMgr = doc.ConfigurationManager;
                    if (configMgr != null && configMgr.GetConfigurationCount() > 0)
                    {
                        Array array = configMgr.GetConfigurationNames();
                        foreach (object obj in array)
                        {
                            try
                            {
                                SwDMConfiguration14 cfg = (SwDMConfiguration14)configMgr.GetConfigurationByName((string)obj);
                                String configuration = cfg.Name;
                                Array names = cfg.GetCustomPropertyNames();
                                Boolean clearResult = this.clearProperty(names, cfg);
                                if (clearResult)
                                    didClear = true;
                            }
                            catch { }
                        }
                    }

                    if (didClear)
                        doc.Save();
                    result = true;
                }
                catch { }
            }
            return result;
        }

        private Boolean clearProperty(Array names, SwDMConfiguration14 cfg)
        {
            Boolean result = false;
            if (names != null && names.Length > 0)
            {
                foreach (String name in names)
                {
                    if (cfg == null)
                    {
                        doc.DeleteCustomProperty(name);
                    }
                    else if (cfg.DeleteCustomProperty(name))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public override Boolean calculateMass(FileObj currentFile)
        {
            Boolean result = false;
            if (doc != null && currentFile != null)
            {
                try
                {
                    currentFile.massResult = new Dictionary<string, double>();
                    SwDMConfigurationMgr configMgr = doc.ConfigurationManager;
                    if (configMgr != null && configMgr.GetConfigurationCount() > 0)
                    {
                        Array array = configMgr.GetConfigurationNames();
                        foreach (object obj in array)
                        {
                            try
                            {
                                SwDMConfiguration14 cfg = (SwDMConfiguration14)configMgr.GetConfigurationByName((string)obj);
                                String configuration = cfg.Name;

                                if (!configuration.EndsWith("FLAT-PATTERN", StringComparison.OrdinalIgnoreCase))
                                {
                                    Double resultValue = this.handleMassProperty(cfg);
                                    if (resultValue > 0)
                                    {
                                        currentFile.massResult.Add(configuration, resultValue);
                                        result = true;
                                    }
                                    else if (resultValue == -2)
                                    {
                                        currentFile.resultCode = -2;
                                        currentFile.massResult.Add(configuration, resultValue);
                                    }
                                }
                            }
                            catch { }
                        }
                    }                  
                }
                catch { }           
            }
            return result;
        }

        #endregion

        #region Detail Property Methods

        private String existingPropertyValue(String name)
        {
            SwDmCustomInfoType type;
            try
            {
                String value = doc.GetCustomProperty(name, out type);
                return value;
            }
            catch
            {

            }
            return null;
        }

        #endregion

        #region Detail Mass Methods

        private Double handleMassProperty(SwDMConfiguration14 swCfg)
        {
            double massKg = massInKg(swCfg);
            if (massKg > 0)
            {
                return massKg * 2.20462262; // convert mass from kg to lb
            }
            return massKg;
        }

        private static double massInKg(SwDMConfiguration14 swCfg)
        {
            SwDmMassPropError nError;
            if (swCfg != null)
            {
                try
                {
                    double[] massArray = swCfg.GetMassProperties(out nError);
                    if (nError == SwDmMassPropError.swDmMassPropErrorNone)
                    {
                        return massArray[5]; //this results in the mass in kg
                    }
                    else if (nError == SwDmMassPropError.swDmMassPropErrorNoData)
                    {
                        return -2;
                    }
                }
                catch { }
            }
            return -1;
        }

        #endregion
    }
}
