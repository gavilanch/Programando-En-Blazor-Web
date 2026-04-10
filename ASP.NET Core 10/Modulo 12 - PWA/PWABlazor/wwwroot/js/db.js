const DB_NAME = "pwablazor";
const STORE_NAME = "peticiones";
const DB_VERSION = 1;


function abrirDB() {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(DB_NAME, DB_VERSION);

        request.onupgradeneeded = event => {
            const db = event.target.result;

            if (!db.objectStoreNames.contains(STORE_NAME)) {
                db.createObjectStore(STORE_NAME, {
                    keyPath: "id",
                    autoIncrement: true
                });
            }
        };

        request.onsuccess = () => resolve(request.result);
        request.onerror = () => reject(request.error);
    });
}

async function agregarPeticion(data) {
    const db = await abrirDB();

    return new Promise((resolve, reject) => {
        const transaccion = db.transaction(STORE_NAME, "readwrite");
        const tabla = transaccion.objectStore(STORE_NAME);
        const peticion = tabla.add({
            ...data,
            createdAt: new Date().toISOString()
        });

        peticion.onsuccess = () => resolve();
        peticion.onerror = () => reject(peticion.error);
    });
}

async function obtenerPeticiones() {
    const db = await abrirDB();

    return new Promise((resolve, reject) => {
        const transaccion = db.transaction(STORE_NAME, "readonly");
        const tabla = transaccion.objectStore(STORE_NAME);
        const peticion = tabla.getAll();

        peticion.onsuccess = () => resolve(peticion.result);
        peticion.onerror = () => reject(peticion.error);
    });
}

async function borrarPeticion(id) {
    const db = await abrirDB();

    return new Promise((resolve, reject) => {
        const transaccion = db.transaction(STORE_NAME, "readwrite");
        const tabla = transaccion.objectStore(STORE_NAME);
        const peticion = tabla.delete(id);

        peticion.onsuccess = () => resolve();
        peticion.onerror = () => reject(peticion.error);
    });
}


