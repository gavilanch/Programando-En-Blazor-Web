function obtenerCurrentCount() {
    DotNet.invokeMethodAsync("BlazorPeliculas", "ObtenerCurrentCount")
        .then(resultado => {
            console.log(`Conteo desde JS: ${resultado}`);
        })
}

function invocarIncrementCount(dotnetHelper) {
    dotnetHelper.invokeMethodAsync("IncrementCount");
}