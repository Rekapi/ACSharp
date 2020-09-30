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
    public class AC
    {
        // command for Listing Entities 
        [CommandMethod("ListEnt")]
        public static void ListEnt()
        {
            // 1. get the current document 
            Document doc = Application.DocumentManager.MdiActiveDocument;

            // 2. get into the database 
            Database db = doc.Database;

            // 3. getting the editor 
            Editor ed = doc.Editor;

            // 4. getting into transaction
            using (Transaction acTrans = db.TransactionManager.StartTransaction())
            {
                // 5. getting into BlockTable (for Read) - contains all blocks in the drawing
                BlockTable bkTable = acTrans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                // 6. getting into BlockTableRecord (for Read) - contains all entities within a specific block definition
                BlockTableRecord bkTabRecord = acTrans.GetObject(bkTable[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;
                int nCnt = 0;
                ed.WriteMessage("\nModel Space Objects : ");
                // 7. iterating through the objects inside the model and display type of each object
                foreach (ObjectId acObjId in bkTabRecord)
                {
                    ed.WriteMessage("\n" + acObjId.ObjectClass.DxfName);
                    nCnt = nCnt + 1;
                }
                // if there is no objects found 
                if (nCnt == 0)
                {
                    ed.WriteMessage("\nThere is no such objects found");
                }
            }
        }
        // command returns the layer table for the current database 
        [CommandMethod("ListLay")]
        public static void List_Lay()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            using (Transaction acTranse = db.TransactionManager.StartTransaction())
            {
                // LayerTable
                LayerTable laytab = acTranse.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                // Iterate through Layer Table Record 
                int nCt = 0;
                foreach (ObjectId acObjId in laytab)
                {
                    LayerTableRecord laytabrec = acTranse.GetObject(acObjId, OpenMode.ForRead) as LayerTableRecord;
                    nCt = nCt + 1;
                    ed.WriteMessage("\nLayer " + nCt  + " : "+ laytabrec.Name);
                }
            }
        }
        [CommandMethod("MinMaxApp")]
        public static void Call()
        {
            CtrlCadAppWindow ctcad = new CtrlCadAppWindow();
            ctcad.Min_max_Window();
        }
        [CommandMethod("VVA")]
        public static void ViewPortArr()
        {
            ViewPort acVp = new ViewPort();
            acVp.Create_Mode_ViewPort();
        }
        [CommandMethod ("OpenDwg", CommandFlags.Session)]
        public static void Open_Cad_File()
        {
            DrawingControl dwgctrl = new DrawingControl();
            dwgctrl.open_cad_file();
        }
        [CommandMethod("SavingActiveDocument")]
        public static void Save_Active_Doc()
        {
            DrawingControl dwgctrl = new DrawingControl();
            dwgctrl.save_cad_file();
        }
        // poly line filter
        [CommandMethod("PolyLineCount")]
        public static void Count_Poly()
        {
            PolyLineArea Poly_count = new PolyLineArea();
            Poly_count.calc_Area();
        }
        // Text Filter 
        [CommandMethod("TextCount")]
        public static void Num_text()
        {
            TextFiltr TxtFilter = new TextFiltr();
            TxtFilter.Filt_text();
        }
    }
}
