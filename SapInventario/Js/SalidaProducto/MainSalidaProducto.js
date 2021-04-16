const txtCodSap = document.querySelector('#txtCodSap');
const btnBuscar = document.querySelector('#btnBuscar');
const detalleProducto = document.querySelector('#detalleProducto');
const ulListadoSalida = document.querySelector('#ulListadoSalida');
const btnIndividualiza = document.querySelector('#btnIndividualiza');
const btnGeneraOrden = document.querySelector('#btnGeneraOrden');
const txtRecepcionadoPor = document.querySelector('#txtRecepcionadoPor');
const txtEntregadoPor = document.querySelector('#txtEntregadoPor');
const txtObservaciones = document.querySelector('#txtObservaciones');
const modalDatosEntrega = document.querySelector('#modalDatosEntrega');
const txtUnidad = document.querySelector('#txtUnidad');
const txtCentroCosto = document.querySelector('#txtCentroCosto');
const txtGradoRecibe = document.querySelector('#txtGradoRecibe');
const txtGradoEntrega = document.querySelector('#txtGradoEntrega');



let idInputsCantidad = [];

document.addEventListener('DOMContentLoaded', () => {
    btnBuscar.addEventListener('click', buscarProducto);
    btnIndividualiza.addEventListener('click', individualizaEntrega);
    btnGeneraOrden.addEventListener('click', generarEntrega);
    //localStorage.clear();

});

async function buscarProducto() {
    if (txtCodSap.value) {
        const EndPoint = '/SalidaProducto/ObtenerListadoProductosPorPalabraClave';

        const palabraClave = `${txtCodSap.value}%`;
        const codigoClave = txtCodSap.value;
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
                //console.log(response.data);
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
            title: 'Debe ingresar código o nombre de producto!',
            text: "Ingrese parametros para buscar",
            icon: 'warning',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Aceptar'
        })
    }
}

function llenarTablaListadoProducto(items) {
    while (detalleProducto.firstChild) {
        detalleProducto.removeChild(detalleProducto.firstChild);
    };

    items.forEach(async item => {
        const { CodigoSap, NombreProducto } = item;
        const stock = await buscarStockProducto(CodigoSap);
        stock.forEach(item => {
            const { Id, CodigoAlmacen, Stock } = item;

            const trListadoProductos = document.createElement('tr');

            const tdCodigo = document.createElement('td');
            tdCodigo.innerHTML = `<p class=""> ${CodigoSap} </p>`;

            const tdNombre = document.createElement('td');
            tdNombre.innerHTML = `<p class="">${NombreProducto}</p>`;

            const tdStock = document.createElement('td');
            tdStock.innerHTML = `<p class="">${Stock}</p>`;

            const tdAlmacen = document.createElement('td');
            tdAlmacen.innerHTML = `<p class="">${CodigoAlmacen}</p>`;

            const tdButton = document.createElement('td');
            const btnSeleccionar = document.createElement('button');
            btnSeleccionar.innerHTML = 'Seleccionar';
            btnSeleccionar.onclick = () => fijarProductoEntrega(CodigoSap, NombreProducto, CodigoAlmacen, Id, Stock);
            btnSeleccionar.classList.add("btn", "btn-danger");
            tdButton.appendChild(btnSeleccionar);

            trListadoProductos.appendChild(tdCodigo);
            trListadoProductos.appendChild(tdNombre);
            trListadoProductos.appendChild(tdStock);
            trListadoProductos.appendChild(tdAlmacen);
            trListadoProductos.appendChild(tdButton);

            detalleProducto.appendChild(trListadoProductos);
        })
    });
};

function fijarProductoEntrega(codigoSap, NombreProd, almacen, Id, Stock) {
    txtCodSap.value = "";
    txtCodSap.focus();
    const existeProducto = ulListadoSalida.classList.contains(`${codigoSap}`);
    if (!existeProducto) {

        idInputsCantidad.push({ codSap: `${codigoSap}`, idInput: `cod${codigoSap}`, almacen: `${almacen}`, NombreProd: `${NombreProd}`, Id: `${Id}`, Stock: `${Stock}` });
        console.log(idInputsCantidad);

        ulListadoSalida.classList.add(`${codigoSap}`);

        const divContenedor = document.createElement('div');
        divContenedor.classList.add('row', 'col-md-12', 'justify-content-between');
        divContenedor.dataset.codsap = `${codigoSap}`;

        const divProd = document.createElement('div');
        divProd.classList.add('row', 'col-md-5', 'p-2', 'justify-content-center');

        const liCodigoSap = document.createElement('li');
        liCodigoSap.classList.add('list-group-item', 'list-group-item-action', 'list-group-item-success', 'col-md-12', 'p-2', 'text-center', `${codigoSap}`);
        liCodigoSap.style.borderRadius = '8px';
        liCodigoSap.textContent = `${codigoSap} ${NombreProd}`;
        divProd.appendChild(liCodigoSap)

        const divInputCantidad = document.createElement('div');
        divInputCantidad.classList.add('row', 'col-md-6', 'p-2', 'justify-content-center');

        const inputCantidad = document.createElement('input');
        inputCantidad.type = 'number';
        inputCantidad.addEventListener('blur', () => verificaStockProducto(`${codigoSap}`, `${NombreProd}`, `${Id}`));
        inputCantidad.placeholder = 'Cantidad';
        inputCantidad.style.width = '50%';
        inputCantidad.style.borderRadius = '8px';
        inputCantidad.classList.add('form-control');
        inputCantidad.classList.add('p-1', `${codigoSap}`);
        inputCantidad.id = `cod${codigoSap}`;
        divInputCantidad.appendChild(inputCantidad);

        const divBtnQuitar = document.createElement('div');
        divBtnQuitar.classList.add('row', 'col-md-1', 'p-2', 'justify-content-center');
        const btnQuitar = document.createElement('button');
        btnQuitar.classList.add('btn', 'btn-danger');
        btnQuitar.textContent = 'Quitar';
        btnQuitar.id = `${codigoSap}`;
        btnQuitar.onclick = () => quitarProductoLista(codigoSap);
        divBtnQuitar.appendChild(btnQuitar);


        divContenedor.appendChild(divProd);
        divContenedor.appendChild(divInputCantidad);
        divContenedor.appendChild(divBtnQuitar);

        ulListadoSalida.appendChild(divContenedor);
    }
    else {
        Swal.fire({
            title: 'El producto ya fue agregado a la lista!',
            text: "Seleccione otro producto para continuar",
            icon: 'warning',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Aceptar'
        })
    }
}

function quitarProductoLista(idProducto) {
    const elemento = document.querySelector(`[data-codsap="${idProducto}"]`);
    idInputsCantidad = idInputsCantidad.filter(producto => producto.codSap !== idProducto);
    elemento.remove();
    ulListadoSalida.classList.remove(`${idProducto}`);
}

async function buscarStockProducto(CodigoSap) {

    const EndPoint = '/SalidaProducto/ObtenerStockTotalProductoPorCodigoSap';

    const data = { CodigoSap };

    try {
        const request = await fetch(EndPoint, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        const response = await request.json();
        return response.data;

    } catch (error) {
        console.log(error)
    }
}

async function buscarAlmacen(codigoSap) {

    const EndPoint = '/SalidaProducto/ObtenerAlmacenProductoExistenteDistribucion';

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
        return response.data;

    } catch (error) {
        console.log(error)
    }
}

async function verificaStockProducto(CodigoSap, NombreProd, Id) {
    const inputStockIngresado = document.querySelector(`#cod${CodigoSap}`);
    if (inputStockIngresado.value !== "") {

        const stock = await buscarStockProductoPorIdRegistro(Id);
        console.log(stock);

        const stockInput = parseInt(inputStockIngresado.value);
        if (parseInt(stock) < stockInput) {
            Swal.fire({
                title: 'Stock Insuficiente!',
                html: `El stock de <strong>${NombreProd}</strong> es de <strong>${stock} unidades</strong>, ingrese una cantidad válida`,
                icon: 'warning',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Aceptar'
            })
            inputStockIngresado.focus();

            inputStockIngresado.value = "";
        }
    }
}

async function buscarStockProductoPorIdRegistro(idRegistro) {

    const EndPoint = '/SalidaProducto/ObtenerStockTotalProductoPorId';

    const data = { idRegistro };

    try {
        const request = await fetch(EndPoint, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        const response = await request.json();
        return response.data;

    } catch (error) {
        console.log(error)
    }
}

function generarEntrega() {
    if (txtRecepcionadoPor.value == "" || txtEntregadoPor.value == "" || txtObservaciones.value == "" || txtUnidad.value == "" || txtCentroCosto.value == "" || txtGradoRecibe.value == "" || txtGradoEntrega.value == "") {
        Swal.fire({
            title: 'Todos los campos son obligatorios!',
            text: "Ingrese datos para individualizar la entrega",
            icon: 'warning',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Aceptar'
        })
    }
    else {
        Swal.fire({
            title: 'Esta seguro de generar la entrega??',
            text: "Se descontará la entrega del stock si continua",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Aceptar'
        }).then(async (result) => {
            if (result.isConfirmed) {
                const recepcionadoPor = txtRecepcionadoPor.value;
                const entregadoPor = txtEntregadoPor.value;
                const observaciones = txtObservaciones.value;
                const unidad = txtUnidad.value;
                const centroCosto = txtCentroCosto.value;
                const gradoRecibe = txtGradoRecibe.value;
                const gradoEntrega = txtGradoEntrega.value;
                const idRegistro = idInputsCantidad[0].Id;
                const stockActualizar = idInputsCantidad[0].Stock;
                const generado = false;

                const objIndividualiza = {
                    recepcionadoPor,
                    entregadoPor,
                    observaciones,
                    unidad,
                    centroCosto,
                    gradoRecibe,
                    gradoEntrega,
                    idRegistro,
                    generado
                };

                $('#modalDatosEntrega').modal('hide');
                setTimeout(() => {
                    const actualizaStock = parseInt(stockActualizar) - 1;
                    ActualizarStockAlmacenPorIdRegistro(idRegistro, actualizaStock);
                    localStorage.setItem('objIndividuoEntrega', JSON.stringify(objIndividualiza));
                    window.open('/SalidaProducto/ReporteSalidaProducto');
                    //location.reload();
                }, 500);

            }
        })
    }
};

function individualizaEntrega() {
    if (idInputsCantidad.length === 0) {
        Swal.fire({
            title: 'No se han agregado Productos!',
            text: "Ingrese productos a la entrega para continuar",
            icon: 'warning',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Aceptar'
        })
    } else {
        let productosEntrega = [];

        idInputsCantidad.forEach(item => {
            const { codSap, idInput, almacen, NombreProd } = item;
            const cantidad = parseInt(document.querySelector(`#${idInput}`).value);
            productosEntrega.push({ codSap, cantidad, almacen, NombreProd });
        });

        const cantidadVacia = productosEntrega.some(producto => {
            return producto.cantidad === "" || producto.cantidad === 0
        });

        if (cantidadVacia) {
            Swal.fire({
                title: 'Debe ingresar una cantidad a entregar!',
                text: "Uno o varios de los productos para entrega detalla cantidad en cero o en blanco",
                icon: 'warning',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Aceptar'
            })
        } else {
            $('#modalDatosEntrega').modal('show');
            localStorage.setItem('objProductoEntrega', JSON.stringify(productosEntrega));
        }
    }
}

async function ActualizarStockAlmacenPorIdRegistro(idRegistro, stock) {

    const EndPoint = '/SalidaProducto/ActualizarStockAlmacenPorIdRegistro';

    const data = { idRegistro, stock };

    try {
        const request = await fetch(EndPoint, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        const response = await request.json();
        console.log(response.data);

    } catch (error) {
        console.log(error)
    }
}