// Decompiled with JetBrains decompiler
// Type: Korot.Properties.Resources
// Assembly: Korot, Version=0.0.0.4, Culture=neutral, PublicKeyToken=null
// MVID: B199FBCD-1DEE-453C-8CBB-C9992752BD7C
// Assembly location: C:\Users\haltroy\arşiv\Mağara devri haltroy\Korot\Korot.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Korot.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (Korot.Properties.Resources.resourceMan == null)
          Korot.Properties.Resources.resourceMan = new ResourceManager("Korot.Properties.Resources", typeof (Korot.Properties.Resources).Assembly);
        return Korot.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return Korot.Properties.Resources.resourceCulture;
      }
      set
      {
        Korot.Properties.Resources.resourceCulture = value;
      }
    }

    internal static Bitmap arrow2_left_512
    {
      get
      {
        return (Bitmap) Korot.Properties.Resources.ResourceManager.GetObject("arrow2-left-512", Korot.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap arrow2_right_512
    {
      get
      {
        return (Bitmap) Korot.Properties.Resources.ResourceManager.GetObject("arrow2-right-512", Korot.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap Başlıksız_1
    {
      get
      {
        return (Bitmap) Korot.Properties.Resources.ResourceManager.GetObject("Başlıksız-1", Korot.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap Cancel_512
    {
      get
      {
        return (Bitmap) Korot.Properties.Resources.ResourceManager.GetObject("Cancel-512", Korot.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap go
    {
      get
      {
        return (Bitmap) Korot.Properties.Resources.ResourceManager.GetObject(nameof (go), Korot.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap home
    {
      get
      {
        return (Bitmap) Korot.Properties.Resources.ResourceManager.GetObject(nameof (home), Korot.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap refresh
    {
      get
      {
        return (Bitmap) Korot.Properties.Resources.ResourceManager.GetObject(nameof (refresh), Korot.Properties.Resources.resourceCulture);
      }
    }
  }
}
