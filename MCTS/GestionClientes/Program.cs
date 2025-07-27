using System;
using System.Collections.Generic;
using System.Text;

namespace GestionClientes
{
        
    class Program
    {
        static void Main(string[] args)
        {

            Cliente joseCarlos = new Cliente("Jose Carlos", "77777777F");
            joseCarlos.A�adirPedido(new Articulo("Placa Base",100));
            joseCarlos.A�adirPedido(new Articulo("Disco Duro",60));
            float facturaJC = joseCarlos.RealizarFactura();

            ClienteEspecial paqui = new ClienteEspecial("Paqui", "77555444D");
            paqui.A�adirPedido(new Articulo("Placa Base", 100));
            paqui.A�adirPedido(new Articulo("Disco Duro", 60));
            float facturaPaqui= paqui.RealizarFactura();
            //solo hace la compra Paqui
            paqui.EntregarRecibo(facturaPaqui);
        }
    }
}
