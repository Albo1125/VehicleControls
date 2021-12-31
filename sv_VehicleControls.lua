function bootfunc(source, args, rawCommand)
	TriggerClientEvent("VehicleControls:Trunk", source)
end

RegisterCommand('boot', bootfunc, false)

function bonnetfunc(source, args, rawCommand)
	TriggerClientEvent("VehicleControls:Bonnet", source)
end

RegisterCommand('bonnet', bonnetfunc, false)

function enginefunc(source, args, rawCommand)
	TriggerClientEvent("VehicleControls:Engine", source)
end

RegisterCommand('engine', enginefunc, false)

function fixfunc(source, args, rawCommand)
	TriggerClientEvent("VehicleControls:Fix", source)
		print("Fixed vehicle for ".. GetPlayerName(source))
end

RegisterCommand('fix', fixfunc, false)

function platefunc(source, args, rawCommand)
	local plate = string.upper(trim(table.concat(args, " ")))
	TriggerClientEvent("VehicleControls:Plate", source, plate)
	TriggerClientEvent('chatMessage', source, 'SYSTEM', {0,0,0}, "Plate set to: ".. plate ..".")
end

RegisterCommand('plate', platefunc, false)

function speedlimitfunc(source, args, rawCommand)
	if args[1] ~= nil then
		TriggerClientEvent("VehicleControls:SetSpeed", source, args[1])
		TriggerClientEvent('chatMessage', source, 'SpeedLimiter', {0,0,0}, "Speed Limiter set to ".. args[1].. " MPH.")
	else
		TriggerClientEvent("VehicleControls:DisableSpeed", source)
		TriggerClientEvent('chatMessage', source, 'SpeedLimiter', {0,0,0}, "Speed Limiter disabled.")
	end
end

RegisterCommand('speedlimit', speedlimitfunc, false)

function trim(s)
  -- from PiL2 20.4
  return (s:gsub("^%s*(.-)%s*$", "%1"))
end