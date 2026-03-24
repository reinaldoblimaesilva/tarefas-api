namespace TarefasApi.Tests;

public class TarefaTests
{
    [Fact]
    public void Tarefa_DeveTerTituloNaoVazio()
    {
        var tarefa = new Tarefa(1, "Estudar CI/CD", false);
        Assert.False(string.IsNullOrEmpty(tarefa.Titulo));
    }

    [Fact]
    public void Tarefa_NovaCriadaDeveEstarNaoConcluida()
    {
        var tarefa = new Tarefa(1, "Nova tarefa", false);
        Assert.False(tarefa.Concluida);
    }
}