using System;
using Hax;

[Command("credit")]
internal class CreditCommand : ICommand
{
    public void Execute(StringArray args)
    {
        if (Helper.Terminal is not Terminal terminal) return;
        if (args.Length is 0)
        {
            Chat.Print("Usage: credit <amount>");
            return;
        }

        if (!int.TryParse(args[0], out var amount))
        {
            Chat.Print("Invalid amount!");
            return;
        }

        terminal.groupCredits = Math.Clamp(terminal.groupCredits + amount, 0, int.MaxValue);
        terminal.SyncGroupCreditsServerRpc(terminal.groupCredits, terminal.numberOfItemsInDropship);
        Chat.Print($"You now have {terminal.groupCredits} credits!");
    }
}