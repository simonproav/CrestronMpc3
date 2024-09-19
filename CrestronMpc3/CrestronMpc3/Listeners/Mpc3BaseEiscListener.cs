using System.Globalization;
using System.Linq;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.DeviceSupport;
using proAV.Core;
using proAV.Core.Extensions;
using Tabor_Cp3_EISC.Constants;
using System;
using proAV.Core.Utilities;
using proAV.Core.CustomEventArgs;
using Crestron.SimplSharpPro.EthernetCommunication;
using Crestron.SimplSharp;

namespace CrestronMpc3.Mpc3s {
    public abstract class Mpc3BaseEiscListener {
        public event EventHandler<BoolEventArgs> MuteFbEvent;
        public event EventHandler<BoolEventArgs> PowerFbEvent;
        public event EventHandler<BoolAndIntEventArgs> ButtonFbEvent;
        public event EventHandler<UShortEventArgs> VolumeFbEvent;

        protected EthernetIntersystemCommunications Eisc;

        public Mpc3BaseEiscListener(EthernetIntersystemCommunications eisc_) {
            Eisc = eisc_;          
        }

        public void Initialise() {
            Eisc.SigChange += EiscOnSigChange;
            CrestronConsole.PrintLine("Initialising Mpc3 Eisc Listener");
        }

        protected virtual void EiscOnSigChange(BasicTriList currentDevice_, SigEventArgs args_) {
            "EiscListener: {0}".PrintLine(string.Format("{0} Join {1}: {2}", args_.Sig.Type.ToString(), args_.Sig.Number, args_.Sig.BoolValue));

            switch (args_.Sig.Type) {
                case eSigType.Bool:                  
                    switch (args_.Sig.Number) {
                        case EiscBoolJoins.MuteFb:                           
                            EventHelper.RaiseEventHandler(this, MuteFbEvent, new BoolEventArgs(args_.Sig.BoolValue));                          
                            break;
                        case EiscBoolJoins.PowerFb:
                            EventHelper.RaiseEventHandler(this, PowerFbEvent, new BoolEventArgs(args_.Sig.BoolValue));
                            break;
                        case EiscBoolJoins.Button1:
                            EventHelper.RaiseEventHandler(this, ButtonFbEvent, new BoolAndIntEventArgs(args_.Sig.BoolValue, 1));
                            break;
                        case EiscBoolJoins.Button2:
                            EventHelper.RaiseEventHandler(this, ButtonFbEvent, new BoolAndIntEventArgs(args_.Sig.BoolValue, 2));
                            break;
                        case EiscBoolJoins.Button3:
                            EventHelper.RaiseEventHandler(this, ButtonFbEvent, new BoolAndIntEventArgs(args_.Sig.BoolValue, 3));
                            break;
                        case EiscBoolJoins.Button4:
                            EventHelper.RaiseEventHandler(this, ButtonFbEvent, new BoolAndIntEventArgs(args_.Sig.BoolValue, 4));
                            break;
                        case EiscBoolJoins.Button5:
                            EventHelper.RaiseEventHandler(this, ButtonFbEvent, new BoolAndIntEventArgs(args_.Sig.BoolValue, 5));
                            break;
                        case EiscBoolJoins.Button6:
                            EventHelper.RaiseEventHandler(this, ButtonFbEvent, new BoolAndIntEventArgs(args_.Sig.BoolValue, 6));
                            break;
                    }
                    break;
                case eSigType.UShort:
                    switch (args_.Sig.Number) {
                        case EiscUShortJoins.VolumeFb:
                            EventHelper.RaiseEventHandler(this, VolumeFbEvent, new UShortEventArgs(args_.Sig.UShortValue));                          
                            break;
                        default:
                            "Unused Serial Eisc {0}: {1}".PrintLine(args_.Sig.Number, args_.Sig.StringValue);
                            break;
                    }
                    break;
                case eSigType.String:
                    switch (args_.Sig.Number) {
                        default:
                            "Unused Serial Eisc {0}: {1}".PrintLine(args_.Sig.Number, args_.Sig.StringValue);
                            break;
                    }
                    break;
            }
        }
    }
}