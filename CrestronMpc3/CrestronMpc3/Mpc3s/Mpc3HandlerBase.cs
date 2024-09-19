using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro.EthernetCommunication;
using CrestronMpc3.Mpc3s;
using proAV.Core;
using proAV.Core.CustomEventArgs;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.DeviceSupport;
using Tabor_Cp3_EISC.Constants;
using proAV.Core.Utilities;

namespace CrestronMpc3.Mpc3s {
    public abstract class Mpc3HandlerBase {
        protected EthernetIntersystemCommunications Eisc;
        protected Mpc3BaseEiscListener Mpc3BaseEiscListener;
        private MPC3Basic Mpc3Basic;

        public Mpc3HandlerBase(EthernetIntersystemCommunications eisc_, Mpc3BaseEiscListener mpc3BaseEiscListener_, MPC3Basic mpc3Basic_) {
            Eisc = eisc_;
            Mpc3BaseEiscListener = mpc3BaseEiscListener_;
            Mpc3Basic = mpc3Basic_;
            CreateConsoleCommands();
        }

        public virtual void Initialise() {
            Mpc3BaseEiscListener.Initialise();

            if (Mpc3Basic != null) {
                Mpc3Basic.EnableMuteButton();
                Mpc3Basic.EnablePowerButton();
                Mpc3Basic.EnableNumericalButton(1);
                Mpc3Basic.EnableNumericalButton(2);
                Mpc3Basic.EnableNumericalButton(3);
                Mpc3Basic.EnableNumericalButton(4);
                Mpc3Basic.EnableNumericalButton(5);
                Mpc3Basic.EnableNumericalButton(6);

                Mpc3Basic.ButtonStateChange += Mpc3BasicOnButtonStateChange;
                Mpc3Basic.Register();
            }           

            Mpc3BaseEiscListener.VolumeFbEvent += VolumeFbOnChange;
            Mpc3BaseEiscListener.MuteFbEvent += MuteFbOnChange;
            Mpc3BaseEiscListener.PowerFbEvent += PowerFbOnChange;
            Mpc3BaseEiscListener.ButtonFbEvent += ButtonFbOnChange;
            Eisc.Register();          
        }

        protected void Mpc3BasicOnButtonStateChange(object sender_, ButtonEventArgs buttonEventArgs_) {
            CrestronConsole.PrintLine("Mpc3 Button State Change : {0} : {1}", buttonEventArgs_.Button.Name, buttonEventArgs_.NewButtonState);
            ButtonPressToEisc(buttonEventArgs_.Button.Name, buttonEventArgs_.NewButtonState);
        }

        protected void ButtonPressToEisc(eButtonName buttonName_, eButtonState newButtonState_) {
            CrestronConsole.PrintLine("Eisc : {0} : {1}", buttonName_, newButtonState_);
            bool buttonState;

            switch (newButtonState_) {
                case eButtonState.Pressed:
                    buttonState = true;
                    break;
                case eButtonState.Released:
                    buttonState = false;
                    break;
                default:
                    return;
            }

            switch (buttonName_) {
                case eButtonName.VolumeUp:
                    Eisc.BooleanInput[EiscBoolJoins.VolumeUp].BoolValue = buttonState;
                    break;
                case eButtonName.VolumeDown:
                    Eisc.BooleanInput[EiscBoolJoins.VolumeDown].BoolValue = buttonState;
                    break;
                case eButtonName.Mute:
                    Eisc.BooleanInput[EiscBoolJoins.Mute].BoolValue = buttonState;
                    break;
                case eButtonName.Power:
                    Eisc.BooleanInput[EiscBoolJoins.Power].BoolValue = buttonState;
                    break;
                case eButtonName.Button1:
                    Eisc.BooleanInput[EiscBoolJoins.Button1].BoolValue = buttonState;
                    break;
                case eButtonName.Button2:
                    Eisc.BooleanInput[EiscBoolJoins.Button2].BoolValue = buttonState;
                    break;
                case eButtonName.Button3:
                    Eisc.BooleanInput[EiscBoolJoins.Button3].BoolValue = buttonState;
                    break;
                case eButtonName.Button4:
                    Eisc.BooleanInput[EiscBoolJoins.Button4].BoolValue = buttonState;
                    break;
                case eButtonName.Button5:
                    Eisc.BooleanInput[EiscBoolJoins.Button5].BoolValue = buttonState;
                    break;
                case eButtonName.Button6:
                    Eisc.BooleanInput[EiscBoolJoins.Button6].BoolValue = buttonState;
                    break;
            }
        }

        protected void VolumeFbOnChange(object sender_, UShortEventArgs uShortEventArgs_) {
            CrestronConsole.PrintLine("EiscFb : VolumeFb : {0}", uShortEventArgs_.Value);
            if (Mpc3Basic != null) {
                Mpc3Basic.VolumeBargraph.UShortValue = uShortEventArgs_.Value;
            }            
        }

        protected void MuteFbOnChange(object sender_, BoolEventArgs boolEventArgs_) {
            CrestronConsole.PrintLine("EiscFb : MuteFb : {0}", boolEventArgs_.Value);
            if (Mpc3Basic != null) {
                Mpc3Basic.FeedbackMute.State = boolEventArgs_.Value;
            }
        }

        protected void PowerFbOnChange(object sender_, BoolEventArgs boolEventArgs_) {
            CrestronConsole.PrintLine("EiscFb : PowerFb : {0}", boolEventArgs_.Value);
            if (Mpc3Basic != null) {
                Mpc3Basic.FeedbackPower.State = boolEventArgs_.Value;
            }
        }

        protected virtual void ButtonFbOnChange(object sender_, BoolAndIntEventArgs boolAndIntEventArgs_) {
            CrestronConsole.PrintLine("Mpc3 Basic Button Change");

            switch (boolAndIntEventArgs_.Integer) {
                case 1:
                    Mpc3Basic.Feedback1.State = boolAndIntEventArgs_.Bool;
                    break;
                case 2:
                    Mpc3Basic.Feedback2.State = boolAndIntEventArgs_.Bool;
                    break;
                case 3:
                    Mpc3Basic.Feedback3.State = boolAndIntEventArgs_.Bool;
                    break;
                case 4:
                    Mpc3Basic.Feedback4.State = boolAndIntEventArgs_.Bool;
                    break;
                case 5:
                    Mpc3Basic.Feedback5.State = boolAndIntEventArgs_.Bool;
                    break;
                case 6:
                    Mpc3Basic.Feedback6.State = boolAndIntEventArgs_.Bool;
                    break;
            }
        }

        private void CreateConsoleCommands() {
            ConsoleCommands.Create(s_ => ButtonPressToEisc(eButtonName.VolumeUp, eButtonState.Pressed), "volUpp", "Simulate Volume Up button press on MPC3");
            ConsoleCommands.Create(s_ => ButtonPressToEisc(eButtonName.VolumeUp, eButtonState.Released), "volUpr", "Simulate Volume Up button release on MPC3");
            ConsoleCommands.Create(s_ => ButtonPressToEisc(eButtonName.VolumeDown, eButtonState.Pressed), "volDownp", "Simulate Volume Down button press on MPC3");
            ConsoleCommands.Create(s_ => ButtonPressToEisc(eButtonName.VolumeDown, eButtonState.Released), "volDownr", "Simulate Volume Down button release on MPC3");
            ConsoleCommands.Create(s_ => {
                ButtonPressToEisc(eButtonName.Mute, eButtonState.Pressed);
                ButtonPressToEisc(eButtonName.Mute, eButtonState.Released);
            }, "mute", "Simulate Volume Mute button on MPC3");
            ConsoleCommands.Create(s_ => {
                ButtonPressToEisc(eButtonName.Power, eButtonState.Pressed);
                ButtonPressToEisc(eButtonName.Power, eButtonState.Released);
            }, "power", "Simulate Power button on MPC3");
            ConsoleCommands.Create(s_ => {
                ButtonPressToEisc(eButtonName.Button1, eButtonState.Pressed);
                ButtonPressToEisc(eButtonName.Button1, eButtonState.Released);
            }, "b1", "Simulate button1 on MPC3");
            ConsoleCommands.Create(s_ => {
                ButtonPressToEisc(eButtonName.Button2, eButtonState.Pressed);
                ButtonPressToEisc(eButtonName.Button2, eButtonState.Released);
            }, "b2", "Simulate button2 on MPC3");
            ConsoleCommands.Create(s_ => {
                ButtonPressToEisc(eButtonName.Button3, eButtonState.Pressed);
                ButtonPressToEisc(eButtonName.Button3, eButtonState.Released);
            }, "b3", "Simulate button3 on MPC3");
            ConsoleCommands.Create(s_ => {
                ButtonPressToEisc(eButtonName.Button4, eButtonState.Pressed);
                ButtonPressToEisc(eButtonName.Button4, eButtonState.Released);
            }, "b4", "Simulate button4 on MPC3");
            ConsoleCommands.Create(s_ => {
                ButtonPressToEisc(eButtonName.Button5, eButtonState.Pressed);
                ButtonPressToEisc(eButtonName.Button5, eButtonState.Released);
            }, "b5", "Simulate button5 on MPC3");
            ConsoleCommands.Create(s_ => {
                ButtonPressToEisc(eButtonName.Button6, eButtonState.Pressed);
                ButtonPressToEisc(eButtonName.Button6, eButtonState.Released);
            }, "b6", "Simulate button6 on MPC3");
        }
    }
}