using System;
using System.Collections.Generic;
using System.Text;

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
}
