public class Vetor<T>
{
	private T[]? data;
	public Vetor()
	{
		data = new T[0];
	}

	public Vetor( T[] _data ) => data = _data;
	public static implicit operator Vetor<T>( T[] array ) => new Vetor<T>(array);

	public int Len
	{
		get
		{
			if ( data != null ) return data.Length;
			return 0;
		}
	}

	private T[] GetData()
	{
		return data!;
	}

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

	public override string ToString()
	{
		if ( data == null || data.Length == 0 ) return "{ }";
		string Str = $"{{ {data[0]}";
		for ( int i = 1; i < Len; i++ ) Str += $", {data[i]}";
		Str += " }";

		return Str;
	}

	public void Pop( int index )
	{
		if ( index < 0 || index >= Len ) throw new Exception("Index out of range");
		if ( data == null || Len == 0 ) throw new Exception("Nothing to pop");

		T[] result = new T[Len-1];

		int j = 0;
		for( int i = 0; j < Len-1; i++ )
		{
			if ( i == index ) i++;

			result[j] = data[i];
			j++;
		}

		data = result;
	}

	public void PopBack()
	{
		if ( data == null || Len == 0 ) throw new Exception("Nothing to pop");

		T[] result = new T[Len-1];
		
		for ( int i = 0; i < Len-1; i++)
		{
			result[i] = data[i];
		}

		data = result;
	}
}