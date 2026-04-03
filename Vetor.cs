namespace UCL_P3_N1;

//Classe para criar uma lista capaz de se expandir e diminuir dinâmicamente
public class Vetor<T>
{
	//dados do vetor
	private T[]? data;
	public Vetor()
	{
		data = new T[0];
	}

	public Vetor( T[] _data ) => data = _data;

	//Imprementacão de inicialização via coleções (chaves {x, y, z...})
	public static implicit operator Vetor<T>( T[] array ) => new Vetor<T>(array);

	/// <summary>
	/// Retorna o cumprimento do vetor e 0 se for nulo ( ou tiver cumprimento 0 )
	/// </summary>
	public int Len
	{
		get
		{
			if ( data != null ) return data.Length;
			return 0;
		}
	}

	/// <summary>
	/// Retorna a array interna da class vetor que guarda seus dados
	/// </summary>
	public T[] GetData()
	{
		return data!;
	}

	/// <summary>
	/// Adiciona um elemento ao final do vetor
	/// </summary>
	/// <param name="element">Elmento a ser adicionado</param>
	public void Add(T element)
	{
		T[] copy = new T[Len+1];
		for ( int i = 0; i < Len; i++ )
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
		if ( data == null || data.Length == 0 )
			return "{ }";

		string Str = $"{{ {data[0]}";

		for ( int i = 1; i < Len; i++ )
			Str += $", {data[i]}";

		Str += " }";

		return Str;
	}

	/// <summary>
	/// Deleta um item em determinada posição do vetor
	/// </summary>
	/// <param name="index">Índice a ser removido</param>
	/// <exception cref="IndexOutOfRangeException">Retorna erros quando o indice está fora do alcançe do vetor</exception>
	public void Pop( int index )
	{
		if ( index < 0 || index >= Len ) throw new IndexOutOfRangeException();

		T[] result = new T[Len-1];

		int j = 0;
		for( int i = 0; j < Len-1; i++ )
		{
			if ( i == index ) i++;

			result[j] = data![i];
			j++;
		}

		data = result;
	}

	/// <summary>
	/// Remove o ultimo item do vetor
	/// </summary>
	public void PopBack()
	{
		Pop(Len-1);
	}

	/// <summary>
	/// Implementação do operador [] no vetor
	/// </summary>
	/// <param name="i">O indice a ser retornado / alterado</param>
	/// <returns><typeparamref name="T"/></returns>
	/// <exception cref="IndexOutOfRangeException">Retorna erro para indice fora de alcançe</exception>
	public T this[int i]
	{
		get
		{
			if ( i < 0 || i >= Len) throw new IndexOutOfRangeException("Indice fora de alcançe");
			return data![i];
		}
		set
		{
			if ( i < 0 || i >= Len) throw new IndexOutOfRangeException("Indice fora de alcançe");
			data![i] = value;
		}
	}
}