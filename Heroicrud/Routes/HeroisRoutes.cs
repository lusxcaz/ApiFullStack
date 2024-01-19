using Heroicrud.Db;
using Heroicrud.Models;
using Heroicrud.Request;
using Microsoft.EntityFrameworkCore;

namespace Heroicrud.Routes
{
    public static class HeroisRoutes
    {
      public static void AddRotasHerois(this WebApplication app)
        {
            var rotasHerois = app.MapGroup("herois");


            //RETORNAR TODOS
            rotasHerois.MapGet("", async (AppDbContext context, CancellationToken ct) =>
            {
                var herois = await context.
                Herois
                .Where(herois => herois.Ativo)
                .ToListAsync(ct);
                return herois;
            });

            //RETORNAR POR ID

            rotasHerois.MapGet("/{id:int}", async (int id,AppDbContext context, CancellationToken ct) =>
            {
                var heroi = await context.Herois.FindAsync(id, ct);

                if (heroi == null)
                    return Results.NotFound("Herói não existe");

               return Results.Ok(heroi);
            });

            //ADICIONAR
            rotasHerois.MapPost("",
                async (AddHeroiRequest request, AppDbContext context, CancellationToken ct) =>
                {
                    var nomeHeroiBanco = await context.Herois
                        .AnyAsync(Heroi => Heroi.NomeHeroi == request.NomeHeroi, ct);

                    if (nomeHeroiBanco)
                        return Results.Conflict("Este herói já foi registrado no banco");

                    var novoHeroi = new Heroi(request.Nome, request.NomeHeroi, request.DataNascimento, request.Altura, request.Peso);
                    await context.Herois.AddAsync(novoHeroi, ct);
                    await context.SaveChangesAsync(ct);

                    return Results.Ok(novoHeroi);

                });


            //Atualizar
            rotasHerois.MapPut("{id:int}", async (int id,UpdateHeroiRequest request, AppDbContext context, CancellationToken ct) =>
            {
                var heroi = await context.Herois
                                .FirstOrDefaultAsync(heroi => heroi.Id == id, ct);

                if(heroi == null)
                    return Results.NotFound("Heroi não existe!");

                var nomeHeroiBanco = await context.Herois
                        .AnyAsync(Heroi => Heroi.NomeHeroi == request.NomeHeroi, ct);

                heroi.AtualizarDados(request.Nome, request.NomeHeroi, request.DataNascimento, request.Altura, request.Peso);

                await context.SaveChangesAsync(ct);
                return Results.Ok(heroi);

            });

            //Deletar

            rotasHerois.MapDelete("{id}", async (int id, AppDbContext context, CancellationToken ct) =>
            {
                var heroi = await context.Herois.SingleOrDefaultAsync(heroi => heroi.Id == id, ct);
                if (heroi == null)
                    return Results.NotFound("Herói não existe");

                context.Remove(heroi);

                await context.SaveChangesAsync(ct);

                return Results.Ok();

            });

        }
    }
}
