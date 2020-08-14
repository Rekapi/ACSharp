using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace BRepo
{
    // create, open, save and close drawings 
    class DrawingControl
    {
        // open an existing drawing 
        public void open_cad_file()
        {
            String strFileName = "D:\\ACSharp\\ACSharp\\BRepo\\TestingFile.dwg";
            DocumentCollection docCol = Application.DocumentManager;

            if (File.Exists(strFileName)){
                docCol.Open(strFileName, false);
            }
            else
            {
                docCol.MdiActiveDocument.Editor.WriteMessage("File " + strFileName + " does not exit.");
            }
        }
        
        // This example saves tthe active drawing to "c:\MyDrawing.dwg" if it is currently not saved under its current name. 
        public void save_cad_file()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            String strDwgName = doc.Name;

            object obj = Application.GetSystemVariable("DWGTITLED");

            // check to see if the drawing has been named 
            if(System.Convert.ToInt16(obj) == 0)
            {
                // if the drawing is using a default name (Drawing1, Drawing2, etc)
                // then provide a new name 
                strDwgName = "c:\\MyDrawing.dwg";
            }
            // save the active drawing 
            doc.Database.SaveAs(strDwgName, true, DwgVersion.Current, doc.Database.SecurityParameters);
        }
    }
}
