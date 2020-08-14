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
    }
}
