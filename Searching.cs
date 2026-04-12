using UCL_P3_N1;
using static UCL_P3_N1.Program;
using static Misc;

public static class Searching
{
	/// <summary>
	/// Busca um aluno pelo nome ou matrícula no vetor de alunos.
	/// </summary>
	/// <returns>O aluno encontrado.</returns>
	public static Aluno? SearchAluno()
	{
		Aluno? pesquisa = null;

		while (true)
		{
			Console.Write("Digite o nome ou matrícula do(a) aluno(a) (0 para voltar): ");
			string? entrada = ToTitleCase(Console.ReadLine());

			if (entrada == "0") return null;
			if(int.TryParse(entrada, out int mat))
			{
				if(!_Alunos.GetData().Any(x => x.getMatricula() == mat))
				{
                    Console.WriteLine($"Nenhum(a) aluno(a) de matrícula {mat} encontrado! Tente novamente");
                    continue;
                }
				pesquisa = _Alunos.GetData().First(x => x.getMatricula() == mat);
				return pesquisa;
			}
			Aluno[] Possiveis = _Alunos.GetData().Where(x => x.getNome() == entrada).ToArray();
			if(Possiveis.Length == 0)
			{
				Console.WriteLine($"Nenhum(a) {entrada} encontrado(a)! Tente novamente");
				continue;
			}
			else if(Possiveis.Length > 1)
			{
				Console.WriteLine($"Mais de um(a) {entrada} encontrado(a). Pesquise por matrícula");

				while(true)
				{
					int Mat = Parse<int>($"Digite a matrícula de {entrada} (0 para sair): ");
					if (Mat == 0) return null;

					if (!Possiveis.Any(x => x.getMatricula() == Mat))
					{
						Console.WriteLine($"Nenhum(a) {entrada} de matrícula {Mat} encontrado(a)! Tente novamente");
						continue;
					}
					pesquisa = _Alunos.GetData().First(x => x.getMatricula() == Mat);
					return pesquisa;
				}
			}

			pesquisa = _Alunos.GetData().First(x => x.getNome() == entrada);
			return pesquisa;
		}
	}

	/// <summary>
    /// Busca uma materia pelo nome ou codigo no vetor de materias.
    /// </summary>
    /// <param name="MATERIAS">O vetor de materias.</param>
    /// <returns>A materia encontrada.</returns>
    public static Materia? SearchMateria()
    {
        Materia? pesquisa = null;

        while (true)
        {
            Console.Write("Digite o nome ou código da matéria (0 para voltar): ");
            string? entrada = ToTitleCase(Console.ReadLine());

            if (entrada == "0") return null;
            if (int.TryParse(entrada, out int cod))
            {
                if (!_Materias.GetData().Any(x => x.getCodigo() == cod))
                {
                    Console.WriteLine($"Nenhuma matéria de código {cod} encontrada! Tente novamente");
                    continue;
                }
                pesquisa = _Materias.GetData().First(x => x.getCodigo() == cod);
                return pesquisa;
            }
            Materia[] Possiveis = _Materias.GetData().Where(x => x.getNome() == entrada).ToArray();
            if (Possiveis.Length == 0)
            {
                Console.WriteLine($"Nenhuma matéria de nome {entrada} encontrada! Tente novamente");
                continue;
            }
            else if (Possiveis.Length > 1)
            {
                Console.WriteLine($"Mais de uma matéria de nome {entrada} encontrada. Pesquise por código");

                while (true)
                {
                    int Cod = Misc.Parse<int>($"Digite a matrícula de {entrada} (0 para sair): ");
                    if (Cod == 0) return null;

                    if (!Possiveis.Any(x => x.getCodigo() == Cod))
                    {
                        Console.WriteLine($"Nenhuma matéria de nome {entrada} e de código {Cod} encontrada! Tente novamente");
                        continue;
                    }
                    pesquisa = _Materias.GetData().First(x => x.getCodigo() == Cod);
                    return pesquisa;
                }
            }

            pesquisa = _Materias.GetData().First(x => x.getNome() == entrada);
            return pesquisa;
        }
    }

	public static Aluno[] GetAlunosByMatricula(ref Aluno[] ALUNOS, int matricula)
	{
		return ALUNOS.Where(x => x.getMatricula() == matricula).ToArray();
	}

	public static Materia[] GetMateriasByCodigo(ref Materia[] MATERIAS, int codigo)
	{
		return MATERIAS.Where(x => x.getCodigo() == codigo).ToArray();
	}
}
