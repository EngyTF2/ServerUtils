using Exiled.API.Features;
using System;
using MEC;
using PlEv = Exiled.Events.Handlers.Player;
using SrvEv = Exiled.Events.Handlers.Server;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Exiled.API.Features.Pickups;

namespace ServerUtils
{
    public class Plugin : Plugin<Config>
    {
        private Harmony harmony;
        private EventHandlers EvH;
        public static Plugin Instance;
        public static Config cfg;
        public override string Name => "ServerUtils";
        public override string Prefix => Name;
        public override string Author => "RABB1T#3072";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(7, 0, 0);

        public override void OnEnabled()
        {
            try
            {
                harmony = new Harmony($"com-ServerUtils-RABB1T-{DateTime.Now.Ticks}");
                harmony.PatchAll();
            }
            catch (Exception e)
            {
                Log.Debug($"Error with harmony! {e}");
            }
            Instance = this;
            EventsOnEnabledMethod();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Instance = null;

            harmony.UnpatchAll();

            EventsOnDisabledMethod();
            base.OnDisabled();
        }
        public void EventsOnEnabledMethod()
        {
            PlEv.FlippingCoin += EvH.OnFlippingCoin;
            PlEv.Shooting += EvH.OnShooting;
            PlEv.Joined += EvH.OnJoined;
            PlEv.ReloadingWeapon += EvH.OnReloadingWeapon;
            PlEv.UsingRadioBattery += EvH.OnUsingRadioBattery;
            SrvEv.RoundEnded += EvH.OnRoundEnded;
            Exiled.Events.Handlers.Scp096.AddingTarget += EvH.OnAddingTarget;
            SrvEv.RoundStarted += EvH.OnRoundStart;
        }
        public void EventsOnDisabledMethod()
        {
            PlEv.FlippingCoin -= EvH.OnFlippingCoin;
            PlEv.Shooting -= EvH.OnShooting;
            PlEv.Joined -= EvH.OnJoined;
            PlEv.ReloadingWeapon -= EvH.OnReloadingWeapon;
            SrvEv.RoundEnded -= EvH.OnRoundEnded;
            PlEv.UsingRadioBattery -= EvH.OnUsingRadioBattery;
            Exiled.Events.Handlers.Scp096.AddingTarget -= EvH.OnAddingTarget;
            SrvEv.RoundStarted -= EvH.OnRoundStart;
        }
        public void RagdollCleanup()
        {
            if (Config.RagdollCleanupTime > 0)
            {
                for (; ; )
                {
                    Timing.CallDelayed(Config.RagdollCleanupTime, delegate ()
                    {
                        foreach (Ragdoll r in Ragdoll.List.ToList())
                        {
                            if (r.Position.y < -1500f)
                            {
                                r.Destroy();
                            }
                        }
                    });
                }
            }
        }
        public void ItemCleanup()
        {
            if (Config.ItemsCleanupTime > 0)
            {
                for (; ; )
                {
                    Timing.CallDelayed(Config.ItemsCleanupTime, delegate ()
                    {
                        foreach (Pickup i in Pickup.List.ToList())
                        {
                            if (i.Position.y < -1500f)
                            {
                                i.Destroy();
                            }
                        }
                    });
                }
            }
        }
    }
}
