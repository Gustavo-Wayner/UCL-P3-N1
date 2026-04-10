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
		/// Obtém o nome da matéria
		/// </summary>
		/// <returns>Retorna o nome da matéria</returns>
		public string getNome() => nome;

		/// <summary>
		/// Define o nome da matéria
		/// </summary>
		/// <param name="_nome">Nome da matéria</param>
		public void setNome(string _nome) => nome = _nome;

		/// <summary>
		/// Obtém a nota mínima da matéria
		/// </summary>
		/// <returns>Retorna a nota mínima da matéria</returns>
		public double getNotaMin() => nota_min;

		/// <summary>
		/// Define a nota mínima da matéria
		/// </summary>
		/// <param name="_nota_min">Nota mínima da matéria</param>
		public void setNotaMin(double _nota_min) => nota_min = _nota_min;

		/// <summary>
		/// Obtém o código da matéria
		/// </summary>
		/// <returns>Retorna o código da matéria</returns>
		public int getCodigo() => codigo;
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
		/// Obtém o aluno da matrícula
		/// </summary>
		/// <returns>Retorna uma referência direta a uma instância de classe Aluno</returns>
		public Aluno GetAluno() => aluno;

		/// <summary>
		/// Obtém a matéria da matrícula
		/// </summary>
		/// <returns>Retorna uma referência direta a uma instância de classe Materia</returns>
		public Materia GetMateria() => materia;

		/// <summary>
		/// Obtém a primeira nota
		/// </summary>
		/// <returns>Retorna a primeira nota</returns>
		public double? GetN1() => n1;

		/// <summary>
		/// Obtém a segunda nota
		/// </summary>
		/// <returns>Retorna a segunda nota</returns>
		public double? GetN2() => n2;

		/// <summary>
		/// Obtém o estado
		/// </summary>
		/// <returns>Retorna o estado</returns>
		public string GetEstado() => estado;

		/// <summary>
		/// Obtém a média
		/// </summary>
		/// <returns>Retorna a média</returns>
		public double? GetMedia() => media;

		/// <summary>
		/// Define a primeira nota
		/// </summary>
		/// <param name="_n1">A primeira nota</param>
		public void SetN1(double _n1) => n1 = _n1;

		/// <summary>
		/// Define a segunda nota
		/// </summary>
		/// <param name="_n2">A segunda nota</param>
		public void SetN2(double _n2) => n2 = _n2;

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
