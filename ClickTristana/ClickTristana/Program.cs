using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using Color = System.Drawing.Color;

namespace ClickTristana
{
    class Program
    {
        public static Menu Menu,
            DrawMenu,
            ComboMenu,
            HarassMenu,
            Prediction;

        public static Spell.Active Q;
        public static Spell.Skillshot W;
        public static Spell.Targeted E;
        public static Spell.Targeted R;

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Game_OnStart;
            Drawing.OnDraw += Game_OnDraw;
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            var alvo = TargetSelector.GetTarget(1000, DamageType.Physical);

            if (!alvo.IsValid()) return;

            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo)
            {
                if (Q.IsReady() && ComboMenu["comboQ"].Cast<CheckBox>().CurrentValue)
                {
                    Q.Cast();
                }

                if (W.IsReady() && _Player.Distance(alvo) <= W.Range + _Player.GetAutoAttackRange() && ComboMenu["comboW"].Cast<CheckBox>().CurrentValue)
                {
                    W.Cast(alvo);
                }

                if (E.IsReady() && E.IsInRange(alvo) && ComboMenu["comboE"].Cast<CheckBox>().CurrentValue)
                {
                    E.Cast(alvo);
                }

                if (R.IsReady() && R.IsInRange(alvo) && ComboMenu["comboR"].Cast<CheckBox>().CurrentValue)
                {
                    R.Cast(alvo);
                }
            }
        }

        private static void Game_OnDraw(EventArgs args)
        {

        }

        private static void Game_OnStart(EventArgs args)
        {
            Chat.Print("Update 1.1");

            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Circular);
            E = new Spell.Targeted(SpellSlot.E, 575);
            R = new Spell.Targeted(SpellSlot.R, 575);

            Menu = MainMenu.AddMenu("Tristana", "ClickTristana");
            Menu.AddSeparator();
            Menu.AddLabel("By ClickBuDyy");

            DrawMenu = MainMenu.AddMenu("Drawings", "Drawings");
            DrawMenu.Add("Disable", new CheckBox("Disable all drawings", false));
            DrawMenu.AddSeparator();
            DrawMenu.Add("Q", new CheckBox("Draw Q Range"));
            DrawMenu.Add("W", new CheckBox("Draw W Range", false));
            DrawMenu.Add("E", new CheckBox("Draw E Range"));
            DrawMenu.Add("R", new CheckBox("Draw R Range", false));
            DrawMenu.Add("Enemy.Target", new CheckBox("Draw circle on enemy target"));
            DrawMenu.Add("Ally.Target", new CheckBox("Draw circle on ally target"));
            ComboMenu = Menu.AddSubMenu("Combo", "macComboTristana");
            ComboMenu.Add("comboQ", new CheckBox("Use Q in combo", true));
            ComboMenu.Add("comboW", new CheckBox("Use W in combo", true));
            ComboMenu.Add("comboE", new CheckBox("Use E in combo", true));
            ComboMenu.Add("comboR", new CheckBox("Use R in combo", true));

        }
    }
 }
