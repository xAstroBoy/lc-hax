#region

using Hax;

#endregion

[HostCommand("quota")]
class QuotaCommand : ICommand {
    public void Execute(StringArray args) {
        if (Helper.TimeOfDay is not TimeOfDay timeOfDay) return;
        if (args.Length < 1) {
            Chat.Print("Usage: quota <amount> <fulfilled?>");
            return;
        }

        if (!ushort.TryParse(args[0], out ushort amount)) {
            Chat.Print("Invalid amount!");
            return;
        }

        if (!args[1].TryParse(0, out ushort fulfilled)) {
            Chat.Print("Invalid fulfilled amount!");
            return;
        }

        timeOfDay.profitQuota = amount;
        timeOfDay.quotaFulfilled = fulfilled;
        timeOfDay.UpdateProfitQuotaCurrentTime();
    }
}
