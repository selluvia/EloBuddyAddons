using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Branddy
{
    internal class Bramenu
    {
        public static Menu Brand, ComboMenu, HarassMenu, KS, Draw;

        public static void StartMenu()
        {
            Brand = MainMenu.AddMenu("Branddy", "branddy");
            Brand.AddLabel("Brand add-on by selluvia");
            Brand.AddLabel("Version 0.1 Beta");

            ComboMenu = Brand.AddSubMenu("Combo", "combo");
            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.Add("useQ", new CheckBox("Use Q"));
            ComboMenu.Add("useW", new CheckBox("Use W"));
            ComboMenu.Add("useE", new CheckBox("Use E"));
            ComboMenu.Add("useR", new CheckBox("Use R"));
            ComboMenu.AddSeparator();
            ComboMenu.Add("smartQ", new CheckBox("Use Smart Q"));

            HarassMenu = Brand.AddSubMenu("Harass", "harass");
            HarassMenu.AddGroupLabel("Harass Settings");
            HarassMenu.Add("UseWharass", new CheckBox("Use W"));
            HarassMenu.Add("UseEharass", new CheckBox("Use E"));
            HarassMenu.Add("manam", new Slider("Min. mana to harass", 40, 0, 100));

            KS = Brand.AddSubMenu("KS Options", "ks");
            KS.AddGroupLabel("KillSteal Settings");
            KS.Add("useigniteks", new CheckBox("Use Ignite"));
            KS.Add("useqks", new CheckBox("Use Q"));
            KS.Add("usewks", new CheckBox("Use W"));
            KS.Add("userks", new CheckBox("Use R"));

            Draw = Brand.AddSubMenu("Drawing", "draw");
            Draw.AddGroupLabel("Drawing Settings");
            Draw.Add("drawQ", new CheckBox("Draw Q", true));
            Draw.Add("drawW", new CheckBox("Draw W", true));
            Draw.Add("drawE", new CheckBox("Draw E", true));
            Draw.Add("drawR", new CheckBox("Draw R", true));


                        
        }
    }
}
