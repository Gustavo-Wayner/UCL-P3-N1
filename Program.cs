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

						// TODO: Implementando
						case 3:
							Materia materia_matricula = SearchMateria(ref Materias);
							Aluno aluno_matricula = SearchAluno(ref Alunos);

							Matriculas.Add(new Matricula(ref aluno_matricula, ref materia_matricula));
							OrderMatriculas(ref Matriculas);

							break;

						// TODO: A implementar
						case 4:
							Matricula? matricula_selecionada = null;
							bool N1OrN2 = false;
							int N;
							do
							{
								N = Parse<int>("Deseja aplicar a nota a: \n" +
									"1 - N1\n" +
									"2 - N2\n" +
									"?->"
								);
								if (N == 1) N1OrN2 = true;
								else if (N == 2) N1OrN2 = true;
								else Console.WriteLine("Entrada inválida!");
							} while (!N1OrN2);


							Materia materia_nota = SearchMateria(ref Materias);
							Aluno aluno_nota = SearchAluno(ref Alunos);

							matricula_selecionada = Matriculas.GetData().Where(x => x.GetAluno().getMatricula() == aluno_nota.getMatricula() && x.GetMateria().getCodigo() == materia_nota.getCodigo()).FirstOrDefault();
							if (matricula_selecionada != null)
							{
								Console.WriteLine("Não ha uma matrícula para esse aluno nessa materia!");
								break;
							}

							double nota = Parse<double>("Informe a nota: ");

							if (N == 1)
							{
								matricula_selecionada!.SetN1(nota);
							}
							else if (N == 2)
							{
								matricula_selecionada!.SetN2(nota);
							}

							matricula_selecionada!.SetMedia((matricula_selecionada!.GetN1() + matricula_selecionada!.GetN2()) / 2);
							matricula_selecionada!.SetEstado(matricula_selecionada!.GetMedia() >= materia_nota.getNotaMin() ? Misc.State.Aprovado : Misc.State.Reprovado);

							break;

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
							sw.WriteLine($"{aluno.getNome()};{aluno.getIdade()};{aluno.getMatricula()}");
					}

					using (StreamWriter sw = new(Global.MateriaDataPath))
					{
						foreach (Materia mat in Materias.GetData())
							sw.WriteLine($"{mat.getNome()};{mat.getNotaMin()};{mat.getCodigo()}");
					}

					using (StreamWriter sw = new(Global.MateriaDataPath))
					{
						foreach (Matricula mat in Matriculas.GetData())
							sw.WriteLine($"{mat.GetAluno().getMatricula()};{mat.GetMateria().getCodigo()};{mat.GetN1()};{mat.GetN2()};{mat.GetMedia()};{mat.GetEstado()}");
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
