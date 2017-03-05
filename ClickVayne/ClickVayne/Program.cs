using System;
using EloBuddy;
using EloBuddy.SDK.Events;

namespace ClickVayne
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Vayne") return;

            MenuManager.Initialize();
            MenuManager.Modes.Gosu.UnSetGod();
            new NotifyVayne().Initialize();
        }
    }
}
