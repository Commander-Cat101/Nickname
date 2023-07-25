using Exiled.API.Features;
using Exiled.CreditTags;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nickname
{
    public class Main : Plugin<Config>
    {
        private static readonly Main Singleton = new Main();
        public static Main Instance = Singleton;
        public override string Author => "Commander__Cat";
        public override string Name => "Nicknames";

        public static Dictionary<string, string> Nicknames = new Dictionary<string, string>();

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers += OnMatchStart;
        }
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers -= OnMatchStart;
            Nicknames = null;
        }
        public void OnMatchStart()
        {
            foreach (var kvp in Nicknames)
            {
                if(Player.TryGet(kvp.Key, out var player))
                {
                    player.CustomName = kvp.Value;
                }
            }
        }
    }
}
