var map = L.map('map').setView([45.9443, 25.0094], 7);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 20,
    attribution: '© OpenStreetMap'
}).addTo(map);
var inputLat = document.get

var inputLat = document.getElementById('LatInput')
var inputLng = document.getElementById('LngInput')


var marker = null

function onMapClick(e) {

    if (marker != null) {
        map.removeLayer(marker);
    }

    inputLat.value = e.latlng.lat.toFixed(9)
    inputLng.value = e.latlng.lng.toFixed(9)

    marker = L.marker([e.latlng.lat, e.latlng.lng], { icon: tentIcon }).addTo(map)

    function reverseGeocode(lat, lng, callback) {
        const apiUrl = `https://nominatim.openstreetmap.org/reverse?lat=${lat}&lon=${lng}&format=json`;
        fetch(apiUrl)
            .then((response) => response.json())
            .then((data) => {
                const address = data.display_name;
                callback(address);
            })
            .catch((error) => {
                console.error("Error performing reverse geocoding:", error);
            });
    }

    // Event listener for map clicks
        const latlng = e.latlng;

        // Clear any existing popups
        map.closePopup();

        // Perform reverse geocoding
        reverseGeocode(e.latlng.lat, e.latlng.lng, function (address) {
            // Create a popup and display the address
            const popup = L.popup()
                .setLatLng(latlng)
                .setContent(address)
                .openOn(map);
        });

};
    



map.on('click', onMapClick);



