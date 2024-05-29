using System.Linq;
using Hax;

[Command("togglepower")]
internal class TogglePowerCommand : ICommand
{
    public void Execute(StringArray _)
    {
        if (Helper.RoundManager is not RoundManager round) return;
        if (round.powerOffPermanently) Chat.Print("Power Can't be toggled, Someone pulled the apparatus.");
        var Breaker = Helper.FindObjects<BreakerBox>().FirstOrDefault(breaker => breaker.name.Contains("(Clone)"));
        if (Breaker != null)
            Helper.SetPowerSwitch(!Breaker.isPowerOn);
        else
            Chat.Print("Breaker box not found, can't toggle the power!");
    }
}