using Funcular.DotNetCore.IdGenerators;

namespace FastFrame.Infrastructure
{
    public class IdGenerate
    {
        private static readonly Base36IdGenerator generator = new(
               numTimestampCharacters: 12,
               numServerCharacters: 6,
               numRandomCharacters: 7,
               reservedValue: "",
               delimiter: "-",
               delimiterPositions: new[] { 20, 15, 10, 5 });

        public static string NetId() => generator.NewId().ToLower();

        public static long NetLongId() => Snowflake.GetId();
    }

}
