using CommandSystem;
using Exiled.API.Features;
using Nickname;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoNickname.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Nick : ICommand
    {
        public string Command { get; } = "nickname";

        public string[] Aliases { get; } = { "nick", "nn" };

        public string Description { get; } = "Nicknames the player, first argument will be the name";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Log.Info("Running Nick Command");

            Player player = Player.Get(sender);

            if (player == null)
            {
                response = "Must be ingame";

                return false;
            }

            if (!Main.Instance.Config.NickPermissions.Any(perm => perm == player.GroupName) || Main.Instance.Config.NickPermissions == Array.Empty<string>())
            {
                response = "Command Failed: No Permissions" ;

                return false;
            }

            if (arguments.Count < 1 || arguments.At(0) == null)
            {
                response = "Command Failed: No arguments";
                return false;
            }

            if (Main.Nicknames.TryGetValue(player.UserId, out string value))
            {
                Main.Nicknames[player.UserId] = arguments.At(0);
            }
            else
            {
                Main.Nicknames.Add(player.UserId, arguments.At(0));
            }

            player.CustomName = arguments.At(0);
            response = "Set Nickname to " + arguments.At(0);

            return true;
        }
    }
    [CommandHandler(typeof(ClientCommandHandler))]
    public class ClearNick : ICommand
    {
        public string Command { get; } = "clearnickname";

        public string[] Aliases { get; } = { "clearnick", "cname" };

        public string Description { get; } = "Clears the players nickname";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            Log.Info("Running Clear Nick Command");

            Player player = Player.Get(sender);

            if (player == null)
            {
                response = "Must be ingame";
                return false;
            }

            Main.Nicknames.Remove(player.UserId);

            player.CustomName = string.Empty;
            response = "Cleared nickname";
            return true;
        }
    }
}
