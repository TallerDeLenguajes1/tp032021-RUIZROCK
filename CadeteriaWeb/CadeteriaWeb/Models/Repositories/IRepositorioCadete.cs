using CadeteriaWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Repositories
{
    public interface IRepositorioCadete
    {
        void DeleteCadete(int id);
        List<Cadete> GetAllCadetes();
        Cadete GetCadeteById(int id);
        Cadete GetCadeteByName(string nombre);
        void InsertCadete(Cadete item);
        void UpdateCadete(Cadete item);
    }
}
