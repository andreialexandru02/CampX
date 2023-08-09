var map = L.map('map').setView([45.9443, 25.0094], 6);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 20,
    attribution: '© OpenStreetMap'
}).addTo(map);
var inputLat = document.get

var inputLat = document.getElementById('LatInput')
var inputLng = document.getElementById('LngInput')



//marker = L.marker([inputLat.value, inputLat.value], { icon: tentIcon }).addTo(map)
if (inputLat.value == 0.0 && inputLng.value == 0.0) {
    var marker = null
}
else {
    marker = L.marker([inputLat.value, inputLng.value], { icon: tentIcon }).addTo(map)
    
}
function onMapClick(e) {

    if (marker != null) {
        map.removeLayer(marker);
    }

    inputLat.value = e.latlng.lat.toFixed(9)
    inputLng.value = e.latlng.lng.toFixed(9)

    marker = L.marker([e.latlng.lat, e.latlng.lng], { icon: tentIcon }).addTo(map)
    

}

map.on('click', onMapClick);



