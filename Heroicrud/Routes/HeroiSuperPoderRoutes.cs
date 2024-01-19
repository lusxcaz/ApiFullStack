using Heroicrud.Db;
using Heroicrud.Models;
using Heroicrud.Request;
using Microsoft.EntityFrameworkCore;
using System;

public static class HeroiSuperPoderRoutes
    {
       public static void AddHeroiSuperPoder(this WebApplication app)
        {

            var rotasHeroiSuperPoder = app.MapGroup("heroisuperpoder");

        ;  //BUSCAR TODOS
            rotasHeroiSuperPoder.MapGet("", async (AppDbContext context, CancellationToken ct) =>
            {
                var superpoderBanco = await context.HeroiSuperPoderes.ToListAsync(ct);
                return superpoderBanco;
            });

            //BUSCAR POR ID
            rotasHeroiSuperPoder.MapGet("/{id:int}", async (int id, AppDbContext context, CancellationToken ct) =>
            {
                var superpoderBanco = await context.HeroiSuperPoderes
                    .Where(h => h.HeroiId == id)
                    .ToListAsync(ct);

                return superpoderBanco;
            });

            //ADICIONAR
            rotasHeroiSuperPoder.MapPost("",
                async (AddHeroiSuperPoderRequest request, AppDbContext context, CancellationToken ct) =>
                {
                    var superpoderBanco = await context.HeroiSuperPoderes
                            .AnyAsync(HeroiSuperPoderes => HeroiSuperPoderes.HeroiId == request.HeroiId, ct);

                    var novosuperpoderBanco = new HeroiSuperPoderes(request.HeroiId, request.SuperPoderId);

                    await context.HeroiSuperPoderes.AddAsync(novosuperpoderBanco, ct);
                    await context.SaveChangesAsync(ct);

                    return Results.Ok(novosuperpoderBanco);

                });
            //ALTERAR
            rotasHeroiSuperPoder.MapPut("{id:int}", async (int id, UpddateHeroiSuperPoderRequest request, AppDbContext context, CancellationToken ct) =>
            {
                var HeroiSuperPoder = await context.HeroiSuperPoderes
                                .FirstOrDefaultAsync(HeroiSuperPoder => HeroiSuperPoder.Id == id, ct);

                if (HeroiSuperPoder == null)
                    return Results.NotFound("não existe um herói com este poder");

                HeroiSuperPoder.AtualizarDados(request.HeroiId, request.SuperPoderId);

                await context.SaveChangesAsync(ct);
                return Results.Ok(HeroiSuperPoder);

            });
    }

}

