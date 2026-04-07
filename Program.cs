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
							Console.WriteLine("Informe o nome da materia em que deseja matricular o aluno:");
							string nome_materia_matricula = Console.ReadLine()!;
							Materia materia_matricula = new("", 0, 0);

							bool found_materia = false;
							while (!found_materia)
							{
								Materia[] materias_achadas = Materias.GetData().Where(x => x.getNome() == nome_materia_matricula).ToArray();
								if (materias_achadas.Length == 0)
								{
									Console.WriteLine("Materia não encontrada!");
									Console.WriteLine("Informe o nome da materia em que deseja matricular o aluno:");
									nome_materia_matricula = Console.ReadLine()!;
								}
								else if (materias_achadas.Length > 1)
								{
									Console.WriteLine($"Mais de uma materia chamada {nome_materia_matricula} encontrada!");
									while (true)
									{
										int cod_materia_matricula = Parse<int>("Informe o código da materia: ");
										if (materias_achadas.Any(x => x.getCodigo() == cod_materia_matricula))
										{
											Materia materia_selecionada = materias_achadas.First(x => x.getCodigo() == cod_materia_matricula);
											nome_materia_matricula = materia_selecionada.getNome();

											materia_matricula = materias_achadas[0];
											found_materia = true;
											break;
										}
										else
										{
											Console.WriteLine($"Materia com código {cod_materia_matricula} não encontrada!");
										}
									}
								}
								else
								{
									materia_matricula = materias_achadas[0];
									found_materia = true;
								}
							}

							Console.WriteLine("Informe o nome do aluno:");
							string nome_aluno_matricula = Console.ReadLine()!;
							Aluno aluno_matricula = new("", 0, 0);

							bool found_aluno = false;
							while (!found_aluno)
							{
								Aluno[] alunos_achados = Alunos.GetData().Where(x => x.getNome() == nome_aluno_matricula).ToArray();
								if (alunos_achados.Length == 0)
								{
									Console.WriteLine($"Aluno com nome {nome_aluno_matricula} não encontrado!");
									Console.WriteLine("Informe o nome do aluno:");
									nome_aluno_matricula = Console.ReadLine()!;
								}
								else if (alunos_achados.Length > 1)
								{
									Console.WriteLine($"Mais de um aluno encontrado com o nome {nome_aluno_matricula}!");
									int mat_aluno = Parse<int>("Informe a matrícula do aluno: ");

									if (alunos_achados.Any(x => x.getMatricula() == mat_aluno))
									{
										nome_aluno_matricula = alunos_achados.First(x => x.getMatricula() == mat_aluno).getNome();

										aluno_matricula = alunos_achados.First(x => x.getMatricula() == mat_aluno);
										found_aluno = true;
									}
									else
										Console.WriteLine("Matrícula não encontrada!");
								}
								else
								{
									aluno_matricula = alunos_achados[0];
									found_aluno = true;
								}
							}

							Matriculas.Add(new Matricula(ref aluno_matricula, ref materia_matricula));
							OrderMatriculas(ref Matriculas);

							break;

						// TODO: A implementar
						case 4:
							bool N1OrN2 = false;
							do
							{
								int N = Parse<int>("Deseja aplicar a nota a: \n" +
									"1 - N1\n" +
									"2 - N2\n" +
									"?->"
								);
								if (N == 1) N1OrN2 = true;
								else if (N == 2) N1OrN2 = true;
								else Console.WriteLine("Entrada inválida!");
							} while (!N1OrN2);

							Console.WriteLine("Informe o nome da materia em que deseja matricular o aluno:");
							string nome_materia_nota = Console.ReadLine()!;
							Materia materia_nota = new("", 0, 0);

							bool found_materia_nota = false;
							while (!found_materia_nota)
							{
								Materia[] materias_achadas = Materias.GetData().Where(x => x.getNome() == nome_materia_nota).ToArray();
								if (materias_achadas.Length == 0)
								{
									Console.WriteLine("Materia não encontrada!");
									Console.WriteLine("Informe o nome da materia em que deseja matricular o aluno:");
									nome_materia_nota = Console.ReadLine()!;
								}
								else if (materias_achadas.Length > 1)
								{
									Console.WriteLine($"Mais de uma materia chamada {nome_materia_nota} encontrada!");
									while (true)
									{
										int cod_materia_nota = Parse<int>("Informe o código da materia: ");
										if (materias_achadas.Any(x => x.getCodigo() == cod_materia_nota))
										{
											Materia materia_selecionada = materias_achadas.First(x => x.getCodigo() == cod_materia_nota);
											nome_materia_nota = materia_selecionada.getNome();

											materia_nota = materias_achadas[0];
											found_materia_nota = true;
											break;
										}
										else
										{
											Console.WriteLine($"Materia com código {cod_materia_nota} não encontrada!");
										}
									}
								}
								else
								{
									materia_matricula = materias_achadas[0];
									found_materia = true;
								}
							}

							Console.WriteLine("Informe o nome do aluno:");
							string nome_aluno_nota = Console.ReadLine()!;
							Aluno aluno_nota = new("", 0, 0);

							bool found_aluno_nota = false;
							while (!found_aluno_nota)
							{
								Aluno[] alunos_achados = Alunos.GetData().Where(x => x.getNome() == nome_aluno_nota).ToArray();
								if (alunos_achados.Length == 0)
								{
									Console.WriteLine($"Aluno com nome {nome_aluno_nota} não encontrado!");
									Console.WriteLine("Informe o nome do aluno:");
									nome_aluno_nota = Console.ReadLine()!;
								}
								else if (alunos_achados.Length > 1)
								{
									Console.WriteLine($"Mais de um aluno encontrado com o nome {nome_aluno_nota}!");
									int mat_aluno = Parse<int>("Informe a matrícula do aluno: ");

									if (alunos_achados.Any(x => x.getMatricula() == mat_aluno))
									{
										nome_aluno_matricula = alunos_achados.First(x => x.getMatricula() == mat_aluno).getNome();

										aluno_matricula = alunos_achados.First(x => x.getMatricula() == mat_aluno);
										found_aluno = true;
									}
									else
										Console.WriteLine("Matrícula não encontrada!");
								}
								else
								{
									aluno_matricula = alunos_achados[0];
									found_aluno = true;
								}
							}

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
