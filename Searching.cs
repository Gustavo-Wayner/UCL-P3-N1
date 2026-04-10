using UCL_P3_N1;

public static class Searching
{
	/// <summary>
	/// Busca um aluno pelo nome ou matrícula no vetor de alunos.
	/// </summary>
	/// <param name="ALUNOS">O vetor de alunos.</param>
	/// <returns>O aluno encontrado.</returns>
	public static Aluno? SearchAluno(ref Vetor<Aluno> ALUNOS)
	{
		Aluno[] Alunos = ALUNOS.GetData();

		if (Alunos.Length == 0)
		{
			Console.WriteLine("Nenhum aluno cadastrado!");
			return null;
		}

		Console.Write("Informe o nome do aluno (ou digite '0' para voltar): ");
		string nome_aluno = Console.ReadLine()!;
		if (nome_aluno == "0") return null;

		Aluno aluno = null!;

		bool found_aluno = false;
		while (!found_aluno)
		{
			Aluno[] alunos_achados = Alunos.Where(x => x.getNome() == nome_aluno).ToArray();
			if (alunos_achados.Length == 0)
			{
				Console.WriteLine($"Aluno com nome {nome_aluno} não encontrado!");
				Console.Write("Informe o nome do aluno (ou digite '0' para voltar): ");
				nome_aluno = Console.ReadLine()!;
				if (nome_aluno == "0") return null;
			}
			else if (alunos_achados.Length > 1)
			{
				Console.WriteLine($"Mais de um aluno encontrado com o nome {nome_aluno}!");
				Console.Write("Informe a matrícula do aluno (ou digite '0' para voltar): ");
				string mat_input = Console.ReadLine()!;
				if (mat_input == "0") return null;

				int mat_aluno = int.Parse(mat_input);

				if (alunos_achados.Any(x => x.getMatricula() == mat_aluno))
				{
					nome_aluno = alunos_achados.First(x => x.getMatricula() == mat_aluno).getNome();

					aluno = alunos_achados.First(x => x.getMatricula() == mat_aluno);
					found_aluno = true;
				}
				else
					Console.WriteLine("Matrícula não encontrada!");
			}
			else
			{
				aluno = alunos_achados[0];
				found_aluno = true;
			}
		}

		return aluno;
	}

	/// <summary>
	/// Busca uma materia pelo nome ou codigo no vetor de materias.
	/// </summary>
	/// <param name="MATERIAS">O vetor de materias.</param>
	/// <returns>A materia encontrada.</returns>
	public static Materia? SearchMateria(ref Vetor<Materia> MATERIAS)
	{
		Materia[] Materias = MATERIAS.GetData();

		if (Materias.Length == 0)
		{
			Console.WriteLine("Nenhuma matéria cadastrada!");
			return null;
		}

		Materia materia = null!;
		bool found_materia = false;

		Console.Write("Informe o nome da materia (ou digite '0' para voltar): ");
		string nome_materia = Console.ReadLine()!;
		if (nome_materia == "0") return null;

		while (!found_materia)
		{
			Materia[] materias_achadas = Materias.Where(x => x.getNome() == nome_materia).ToArray();
			if (materias_achadas.Length == 0)
			{
				Console.WriteLine("Materia não encontrada!");
				Console.Write("Informe o nome da materia (ou digite '0' para voltar): ");
				nome_materia = Console.ReadLine()!;
				if (nome_materia == "0") return null;
			}
			else if (materias_achadas.Length > 1)
			{
				Console.WriteLine($"Mais de uma materia chamada {nome_materia} encontrada!");
				while (true)
				{
					Console.Write("Informe o código da materia (ou digite '0' para voltar): ");
					string cod_input = Console.ReadLine()!;
					if (cod_input == "0") return null;

					int cod_materia = int.Parse(cod_input);
					if (materias_achadas.Any(x => x.getCodigo() == cod_materia))
					{
						Materia materia_selecionada = materias_achadas.First(x => x.getCodigo() == cod_materia);
						nome_materia = materia_selecionada.getNome();

						materia = materias_achadas[0];
						found_materia = true;
						break;
					}
					else
					{
						Console.WriteLine($"Materia com código {cod_materia} não encontrada!");
					}
				}
			}
			else
			{
				materia = materias_achadas[0];
				found_materia = true;
			}
		}

		return materia;
	}

	public static Aluno[] GetAlunosByMatricula(ref Aluno[] ALUNOS, int matricula)
	{
		return ALUNOS.Where(x => x.getMatricula() == matricula).ToArray();
	}

	public static Materia[] GetMateriasByCodigo(ref Materia[] MATERIAS, int codigo)
	{
		return MATERIAS.Where(x => x.getCodigo() == codigo).ToArray();
	}
}
