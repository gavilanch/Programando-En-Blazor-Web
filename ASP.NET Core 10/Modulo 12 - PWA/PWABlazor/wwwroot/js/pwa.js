let serviceWorkerActual = null;
let nuevoServiceWorker = null;
let dotNetRef = null;

async function revisarSiHayNuevaVersionDisponible() {
    await serviceWorkerActual.update();

    if (serviceWorkerActual.waiting) {
        await notificarNuevaVersion(serviceWorkerActual.waiting);
        return;
    }

    if (serviceWorkerActual.installing) {
        escucharInstalacion(serviceWorkerActual.installing);
    }
}

async function notificarNuevaVersion(worker) {
    nuevoServiceWorker = worker;
    await dotNetRef.invokeMethodAsync('NuevaVersionDisponible');
}

function escucharInstalacion(worker) {
    worker.addEventListener('statechange', async () => {
        if (worker.state === 'installed' && navigator.serviceWorker.controller) {
            await notificarNuevaVersion(worker);
        }
    });
}

window.configurarRevisionActualizacionPWA = async function (helper) {
    dotNetRef = helper;

    serviceWorkerActual = await navigator.serviceWorker.register('service-worker.js');

    if (serviceWorkerActual.waiting) {
        await notificarNuevaVersion(serviceWorkerActual.waiting);
    }

    if (serviceWorkerActual.installing) {
        escucharInstalacion(serviceWorkerActual.installing);
    }

    serviceWorkerActual.addEventListener('updatefound', () => {
        escucharInstalacion(serviceWorkerActual.installing);
    });

    document.addEventListener('visibilitychange', async () => {
        if (document.visibilityState === 'visible') {
            await revisarSiHayNuevaVersionDisponible();
        }
    });

    window.addEventListener('online', async () => {
        await revisarSiHayNuevaVersionDisponible();
    });

    setInterval(revisarSiHayNuevaVersionDisponible, 15 * 1000);
}

window.actualizarPWA = function () {
    nuevoServiceWorker.postMessage({ type: 'SKIP_WAITING' });
}

