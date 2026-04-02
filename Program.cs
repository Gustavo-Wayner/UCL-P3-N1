using System.Collections;

namespace UCL_P3_N1;

public static class Program
{
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

	public static void Main()
	{
		Vetor<Aluno> Alunos = LerAlunosDoDat();

		int input = 0;

		while ( input != 3)
		{
			input = Misc.Parse<int>("Digite o numero correspondente para selecionar uma opção\n" +
				"1 - Matricular aluno;\n" +
				"2 - Listar alunos;\n" +
				"3 - Saír;\n" +
				"->"
			);

			switch (input)
			{
				case 1:
					Console.Write("Informe o nome do(a) aluno(a): ");
					string nome = Console.ReadLine()!;

					Console.Write("Informe o cpf do(a) aluno(a): ");
					string cpf = Console.ReadLine()!;

					Alunos.Add(new (nome, cpf));
				break;

				case 2:
					Console.WriteLine(Alunos);
				break;

				case 3: break;

				default:
					Console.WriteLine( "Entrada inválida!" );
				break;
			}
		}

		using ( StreamWriter sw = new( Global.AlunoDataPath ) )
		{
			foreach (Aluno aluno in Alunos.GetData() )
				sw.WriteLine($"{aluno.getNome()};{aluno.getCpf()}");
		}
	}
}
