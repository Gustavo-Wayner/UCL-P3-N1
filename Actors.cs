using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace UCL_P3_N1
{
	public class Aluno
	{
		private string nome;
		private string cpf;
		private double n1;
		private double n2;
		private Misc.State estado;

		public Aluno(string _nome, string _cpf)
		{
			if (_cpf.Length != 11)
				throw new Exception("CpfNaoTem11Numeros");

			nome = _nome;
			cpf = _cpf;
			estado = Misc.State.ADeterminar;
		}

		public Aluno(string _nome, string _cpf, double _n1, double _n2)
		{
			if (_cpf.Length != 11)
				throw new Exception("CpfNaoTem11Numeros");

			nome = _nome;
			cpf = _cpf;
			estado = Misc.State.ADeterminar;
			n1 = _n1;
			n2 = _n2;
		}

		public string getNome() => nome;
		public void setNome( string _nome) => nome = _nome;

		public string getCpf() => cpf;
		public void setCpf( string _cpf  ) => cpf = _cpf;
	}
}
