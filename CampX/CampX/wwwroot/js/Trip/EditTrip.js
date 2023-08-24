var map = L.map('map').setView([45.9443, 25.0094], 6);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 20,
    attribution: '© OpenStreetMap'
}).addTo(map);

function GenerateRandomCode() {
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
    var randomCode = '';

    for (var i = 0; i < 6; i++) {
        var randomIndex = Math.floor(Math.random() * characters.length);
        randomCode += characters.charAt(randomIndex);
    }

    return randomCode;
}
var url = window.location.href.split('/');
var id = url[url.length - 1];
var tentIcon = L.icon({
    iconUrl: 'https://localhost:44364/Images/icons8-tent-48.png',

    iconSize: [40, 40], // size of the icon

    iconAnchor: [20, 20], // point of the icon which will correspond to marker's location
});

$.ajax({
    type: "get",
    url: "/Trip/DisplayCampsites",
    datatype: "json",
})
    .done((campsites) => {

        var currentCamper = document.getElementById('currentCamper')
        var nameInput = document.getElementById('nameInput')
        nameInput.value = ''
        var descriptionInput = document.getElementById('descriptionInput')
        descriptionInput.value = ''
        var publicInput = document.getElementById('visibility')
        var dateInput = document.getElementById('dateInput')

        var submitButton = document.getElementById('submitButton')
        var selectedCampsitesIdsList = []

        
        $.ajax({
            type: "get",
            url: "/Trip/TripDetailsJSON",
            data: { id: id }
        })
            .done((trip) => {

                trip.campsites.forEach((campsite) => {


                    selectedCampsitesIdsList.push(campsite.id)
                    marker = document.getElementById(`marker${campsite.id}`)
                    map.removeLayer(marker)
                    var campsiteDiv = document.createElement('div')
                    var img = document.createElement('img')
                    img.src = 'https://localhost:44364/Images/icons8-tent-48.png'
                    img.height = 60
                    img.width = 60
                    img.alt = 'Tent Icon'
                    var minusIcon = document.createElement('i')
                    minusIcon.className = 'fas fa-times'
                    img.onclick = () => {

                        window.location.href = '../Map/CampsiteDetails/' + campsite.id;
                    }
                    var campsiteName = document.createElement('span')
                    nightImg = document.createElement('img')
                    nightImg.className = 'nightIcon'
                    nightImg.src = 'https://localhost:44364/Images/night_624106.png'
                    nightImg.height = 25
                    nightImg.width = 25
                    nightImg.alt = 'Night Icon'
                    nightsSpan = document.createElement('span')
                    nightsSpan.id = `nightspan${campsite.id}`
                    numberOfNights += 1
                    nightsSpan.appendChild(nightImg)

                    addNight = document.createElement('i')
                    addNight.className = 'fas fa-plus'
                    deleteNight = document.createElement('i')
                    deleteNight.className = 'fas fa-minus'

                    for (var i = 0; i < numberOfNights; i++) {
                        e.target.previousSibling.appendChild(nightImg.cloneNode(true))

                    }


                    campsiteName.innerHTML = ` ${campsite.name} `
                    campsiteDiv.appendChild(minusIcon)
                    campsiteDiv.appendChild(img)
                    campsiteDiv.appendChild(campsiteName)
                    campsiteDiv.appendChild(nightsSpan)
                    campsiteDiv.appendChild(addNight)
                    campsiteDiv.appendChild(deleteNight)

                    selectedCampsites.appendChild(campsiteDiv)
                    minusIcon.onclick = () => {
                        selectedCampsites.removeChild(campsiteDiv)
                        marker.addTo(map)
                        const index = selectedCampsitesIdsList.indexOf(campsite.id);
                        if (index > -1) {

                            selectedCampsitesIdsList.splice(index, 1);
                        }
                    }
                })
            })
    })
   
       /* campsites.forEach(campsite => {


            var marker = L.marker([campsite.latitude, campsite.longitude], { icon: tentIcon }).addTo(map)
            //marker.id = `marker${campsite.id}`
           // var selectedCampsites = document.getElementById("selectedCampsites")

            *//*marker.on('click', function () {

                selectedCampsitesIdsList.push(campsite.id)
                map.removeLayer(marker)
                campsiteDiv = document.createElement('div')
                img = document.createElement('img')
                img.src = 'https://localhost:44364/Images/icons8-tent-48.png'
                img.height = 60
                img.width = 60
                img.alt = 'Tent Icon'
                minusIcon = document.createElement('i')
                minusIcon.className = 'fas fa-times'
                img.onclick = () => {

                    window.location.href = '../Map/CampsiteDetails/' + campsite.id;
                }
                campsiteName = document.createElement('span')
                nightImg = document.createElement('img')
                nightImg.className = 'nightIcon'
                nightImg.src = 'https://localhost:44364/Images/night_624106.png'
                nightImg.height = 25
                nightImg.width = 25
                nightImg.alt = 'Night Icon'
                nightsSpan = document.createElement('span')
                nightsSpan.id = `nightspan${campsite.id}`
                numberOfNights += 1
                nightsSpan.appendChild(nightImg)

                addNight = document.createElement('i')
                addNight.className = 'fas fa-plus'
                deleteNight = document.createElement('i')
                deleteNight.className = 'fas fa-minus'

                addNight.onclick = (e) => {

                    numberOfNights += 1
                    e.target.previousSibling.appendChild(nightImg.cloneNode(true))
                    console.log(numberOfNights)
                }
                deleteNight.onclick = (e) => {

                    const nightsSpan = document.getElementById(`nightspan${campsite.id}`)

                    if (nightsSpan.children.length > 1) {
                        numberOfNights -= 1
                        nightsSpan.removeChild(nightsSpan.children[nightsSpan.children.length - 1]);
                    }
                    console.log(numberOfNights)
                }

                campsiteName.innerHTML = ` ${campsite.name} `
                campsiteDiv.appendChild(minusIcon)
                campsiteDiv.appendChild(img)
                campsiteDiv.appendChild(campsiteName)
                campsiteDiv.appendChild(nightsSpan)
                campsiteDiv.appendChild(addNight)
                campsiteDiv.appendChild(deleteNight)

                selectedCampsites.appendChild(campsiteDiv)
                *//*minusIcon.onclick = () => {
                    selectedCampsites.removeChild(campsiteDiv)
                    marker.addTo(map)
                    const index = selectedCampsitesIdsList.indexOf(campsite.id);
                    if (index > -1) {

                        selectedCampsitesIdsList.splice(index, 1);
                    }
                }*//*
            })*//*

        })*/

        
        var codeInput = GenerateRandomCode()
        let span = document.createElement('span')
        span.style.color = 'red'
        document.getElementById('tripForm').appendChild(span)
        span.style.display = 'none'
        submitButton.onclick = () => {

            if (selectedCampsitesIdsList.length == 0) {
                span.innerText = 'Selecteaza de pe harta campsite-urile unde vrei sa mergi!'
                span.style.display = 'block'
            }
            else if (nameInput.value === '') {
                span.innerText = 'Seteaza numele'
                span.style.display = 'block'
            }
            else if (publicInput.value === "") {
                span.innerText = 'Seteaza daca vrei ca tripul tau sa fie public sau privat'
                span.style.display = 'block'
            }
            else {
                $.ajax({
                    type: "post",
                    url: `/Trip/AddTrip`,
                    datatype: "json",
                    data: {
                        Name: nameInput.value
                        , Description: descriptionInput.value
                        , IsPublic: publicInput.value == 'public'
                        , Date: dateInput.value
                        , Code: codeInput
                        , Campsites: selectedCampsitesIdsList
                        , TripCampers: [currentCamper.value]
                        , Nights: numberOfNights
                    }
                })
                    .done(() => {

                        window.location.reload()
                    })
            }
        }

    })
