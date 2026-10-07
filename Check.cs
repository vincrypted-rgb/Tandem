namespace LocalAI.Core;

public record Check(CheckKind Kind, Quantifier Quant, params string[] Args)
{
  public string? Validate()
  {
    Return Kind switch
    {
        _ => null,
    };
}
