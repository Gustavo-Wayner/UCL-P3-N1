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

		public string getNome() => nome;
		public void setNome(string _nome) => nome = _nome;

		public string getCpf() => cpf;
		public void setCpf(string _cpf) => cpf = _cpf;

		public override string ToString()
		{
			return $"[{nome}, {cpf}]";
		}
	}

	public class Materia
	{
		int codigo;
		string nome;


		public Materia(int _codigo, string _nome)
		{
			codigo = _codigo;
			nome = _nome;
		}

		public int getCodigo() => codigo;
		public void setCodigo(int _codigo) => codigo = _codigo;

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
