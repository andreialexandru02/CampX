
var latitude = document.getElementById('Lat')
var longitude = document.getElementById('Lng')

var map = L.map('map').setView([latitude.innerText, longitude.innerText],12);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 20,
    attribution: '© OpenStreetMap'
}).addTo(map);
var currentCamper = document.getElementById('currentCamper')
const ShowReviews = (id) => {
    $.ajax({
        type: "get",
        url: `/Review/ShowReviews/${id}`,
        datatype: "json",
    })
        .done((reviews) => {

            var div = document.getElementById("review-container")
            var plusIcon = document.createElement('i')
            plusIcon.className = "fas fa-plus"
            reviewSpan = document.getElementById('reviewSpan')
            reviewSpan.appendChild(plusIcon)
            var reviewInput = document.getElementById("reviewInput")
            reviewButton = document.getElementById("reviewButton")
            var reviewContent = document.getElementById("expandableField")
            var reviewRating = document.getElementById("reviewRating")
            let span = document.createElement('span')
            span.innerText = 'Review incomplet'
            span.style.color = 'red'
            reviewInput.appendChild(span)
            span.style.display = 'none'
            document.getElementById('ratingDiv').onclick = () => {
                reviewRating.disabled = false
            }
            plusIcon.onclick = () => {

                if (plusIcon.className == 'fas fa-plus') { 
                    reviewRating.value = ''
                    reviewContent.value = ''
                    
                    plusIcon.className = 'fas fa-minus'
                    reviewInput.style.display = "block";
                    
                    reviewButton.onclick = () => {                      
                        if (reviewRating.disabled === true || reviewContent.value === '') {

                            span.style.color = 'red'
                            span.innerText = 'Review incomplet'
                            span.style.display = 'block'
                        }
                        else if (reviewContent.value.length > 500) {
                            span.style.color = 'black'
                            span.innerText = "Review prea lung!"
                            span.style.display = 'block'
                        }
                        else {
                            $.ajax({
                                type: "post",
                                url: `/Review/AddReview`,
                                datatype: "json",
                                data: {
                                    campsiteid: id,
                                    CamperId: currentCamper.value,
                                    rating: reviewRating.value,
                                    content: reviewContent.value
                                }
                            })
                                .done(() => {
                                    span.innerText = 'Review trimis către aprobare'
                                    span.style.display = 'block'
                                    span.style.color = 'black'
                                    setTimeout(() => {
                                        reviewInput.style.display = "none";
                                        plusIcon.className = 'fas fa-plus'
                                        span.style.display = 'none'
                                    },1000)
                                })
                        }

                    }
                }
                else {
                    reviewInput.style.display = "none";
                    plusIcon.className = 'fas fa-plus'
                }
            }

            var sumReviews = 0
            reviews.forEach(review => {

                var authorized = review.camperId == currentCamper.value || currentCamper.value == 9
                sumReviews += review.rating
                reviewElement = document.createElement('div')
                var contentSpan = document.createElement('span')
                contentSpan.innerText = `Continut: ${review.content}`
                var ratingSpan = document.createElement('span')
                //ratingSpan.innerText = `Rating: ${review.rating}`
                var starDiv = document.createElement('div')
                for (var i = 0; i < 5; i++) {
                    var starIcon = document.createElement('i')
                    starIcon.className = 'fas fa-star'
                    if (i < review.rating) {
                        starIcon.style.color = '#EBB446'
                    }
                    starDiv.appendChild(starIcon)
                }
                ratingSpan.appendChild(starDiv)
                reviewDiv = document.createElement('div')
                reviewDiv.className = "reviewDiv"
                reviewDiv.appendChild(ratingSpan)
                reviewDiv.appendChild(document.createElement('br'))
                reviewDiv.appendChild(contentSpan)
                var deleteIcon = document.createElement('i')
                var editIcon = document.createElement('i')
                editIcon.className = 'fas fa-pencil-alt'
                deleteIcon.className = 'fas fa-times'
                reviewDiv.appendChild(editIcon)
                reviewDiv.appendChild(deleteIcon)
                reviewElement.appendChild(reviewDiv)
                if (!authorized) {

                    deleteIcon.style.display = 'none'
                    editIcon.style.display = 'none'
                }
                if (currentCamper.value == 9) {
                     editIcon.style.display = 'none'
                }
                div.appendChild(reviewElement)
                console.log(review.rating)
                deleteIcon.onclick = () => {
                    debugger
                    let result = confirm("Esti sigur ca vrei sa stergi?");
                    if (result) {

                        if (!authorized) {
                            window.location.href = '/Home/Error_Unauthorized';
                        }
                        else {
                            $.ajax({
                                type: "post",
                                url: `/Review/DeleteReview`,
                                datatype: "json",
                                data: {
                                    Id: review.id,
                                    CampsiteId: id
                                }
                            })
                                .done(() => {
                                    window.location.reload()
                                })
                        }
                    }
                }
                               
                editIcon.onclick = (e) => {
                    if (!authorized) {
                        window.location.href = '/Home/Error_Unauthorized';
                    }
                    else {

                        reviewInput.style.display = "block";
                        plusIcon.className = 'fas fa-minus'
                        reviewContent.value = e.target.previousSibling.innerText.split(': ')[1]
                        console.log(e.target.previousSibling)
                        reviewButton.onclick = () => {
                            $.ajax({
                                type: "post",
                                url: `/Review/EditReview`,
                                datatype: "json",
                                data: {
                                    Id: review.id,
                                    Campsiteid: id,
                                    CamperId: currentCamper.value,
                                    Rating: reviewRating.value,
                                    Content: reviewContent.value

                                }
                            })
                                .done(() => {
                                    window.location.reload()
                                })

                        }
                    }  
                    
                }
            })
            if (reviews.length == 0) {
                document.getElementById('ratingAverage').innerText += 'Nu exista rating!'
            }
            else {

                document.getElementById('ratingAverage').innerText += ` ${(sumReviews / reviews.length).toFixed(1)}`
            }
             
        })
            
}
marker = L.marker([latitude.innerText, longitude.innerText], { icon: tentIcon }).addTo(map)
var url = window.location.href.split('/');
var id = url[url.length - 1];
ShowReviews(id);

var deleteCampsiteButton = document.getElementById("deleteCampsiteButton");
var alreadyClicked = false;

deleteCampsiteButton.onclick = (event) => {
    event.preventDefault();
    if (!alreadyClicked) {
        alreadyClicked = true;
        $('#confirmDeleteModal').modal('show');
    }
}

document.getElementById('confirmDeleteButton').onclick = () => {
    $.ajax({
        type: "post",
        url: `/Map/DeleteCampsite/${id}`,
        datatype: "json",
    })
        .done(() => {
            window.location.href = `/Map/ShowMap`;
        });
};
document.getElementById("cancelDeleteButton").onclick = () => {
    $('#confirmDeleteModal').modal('hide');
}

// Reset alreadyClicked flag when the modal is hidden
$('#confirmDeleteModal').on('hidden.bs.modal', function () {
    alreadyClicked = false;
});
