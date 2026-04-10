window.estaOnline = function () {
    return navigator.onLine;
}

window.encolar = async function (peticion) {
    await agregarPeticion(peticion);
}

window.addEventListener("online", async () => {
    console.log('devuelta online');
    await window.procesarCola();
});

window.procesarCola = async function () {
    const peticiones = await obtenerPeticiones();

    for (const item of peticiones) {
        try {
            const respuesta = await fetch(item.url, {
                method: item.method,
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(item.body)
            });

            if (respuesta.ok) {
                await borrarPeticion(item.id);
            }
        } catch {
            // Si sigue sin internet o hubo fallo, la dejamos en cola
        }
    }
}
