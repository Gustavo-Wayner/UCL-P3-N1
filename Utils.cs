using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UCL_P3_N1;
public static class Misc
{
	public enum State
	{
		Aprovado,
		Reprovado,
		ADeterminar
	}

	public static Aluno[] GetAlunosByMatricula(ref Vetor<Aluno> ALUNOS, int matricula)
	{
		Aluno[] Alunos = ALUNOS.GetData();
		return Alunos.Where(x => x.getMatricula() == matricula).ToArray();
	}

	public static Materia[] GetMateriasByCodigo(ref Vetor<Materia> MATERIAS, int codigo)
	{
		Materia[] Materias = MATERIAS.GetData();
		return Materias.Where(x => x.getCodigo() == codigo).ToArray();
	}


	/// <summary>
	/// Busca um aluno pelo nome ou matrícula no vetor de alunos.
	/// </summary>
	/// <param name="ALUNOS">O vetor de alunos.</param>
	/// <returns>O aluno encontrado.</returns>
	public static Aluno SearchAluno(ref Vetor<Aluno> ALUNOS)
	{
		Aluno[] Alunos = ALUNOS.GetData();

		Console.WriteLine("Informe o nome do aluno:");
		string nome_aluno = Console.ReadLine()!;
		Aluno aluno = new("", 0, 0);

		bool found_aluno = false;
		while (!found_aluno)
		{
			Aluno[] alunos_achados = Alunos.Where(x => x.getNome() == nome_aluno).ToArray();
			if (alunos_achados.Length == 0)
			{
				Console.WriteLine($"Aluno com nome {nome_aluno} não encontrado!");
				Console.WriteLine("Informe o nome do aluno:");
				nome_aluno = Console.ReadLine()!;
			}
			else if (alunos_achados.Length > 1)
			{
				Console.WriteLine($"Mais de um aluno encontrado com o nome {nome_aluno}!");
				int mat_aluno = Parse<int>("Informe a matrícula do aluno: ");

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
	public static Materia SearchMateria(ref Vetor<Materia> MATERIAS)
	{
		Materia[] Materias = MATERIAS.GetData();
		Materia materia = default!;

		bool found_materia = false;

		Console.WriteLine("Informe o nome da materia em que deseja matricular o aluno:");
		string nome_materia = Console.ReadLine()!;

		while (!found_materia)
		{
			Materia[] materias_achadas = Materias.Where(x => x.getNome() == nome_materia).ToArray();
			if (materias_achadas.Length == 0)
			{
				Console.WriteLine("Materia não encontrada!");
				Console.WriteLine("Informe o nome da materia em que deseja matricular o aluno:");
				nome_materia = Console.ReadLine()!;
			}
			else if (materias_achadas.Length > 1)
			{
				Console.WriteLine($"Mais de uma materia chamada {nome_materia} encontrada!");
				while (true)
				{
					int cod_materia = Parse<int>("Informe o código da materia: ");
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

	/// <summary>
	/// Lê o conteudo do arquivo Alunos.dat, e o usa para popular um Vetor&lt;Aluno&gt;; Se vazio ou inexistente o arquivo .dat, retorna um Vetor vazio e cria o arquivo
	/// </summary>
	/// <returns></returns>
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
					for (int col = 0; col < parts.Length; col++)
					{
						if (string.IsNullOrEmpty(parts[col]) || string.IsNullOrWhiteSpace(parts[col]))
						{
							Console.WriteLine($"A entrada da coluna {col + 1} linha {lineNumber} está vazia. Favor concertar antes de prosseguir");
							throw new Exception("EntradaFaltando");
						}
					}
					if (parts.Length == 3)
					{
						// Erro para idade negativa
						if (int.TryParse(parts[1], out int idade))
						{
							if (idade < 0)
							{
								Console.WriteLine($"O valor da idade de {parts[0]} em alunos.dat (segunda coluna, linha {lineNumber}) não é um numero natural. Favor corrigir antes de prosseguir");
								throw new Exception("IdadeNegativaNaoPodeFi");
							}
						}

						// Erro para valor em idade que não pode ser convertida em numero
						else
						{
							Console.WriteLine($"O valor da idade de {parts[0]} em alunos.dat (segunda coluna, linha {lineNumber}) não é um numero natural. Favor corrigir antes de prosseguir");
							throw new Exception("IdadeNaoENumeroNatural");
						}

						// Erro para matricula que não pode ser convertido em numero
						if (!int.TryParse(parts[2], out int matricula))
						{
							Console.WriteLine($"O valor da matrícula de {parts[0]} em alunos.dat (terceira coluna, linha {lineNumber}) não é um numero. Favor corrigir antes de prosseguir");
							throw new Exception("MatriculaNaoENumero");
						}

						alunos.Add(new Aluno(parts[0], idade, matricula));

					}
					else
					{
						Console.WriteLine($"A entrada da linha {lineNumber} do arquivo alunos.dat está incompleta");

						throw new Exception("EntradaFaltando");
					}


					lineNumber++;
				}
			}
		}
		OrderAlunos(ref alunos);

		return alunos;
	}

	public static Vetor<Materia> LerMateriasDoDat()
	{
		int lineNumber = 1;
		Vetor<Materia> mat = new Vetor<Materia>();

		if (File.Exists(Global.AlunoDataPath))
		{
			using (StreamReader reader = new(Global.AlunoDataPath))
			{
				string line;
				while ((line = reader.ReadLine()!) != null)
				{
					string[] parts = line.Split(';');
					if (parts.Length == 3)
					{
						double nota_min;

						// erro para a nota minima estar fora do range de 0-10
						if (double.TryParse(parts[1], out nota_min))
						{
							if (nota_min < 0 || nota_min > 10)
							{
								Console.WriteLine($"Nota minima (segunda coluna) da materia {parts[0]}, linha {lineNumber} do arquivo materias.dat não está entre 0 e 10!!!");
								throw new Exception("NotaMinimaMenorQue0OuMaiorQue10");
							}
						}

						// Erro para nota minima que não pode ser convertida em numero real positivo
						else
						{
							Console.WriteLine($"Nota minima (segunda coluna) da materia {parts[0]}, linha {lineNumber} do arquivo materias.dat não pôde ser convertido para um numero!!!");
							throw new Exception("NotaMinimaNaoENumero");
						}

						// Erro para o código não ser um numero
						if (!int.TryParse(parts[2], out int codigo))
						{
							Console.WriteLine($"O valor do código da materia de {parts[0]} em materias.dat (terceira coluna, linha {lineNumber}) não é um numero. Favor corrigir antes de prosseguir");
							throw new Exception("CodigoNaoENumero");
						}

						mat.Add(new Materia(parts[0], nota_min, codigo));
					}

					lineNumber++;
				}
			}
		}

		OrderMaterias(ref mat);
		return mat;
	}

	public static Vetor<Matricula> LerMatriculasDoDat()
	{
		Materia[] materias = LerMateriasDoDat().GetData();
		Aluno[] alunos = LerAlunosDoDat().GetData();

		int lineNumber = 1;
		Vetor<Matricula> mat = new Vetor<Matricula>();

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
						mat.Add(new Matricula(parts[0], parts[1]));
					}

					lineNumber++;
				}
			}
		}

		OrderMatriculas(ref mat);
		return mat;
	}

	public static void OrderAlunos(ref Vetor<Aluno> alunos)
	{
		Aluno[] data = alunos.GetData();
		alunos = data.OrderBy(x => x.getNome()).ToArray();
	}

	public static void OrderMaterias(ref Vetor<Materia> materia)
	{
		Materia[] data = materia.GetData();
		materia = data.OrderBy(x => x.getNome()).ToArray();
	}

	public static void OrderMatriculas(ref Vetor<Matricula> matricula)
	{
		Matricula[] data = matricula.GetData();
		matricula = data.OrderBy(x => x.GetMateria().getNome()).ThenBy(y => y.GetAluno().getNome()).ToArray();
	}
}
