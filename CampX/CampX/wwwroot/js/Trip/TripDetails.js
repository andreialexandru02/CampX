var map = L.map('map').setView([45.9443, 25.0094], 6);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 20,
    attribution: '© OpenStreetMap'
}).addTo(map);
var currentCamper = document.getElementById('currentCamper')

var url = window.location.href.split('/');
var id = url[url.length - 1];
console.log(id)
$.ajax({
    type: "GET",
    url: "/Trip/TripDetailsJSON",
    data: { id: id }
}).done((trip) => {
    trip.campsites.forEach((campsite) => {
        console.log(campsite.latitude, campsite.longitude)
        marker = L.marker([campsite.latitude, campsite.longitude], { icon: tentIcon }).addTo(map)

        var iframeLink = `https://www.meteoblue.com/en/weather/widget/three?geoloc=manual&lat=${campsite.latitude}&lon=${campsite.longitude}&nocurrent=0&noforecast=0&days=4&tempunit=CELSIUS&windunit=KILOMETER_PER_HOUR&layout=image` 
        var iframe = document.createElement("iframe");
        console.log(iframeLink)
        iframe.src = iframeLink;
        iframe.frameBorder = "0";
        iframe.scrolling = "NO";
        iframe.allowTransparency = "true";
        iframe.sandbox = "allow-same-origin allow-scripts allow-popups allow-popups-to-escape-sandbox";
        iframe.style.width = "460px";
        iframe.style.height = "590px";

        document.getElementById("weatherContainer").appendChild(iframe);
        marker.on('click', function () {
            window.location.href = '/Map/CampsiteDetails/' + campsite.id;
        })
    })
    deleteButton = document.getElementById("deleteButton")
    deleteButton.style.display = 'none'
    trip.tripCampers.forEach((item) => {

        if (item.camper.id == currentCamper.value && item.isOrganizer) {

            deleteButton.style.display = 'block'
        }   
    })
    var requestDiv = document.getElementById('requestContainer')
    requestDiv.style.display = 'none'
    var requestDescription = document.getElementById('expandableField')
    joinButton = document.getElementById('joinButton')
    var isInTrip = false
    trip.tripCampers.forEach((item) => {

        if (item.camper.id == currentCamper.value) {
            isInTrip = true;
        }
    })
    if (isInTrip)
    {
        joinButton.style.display = 'none'
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
           requestDiv.style.display = 'none'
           console.log(trip.id)

            
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
                        requestSpan.style.display = 'block'
                        requestDiv.style.display = 'block'
                        
                    }
                }
            )

        }

    }
    
})
