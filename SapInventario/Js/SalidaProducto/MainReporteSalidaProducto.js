
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






document.addEventListener('DOMContentLoaded', () => {

    btnGeneraPdf.addEventListener('click', generaPdf);
    llenarListadoProductos(objProductoEntrega);
    fijarIndividualizacion(objIndividuoEntrega);
    fijarFechaReporte();
});

console.log(objProductoEntrega);
console.log(objIndividuoEntrega);

function llenarListadoProductos(items) {
    while (listadoProductoEntrega.firstChild) {
        listadoProductoEntrega.removeChild(listadoProductoEntrega.firstChild);
    };

    items.forEach(async item => {
        const { codSap, NombreProd, cantidad, almacen } = item;

        const trListadoProductos = document.createElement('tr');

        const tdCodigo = document.createElement('td');
        tdCodigo.innerHTML = `<p class=""> ${codSap} (${almacen}) </p>`;

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
    const mes = diaActual.getMonth();
    const agno = diaActual.getFullYear();

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

function fijarIndividualizacion(items) {
    const { entregadoPor, observaciones, recepcionadoPor } = items;
    lbRecepcionadoPor.textContent = recepcionadoPor;
    lbEntregadoPorFirma.textContent = entregadoPor;
    lbRecepcionadoPorFirma.textContent = recepcionadoPor;
    pObservaciones.textContent = observaciones;
}