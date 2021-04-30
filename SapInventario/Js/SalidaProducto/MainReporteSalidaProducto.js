
const objProductoEntrega = JSON.parse(localStorage.getItem('objProductoEntrega')) || [];
const objIndividuoEntrega = JSON.parse(localStorage.getItem('objIndividuoEntrega')) || [];


const listadoProductoEntrega = document.querySelector('#listadoProductoEntrega');
const lblDia = document.querySelector('#lblDia');
const lblMes = document.querySelector('#lblMes');
const lblAgno = document.querySelector('#lblAgno');
const html2Pdf = document.querySelector("#zonaImpresion");
const btnGeneraPdf = document.querySelector('#btnGeneraPdf');

const lbRecepcionadoPor = document.querySelector('#lbRecepcionadoPor');
const lbEntregadoPorFirma = document.querySelector('#lbEntregadoPorFirma');
const lbRecepcionadoPorFirma = document.querySelector('#lbRecepcionadoPorFirma');
const pObservaciones = document.querySelector('#pObservaciones');

const lbCortesiaEntrega = document.querySelector('#lbCortesiaEntrega');
const lbCortesiaRecepciona = document.querySelector('#lbCortesiaRecepciona');
const lbUnidad = document.querySelector('#lbUnidad');
const numActa = document.querySelector('#numActa');





document.addEventListener('DOMContentLoaded', () => {

    btnGeneraPdf.addEventListener('click', generaPdf);
    fijarFechaReporte();
    llenarListadoProductos(objProductoEntrega);
    fijarIndividualizacion(objIndividuoEntrega);
});

function llenarListadoProductos(items) {
    while (listadoProductoEntrega.firstChild) {
        listadoProductoEntrega.removeChild(listadoProductoEntrega.firstChild);
    };

    items.forEach(async item => {
        const { CodigoSap, NombreProd, cantidad, almacen } = item;

        const trListadoProductos = document.createElement('tr');

        const tdCodigo = document.createElement('td');
        tdCodigo.innerHTML = `<p class=""> ${CodigoSap} (${almacen}) </p>`;

        const tdDescripcion = document.createElement('td');
        tdDescripcion.innerHTML = `<p class="">${NombreProd} (${almacen}) </p>`;

        const tdUnd = document.createElement('td');
        tdUnd.innerHTML = `<p class="">Und</p>`;

        const tdCantidad = document.createElement('td');
        tdCantidad.innerHTML = `<p class="">${cantidad}</p>`;

        trListadoProductos.appendChild(tdCodigo);
        trListadoProductos.appendChild(tdDescripcion);
        trListadoProductos.appendChild(tdUnd);
        trListadoProductos.appendChild(tdCantidad);

        listadoProductoEntrega.appendChild(trListadoProductos);
    });
};

function fijarFechaReporte() {
    const diaActual = new Date();
    const dia = diaActual.getDate();
    const mes = diaActual.getMonth() + 1;
    const agno = diaActual.getFullYear();

    console.log(diaActual, dia, mes, agno);

    lblDia.textContent = dia < 10 ? `0${dia}` : dia;
    lblMes.textContent = mes < 10 ? `0${mes}` : mes;
    lblAgno.textContent = agno;
}

function generaPdf() {
    html2pdf(html2Pdf, {
        margin: 0,
        filename: 'documento.pdf',
        image: {
            type: 'jpeg',
            quality: 0.95
        },
        html2canvas: {
            scale: 3, //a mayor escala mejores graficos, pero mas peso
            letterRendering: true,
        },
        jsPDF: {
            unit: "mm",
            format: "a4",
            orientation: 'portrait' //lanscape o portrait
        }
    });
}

async function fijarIndividualizacion(items) {

    const { entregadoPor, observaciones, recepcionadoPor, centroCosto, gradoEntrega, gradoRecibe, unidad } = items;
    console.log(objIndividuoEntrega.generado)

    lbRecepcionadoPor.textContent = centroCosto;
    lbEntregadoPorFirma.textContent = entregadoPor;
    lbRecepcionadoPorFirma.textContent = recepcionadoPor;
    pObservaciones.textContent = observaciones;
    lbCortesiaEntrega.textContent = gradoEntrega;
    lbCortesiaRecepciona.textContent = gradoRecibe;
    lbUnidad.textContent = unidad;

    numActa.textContent = await obtenerNumActa();
    if (!objIndividuoEntrega.generado) {
        await registrarSalida();
    }
    objIndividuoEntrega.generado = true;
    localStorage.setItem('objIndividuoEntrega', JSON.stringify(objIndividuoEntrega));

    console.log(objIndividuoEntrega);
}

async function obtenerNumActa() {

    const EndPoint = '/SalidaProducto/ObtenerNumeroActa';


    try {
        const request = await fetch(EndPoint, {
            method: 'POST'
        });

        const response = await request.json();
        return response.data;

    } catch (error) {
        console.log(error)
    }
};

async function registrarSalida() {

    const { centroCosto, entregadoPor, observaciones, recepcionadoPor, unidad } = objIndividuoEntrega;

    const objSalidaProducto = {
        IdUsuarioEntrega: 1,
        UnidadDestino: `${centroCosto}-${unidad}`,
        FechaEntrega: `${lblAgno.textContent}-${lblMes.textContent}-${lblDia.textContent}`,
        RecepcionadoPor: recepcionadoPor,
        Observaciones: observaciones,
        NumActa: numActa.textContent
    }

    objProductoEntrega.forEach(producto => {
        producto.NumActa = numActa.textContent;
    });

    console.log(objSalidaProducto);
    console.log(objProductoEntrega);

    await RegistrarProductosSalida(objProductoEntrega);
    await registrarActa(objSalidaProducto);

}

async function registrarActa(objSalidaProducto) {

    const EndPoint = '/SalidaProducto/RegistrarActa';

    const data = { objSalidaProducto };

    try {
        const request = await fetch(EndPoint, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        const response = await request.json();
        if (response.data) {
            Swal.fire({
                title: 'Exito!',
                text: "El acta fue registrada sin errores",
                icon: 'success',
                showCancelButton: false,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Aceptar'
            }).then((result) => {
                if (result.isConfirmed) {

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

async function RegistrarProductosSalida(listaSalidaProducto) {

    const EndPoint = '/SalidaProducto/RegistrarProductosSalida';

    const data = { listaSalidaProducto };

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

    } catch (error) {
        console.log(error)
    }
}