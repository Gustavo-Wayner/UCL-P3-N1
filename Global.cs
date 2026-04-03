namespace UCL_P3_N1;

public static class Global
{
	/// <summary>
	/// Serve para sincronizar a forma como File.Exists e StreamWriter e StreamReader Procuram por arquivos
	/// forçando todos a usarem um caminho relativo ao exe
	/// </summary>
	public static readonly string AlunoDataPath =  Path.Combine(AppContext.BaseDirectory, "alunos.dat");
}