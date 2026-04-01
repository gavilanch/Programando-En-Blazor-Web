window.mostrarPreview = (inputFile, tagImagen) => {
    const url = URL.createObjectURL(inputFile.files[0]);
    tagImagen.addEventListener('load', () => URL.revokeObjectURL(url), { once: true });
    tagImagen.src = url;
}
