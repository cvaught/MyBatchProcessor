using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MyBatchProcessor.AppObjects;

namespace MyBatchProcessor.TaskObjects
{
    public abstract class TaskObject
    {
        public List<FileObj> fileObjList;
        public Boolean hasValidFiles;
        public int applicationID;
        public AppObject appObject;
        public Boolean canceled;
        public Message msg;

        public abstract Boolean isTaskMissingInputs();
        public abstract List<String> fileTypesForTask();
        public abstract String resultHeader();

        public abstract Boolean performTask(FileObj fileObj);

        #region Determine Task Selected

        public static Boolean isMassOutputTaskSelected(int tabIndex)
        {
            return (tabIndex == 1);
        }

        public static Boolean isPropertyUpdateTaskSelected(int tabIndex)
        {
            return (tabIndex == 0);
        }

        public static Boolean isClearPropertyTaskSelected(int tabIndex)
        {
            return (tabIndex == 2);
        }

        #endregion

        #region Process File Input

        public Boolean processFileInput(String pathInput)
        {
            fileObjList = new List<FileObj>();
            List<String> filePathList = new List<String>();

            if (pathInput == null || pathInput.Trim().Length == 0)
                return false;

            List<String> fileTypes = this.fileTypesForTask();

            String[] paths = pathInput.Split(new string[] { Constants.DELIMETER }, StringSplitOptions.None);
            foreach (String path in paths)
            {
                if (File.Exists(path) && isFileOfFileType(path, fileTypes))
                {

                    // this is a single part file
                    if (filePathList.Contains(path) == false)
                        fileObjList.Add(new FileObj(path));
                }
                else if (Directory.Exists(path))
                {
                    // this is a directory
                    List<String> files = filesInFolder(path, fileTypes, true);
                    foreach (String file in files)
                    {
                        if (filePathList.Contains(file) == false)
                            fileObjList.Add(new FileObj(file));
                    }
                }
            }

            return (fileObjList.Count > 0);
        }

        public static List<string> filesInFolder(string folderPath, List<string> fileTypes, bool includeSubFolders)
        {
            List<String> fileList = new List<String>();

            //get all of the files in this folder for the file type
            string[] files = allFilesInFolder(folderPath);
            //add each file to the master file list
            foreach (String file in files)
            {
                if (isFileOfFileType(file, fileTypes))
                    fileList.Add(file);
            }
            if (includeSubFolders)
            {
                //add the files from any  subfolders
                fileList.AddRange(getAllFilesInSubfolders(folderPath, fileTypes, true));
            }
            return fileList;
        }

        public static string[] allFilesInFolder(string folder)
        {
            string[] files = new string[0];
            if (Directory.Exists(folder) == true)
            {
                try
                {
                    files = System.IO.Directory.GetFiles(folder);
                }
                catch
                {
                }
            }
            return files;
        }

        public static List<string> getAllFilesInSubfolders(String folderPath, List<String> fileTypes, Boolean ignoreRevisionFolders)
        {
            List<string> result = new List<string>();
            if (Directory.Exists(folderPath) == true)
            {
                string[] folders = System.IO.Directory.GetDirectories(folderPath);

                foreach (String folder in folders)
                {
                    if (folder.Substring(folder.Length - 11).ToLower().Equals("oldversions") || folder.Substring(folder.Length - 10).ToLower().Equals("oldversion"))
                    {
                        //ignore this subfolder
                    }
                    else
                    {
                        //get each file in folder
                        string[] files = allFilesInFolder(folder);
                        //add each file to the master file list
                        foreach (String file in files)
                        {
                            if (isFileOfFileType(file, fileTypes))
                                result.Add(file);
                        }
                        //add the files from any  subfolders
                        result.AddRange(getAllFilesInSubfolders(folder, fileTypes, false));
                    }
                }
            }

            return result;
        }

        public static Boolean isFileOfFileType(String filePath, List<String> fileType)
        {
            foreach (String type in fileType)
            {
                if (ignoreFile(filePath))
                {
                    return false;
                }
                else
                {
                    if (isFileOfFileType(filePath, type) == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static Boolean isFileOfFileType(String filePath, String fileType)
        {
            if (ignoreFile(filePath))
            {
                return false;
            }
            if (fileType == null)
                return true;

            if (Path.GetExtension(filePath).Equals("." + fileType, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public static Boolean ignoreFile(String filePath)
        {
            if (Path.GetFileNameWithoutExtension(filePath).StartsWith("~") || Path.GetFileNameWithoutExtension(filePath).StartsWith("."))
            {
                return true;
            }
            return false;
        }

        #endregion

        public int start()
        {
            // create the application object
            if (applicationID == SolidWorksDocMgr.APP_ID)
            {
                appObject = new SolidWorksDocMgr();
            }
            else if (applicationID == WordXML.APP_ID)
            {
                appObject = new WordXML();
            }

            // attempt to connect to the application
            if (!appObject.connect())
            {
                return Constants.RESULT_NO_CONNECTION;
            }
            else
            {
                int result = Constants.RESULT_SUCCESS;

                Boolean hasSuccess = false;
                Boolean hasFailure = false;
                String resultText = "";

                foreach (FileObj fileObj in this.fileObjList)
                {
                    msg.doMsg("Processing " + fileObj.filename + "...");
                    // open the document
                    if (appObject.openDocument(fileObj))
                    {
                        // perform the task
                        if (this.performTask(fileObj))
                        {
                            hasSuccess = true;
                            String resultString = fileObj.determineTextResult();
                            if (resultString != null)
                            {
                                resultText += resultString;
                            }
                        }
                        else
                        {
                            hasFailure = true;
                        }
                    }
                    else
                    {
                        msg.doErrorMsg("File could not be opened.");
                        hasFailure = true;
                    }
                    appObject.closeDocument();
                }

                if (resultText.Length > 0)
                {
                    msg.doDivider();
                    msg.doNoTimeMsg(this.resultHeader());
                    msg.doNoTimeMsg(resultText);
                    msg.doDivider();
                }


                if (hasSuccess && hasFailure)
                {
                    result = Constants.RESULT_PARTIAL_FAILURE;
                }
                else if (hasFailure)
                {
                    result = Constants.RESULT_FAILED;
                }

                appObject.release();
                return result;
            }
        }
    }
}
