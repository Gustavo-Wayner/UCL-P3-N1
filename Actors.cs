using System;
using System.ComponentModel;

namespace UCL_P3_N1
{
	public class Aluno
	{
		private string nome;
		private int idade;
		private int matricula;

		public Aluno(string _nome, int _idade, int _matricula)
		{
			if (_idade < 11)
				throw new Exception("IdadeNegativaNaoRolaFi");

			nome = _nome;
			idade = _idade;
			matricula = _matricula;
		}

		/// <summary>
		/// Retorna o nome do aluno
		/// </summary>
		/// <returns></returns>
		public string getNome() => nome;

		/// <summary>
		/// Altera o nome do aluno
		/// </summary>
		/// <param name="_nome">Variável correspondente ao novo nome</param>
		public void setNome(string _nome) => nome = _nome;

		/// <summary>
		/// Retorna a idade do aluno
		/// </summary>
		/// <returns></returns>
		public int getIdade() => idade;

		/// <summary>
		/// Altera a idade do aluno
		/// </summary>
		/// <param name="_idade">Variável correspondente a nova idade</param>
		public void setIdade(int _idade) => idade = _idade;

		public int getMatricula() => matricula;

		/// <summary>
		/// Converte um Aluno em string
		/// </summary>
		/// <returns>Retorna o aluno em forma de string entre colchetes</returns>
		public override string ToString()
		{
			return $"[{matricula}, {nome}, {idade}]";
		}
	}

	public class Materia
	{
		string nome;
		double nota_min;
		int codigo;

		public Materia(string _nome, double _nota_min, int _codigo)
		{
			nome = _nome;
			nota_min = _nota_min;
			codigo = _codigo;
		}


		public string getNome() => nome;
		public void setNome(string _nome) => nome = _nome;

		public double getNotaMin() => nota_min;
		public void setNotaMin(double _nota_min) => nota_min = _nota_min;

		public int getCodigo() => codigo;
	}

	public class Matricula
	{
		private Aluno aluno;
		private Materia materia;

		private double n1;
		private double n2;
		private Misc.State estado;

		/// <summary>
		/// Construtor da classe Matricula
		/// </summary>
		/// <param name="_aluno">Referencia direta a uma instancia de classe Aluno</param>
		/// <param name="_materia">Referencia direta a uma instancia de classe Materia</param>
		/// <param name="_n1">Primeira nota (Opcional); Valor padrão: 0</param>
		/// <param name="_n2">Segunda nota (Opcional); Valor padrão: 0</param>
		/// <param name="_estado">Estado (Aprovado, Reprovado, A definir)(Opcional); Valor padrão: ADefinir</param>
		public Matricula(ref Aluno _aluno, ref Materia _materia, double _n1 = 0, double _n2 = 0, Misc.State _estado = Misc.State.ADeterminar)
		{
			aluno = _aluno;
			materia = _materia;
			n1 = _n1;
			n2 = _n2;
			estado = _estado;
		}

		public Aluno GetAluno() => aluno;
		public Materia GetMateria() => materia;
	}
}
