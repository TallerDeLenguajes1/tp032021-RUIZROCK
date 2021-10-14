using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cadeteria.Models
{
    public class DBTemporal
    {
        private Cadeteria cadeteria;
        string ubicacionCadetes = @"Cadetes.json";
        string ubicacionPedidos = @"Pedidos.json";
        public DBTemporal()
        {
            Cadeteria = new Cadeteria();
            if(verListaCadetes() != null)
            {
                Cadeteria.ListaCadetes = verListaCadetes();
            }

            if (verListaPedidos() != null)
            {
                Cadeteria.ListaPedidos = verListaPedidos();
            }
        }

        //------------------------ CADETES ------------------------

        //Guardar
        public void GuardarCadete(List<Cadete> cadetes)
        {
            try
            {
                string cadetesJson = JsonSerializer.Serialize(cadetes);
                using(FileStream miArchivo = new FileStream(ubicacionCadetes, FileMode.Create))
                {
                    using(StreamWriter strWrite = new StreamWriter(miArchivo))
                    {
                        strWrite.Write(cadetesJson);
                        strWrite.Close();
                        strWrite.Dispose();
                    }
                }

            } catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        //Mostrar Lista
        public List<Cadete> verListaCadetes()
        {
            List<Cadete> cadetesJson = null;
            try
            {
                if(File.Exists(ubicacionCadetes))
                {
                    using(FileStream miArchivo = new FileStream(ubicacionCadetes, FileMode.Open))
                    {
                        using(StreamReader strReader = new StreamReader(miArchivo))
                        {
                            string strCadetes = strReader.ReadToEnd();
                            cadetesJson = JsonSerializer.Deserialize<List<Cadete>>(strCadetes);
                        }
                    }
                }
            } catch(Exception ex)
            {
                string error = ex.Message;
            }
            return cadetesJson;
        }

        //Borrar
        public void BorrarCadete(int id)
        {
            try
            {
                List<Cadete> listaDeCadetes = verListaCadetes();

                Cadete cadeteAEliminar = listaDeCadetes.Where(cadete => cadete.Id == id).Single();
                listaDeCadetes.Remove(cadeteAEliminar);

                GuardarCadete(listaDeCadetes);

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        //Modificar
        public void ModificarCadete(Cadete cadete)
        {
            try
            {
                List<Cadete> listaCadetes = verListaCadetes();

                Cadete cadeteSeleccionado = listaCadetes.Where(cad => cad.Id == cadete.Id).Single();

                if (cadeteSeleccionado != null)
                {
                    cadeteSeleccionado.Nombre = cadete.Nombre;
                    cadeteSeleccionado.Direccion = cadete.Direccion;
                    cadeteSeleccionado.Telefono = cadete.Telefono;

                    GuardarCadete(listaCadetes);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        //------------------------ PEDIDOS ------------------------

        //Guardar
        public void GuardarPedido(List<Pedidos> pedidos)
        {
            try
            {
                string pedidosJson = JsonSerializer.Serialize(pedidos);
                using(FileStream miArchivo = new FileStream(ubicacionPedidos, FileMode.Create))
                {
                    using(StreamWriter strWrite = new StreamWriter(miArchivo))
                    {
                        strWrite.Write(pedidosJson);
                        strWrite.Close();
                        strWrite.Dispose();
                    }
                }

            } catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        //Mostrar Lista
        public List<Pedidos> verListaPedidos()
        {
            List<Pedidos> pedidosJson = null;
            try
            {
                if(File.Exists(ubicacionPedidos))
                {
                    using(FileStream miArchivo = new FileStream(ubicacionPedidos, FileMode.Open))
                    {
                        using(StreamReader strReader = new StreamReader(miArchivo))
                        {
                            string strPedidos = strReader.ReadToEnd();
                            pedidosJson = JsonSerializer.Deserialize<List<Pedidos>>(strPedidos);
                        }
                    }
                }
            } catch(Exception ex)
            {
                string error = ex.Message;
            }
            return pedidosJson;
        }

        //Modificar
        public void ModificarPedido(Pedidos pedido)
        {
            try
            {
                List<Pedidos> listaPedidos = verListaPedidos();

                Pedidos pedidoSeleccionado = listaPedidos.Where(ped => ped.NumeroPedido == pedido.NumeroPedido).Single();

                if (pedidoSeleccionado != null)
                {
                    pedidoSeleccionado.Observaciones = pedido.Observaciones;
                    pedidoSeleccionado.Estado = pedido.Estado;
                    pedidoSeleccionado.Cliente.Nombre = pedido.Cliente.Nombre;
                    pedidoSeleccionado.Cliente.Id = pedido.Cliente.Id;
                    pedidoSeleccionado.Cliente.Direccion = pedido.Cliente.Direccion;
                    pedidoSeleccionado.Cliente.Telefono = pedido.Cliente.Telefono;

                    GuardarPedido(listaPedidos);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        public Cadeteria Cadeteria { get => cadeteria; set => cadeteria = value; }
    }
}
