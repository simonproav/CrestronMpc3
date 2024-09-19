using System;
using System.Collections.Generic;
using Crestron.SimplSharp;
using proAV.Core.Extensions;
using proAV.Core.Utilities;

namespace CrestronMpc3 {
    public static class BootManager {
        public static List<Action> BootFunctions = new List<Action>();
        public static bool SystemBooted { get; set; }

        public static void BootComplete() {
            SystemBooted = true;
            CrestronConsole.PrintLine("Boot Complete");
        }
        public static void WriteBootPositionToConsole(int value_) {
            var percentageComplete = NumberScaling.ScaleIntoPercent(value_, 0, BootFunctions.Count);
            "".PrintLine();
            "SystemInitialiser {0}% Complete".PrintSystemMessage(percentageComplete);
            "".PrintLine();
        }
    }
}