using UCL_P3_N1;

public static class Misc
{
	/// <summary>
	/// Converte uma entrada do usuário para qualquer tipo T que implmente a função TryParse()
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="prompt">Mensagem a ser mostrada ao usuário antes da entrada</param>
	/// <param name="error">Mensagem a ser mostrada em caso de entrada inválida. Valor padrão: "Entrada inválida!"</param>
	/// <returns><typeparamref name="T"/></returns>
	public static T Parse<T>(string prompt, string error = "Entrada inválida!", bool clearConsole = false)
	where T : IParsable<T>
	{
		while (true)
		{
			Console.Write(prompt);
			if (T.TryParse(Console.ReadLine()!.Trim(), null, out T? output)) return output;
			if (clearConsole)
				Console.Clear();
			Console.WriteLine(error);
		}
	}

	public static T FilterFirst<T>(T[] original, Func<T, bool> filter)
	{
		foreach (T item in original)
		{
			if (filter(item))
				return item;
		}

		throw new Exception("NenhumItemPresenteQueCumpraOsRequisitos");
	}

	public static T FilterFirst<T>(Vetor<T> original, Func<T, bool> filter)
	{
		foreach (T item in original.Data!)
		{
			if (filter(item))
				return item;
		}

		throw new Exception("NenhumItemPresenteQueCumpraOsRequisitos");
	}

	public static bool Any<T>(T[] original, Func<T, bool> filter)
	{
		foreach (T item in original)
		{
			if (filter(item))
				return true;
		}

		return false;
	}

	public static bool Any<T>(Vetor<T> original, Func<T, bool> filter)
	{
		foreach (T item in original.Data!)
		{
			if (filter(item))
				return true;
		}

		return false;
	}

	public static T[] FilterAll<T>(T[] original, Func<T, bool> filter)
	{
		if (original == null) return [];
		Vetor<T> ret = new();
		foreach (T item in original)
		{
			if (filter(item))
				ret.Add(item);
		}

		return ret.Data!;
	}

	public static T[] FilterAll<T>(Vetor<T> original, Func<T, bool> filter)
	{
		if (original == null) return [];
		Vetor<T> ret = new();
		foreach (T item in original.Data!)
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
