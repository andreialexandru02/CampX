var map = L.map('map').setView([45.9443, 25.0094], 6);
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
        url: "/Trip/DisplayCampsites",
        datatype: "json",
    })
        .done((campsites) => {

            var selecteCampsitesIdsList = []
            campsites.forEach(campsite => {

                
                var marker = L.marker([campsite.latitude, campsite.longitude], { icon: tentIcon }).addTo(map)

                var selectedCampsites = document.getElementById("selectedCampsites")
              
                marker.on('click', function () {                    

                    selecteCampsitesIdsList.push(campsite.id)
                    map.removeLayer(marker)
                    var campsiteDiv = document.createElement('div')
                    var img = document.createElement('img')
                    img.src = 'https://localhost:44364/Images/icons8-tent-48.png'
                    img.height = 40
                    img.width = 40
                    img.alt = 'Tent Icon'
                    img.onclick = () => {

                        window.location.href = '../Map/CampsiteDetails/' + campsite.id;
                    }
                    var campsiteName = document.createElement('span')
                    campsiteName.innerHTML = ` ${campsite.name}`
                    campsiteDiv.appendChild(img)
                    campsiteDiv.appendChild(campsiteName)
                    selectedCampsites.appendChild(campsiteDiv)
                })

            })
        })
}
displayCampsites()
