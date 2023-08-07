
var latitude = document.getElementById('Lat')
var longitude = document.getElementById('Lng')

var map = L.map('map').setView([latitude.innerText, longitude.innerText],12);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 20,
    attribution: '© OpenStreetMap'
}).addTo(map);

var tentIcon = L.icon({
    iconUrl: 'https://localhost:44364/Images/icons8-tent-48.png',

    iconSize: [40, 40], // size of the icon

    iconAnchor: [20, 20], // point of the icon which will correspond to marker's location
});


marker = L.marker([latitude.innerText, longitude.innerText], { icon: tentIcon }).addTo(map)


const ShowReviews = (id) => {
    $.ajax({
        type: "get",
        url: `/Review/ShowReviews/${id}`,
        datatype: "json",
    })
        .done((reviews) => {

            var div = document.getElementById("review-container")
            reviews.forEach(review => {

                reviewElement = document.createElement('div')
                var contentSpan = document.createElement('span')
                contentSpan.innerText = review.content
                var ratingSpan = document.createElement('span')
                ratingSpan.innerText = review.rating
                reviewElement.appendChild(ratingSpan)
                reviewElement.appendChild(contentSpan)  
                var edit = document.createElement('i')
                edit.className = 'fas fa-times'
                reviewElement.appendChild(edit)
                div.appendChild(reviewElement)
                console.log(review.rating)
                edit.onclick = () => {
                    //window.location.href = `/Review/Delete/${review.id}&${id}`
                    
                    window.location.href = '/Review/Delete/' 
                    + "?param1=" + `${review.id}` + "&param2=" + `${id}`
                }
            })
            var plus = document.createElement('i')
            plus.className = "fas fa-plus"
            div.appendChild(plus)
            plus.onclick = () => {
                window.location.href = `/Review/AddReview/${id}`
            }
            
            
        })
}
var url = window.location.href.split('/');
var id = url[url.length - 1];

ShowReviews(id)
