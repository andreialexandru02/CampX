
var inputLat = document.getElementById('LatInput')
var inputLng = document.getElementById('LngInput')

var map = L.map('map').setView([inputLat.value, inputLng.value], 7);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 20,
    attribution: '© OpenStreetMap'
}).addTo(map);


marker = L.marker([inputLat.value, inputLng.value], { icon: tentIcon }).addTo(map)


function onMapClick(e) {

    if (marker != null) {
        map.removeLayer(marker);
    }

    inputLat.value = e.latlng.lat.toFixed(9)
    inputLng.value = e.latlng.lng.toFixed(9)

    marker = L.marker([e.latlng.lat, e.latlng.lng], { icon: tentIcon }).addTo(map)


}

map.on('click', onMapClick);

var url = window.location.href.split('/');
var id = url[url.length - 1];
$.ajax({
    type: "GET",
    url: "/Map/EditCampsiteJSON",
    data: { id: id }
}).done((campsite) => {

   var imagesToDeleteIds = []
    campsite.imageIds.forEach((imgId) => {
        document.getElementById(`option${imgId}`).selected = false;
        var button = document.getElementById(`buton${imgId}`)
        button.onclick = () => {
            document.getElementById(`div${imgId}`).style.display = 'none'
            document.getElementById(`option${imgId}`).selected = true;
            console.log(imgId)
        }
    })
})
