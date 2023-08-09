var map = L.map('map').setView([45.9443, 25.0094], 6);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 20,
    attribution: '© OpenStreetMap'
}).addTo(map);

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
        marker.on('click', function () {
            window.location.href = '../Map/CampsiteDetails/' + campsite.id;
        })
    })
    var currentCamper = document.getElementById('currentCamper')
    var requestDiv = document.getElementById('requestContainer')
    requestDiv.style.display = 'none'
    var requestDescription = document.getElementById('expandableField')
    joinButton = document.getElementById('joinButton')
    requestButton = document.getElementById('requestButton')

    joinButton.onclick = () => {
        joinButton.style.display = 'none'
        requestDiv.style.display = 'block'

        requestButton.onclick = () => {
            requestDiv.style.display = 'none'
            console.log(typeof requestDescription.value)
            $.ajax({
                type: "post",
                url: '/Request/AddRequest',
                datatype: "json",
                data: {
                    Trip: trip
                    ,CamperId: currentCamper.value                 
                    ,Description: requestDescription.value ?? 'Nu a fost introdusa o descriere!'
                }
            })
                .done(() => {
                    window.location.reload()
                })
        }



    }

})