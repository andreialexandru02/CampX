var map = L.map('map').setView([45.9443, 25.0094], 6);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 20,
    attribution: '© OpenStreetMap'
}).addTo(map);
var currentCamper = document.getElementById('currentCamper')

var url = window.location.href.split('/');
var id = url[url.length - 1].split('?')[0];
console.log(id) 
$.ajax({
    type: "GET",
    url: "/Trip/TripDetailsJSON",
    data: { id: id }
}).done((trip) => {
    
    trip.campsites.forEach((campsite) => {
        console.log(campsite.latitude, campsite.longitude)
        
        marker = L.marker([campsite.latitude, campsite.longitude], { icon: tentIcon }).addTo(map)
        var popupLText = trip.nightsAtCampsite[campsite.id] > 1 ? `${trip.nightsAtCampsite[campsite.id]} nopti!` : 'O noapte!'

        marker.on('mouseover', function (e) {
            console.log(trip.nightsAtCampsite[campsite.id])
            e.target.bindPopup(popupLText, { closeOnClick: false, autoClose: false }).openPopup()
        }) 
        marker.on('mouseout', function (e) {
            console.log(campsite.id)
            //e.target.bindPopup("outfasd", { closeOnClick: false, autoClose: false }).openPopup()
            map.closePopup(e.target.getPopup());
            e.target.unbindPopup(); 
        }) 
        
       //console.log(trip.nightsAtCampsite[campsite.id])
        /*var iframeLink = `https://www.meteoblue.com/en/weather/widget/three?geoloc=manual&lat=${campsite.latitude}&lon=${campsite.longitude}&nocurrent=0&noforecast=0&days=4&tempunit=CELSIUS&windunit=KILOMETER_PER_HOUR&layout=image` 
        var iframe = document.createElement("iframe");
        console.log(iframeLink)
        iframe.src = iframeLink;
        iframe.frameBorder = "0";
        iframe.scrolling = "NO";
        iframe.allowTransparency = "true";
        iframe.sandbox = "allow-same-origin allow-scripts allow-popups allow-popups-to-escape-sandbox";
        iframe.style.width = "460px";
        iframe.style.height = "590px";

        document.getElementById("weatherContainer").appendChild(iframe);*/
        marker.on('click', function () {
            window.location.href = '/Map/CampsiteDetails/' + campsite.id;
        })
    })
    deleteButton = document.getElementById("deleteButton")
    deleteButton.style.display = 'none'
    finishButton = document.getElementById("finishButton")
    finishButton.style.display = 'none'
    editButton = document.getElementById("editButton")
    editButton.style.display = 'none'
    trip.tripCampers.forEach((item) => {
        
        if (item.camper.id == currentCamper.value && item.isOrganizer) {

            deleteButton.style.display = 'block'
            finishButton.style.display = 'block'
            editButton.style.display = 'block'
          // break
        }
        else if (currentCamper.value == 9) {
            deleteButton.style.display = 'block'
        }
        
    })
    var requestDiv = document.getElementById('requestContainer')
    requestDiv.style.display = 'none'
    var requestDescription = document.getElementById('expandableField')
    joinButton = document.getElementById('joinButton')
    var noteDiv = document.getElementById("note-container")
    var isInTrip = false
    trip.tripCampers.forEach((item) => {

        if (item.camper.id == currentCamper.value) {
            isInTrip = true;
        }
    })
    if (isInTrip) {
        joinButton.style.display = 'none'
    }
    else {
        noteDiv.style.display = 'none'
    }
    requestButton = document.getElementById('requestButton')
    var requestSpan = document.createElement('span')
    requestSpan.innerHTML = 'Ai facut deja un request pentru acest trip!'
    requestSpan.style.color = 'red';
    requestDiv.appendChild(requestSpan)
    requestSpan.style.display = 'none'
    
    joinButton.onclick = () => {
        joinButton.style.display = 'none'
        requestDiv.style.display = 'block'
        requestButton.onclick = () => {
            if (requestDescription.value.length > 500) {

                requestSpan.innerHTML = 'Descrierea este prea lunga!'
                requestSpan.style.display = 'block'
            }
            else {

                requestDiv.style.display = 'none'
               //console.log(trip.id)

            
                $.ajax({
                    type: "post",
                    url: '/Request/CheckRequest',
                    data: {
                        TripId: trip.id
                        , CamperId: currentCamper.value
                        , Description: requestDescription.value ?? 'Nu a fost introdusa o descriere!'
                    }
                })  
                    .done((result) => {
                        if (result) {
                            $.ajax({
                                type: "post",
                                url: '/Request/AddRequest',
                                datatype: "json",
                                data: {
                                    TripId: trip.id
                                    , CamperId: currentCamper.value
                                    , Description: requestDescription.value ?? 'Nu a fost introdusa o descriere!'
                                }
                            })
                                .done(() => {
                                    window.location.reload()
                                })
                        }
                        else {                       
                            requestSpan.innerHTML = 'Ai facut deja un request pentru acest trip!'
                            requestSpan.style.display = 'block'
                            requestDiv.style.display = 'block'
                        
                        }
                    }
                )
            }

        }

    }
    
})
//NOTES

$.ajax({
    type: "GET",
    url: "/Note/ShowNotes",
    data: { id: id }
}).done((notes) => {

            var noteDiv = document.getElementById("note-container")
            var plusIcon = document.createElement('i')
            plusIcon.className = "fas fa-plus"
            noteDiv.appendChild(plusIcon)
            var noteInput = document.getElementById("noteInput")
            noteInput.style.display = 'none'
            noteButton = document.getElementById("noteButton")
            var noteContent = document.getElementById("noteExpandableField")
            let span = document.createElement('span')
            span.innerText = 'Notita incompleta'
            span.style.color = 'red'
            noteInput.appendChild(span)
            span.style.display = 'none'
            plusIcon.onclick = () => {

                if (plusIcon.className == 'fas fa-plus') {
                    noteContent.value = ''

                    plusIcon.className = 'fas fa-minus'
                    noteInput.style.display = "block";

                    noteButton.onclick = () => {
                        console.log(noteContent.value)
                        if (noteContent.value == '') {
                            span.innerText = 'Notita incompleta'
                            span.style.display = 'block'
                        }
                        else if (noteContent.value.length > 500) {
                            span.innerText = "Notita prea lunga!"
                            span.style.display = 'block'
                        }
                        else {
                            $.ajax({
                                type: "post",
                                url: `/Note/AddNote`,
                                datatype: "json",
                                data: {
                                    content: noteContent.value,
                                    tripId: id,
                                    camperId: currentCamper.value
                                }
                            })
                                .done(() => {
                                    window.location.reload()
                                })
                        }

                    }
                }
                else {
                    noteInput.style.display = "none";
                    plusIcon.className = 'fas fa-plus'
                }
            }

            notes.forEach(note => {

                var authorized = note.camperId == currentCamper.value
                noteElement = document.createElement('div')
                var contentSpan = document.createElement('span')
                contentSpan.innerText = note.content
                //noteElement.appendChild(document.createElement('br'))
                noteElement.appendChild(contentSpan)
                var deleteIcon = document.createElement('i')
                var editIcon = document.createElement('i')
                editIcon.className = 'fas fa-pencil-alt'
                deleteIcon.className = 'fas fa-times'
                noteElement.appendChild(editIcon)
                noteElement.appendChild(deleteIcon)
                noteElement.className = 'noteElement'
                if (!authorized) {

                    deleteIcon.style.display = 'none'
                    editIcon.style.display = 'none'
                }
                noteDiv.appendChild(noteElement)
                deleteIcon.onclick = () => {

                    if (!authorized) {
                        window.location.href = '/Home/Error_Unauthorized';
                    }
                    else{

                        $.ajax({
                            type: "post",
                            url: `/Note/DeleteNote`,
                            datatype: "json",
                            data: {
                                Id: note.id,
                                TripId: id,
                                Content: noteContent.value,
                                CamperId: currentCamper.value
                            }
                        })
                        .done(() => {
                            window.location.reload()
                        })
                    }
                }

                editIcon.onclick = (e) => {
                    if (!authorized) {
                        window.location.href = '/Home/Error_Unauthorized';
                    }
                    else {

                        noteInput.style.display = "block";
                        plusIcon.className = 'fas fa-minus'
                        noteContent.value = e.target.previousSibling.innerText
                        noteButton.onclick = () => {
                            $.ajax({
                                type: "post",
                                url: `/Note/EditNote`,
                                datatype: "json",
                                data: {
                                    Id: note.id,
                                    Tripid: id,
                                    CamperId: currentCamper.value,
                                    Content: noteContent.value
                                }
                            })
                                .done(() => {
                                    window.location.reload()
                                })
                        }
                    }
                }
            })

})

deleteTripButton = document.getElementById("deleteButton");

deleteTripButton.onclick = () => {
    let result = confirm("Esti sigur ca vrei sa stergi?");
    if (result) {
        deleteTripButton.click()
    }
    else {
        deleteTripButton.href = window.location.href
    }
}

finishTripButton = document.getElementById("finishButton");

finishTripButton.onclick = () => {
    let result = confirm("Esti sigur ca vrei sa finalizezi?");
    if (result) {
        finishTripButton.click()
    }
    else {
        finishTripButton.href = window.location.href
    }
}