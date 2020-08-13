using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using System.Drawing;
using System.Windows;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;

namespace BRepo
{
    class CtrlCadAppWindow
    {
     // command for minimize and maximize the window
     public void Min_max_Window()
        {
            // Minimize the application window
            Application.MainWindow.WindowState = Window.State.Minimized;
            System.Windows.Forms.MessageBox.Show("Minimized", "MinMax", System.Windows.Forms.MessageBoxButtons.OK,
                                                                               System.Windows.Forms.MessageBoxIcon.None,
                                                                               System.Windows.Forms.MessageBoxDefaultButton.Button1,
                                                                               System.Windows.Forms.MessageBoxOptions.ServiceNotification);
            // Maximize the application window 
            Application.MainWindow.WindowState = Window.State.Maximized;
            System.Windows.Forms.MessageBox.Show("Minimized", "MinMax");
        }
    }
}
