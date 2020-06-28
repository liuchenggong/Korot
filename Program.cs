// Decompiled with JetBrains decompiler
// Type: Korot.Program
// Assembly: Korot, Version=0.0.0.4, Culture=neutral, PublicKeyToken=null
// MVID: B199FBCD-1DEE-453C-8CBB-C9992752BD7C
// Assembly location: C:\Users\haltroy\arşiv\Mağara devri haltroy\Korot\Korot.exe

using System;
using System.Windows.Forms;

namespace Korot
{
  internal static class Program
  {
    [STAThread]
    private static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new frmMain(false, args));
    }
  }
}
