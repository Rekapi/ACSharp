using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace BRepo
{
    class ViewPort
    {
        // Create a new tiled viewport configuration with two windows
      public void Create_Mode_ViewPort()
        {
            // get the current database 
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            using (Transaction acTrans = db.TransactionManager.StartTransaction())
            {
                // open the viewport table for read 
                ViewportTable vtab = acTrans.GetObject(db.ViewportTableId, OpenMode.ForRead) as ViewportTable;
                
                // check to see if the named view 'Test Viewport' exists 
                if (vtab.Has("Test_ViewPort") == false)
                {
                    // open the view table for write 
                    vtab.UpgradeOpen();

                    // add the new viewport to the viewport table and the transaction
                    ViewportTableRecord vptableRecordLwr = new ViewportTableRecord();
                    vtab.Add(vptableRecordLwr);
                    acTrans.AddNewlyCreatedDBObject(vptableRecordLwr, true);

                    // name the new viewport 'Test_ViewPort' and assign it to be the lower half of the drawing window
                    vptableRecordLwr.Name = "Test_ViewPort";
                    vptableRecordLwr.LowerLeftCorner = new Point2d(0, 0);
                    vptableRecordLwr.UpperRightCorner = new Point2d(1, 0.5);

                    // Add the new viewport to the viewport table and the transaction
                    ViewportTableRecord vptableRecordUpr = new ViewportTableRecord();
                    vtab.Add(vptableRecordUpr);
                    acTrans.AddNewlyCreatedDBObject(vptableRecordUpr, true);

                    // name the new viewport 'Test_ViewPort' and assign it to be the upper half of the drawing window
                    vptableRecordUpr.Name = "Test_ViewPort";
                    vptableRecordUpr.LowerLeftCorner = new Point2d(0, .5);
                    vptableRecordUpr.UpperRightCorner = new Point2d(1, 1);

                    // to assign the new viewport as the active viewpport, 
                    // the viewport named active nedd to be removed and recreated based on "Test_ViewPort"

                    // step through each object in the symbol table 
                    foreach (ObjectId acObjectId in vtab)
                    {
                        // open the object for read 
                        ViewportTableRecord acVportTblRec = acTrans.GetObject(acObjectId, OpenMode.ForRead) as ViewportTableRecord;

                        // see if it is one of the active viewports, and if so erase it 
                        if (acVportTblRec.Name == "*Active")
                        {
                            acVportTblRec.UpgradeOpen();
                            acVportTblRec.Erase();
                        }
                    }
                    // clone the new viewport as the active viewports 
                    foreach (ObjectId acObjId in vtab)
                    {
                        // open the object for read 
                        ViewportTableRecord acVportTblRec = acTrans.GetObject(acObjId, OpenMode.ForRead) as ViewportTableRecord;

                        // see if it is one of the active viewports, and if so erase it 
                        if (acVportTblRec.Name == "*Test_ViewPort")
                        {
                            ViewTableRecord acVportTblRecClone = acVportTblRec.Clone() as ViewTableRecord;
                            // add the new viewport to the viewport table and the transaction
                            vtab.Add(acVportTblRecClone);
                            acVportTblRecClone.Name = "*Active";
                            acTrans.AddNewlyCreatedDBObject(acVportTblRecClone, true);
                        }
                    }
                    // update display with the new tiled viewports arrangment 
                    Editor ed = doc.Editor;
                    ed.UpdateTiledViewportsFromDatabase();
                    // commit changes 
                    acTrans.Commit();
                }
            }
        }  
    }
}
