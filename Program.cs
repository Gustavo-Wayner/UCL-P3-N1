using static Misc;

namespace UCL_P3_N1;

public static class Program
{
	public static void Main()
	{
		Vetor<Aluno> Alunos = LerAlunosDoDat();

		int input = 0;

		while ( input != 4)
		{
			input = Parse<int>("Digite o numero correspondente para selecionar uma opção\n" +
				"1 - Listar;\n" +
				"2 - Cadastrar;\n" +
				"3 - Salvar;\n" +
				"4 - Sair;\n" +
				"->"
			);

			switch(input)
			{
				case 1:
					input = Parse<int>("Digite o numero correspondente para selecionar uma opção\n" +
						"1 - Cadastrar aluno;\n" +
						"2 - Cadastrar materia;\n" +
						"3 - Matricular aluno em materia;\n" +
						"4 - Atribuir nota;\n" +
						"5 - Voltar;\n" +
						"->"
					);

					switch (input)
					{
						case 1:
							Console.Write("Informe o nome do(a) aluno(a): ");
							string nome = Console.ReadLine()!;

							Console.Write("Informe o cpf do(a) aluno(a): ");
							string cpf = Console.ReadLine()!;

							while ( cpf.Length != 11)
							{	
								Console.WriteLine("O cpf deve ter 11 digitos!!!");
								Console.Write("Informe o cpf do(a) aluno(a): ");
								cpf = Console.ReadLine()!;
							}

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
					input = 0;
				break;
			}
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
