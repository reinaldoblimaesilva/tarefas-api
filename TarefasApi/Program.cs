using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

var tarefas = new List<Tarefa>
{
    new(1, "Estudar Docker",     false),
    new(2, "Estudar Kubernetes", false),
};

// Health check para o GCP Load Balancer
app.MapGet("/", () => Results.Ok(new { status = "healthy" }));

app.MapGet("/tarefas", () => tarefas).WithName("ListarTarefas");

app.MapGet("/tarefas/{id}", (int id) =>
    tarefas.FirstOrDefault(t => t.Id == id)
        is Tarefa t ? Results.Ok(t) : Results.NotFound())
   .WithName("BuscarTarefa");

app.MapPost("/tarefas", (Tarefa nova) => {
    nova = nova with { Id = tarefas.Count + 1 };
    tarefas.Add(nova);
    return Results.Created($"/tarefas/{nova.Id}", nova);
}).WithName("CriarTarefa");

app.MapPut("/tarefas/{id}", (int id, Tarefa atualizada) => {
    var idx = tarefas.FindIndex(t => t.Id == id);
    if (idx < 0) return Results.NotFound();
    tarefas[idx] = atualizada with { Id = id };
    return Results.Ok(tarefas[idx]);
}).WithName("AtualizarTarefa");

app.MapDelete("/tarefas/{id}", (int id) => {
    var t = tarefas.FirstOrDefault(t => t.Id == id);
    if (t is null) return Results.NotFound();
    tarefas.Remove(t);
    return Results.NoContent();
}).WithName("DeletarTarefa");

app.Run();

public record Tarefa(int Id, string Titulo, bool Concluida);