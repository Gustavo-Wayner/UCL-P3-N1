using System;
using System.Collections.Generic;
using System.Text;

namespace UCL_P3_N1
{
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

		public override string ToString()
		{
			return $"{aluno.Matricula};{materia.Codigo};{n1};{n2};{media};{estado}";
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
