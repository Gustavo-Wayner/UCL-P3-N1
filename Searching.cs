using UCL_P3_N1;
using static UCL_P3_N1.Program;
using static Misc;

public static class Searching
{
	/// <summary>
	/// Busca um aluno pelo nome ou matrícula no vetor de alunos.
	/// </summary>
	/// <returns>O aluno encontrado.</returns>
	public static Aluno? SearchAluno()
	{
		Aluno? pesquisa = null;

		while (true)
		{
			Console.Write("Digite o nome ou matrícula do(a) aluno(a) (0 para voltar): ");
			string? entrada = ToTitleCase(Console.ReadLine());

			if (entrada == "0" || _Alunos.Data == null) return null;
			if (int.TryParse(entrada, out int mat))
			{
				if (!_Alunos.Data.Any(x => x.Matricula == mat))
				{
					Console.WriteLine($"Nenhum(a) aluno(a) de matrícula {mat} encontrado! Tente novamente");
					continue;
				}
				pesquisa = _Alunos.Data.First(x => x.Matricula == mat);
				return pesquisa;
			}
			Aluno[] Possiveis = _Alunos.Data.Where(x => x.Nome == entrada).ToArray();
			if (Possiveis.Length == 0)
			{
				Console.WriteLine($"Nenhum(a) {entrada} encontrado(a)! Tente novamente");
				continue;
			}
			else if (Possiveis.Length > 1)
			{
				Console.WriteLine($"Mais de um(a) {entrada} encontrado(a). Pesquise por matrícula");

				while (true)
				{
					int Mat = Parse<int>($"Digite a matrícula de {entrada} (0 para sair): ");
					if (Mat == 0) return null;

					if (!Possiveis.Any(x => x.Matricula == Mat))
					{
						Console.WriteLine($"Nenhum(a) {entrada} de matrícula {Mat} encontrado(a)! Tente novamente");
						continue;
					}
					pesquisa = _Alunos.Data.First(x => x.Matricula == Mat);
					return pesquisa;
				}
			}

			pesquisa = _Alunos.Data.First(x => x.Nome == entrada);
			return pesquisa;
		}
	}

	/// <summary>
	/// Busca uma materia pelo nome ou codigo no vetor de materias.
	/// </summary>
	/// <param name="MATERIAS">O vetor de materias.</param>
	/// <returns>A materia encontrada.</returns>
	public static Materia? SearchMateria()
	{
		Materia? pesquisa = null;

		while (true)
		{
			Console.Write("Digite o nome ou código da matéria (0 para voltar): ");
			string? entrada = ToTitleCase(Console.ReadLine());

			if (entrada == "0" || _Materias.Data == null) return null;
			if (int.TryParse(entrada, out int cod))
			{
				if (!_Materias.Data.Any(x => x.Codigo == cod))
				{
					Console.WriteLine($"Nenhuma matéria de código {cod} encontrada! Tente novamente");
					continue;
				}
				pesquisa = _Materias.Data.First(x => x.Codigo == cod);
				return pesquisa;
			}
			Materia[] Possiveis = _Materias.Data.Where(x => x.Nome == entrada).ToArray();
			if (Possiveis.Length == 0)
			{
				Console.WriteLine($"Nenhuma matéria de nome {entrada} encontrada! Tente novamente");
				continue;
			}
			else if (Possiveis.Length > 1)
			{
				Console.WriteLine($"Mais de uma matéria de nome {entrada} encontrada. Pesquise por código");

				while (true)
				{
					int Cod = Misc.Parse<int>($"Digite a matrícula de {entrada} (0 para sair): ");
					if (Cod == 0) return null;

					if (!Possiveis.Any(x => x.Codigo == Cod))
					{
						Console.WriteLine($"Nenhuma matéria de nome {entrada} e de código {Cod} encontrada! Tente novamente");
						continue;
					}
					pesquisa = _Materias.Data?.First(x => x.Codigo == Cod);
					return pesquisa;
				}
			}

			pesquisa = _Materias.Data?.First(x => x.Nome == entrada);
			return pesquisa;
		}
	}

	public static Aluno[] GetAlunosByMatricula(ref Aluno[] ALUNOS, int matricula)
	{
		return ALUNOS.Where(x => x.Matricula == matricula).ToArray();
	}

	public static Materia[] GetMateriasByCodigo(ref Materia[] MATERIAS, int codigo)
	{
		return MATERIAS.Where(x => x.Codigo == codigo).ToArray();
	}
}
