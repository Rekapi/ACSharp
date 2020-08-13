using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;
// autocad references 
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using AutoCAD;

namespace BRepo
{
    class AutoMate
    {
        // command automating our command (launching dll files automatic throught autocad)
        [CommandMethod("AutoMateCad")]
        public static void Auto_Mate()
        {
            AcadApplication acAppObj = null;
            const String strProgId = "AutoCAD.Application.22.0";
            // get a running instance of autocad
            try
            {
                acAppObj = (AcadApplication)Marshal.GetActiveObject(strProgId);
            }
            catch  // an error occurs if no instance is running 
            {
                try
                {
                    // create a new instance of autocad 
                    acAppObj = (AcadApplication)Activator.CreateInstance(Type.GetTypeFromProgID(strProgId), true);
                }
                catch 
                {
                    // if an instandce is not created then message and exit 
                    System.Windows.Forms.MessageBox.Show("Instance of 'AutoCAD.Application' could not be created");
                    return;
                }
            }
            // display the application and return the name and the version 
            acAppObj.Visible = true;
            System.Windows.Forms.MessageBox.Show("Now Running " + acAppObj.Name + " Version " + acAppObj.Version);

            // get the active document 
            AcadDocument acDoc = acAppObj.ActiveDocument;

            // optionally, load your assembly and start command 
            
        }
    }
}
