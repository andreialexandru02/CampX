var map = L.map('map').setView([45.9443, 25.0094], 8);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 10,
    attribution: '© OpenStreetMap'
}).addTo(map);

var tentIcon = L.icon({
    iconUrl: '../Photos/icons8-tent-48.png',

    iconSize: [40, 40], // size of the icon

    iconAnchor: [20, 20], // point of the icon which will correspond to marker's location
});
const getcoordinates = () => {
    $.ajax({
        type: "get",
        url: "/map/getcoordinates",
        datatype: "json",
    })
    .done((coordinates) => {


        for (var i = 0; i < coordinates.length ; i++){

            marker = L.marker(([coordinates[i][0], coordinates[i][1]]), { icon: tentIcon }).addTo(map)
            console.log(coordinates[i][0], coordinates[i][1])
            marker.bindPopup(`${coordinates[i][0]}, ${coordinates[i][1]}`).openPopup();
        }
    });
}
getcoordinates()
