using Exiled.API.Enums;
using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerUtils
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = true;
        [Description("Welcome message for connected player")]
        public string WelcomeMessage { get; set; } = "Hi!";
        [Description("Welcome message time")]
        public int WelcomeMessageTime { get; set; } = 10;
        [Description("FFON EndRound BroadcastTime")]
        public ushort BroadcastTime { get; set; } = 30;
        [Description("FFON EndRound Broadcast")]
        public string BroadcastMessage { get; set; }
        [Description("Hint for SCP096 target")]
        public string MessageForScp096target { get; set; } = "You are SCP096 target!";
        [Description("Time for all hints")]
        public float HintTime { get; set; } = 5f;
        [Description("Enable infinity radio?")]
        public bool InfinityRadio { get; set; } = true;
        [Description("Infinity ammo mode. If you set null, disable or dont set anything and it will be disabled. Modes - Reload(Infinity ammo with reload), NoReload(Infinity ammo without reload)")]
        public string InfinityAmmoMode { get; set; } = "Reload";

        [Description("Tp coin rooms. Set nothing to disable")]
        public List<RoomType> TpCoinRooms = new List<RoomType>()
        {
            RoomType.LczArmory,
            RoomType.LczCurve,
            RoomType.LczTCross,
            RoomType.EzCafeteria
        };
        [Description("Ragdoll cleanup time. Set 0 to disable")]
        public int RagdollCleanupTime { get; set; } = 600;
        [Description("Items cleanup time. Set 0 to disable")]
        public int ItemsCleanupTime { get; set; } = 1200;
    }
}
