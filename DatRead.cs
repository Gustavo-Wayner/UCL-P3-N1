using UCL_P3_N1;

public static class DatRead
{
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
								throw new Exception("IdadeNegativaPodeNaoFi");
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
		Misc.OrderAlunos(ref alunos);

		return alunos;
	}

	/// <summary>
	/// Lê as matérias do arquivo materias.dat
	/// </summary>
	/// <returns>Vetor de matérias</returns>
	public static Vetor<Materia> LerMateriasDoDat()
	{
		int lineNumber = 1;
		Vetor<Materia> mat = new Vetor<Materia>();

		if (File.Exists(Global.MateriaDataPath))
		{
			using (StreamReader reader = new(Global.MateriaDataPath))
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

		Misc.OrderMaterias(ref mat);
		return mat;
	}

	/// <summary>
	/// Lê as matrículas do arquivo matriculas.dat
	/// </summary>
	/// <returns>Vetor de matrículas</returns>
	public static Vetor<Matricula> LerMatriculasDoDat()
	{
		Materia[] materias = Program._Materias.Data!;
		Aluno[] alunos = Program._Alunos.Data!;

		int lineNumber = 1;
		Vetor<Matricula> mat = new Vetor<Matricula>();

		if (File.Exists(Global.MatriculaDataPath))
		{
			using (StreamReader reader = new(Global.MatriculaDataPath))
			{
				string line;
				while ((line = reader.ReadLine()!) != null)
				{
					string[] parts = line.Split(';');
					if (parts.Length == 6)
					{
						Aluno[] ALN = Searching.GetAlunosByMatricula(ref alunos, int.Parse(parts[0]));
						if (ALN.Length > 1)
						{
							Console.WriteLine($"Mais de um aluno encontrado para a matrícula {parts[0]}. Favor corrigir antes de prosseguir (alunos.dat)");
							throw new Exception("MaisDeUmAlunoParaMatricula");
						}

						Materia[] MTR = Searching.GetMateriasByCodigo(ref materias, int.Parse(parts[1]));
						if (MTR.Length > 1)
						{
							Console.WriteLine($"Mais de uma matéria encontrada para o código {parts[1]}. Favor corrigir antes de prosseguir (materias.dat)");
							throw new Exception("MaisDeUmaMateriaParaCodigo");
						}

						Aluno aluno = ALN[0];
						Materia materia = MTR[0];
						double? n1 = parts[2] == "" ? null : double.Parse(parts[2]);
						double? n2 = parts[3] == "" ? null : double.Parse(parts[3]);
						double? media = parts[4] == "" ? null : double.Parse(parts[4]);
						mat.Add(new Matricula(ref aluno, ref materia, n1, n2, media, parts[5]));
					}
					else
					{
						Console.WriteLine($"Linha {lineNumber} inválida por conter mais ou menos de 6 campos. Favor corrigir antes de prosseguir (matriculas.dat)");
						throw new Exception("LinhaInvalida");
					}

					lineNumber++;
				}
			}
		}

		Misc.OrderMatriculas(ref mat);
		return mat;
	}
}
