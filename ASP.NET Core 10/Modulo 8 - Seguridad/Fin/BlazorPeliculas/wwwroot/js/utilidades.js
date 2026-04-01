window.mostrarPreview = (inputFile, tagImagen) => {
    const url = URL.createObjectURL(inputFile.files[0]);
    tagImagen.addEventListener('load', () => URL.revokeObjectURL(url), { once: true });
    tagImagen.src = url;
}

window.postearForm = (formId) => {
    const form = document.getElementById(formId);
    if (form) {
        form.submit();
    }
};

window.mostrarAlerta = (title, text, icon) => {
    Swal.fire({
        title,
        text,
        icon
    });
}
