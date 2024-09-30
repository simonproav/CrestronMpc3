using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using CrestronMpc3.Mpc3s;
using proAV.Core;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.EthernetCommunication;
using Crestron.SimplSharpPro.DeviceSupport;
using proAV.Core.Utilities;
using proAV.Core.CustomEventArgs;

namespace CrestronMpc3.Mpc3s {
    public class Mpc3_102_Handler : Mpc3HandlerBase {
        private MPC3x102Touchscreen _mpc3x102;
        private Mpc3_102_EiscListener _mpc3x102EiscListener;

        public Mpc3_102_Handler(EthernetIntersystemCommunications eisc_, Mpc3_102_EiscListener mpc3x102EiscListener_, MPC3x102Touchscreen mpc3x102_) :
            base(eisc_, mpc3x102EiscListener_, mpc3x102_) {
            _mpc3x102 = mpc3x102_;
            _mpc3x102EiscListener = mpc3x102EiscListener_;           
        }

        public new void Initialise() {
            _mpc3x102.EnableVolumeUpButton();
            _mpc3x102.EnableVolumeDownButton();
            _mpc3x102.EnableProximityWakeup.BoolValue = true;
            base.Initialise();         
        }

        protected override void ButtonFbOnChange(object sender_, BoolAndIntEventArgs boolAndIntEventArgs_) {
            CrestronConsole.PrintLine("Mpc3 102 Button {0} Change : {1}", boolAndIntEventArgs_.Integer, boolAndIntEventArgs_.Bool);

            if (_mpc3x102 == null) {
                return;
            }

            switch (boolAndIntEventArgs_.Integer) {
                case 1:
                    _mpc3x102.Feedback1.State = boolAndIntEventArgs_.Bool;
                    break;
                case 2:
                    _mpc3x102.Feedback2.State = boolAndIntEventArgs_.Bool;
                    break;
                case 3:
                    _mpc3x102.Feedback3.State = boolAndIntEventArgs_.Bool;
                    break;
                case 4:
                    _mpc3x102.Feedback4.State = boolAndIntEventArgs_.Bool;
                    break;
                case 5:
                    _mpc3x102.Feedback5.State = boolAndIntEventArgs_.Bool;
                    break;
                case 6:
                    _mpc3x102.Feedback6.State = boolAndIntEventArgs_.Bool;
                    break;
                case 7:
                    _mpc3x102.Feedback6.State = boolAndIntEventArgs_.Bool;
                    break;
                case 8:
                    _mpc3x102.Feedback6.State = boolAndIntEventArgs_.Bool;
                    break;
                case 9:
                    _mpc3x102.Feedback6.State = boolAndIntEventArgs_.Bool;
                    break;
            }
        }    
    }
}