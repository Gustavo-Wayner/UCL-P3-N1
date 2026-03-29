public static class Program
{
	public static void Main()
	{
		Vetor<int> v = new []{2, 3, 5, 7, 11, 13, 17, 19, 23};

		Console.WriteLine($"Vetor v {v}; Tamanho: {v.Len}");

		v.Add(29);
		Console.WriteLine($"29 adicionado {v}; Tamanho: {v.Len}");

		v.Pop(1);
		Console.WriteLine($"3 removido {v}; Tamanho: {v.Len}");

		v.PopBack();
		Console.WriteLine($"29 removido {v}; Tamanho: {v.Len}");
	}
}