using System.Linq;
using Hax;

[Command("doortroll")]
internal class DoorTrollCommand : ICommand
{
    public void Execute(StringArray args)
    {
        if (args.Length is 0)
        {
            Chat.Print("Usage: doortroll <on/off> [time]");
            return;
        }

        var Doors = Helper.FindObjects<TerminalAccessibleObject>().Where(door => door.isBigDoor).ToArray();

        if (Doors.Count() is 0)
        {
            Chat.Print("Breaker box is not found!");
            return;
        }

        if (args[0].ToLower() == "on")
        {
            if (args.Length == 1)
            {
                Doors.ForEach(breaker => breaker.GetOrAddComponent<DoorTrollMod>());
                Chat.Print("Door Troll Mod installed.");
                return;
            }

            if (float.TryParse(args[1], out var Timer))
            {
                Doors.ForEach(breaker =>
                    {
                        var DoorTrollMod = breaker.GetOrAddComponent<DoorTrollMod>();
                        DoorTrollMod.TimeForToggles = Timer;
                    }
                );
                Chat.Print($"Door Troll Mod Installed & set with delay {Timer}.");
                return;
            }
            else
            {
                Chat.Print("Invalid value.");
                return;
            }
        }

        if (args[0].ToLower() == "off")
        {
            Doors.ForEach(breaker => breaker.RemoveComponent<DoorTrollMod>());
            Chat.Print("Door Troll Mod uninstalled.");
        }
    }
}