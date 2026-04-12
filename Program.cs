using static Misc;
using static Searching;
using static DatRead;

namespace UCL_P3_N1;

public static class Program
{
	/// <summary>
	/// Vetor estático que armazena todos os alunos cadastrados no sistema
	/// </summary>
	private static Vetor<Aluno> Alunos = new();
	public static Vetor<Aluno> _Alunos { get { return Alunos; } set { Alunos = value; } }

	/// <summary>
	/// Vetor estático que armazena todas as matérias cadastradas no sistema
	/// </summary>
	private static Vetor<Materia> Materias = new();
	public static Vetor<Materia> _Materias { get { return Materias; } set { Materias = value; } }

	/// <summary>
	/// Vetor estático que armazena todas as matrículas (aluno em matéria) no sistema
	/// </summary>
	private static Vetor<Matricula> Matriculas = new();
	public static Vetor<Matricula> _Matriculas { get { return Matriculas; } set { Matriculas = value; } }

	/// <summary>
	/// Ponto de entrada principal do programa.
	/// Inicializa os dados dos arquivos .dat e exibe o menu principal.
	/// </summary>
	public static void Main()
	{
		Alunos = LerAlunosDoDat();
		Materias = LerMateriasDoDat();
		Matriculas = LerMatriculasDoDat();

		int input = 0;
		bool over = false;

		while (!over)
		{
			input = Parse<int>($"{new string('=', 16)}HOME{new string('=', 16)}\n" +
				"1 - Listar;\n" +
				"2 - Cadastrar;\n" +
				"3 - Salvar;\n" +
				"4 - Sair;\n" +
				"->"
			);

			switch (input)
			{
				case 1:
					ListarMenu();
					break;
				case 2:
					CadastrarMenu();
					break;
				case 3:
					SalvarDados();
					break;
				case 4:
					over = true;
					break;
				default:
					Console.WriteLine("Entrada inválida!!!");
					break;
			}
		}

		Console.Clear();
	}

	/// <summary>
	/// Exibe o submenu de opções de listagem e redireciona para a função correspondente.
	/// </summary>
	private static void ListarMenu()
	{
		Console.Clear();
		int input = Parse<int>($"{new string('=', 16)}LISTAGEM{new string('=', 16)}\n" +
			"1 - Listar alunos;\n" +
			"2 - Listar materias;\n" +
			"3 - Listar alunos de uma materia;\n" +
			"4 - Listar boletim de um aluno;\n" +
			"5 - Voltar;\n" +
			"->"
		);

		switch (input)
		{
			case 1:
				ListarAlunos();
				break;
			case 2:
				ListarMaterias();
				break;
			case 3:
				ListarAlunosDeMateria();
				break;
			case 4:
				ListarBoletimDeAluno();
				break;
			case 5:
				Console.Clear();
				break;
			default:
				Console.WriteLine("Entrada inválida!");
				break;
		}
	}

	/// <summary>
	/// Lista todos os alunos cadastrados no sistema em formato de tabela.
	/// Exibe matrícula, nome e idade de cada aluno.
	/// </summary>
	private static void ListarAlunos() // TODO: Implementa essa função
	{
		if (Alunos.Len == 0)
		{
			Console.Clear();
			Console.WriteLine("Nenhum aluno para listar!!!");
			return;
		}
	}

	/// <summary>
	/// Lista todas as matérias cadastradas no sistema em formato de tabela.
	/// Exibe código, nome e nota mínima de cada matéria.
	/// </summary>
	private static void ListarMaterias() // TODO: Implementa essa função
	{
		if (Materias.Len == 0)
		{
			Console.Clear();
			Console.WriteLine("Nenhuma materia para listar!!!");
			return;
		}
	}

	/// <summary>
	/// Lista todos os alunos matriculados em uma matéria específica.
	/// Exibe matrícula, nome, idade, notas (N1, N2), média e estado de cada aluno na matéria.
	/// </summary>
	private static void ListarAlunosDeMateria()
	{
		if (Materias.Len == 0)
		{
			Console.Clear();
			Console.WriteLine("Nenhuma materia para listar alunos!!!");
			return;
		}

		Materia? Mat_pesquisa = SearchMateria();
		if (Mat_pesquisa == null) return;

		Matricula[] matriculas = Matriculas.GetData().Where(x => x.GetMateria().getCodigo() == Mat_pesquisa.getCodigo()).ToArray();
		if (matriculas.Length == 0)
		{
			Console.Clear();
			Console.WriteLine("Nenhum aluno matriculado na materia.");
			return;
		}

		Aluno[] alunos_em_mat = matriculas.Select(x => x.GetAluno()).ToArray();
		Console.WriteLine($"Alunos matriculados na materia {Mat_pesquisa.getNome()}:");

		Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
		Console.WriteLine($"{"Matrícula",-13}|{"Nome",-38} |{"Idade",-8} |{"N1",-6} |{"N2",-6} |{"Média",-6} |{"Estado"}");
		Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
		foreach (var aluno in alunos_em_mat)
		{
			double? N1 = matriculas.First(x => x.GetAluno().getMatricula() == aluno.getMatricula()).GetN1();
			double? N2 = matriculas.First(x => x.GetAluno().getMatricula() == aluno.getMatricula()).GetN2();
			double? media = (N1 == null || N2 == null) ? null : (N1 + N2) / 2;

			string estado = "N/A";
			if (media != null) estado = media >= Mat_pesquisa.getNotaMin() ? "Aprovado" : "Reprovado";
			Console.WriteLine($"{aluno.getMatricula(),-13}|{aluno.getNome(),-38} |{aluno.getIdade(),-8} |{(N1 == null ? "N/A" : N1.Value.ToString("F2")),-6} |{(N2 == null ? "N/A" : N2.Value.ToString("F2")),-6} |{(media == null ? "N/A" : media.Value.ToString("F2")),-6} |{estado}");
		}
		Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
		Console.WriteLine("Digite qualquer coisa para voltar");
		Console.ReadLine();
		Console.Clear();
	}

	/// <summary>
	/// Exibe o boletim de um aluno específico, mostrando todas as matérias em que está matriculado
	/// com suas respectivas notas (N1, N2), média e estado.
	/// </summary>
	private static void ListarBoletimDeAluno()
	{
		if (Alunos.Len == 0)
		{
			Console.Clear();
			Console.WriteLine("Nenhum aluno para listar o boletim!!!");
			return;
		}

		Aluno? aluno_pesquisa = SearchAluno();
		if (aluno_pesquisa == null) return;

		Matricula[] matriculas_aluno = Matriculas.GetData().Where(x => x.GetAluno().getMatricula() == aluno_pesquisa!.getMatricula()).ToArray();

		Console.WriteLine($"Boletim de {aluno_pesquisa!.getNome()}");
		Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
		Console.WriteLine($"{"Materia",-38} |{"Nota 1",-6} |{"Nota 2",-6} |{"Media",-6} |{"Estado"}");
		Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
		foreach (var matricula in matriculas_aluno)
		{
			Console.WriteLine($"{matricula.GetMateria().getNome(),-38} |{(matricula.GetN1() == null ? "N/A" : matricula.GetN1()!.Value.ToString("F2")),-6} |{(matricula.GetN2() == null ? "N/A" : matricula.GetN2()!.Value.ToString("F2")),-6} |{(matricula.GetMedia() == null ? "N/A" : matricula.GetMedia()!.Value.ToString("F2")),-6} |{matricula.GetEstado()}");
		}
		Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
		Console.WriteLine("Digite qualquer coisa para voltar");
		Console.ReadLine();
		Console.Clear();
	}

	/// <summary>
	/// Exibe o submenu de opções de cadastro e redireciona para a função correspondente.
	/// </summary>
	private static void CadastrarMenu()
	{
		Console.Clear();
		int input = Parse<int>($"{new string('=', 16)}CADASTROS{new string('=', 16)}\n" +
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
				CadastrarAluno();
				break;
			case 2:
				CadastrarMateria();
				break;
			case 3:
				MatricularAlunoEmMateria();
				break;
			case 4:
				AtribuirNota();
				break;
			case 5:
				Console.Clear();
				break;
			default:
				Console.WriteLine("Entrada inválida!");
				break;
		}
	}

	/// <summary>
	/// Cadastra um novo aluno no sistema.
	/// Solicita nome, idade e matrícula, verificando se a matrícula já existe.
	/// </summary>
	private static void CadastrarAluno()
	{
		string? nome_aluno;
		while (true)
		{
			Console.Write("Informe o nome do(a) aluno(a) (0 para voltar): ");
			nome_aluno = ToTitleCase(Console.ReadLine());

			if(string.IsNullOrWhiteSpace(nome_aluno))
			{
				Console.WriteLine("Nome deve ser preenchido!!!");
				continue;
			}

			if(int.TryParse(nome_aluno, out int n) && nome_aluno != "0")
			{
				Console.WriteLine("O nome não deve ser um numero!");
				continue;
			}

			if(nome_aluno.ContainsAny("#;:{}[]/?°\\|!@$%¨&*()_+=-¹²³£¢¬§´`~^ªº\'\".,<>".ToArray()))
			{
				Console.WriteLine("Entrada inválida");
				continue;
			}
			break;
		}

		if (nome_aluno == "0")
		{
			Console.Clear();
			return;
		}

		int idade = (int)Parse<uint>("Informe a idade do(a) aluno(a) (0 para voltar): ", "Idade deve ser um numero natural!!!");

		if (idade == 0)
		{
			Console.Clear();
			return;
		}

		int matricula;
		bool clone;
		do
		{
			clone = false;
			matricula = (int)Parse<uint>($"Informe um codigo de matricula para {nome_aluno} (0 para voltar): ", "Matricula deve ser um numero natural!!!");

			foreach (Aluno aluno in Alunos.GetData())
			{
				if (aluno.getMatricula() == matricula)
				{
					Console.WriteLine("Matricula repetida!!!");
					clone = true;
					break;
				}
			}
		} while (clone);

		if (matricula == 0)
		{
			Console.Clear();
			return;
		}

		Alunos.Add(new(nome_aluno, idade, matricula));
		OrderAlunos(ref Alunos);
	}

	/// <summary>
	/// Cadastra uma nova matéria no sistema.
	/// Solicita nome, nota mínima e código, verificando se o código já existe.
	/// </summary>
	private static void CadastrarMateria()
	{
		string? nome_materia;
		double nota_min;
		int codigo;

		while (true)
		{
			Console.Write("Informe o nome da matéria (0 para voltar): ");
			nome_materia = ToTitleCase(Console.ReadLine());

			if(string.IsNullOrWhiteSpace(nome_materia))
			{
				Console.WriteLine("Nome deve ser preenchido!!!");
				continue;
			}

			if(int.TryParse(nome_materia, out int n) && nome_materia != "0")
			{
				Console.WriteLine("O nome não deve ser um numero!");
				continue;
			}

			if(nome_materia.ContainsAny("#;:{}[]/?°\\|!@$%¨&*()_+=-¹²³£¢¬§´`~^ªº\'\".,><".ToArray()))
			{
				Console.WriteLine("Entrada inválida");
				continue;
			}
			break;
		}

		if (nome_materia == "0")
		{
			Console.Clear();
			return;
		}

		nota_min = Parse<double>("Informe a nota minima para a materia (0 para voltar): ");
		while (nota_min < 0 || nota_min >= 10)
		{
			Console.WriteLine("Nota minima deve estar entre 0 e 10!!!");
			nota_min = Parse<double>("Informe a nota minima para a materia: ");
		}
		if (nota_min == 0)
		{
			Console.Clear();
			return;
		}

		while (true)
		{
			codigo = (int)Parse<uint>("Informe o codigo da materia (0 para voltar): ");

			Materia[] matCod = Materias.GetData().Where(m => m.getCodigo() == codigo).ToArray();
			if (matCod.Length == 0)
			{
				break;
			}
			Console.WriteLine("Codigo repetido!!!");
		}
		if (codigo == 0)
		{
			Console.Clear();
			return;
		}

		Materias.Add(new(nome_materia, nota_min, codigo));
		OrderMaterias(ref Materias);
	}

	/// <summary>
	/// Matricula um aluno em uma matéria.
	/// Solicita a seleção de uma matéria e um aluno, e cria uma nova matrícula.
	/// </summary>
	private static void MatricularAlunoEmMateria()
	{
		if (Alunos.Len == 0)
		{
			Console.Clear();
			Console.WriteLine("Nenhum aluno para matricular!!!");
			return;
		}
		if (Materias.Len == 0)
		{
			Console.Clear();
			Console.WriteLine("Nenhuma materia em que matricular!!!");
			return;
		}

		Materia? materia_matricula = SearchMateria();
		if (materia_matricula == null)
		{
			Console.Clear();
			return;
		}

		Aluno? aluno_matricula = SearchAluno();
		if (aluno_matricula == null)
		{
			Console.Clear();
			return;
		}

		if(Matriculas.GetData().Any(x => x.GetAluno().getMatricula() == aluno_matricula.getMatricula() && x.GetMateria().getCodigo() == materia_matricula.getCodigo()))
		{
			Console.Clear();
			Console.WriteLine($"{aluno_matricula.getNome()} ja possúi matrícula em {materia_matricula.getNome()}");
			return;
		}

		Matriculas.Add(new Matricula(ref aluno_matricula, ref materia_matricula));
		OrderMatriculas(ref Matriculas);
	}

	/// <summary>
	/// Atribui uma nota (N1 ou N2) a um aluno em uma matéria específica.
	/// Calcula automaticamente a média e define o estado (Aprovado/Reprovado) quando ambas as notas estão presentes.
	/// </summary>
	private static void AtribuirNota()
	{
		if (Matriculas.Len == 0)
		{
			Console.Clear();
			Console.WriteLine("Nenhuma matrícula para atribuir nota!!!");
			return;
		}

		Matricula? matricula_selecionada = null;
		bool N1OrN2 = false;
		bool back = false;
		int N;
		do
		{
			N = Parse<int>("Deseja aplicar a nota a: \n" +
				"1 - N1\n" +
				"2 - N2\n" +
				"3 - Voltar\n" +
				"?->"
			);
			if (N == 1) N1OrN2 = true;
			else if (N == 2) N1OrN2 = true;
			else if (N == 3) back = true;
			else Console.WriteLine("Entrada inválida!");
		} while (!N1OrN2);

		if (back)
		{
			Console.Clear();
			return;
		}

		Materia? materia_nota = SearchMateria();
		if (materia_nota == null)
		{
			Console.Clear();
			return;
		}

		Aluno? aluno_nota = SearchAluno();
		if (aluno_nota == null)
		{
			Console.Clear();
			return;
		}

		if (!Matriculas.GetData().Any(x => x.GetAluno().getMatricula() == aluno_nota.getMatricula() && x.GetMateria().getCodigo() == materia_nota.getCodigo()))
		{
			Console.Clear();
			Console.WriteLine("Não ha uma matrícula para esse aluno nessa materia!");
			return;
		}
		matricula_selecionada = Matriculas.GetData().First(x => x.GetAluno().getMatricula() == aluno_nota.getMatricula() && x.GetMateria().getCodigo() == materia_nota.getCodigo());

		double nota;

		while(true)
		{
			nota = Parse<double>("Informe a nota: ");

			if (nota >= 0 && nota <= 10)
				break;
			Console.WriteLine("A nota deve estar entre 0 e 10!");
		}

		if (N == 1)
		{
			matricula_selecionada!.SetN1(nota);
		}
		else if (N == 2)
		{
			matricula_selecionada!.SetN2(nota);
		}

		matricula_selecionada!.SetMedia((matricula_selecionada!.GetN1() + matricula_selecionada!.GetN2()) / 2);

		if (matricula_selecionada.GetN1() != null && matricula_selecionada.GetN2() != null)
			matricula_selecionada!.SetEstado(matricula_selecionada!.GetMedia() >= materia_nota.getNotaMin() ? 0 : 1);
	}

	/// <summary>
	/// Salva todos os dados do sistema (alunos, matérias e matrículas) nos arquivos .dat correspondentes.
	/// </summary>
	private static void SalvarDados()
	{
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

		using (StreamWriter sw = new(Global.MatriculaDataPath))
		{
			foreach (Matricula mat in Matriculas.GetData())
				sw.WriteLine($"{mat.GetAluno().getMatricula()};{mat.GetMateria().getCodigo()};{mat.GetN1()};{mat.GetN2()};{mat.GetMedia()};{mat.GetEstado()}");
		}
	}
}
