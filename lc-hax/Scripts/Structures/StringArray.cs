#region

using System;

#endregion

readonly ref struct StringArray {
    internal required ReadOnlySpan<string> Span { get; init; }

    internal int Length => this.Span.Length;

    internal string? this[ushort index] => index >= this.Span.Length ? null : this.Span[index];

    internal StringArray this[Range range] => new() {
        Span = this.Span[range]
    };

    public static implicit operator ReadOnlySpan<string>(StringArray stringArray) => stringArray.Span;

    public static implicit operator StringArray(ReadOnlySpan<string> span) =>
        new() {
            Span = span
        };

    public static implicit operator StringArray(string[] array) =>
        new() {
            Span = array
        };

    public static implicit operator string[](StringArray stringArray) => stringArray.Span.ToArray();
}
