function AcceptDelete(reviewId, method) {

    $.ajax({
        type: "Post",
        url: method,
        data: {
            id: parseInt(reviewId)
        }
    }).done(() => {
        window.location.reload()
    })

}
