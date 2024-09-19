using System;
using System.Collections.Generic;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.DM;
using Crestron.SimplSharpPro.EthernetCommunication;
using Crestron.SimplSharpPro.GeneralIO;
using proAV.Core;
using proAV.Core.Extensions;
using proAV.Core.Framework;
using CrestronMpc3.Mpc3s;
using Crestron.SimplSharp;

namespace CrestronMpc3 {
    public class SystemInitialiser : SystemInitialiserBase {
        public override void Initialise() {
            this.PrintFunctionName("Initialise");
            ControlSystem.Mpc3Handler.Initialise();
        }

        [SystemCreationMethod("Mpc3", Order = 1)]
        private void CreateMpc3() {
            var ipid = ControlSystem.Configs.Mpc3Config.Connection.Ipid.ToIpId();
            var ipAddress = ControlSystem.Configs.Mpc3Config.Connection.Address;

            switch (ControlSystem.Configs.Mpc3Config.Type.ToLower()) {
                case "mpc3x102touchscreen":
                    var eisc = new EthernetIntersystemCommunications(ipid, ipAddress, ProAvControlSystem.ControlSystem);
                    var mpc3x102EiscListener = new Mpc3_102_EiscListener(eisc);
                   
                    if(ProAvControlSystem.ControlSystem.MPC3x102TouchscreenSlot == null) {
                        ErrorLog.Error("!!! ControlSystem does not contain a MPC3x102Touchscreen Slot !!!");
                        //return;
                    }

                    ControlSystem.Mpc3Handler = new Mpc3_102_Handler(eisc, mpc3x102EiscListener, ProAvControlSystem.ControlSystem.MPC3x102TouchscreenSlot);
                    break;
            }
        }
    }
}