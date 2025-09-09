
using System.ClientModel.Primitives;
using Practica01.Domain;
using Practica01.Services;

FacturaService oService = new FacturaService();
Console.WriteLine("Obtener todas las facturas - GetAll");

List<Factura> lf = oService.GetFacturas();

if (lf.Count > 0)
    foreach (Factura f in lf)
        Console.WriteLine(f);
else
    Console.WriteLine("No hay facturas...");

Console.WriteLine("\nObtener una factura por id - GetById");
Factura? factura1 = oService.GetFacturaById(1);
if (factura1 != null)
{
    Console.WriteLine(factura1);
}
else
{
    Console.WriteLine("No hay factura con codigo = 1");
}
//Console.WriteLine("\nEliminar una factura por id - DeleteFactura"
Console.WriteLine("\nCrear una nueva factura - SaveFactura");
Factura myFactura = new Factura();
myFactura.Date =  DateTime.Now;
myFactura.FormaPago = new FormaPago
{
    Id = 1,
    Nombre = "Efectivo"
};
myFactura.Cliente = "Cliente prueba";

bool resultCreate = oService.SaveFactura(myFactura);

if (resultCreate)
{
    Console.WriteLine("Se ha creado la factura con exito");
}
else
{
    Console.WriteLine("No se ha podido crear la factura");
}
Console.WriteLine("\nActualizar datos de una factura - SaveFactura");
Factura myFactura2 = new Factura();
myFactura2.Date = DateTime.Now;
myFactura2.FormaPago = new FormaPago
{
    Id = 2,
    Nombre = "Transferencia"
};
myFactura2.Cliente = "Cliente prueba 2";

bool resultUpdate = oService.SaveFactura(myFactura2);
if (resultUpdate)
{
    Console.WriteLine("Se ha actualizado la factura con exito");
}
else
{
    Console.WriteLine("No se ha podido actualizar la factura");
}
Console.WriteLine("\nGuardar una factura y sus detalles - ExecuteTransaction");

Factura complexFactura = new Factura()
{
    Date = DateTime.Now,
    FormaPago = myFactura.FormaPago,
    Cliente = "Cliente prueba 3",

    Detalles = new List<DetallleFactura>()
    {
        new DetallleFactura()
        {
            Articulo = new Articulo()
            {
                Nombre = "AZUCAR",
                PrecioUnitario = 3000
            },
            Cantidad = 2,
            Precio = 3000
        },
        new DetallleFactura()
        {
            Articulo = new Articulo()
            {
                Nombre = "YERBA",
                PrecioUnitario = 3500
            },
            Cantidad = 1,
            Precio = 3500
        }
    }
};
bool resultTransaction = oService.ExecuteTransaction(complexFactura);

if (resultTransaction)
{
    Console.WriteLine("Se ha creado la factura con exito");

}
else
{
    Console.WriteLine("No se ha podido crear la factura");
}
