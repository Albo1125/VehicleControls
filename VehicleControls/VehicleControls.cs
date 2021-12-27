using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleControls
{
    public class VehicleControls : BaseScript
    {
        private float maxSpeed = -1;
        public VehicleControls()
        {
            EventHandlers["VehicleControls:Trunk"] += new Action<dynamic>((dynamic) => {
                ToggleDoor(VehicleDoorIndex.Trunk);
            });
            EventHandlers["VehicleControls:Bonnet"] += new Action<dynamic>((dynamic) => {
                ToggleDoor(VehicleDoorIndex.Hood);
            });
            EventHandlers["VehicleControls:SetSpeed"] += new Action<float>((float speed) => {
                maxSpeed = ConvertMPHToMPS(speed);
            });
            EventHandlers["VehicleControls:DisableSpeed"] += new Action<dynamic>((dynamic) => {
                maxSpeed = -1;
            });
            EventHandlers["VehicleControls:Engine"] += new Action<dynamic>((dynamic) => {
                ToggleEngine();
            });
            EventHandlers["VehicleControls:Fix"] += new Action<dynamic>((dynamic) => {
                Fix();
            });
            EventHandlers["VehicleControls:Plate"] += new Action<string>((string plate) => {
                SetPlate(plate);
            });
            Main();
        }

        private static float ConvertMPSToMPH(float meterspersecond)
        {
            return meterspersecond * 2.2f;
        }

        private static float ConvertMPHToMPS(float mph)
        {
            return mph / 2.2f;
        }

        private void ToggleDoor(VehicleDoorIndex index)
        {
            if (LocalPlayer != null && LocalPlayer.Character != null && LocalPlayer.Character.CurrentVehicle != null && LocalPlayer.Character.CurrentVehicle.Exists())
            {
                VehicleDoor door = LocalPlayer.Character.CurrentVehicle.Doors[index];
                if (door != null)
                {
                    if (door.IsOpen)
                    {
                        door.Close();
                    }
                    else
                    {
                        door.Open();
                    }
                }
            }
        }

        private void ToggleEngine()
        {
            if (LocalPlayer != null && LocalPlayer.Character != null && LocalPlayer.Character.CurrentVehicle != null && LocalPlayer.Character.CurrentVehicle.Exists())
            {
                Vehicle veh = LocalPlayer.Character.CurrentVehicle;
                veh.IsEngineRunning = !veh.IsEngineRunning;
            }
        }

        private void Fix()
        {
            if (LocalPlayer != null && LocalPlayer.Character != null && LocalPlayer.Character.CurrentVehicle != null && LocalPlayer.Character.CurrentVehicle.Exists())
            {
                Vehicle veh = LocalPlayer.Character.CurrentVehicle;
                veh.Repair();
            }
        }

        private void SetPlate(string plate)
        {
            if (LocalPlayer != null && LocalPlayer.Character != null && LocalPlayer.Character.CurrentVehicle != null && LocalPlayer.Character.CurrentVehicle.Exists())
            {
                Vehicle veh = LocalPlayer.Character.CurrentVehicle;
                API.SetVehicleNumberPlateText(veh.Handle, plate);
            }
        }

        private bool LastInputWasController()
        {
            return !CitizenFX.Core.Native.Function.Call<bool>(CitizenFX.Core.Native.Hash._IS_INPUT_DISABLED, 2);
        }

        private async void Main()
        {
            bool plateWarningChecked = false;
            while (true)
            {
                await Delay(0);
                if (LocalPlayer != null && LocalPlayer.Character != null && LocalPlayer.Character.CurrentVehicle != null && LocalPlayer.Character.CurrentVehicle.Exists())
                {                    

                    //Speed/plate stuff
                    if (LocalPlayer.Character.CurrentVehicle.Driver == LocalPlayer.Character)
                    {
                        //Plate validation stuff
                        if (!plateWarningChecked)
                        {
                            string plate = API.GetVehicleNumberPlateText(LocalPlayer.Character.CurrentVehicle.Handle).Trim();
                            if (plate.Length == 8 && char.IsUpper(plate, 0) && char.IsUpper(plate, 1) && char.IsDigit(plate, 2) && char.IsDigit(plate, 3) && char.IsWhiteSpace(plate, 4) && char.IsUpper(plate, 5) &&
                                char.IsUpper(plate, 6) && char.IsUpper(plate, 7))
                            {
                                plateWarningChecked = true;
                            }
                            else
                            {
                                TriggerEvent("chatMessage", "SYSTEM", new int[] { 0, 0, 0 }, "Use /plate to set your in-game plate to a correctly formatted one. For example: /plate BX16 BKL");
                                plateWarningChecked = true;
                            }
                        }

                        if (!LastInputWasController() && (Game.IsControlJustPressed(0, Control.InteractionMenu) || Game.IsDisabledControlJustPressed(0, Control.InteractionMenu)))
                        {
                            if (maxSpeed != -1)
                            {
                                maxSpeed = -1;
                                Screen.ShowSubtitle("Speed Limiter disabled.");
                            }
                            else if (LocalPlayer.Character.CurrentVehicle.Speed > 3)
                            {
                                maxSpeed = LocalPlayer.Character.CurrentVehicle.Speed;
                                Screen.ShowSubtitle("Speed Limiter set to " + (int)Math.Ceiling(ConvertMPSToMPH(maxSpeed)) + " MPH.");
                            }
                        }


                        if (maxSpeed != -1 && LocalPlayer.Character.CurrentVehicle.Speed >= maxSpeed)
                        {
                            Game.DisableControlThisFrame(0, Control.VehicleAccelerate);
                        }
                    }
                }
                else
                {
                    plateWarningChecked = false;
                }
            }
        }
    }
}
