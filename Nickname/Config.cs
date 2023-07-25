using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nickname
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;

        [Description("Player groups allowed to use the command, leave empty for everyone!")]
        public string[] NickPermissions { get; set; } = Array.Empty<string>();
    }
}
