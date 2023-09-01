var map = L.map('map').setView([45.9443, 25.0094], 6);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 20,
    attribution: '© OpenStreetMap'
}).addTo(map);
var url = window.location.href.split('/');
var ViewId = url[url.length - 1];
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
    .done((campsites) =>
        $.ajax({
            type: "get",
            url: "/Trip/TripDetailsJSON",
            data: { id: id }
        }).done((trip) => {
            var currentCamper = document.getElementById('currentCamper')
            var nameInput = document.getElementById('nameInput')
            nameInput.value = ''
            var descriptionInput = document.getElementById('descriptionInput')
            descriptionInput.value = ''
            var publicInput = document.getElementById('visibility')
            var dateInput = document.getElementById('dateInput')

            var submitButton = document.getElementById('submitButton')
            var selectedCampsitesIdsList = []
            var selectedCampsites = document.getElementById("selectedCampsites")
            var createCampsiteSelectionDiv = (campsite) => {
                selectedCampsitesIdsList.push(campsite.id)
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
                nightsSpan.appendChild(nightImg)
                if (trip.nightsAtCampsite[campsite.id] == null) {
                    trip.nightsAtCampsite[campsite.id] = 1
                }
                for (var i = 1; i < trip.nightsAtCampsite[campsite.id]; i++) {

                    nightsSpan.appendChild(nightImg.cloneNode(true))
                }
                var addNight = document.createElement('i')
                addNight.className = 'fas fa-plus'
                var deleteNight = document.createElement('i')
                deleteNight.className = 'fas fa-minus'

                addNight.onclick = (e) => {

                    trip.nightsAtCampsite[campsite.id] += 1
                    e.target.previousSibling.appendChild(nightImg.cloneNode(true))
                    console.log(trip.nightsAtCampsite[campsite.id])
                }
                deleteNight.onclick = (e) => {

                    const nightsSpan = document.getElementById(`nightspan${campsite.id}`)
/*
                    if (nightsSpan.children.length > 1) {
                        trip.nightsAtCampsite[campsite.id] -= 1
                        nightsSpan.removeChild(nightsSpan.children[nightsSpan.children.length - 1]);
                    }
                    const nightsSpan = document.getElementById(`nightspan${campsite.id}`)
*/
                    if (nightsSpan.children.length > 1) {
                        trip.nightsAtCampsite[campsite.id] -= 1
                        nightToDelete = nightsSpan.children[nightsSpan.children.length - 1]
                        nightToDelete.classList.add("slide-down");
                        setTimeout(() => {
                            nightsSpan.removeChild(nightToDelete);
                        }, 400);
                    }
                    console.log(trip.nightsAtCampsite[campsite.id])
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

                    campsite.marker.addTo(map)
                    delete trip.nightsAtCampsite[campsite.id]
                    const index = selectedCampsitesIdsList.indexOf(campsite.id);
                    if (index > -1) {

                        selectedCampsitesIdsList.splice(index, 1);
                    }
                }
            }

            var selectedCampsiteIds = trip.campsites.map(c => c.id);
            campsites.filter(c => selectedCampsiteIds.indexOf(c.id) > -1).forEach((campsite) => {

                createCampsiteSelectionDiv(campsite)
                campsite.isInitiallySelected = true;
            })

            //unselectedCampsites = campsites.filter(campsite => !trip.campsites.some(tripCampsite => campsite.id == tripCampsite.id));
            campsites.forEach(campsite => {

                campsite.marker = L.marker([campsite.latitude, campsite.longitude], { icon: tentIcon })
                if (!campsite.isInitiallySelected) {
                    campsite.marker.addTo(map);
                }
                campsite.marker.on('click', function () {
                    map.removeLayer(campsite.marker)
                    createCampsiteSelectionDiv(campsite);
                })
            })
            var participantsDiv = document.getElementById("participantsDiv")
            trip.tripCampers.forEach(participant => {
                var participantSpan = document.createElement('span')
                var soloParticipant = document.createElement('span')
                soloParticipant.innerText = 'Esti singurul participant al acestui trip'
                participantsDiv.appendChild(soloParticipant)
                soloParticipant.style.display = 'none'

                if (trip.tripCampers.length == 1) {
                    soloParticipant.style.display = 'block'
                }
                else {

                    if (!participant.isOrganizer) {
                        
                        participantSpan.innerText = `${participant.camper.firstName} ${participant.camper.lastName}`                      
                        participantsDiv.appendChild(participantSpan)
                    }

                    participantSpan.onclick = () => {
                        participantsDiv.removeChild(participantSpan)
                        trip.tripCampers = trip.tripCampers.filter(obj => obj !== participant);
                        if (trip.tripCampers.length == 1) {
                            soloParticipant.style.display = 'block'
                        }
                    }
                }
                

            })
            var codeInput = GenerateRandomCode()
            let span = document.createElement('span')
            span.style.color = 'red'
            document.getElementById('tripForm').appendChild(span)
            span.style.display = 'none'
            nameInput.value = trip.name
            descriptionInput.value = trip.description
            dateInput.value = new Date(trip.date).toISOString().substr(0, 10);
            trip.isPublic ? document.getElementById('public').selected = true : document.getElementById('privat').selected = true
            submitButton.onclick = (e) => {
                e.preventDefault();
                if (selectedCampsitesIdsList.length == 0) {
                    span.innerText = 'Selecteaza de pe harta campsite-urile unde vrei sa mergi!'
                    span.style.display = 'block'
                }
                else if (nameInput.value === '') {
                    span.innerText = 'Seteaza numele'
                    span.style.display = 'block'
                }
                else if (nameInput.value.length > 100) {
                    span.innerText = 'Numele este prea lung'
                    span.style.display = 'block'
                }
                else if (descriptionInput.value != null && descriptionInput.value.length > 500) {
                    span.innerText = 'Descriere prea lunga'
                    span.style.display = 'block'
                }
                else if (publicInput.value === "") {
                    span.innerText = 'Seteaza daca vrei ca tripul tau sa fie public sau privat'
                    span.style.display = 'block'
                }
                else {
                    $.ajax({
                        type: "post",
                        url: `/Trip/EditTrip`,
                        datatype: "json",
                        data: {
                             Id: ViewId
                            , Name: nameInput.value
                            , Description: descriptionInput.value
                            , IsPublic: publicInput.value == 'public'
                            , Date: dateInput.value
                            , Code: codeInput
                            , Campsites: selectedCampsitesIdsList
                            , TripCampers: trip.tripCampers
                            , NightsAtCampsite: trip.nightsAtCampsite
                             
                        }
                    })
                        .done(() => {

                            window.location.href = '/Trip/TripDetails/' + trip.id;
                        })
                }
            }
        })

    )
