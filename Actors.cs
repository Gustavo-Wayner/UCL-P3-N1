using System;

namespace UCL_P3_N1
{
	public class Aluno
	{
		private string nome;
		private string cpf;

		public Aluno(string _nome, string _cpf)
		{
			if (_cpf.Length != 11)
				throw new Exception("CpfNaoTem11Numeros");

			nome = _nome;
			cpf = _cpf;
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
		/// Retorna o cpf do aluno
		/// </summary>
		/// <returns></returns>
		public string getCpf() => cpf;

		/// <summary>
		/// Altera o cpf do aluno
		/// </summary>
		/// <param name="_cpf">Variável correspondente ao novo cpf</param>
		public void setCpf(string _cpf) => cpf = _cpf;

		/// <summary>
		/// Converte um Aluno em string
		/// </summary>
		/// <returns>Retorna o aluno em forma de string entre colchetes</returns>
		public override string ToString()
		{
			return $"[{nome}, {cpf}]";
		}
	}

	public class Materia
	{
		string nome;

		public Materia(string _nome)
		{
			nome = _nome;
		}


		public string getNome() => nome;
		public void setNome(string _nome) => nome = _nome;
	}

	public class Matricula
	{
		private Aluno aluno;
		private Materia materia;

		private double n1;
		private double n2;
		private Misc.State estado;

		public Matricula(ref Aluno _aluno, ref Materia _materia, double _n1, double _n2, Misc.State _estado)
		{
			aluno = _aluno;
			materia = _materia;
			n1 = _n1;
			n2 = _n2;
			estado = _estado;
		}
	}
}
