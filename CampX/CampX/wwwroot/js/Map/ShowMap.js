
var map = L.map('map').setView([45.9443, 25.0094], 8);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 20,
    attribution: '© OpenStreetMap'
}).addTo(map);


var tentIcon = L.icon({
    iconUrl: 'https://localhost:44364/Images/icons8-tent-48.png',

    iconSize: [40, 40], // size of the icon

    iconAnchor: [20, 20], // point of the icon which will correspond to marker's location
});
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
