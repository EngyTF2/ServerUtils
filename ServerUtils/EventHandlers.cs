using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp096;
using Exiled.Events.EventArgs.Server;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using UnityEngine;
using SysRandom = System.Random;
using URandom = UnityEngine.Random;

namespace ServerUtils
{
    public class EventHandlers
    {
        public void OnRoundStart()
        {
            Log.Debug("Round started!");
            if (Server.FriendlyFire == true)
            {
                Log.Debug("Disabling FF...");
                Server.FriendlyFire = false;
            }
            foreach (Player p in Player.List)
            {
                p.SetAmmo(AmmoType.Nato556, 100);
                p.SetAmmo(AmmoType.Nato762, 100);
                p.SetAmmo(AmmoType.Nato9, 100);
                p.SetAmmo(AmmoType.Ammo12Gauge, 100);
                p.SetAmmo(AmmoType.Ammo44Cal, 100);
            }
            try
            {
                Plugin.Instance.RagdollCleanup();
                Plugin.Instance.ItemCleanup();
            }
            catch (Exception e)
            {
                Log.Error($"Error with cleanup!\n{e}");
            }
        }
        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            if (Server.FriendlyFire == false)
            {
                Log.Debug("FF on!");
                Map.Broadcast(Plugin.cfg.BroadcastTime, Plugin.cfg.BroadcastMessage, Broadcast.BroadcastFlags.Normal, true);
                Server.FriendlyFire = true;
            }
        }

        public void OnAddingTarget(AddingTargetEventArgs ev)
        {
            try
            {
                Log.Debug($"{ev.Target} see {ev.Player} face!");
                if (ev.Target != null && ev.Player != null)
                {
                    ev.Target.ShowHint(Plugin.cfg.MessageForScp096target, Plugin.cfg.HintTime);
                }
            }
            catch (Exception e)
            {
                Log.Error($"Error with SCP096!\n{e}");
            }
        }

        public void OnFlippingCoin(FlippingCoinEventArgs ev)
        {
            try
            {
                if (Plugin.cfg.TpCoinRooms.Count > 2)
                {
                    SysRandom r = new SysRandom();

                    for (int i = 0; i < 1; i++)
                    {
                        int index = r.Next(Plugin.cfg.TpCoinRooms.Count);
                        ev.Player.Teleport(Plugin.cfg.TpCoinRooms[index]);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error($"Error with tp player!\n{e}");
            }
        }
        public void OnUsingRadioBattery(UsingRadioBatteryEventArgs ev)
        {
            if (Plugin.cfg.InfinityRadio)
            {
                ev.IsAllowed = false;
            }
        }

        public void OnReloadingWeapon(ReloadingWeaponEventArgs ev)
        {
            if (Plugin.cfg.InfinityAmmoMode.Contains("Reload"))
            {
                ev.Player.SetAmmo(AmmoType.Nato556, 101);
                ev.Player.SetAmmo(AmmoType.Nato762, 101);
                ev.Player.SetAmmo(AmmoType.Nato9, 101);
                ev.Player.SetAmmo(AmmoType.Ammo12Gauge, 101);
                ev.Player.SetAmmo(AmmoType.Ammo44Cal, 101);
                if (ev.Firearm.Type != ItemType.ParticleDisruptor && ev.Firearm != null)
                {
                    ev.Player.SetAmmo(ev.Firearm.AmmoType, (ushort)(ev.Firearm.MaxAmmo + 2));
                }
            }
        }
        public void OnShooting(ShootingEventArgs ev)
        {
            if (Plugin.cfg.InfinityAmmoMode.Contains("NoReload") && ev.Player != null)
            {
                ev.Player.SetAmmo(AmmoType.Nato556, 101);
                ev.Player.SetAmmo(AmmoType.Nato762, 101);
                ev.Player.SetAmmo(AmmoType.Nato9, 101);
                ev.Player.SetAmmo(AmmoType.Ammo12Gauge, 101);
                ev.Player.SetAmmo(AmmoType.Ammo44Cal, 101);
            }
        }

        public void OnJoined(JoinedEventArgs ev)
        {
            if (Plugin.cfg.WelcomeMessage.Length > 0)
            {
                ev.Player.Broadcast(Plugin.cfg.BroadcastTime, Plugin.cfg.BroadcastMessage, Broadcast.BroadcastFlags.Normal, true);
            }
        }
    }
}
