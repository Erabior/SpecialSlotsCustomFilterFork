﻿using BepInEx;
using BepInEx.Configuration;
using System;
using SpecialSlots.Utilities;
using static SpecialSlots.Utilities.VersionChecker;

/*
 Some code borrowed from GhostFenixx's "HideSpecialIcon.dll" fix for the same function that this mod provides.
 */

namespace SpecialSlots
{
    [BepInPlugin("com.jbs4bmx.SpecialSlots", "SpecialSlots", "390.0.2")]
    public class SlotsPlugin : BaseUnityPlugin
    {
        public const int TarkovVersion = 30626;
        public static SlotsPlugin Instance { get; private set; }
        public ConfigEntry<bool> Enable { get; private set; }
        public ConfigEntry<int> FramesToWait { get; private set; }

        private void Awake()
        {
            // Verify EFT version is correct.
            if (!VersionChecker.CheckEftVersion(Logger, Info, Config))
            {
                throw new Exception("Invalid EFT Version");
            }

            SlotsPlugin.Instance = this;
            Enable = Config.Bind<bool>(
                "General",
                "Enable",
                true,
                new ConfigDescription
                (
                    "Enable or disable this service.",
                    null,
                    new ConfigurationManagerAttributes
                    {
                        Order = 2,
                    }
                )
            );
            FramesToWait = Config.Bind<int>(
                "General",
                "Frames To Wait",
                1,
                new ConfigDescription
                (
                    "Time (in seconds) to wait before the service processes.",
                    null,
                    new ConfigurationManagerAttributes
                    {
                        Order = 1,
                    }
                )
            );

            Logger.LogInfo("Special Slots Client Mod v390.0.2 is loading...");
            new SlotItemViewNewSlotItemViewPatch().Enable();
            Logger.LogInfo("Special Slots Client Mod v390.0.2 has loaded!");
        }
    }
}
