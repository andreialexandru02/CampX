
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
            div.appendChild(plusIcon)
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
                                    window.location.reload()
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

                var authorized = review.camperId == currentCamper.value
                sumReviews += review.rating
                reviewElement = document.createElement('div')
                var contentSpan = document.createElement('span')
                contentSpan.innerText = `Continut: ${review.content}`
                var ratingSpan = document.createElement('span')
                ratingSpan.innerText = `Rating: ${review.rating}`
                reviewElement.appendChild(ratingSpan)
                reviewElement.appendChild(document.createElement('br'))
                reviewElement.appendChild(contentSpan)
                var deleteIcon = document.createElement('i')
                var editIcon = document.createElement('i')
                editIcon.className = 'fas fa-pencil-alt'
                deleteIcon.className = 'fas fa-times'
                reviewElement.appendChild(editIcon)
                reviewElement.appendChild(deleteIcon)
                if (!authorized) {

                    deleteIcon.style.display = 'none'
                    editIcon.style.display = 'none'
                }
                div.appendChild(reviewElement)
                console.log(review.rating)
                deleteIcon.onclick = () => {
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
ShowReviews(id)
