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
