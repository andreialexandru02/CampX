/*


*/
function AcceptDelete(camperId, tripId,method) {

    $.ajax({
        type: "Post",
        url: method,
        data: 'json',
        data: {
            TripId: tripId
            ,CamperId: camperId          
        }
    }).done(() => {
       window.location.reload()
    }) 

}
