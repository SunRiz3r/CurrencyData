function downloadFileFromUrl(fileName, url) {
    const link = document.createElement('a');
    link.href = url;
    link.download = fileName;
    link.click();
}