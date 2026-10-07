namespace LocalAI.Core;

public record Check(CheckKind Kind, Quantifier Quant, params string[] Args)
{
  public string? Validate()
  {
    Returen Kind switch
    {
        _ => null,
    };
}
