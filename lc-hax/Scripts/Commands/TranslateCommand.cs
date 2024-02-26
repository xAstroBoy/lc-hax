using Hax;

[Command("translate")]
class TranslateCommand : ICommand {
    public void Execute(StringArray args) {
        if (args.Length < 3) {
            Chat.Print("Usages:",
                "translate <language> <language> <text>",
                "translate <language> <language> -d"
            );

            return;
        }

        string? source = args[0];
        string? target = args[1];

        if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(target)) {
            Chat.Print("Invalid arguments!");
            return;
        }

        if (args[2] is "-d") {
            State.TranslateDetachedState = new TranslatePipe() {
                SourceLanguage = source,
                TargetLanguage = target
            };

            Helper.CreateToggleableComponent<WaitForGameEndBehaviour>("Translate Detached")?
                  .AddActionAfter(() => State.TranslateDetachedState = null);
        }

        else {
            Helper.Translate(args[0], args[1], string.Join(' ', args[2..]));
        }
    }
}
