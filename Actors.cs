using System;
using System.ComponentModel;

namespace UCL_P3_N1
{
	public class Aluno
	{
		private string nome;
		private int idade;
		private int matricula;

		/// <summary>
		/// Construtor da classe Aluno
		/// </summary>
		/// <param name="_nome">Nome do aluno</param>
		/// <param name="_idade">Idade do aluno</param>
		/// <param name="_matricula">Matrícula do aluno</param>
		public Aluno(string _nome, int _idade, int _matricula)
		{
			if (_idade < 0)
				throw new Exception("IdadeNegativaNaoRolaFi");

			nome = _nome;
			idade = _idade;
			matricula = _matricula;
		}

		/// <summary>
		/// Interface para o nome do aluno
		/// </summary>
		public string Nome { get => nome; set => nome = value; }

		/// <summary>
		/// Interface para a idade do aluno
		/// </summary>
		public int Idade { get => idade; set => idade = value; }

		/// <summary>
		/// Interface para a matrícula do aluno
		/// </summary>
		public int Matricula { get => matricula; set => matricula = value; }

		/// <summary>
		/// Converte um Aluno em string
		/// </summary>
		/// <returns>Retorna o aluno em forma de string entre colchetes</returns>
		public override string ToString()
		{
			return $"{matricula};{nome};{idade}";
		}
	}

	public class Materia
	{
		string nome;
		double nota_min;
		int codigo;

		/// <summary>
		/// Construtor da classe Materia
		/// </summary>
		/// <param name="_nome">Nome da matéria</param>
		/// <param name="_nota_min">Nota mínima da matéria</param>
		/// <param name="_codigo">Código da matéria</param>
		public Materia(string _nome, double _nota_min, int _codigo)
		{
			nome = _nome;
			nota_min = _nota_min;
			codigo = _codigo;
		}

		/// <summary>
		/// Converte uma Materia em string
		/// </summary>
		/// <returns>Retorna o aluno em forma de string entre colchetes</returns>
		public override string ToString()
		{
			return $"{nome};{nota_min};{codigo}";
		}

		/// <summary>
		/// Interface para o nome da matéria
		/// </summary>
		/// <returns>Retorna o nome da matéria</returns>
		public string Nome { get => nome; set => nome = value; }

		/// <summary>
		/// Interface para a nota mínima da matéria
		/// </summary>
		/// <returns>Retorna a nota mínima da matéria</returns>
		public double NotaMin { get => nota_min; set => nota_min = value; }

		/// <summary>
		/// Interface para o código da matéria
		/// </summary>
		/// <returns>Retorna o código da matéria</returns>
		public int Codigo { get => codigo; set => codigo = value; }
	}

	public class Matricula
	{
		private Aluno aluno;
		private Materia materia;

		private double? n1;
		private double? n2;
		private double? media;
		private string estado;

		/// <summary>
		/// Construtor da classe Matricula
		/// </summary>
		/// <param name="_aluno">Referencia direta a uma instancia de classe Aluno</param>
		/// <param name="_materia">Referencia direta a uma instancia de classe Materia</param>
		/// <param name="_n1">Primeira nota (Opcional); Valor padrão: 0</param>
		/// <param name="_n2">Segunda nota (Opcional); Valor padrão: 0</param>
		/// <param name="_estado">Estado (Aprovado, Reprovado, A definir)(Opcional); Valor padrão: ADefinir</param>
		/// <param name="_media">Média (Opcional); Valor padrão: 0</param>
		public Matricula(ref Aluno _aluno, ref Materia _materia, double? _n1 = null, double? _n2 = null, double? _media = null, string _estado = "N/A")
		{
			aluno = _aluno;
			materia = _materia;
			n1 = _n1;
			n2 = _n2;
			media = _media;
			estado = _estado;
		}

		/// <summary>
		/// Interface para o aluno da matrícula
		/// </summary>
		public Aluno _Aluno { get => aluno; set => aluno = value; }

		/// <summary>
		/// Interface para a matéria da matrícula
		/// </summary>
		public Materia _Materia { get => materia; set => materia = value; }

		/// <summary>
		/// Interface para a primeira nota
		/// </summary>
		public double? N1 { get => n1; set => n1 = value; }

		/// <summary>
		/// Interface para a segunda nota
		/// </summary>
		public double? N2 { get => n2; set => n2 = value; }

		/// <summary>
		/// Interface para a média
		/// </summary>
		public double? Media { get => media; set => media = value; }

		/// <summary>
		/// Interface para o estado
		/// </summary>
		public string Estado { get => estado; }

		/// <summary>
		/// Define o estado
		///
		/// _estado == 0: Aprovado
		/// _estado == 1: Reprovado
		/// _estado < 0 ou _estado > 1 : A definir
		/// </summary>
		/// <param name="_estado">O estado</param>
		public void SetEstado(int _estado)
		{
			switch (_estado)
			{
				case 0:
					estado = "Aprovado";
					break;
				case 1:
					estado = "Reprovado";
					break;
				default:
					estado = "N/A";
					break;
			}
		}

		/// <summary>
		/// Define a média
		/// </summary>
		/// <param name="_media">A média</param>
		public void SetMedia(double? _media) => media = _media;
	}
}
