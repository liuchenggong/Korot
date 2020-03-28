using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;

namespace Korot
{
    class UACControl
    {
        public static bool IsProcessElevated
        {
            get
            {
                    return new WindowsPrincipal
                        (WindowsIdentity.GetCurrent()).IsInRole
                        (WindowsBuiltInRole.Administrator);
            }
        }
    }
}
