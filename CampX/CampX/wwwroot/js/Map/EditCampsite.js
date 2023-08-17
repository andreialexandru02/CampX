var map = L.map('map').setView([45.9443, 25.0094], 6);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 20,
    attribution: '© OpenStreetMap'
}).addTo(map);
var inputLat = document.get

var inputLat = document.getElementById('LatInput')
var inputLng = document.getElementById('LngInput')


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


    // Function to handle click on delete buttons
    function handleDeleteClick(event) {
        var button = event.target;
        var element = parseInt(button.getAttribute("data-imgId"));

        if (!isNaN(element)) {
            console.log("Element:", element);
            // Perform your desired action with the element
        }
    }

    var deleteButtons = document.querySelectorAll(".butoaneStergere");
    deleteButtons.forEach((button) => {
        button.addEventListener("click", handleDeleteClick);
    });
  


})
