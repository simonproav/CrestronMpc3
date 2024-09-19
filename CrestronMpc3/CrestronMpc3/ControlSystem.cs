using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharp;
using Crestron.SimplSharp.Reflection;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.CrestronThread;
using Crestron.SimplSharpPro.EthernetCommunication;
using Crestron.SimplSharpPro.GeneralIO;
using proAV.Core;
using proAV.Core.Extensions;
using proAV.Core.Interfaces.API;
using proAV.Core.Utilities;
using CrestronMpc3.Mpc3s;
using CrestronMpc3.Configs;      	

namespace CrestronMpc3 {
    public class ControlSystem : ProAvControlSystem {
        public static ProjectConfigs Configs { get; private set; }
        public static Mpc3HandlerBase Mpc3Handler { get; set; }

        public ControlSystem()
            : base(Assembly.GetExecutingAssembly(), 20) { }

        protected override object StartProgram(object _) {
            Configs = new ProjectConfigs();

#if DEBUG
            CrestronConsole.PrintLine("GO!");
            ProgramUpdateChecker.AutoUpdateProgram = true;
            Configs.GetEmbeddedConfigs(Assembly.GetExecutingAssembly(), true);
#endif
#if !DEBUG
			Configs.GetEmbeddedConfigs(Assembly.GetExecutingAssembly(), false);
#endif

            Configs.UpdateConfigs();

            var initialiser = new SystemInitialiser();
            initialiser.Create(new ConsolePercentageFeedback());
            initialiser.Initialise();

            CreateProjectConsoleCommands();
            return null;
        }

        private void CreateProjectConsoleCommands() {
            ConsoleCommands.Create(s_ => Debugger.Break(), "debugcs", "Stop code in ControlSystem.cs");           
        }
    }
}