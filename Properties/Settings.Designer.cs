// Decompiled with JetBrains decompiler
// Type: Korot.Properties.Settings
// Assembly: Korot, Version=0.0.0.4, Culture=neutral, PublicKeyToken=null
// MVID: B199FBCD-1DEE-453C-8CBB-C9992752BD7C
// Assembly location: C:\Users\haltroy\arşiv\Mağara devri haltroy\Korot\Korot.exe

using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Korot.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.7.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings defaultInstance = Settings.defaultInstance;
        return defaultInstance;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("about:blank")]
    public string homepage
    {
      get
      {
        return (string) this[nameof (homepage)];
      }
      set
      {
        this[nameof (homepage)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" />")]
    public StringCollection History
    {
      get
      {
        return (StringCollection) this[nameof (History)];
      }
      set
      {
        this[nameof (History)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    public bool CheckForUpdates
    {
      get
      {
        return (bool) this[nameof (CheckForUpdates)];
      }
      set
      {
        this[nameof (CheckForUpdates)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    public bool Notify
    {
      get
      {
        return (bool) this[nameof (Notify)];
      }
      set
      {
        this[nameof (Notify)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("0")]
    public int WindowSizeH
    {
      get
      {
        return (int) this[nameof (WindowSizeH)];
      }
      set
      {
        this[nameof (WindowSizeH)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("0")]
    public int WindowSizeW
    {
      get
      {
        return (int) this[nameof (WindowSizeW)];
      }
      set
      {
        this[nameof (WindowSizeW)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("0")]
    public int WindowPosX
    {
      get
      {
        return (int) this[nameof (WindowPosX)];
      }
      set
      {
        this[nameof (WindowPosX)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("25")]
    public int WindowPosY
    {
      get
      {
        return (int) this[nameof (WindowPosY)];
      }
      set
      {
        this[nameof (WindowPosY)] = (object) value;
      }
    }
  }
}
