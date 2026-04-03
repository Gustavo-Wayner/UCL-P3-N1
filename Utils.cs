public static class Misc
{
	public enum State
	{
		Aprovado,
		Reprovado,
		ADeterminar
	}

	/// <summary>
	/// Converte uma entrada do usuário para qualquer tipo T que implmente a função TryParse()
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="prompt">Mensagem a ser mostrada ao usuário antes da entrada</param>
	/// <param name="error">Mensagem a ser mostrada em caso de entrada inválida. Valor padrão: "Entrada inválida!"</param>
	/// <returns><typeparamref name="T"/></returns>
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

	/// <summary>
	/// Metodo para converter um State em string
	/// </summary>
	/// <param name="state">Estado a ser convertido</param>
	/// <returns>string</returns>
	/// <exception cref="NotImplementedException">Retorna um erro para um estado nulo ou indefinido</exception>
	public static string ToLabel(State state) => state switch
	{
		State.Aprovado => "Aprovado",
		State.Reprovado => "Reprovado",
		State.ADeterminar => "A determinar",
		_ => throw new NotImplementedException()
	};
}