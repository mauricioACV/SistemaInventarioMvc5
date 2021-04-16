//Selectores
const lbCodigoSap = document.querySelector('#lbCodigoSap');
const ddlAlmacen = document.querySelector('#ddlAlmacen');
const txtStockProducto = document.querySelector('#txtStockProducto');
const txtValorUnitario = document.querySelector('#txtValorUnitario');
const btnRegistrar = document.querySelector('#btnRegistrar');
const ddlUbicacion = document.querySelector('#ddlUbicacion');
const txtBusquedaProducto = document.querySelector('#txtBusquedaProducto');
const btnBuscar = document.querySelector('#btnBuscar');
const RestultBusquedaProducto = document.querySelector('#RestultBusquedaProducto');
const lbNombreProducto = document.querySelector('#lbNombreProducto');
const txtNumOrdCompra = document.querySelector('#txtNumOrdCompra');


//Cuando esta cargado el html
document.addEventListener('DOMContentLoaded', async () => {
    await ObtenerListaAlmacenes();
    btnRegistrar.addEventListener('click', obtenerDatosFormulario);
    btnBuscar.addEventListener('click', ObtenerListadoProductosPorPalabraClave);
    ddlAlmacen.addEventListener('change', ObtenerUbicacionAlmacenPorCodigoAlmacen);
    ddlAlmacen.addEventListener('change', buscarAlmacen)
});


function obtenerDatosFormulario() {
    if (lbCodigoSap.value && ddlAlmacen.value && txtStockProducto.value && txtValorUnitario.value && txtNumOrdCompra.value) {
        const datosFormulario = {
            CodigoSap: lbCodigoSap.value,
            NumOrdCompra: txtNumOrdCompra.value,
            CodigoAlmacen: ddlAlmacen.value,
            CodigoUbicacion: ddlUbicacion.value,
            Stock: txtStockProducto.value,
            ValorUnitario: txtValorUnitario.value,
            FechaCompra: new Date()
        }

        RegistrarProducto(datosFormulario);
    } else {
        Swal.fire({
            title: 'Debe Ingresar todos los Datos',
            text: "Hay campos vacios!",
            icon: 'warning',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Aceptar'
        })
    }

}

async function RegistrarProducto(inventarioProducto) {

    Swal.fire({
        title: 'Esta seguro de guardar los datos??',
        text: "Se guardaran los cambios si acepta",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Aceptar'
    }).then(async (result) => {
        if (result.isConfirmed) {

            const EndPoint = '/InventarioProducto/RegistrarProducto';
            const data = { inventarioProducto };

            try {
                const request = await fetch(EndPoint, {
                    method: 'POST',
                    body: JSON.stringify(data),
                    headers: {
                        'Content-Type': 'application/json'
                    }
                });

                const response = await request.json();
                console.log(response);
                if (response) {
                    Swal.fire({
                        title: 'Exito!',
                        text: "Los datos se guardaron correctamente",
                        icon: 'success',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Aceptar'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            location.reload();
                        }
                    })

                } else {
                    Swal.fire({
                        title: 'Falló el registro',
                        text: "Algo salió mal, contacte al Administrador!",
                        icon: 'warning',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Aceptar'
                    })
                }

            } catch (error) {
                console.log(error)
            }
        }
    })
};

async function ObtenerListaAlmacenes() {
    const EndPoint = '/InventarioProducto/ObtenerListaAlmacenes';

    try {
        const request = await fetch(EndPoint, {
            method: 'POST',
        });

        const response = await request.json();
        llenarSelectAlmacen(response.data);

    } catch (error) {
        console.log(error)
    }
};

function llenarSelectAlmacen(items) {
    items.forEach(item => {
        const { CodigoAlmacen, NombreAlmacen } = item;
        const optionSelectAlmacen = document.createElement('option');
        optionSelectAlmacen.value = CodigoAlmacen;
        optionSelectAlmacen.textContent = NombreAlmacen;
        ddlAlmacen.appendChild(optionSelectAlmacen);
    });
};

async function ObtenerListadoProductosPorPalabraClave() {

    if (txtBusquedaProducto.value) {
        const palabraClave = `${txtBusquedaProducto.value}%`;
        const codigoClave = txtBusquedaProducto.value;
        console.log(palabraClave);
        console.log(codigoClave);
        const EndPoint = '/InventarioProducto/ObtenerListadoProductosPorPalabraClave';
        const data = { codigoClave, palabraClave };

        try {
            const request = await fetch(EndPoint, {
                method: 'POST',
                body: JSON.stringify(data),
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            const response = await request.json();
            if (response.data.length) {
                llenarTablaListadoProducto(response.data);
            } else {
                Swal.fire({
                    title: 'El producto que busca no existe!',
                    text: "Intente con otro parametro de búsqueda",
                    icon: 'warning',
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Aceptar'
                })
            }
        } catch (error) {
            console.log(error)
        }
    } else {
        Swal.fire({
            title: 'Escriba un código o nombre de producto',
            text: "Hay campos vacios!",
            icon: 'warning',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Aceptar'
        });
    };
};

function llenarTablaListadoProducto(items) {
    while (RestultBusquedaProducto.firstChild) {
        RestultBusquedaProducto.removeChild(RestultBusquedaProducto.firstChild);
    };

    items.forEach(item => {
        const { CodigoSap, NombreProducto } = item;

        const trListadoProductos = document.createElement('tr');

        const tdCodigo = document.createElement('td');
        tdCodigo.innerHTML = `<p class=""> ${CodigoSap} </p>`;

        const tdNombre = document.createElement('td');
        tdNombre.innerHTML = `<p class="">${NombreProducto}</p>`;

        const tdButton = document.createElement('td');
        const btnSeleccionar = document.createElement('button');
        btnSeleccionar.innerHTML = 'Seleccionar';
        btnSeleccionar.onclick = () => fijarCodigSap(CodigoSap, NombreProducto);
        btnSeleccionar.classList.add("btn", "btn-danger");
        tdButton.appendChild(btnSeleccionar);
        trListadoProductos.appendChild(tdCodigo);
        trListadoProductos.appendChild(tdNombre);
        trListadoProductos.appendChild(tdButton);

        RestultBusquedaProducto.appendChild(trListadoProductos);
    });
};

function fijarCodigSap(CodigoSap, NombreProducto) {
    lbCodigoSap.value = `${CodigoSap}`;
    lbNombreProducto.textContent = `${NombreProducto}`;
}

async function ObtenerUbicacionAlmacenPorCodigoAlmacen() {
    const codigoAlmacen = ddlAlmacen.value;
    console.log(codigoAlmacen);
    const EndPoint = '/InventarioProducto/ObtenerUbicacionAlmacenPorCodigoAlmacen';
    const data = { codigoAlmacen };

    try {
        const request = await fetch(EndPoint, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        const response = await request.json();
        llenarSelectUbicacionAlmacen(response.data);

    } catch (error) {
        console.log(error)
    }
};

function llenarSelectUbicacionAlmacen(items) {
    while (ddlUbicacion.firstChild) {
        ddlUbicacion.removeChild(ddlUbicacion.firstChild);
    }
    items.forEach(item => {
        const { CodigoUbicacion } = item;
        const optionSelectUbicacionAlmacen = document.createElement('option');
        optionSelectUbicacionAlmacen.value = CodigoUbicacion;
        optionSelectUbicacionAlmacen.textContent = CodigoUbicacion;
        ddlUbicacion.appendChild(optionSelectUbicacionAlmacen);
    });
};

async function buscarAlmacen() {
    const codigoSap = lbCodigoSap.value;
    if (codigoSap !== "") {
        const EndPoint = '/InventarioProducto/ObtenerAlmacenProductoExistenteDistribucion';

        const data = { codigoSap };

        try {
            const request = await fetch(EndPoint, {
                method: 'POST',
                body: JSON.stringify(data),
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            const response = await request.json();
            verificaSeleccionAlmacen(response.data);

        } catch (error) {
            console.log(error)
        }
    } else {
        Swal.fire({
            title: 'No ha seleccionado un Producto',
            text: 'Por favor seleccione un producto antes de seleccionar Almacén',
            icon: 'warning',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Aceptar'
        })
        ddlAlmacen.value = "";
    }

}

async function verificaSeleccionAlmacen(codAlmacenServer) {
    const codigoAlmacenSelecc = ddlAlmacen.value;
    if (codAlmacenServer !== 0) {
        if (codAlmacenServer !== parseInt(codigoAlmacenSelecc)) {
            Swal.fire({
                title: 'El producto ya tiene almacen!',
                text: `El producto se encuentra en el almacén: ${codAlmacenServer}, por favor cambie el almacén`,
                icon: 'warning',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Aceptar'
            })
        }
    }

}