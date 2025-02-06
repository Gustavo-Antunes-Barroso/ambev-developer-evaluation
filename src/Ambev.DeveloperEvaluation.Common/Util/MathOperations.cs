namespace Ambev.DeveloperEvaluation.Common.Util
{
    public static class MathOperations
    {
        public static decimal RoundDecimalWithTwoPlaces(decimal value)
            => decimal.Round(value, 2, MidpointRounding.AwayFromZero);
    }
}
