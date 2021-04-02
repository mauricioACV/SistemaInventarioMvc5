const txtCodSap = document.querySelector('#txtCodSap');
const btnBuscar = document.querySelector('#btnBuscar');
const detalleProducto = document.querySelector('#detalleProducto');
const ulListadoSalida = document.querySelector('#ulListadoSalida');

document.addEventListener('DOMContentLoaded', () => {
    btnBuscar.addEventListener('click', buscarProducto);
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
        btnSeleccionar.onclick = () => fijarProductoEntrega(CodigoSap, NombreProducto);
        btnSeleccionar.classList.add("btn", "btn-danger");
        tdButton.appendChild(btnSeleccionar);
        trListadoProductos.appendChild(tdCodigo);
        trListadoProductos.appendChild(tdNombre);
        trListadoProductos.appendChild(tdButton);

        detalleProducto.appendChild(trListadoProductos);
    });
};

function fijarProductoEntrega(codigoSap, NombreProd) {
    txtCodSap.value = "";
    txtCodSap.focus();
    const existeProducto = ulListadoSalida.classList.contains(`${codigoSap}`);
    if (!existeProducto) {

        ulListadoSalida.classList.add(`${codigoSap}`);

        const divContenedor = document.createElement('div');
        divContenedor.classList.add('row', 'col-md-12', 'justify-content-between');

        const divProd = document.createElement('div');
        divProd.classList.add('row', 'col-md-6', 'p-2');
        
        const liCodigoSap = document.createElement('li');
        liCodigoSap.classList.add('list-group-item', 'list-group-item-success','col-md-12', 'p-1', 'text-center', `${codigoSap}`);
        liCodigoSap.style.borderRadius = '15px';
        liCodigoSap.textContent = `${codigoSap} ${NombreProd}`;
        divProd.appendChild(liCodigoSap)

        const divBtn = document.createElement('div');
        divBtn.classList.add('row', 'col-md-6', 'p-1');

        const btnSeleccionar = document.createElement('button');
        btnSeleccionar.innerHTML = 'Seleccionar';
        btnSeleccionar.classList.add('col-md-12', 'p-1', 'text-center', `${codigoSap}`);
        divBtn.appendChild(btnSeleccionar);

        divContenedor.appendChild(divProd);
        divContenedor.appendChild(divBtn);

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