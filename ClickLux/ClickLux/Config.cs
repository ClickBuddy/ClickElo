using System;
using System.Media;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ClickLux.Modes;
using SharpDX;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace ClickLux
{
    public static class Config
    {
        private const string MenuName = "Lux";

        private static readonly Menu Menu;

        public static Vector3 singularityPosition = Vector3.Zero;


        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("ClickLux");
            Menu.AddLabel("Special Addon.");

            Modes.Initialize();

            Obj_AI_Base.OnProcessSpellCast +=ObjAiBaseOnOnProcessSpellCast;
            Gapcloser.OnGapcloser += GapcloserOnOnGapcloser;

        }

        private static void GapcloserOnOnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            if (sender.IsAlly || args.End.Distance(Player.Instance.Position) >= 50) return;

            if (Modes.Misc.AntiGapQ && SpellManager.Q.IsReady())
                ModeManager.QLogic.HitChanceCast(SpellManager.Q, sender);
            if (Modes.Misc.AntiGapW && SpellManager.W.IsReady())
                SpellManager.W.Cast(args.End);
            if (Modes.Misc.AntiGapE && SpellManager.E.IsReady())
                ModeManager.ELogic.HitChanceCast(SpellManager.E, sender);
        }

        private static void ObjAiBaseOnOnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if(sender.IsEnemy) return;

            if (args.SData.Name.ToLower() == "luxlightstrikekugel")
            {
                singularityPosition = args.End;
                Core.DelayAction(() => singularityPosition = Vector3.Zero, 5001);
            }

            if (Modes.Misc.IMAFIRINGMALAZER && args.SData.Name.ToLower() == "luxmalicecannon")
            {
                
            }
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu Menu;

            static Modes()
            {
                Menu = Config.Menu.AddSubMenu("Drawing");
                Draw.Initialize();

                Menu = Config.Menu.AddSubMenu("Modes");

                Combo.Initialize();
                Menu.AddSeparator();

                Harass.Initialize();
                Menu.AddSeparator();

                LaneClear.Initialize();
                Menu.AddSeparator();

                JungleClear.Initialize();
                Menu.AddSeparator();

                Menu = Config.Menu.AddSubMenu("Miscellanea");

                Misc.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class Draw
            {
                private static readonly CheckBox _drawQ;
                private static readonly CheckBox _drawW;
                private static readonly CheckBox _drawE;
                private static readonly CheckBox _drawR;

                public static bool UseQ
                {
                    get { return _drawQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _drawW.CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _drawE.CurrentValue; }
                }
                public static bool UseR
                {
                    get { return _drawR.CurrentValue; }
                }

                static Draw()
                {
                    Menu.AddGroupLabel("Draw");
                    _drawQ = Menu.Add("draw.Q", new CheckBox("Draw Q"));
                    _drawW = Menu.Add("draw.W", new CheckBox("Draw W"));
                    _drawE = Menu.Add("draw.E", new CheckBox("Draw E"));
                    _drawR = Menu.Add("draw.R", new CheckBox("Draw R"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useR;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }
                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                static Combo()
                {
                    Menu.AddGroupLabel("Combo");
                    _useQ = Menu.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("comboUseW", new CheckBox("Use W"));
                    _useE = Menu.Add("comboUseE", new CheckBox("Use E"));
                    _useR = Menu.Add("comboUseR", new CheckBox("Use R"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly KeyBind _autoHarass;
                private static readonly Slider _manaPercent;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }
                public static bool AutoHarass
                {
                    get { return _autoHarass.CurrentValue; }
                }
                public static bool Mana
                {
                    get { return  Player.Instance.ManaPercent > _manaPercent.CurrentValue; }
                }

                static Harass()
                {
                    Menu.AddGroupLabel("Harass");
                    _useQ = Menu.Add("harass.q", new CheckBox("Use Q"));
                    _useW = Menu.Add("harass.w", new CheckBox("Use W"));
                    _useE = Menu.Add("harass.e", new CheckBox("Use E"));
                    _autoHarass = Menu.Add("harass.auto", new KeyBind("Auto Harass", false, KeyBind.BindTypes.PressToggle));
                    _manaPercent = Menu.Add("harass.mana", new Slider("Min Mana %", 45, 0, 100));
                }

                public static void Initialize()
                {
                }
            }

            public static class LaneClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly Slider _manaPercent;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }
                public static bool Mana
                {
                    get { return Player.Instance.ManaPercent > _manaPercent.CurrentValue; }
                }

                static LaneClear()
                {
                    Menu.AddGroupLabel("Lane Clear");
                    _useQ = Menu.Add("laneclear.q", new CheckBox("Use Q"));
                    _useW = Menu.Add("laneclear.w", new CheckBox("Use W"));
                    _useE = Menu.Add("laneclear.e", new CheckBox("Use E"));
                    _manaPercent = Menu.Add("laneclear.mana", new Slider("Min Mana %", 45, 0, 100));
                }

                public static void Initialize()
                {
                }
            }

            public static class JungleClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly Slider _manaPercent;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }
                public static bool Mana
                {
                    get { return Player.Instance.ManaPercent > _manaPercent.CurrentValue; }
                }

                static JungleClear()
                {
                    Menu.AddGroupLabel("Jungle Clear");
                    _useQ = Menu.Add("jungleclear.q", new CheckBox("Use Q"));
                    _useW = Menu.Add("jungleclear.w", new CheckBox("Use W"));
                    _useE = Menu.Add("jungleclear.e", new CheckBox("Use E"));
                    _manaPercent = Menu.Add("jungleclear.mana", new Slider("Min Mana %", 45, 0, 100));
                }

                public static void Initialize()
                {
                }
            }

            public static class Misc
            {
                private static readonly CheckBox _autoQImmobile;
                private static readonly CheckBox _autoQOnDash;
                private static readonly CheckBox _autoEOnDash;
                private static readonly CheckBox _antiGapCloserQ;
                private static readonly CheckBox _antiGapCloserW;
                private static readonly CheckBox _antiGapCloserE;
                private static readonly CheckBox _playSoundToRCast;

                public static bool AutoQImmobile
                {
                    get { return _autoQImmobile.CurrentValue; }
                }

                public static bool AutoQOnDash
                {
                    get { return _autoQOnDash.CurrentValue; }
                }

                public static bool AutoEOnDash
                {
                    get { return _autoEOnDash.CurrentValue; }
                }

                public static bool AntiGapQ
                {
                    get { return _antiGapCloserQ.CurrentValue; }
                }

                public static bool AntiGapW
                {
                    get { return _antiGapCloserW.CurrentValue; }
                }

                public static bool AntiGapE
                {
                    get { return _antiGapCloserE.CurrentValue; }
                }

                public static bool IMAFIRINGMALAZER
                {
                    get { return _playSoundToRCast.CurrentValue; }
                }

                static Misc()
                {
                    Menu.AddGroupLabel("Misc");
                    _autoQImmobile = Menu.Add("misc.immobile.q", new CheckBox("Use Q on Immobile"));
                    _antiGapCloserQ = Menu.Add("misc.antigap.q", new CheckBox("Q for anti gap closers"));
                    _antiGapCloserW = Menu.Add("misc.antigap.w", new CheckBox("W for anti gap closers", false));
                    _antiGapCloserE = Menu.Add("misc.antigap.e", new CheckBox("E for anti gap closers", false));
                    _playSoundToRCast = Menu.Add("misc.lazer.sound", new CheckBox("IMAFIRINGMALAZER :========", false));
                }

                public static void Initialize()
                {
                }
            }
        }
    }
}
