# VehicleControls
VehicleControls is a UK-based resource for FiveM by Albo1125 that provides for some additional control over vehicles. It is available at [https://github.com/Albo1125/VehicleControls](https://github.com/Albo1125/VehicleControls)

## Installation & Usage
1. Download the latest release.
2. Unzip the VehicleControls folder into your resources folder on your FiveM server.
3. Add the following to your server.cfg file:
```text
ensure VehicleControls
```
4. Optionally, customise the commands in sv_VehicleControls.lua.

## Commands & Controls
* /boot - Toggles the boot of your vehicle.
* /bonnet - Toggles the bonnet of your vehicle.
* /engine - Toggles the engine of your vehicle.
* /fix - Fixes your vehicle.
* /plate PLATE - Sets your vehicle's plate to PLATE. Will prompt users to do this when getting into a vehicle with an incorrectly formatted plate.
* /speedlimit SPEED - Limits your vehicle's speed to SPEED MPH. If SPEED is unspecified, removes the speed limit.
* Press M while in a moving vehicle to toggle the speed limit function.

## Improvements & Licencing
Please view the license. Improvements and new feature additions are very welcome, please feel free to create a pull request. As a guideline, please do not release separate versions with minor modifications, but contribute to this repository directly. However, if you really do wish to release modified versions of my work, proper credit is always required and you should always link back to this original source and respect the licence.

## Libraries used (many thanks to their authors)
* [CitizenFX.Core.Client](https://www.nuget.org/packages/CitizenFX.Core.Client)
