using System;
using System.Collections.Generic;
using System.Text;

namespace UCL_P3_N1
{
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
}
