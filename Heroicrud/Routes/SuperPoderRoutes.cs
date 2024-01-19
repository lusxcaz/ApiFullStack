using Heroicrud.Db;
using Heroicrud.Models;
using Heroicrud.Request;
using Microsoft.EntityFrameworkCore;
using System;



namespace Heroicrud.Routes
{
    public static class SuperPoderRoutes
    {

        public static void AddSuperPoderRoutes(this WebApplication app)
        {
            var rotasPoder = app.MapGroup("superpoder");

            rotasPoder.MapGet("", async (AppDbContext context) =>
            {
                var poderes = await context.SuperPoder.ToListAsync();
                return  poderes;
            });

        }

    }
}
