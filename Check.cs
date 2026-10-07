namespace LocalAI.Core;

public record Check(CheckKind Kind, Quantifier Quant, params string[] Args)
{
    public string? Validate()
    {
        return Kind switch
        {
            CheckKind.Contains or CheckKind.Has or CheckKind.Uses or CheckKind.Requires
                => Args.Length >= 1 ? null : $"{Kind} needs at least 1 arg",

            CheckKind.Is or CheckKind.Equals or CheckKind.Compares or CheckKind.Contrasts
                => Args.Length is 0 or 2 ? null : $"{Kind} takes 0 or 2 args",

            CheckKind.Before or CheckKind.After or CheckKind.During
                => Args.Length == 2 ? null : $"{Kind} needs exactly 2 args",

            CheckKind.Between
                => Args.Length == 3 ? null : "Between needs exactly 3 args",

            CheckKind.DependsOn or CheckKind.Affects or CheckKind.Causes or CheckKind.ResultsIn
                => Args.Length == 2 ? null : $"{Kind} needs exactly 2 args",

            CheckKind.Changes
                => Args.Length == 3 ? null : "Changes needs exactly 3 args",

            CheckKind.Remains
                => Args.Length is 1 or 2 ? null : "Remains takes 1 or 2 args",

            CheckKind.Internal or CheckKind.External
                => Args.Length == 2 ? null : $"{Kind} needs exactly 2 args",

            _ => $"Unknown check kind: {Kind}"
        };
    }

    public string Describe()
    {
        return Kind switch
        {
            CheckKind.Contains => $"contains {JoinOr(Args)}",
            CheckKind.Has => $"has {JoinOr(Args)}",
            CheckKind.Uses => $"uses {JoinOr(Args)}",
            CheckKind.Requires => $"requires {JoinOr(Args)}",

            CheckKind.Is =>
                Args.Length == 0
                    ? "the two sides are the same kind"
                    : $"{Args[0]} is a kind of {Args[1]}",

            CheckKind.Equals =>
                Args.Length == 0
                    ? "the two sides are equal"
                    : $"{Args[0]} equals {Args[1]}",

            CheckKind.Compares =>
                Args.Length == 0
                    ? "the two sides are similar"
                    : $"{Args[0]} compares to {Args[1]}",

            CheckKind.Contrasts =>
                Args.Length == 0
                    ? "the two sides are different"
                    : $"{Args[0]} contrasts with {Args[1]}",

            CheckKind.Before => $"{Args[0]} comes before {Args[1]}",
            CheckKind.After => $"{Args[0]} comes after {Args[1]}",
            CheckKind.During => $"{Args[0]} happens during {Args[1]}",
            CheckKind.Between => $"{Args[0]} is between {Args[1]} and {Args[2]}",

            CheckKind.DependsOn => $"{Args[0]} depends on {Args[1]}",
            CheckKind.Affects => $"{Args[0]} affects {Args[1]}",
            CheckKind.Causes => $"{Args[0]} causes {Args[1]}",
            CheckKind.ResultsIn => $"{Args[0]} results in {Args[1]}",

            CheckKind.Changes => $"{Args[0]} changes from {Args[1]} to {Args[2]}",

            CheckKind.Remains =>
                Args.Length == 1
                    ? $"{Args[0]} remains the same"
                    : $"{Args[0]} remains {Args[1]}",

            CheckKind.Internal => $"{Args[0]} is internal to {Args[1]}",
            CheckKind.External => $"{Args[0]} is external to {Args[1]}",

            _ => $"{Kind}"
        };
    }

    private static string JoinOr(string[] items)
    {
        return items.Length switch
        {
            0 => "",
            1 => items[0],
            _ => string.Join(" or ", items)
        };
    }
}
