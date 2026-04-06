using static Misc;

namespace UCL_P3_N1;

public static class Program
{
	public static void Main()
	{
		Vetor<Aluno> Alunos = LerAlunosDoDat();
		Vetor<Materia> Materias = LerMateriasDoDat();
		Vetor<Matricula> Matriculas = LerMatriculasDoDat();

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

							int idade = (int)Parse<uint>("Informe o cpf do(a) aluno(a): ", "Idade deve ser um numero natural!!!");
/* 
							while ( idade < 0)
							{	
								Console.WriteLine("Idade deve ser um numero natural!!!");
								idade = Parse<int>("Informe o cpf do(a) aluno(a): ");
							} */

							Alunos.Add(new (nome, idade));
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

				case 2: break;
				case 3:
					using ( StreamWriter sw = new( Global.AlunoDataPath ) )
					{
						foreach (Aluno aluno in Alunos.GetData() )
							sw.WriteLine($"{aluno.getNome()};{aluno.getIdade()}");
					}

					using ( StreamWriter sw = new( Global.MateriaDataPath ) )
					{
						foreach (Materia mat in Materias.GetData() )
							sw.WriteLine($"{mat.getNome()};{mat.getNotaMin()}");
					}

					using ( StreamWriter sw = new( Global.MateriaDataPath ) )
					{
						foreach (Materia mat in Materias.GetData() )
							sw.WriteLine($"{mat.getNome()};{mat.getNotaMin()}");
					}
				break;
				case 4: break;
			}
		}

		using ( StreamWriter sw = new( Global.AlunoDataPath ) )
		{
			foreach (Aluno aluno in Alunos.GetData() )
				sw.WriteLine($"{aluno.getNome()};{aluno.getIdade()}");
		}
	}
}
