using UCL_P3_N1;
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

	public static Vetor<Aluno> LerAlunosDoDat()
	{
		int lineNumber = 1;
		Vetor<Aluno> alunos = new Vetor<Aluno>();

		if (File.Exists(Global.AlunoDataPath))
		{
			using (StreamReader reader = new(Global.AlunoDataPath))
			{
				string line;
				while ((line = reader.ReadLine()!) != null)
				{
					string[] parts = line.Split(';');
					if (parts.Length == 2)
					{
						if ( parts[1].Length != 11 )
							Console.WriteLine($"O cpf do aluno {parts[0]} na linha {lineNumber} do arquivo alunos.dat " + 
								"parece ter sido adulterado pois não tem 11 digitos. Favor concertar antes de prosseguir " +
								"com a execução do programa!!!"
						);
						alunos.Add(new Aluno(parts[0], parts[1]));
					}

					lineNumber++;
				}
			}
		}

		return alunos;
	}
}