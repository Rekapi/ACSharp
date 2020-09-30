using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

namespace BRepo
{
    // Calculate polyline area 
    class PolyLineArea
    {
        public void calc_Area()
        {
            // get current database 
            //Document acDoc = Application.DocumentManager.MdiActiveDocument;
            //Database acDb = acDoc.Database;
            Editor acEd = Application.DocumentManager.MdiActiveDocument.Editor;
            // select only polyline 
            TypedValue[] acTypValAr = new TypedValue[4];
            acTypValAr.SetValue(new TypedValue((int) DxfCode.Operator, "<or"), 0);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "LWPOLYLINE"), 1);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "LINE"), 2);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Operator, "or>"), 3);
            // Assign the filter criteria to selection filter object 
            SelectionFilter acSelFilter = new SelectionFilter(acTypValAr);

            // request for objects to be selectd in drawing area 
            PromptSelectionResult acSSPrompt;
            acSSPrompt = acEd.GetSelection(acSelFilter);
            if (acSSPrompt.Status == PromptStatus.OK)
            {
                SelectionSet acSSet = acSSPrompt.Value;
                Application.ShowAlertDialog("Number of objects selected  :" + acSSet.Count.ToString());
            }
            else
            {
                Application.ShowAlertDialog("Number of objects selected  :  0");
            }
        }
    }
}
