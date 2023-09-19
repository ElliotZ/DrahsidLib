using Dalamud.Configuration;
using Dalamud.Plugin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrahsidLib;


public class Configuration : IPluginConfiguration
{
    int IPluginConfiguration.Version { get; set; }

    #region Saved configuration values
    public bool HideTooltips { get; set; } = false;
    #endregion

    private DalamudPluginInterface pluginInterface;

    public void Initialize(DalamudPluginInterface pi) {
        pluginInterface = pi;

        Save();
    }

    public void Save() {
        pluginInterface.SavePluginConfig(this);
    }
}
