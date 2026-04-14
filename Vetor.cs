namespace UCL_P3_N1;

/// <summary>
/// Classe para criar uma lista capaz de se expandir e diminuir dinâmicamente
/// </summary>
/// <typeparam name="Tdados"></typeparam>
public class Vetor<Tdados>
{
	//dados do vetor
	private Tdados[]? data;

	/// <summary>
	/// Retorna a array interna da class vetor que guarda seus dados
	/// </summary>
	public Tdados[]? Data => data;

	public Vetor()
	{
		data = new Tdados[0];
	}

	public Vetor(Tdados[] _data) => data = _data;

	//Imprementacão de inicialização via coleções (chaves {x, y, z...})
	public static implicit operator Vetor<Tdados>(Tdados[] array) => new Vetor<Tdados>(array);

	/// <summary>
	/// Retorna o cumprimento do vetor e 0 se for nulo ( ou tiver cumprimento 0 )
	/// </summary>
	public int Len
	{
		get
		{
			if (data != null) return data.Length;
			return 0;
		}
	}

	/// <summary>
	/// Adiciona um elemento ao final do vetor
	/// </summary>
	/// <param name="element">Elmento a ser adicionado</param>
	public void Add(Tdados element)
	{
		Tdados[] copy = new Tdados[Len + 1];
		for (int i = 0; i < Len; i++)
		{
			copy[i] = data![i];
		}

		copy[Len] = element;
		data = copy;
	}

	/// <summary>
	/// Converte um vetor em string
	/// </summary>
	/// <returns>Retorna o vetor em forma de string entre chaves, ou apénas {} se o vetor estiver vazio </returns>
	public override string ToString()
	{
		if (data == null || data.Length == 0)
			return "{ }";

		string Str = $"{{ {data[0]}";

		for (int i = 1; i < Len; i++)
			Str += $", {data[i]}";

		Str += " }";

		return Str;
	}

	/// <summary>
	/// Deleta um item em determinada posição do vetor
	/// </summary>
	/// <param name="index">Índice a ser removido</param>
	/// <exception cref="IndexOutOfRangeException">Retorna erros quando o indice está fora do alcançe do vetor</exception>
	public void Pop(int index)
	{
		if (index < 0 || index >= Len) throw new IndexOutOfRangeException();

		Tdados[] result = new Tdados[Len - 1];

		int j = 0;
		for (int i = 0; j < Len - 1; i = ++j)
		{
			if (i == index) i++;

			result[j] = data![i];
		}

		data = result;
	}

	/// <summary>
	/// Remove o ultimo item do vetor
	/// </summary>
	public void PopBack()
	{
		Pop(Len - 1);
	}

	/// <summary>
	/// Implementação do operador [] no vetor
	/// </summary>
	/// <param name="i">O indice a ser retornado / alterado</param>
	/// <returns><typeparamref name="Tdados"/></returns>
	/// <exception cref="IndexOutOfRangeException">Retorna erro para indice fora de alcançe</exception>
	public Tdados this[int i]
	{
		get
		{
			if (i < 0 || i >= Len) throw new IndexOutOfRangeException("Indice fora de alcançe");
			return data![i];
		}
		set
		{
			if (i < 0 || i >= Len) throw new IndexOutOfRangeException("Indice fora de alcançe");
			data![i] = value;
		}
	}
}
