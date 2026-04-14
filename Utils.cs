using UCL_P3_N1;

public static class Misc
{
	/// <summary>
	/// Converte uma entrada do usuário para qualquer tipo T que implmente a função TryParse()
	/// </summary>
	/// <typeparam name="TParsear"></typeparam>
	/// <param name="prompt">Mensagem a ser mostrada ao usuário antes da entrada</param>
	/// <param name="error">Mensagem a ser mostrada em caso de entrada inválida. Valor padrão: "Entrada inválida!"</param>
	/// <returns><typeparamref name="TParsear"/></returns>
	public static TParsear Parse<TParsear>(string prompt, string error = "Entrada inválida!", bool clearConsole = false)
	where TParsear : IParsable<TParsear>
	{
		while (true)
		{
			Console.Write(prompt);
			if (TParsear.TryParse(Console.ReadLine()!.Trim(), null, out TParsear? output)) return output;
			if (clearConsole)
				Console.Clear();
			Console.WriteLine(error);
		}
	}

	/// <summary>
	/// Coleta o primeiro valore de um Vetor que cumprir com um dado critério
	/// </summary>
	/// <typeparam name="TFiltrar"></typeparam>
	/// <param name="original">Vetor base</param>
	/// <param name="filter">Critério</param>
	/// <returns>O vetor com apénas os itens que cumprem o critério</returns>
	public static TFiltrar FilterFirst<TFiltrar>(TFiltrar[] original, Func<TFiltrar, bool> filter)
	{
		foreach (TFiltrar item in original)
		{
			if (filter(item))
				return item;
		}

		throw new Exception("NenhumItemPresenteQueCumpraOsRequisitos");
	}

	/// <summary>
	/// Coleta o primeiro valore de um Vetor que cumprir com um dado critério
	/// </summary>
	/// <typeparam name="TFiltrar"></typeparam>
	/// <param name="original">Vetor base</param>
	/// <param name="filter">Critério</param>
	/// <returns>O vetor com apénas os itens que cumprem o critério</returns>
	public static TFiltrar FilterFirst<TFiltrar>(Vetor<TFiltrar> original, Func<TFiltrar, bool> filter)
	{
		foreach (TFiltrar item in original.Data!)
		{
			if (filter(item))
				return item;
		}

		throw new Exception("NenhumItemPresenteQueCumpraOsRequisitos");
	}

	/// <summary>
	/// Pergunta se há algum objeto no vetor que cumpra com um dado critério
	/// </summary>
	/// <typeparam name="TFiltrar"></typeparam>
	/// <param name="original">Vetor base</param>
	/// <param name="filter">Critério</param>
	/// <returns>true se houver, do contrário false</returns>
	public static bool Any<TFiltrar>(TFiltrar[] original, Func<TFiltrar, bool> filter)
	{
		foreach (TFiltrar item in original)
		{
			if (filter(item))
				return true;
		}

		return false;
	}

	/// <summary>
	/// Pergunta se há algum objeto no vetor que cumpra com um dado critério
	/// </summary>
	/// <typeparam name="TFiltrar"></typeparam>
	/// <param name="original">Vetor base</param>
	/// <param name="filter">Critério</param>
	/// <returns>true se houver, do contrário false</returns>
	public static bool Any<TFiltrar>(Vetor<TFiltrar> original, Func<TFiltrar, bool> filter)
	{
		foreach (TFiltrar item in original.Data!)
		{
			if (filter(item))
				return true;
		}

		return false;
	}

	/// <summary>
	/// Coleta todos os valores de um Vetor que cumprirem com um dado critério
	/// </summary>
	/// <typeparam name="TFiltrar"></typeparam>
	/// <param name="original">Vetor base</param>
	/// <param name="filter">Critério</param>
	/// <returns>O vetor com apénas os itens que cumprem o critério</returns>
	public static Filtrar[] FilterAll<Filtrar>(Filtrar[] original, Func<Filtrar, bool> filter)
	{
		if (original == null) return [];
		Vetor<Filtrar> ret = new();
		foreach (Filtrar item in original)
		{
			if (filter(item))
				ret.Add(item);
		}

		return ret.Data!;
	}

	/// <summary>
	/// Coleta todos os valores de um Vetor que cumprirem com um dado critério
	/// </summary>
	/// <typeparam name="TFiltrar"></typeparam>
	/// <param name="original">Vetor base</param>
	/// <param name="filter">Critério</param>
	/// <returns></returns>
	public static TFiltrar[] FilterAll<TFiltrar>(Vetor<TFiltrar> original, Func<TFiltrar, bool> filter)
	{
		if (original == null) return [];
		Vetor<TFiltrar> ret = new();
		foreach (TFiltrar item in original.Data!)
		{
			if (filter(item))
				ret.Add(item);
		}

		return ret.Data!;
	}

	/// <summary>
	/// Converte uma string para title case (Primeira letra de cada palavra maiúscula)
	/// </summary>
	/// <param name="input">string de entrada</param>
	/// <returns>string de entrada em title case</returns>
	public static string? ToTitleCase(string? input)
	{
		if (string.IsNullOrWhiteSpace(input))
			return null;
		string output = string.Empty;
		input = input.Trim();

		output += char.ToUpper(input[0]);
		for (int i = 1; i < input.Length; i++)
			output += input[i - 1] == ' ' ? char.ToUpper(input[i]) : char.ToLower(input[i]);

		return output;
	}

	/// <summary>
	/// Ordena os alunos por nome
	/// </summary>
	/// <param name="alunos">Vetor de alunos a ser ordenado</param>
	public static void OrderAlunos(ref Vetor<Aluno> alunos)
	{
		Aluno[] data = alunos.Data!;
		alunos = data.OrderBy(x => x.Nome).ToArray();
	}

	/// <summary>
	/// Ordena as matérias por nome
	/// </summary>
	/// <param name="materia">Vetor de matérias a ser ordenado</param>
	public static void OrderMaterias(ref Vetor<Materia> materia)
	{
		Materia[] data = materia.Data!;
		materia = data.OrderBy(x => x.Nome).ToArray();
	}

	/// <summary>
	/// Ordena as matrículas por nome da matéria e nome do aluno
	/// </summary>
	/// <param name="matricula">Vetor de matrículas a ser ordenado</param>
	public static void OrderMatriculas(ref Vetor<Matricula> matricula)
	{
		Matricula[] data = matricula.Data!;
		matricula = data.OrderBy(x => x._Materia.Nome).ThenBy(y => y._Aluno.Nome).ToArray();
	}
}
