/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/
using System.Security.Principal;

namespace Korot
{
    internal class UACControl
    {
        public static bool IsProcessElevated => new WindowsPrincipal
                        (WindowsIdentity.GetCurrent()).IsInRole
                        (WindowsBuiltInRole.Administrator);
    }
}