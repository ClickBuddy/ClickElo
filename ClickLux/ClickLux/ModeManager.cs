using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Utils;
using ClickLux.Modes;
using ClickLux.Spell;

namespace ClickLux
{
    public static class ModeManager
    {
        private static List<ModeBase> Modes { get; set; }
        public static LogicBase QLogic { get; set; }
        public static LogicBase WLogic { get; set; }
        public static LogicBase ELogic { get; set; }
        public static LogicBase RLogic { get; set; }

        static ModeManager()
        {
            // Initialize properties
            Modes = new List<ModeBase>();

            {
                QLogic = new QLogic();
                WLogic = new WLogic();
                ELogic = new ELogic();
                RLogic = new RLogic();
            }

            Modes.AddRange(new ModeBase[]
            {
                new PermaActive(),
                new Combo(),
                new Harass(),
                new LaneClear(),
                new JungleClear(),
                new LastHit(),
                new Flee()
            });

            
            Game.OnTick += OnTick;
        }

        public static void Initialize()
        {
            
        }

        private static void OnTick(EventArgs args)
        {
            // Execute all modes
            Modes.ForEach(mode =>
            {
                try
                {
                    // Precheck if the mode should be executed
                    if (mode.ShouldBeExecuted())
                    {
                        // Execute the mode
                        mode.Execute();
                    }
                }
                catch (Exception e)
                {
                    // Please enable the debug window to see and solve the exceptions that might occur!
                    Logger.Log(LogLevel.Error, "Error executing mode '{0}'\n{1}", mode.GetType().Name, e);
                }
            });
        }
    }
}
