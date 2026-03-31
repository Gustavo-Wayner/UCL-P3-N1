public static class Misc
{
	public enum State
	{
		Aprovado,
		Reprovado,
		ADeterminar
	}

	public static T Parse<T>(string prompt, string error = "Entrada inválida!")
    where T : IParsable<T>
    {
        while (true)
        {
            Console.Write(prompt);
            if (T.TryParse(Console.ReadLine(), null, out T? output)) return output;
            Console.WriteLine(error);
        }
    }
	public static string ToLabel(State state) => state switch
	{
		State.Aprovado => "Aprovado",
		State.Reprovado => "Reprovado",
		State.ADeterminar => "A determinar",
		_ => throw new NotImplementedException()
	};
}