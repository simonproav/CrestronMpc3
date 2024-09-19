using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro.EthernetCommunication;

namespace CrestronMpc3.Mpc3s {
    public class Mpc3_102_EiscListener : Mpc3BaseEiscListener {
        public Mpc3_102_EiscListener(EthernetIntersystemCommunications eisc_) : base(eisc_) { }

        public new void Initialise() {
            base.Initialise();
        }
    }
}