
var map = L.map('map').setView([45.9443, 25.0094], 7);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 20,
    attribution: '© OpenStreetMap'
}).addTo(map);
map.className = "box-shadow"


const displayCampsites = () => {
    $.ajax({
        type: "get",
        url: "/Map/DisplayCampsites",
        datatype: "json",
    })
    .done((campsites) => {


        campsites.forEach(campsite => {


            marker = L.marker([campsite.latitude, campsite.longitude], { icon: tentIcon }).addTo(map)
            marker.on('click', function () {
                window.location.href = '../Map/CampsiteDetails/' + campsite.id;
            })
           
        })
    })
}
displayCampsites()
