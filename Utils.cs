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
					if (parts.Length == 3)
					{
						// Erro para idade negativa
						if ( int.TryParse(parts[1], out int idade ) )
						{
							if ( idade < 0 )
							{
								Console.WriteLine($"O valor da idade de {parts[0]} em alunos.dat (segunda coluna, linha {lineNumber}) não é um numero natural. Favor corrigir antes de prosseguir");
								throw new Exception("IdadeNegativaNaoPodeFi");
							}
						}

						// Erro para valor em idade que não pode ser convertido em numero
						else
						{
							Console.WriteLine($"O valor da idade de {parts[0]} em alunos.dat (segunda coluna, linha {lineNumber}) não é um numero natural. Favor corrigir antes de prosseguir");
							throw new Exception("IdadeNaoENumeroNatural");
						}

						alunos.Add(new Aluno(parts[0], idade));
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
					if (parts.Length == 2)
					{
						mat.Add(new Materia(parts[0], parts[1]));
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
						mat.Add(new Materia(parts[0], parts[1]));
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
		alunos = data.OrderBy( x => x.getNome() ).ToArray();
	}

	public static void OrderMaterias(ref Vetor<Materia> materia)
	{
		Materia[] data = materia.GetData();
		materia = data.OrderBy( x => x.getNome() ).ToArray();
	}

	public static void OrderMatriculas(ref Vetor<Matricula> matricula)
	{
		Matricula[] data = matricula.GetData();
		matricula = data.OrderBy( x => x.GetMateria().getNome() ).ThenBy( y => y.GetAluno().getNome() ).ToArray();
	}
}