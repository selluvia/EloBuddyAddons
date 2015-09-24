using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using EloBuddy.SDK.Events;

namespace Branddy
{
    class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (ObjectManager.Player.ChampionName.ToLower() != "brand") return;
            Bootstrap.Init(null);
            Bramenu.StartMenu();
            Configs.SpellInitiation();
            Configs.AutoStun();
            Game.OnTick += Configs.Game_OnTick;
            Chat.Print("Branddy ready to incinerate the world!");


        }
        
    }
}
