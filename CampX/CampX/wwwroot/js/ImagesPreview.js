
var fileInput = document.getElementById("ImageData")
imageContainer = document.getElementById("imgContainer")
/*images = []*/
fileInput.addEventListener('change', function () {

//    imageContainer.innerHTML = '';
    for (const file of this.files) {
        const imageUrl = URL.createObjectURL(file);

      //  images.push(file.src)
        const imageContainerDiv = document.createElement('div');
        imageContainerDiv.classList.add('imgPreview');


        const imageElement = document.createElement('img');
        imageElement.src = imageUrl;


        const deleteButton = document.createElement('span');
        deleteButton.classList.add('deleteButton');
        deleteButton.innerText = 'Delete';


        deleteButton.addEventListener('click', function () {

            imageContainerDiv.remove();
        });
        imageContainerDiv.appendChild(imageElement);
        imageContainerDiv.appendChild(deleteButton);


        imageContainer.appendChild(imageContainerDiv);
    }
})

submitButton = document.getElementById("submitButton")

/*submitButton.onclick = () => {
    fileInput.value = images
}*/