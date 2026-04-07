using System.Runtime.InteropServices;
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
		bool over = false;

		while (!over)
		{
			input = Parse<int>("Digite o numero correspondente para selecionar uma opção\n" +
				"1 - Listar;\n" +
				"2 - Cadastrar;\n" +
				"3 - Salvar;\n" +
				"4 - Sair;\n" +
				"->"
			);

			switch (input)
			{
				// TODO: A implementar
				case 1: break;

				case 2:
					// Segunda tela começa
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
							string nome_aluno = Console.ReadLine()!;

							int idade = (int)Parse<uint>("Informe a idade do(a) aluno(a): ", "Idade deve ser um numero natural!!!");

							// TODO: Confirmar ncessidade de validação a parte da interna da função Parse<T>
							/*
							while ( idade < 0)
							{
								Console.WriteLine("Idade deve ser um numero natural!!!");
								idade = Parse<int>("Informe a idade do(a) aluno(a): ");
							} */

							int matricula;
							bool clone = false;
							do
							{
								clone = false;
								matricula = Parse<int>($"Informe um codigo de matricula para {nome_aluno}: ", "Matricula deve ser um numero!!!");

								foreach (Aluno aluno in Alunos.GetData())
								{
									if (aluno.getMatricula() == matricula)
									{
										Console.WriteLine("Matricula repetida!!!");
										clone = true;
									}
								}
							} while (clone);

							Alunos.Add(new(nome_aluno, idade, matricula));
							OrderAlunos(ref Alunos);
							break;

						// Em implementação
						case 2:
							bool cod_repetido = false;
							string nome_materia;
							double nota_min;
							int codigo;

							Console.Write("Digite o nome da materia: ");
							nome_materia = Console.ReadLine()!;
							nota_min = Parse<double>("Informe a nota minima para a materia: ");

							do
							{
								codigo = Parse<int>("Informe o codigo da materia: ");

								foreach (Materia materia in Materias.GetData())
								{
									if (materia.getCodigo() == codigo)
									{
										Console.WriteLine("Codigo repetido!!!");
										cod_repetido = true;
										break;
									}
								}
							} while (!cod_repetido);

							Materias.Add(new(nome_materia, nota_min, codigo));

							OrderMaterias(ref Materias);
							break;

						// TODO: A implementar
						case 3: break;

						// TODO: A implementar
						case 4: break;

						// Só isso mesmo
						case 5: break;

						default:
							Console.WriteLine("Entrada inválida!");
							break;
					}
					// Segunda tela acaba
					break;

				case 3:
					using (StreamWriter sw = new(Global.AlunoDataPath))
					{
						foreach (Aluno aluno in Alunos.GetData())
							sw.WriteLine($"{aluno.getNome()};{aluno.getIdade()}");
					}

					using (StreamWriter sw = new(Global.MateriaDataPath))
					{
						foreach (Materia mat in Materias.GetData())
							sw.WriteLine($"{mat.getNome()};{mat.getNotaMin()}");
					}

					using (StreamWriter sw = new(Global.MateriaDataPath))
					{
						foreach (Materia mat in Materias.GetData())
							sw.WriteLine($"{mat.getNome()};{mat.getNotaMin()}");
					}
					break;

				case 4:
					over = true;
					break;

				default:
					Console.WriteLine("Entrada inválida!!!");
					break;
			}
		}
	}
}
